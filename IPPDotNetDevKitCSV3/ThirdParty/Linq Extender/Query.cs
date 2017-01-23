using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Intuit.Ipp.LinqExtender.Abstraction;
using Intuit.Ipp.LinqExtender.Attributes;
using Intuit.Ipp.LinqExtender.Collection;

namespace Intuit.Ipp.LinqExtender
{
    
    ///<summary>
    /// Entry class for LINQ provider. Containter of the virtual methods that will be invoked on select, intsert, update, remove or get calls.
    ///</summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class Query<T> : ExpressionVisitor, IModifiableCollection<T>, IOrderedQueryable<T>, IDisposable, IQueryProvider
    {
        /// <summary>
        /// Creates a new instance of <see cref="Query{T}"/> class.
        /// </summary>
        public Query(IQueryContext<T> context)
        {
            this.context = new QueryContextImpl<T>(context);
        }

        /// <summary>
        /// Gets the current context associated with the provider.
        /// </summary>
        internal IQueryContextImpl<T> Context
        {
            get
            {
                return context;
            }
        }

        /// <summary>
        /// Gets the collection item for an index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns><typeparamref name="T"/></returns>
        public T this[int index]
        {
            get
            {
                return ((QueryCollection<T>)context.Collection).Items[index];
            }
        }

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (this as IQueryable).Provider.Execute<IList<T>>(currentExpression).GetEnumerator();
        }

        #endregion

        #region IQueryable Members
        /// <summary>
        /// Gets element type for the expression.
        /// </summary>
        public Type ElementType
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Gets the expression tree.
        /// </summary>
        public Expression Expression
        {
            get
            {
                return Expression.Constant(this);
            }
        }
        /// <summary>
        /// Gets a query provider the LINQ query.
        /// </summary>
        public IQueryProvider Provider
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region IEnumerable<Item> Members

        /// <summary>
        /// Executes the query and gets a iterator for it.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return (this as IQueryable).Provider.Execute<IList<T>>(currentExpression).GetEnumerator();
        }

        #endregion

        #region IQueryProvider Members

        /// <summary>
        /// Creates the query for type and current expression.
        /// </summary>
        /// <typeparam name="TS">currenty type passed by frameowrk</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TS> CreateQuery<TS>(Expression expression)
        {
            //this.CheckIfStartIsFirst(expression);

            // make sure there are no previous items left in the collection.
            if ((int)this.Count() > 0) this.Clear();

            this.currentExpression = expression;
            var curentMethodcall = currentExpression as MethodCallExpression;

            if (curentMethodcall != null)
            {
                string methodName = curentMethodcall.Method.Name;
                if (methodName == MethodNames.Join)
                {
                    throw new ProviderException(Messages.DirectJoinNotSupported);
                }

                if (methodName == MethodNames.Select)
                {
                    if (Buckets.Current == null)
                    {
                        Buckets.Current = BucketImpl<T>.NewInstance.Init();
                    }

                    switch (curentMethodcall.Arguments[1].NodeType)
                    {
                        case ExpressionType.Lambda:
                            LambdaExpression lambdaExpr = curentMethodcall.Arguments[1] as LambdaExpression;
                            this.HandleSelectLambda(lambdaExpr);
                            break;
                        case ExpressionType.Quote:
                            LambdaExpression lambdaExpr1 = ((curentMethodcall.Arguments[1] as System.Linq.Expressions.UnaryExpression).Operand as LambdaExpression);
                            this.HandleSelectLambda(lambdaExpr1);
                            break;
                    }
                }

                if (IsOnQueryMethodCall(curentMethodcall.Method))
                {
                    var args = new MethodCall.Parameter[curentMethodcall.Arguments.Count - 1];

                    int index = 0;

                    foreach (var arg in args)
                    {
                        var argument = curentMethodcall.Arguments[index + 1];

                        this.Visit(argument);
                        args[index] = new MethodCall.Parameter(argument.Type, this.value);

                        index++;
                    }

                    var target = (curentMethodcall.Arguments[0] as ConstantExpression).Value;
                    if (Buckets.Current == null)
                    {
                        Buckets.Current = BucketImpl<T>.NewInstance.Init();
                    }

                    // just append the method here.
                    Buckets.Current.Methods.Add(new MethodCall(target, curentMethodcall.Method, args));
                }
                else
                {
                    // Create a new bucket when Query<T>.Execute is called or it is empty for current type.
                    if ((!Buckets.ContainsKey(typeof(T))) || Buckets.Current.Processed)
                    {
                        Buckets.Current = BucketImpl<T>.NewInstance.Init();
                    }

                    this.Visit(curentMethodcall);
                }
            }

            if (typeof(T) != typeof(TS))
            {
                projectedQuery = new ProjectedQuery<T, TS>(expression, this);

                return (IQueryable<TS>)projectedQuery;
            }

            return (IQueryable<TS>)this;
        }

        private void CheckIfStartIsFirst(Expression expression)
        {
            var curentMethodcall = expression as MethodCallExpression;
            if (curentMethodcall != null)
            {
                string methodName = curentMethodcall.Method.Name;
                if (methodName == MethodNames.Select && this.currentExpression == null)
                {
                    // Throw exception since Select cannot be the first clause
                    throw new InvalidOperationException("Select clause cannot be the first clause in the LINQ query. Use Where, OrderBy befor Select.");
                }
            }
        }

        private void HandleSelectLambda(LambdaExpression lambdaExpr)
        {
            switch (lambdaExpr.Body.NodeType)
            {
                case ExpressionType.New:
                    NewExpression newExpr = lambdaExpr.Body as System.Linq.Expressions.NewExpression;
                    foreach (var item in newExpr.Arguments)
                    {
                        // Root properties
                        string name = string.Empty;
                        if (item.NodeType == ExpressionType.Parameter)
                        {
                            name = "*";
                        }
                        else
                        {
                            MemberExpression memEx = item as MemberExpression;
                            this.IsConstantAndThowException(memEx);
                            if ((memEx.Expression as MemberExpression) != null)
                            {
                                name += (memEx.Expression as MemberExpression).Member.Name + ".";
                            }

                            name += this.GetArrayPropertyName(memEx) + memEx.Member.Name;
                            if (!IsDotNetType(item.Type))
                            {
                                name += ".*";
                            }
                        }

                        Buckets.Current.SelectItems.Add(new Bucket.SelectInfo { PropertyName = name });
                    }
                    break;
                case ExpressionType.MemberAccess:
                    // Root properties
                    MemberExpression memExpr = lambdaExpr.Body as MemberExpression;
                    this.IsConstantAndThowException(memExpr);
                    string namee = string.Empty;
                    if ((memExpr.Expression as MemberExpression) != null)
                    {
                        namee += (memExpr.Expression as MemberExpression).Member.Name + ".";
                    }

                    namee += this.GetArrayPropertyName(memExpr) + memExpr.Member.Name;
                    if (!IsDotNetType(memExpr.Type))
                    {
                        namee += ".*";
                    }

                    Buckets.Current.SelectItems.Add(new Bucket.SelectInfo { PropertyName = namee });
                    break;
            }
        }

        private void IsConstantAndThowException(MemberExpression memEx)
        {
            if (memEx.Expression != null)
            {
                if (memEx.Expression.NodeType == ExpressionType.Constant)
                {
                    throw new InvalidFilterCriteriaException(string.Format("The member [{0}] used is not a property of the undnerlying type. Literal Values are not to be used in Select clause.", memEx.Member.Name));
                }
            }
        }

        private bool IsOnQueryMethodCall(MethodInfo methodInfo)
        {
            bool result = methodInfo.Name == MethodNames.Take;

            result |= methodInfo.Name == MethodNames.Skip;

            return result;
        }

        /// <summary>
        /// Creates the query for current expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>ref to IQueryable instance</returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return (this as IQueryProvider).CreateQuery<T>(expression);
        }

        /// <summary>
        /// Executes the query for current type and expression
        /// </summary>
        /// <typeparam name="TResult">Current type</typeparam>
        /// <param name="expression"></param>
        /// <returns>typed result</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)(this as IQueryProvider).Execute(expression);
        }

        /// <summary>
        /// Executes the query for current expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>object/collection</returns>
        public object Execute(Expression expression)
        {
            if (expression == null)
            {
                // do a generic select;
                //expression = (this as IEnumerable<T>).Select(x=> x).Expression;
            }

            if (expression is MethodCallExpression)
            {
                var mCallExp = (MethodCallExpression)expression;
                if (mCallExp.Method.Name == "Count")
                {
                    if (Buckets.Current == null)
                    {
                        Buckets.Current = BucketImpl<T>.NewInstance.Init();
                    }

                    Buckets.Current.Methods.Add(new MethodCall(null, mCallExp.Method, null));
                }

                if (mCallExp.Method.Name.Equals("ToIdsQuery", StringComparison.OrdinalIgnoreCase))
                {
                    if (Buckets.Current == null)
                    {
                        Buckets.Current = BucketImpl<T>.NewInstance.Init();
                    }

                    Buckets.Current.Methods.Add(new MethodCall(null, mCallExp.Method, null));
                    this.isToIdsQueryMethod = true;
                }
            }

            ProcessItem(Buckets.Current);

            if (expression is MethodCallExpression)
            {
                var mCallExp = (MethodCallExpression)expression;

                // when first , last or single is called 
                string methodName = mCallExp.Method.Name;
                if (methodName.Equals("ToIdsQuery", StringComparison.OrdinalIgnoreCase))
                {
                    return idsQuery;
                }

                Type itemGenericType = typeof(IMethodCall<T>);
                Type itemNonGenericType = typeof(IMethodCall);

                if (mCallExp.Method.ReturnType == typeof(T))
                {
                    return Utility.InvokeMethod(methodName, itemGenericType, this);
                }

                /* Try for Non Generics Result */
                object obj = Utility.InvokeMethod(methodName, itemNonGenericType, this);

                if (obj != null)
                {
                    return obj;
                }

            }

            return ((QueryCollection<T>)context.Collection).Items;
        }

        #endregion

        #region Implementation of IQuery<T>

        /// <summary>
        /// Returns a single item from the collection.
        /// </summary>
        /// <returns></returns>
        public T Single()
        {
            return context.Collection.Single();
        }

        /// <summary>
        /// Returns a single item or default value if empty.
        /// </summary>
        /// <returns></returns>
        public T SingleOrDefault()
        {
            return context.Collection.SingleOrDefault();
        }

        /// <summary>
        /// Returns the first item from the collection.
        /// </summary>
        /// <returns></returns>
        public T First()
        {
            return context.Collection.First();
        }

        /// <summary>
        /// Returns first item or default value if empty.
        /// </summary>
        /// <returns></returns>
        public T FirstOrDefault()
        {
            return context.Collection.FirstOrDefault();
        }

        /// <summary>
        /// Returns the last item from the collection.
        /// </summary>
        /// <returns></returns>
        public T Last()
        {
            return context.Collection.Last();
        }

        /// <summary>
        /// Returns last item or default value if empty.
        /// </summary>
        /// <returns></returns>
        public T LastOrDefault()
        {
            return context.Collection.LastOrDefault();
        }

        #endregion

        #region Implementation of IQuery

        /// <summary>
        /// Return true if there is any item in collection.
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            return context.Collection.Any();
        }

        /// <summary>
        /// Returns the count of items in the collection.
        /// </summary>
        /// <returns></returns>
        public object Count()
        {
            return context.Collection.Count();
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            // clean up expression object from memory.
            if (this.currentExpression != null)
            {
                currentExpression = null;
            }
            Buckets.Clear();

            Clear();
        }

        #endregion

        /// <summary>
        /// Clears out items from collection.
        /// </summary>
        public void Clear()
        {
            context.Collection.Clear();
        }

        /// <summary>
        /// internally tries to sort , if the query contains orderby statement.
        /// </summary>
        public void Sort()
        {
            if (Buckets.Current.OrderByItems != null)
            {
                foreach (var orderByInfo in Buckets.Current.OrderByItems)
                {
                    ((QueryCollection<T>)context.Collection)
                        .Sort(new QueryItemComparer<QueryObject<T>>(orderByInfo.Member.MemberInfo.Name, orderByInfo.IsAscending));
                }
            }
        }

        /// <summary>
        /// Marks an item to be removed.
        /// </summary>
        /// <param name="value">query object.</param>
        public void Remove(T value)
        {
            context.Collection.Remove(value);
        }

        /// <summary>
        /// Addes a range of items to the collection.
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            context.Collection.AddRange(items);
        }

        /// <summary>
        /// Adds list of items to the collection , optionally calls in memory sort. Used in Query<typeparamref name="T"/>.SelectItem
        /// </summary>
        /// <param name="items">collection</param>
        /// <param name="inMemorySort">true/false</param>
        public void AddRange(IEnumerable<T> items, bool inMemorySort)
        {
            context.Collection.AddRange(items);

            if (inMemorySort)
            {
                context.Collection.Sort();
            }
        }

        /// <summary>
        /// Adds a new item to the collection
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            context.Collection.Add(item);
        }

        #region Tobe overriden methods
        /// <summary>
        /// Invoked after SubmitChanges(), if there is new item in the colleciton.
        /// </summary>
        internal virtual bool AddItem(IBucket bucket)
        {
            // do nothing.
            return false;
        }
        /// <summary>
        /// Invoked after SubmitChanges(), if there are delted items in the collection.
        /// </summary>
        internal virtual bool RemoveItem(IBucket bucket)
        {
            // do nothing.
            return false;
        }
        /// <summary>
        /// Invoked after SubmitChanges(), if any of the object value is altered.
        /// </summary>
        internal virtual bool UpdateItem(IBucket bucket)
        {
            // do nothing.
            return false;
        }

        /// <summary>
        /// Called by the extender for select queries.
        /// </summary>
        /// <param name="bucket">bucekt interface.</param>
        /// <param name="items"></param>
        internal virtual void ExecuteQuery(IBucket bucket, IModifiableCollection<T> items, bool isToIdsQueryMethod, out string idsQuery)
        {
            idsQuery = string.Empty;
            // does nothing.
        }

        #endregion

        ///<summary>
        /// When called, it invokes the appropiate Query<typeparamref name="T"/> method to finalize the collection changes.
        ///</summary>
        public void SubmitChanges()
        {
            var queryColleciton = (QueryCollection<T>)context.Collection;

            var bucket = BucketImpl<T>.NewInstance.Init();

            var deletedItems = new List<QueryObject<T>>();

            foreach (var item in queryColleciton.Objects)
            {
                try
                {
                    if (item.IsNewlyAdded)
                    {
                        bool added = PerformChange(bucket, item, this.AddItem);

                        if (added)
                        {
                            // cache the item to track for update.
                            (item as IVersionItem).Commit();
                        }
                        else
                        {
                            RaiseError(String.Format("{0} add failed", bucket.Name));
                        }

                    }
                    else if (item.IsDeleted)
                    {
                        if (PerformChange(bucket, item, this.RemoveItem))
                        {
                            deletedItems.Add(item);
                        }
                        else
                        {
                            RaiseError(String.Format("{0} delete failed", bucket.Name));
                        }
                    }
                    else if (item.IsAltered)
                    {
                        if (PerformChange(bucket, item, this.UpdateItem))
                        {
                            (item as IVersionItem).Commit();
                        }
                        else
                        {
                            (item as IVersionItem).Revert();
                            RaiseError(String.Format("{0} update failed", bucket.Name));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ProviderException(ex.Message, ex);
                }
            }
            // delete the removed items. 
            foreach (var queryObject in deletedItems)
            {
                queryColleciton.Objects.Remove(queryObject);
            }
        }

        /// <summary>
        /// Visits the binary expression.
        /// </summary>
        /// <param name="expression">Target expression</param>
        /// <returns>Result expression</returns>
        public override Expression VisitBinary(BinaryExpression expression)
        {

            if (expression.NodeType == ExpressionType.AndAlso
                || expression.NodeType == ExpressionType.And
                || expression.NodeType == ExpressionType.Or
                || expression.NodeType == ExpressionType.OrElse)
            {
                var op = expression.NodeType == ExpressionType.AndAlso
                    || expression.NodeType == ExpressionType.And ? LogicalOperator.And
                    : LogicalOperator.Or;

                var temp = GetCurrentNode(parent, op);

                parent = temp;

                ++level;

                this.Visit(expression.Left);

                parent = temp;

                ++level;

                this.Visit(expression.Right);

                Buckets.Current.SyntaxStack.Pop();

                return expression;
            }

            bool singleOrExtensionCall = false;

            // for extension and single item call.
            if (Buckets.Current.SyntaxStack.Count == 0)
            {
                parent = GetCurrentNode(parent, LogicalOperator.And);
                singleOrExtensionCall = true;
            }

            Buckets.Current.Level = level;
            Buckets.Current.CurrentExpessionType = expression.NodeType;
            Buckets.Current.SyntaxStack.Peek().Level = level;

            Buckets.Current.SyntaxStack.Push(new BucketImpl<T>.TreeNodeInfo
            {
                CompoundOperator = LogicalOperator.None,
                Id = Guid.NewGuid(),
                ParentId = Buckets.Current.SyntaxStack.Peek().Id,
            });

            Buckets.Current.CurrentTreeNode = parent;

            // push the state.
            this.ProcessBinaryResult(expression);

            if (singleOrExtensionCall)
            {
                Buckets.Current.SyntaxStack.Pop();
            }

            return expression;
        }

        public override Expression VisitConstant(ConstantExpression expression)
        {
            this.value = expression.Value;
            return expression;
        }

        public override Expression VisitUnary(UnaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Not)
            {
                if (expression.Operand.NodeType == ExpressionType.And
                    || expression.Operand.NodeType == ExpressionType.AndAlso
                    || expression.Operand.NodeType == ExpressionType.Or
                    || expression.Operand.NodeType == ExpressionType.OrElse)
                {
                    throw new Exception("NOT opertor cannot be used with and operator. NOT operator can be applied only to a single expression type");
                }

                string propName = string.Empty;
                // TODO: Get navigation property and array prop names
                switch (expression.Operand.NodeType)
                {
                    case ExpressionType.Call:
                        MethodCallExpression mce = expression.Operand as MethodCallExpression;
                        if (mce.Arguments[0].NodeType == ExpressionType.Constant)
                        {
                            propName = ((expression.Operand as MethodCallExpression).Object as MemberExpression).Member.Name;
                        }
                        else
                        {
                            propName = (mce.Arguments[0] as MemberExpression).Member.Name;
                        }

                        break;
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.NotEqual:
                    case ExpressionType.Equal:
                        BinaryExpression be = expression.Operand as BinaryExpression;
                        if (be.Left.NodeType == ExpressionType.Convert)
                        {
                            propName = ((be.Left as UnaryExpression).Operand as MemberExpression).Member.Name;
                        }
                        else
                        {
                            MemberExpression me = be.Left as MemberExpression;
                            propName = me.Member.Name;
                            if (me.Expression != null)
                            {
                                if (me.Expression.NodeType == ExpressionType.ArrayIndex)
                                {
                                    if ((me.Expression as BinaryExpression) != null)
                                        propName = propName.Insert(0, ((me.Expression as BinaryExpression).Left as MemberExpression).Member.Name + ".");
                                }
                                else
                                {
                                    if ((me.Expression as MemberExpression) != null)
                                        propName = propName.Insert(0, (me.Expression as MemberExpression).Member.Name + ".");
                                }
                            }
                        }
                        break;
                }

                if (!string.IsNullOrEmpty(propName))
                {
                    Buckets.Current.NotItems.Add(new Bucket.NotInfo { PropertyName = propName });
                }

                Visit(expression.Operand);
                //this.Visit(Expression.MakeBinary(ExpressionType.NotEqual, expression.Operand, Expression.Constant(true)));
            }
            else
            {
                Visit(expression.Operand);
            }

            return expression;
        }

        public override Expression VisitMemberAccess(MemberExpression expression)
        {
            if (this.currentExpression is MethodCallExpression)
            {
                var mCall = this.currentExpression as MethodCallExpression;

                if (mCall.Method.Name == MethodNames.Group)
                {
                    this.Visit(mCall);

                    string key = Convert.ToString(this.value);

                    if (!string.IsNullOrEmpty(key))
                    {
                        Buckets.Current.Group = new Bucket.GroupByContainer { Key = key };
                    }
                }
                else if (mCall.Method.Name == MethodNames.Where)
                {
                    // Handle where clause with just a boolean member type
                    // Interpret Where(a => a.Member) as Where(a => a.Member == true)
                    if (expression.NodeType == ExpressionType.MemberAccess && expression.Type == typeof(bool))
                    {
                        Visit(Expression.MakeBinary(ExpressionType.Equal, expression, Expression.Constant(true)));
                    }
                }
            }

            return expression;
        }

        /// <summary>
        /// Visits the method call expression
        /// </summary>
        /// <param name="expression">Target expression</param>
        /// <returns>Result expression</returns>
        public override Expression VisitMethodCall(MethodCallExpression expression)
        {
            string methodName = expression.Method.Name;

            if (methodName != MethodNames.Where)
            {
                parent = null;
                level = 0;
            }

            if (methodName == MethodNames.Orderby || methodName == MethodNames.Orderbydesc || methodName == MethodNames.ThenBy)
            {
                Buckets.Current.IsAsc = methodName == MethodNames.Orderbydesc ? false : true;

                var orderbyVisitor = new OrderbyVisitor();

                orderbyVisitor.Visit(expression);

                Buckets.Current.OrderByItems.Add(new Bucket.OrderByInfo(orderbyVisitor.Member, orderbyVisitor.Suffix, Buckets.Current.IsAsc));

                return expression;
            }
            else
            {
                // Handle In Operator here
                if (methodName == MethodNames.In)
                {
                    this.HandleInOperator(expression);
                    return expression;
                }

                if (expression.Arguments.Count == 2)
                    return this.Visit(expression.Arguments[1]);
            }

            // Handle String manipulation methods StartsWith, Contains, EndsWith etc.
            if (methodName == MethodNames.StartsWith || methodName == MethodNames.EndsWith || methodName == MethodNames.Contains)
            {
                this.HandleLikeOperator(expression);
                return expression;
            }

            throw new NotImplementedException(Messages.NotImplemetedExpressionWithMultipleArguments);
        }

        private void HandleLikeOperator(MethodCallExpression expression)
        {
            string methodName = expression.Method.Name;
            MemberExpression expr = expression.Object as MemberExpression;
            string target = this.GetArrayPropertyName(expr) + expr.Member.Name;
            var parameterValue = (expression.Arguments[0] as ConstantExpression).Value;
            if (methodName == MethodNames.StartsWith)
            {
                parameterValue = parameterValue.ToString() + "%";
            }

            if (methodName == MethodNames.EndsWith)
            {
                parameterValue = "%" + parameterValue.ToString();
            }

            Buckets.Current.Methods.Add(new MethodCall(target, expression.Method, new MethodCall.Parameter[] { new MethodCall.Parameter(typeof(string), parameterValue) }));
        }

        private string GetArrayPropertyName(MemberExpression expr)
        {
            if (expr.Expression.NodeType == ExpressionType.ArrayIndex)
            {
                return ((expr.Expression as BinaryExpression).Left as MemberExpression).Member.Name + ".";
            }

            return string.Empty;
        }

        private void HandleInOperator(MethodCallExpression expression)
        {
            MemberExpression expr = expression.Arguments[0] as MemberExpression;
            string target = this.GetArrayPropertyName(expr) + expr.Member.Name;

            List<string> values = new List<string>();
            switch (expression.Arguments[1].NodeType)
            {
                case ExpressionType.NewArrayInit:
                    System.Collections.ObjectModel.ReadOnlyCollection<Expression> expressions = (expression.Arguments[1] as System.Linq.Expressions.NewArrayExpression).Expressions;
                    foreach (var item in expressions)
                    {
                        values.Add((item as ConstantExpression).Value as string);
                    }

                    break;
                case ExpressionType.MemberAccess:
                    FieldInfo fi = (expression.Arguments[1] as MemberExpression).Expression.Type.GetFields().First();
                    string[] tempValues = (string[])fi.GetValue(((expression.Arguments[1] as MemberExpression).Expression as ConstantExpression).Value);
                    values = tempValues.ToList<string>();
                    break;
            }

            MethodCall methodCall = new MethodCall(target, expression.Method, new MethodCall.Parameter[] { new MethodCall.Parameter(typeof(string[]), values.ToArray<string>()) });
            Buckets.Current.Methods.Add(methodCall);
        }

        //private void FillOptionalBucketItems(Expression expression)
        //{
        //    if (expression is MethodCallExpression)
        //    {
        //        var mCall = this.currentExpression as MethodCallExpression;

        //        if (mCall.Method.Name == MethodNames.Take)
        //        {
        //            Buckets.Current.ItemsToTake = (int)value;
        //        }
        //        else if (mCall.Method.Name == MethodNames.Skip)
        //        {
        //            Buckets.Current.ItemsToSkip = (int)value;
        //        }
        //    }
        //}

        private TreeNode GetCurrentNode(TreeNode parentNode, LogicalOperator op)
        {
            Buckets.Current.SyntaxStack.Push(new BucketImpl<T>.TreeNodeInfo
            {
                CompoundOperator = op,
                Id = Guid.NewGuid(),
                ParentId = Buckets.Current.SyntaxStack.Count > 0 ? Buckets.Current.SyntaxStack.Peek().Id : Guid.Empty,
            });


            Bucket bucket = Buckets.Current;
            TreeNode currentNode = null;

            var child = new TreeNode();

            if (parentNode == null && bucket.CurrentNode.Nodes.Count == 2)
            {
                parentNode = bucket.CurrentNode;
                // child becomes parent.
                child.Id = Guid.NewGuid();
                parentNode.ParentId = child.Id;
                child.RootImpl = op;
                child.Nodes.Add(new TreeNode.Node { Value = parentNode });
                bucket.CurrentNode = child;
                currentNode = child;
            }
            else if (parentNode != null)
            {
                child.Id = Buckets.Current.SyntaxStack.Peek().Id;
                child.RootImpl = op;
                child.ParentId = parentNode.Id;
                // make it a child.
                parentNode.Nodes.Add(new TreeNode.Node { Value = child });
                currentNode = child;
            }
            else
            {
                bucket.CurrentNode.Id = Buckets.Current.SyntaxStack.Peek().Id;
                bucket.CurrentNode.RootImpl = op;
                currentNode = bucket.CurrentNode;
            }
            return currentNode;
        }

        private Buckets<T> Buckets
        {
            get
            {
                if (queryObjects == null)
                    queryObjects = new Buckets<T>();
                return queryObjects;
            }
        }

        internal void ProcessBinaryResult(Expression expression)
        {
            var binaryExpression = expression as BinaryExpression;

            if (binaryExpression != null)
            {
                if (binaryExpression.Left is MemberExpression)
                {
                    ExtractDataFromExpression(Buckets.Current, binaryExpression.Left, binaryExpression.Right);
                }
                else
                {
                    // For enumeration comparison, parse the additional Covert(ph.something) call 
                    if (binaryExpression.Left is UnaryExpression)
                    {
                        var uExp = (UnaryExpression)binaryExpression.Left;

                        if (uExp.Operand is MethodCallExpression)
                        {
                            var methodCallExpression = (MethodCallExpression)uExp.Operand;

                            FillBucketFromMethodCall(binaryExpression, methodCallExpression);
                        }
                        else
                        {
                            ExtractDataFromExpression(Buckets.Current, uExp.Operand, binaryExpression.Right);
                        }

                    }
                    else if (binaryExpression.Left is MethodCallExpression)
                    {
                        var methodCallExpression = (MethodCallExpression)binaryExpression.Left;
                        // if there are two arguments for name and value.
                        if (methodCallExpression.Arguments.Count > 1)
                        {
                            ExtractDataFromExpression(Buckets.Current, methodCallExpression.Arguments[0],
                                                      methodCallExpression.Arguments[1]);
                        }
                        else
                        {
                            FillBucketFromMethodCall(binaryExpression, methodCallExpression);
                        }
                    }
                }
            }
            else
            {
                var methodCallExpression = expression as MethodCallExpression;

                if (methodCallExpression != null)
                {
                    FillBucketFromMethodCall(binaryExpression, methodCallExpression);
                }
            }
        }

        private void FillBucketFromMethodCall(BinaryExpression expression, MethodCallExpression methodCallExpression)
        {
            var bucketImpl = Buckets.Current;
            bucketImpl.IsDirty = true;
            bucketImpl.ClauseItemCount = bucketImpl.ClauseItemCount + 1;
            Buckets.Current.SyntaxStack.Pop();

            if (expression != null)
            {
                object value = Expression.Lambda(expression.Right).Compile().DynamicInvoke();

                var leafItem = new BucketItem
                {
                    Name = methodCallExpression.Method.Name,
                    Method = new BucketItem.ExtenderMethod
                    {
                        Name = methodCallExpression.Method.Name,
                        Arguments = methodCallExpression.Arguments,
                        Method = methodCallExpression.Method
                    }
                };

                leafItem.Values.Add(new BucketItem.QueryCondition(value, bucketImpl.Relation));
                bucketImpl.CurrentTreeNode.Nodes.Add(new TreeNode.Node() { Value = leafItem });
            }
            else
            {
                var value = Expression.Lambda(methodCallExpression.Object).Compile().DynamicInvoke() as IEnumerable<object>;

                if (value != null)
                {

                    var memberExpression = methodCallExpression.Arguments[0] as MemberExpression;

                    if (memberExpression != null)
                    {
                        string memberName = memberExpression.Member.Name;

                        if (bucketImpl.Items.ContainsKey(memberName))
                        {
                            var leafItem = new BucketItem
                            {
                                DeclaringType = memberExpression.Member.DeclaringType,
                                Name = bucketImpl.Items[memberName].Name,
                                ProperyName = bucketImpl.Items[memberName].ProperyName,
                                PropertyType = bucketImpl.Items[memberName].PropertyType,
                                Unique = bucketImpl.Items[memberName].Unique,
                                Child = bucketImpl.Items[memberName].Child,
                                Container = bucketImpl.Items[memberName].Container
                            };

                            foreach (object item in value)
                            {
                                leafItem.Values.Add(new BucketItem.QueryCondition(item, BinaryOperator.Contains));
                            }


                            bucketImpl.CurrentTreeNode.Nodes.Add(new TreeNode.Node { Value = leafItem });
                        }
                    }
                }
            }
        }

        private void ExtractDataFromExpression(BucketImpl bucket, Expression left, Expression right)
        {
            object value = Expression.Lambda(right).Compile().DynamicInvoke();

            MemberExpression memberExpression = (MemberExpression)left;
            string originalMembername = memberExpression.Member.Name;

            PropertyInfo targetProperty = null;

            targetProperty = typeof(T).GetProperty(originalMembername);
            if (targetProperty == null)
            {
                // for nested types.
                Type targetType = memberExpression.Member.DeclaringType;

                while (true)
                {
                    if (targetType.DeclaringType == null || targetType.DeclaringType == typeof(T))
                        break;
                    targetType = targetType.DeclaringType;
                }

                PropertyInfo[] infos = typeof(T).GetProperties();

                if (memberExpression.Expression.NodeType == ExpressionType.ArrayIndex)
                {
                    targetProperty = FindTargetPropertyWhereUsed(infos, (memberExpression.Expression as BinaryExpression).Left.Type);
                }
                else
                {
                    targetProperty = FindTargetPropertyWhereUsed(infos, targetType);
                }

                object nestedObj = Activator.CreateInstance(targetType);

                if (targetProperty.CanWrite)
                {
                    var property = nestedObj.GetType().GetProperty(memberExpression.Member.Name);

                    if (property == null)
                    {
                        // go deep find n.
                        object nestedChildObject = FindDeepObject(nestedObj, targetType, memberExpression.Member.Name);

                        nestedChildObject.GetType()
                            .GetProperty(memberExpression.Member.Name)
                            .SetValue(nestedChildObject, value, null);

                    }
                    else
                    {
                        property.SetValue(nestedObj, value, null);
                    }

                    // reset the value.
                    value = nestedObj;
                }
            }

            object[] attr = targetProperty.GetCustomAttributes(typeof(IgnoreAttribute), true);

            if (attr.Length == 0)
            {
                if (targetProperty.CanRead)
                {
                    bucket.IsDirty = true;
                    FillBucket(bucket, targetProperty, value, memberExpression);
                }
            }

        }

        private PropertyInfo FindTargetPropertyWhereUsed(PropertyInfo[] infos, Type targetType)
        {
            IList<PropertyInfo> compositeProperties = new List<PropertyInfo>();

            foreach (PropertyInfo property in infos)
            {
                if (!property.PropertyType.IsPrimitive && property.PropertyType.FullName.IndexOf("System") == -1 && !property.PropertyType.IsEnum)
                {
                    compositeProperties.Add(property);
                }

                if (property.PropertyType == targetType || property.Name.Equals(targetType.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return property;
                }
            }
            // try only if no properties found on the first step.
            foreach (PropertyInfo info in compositeProperties)
            {
                var ret = FindTargetPropertyWhereUsed(info.PropertyType.GetProperties(), targetType);
                if (ret != null)
                    return ret;
            }

            return null;
        }

        private static object FindDeepObject(object nestedObject, Type baseType, string propName)
        {
            PropertyInfo[] infos = baseType.GetProperties();

            foreach (var info in infos)
            {
                if (info.Name == propName)
                {
                    return nestedObject;
                }
                else
                {
                    if (info.PropertyType.GetProperties().Count() > 0)
                    {
                        try
                        {
                            object tobeNested = info.GetValue(nestedObject, null);

                            if (tobeNested == null)
                            {
                                tobeNested = Activator.CreateInstance(info.PropertyType);
                                if (info.CanWrite)
                                {
                                    info.SetValue(nestedObject, tobeNested, null);
                                }
                            }
                            return FindDeepObject(tobeNested, info.PropertyType, propName);
                        }
                        catch
                        {
                            throw new ProviderException(Messages.CouldNotFindTheNestedProperty);
                        }
                    }
                }
            }
            return null;
        }

        private bool IsMinDateTimeValue(object value)
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime == DateTime.MinValue)
            {
                return true;
            }

            return false;
        }

        private void FillBucket(BucketImpl bucket, PropertyInfo info, object value, MemberExpression memberExpression)
        {
            bucket.ClauseItemCount = bucket.ClauseItemCount + 1;

            var expression = memberExpression.Expression;

            Buckets.Current.SyntaxStack.Pop();

            string[] parts = expression.ToString().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            parts = this.RemvoeIndex(parts);

            Bucket current = bucket;
            bool nested = false;

            for (int index = 1; index < parts.Length; index++)
            {
                Type propertyType = current.Items[parts[index]].PropertyType;
                if (propertyType.IsArray)
                {
                    propertyType = propertyType.Assembly.GetType("Intuit.Ipp.Data." + parts[index]);
                }

                if (!propertyType.IsPrimitive
                    && propertyType.FullName.IndexOf("System") == -1
                    && !propertyType.IsEnum)
                {
                    if (current.Items[parts[index]].Child == null)
                    {
                        current.Items[parts[index]].Child = BucketImpl.NewInstance(propertyType).Init();
                        current.Items[parts[index]].Child.Container = current.Items[parts[index]];
                    }
                    // move on.
                    current = current.Items[parts[index]].Child;
                    nested = true;
                }
            }

            BucketItem item = null;

            if (current != null && nested)
            {
                if (info.PropertyType.IsArray)
                {
                    var a = Activator.CreateInstance(value.GetType());
                    foreach (var item1 in a.GetType().GetProperties())
                    {
                        object targetValue = item1.GetValue(value, null);

                        if (targetValue != null && !targetValue.EqualsDefault(item1.Name, value))
                        {
                            if (targetValue.GetType().Name.Equals("DateTime", StringComparison.OrdinalIgnoreCase) && !this.IsMinDateTimeValue(targetValue) || !targetValue.GetType().Name.Equals("DateTime", StringComparison.OrdinalIgnoreCase))
                            {
                                current.Items[item1.Name].Values.Add(new BucketItem.QueryCondition(targetValue, bucket.Relation));
                            }
                            else
                            {
                                current.Items[item1.Name].Values.Add(new BucketItem.QueryCondition(targetValue, bucket.Relation));
                            }
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo property in info.PropertyType.GetProperties())
                    {
                        object targetValue = property.GetValue(value, null);

                        if (targetValue != null && !targetValue.EqualsDefault(property.Name, value))
                        {
                            if (targetValue.GetType().Name.Equals("DateTime", StringComparison.OrdinalIgnoreCase) && !this.IsMinDateTimeValue(targetValue) || !targetValue.GetType().Name.Equals("DateTime", StringComparison.OrdinalIgnoreCase))
                            {
                                current.Items[property.Name].Values.Add(new BucketItem.QueryCondition(targetValue, bucket.Relation));
                            }
                        }
                    }
                }
            }
            item = parts.Length > 1 ? bucket.Items[parts[1]] : bucket.Items[info.Name];

            BucketItem leafItem;

            if (item.Child != null)
            {
                BucketItem i = item.GetActiveItem();

                leafItem = new BucketItem
                {
                    DeclaringType = i.DeclaringType,
                    Name = i.Name,
                    ProperyName = i.ProperyName,
                    PropertyType = info.PropertyType,
                    Unique = i.Unique,
                    Child = i.Child,
                    Container = i.Container
                };

                leafItem.Values.Add(new BucketItem.QueryCondition(i.Value, bucket.Relation));
            }
            else
            {
                // for getting the values directly.
                // add it to the bucket condition list.
                item.Values.Add(new BucketItem.QueryCondition(value, bucket.Relation));

                leafItem = new BucketItem
                {
                    DeclaringType = item.DeclaringType,
                    Name = item.Name,
                    ProperyName = item.ProperyName,
                    PropertyType = info.PropertyType,
                    MemberInfo = memberExpression.Member,
                    Unique = item.Unique,
                    Container = item.Container
                };

                leafItem.Values.Add(new BucketItem.QueryCondition(value, bucket.Relation));
            }
            bucket.CurrentTreeNode.Nodes.Add(new TreeNode.Node() { Value = leafItem });
        }

        private string[] RemvoeIndex(string[] parts)
        {
            List<string> strings = new List<string>();
            foreach (var item in parts)
            {
                strings.Add(item.Replace("[0]", ""));
            }

            return strings.ToArray<string>();
        }

        private static bool PerformChange(Bucket bucket, IQueryObjectImpl item, ActualMethodHandler callback)
        {
            bool success;
            // copy item
            bucket = item.FillBucket(bucket);

            if (callback != null)
                success = callback(bucket);
            else
                success = true;

            if (success && !item.IsDeleted)
                item.FillObject(bucket);

            return success;
        }

        private static void RaiseError(string message)
        {
            new ProviderException(message);
        }

        private void ProcessItem(BucketImpl item)
        {
            string idsQuerys = string.Empty;
            if (!item.Processed)
            {
                try
                {
                    ExecuteQuery(item, context.Collection, isToIdsQueryMethod, out idsQuerys);
                }
                catch (Exception ex)
                {
                    throw new ProviderException(Messages.ErrorWhileExecutingTheQuery, ex);
                }

                item.Processed = true;
            }

            this.idsQuery = idsQuerys;
        }

        internal class OrderbyVisitor : ExpressionVisitor
        {
            public string Suffix = string.Empty;

            public override Expression VisitMemberAccess(MemberExpression expression)
            {
                if (expression.Expression.NodeType == ExpressionType.Constant)
                    this.value = Expression.Lambda(expression).Compile().DynamicInvoke();
                else if (expression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    this.Suffix += string.Format(".{0}", expression.Member.Name);
                    return this.Visit(expression.Expression);
                }
                else
                    this.member = new MemberReference(expression.Member);

                return expression;
            }

            public override Expression VisitConstant(ConstantExpression expression)
            {
                value = expression.Value;
                return expression;
            }

            public override Expression VisitMethodCall(MethodCallExpression expression)
            {
                if (expression.Arguments.Count == 2)
                    return this.Visit(expression.Arguments[1]);

                return expression;
            }

            public MemberReference Member
            {
                get
                {
                    return member;
                }
            }


            private MemberReference member;
            private object value;
        }

        private object value;
        private TreeNode parent;
        private int level;
        private Expression currentExpression;
        private Buckets<T> queryObjects;
        private object projectedQuery;
        private IQueryContextImpl<T> context;

        internal string idsQuery;
        internal bool isToIdsQueryMethod;

        private delegate bool ActualMethodHandler(Bucket bucket);

        private bool IsDotNetType(System.Type type)
        {
            if (type.Name.Equals("string", StringComparison.OrdinalIgnoreCase)
                || type.Name.Equals("decimal", StringComparison.OrdinalIgnoreCase)
                || type.Name.Equals("datetime", StringComparison.OrdinalIgnoreCase)
                || type.Name.Equals("int", StringComparison.OrdinalIgnoreCase)
                || type.Name.Equals("int32", StringComparison.OrdinalIgnoreCase)
                || type.Name.Equals("float", StringComparison.OrdinalIgnoreCase)
                || type.IsEnum)
            {
                return true;
            }

            return false;
        }
    }
}

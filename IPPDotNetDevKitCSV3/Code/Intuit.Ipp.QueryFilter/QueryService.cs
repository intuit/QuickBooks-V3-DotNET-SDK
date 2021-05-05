////********************************************************************
// <copyright file="QueryService.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// <summary>Contains methods used to parse the expression tree and execute the 
// query generated and return the result.</summary>
////********************************************************************

using System.ComponentModel;
using System.Xml.Serialization;

namespace Intuit.Ipp.QueryFilter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.QueryFilter.Properties;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Contains methods used to parse the expression tree and execute the query generated and return the result.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class QueryService<T>   //ExpressionVisitor, IQueryContext<T>
    {
        #region Private Members

        /// <summary>
        /// Query string builder.
        /// </summary>
        private StringBuilder queryBuilder;

        /// <summary>
        /// Select string builder.
        /// </summary>
        private StringBuilder selectBuilder;

        /// <summary>
        /// Count string builder.
        /// </summary>
        private StringBuilder countBuilder;

        /// <summary>
        /// OrderBy string builder.
        /// </summary>
        private StringBuilder orderByBuilder;

        /// <summary>
        /// Where string builder.
        /// </summary>
        private StringBuilder whereBuilder;

        /// <summary>
        /// From string builder.
        /// </summary>
        private StringBuilder fromBuilder;

        /// <summary>
        /// Pagination string builder.
        /// </summary>
        private StringBuilder pageBuilder;

        /// <summary>
        /// Service Context.
        /// </summary>
        private ServiceContext serviceContext;

        ///// <summary>
        ///// Operation Type.
        ///// </summary>
        //private QueryOperationType operationType;

        /// <summary>
        /// Rest Request Handler.
        /// </summary>
        private IRestHandler restHandler;

        /// <summary>
        /// Response Serializer.
        /// </summary>
        private IEntitySerializer responseSerializer;

        ///// <summary>
        ///// Collection of Not expressions.
        ///// </summary>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //private IEnumerable<LinqExtender.Ast.Expression> notExpressions;

        #endregion

        /// <summary>
        /// Initializes a new instance of the QueryService class.
        /// </summary>
        /// <param name="serviceContext">The ServiceContext class.</param>
        public QueryService(ServiceContext serviceContext)
        {
            this.serviceContext = serviceContext;

            // Set the Service type to either QBO by calling a method.
            this.serviceContext.UseDataServices();

            this.restHandler = new SyncRestHandler(this.serviceContext);
            this.responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
            this.queryBuilder = new StringBuilder();
            this.selectBuilder = new StringBuilder();
            this.countBuilder = new StringBuilder();
            this.orderByBuilder = new StringBuilder();
            this.whereBuilder = new StringBuilder();
            this.fromBuilder = new StringBuilder();
            this.pageBuilder = new StringBuilder();
            //this.notExpressions = new List<LinqExtender.Ast.Expression>();
        }

        /// <summary>
        /// Executes the Ids Query and returns the response.
        /// </summary>
        /// <param name="idsQuery">The string representation of ids query for getting just the count of records.</param>
        /// <param name="queryOperationType">Query Operation Type. Default value is query.</param>
        /// <returns>Count of records.</returns>
        public long ExecuteIdsQueryForCount(string idsQuery, QueryOperationType queryOperationType = QueryOperationType.query)
        {
            // Validate Parameter
            if (string.IsNullOrWhiteSpace(idsQuery))
            {
                throw new InvalidParameterException(string.Format(CultureInfo.InvariantCulture, "The parameter idsQuery cannot be null or empty."));
            }



            // Buid the service uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, queryOperationType);

            // Creates request parameters
            RequestParameters parameters = null;

            parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONTEXT);


            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, idsQuery);
            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            // Check whether the response is null or empty and throw communication exception.
            CoreHelper.CheckNullResponseAndThrowException(response);

            // Deserialize object
            IntuitResponse restResponse = (IntuitResponse)this.responseSerializer.Deserialize<IntuitResponse>(response);
            QueryResponse queryResponse = restResponse.AnyIntuitObject as QueryResponse;


            int totalCount = queryResponse.totalCount;




            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method ExecuteIdsQuery.");

            return totalCount;

        }

        /// <summary>
        /// Executes the Ids Query and returns the response.
        /// </summary>
        /// <param name="idsQuery">The string representation of ids query.</param>
        /// <param name="queryOperationType">Query Operation Type. Default value is query.</param>
        /// <returns>ReadOnly Collection fo items of type T.</returns>
        public System.Collections.ObjectModel.ReadOnlyCollection<T> ExecuteIdsQuery(string idsQuery, QueryOperationType queryOperationType = QueryOperationType.query)
        {
            // Validate Parameter
            if (string.IsNullOrWhiteSpace(idsQuery))
            {
                throw new InvalidParameterException(string.Format(CultureInfo.InvariantCulture, "The parameter idsQuery cannot be null or empty."));
            }

            // Buid the service uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, queryOperationType);

            // Creates request parameters
            RequestParameters parameters = null;

            parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONTEXT);


            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, idsQuery);
            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            // Check whether the response is null or empty and throw communication exception.
            CoreHelper.CheckNullResponseAndThrowException(response);

            // Deserialize object
            IntuitResponse restResponse = (IntuitResponse)this.responseSerializer.Deserialize<IntuitResponse>(response);
            QueryResponse queryResponse = restResponse.AnyIntuitObject as QueryResponse;

            if (idsQuery.ToLower().Contains("count(*)"))
            {
                int totalCount = queryResponse.totalCount;
                List<T> dummyCountCollection = new List<T>();
                for (int i = 0; i < totalCount; i++)
                {
                    dummyCountCollection.Add((T)Activator.CreateInstance(typeof(T)));
                }

                System.Collections.ObjectModel.ReadOnlyCollection<T> countCollection = new System.Collections.ObjectModel.ReadOnlyCollection<T>(dummyCountCollection);
                return countCollection;
            }

            List<T> entities = new List<T>();

            if (queryResponse.maxResults > 0)

            {
                object tempEntities = queryResponse.AnyIntuitObjects;
                if (tempEntities != null)
                {
                    object[] tempEntityArray = (object[])tempEntities;

                    if (tempEntityArray.Length > 0)
                    {
                        foreach (object item in tempEntityArray)
                        {
                            entities.Add((T)item);
                        }
                    }
                }
            }

            /* Type type = queryResponse.GetType();
             List<T> entities = new List<T>();

             PropertyInfo[] propertyInfoArray = type.GetProperties();

             foreach (PropertyInfo propertyInfo in propertyInfoArray)
             {
                 if (true == propertyInfo.PropertyType.)
                 {
                     object tempEntities = propertyInfo.GetValue(queryResponse, null);
                     if (tempEntities != null)
                     {
                         object[] tempEntityArray = (object[])tempEntities;

                         if (tempEntityArray.Length > 0)
                         {
                             foreach (object item in tempEntityArray)
                             {
                                 entities.Add((T)item);
                             }
                         }
                     }

                     break;
                 }
             }*/

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method ExecuteIdsQuery.");
            System.Collections.ObjectModel.ReadOnlyCollection<T> readOnlyCollection = new System.Collections.ObjectModel.ReadOnlyCollection<T>(entities);
            return readOnlyCollection;
        }

        /// <summary>
        /// Entity Service supports multiple queries within a request. Use this method to perform multiple entity query operation.
        /// </summary>
        /// <param name="queryOperationValues">The simple query language string collection.</param>
        /// <typeparam name="TSource">Where TSource is IEntity.</typeparam>
        /// <returns>Returns a collection of entities for individual simple query.</returns>
        /// <exception cref="InvalidParameterException">If the parameter is null or empty or exceeds a maximum of five(5) queries.</exception>
        /// <exception cref="ValidationException">If the query syntax is incorrect.</exception>
        /// <exception cref="SerializationException">If there were serialization issues with the response from the service.</exception>
        /// <exception cref="IdsException">When service returned with an error information.</exception>
        /// <example>
        /// Usage:
        /// Use the query service created and invoke the ToIdsQuery method to obtain the simple query as string.
        /// <code>
        /// string customerQueryValue = customerContext.Where(c => c.MetaData.CreateTime > this.dateTime).ToIdsQuery();
        /// string invoiceQueryValue = invoiceContext.Select(i => new { i.Id, i.status }).ToIdsQuery();
        /// </code>
        /// Invoke the ExecuteMultipleEntityQueries method with the read only collection of the queries. This method can throw exception so surround the method with a try catch block.
        /// <code>
        /// <![CDATA[List<string> values = new List<string> { customerQueryValue, invoiceQueryValue };]]>
        /// try
        /// {
        ///     <![CDATA[ReadOnlyCollection<ReadOnlyCollection<IEntity>> results = customerContext.ExecuteMultipleEntityQueries<IEntity>(values.AsReadOnly());]]>
        ///     Iterate through the values obtained:
        ///     foreach (var item in results)
        ///     {
        ///         // Read the values
        ///     }
        /// }
        /// catch(IdsException)
        /// {
        ///     // Perform logic here
        /// }
        /// </code>
        /// </example>
        public System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.ObjectModel.ReadOnlyCollection<TSource>> ExecuteMultipleEntityQueries<TSource>(System.Collections.ObjectModel.ReadOnlyCollection<string> queryOperationValues) where TSource : IEntity
        {
            if (queryOperationValues == null || queryOperationValues.Count == 0)
            {
                throw new InvalidParameterException(string.Format(CultureInfo.InvariantCulture, "The parameter queryOperationValues cannot be null or empty."));
            }

            if (queryOperationValues.Count > 5)
            {
                throw new InvalidParameterException(string.Format(CultureInfo.InvariantCulture, "Query Count exceeded. A maximum of 5 queries can be provided at a time."));
            }

            string idsQuery = string.Empty;
            foreach (var item in queryOperationValues)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    idsQuery += item + ";";
                }
            }

            // Buid the service uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/query", CoreConstants.VERSION, this.serviceContext.RealmId);

            // Creates request parameters
            RequestParameters parameters = null;

            parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONTEXT);


            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, idsQuery);
            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            // Check whether the response is null or empty and throw communication exception.
            CoreHelper.CheckNullResponseAndThrowException(response);

            // Deserialize object
            IntuitResponse restResponse = (IntuitResponse)this.responseSerializer.Deserialize<IntuitResponse>(response);
            QueryResponse[] queryResponses = restResponse.AnyIntuitObjects as QueryResponse[];

            List<System.Collections.ObjectModel.ReadOnlyCollection<TSource>> returnValues = new List<System.Collections.ObjectModel.ReadOnlyCollection<TSource>>();

            foreach (var queryResponse in queryResponses)
            {
                Type type = queryResponse.GetType();
                List<TSource> entities = new List<TSource>();

                PropertyInfo[] propertyInfoArray = type.GetProperties();

                foreach (PropertyInfo propertyInfo in propertyInfoArray)
                {
                    if (true == propertyInfo.PropertyType.IsArray)
                    {
                        object tempEntities = propertyInfo.GetValue(queryResponse, null);
                        if (tempEntities != null)
                        {
                            object[] tempEntityArray = (object[])tempEntities;

                            if (tempEntityArray.Length > 0)
                            {
                                foreach (object item in tempEntityArray)
                                {
                                    entities.Add((TSource)item);
                                }
                            }
                        }

                        break;
                    }
                }

                returnValues.Add(entities.AsReadOnly());
            }

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindAll.");
            return returnValues.AsReadOnly();
        }

        //#region IQueryContext<T> methods

        ///// <summary>
        ///// Executes the LinqExtender expression by converting it to Ids Query and return the response.
        ///// </summary>
        ///// <param name="expression">The LinqExtender expression.</param>
        ///// <param name="isToIdsQueryMethod">If true executes the ids query and returns the response else does not execute the ids query.</param>
        ///// <param name="idsQuery">The generated ids query.</param>
        ///// <returns>A collection of items of type T.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //public IEnumerable<T> Execute(LinqExtender.Ast.Expression expression, bool isToIdsQueryMethod, out string idsQuery)
        //{
        //    this.queryBuilder.Clear();
        //    this.countBuilder.Clear();
        //    this.selectBuilder.Clear();
        //    this.orderByBuilder.Clear();
        //    this.whereBuilder.Clear();
        //    this.fromBuilder.Clear();
        //    this.pageBuilder.Clear();
        //    this.notExpressions = (expression as LinqExtender.Ast.BlockExpression).Expressions.Where(e => e.CodeType == CodeType.NotExpression);

        //    this.Visit(expression);
        //    if (!string.IsNullOrWhiteSpace(this.selectBuilder.ToString()))
        //    {
        //        this.queryBuilder.Append(this.selectBuilder.ToString().TrimEnd(new char[] { ',' }));
        //    }
        //    else
        //    {
        //        this.queryBuilder.Append("*");
        //    }

        //    string count = this.countBuilder.ToString();
        //    if (!string.IsNullOrWhiteSpace(count))
        //    {
        //        var queryValue = this.queryBuilder.ToString();
        //        var pattern = @"(?<=^|\s)\*(?=\s|$)"; //find asterisk as whole word
        //        queryValue = System.Text.RegularExpressions.Regex.Replace(queryValue, pattern, count);
        //        this.queryBuilder.Clear();
        //        this.queryBuilder.Append(queryValue);
        //    }

        //    this.queryBuilder.Append(this.fromBuilder);

        //    if (!string.IsNullOrWhiteSpace(this.whereBuilder.ToString()))
        //    {
        //        string whereString = this.whereBuilder.ToString();
        //        if (whereString.StartsWith(" AND", StringComparison.OrdinalIgnoreCase))
        //        {
        //            //whereString = whereString.TrimStart(new char[] { ' ', 'A', 'N', 'D' });
        //            whereString = whereString.TrimStart().Remove(0, 3);
        //        }

        //        this.queryBuilder.Append(" WHERE " + whereString);
        //    }

        //    if (!string.IsNullOrWhiteSpace(this.orderByBuilder.ToString()))
        //    {
        //        this.queryBuilder.Append(" ORDER BY " + this.orderByBuilder.ToString().TrimStart(new char[] { ',' }));
        //    }

        //    if (!string.IsNullOrWhiteSpace(this.pageBuilder.ToString()))
        //    {
        //        this.queryBuilder.Append(this.pageBuilder);
        //    }

        //    string query = idsQuery = this.queryBuilder.ToString();
        //    if (isToIdsQueryMethod)
        //    {
        //        return new List<T>();
        //    }

        //    return this.ExecuteIdsQuery(query, this.operationType);
        //}

        //#endregion

        //#region ExpressionVisitor methods

        ///// <summary>
        ///// Visit Select Call expression.
        ///// </summary>
        ///// <param name="selectExpression">Select expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitSelectCallExpression(LinqExtender.Ast.SelectExpression selectExpression)
        //{
        //    this.selectBuilder.Append(selectExpression.PropertyName + ",");
        //    return selectExpression;
        //}

        ///// <summary>
        ///// Visit Type Expression.
        ///// </summary>
        ///// <param name="expression">Type expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitTypeExpression(LinqExtender.Ast.TypeExpression expression)
        //{
        //    // For Report entities the format of the query string is different
        //    string format = string.Empty;
        //    if (expression.Type.UnderlyingType.BaseType.Name.Equals("ReportQueryBase"))
        //    {
        //        this.operationType = QueryOperationType.report;

        //        // TODO: Uncomment this if the query language is different for report and query
        //        // format = "REPORT {0} WITH ";
        //        format = " {0} ";
        //    }
        //    else if (expression.Type.UnderlyingType.Name.Equals("ChangeData"))
        //    {
        //        this.operationType = QueryOperationType.changedata;
        //    }
        //    else
        //    {
        //        this.operationType = QueryOperationType.query;
        //        format = " FROM {0} ";
        //    }

        //    this.queryBuilder.Append("Select ");
        //    this.fromBuilder.Append(string.Format(CultureInfo.InvariantCulture, format, expression.Type.Name));
        //    return expression;
        //}

        ///// <summary>
        ///// Visit Lambda Expression.
        ///// </summary>
        ///// <param name="expression">Lambda expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitLambdaExpression(LinqExtender.Ast.LambdaExpression expression)
        //{
        //    this.Visit(expression.Body);
        //    return expression;
        //}

        ///// <summary>
        ///// Visit Binary Expression.
        ///// </summary>
        ///// <param name="expression">Binary expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitBinaryExpression(LinqExtender.Ast.BinaryExpression expression)
        //{
        //    this.Visit(expression.Left);
        //    this.whereBuilder.Append(GetBinaryOperator(expression.Operator));
        //    this.Visit(expression.Right);
        //    return expression;
        //}

        ///// <summary>
        ///// Visit Logical Expression.
        ///// </summary>
        ///// <param name="expression">Logical expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitLogicalExpression(LinqExtender.Ast.LogicalExpression expression)
        //{
        //    this.Visit(expression.Left);
        //    this.WriteLogicalOperator(expression.Operator);
        //    this.Visit(expression.Right);
        //    return expression;
        //}

        ///// <summary>
        ///// Visit Member Expression.
        ///// </summary>
        ///// <param name="expression">Member expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitMemberExpression(LinqExtender.Ast.MemberExpression expression)
        //{
        //    string name = expression.FullName.TrimStart(new char[] { '.' });
        //    if (this.IsNotUsed(name))
        //    {
        //        this.whereBuilder.Append(" NOT ");
        //    }

        //    Type t = new ReferenceType().GetType();
        //    if (expression.DeclaringType == t && name.Contains(".Value"))
        //        name = name.Replace(".Value", "");

        //    this.whereBuilder.Append(name);
        //    return expression;
        //}

        ///// <summary>
        ///// Visit Literal Expression.
        ///// </summary>
        ///// <param name="expression">Literal expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitLiteralExpression(LinqExtender.Ast.LiteralExpression expression)
        //{
        //    this.WriteValue(expression.Type, expression.Value);
        //    return expression;
        //}

        ///// <summary>
        ///// Visit OrderBy Expression.
        ///// </summary>
        ///// <param name="expression">OrderBy expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitOrderbyExpression(LinqExtender.Ast.OrderbyExpression expression)
        //{
        //    this.orderByBuilder.Append(string.Format(CultureInfo.InvariantCulture, ", {0}{1} {2} ", expression.Member.Name, expression.Suffix, expression.Ascending ? " " : " DESC "));
        //    return expression;
        //}

        ///// <summary>
        ///// Visit MethodCall Expression.
        ///// </summary>
        ///// <param name="methodCallExpression">MethodCall expression.</param>
        ///// <returns>The LinqExtender Expression.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //internal override LinqExtender.Ast.Expression VisitMethodCallExpression(LinqExtender.Ast.MethodCallExpression methodCallExpression)
        //{
        //    if (methodCallExpression.IsTake)
        //    {
        //        this.pageBuilder.Append(string.Format(CultureInfo.InvariantCulture, " maxResults {0} ", methodCallExpression.Paramters[0].Value));
        //    }

        //    if (methodCallExpression.IsSkip)
        //    {
        //        int skip = Convert.ToInt32(methodCallExpression.Paramters[0].Value, CultureInfo.InvariantCulture);
        //        this.pageBuilder.Append(string.Format(CultureInfo.InvariantCulture, " startPosition {0} ", (skip + 1)));
        //    }

        //    if (methodCallExpression.Method.Name == "StartsWith" || methodCallExpression.Method.Name == "EndsWith" || methodCallExpression.Method.Name == "Contains")
        //    {
        //        this.whereBuilder.Append(" AND ");
        //        if (this.IsNotUsed(methodCallExpression.Target.ToString()))
        //        {
        //            this.whereBuilder.Append(" NOT ");
        //        }

        //        if (methodCallExpression.Method.Name == "Contains")
        //        {
        //            this.whereBuilder.Append(string.Format(CultureInfo.InvariantCulture, " {0} {1} '%{2}%' ", methodCallExpression.Target, "like", methodCallExpression.Paramters[0].Value));
        //        }
        //        else
        //        {
        //            this.whereBuilder.Append(string.Format(CultureInfo.InvariantCulture, " {0} {1} '{2}' ", methodCallExpression.Target, "like", methodCallExpression.Paramters[0].Value));
        //        }
        //    }

        //    if (methodCallExpression.Method.Name == "In")
        //    {
        //        string[] values = methodCallExpression.Paramters[0].Value as string[];
        //        string inValues = string.Empty;
        //        foreach (var item in values)
        //        {
        //            inValues += string.Format(CultureInfo.InvariantCulture, "'{0}',", item);
        //        }

        //        inValues = inValues.TrimEnd(new char[] { ',' });
        //        this.whereBuilder.Append(" AND ");
        //        if (this.IsNotUsed(methodCallExpression.Target.ToString()))
        //        {
        //            this.whereBuilder.Append(" NOT ");
        //        }

        //        this.whereBuilder.Append(string.Format(CultureInfo.InvariantCulture, " {0} {1} ({2}) ", methodCallExpression.Target, "IN", inValues));
        //    }

        //    if (methodCallExpression.Method.Name == "Count")
        //    {
        //        this.countBuilder.Append(" COUNT(*) ");
        //    }

        //    return methodCallExpression;
        //}

        //#endregion

        ///// <summary>
        ///// Gets the string representation of the BinaryOperator.
        ///// </summary>
        ///// <param name="operator">The BinaryOperator enum value.</param>
        ///// <returns>String representation of the binary operator.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //private static string GetBinaryOperator(BinaryOperator @operator)
        //{
        //    switch (@operator)
        //    {
        //        case BinaryOperator.Contains:
        //            return " like ";
        //        case BinaryOperator.Equal:
        //            return " = ";
        //        case BinaryOperator.GreaterThan:
        //            return " > ";
        //        case BinaryOperator.GreaterThanEqual:
        //            return " >= ";
        //        case BinaryOperator.LessThan:
        //            return " < ";
        //        case BinaryOperator.LessThanEqual:
        //            return " <= ";
        //        case BinaryOperator.NotEqual:
        //            return " != ";
        //    }

        //    throw new ArgumentException("Invalid binary operator");
        //}

        ///// <summary>
        ///// Writes the logical operator value to the query builder.
        ///// </summary>
        ///// <param name="logicalOperator">The Logical Operator enum value.</param>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //private void WriteLogicalOperator(LogicalOperator logicalOperator)
        //{
        //    this.whereBuilder.Append(string.Format(CultureInfo.InvariantCulture, " {0} ", logicalOperator.ToString().ToUpper(CultureInfo.InvariantCulture)));
        //}

        ///// <summary>
        ///// Writes the literal value to the query builder.
        ///// </summary>
        ///// <param name="type">The type of the value.</param>
        ///// <param name="value">The value of the object.</param>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //private void WriteValue(TypeReference type, object value)
        //{
        //    if (type.UnderlyingType == typeof(bool))
        //    {
        //        this.whereBuilder.Append(value);
        //    }
        //    else
        //    {
        //        if (type.UnderlyingType.IsEnum)
        //        {
        //            this.whereBuilder.Append(string.Format(CultureInfo.InvariantCulture, "'{0}'", StringValueOf(Enum.ToObject(type.UnderlyingType, value) as Enum)));
        //        }
        //        else
        //        {
        //            this.whereBuilder.Append(string.Format(CultureInfo.InvariantCulture, "'{0}'", this.GetIdsDateTimeFormat(value)));
        //        }
        //    }
        //}

        /// <summary>
        /// Returns the XmlEnumAttribute name if present else returns the enum object.
        /// Add any specific types in switch case if needed in future
        /// </summary>
        /// <param name="value">The value of the object.</param>
        private object StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            XmlEnumAttribute[] attributes = (XmlEnumAttribute[])fi.GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Name;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Gets the string value of the date time if the parameter is of type datetime.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>String if conversion was successful or else the value itself.</returns>
        private object GetIdsDateTimeFormat(object value)
        {
            // Generate proper DateTime string
            try
            {
                DateTime dt = (DateTime)value;
                if (dt != null && dt != DateTime.MinValue)
                {
                    // TODO: dt.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
                    // For new DateTime(2012, 06, 01) the output is 2012-06-01T00:00:00
                    string returnValue = dt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK", CultureInfo.InvariantCulture);
                    return returnValue;
                }
            }
            catch (Exception)
            {
                return value;
            }

            return value;
        }

        ///// <summary>
        ///// Indicates whether the property name has been used with the NOT operator.
        ///// </summary>
        ///// <param name="propName">Name of the property.</param>
        ///// <returns>True if the property has been used with the NOT operator else false.</returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //private bool IsNotUsed(string propName)
        //{
        //    foreach (LinqExtender.Ast.NotExpression item in this.notExpressions)
        //    {
        //        if (item.PropertyName.Equals(propName))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// TODO: Update summary
        ///// </summary>
        ///// <param name="exprssion"></param>
        ///// <returns></returns>
        //[Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        //public IEnumerable<T> Execute(LinqExtender.Ast.Expression exprssion)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

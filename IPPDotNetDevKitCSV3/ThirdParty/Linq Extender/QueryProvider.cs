////********************************************************************
//https://github.com/mehfuzh/LinqExtender/blob/master/License.txt
//Copyright (c) 2007- 2010 LinqExtender Toolkit Project. 
//Project Modified by Intuit
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
// 
////********************************************************************

using System.Collections.Generic;
using Intuit.Ipp.LinqExtender.Abstraction;
using Intuit.Ipp.LinqExtender.Fluent;
using System;

namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class QueryProvider<T> : Query<T>
    {
        public QueryProvider(IQueryContext<T> context)
            : base(context)
        {
            // intentionally left blank
        }

        internal override void ExecuteQuery(IBucket bucket, IModifiableCollection<T> items, bool isToIdsQueryMethod, out string idsQuery)
        {
            var fluentBucket = FluentBucket.As(bucket);
            var expressionStack = new Stack<Ast.Expression>();

            var blockExpression = new Ast.BlockExpression();

            blockExpression.Expressions.Add(new Ast.TypeExpression(typeof(T)));

            if (fluentBucket.IsDirty)
            {
                var lambda = new Ast.LambdaExpression(typeof(T));

                fluentBucket.ExpressionTree
                    .DescribeContainerAs(expressionStack)
                    .Root((stack, operatorType) =>
                    {
                        var logical = new Ast.LogicalExpression(operatorType);

                        var left = stack.Pop();

                        MarkAsChild(left);

                        logical.Left = left;
                        stack.Push(logical);
                    })
                    .EachLeaf((stack, item) =>
                    {
                        var binary = new Ast.BinaryExpression(item.Operator)
                        {
                            Left = new Ast.MemberExpression(item),
                            Right = new Ast.LiteralExpression(item.PropertyType, item.Value)
                        };

                        stack.Push(binary);

                    })
                    .End(stack =>
                    {
                        var binary = stack.Pop();

                        if (stack.Count > 0
                            && stack.Peek().CodeType == CodeType.LogicalExpression)
                        {
                            var logical = stack.Pop() as Ast.LogicalExpression;
                            logical.Right = binary;

                            // nested.
                            if (Peek(stack) as Ast.LogicalExpression != null)
                            {
                                MarkAsChild(logical);
                            }

                            stack.Push(logical);
                        }
                        else
                        {
                            stack.Push(binary);
                        }

                    })
                    .Execute();

                lambda.Body = expressionStack.Pop();
                blockExpression.Expressions.Add(lambda);
            }

            fluentBucket.Method.ForEach((method) =>
            {
                blockExpression.Expressions.Add(new Ast.MethodCallExpression(method));
            });

            fluentBucket.Entity.OrderBy.IfUsed(() =>
                fluentBucket.Entity.OrderBy.ForEach.Process((member, suffix, asending) =>
                {
                    blockExpression.Expressions.Add(new Ast.OrderbyExpression(member, suffix, asending));
                }));

            fluentBucket.Entity.Select.ForEach.Process((select) =>
                {
                    blockExpression.Expressions.Add(new Ast.SelectExpression(select));
                });

            fluentBucket.Entity.Nots.ForEach.Process((not) =>
                {
                    blockExpression.Expressions.Add(new Ast.NotExpression(not));
                });

            items.AddRange(Context.Execute(blockExpression, isToIdsQueryMethod, out idsQuery));
        }

        private void MarkAsChild(Ast.Expression childExpression)
        {
            if (childExpression.CodeType == CodeType.LogicalExpression)
            {
                ((Ast.LogicalExpression)childExpression).IsChild = true;
            }
        }

        public Ast.Expression Peek(Stack<Ast.Expression> expressionStack)
        {
            return expressionStack.Count > 0 ? expressionStack.Peek() : null;
        }
    }
}

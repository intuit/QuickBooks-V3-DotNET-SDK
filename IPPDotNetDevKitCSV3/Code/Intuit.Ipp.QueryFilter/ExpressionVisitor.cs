////********************************************************************
// <copyright file="ExpressionVisitor.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>Contains methods used to parse the expression tree.</summary>
////********************************************************************

using Intuit.Ipp.LinqExtender;
using System.Runtime.CompilerServices;


namespace Intuit.Ipp.QueryFilter
{
    using System;

    /// <summary>
    /// Contains methods used to parse the expression tree.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class ExpressionVisitor
    {
        /// <summary>
        /// Visits the root expression node.
        /// </summary>
        /// <param name="expression">The root expression.</param>
        /// <returns>The LinqExtender expression class.</returns>
        internal LinqExtender.Ast.Expression Visit(LinqExtender.Ast.Expression expression)
        {
            switch (expression.CodeType)
            {
                case CodeType.BlockExpression:
                    return this.VisitBlockExpression((LinqExtender.Ast.BlockExpression)expression);
                case CodeType.TypeExpression:
                    return this.VisitTypeExpression((LinqExtender.Ast.TypeExpression)expression);
                case CodeType.LambdaExpresion:
                    return this.VisitLambdaExpression((LinqExtender.Ast.LambdaExpression)expression);
                case CodeType.LogicalExpression:
                    return this.VisitLogicalExpression((LinqExtender.Ast.LogicalExpression)expression);
                case CodeType.BinaryExpression:
                    return this.VisitBinaryExpression((LinqExtender.Ast.BinaryExpression)expression);
                case CodeType.LiteralExpression:
                    return this.VisitLiteralExpression((LinqExtender.Ast.LiteralExpression)expression);
                case CodeType.MemberExpression:
                    return this.VisitMemberExpression((LinqExtender.Ast.MemberExpression)expression);
                case CodeType.OrderbyExpression:
                    return this.VisitOrderbyExpression((LinqExtender.Ast.OrderbyExpression)expression);
                case CodeType.MethodCallExpression:
                    return this.VisitMethodCallExpression((LinqExtender.Ast.MethodCallExpression)expression);
                case CodeType.SelectExpression:
                    return this.VisitSelectCallExpression((LinqExtender.Ast.SelectExpression)expression);
                case CodeType.NotExpression:
                    return expression;
            }

            throw new ArgumentException("Expression type is not supported");
        }

        /// <summary>
        /// Visit Select Call expression.
        /// </summary>
        /// <param name="selectExpression">Select expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitSelectCallExpression(LinqExtender.Ast.SelectExpression selectExpression)
        {
            return selectExpression;
        }

        /// <summary>
        /// Visit Type Expression.
        /// </summary>
        /// <param name="typeExpression">Type expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitTypeExpression(LinqExtender.Ast.TypeExpression typeExpression)
        {
            return typeExpression;
        }

        /// <summary>
        /// Visit Block Expression.
        /// </summary>
        /// <param name="blockExpression">Block expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitBlockExpression(LinqExtender.Ast.BlockExpression blockExpression)
        {
            foreach (var expression in blockExpression.Expressions)
            {
                this.Visit(expression);
            }

            return blockExpression;
        }

        /// <summary>
        /// Visit MethodCall Expression.
        /// </summary>
        /// <param name="methodCallExpression">MethodCall expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitMethodCallExpression(LinqExtender.Ast.MethodCallExpression methodCallExpression)
        {
            return methodCallExpression;
        }

        /// <summary>
        /// Visit Logical Expression.
        /// </summary>
        /// <param name="expression">Logical expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitLogicalExpression(LinqExtender.Ast.LogicalExpression expression)
        {
            this.Visit(expression.Left);
            this.Visit(expression.Right);
            return expression;
        }

        /// <summary>
        /// Visit Lambda Expression.
        /// </summary>
        /// <param name="expression">Lambda expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitLambdaExpression(LinqExtender.Ast.LambdaExpression expression)
        {
            if (expression.Body != null)
            {
                return this.Visit(expression.Body);
            }

            return expression;
        }

        /// <summary>
        /// Visit Binary Expression.
        /// </summary>
        /// <param name="expression">Binary expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitBinaryExpression(LinqExtender.Ast.BinaryExpression expression)
        {
            this.Visit(expression.Left);
            this.Visit(expression.Right);
            return expression;
        }

        /// <summary>
        /// Visit Member Expression.
        /// </summary>
        /// <param name="expression">Member expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitMemberExpression(LinqExtender.Ast.MemberExpression expression)
        {
            return expression;
        }

        /// <summary>
        /// Visit Literal Expression.
        /// </summary>
        /// <param name="expression">Literal expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitLiteralExpression(LinqExtender.Ast.LiteralExpression expression)
        {
            return expression;
        }

        /// <summary>
        /// Visit OrderBy Expression.
        /// </summary>
        /// <param name="expression">OrderBy expression.</param>
        /// <returns>The LinqExtender Expression.</returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        internal virtual LinqExtender.Ast.Expression VisitOrderbyExpression(LinqExtender.Ast.OrderbyExpression expression)
        {
            return expression;
        }
    }
}

using System;
using LinqExtender;

namespace Intuit.Ipp.Query
{
    public class ExpressionVisitor
    {
        internal LinqExtender.Ast.Expression Visit(LinqExtender.Ast.Expression expression)
        {
            switch (expression.CodeType)
            {
                case CodeType.BlockExpression:
                    return VisitBlockExpression((LinqExtender.Ast.BlockExpression)expression);
                case CodeType.TypeExpression:
                    return VisitTypeExpression((LinqExtender.Ast.TypeExpression)expression);
                case CodeType.LambdaExpresion:
                    return VisitLambdaExpression((LinqExtender.Ast.LambdaExpression)expression);
                case CodeType.LogicalExpression:
                    return VisitLogicalExpression((LinqExtender.Ast.LogicalExpression)expression);
                case CodeType.BinaryExpression:
                    return VisitBinaryExpression((LinqExtender.Ast.BinaryExpression)expression);
                case CodeType.LiteralExpression:
                    return VisitLiteralExpression((LinqExtender.Ast.LiteralExpression)expression);
                case CodeType.MemberExpression:
                    return VisitMemberExpression((LinqExtender.Ast.MemberExpression)expression);
                case CodeType.OrderbyExpression:
                    return VisitOrderbyExpression((LinqExtender.Ast.OrderbyExpression)expression);
                case CodeType.MethodCallExpression:
                    return VisitMethodCallExpression((LinqExtender.Ast.MethodCallExpression)expression);
                case CodeType.SelectExpression:
                    return VisitSelectCallExpression((LinqExtender.Ast.SelectExpression)expression);
                case CodeType.NotExpression:
                    return expression;
            }

            throw new ArgumentException("Expression type is not supported");
        }

        public virtual LinqExtender.Ast.Expression VisitSelectCallExpression(LinqExtender.Ast.SelectExpression selectExpression)
        {
            return selectExpression;
        }

        public virtual LinqExtender.Ast.Expression VisitTypeExpression(LinqExtender.Ast.TypeExpression typeExpression)
        {
            return typeExpression;
        }

        public virtual LinqExtender.Ast.Expression VisitBlockExpression(LinqExtender.Ast.BlockExpression blockExpression)
        {
            foreach (var expression in blockExpression.Expressions)
                this.Visit(expression);

            return blockExpression;
        }

        public virtual LinqExtender.Ast.Expression VisitMethodCallExpression(LinqExtender.Ast.MethodCallExpression methodCallExpression)
        {
            return methodCallExpression;
        }

        public virtual LinqExtender.Ast.Expression VisitLogicalExpression(LinqExtender.Ast.LogicalExpression expression)
        {
            this.Visit(expression.Left);
            this.Visit(expression.Right);
            return expression;
        }

        public virtual LinqExtender.Ast.Expression VisitLambdaExpression(LinqExtender.Ast.LambdaExpression expression)
        {
            if (expression.Body != null)
                return this.Visit(expression.Body);
            return expression;
        }

        public virtual LinqExtender.Ast.Expression VisitBinaryExpression(LinqExtender.Ast.BinaryExpression expression)
        {
            this.Visit(expression.Left);
            this.Visit(expression.Right);

            return expression;
        }

        public virtual LinqExtender.Ast.Expression VisitMemberExpression(LinqExtender.Ast.MemberExpression expression)
        {
            return expression;
        }

        public virtual LinqExtender.Ast.Expression VisitLiteralExpression(LinqExtender.Ast.LiteralExpression expression)
        {
            return expression;
        }

        public virtual LinqExtender.Ast.Expression VisitOrderbyExpression(LinqExtender.Ast.OrderbyExpression expression)
        {
            return expression;
        }

    }
}

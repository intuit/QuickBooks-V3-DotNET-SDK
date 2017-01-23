// -----------------------------------------------------------------------
// <copyright file="IppContext.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using LinqExtender;
    using System.Collections;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class IppContext<T> : ExpressionVisitor, IQueryContext<T>
    {
        StringBuilder builder, selectBuilder, countBuilder;
        bool isReportQuery = false;
        IEnumerable<LinqExtender.Ast.Expression> notExpressions = new List<LinqExtender.Ast.Expression>();
        StringBuilder orderByBuilder, whereBuilder, fromBuilder, endBuilder;

        public IppContext()
        {
            builder = new StringBuilder();
            selectBuilder = new StringBuilder();
            this.countBuilder = new StringBuilder();
            this.orderByBuilder = new StringBuilder();
            this.whereBuilder = new StringBuilder();
            this.fromBuilder = new StringBuilder();
            this.endBuilder = new StringBuilder();
        }

        public IEnumerable<T> Execute(LinqExtender.Ast.Expression expression, bool isToIdsQueryMethod, out string idsQuery)
        {
            notExpressions = (expression as LinqExtender.Ast.BlockExpression).Expressions.Where(e => e.CodeType == CodeType.NotExpression);
            this.Visit(expression);
            if (!string.IsNullOrWhiteSpace(this.selectBuilder.ToString()))
            {
                builder.Append(this.selectBuilder.ToString().TrimEnd(new char[] { ',' }));
            }
            else
            {
                builder.Append("* ");
            }

            string count = this.countBuilder.ToString();
            if (!string.IsNullOrWhiteSpace(count))
            {
                string newValue = this.builder.ToString().Replace(" * ", count);
                builder.Clear();
                builder.Append(newValue);
            }

            builder.Append(fromBuilder);

            if (!string.IsNullOrWhiteSpace(this.whereBuilder.ToString()))
            {
                string whereString = this.whereBuilder.ToString();
                if (whereString.StartsWith(" AND"))
                {
                    whereString = whereString.TrimStart(new char[] { ' ', 'A', 'N', 'D' });
                }

                builder.Append(" WHERE " + whereString);
            }

            if (!string.IsNullOrWhiteSpace(this.orderByBuilder.ToString()))
            {
                builder.Append(" ORDER BY " + this.orderByBuilder.ToString().TrimStart(new char[] { ',' }));
            }

            if (!string.IsNullOrWhiteSpace(this.endBuilder.ToString()))
            {
                builder.Append(this.endBuilder);
            }

            idsQuery = builder.ToString().Trim();

            return new List<T>().AsEnumerable();
        }

        public override LinqExtender.Ast.Expression VisitSelectCallExpression(LinqExtender.Ast.SelectExpression selectExpression)
        {
            selectBuilder.Append(selectExpression.PropertyName + ",");
            return selectExpression;
        }

        public override LinqExtender.Ast.Expression VisitTypeExpression(LinqExtender.Ast.TypeExpression expression)
        {
            // For Report entities the format of the query string is different
            string format = string.Empty;
            if (expression.Type.UnderlyingType.BaseType.Name.Equals("ReportQueryBase"))
            {
                this.isReportQuery = true;
                format = " {0} WITH ";
            }
            else
            {
                format = " FROM {0} ";
            }

            builder.Append("QUERY ");
            fromBuilder.Append(string.Format(format, expression.Type.Name));
            //builder.Append(string.Format("QUERY * FROM {0} ", expression.Type.Name));
            return expression;
        }

        public override LinqExtender.Ast.Expression VisitLambdaExpression(LinqExtender.Ast.LambdaExpression expression)
        {
            this.Visit(expression.Body);
            return expression;
        }

        public override LinqExtender.Ast.Expression VisitBinaryExpression(LinqExtender.Ast.BinaryExpression expression)
        {
            this.Visit(expression.Left);
            whereBuilder.Append(GetBinaryOperator(expression.Operator));
            this.Visit(expression.Right);

            return expression;
        }

        public override LinqExtender.Ast.Expression VisitLogicalExpression(LinqExtender.Ast.LogicalExpression expression)
        {
            this.Visit(expression.Left);
            WriteLogicalOperator(expression.Operator);
            this.Visit(expression.Right);
            return expression;
        }

        public override LinqExtender.Ast.Expression VisitMemberExpression(LinqExtender.Ast.MemberExpression expression)
        {
            string name = expression.FullName.TrimStart(new char[] { '.' });
            if (this.IsNotUsed(name))
            {
                this.whereBuilder.Append(" NOT ");
            }
            
            this.whereBuilder.Append(name);
            return expression;
        }

        public override LinqExtender.Ast.Expression VisitLiteralExpression(LinqExtender.Ast.LiteralExpression expression)
        {
            WriteValue(expression.Type, expression.Value);
            return expression;
        }

        public override LinqExtender.Ast.Expression VisitOrderbyExpression(LinqExtender.Ast.OrderbyExpression expression)
        {
            orderByBuilder.Append(string.Format(", {0}{1} {2} ", expression.Member.Name, expression.Suffix, expression.Ascending ? " " : " DESC "));
            return expression;
        }

        public override LinqExtender.Ast.Expression VisitMethodCallExpression(LinqExtender.Ast.MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.IsTake)
            {
                endBuilder.Append(string.Format(" maxResults {0} ", methodCallExpression.Paramters[0].Value));
            }

            if (methodCallExpression.IsSkip)
            {
                endBuilder.Append(string.Format(" startPosition {0} ", methodCallExpression.Paramters[0].Value));
            }

            if (methodCallExpression.Method.Name == "StartsWith" || methodCallExpression.Method.Name == "EndsWith" || methodCallExpression.Method.Name == "Contains")
            {
                this.whereBuilder.Append(" AND ");
                if (this.IsNotUsed(methodCallExpression.Target.ToString()))
                {
                    this.whereBuilder.Append(" NOT ");
                }
            
                whereBuilder.Append(string.Format(" {0} {1} '{2}' ", methodCallExpression.Target, "LIKE", methodCallExpression.Paramters[0].Value));
            }

            if (methodCallExpression.Method.Name == "In")
            {
                string[] values = methodCallExpression.Paramters[0].Value as string[];
                string inValues = string.Empty;
                foreach (var item in values)
                {
                    inValues += string.Format("'{0}',", item);
                }

                inValues = inValues.TrimEnd(new char[] { ',' });
                this.whereBuilder.Append(" AND ");
                if (this.IsNotUsed(methodCallExpression.Target.ToString()))
                {
                    this.whereBuilder.Append(" NOT ");
                }
            
                whereBuilder.Append(string.Format(" {0} {1} ({2}) ", methodCallExpression.Target, "IN", inValues));
            }

            if (methodCallExpression.Method.Name == "Count")
            {
                countBuilder.Append(" COUNT(*) ");
            }

            return methodCallExpression;
        }

        private static string GetBinaryOperator(BinaryOperator @operator)
        {
            switch (@operator)
            {
                case BinaryOperator.Contains:
                    return " LIKE ";
                case BinaryOperator.Equal:
                    return " EQ ";
                case BinaryOperator.GreaterThan:
                    return " GT ";
                case BinaryOperator.GreaterThanEqual:
                    return " GTE ";
                case BinaryOperator.LessThan:
                    return " LT ";
                case BinaryOperator.LessThanEqual:
                    return " LTE ";
                case BinaryOperator.NotEqual:
                    return " NE ";
            }

            throw new ArgumentException("Invalid binary operator");
        }

        private void WriteLogicalOperator(LogicalOperator logicalOperator)
        {
            whereBuilder.Append(string.Format(" {0} ", logicalOperator.ToString().ToUpper()));
        }

        private void WriteValue(TypeReference type, object value)
        {
            if (type.UnderlyingType == typeof(Boolean) || type.UnderlyingType == typeof(bool))
            {
                whereBuilder.Append(value);
            }
            else
            {
                if (type.UnderlyingType.IsEnum)
                {
                    whereBuilder.Append(String.Format("'{0}'", Enum.ToObject(type.UnderlyingType, value)));
                }
                else
                {
                    whereBuilder.Append(String.Format("'{0}'", this.GetIdsDateTimeFormat(value)));
                }
            }
        }

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
                    string returnValue = dt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
                    return returnValue;
                }
            }
            catch (Exception)
            {
                return value;
            }

            return value;
        }

        private void Write(string value)
        {
            whereBuilder.Append(value);
        }

        private bool IsNotUsed(string propName)
        {
            foreach (LinqExtender.Ast.NotExpression item in this.notExpressions)
            {
                if (item.PropertyName.Equals(propName))
                {
                    return true;
                }
            }

            return false;
        }

        public bool parameter;
    }

    public static class StringExtensions
    {
        public static bool In(this string value, string[] values)
        {
            if (values.Contains(value))
            {
                return true;
            }

            return false;
        }

        public static string RemoveWhitespaces(this string value)
        {
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", " ");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace IDSLinq
{
    class QueryExecuter
    {
        internal static object Execute(Expression expression, bool IsEnumerable)
        {
            string queryString = string.Empty;

            if (expression is LambdaExpression)
            {
                LambdaExpression lambdaExp = (LambdaExpression)expression;
            }
            else if (expression is BinaryExpression)
            {
                BinaryExpression binaryExp = (BinaryExpression)expression;
            }
            else if (expression is MethodCallExpression)
            {
                
                MethodCallExpression methExp = (MethodCallExpression)expression;
                Expression arg0Exp = methExp.Arguments[0];
                Expression arg1Exp = methExp.Arguments[1];
                
                //BuildIDSQuery(arg0Exp);
                queryString = BuildIDSQuery(arg1Exp);
                
            }

            if (string.IsNullOrEmpty(queryString))
            {

                // Call REST service

                // Return objects

            }
            List<Customer> accounts = new List<Customer>();
            Customer account = new Customer();
            accounts.Add(account);

            return accounts;
        }      

        private static string BuildIDSQuery(Expression expression)
        {
            string queryString = string.Empty;

            if (expression is LambdaExpression)
            {
                LambdaExpression lambdaExp = (LambdaExpression)expression;
            }
            else if (expression is BinaryExpression)
            {
                BinaryExpression binaryExp = (BinaryExpression)expression;
                
            }
            else if (expression is ConstantExpression)
            {                
                ConstantExpression consExp = (ConstantExpression)expression;                
            }
            
            else if (expression is MemberExpression)
            {

            }
            else if (expression is MethodCallExpression)
            {
            }
            
            else if (expression is NewArrayExpression)
            {
            }
            else if (expression is NewExpression)
            {
            }
            else if (expression is ParameterExpression)
            {
            }
            
            else if (expression is TypeBinaryExpression)
            {
            }

            else if (expression is UnaryExpression)
            {                

                UnaryExpression unaryExp = (UnaryExpression)expression;               
                    
                if (((BinaryExpression)((LambdaExpression)unaryExp.Operand).Body).NodeType == ExpressionType.Equal)
                {
                    string left = ((BinaryExpression)((LambdaExpression)unaryExp.Operand).Body).Left.ToString();
                    string right = ((BinaryExpression)((LambdaExpression)unaryExp.Operand).Body).Right.ToString();
                    queryString = left + " :EQUALS: " + right;
                        
                }

                if (((BinaryExpression)((LambdaExpression)unaryExp.Operand).Body).NodeType == ExpressionType.AndAlso)
                {
                    queryString = ProcessAndAlso(unaryExp);

                }

                // unaryExp gives values as "account => (account.AccountParentName == "SampleAccount")"
                // Couldn't find anyway to parse this unaryExp and get values

            }

            return queryString;
        }

        private static string ProcessAndAlso(UnaryExpression andAlso)
        {
            string leftBinary = ProcessEqual(((BinaryExpression)((LambdaExpression)andAlso.Operand).Body).Left);
            string rightBinary = ProcessEqual(((BinaryExpression)((LambdaExpression)andAlso.Operand).Body).Right);

            return leftBinary + " :AND: " + rightBinary;

        }

        private static string ProcessEqual(Expression equal)
        {
            string left = ((BinaryExpression)equal).Left.ToString();
            string right = ((BinaryExpression)equal).Right.ToString();

            return left + " :EQUALS: " + right;
        }

    }
}

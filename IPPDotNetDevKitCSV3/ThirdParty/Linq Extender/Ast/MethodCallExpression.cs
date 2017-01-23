
using System.Reflection;
using System;

namespace Intuit.Ipp.LinqExtender.Ast
{
   
    /// <summary>
    /// Defines method calls on the query
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class MethodCallExpression : Expression
    {
        internal MethodCallExpression(MethodCall methodCall)
        {
            this.methodCall = methodCall;
        }

        /// <summary>
        /// Gets the target
        /// </summary>
        public object Target
        {
            get
            {
                return methodCall.Target;
            }
        }

        /// <summary>
        /// Gets the underlying method info.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return methodCall.Method;
            }
        }

        /// <summary>
        /// Gets a value indicating that it is a take call.
        /// </summary>
        public bool IsTake
        {
            get
            {
                return methodCall.Method.Name == MethodNames.Take;
            }
        }

        /// <summary>
        /// Gets a value indicating that it is a skip method.
        /// </summary>
        public bool IsSkip
        {
            get
            {
                return methodCall.Method.Name == MethodNames.Skip;
            }
        }

        /// <summary>
        /// Gets the method parameters.
        /// </summary>
        public MethodCall.Parameter [] Paramters
        {
            get
            {
                return methodCall.Parameters;
            }
        }

        /// <summary>
        /// Override member
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.MethodCallExpression; }
        }

        private readonly MethodCall methodCall;
    }
}

using System;
using System.Reflection;
using System.Linq.Expressions;

namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class MethodCall
    {
        /// <summary>
        /// Initalizes the instance of <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="name">Name of the method</param>
        /// <param name="parameters">Method arguments.</param>
        internal MethodCall(object target, MethodInfo methodInfo, Parameter[] parameters)
        {
            this.target = target;
            this.methodInfo = methodInfo;
            this.parameters = parameters;
        }

        /// <summary>
        /// Gets the target expression.
        /// </summary>
        public object Target
        {
            get
            {
                return target;
            }
        }

        /// <summary>
        /// Gets the underlying method info.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return methodInfo;
            }
        }

        /// <summary>
        /// Gets the array of parameter.
        /// </summary>
        public Parameter[] Parameters
        {
            get
            {
                return parameters;
            }
        }

        public class Parameter
        {
            /// <summary>
            /// Initalizes the new instance of <see cref="Argument"/> class.
            /// </summary>
            /// <param name="type">Type of the argument</param>
            /// <param name="value">Value of the argument</param>
            internal Parameter(Type type, object value)
            {
                this.type = type;
                this.value = value;
            }

            /// <summary>
            /// Gets the parameter value
            /// </summary>
            public object Value
            {
                get
                {
                    return value;
                }
            }

            /// <summary>
            /// Gets the underlying type.
            /// </summary>
            public Type Type
            {
                get
                {
                    return type;
                }
            }


            private readonly Type type;
            private readonly object value;
        }


        private readonly object target;
        private readonly MethodInfo methodInfo;
        private readonly Parameter[] parameters;
    }
}

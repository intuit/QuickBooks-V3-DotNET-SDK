using System;
using Intuit.Ipp.LinqExtender.Abstraction;

namespace Intuit.Ipp.LinqExtender.Fluent
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class FluentMethod
    {
        /// <summary>
        /// Initializes the instance of <see cref="FluentMethod"/> class.
        /// </summary>
        /// <param name="bucket">Target bucket</param>
        public FluentMethod(IBucket bucket)
        {
            this.bucket = bucket;
        }

        internal void ForEach(Action<MethodCall> action)
        {
            for (int index = 0; index < bucket.Methods.Count; index++)
            {
                action(bucket.Methods[index]);
            }
        }

        private readonly IBucket bucket;
    }
}

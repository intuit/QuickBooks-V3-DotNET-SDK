using System;
using System.Collections.Generic;

namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class Buckets<T> : Dictionary<Type, BucketImpl>
    {
        public BucketImpl Current
        {
            get
            {
                Ensure(typeof(T));
                return current;
            }
            set
            {
                this[typeof (T)] = value; 
            }
        }

        private void Ensure(Type targetType)
        {
            if (this.ContainsKey(targetType))
            {
                // set the current object.
                current = this[targetType].InstanceImpl;
            }
        }

        private BucketImpl current = null;
  
    }
}
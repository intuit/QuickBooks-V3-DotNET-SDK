using System;
using Intuit.Ipp.LinqExtender.Attributes;

namespace Intuit.Ipp.LinqExtender
{
   
    /// <summary>
    /// Wrapper over a system type.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class TypeReference
    {
        internal TypeReference(Type targetType)
        {
            this.targetType = targetType;
        }

        /// <summary>
        /// Gets the name of the type, applies <see cref="NameAttribute"/> first.
        /// </summary>
        public string Name
        {
            get
            {
                var nameAtt = FindAttribute<NameAttribute>();

                if ((nameAtt != null))
                {
                    return nameAtt.Name;
                }
                return targetType.Name;
            }
        }

        /// <summary>
        /// Gets the underlying type
        /// </summary>
        public Type UnderlyingType
        {
            get
            {
                return targetType;
            }
        }

        /// <summary>
        /// Finds the specific attribute from the type.
        /// </summary>
        /// <typeparam name="T">Attribute to find</typeparam>
        /// <returns>Target attribute reference</returns>
        public T FindAttribute<T>()
        {
            return (T)Utility.FindAttribute(typeof(T), targetType);
        }

        private readonly Type targetType;
    }
}

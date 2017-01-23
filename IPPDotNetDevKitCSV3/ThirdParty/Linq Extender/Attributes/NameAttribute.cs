using System;

namespace Intuit.Ipp.LinqExtender.Attributes
{

    /// <summary>
    /// Marks a class property or class with special name 
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class NameAttribute : Attribute
    {
        /// <summary>
        /// Initalizes a new instance of the <see cref="NameAttribute"/> class.
        /// </summary>
        /// <param name="targetName">Name of the reflected object.</param>
        public NameAttribute(string targetName)
        {
            this.targetName = targetName;
        }
         
        /// <summary>
        /// maps to the name of the original enity name.
        /// </summary>
        public string Name
        {
            get
            {
                return targetName;
            }
        }

        private readonly string targetName = string.Empty;
    }
}
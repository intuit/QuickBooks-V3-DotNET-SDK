using System.Reflection;
using System;


using Intuit.Ipp.LinqExtender.Attributes;

namespace Intuit.Ipp.LinqExtender
{

    /// <summary>
    /// Wraps and extends the <see cref="MemberInfo"/> instance.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class MemberReference
    {
        internal MemberReference(MemberInfo memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        /// <summary>
        /// Get the decalaring type.
        /// </summary>
        public TypeReference DeclaringType
        {
            get
            {
                return new TypeReference(memberInfo.DeclaringType);
            }
        }

        /// <summary>
        /// Gets the name of the member, applies <see cref="NameAttribute"/> first.
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
                return this.memberInfo.Name;
            }
        }

        /// <summary>
        /// Gets the <see cref="MemberInfo"/> associated with the reference.
        /// </summary>
        public MemberInfo MemberInfo
        {
            get
            {
                return memberInfo;
            }
        }

        /// <summary>
        /// Finds the specific attribute from the member.
        /// </summary>
        /// <typeparam name="T">Attribute to find</typeparam>
        /// <returns>Target attribute reference</returns>
        public T FindAttribute<T>()
        {
            return (T)Utility.FindAttribute(typeof(T), memberInfo);
        }

        private readonly MemberInfo memberInfo;
    }
}

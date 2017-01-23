using System;
using System.Reflection;

namespace Intuit.Ipp.LinqExtender.Ast
{
   
    /// <summary>
    /// Represents query members
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class MemberExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberExpression"/> class.
        /// </summary>
        /// <param name="item">Target <see cref="BucketItem"/></param>
        internal MemberExpression(BucketItem item)
        {
            this.item = item;
            this.member = new MemberReference(item.MemberInfo);
        }

        /// <summary>
        /// Gets the member reference.
        /// </summary>
        public MemberReference Member
        {
            get
            {
                return member;
            }
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        public string Name
        {
            get
            {
                return member.Name;
            }
        }

        /// <summary>
        /// Gets the full name of the member.
        /// </summary>
        public string FullName
        {
            get
            {
                return this.item.FullName;
            }
        }

        /// <summary>
        /// Gets the declaring type for the member.
        /// </summary>
        public Type DeclaringType
        {
            get
            {
                return this.item.DeclaringType;
            }
        }

        /// <summary>
        /// Finds the target custom attribute for the member.
        /// </summary>
        public T FindAttribute<T>()
        {
            return (T)item.FindAttribute(typeof(T));
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.MemberExpression; }
        }

        private BucketItem item;
        private MemberReference member;
    }
}

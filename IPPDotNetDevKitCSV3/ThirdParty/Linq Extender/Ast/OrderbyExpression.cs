using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Ast
{
    /// <summary>
    /// Represents order by query.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class OrderbyExpression : Ast.Expression
    {
        /// <summary>
        /// Intializes a new instance of the <see cref="OrderbyExpression"/> class.
        /// </summary>
        /// <param name="memberReference">Target memberReference</param>
        /// <param name="ascending">Sort order</param>
        internal OrderbyExpression(MemberReference memberReference, bool ascending)
        {
            this.memberReference = memberReference;
            this.ascending = ascending;
        }

        internal OrderbyExpression(MemberReference memberReference, string suffix, bool ascending)
        {
            this.memberReference = memberReference;
            this.ascending = ascending;
            this.Suffix = suffix;
        }

        /// <summary>
        /// Gets the associated member.
        /// </summary>
        public MemberReference Member
        {
            get
            {
                return memberReference;
            }
        }

        /// <summary>
        /// Gets a value indicating if the order should be made in ascending order.
        /// </summary>
        public bool Ascending
        {
            get
            {
                return ascending;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.OrderbyExpression; }
        }

        private MemberReference memberReference;
        private bool ascending;

        /// <summary>
        /// Get or set Suffix
        /// </summary>
        public string Suffix { get; private set; }
    }
}

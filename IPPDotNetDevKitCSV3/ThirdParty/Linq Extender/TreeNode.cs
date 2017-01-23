using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Intuit.Ipp.LinqExtender
{
 
    /// <summary>
    /// Represents the query conditions in a tree logical tree form.
    /// </summary>
    [DebuggerStepThrough]
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class TreeNode
    {
        /// <summary>
        /// Defines a tree node.
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Gets a value for the tree node.
            /// </summary>
            public object Value { get; set; }
        }
        /// <summary>
        /// Id of the current node.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// parent Id of the current node.
        /// </summary>
        public Guid ParentId { get; set; }
       
        /// <summary>
        /// list of nodes under each expression.
        /// </summary>
        internal IList<Node> Nodes
        {
            get
            {
                if (nodes == null)
                {
                    nodes = new List<Node>();  
                }
                return nodes;
            }
            set
            {
                nodes = value;
            }
        }
        /// <summary>
        /// left leaf of the current root, can contain bucketItem or a CurrentNode
        /// </summary>
        public Node Left 
        {
            get
            {
                if (Nodes.Count > 0)
                {
                    return Nodes[0];
                }
                return null;
            }   
        }
        /// <summary>
        /// right leaf of the current root, can contain bucketItem or a CurrentNode
        /// </summary>
        /// 
        public Node Right
        {
            get
            {
                if (Nodes.Count > 1)
                {
                    return Nodes[1];
                }
                return null;
            }
        }

        internal LogicalOperator RootImpl { get; set; }
   
        ///<summary>
        /// Root which the left and right item follows.
        ///</summary>
        public LogicalOperator Root
        {
            get
            {
                if (Right != null)
                    return RootImpl;
                // for single item should return NONE.
                return LogicalOperator.None;
            }
        }
        /// <summary>
        /// Clones the tree node.
        /// </summary>
        /// <returns>clonned <see cref="TreeNode"/></returns>
        public TreeNode Clone()
        {
            var clonned = Activator.CreateInstance<TreeNode>();

            foreach (var node in this.GetType().GetProperties())
            {
                if (node.CanWrite)
                {
                    node.SetValue(clonned, node.GetValue(this, null), null);
                }
            }
            return clonned;
        }

        private IList<Node> nodes;

    }
}
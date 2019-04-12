////********************************************************************
//https://github.com/mehfuzh/LinqExtender/blob/master/License.txt
//Copyright (c) 2007- 2010 LinqExtender Toolkit Project. 
//Project Modified by Intuit
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// 
////********************************************************************

using System.Diagnostics;
using Intuit.Ipp.LinqExtender;
using System;

namespace Intuit.Ipp.LinqExtender.Fluent
{

    /// <summary>
    /// Fluent implementation for the simplified expression tree.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class FluentExpressionTree
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentExpressionTree"/> class.
        /// </summary>
        /// <param name="node"></param>
        public FluentExpressionTree(TreeNode node)
        {
            this.node = node;
        }
        /// <summary>
        /// Gets the tree node.
        /// </summary>
        public TreeNode Node
        {
            get
            {
                return node;
            }
        }
        /// <summary>
        /// Describes the container for which the expression tree will be evaluated.
        /// </summary>
        /// <typeparam name="T">Type of the reference.</typeparam>
        /// <param name="container">Target container</param>
        /// <returns></returns>
        public FluentExpressionTree<T> DescribeContainerAs<T>(T container)
        {
            return new FluentExpressionTree<T>(this.Node, container);
        }

        private readonly TreeNode node;
    }
    /// <summary>
    /// Fluent expression tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class FluentExpressionTree<T> : FluentExpressionTree
    {
        /// <summary>
        /// Initalizes a new instance of the <see cref="FluentExpressionTree"/> class.
        /// </summary>
        /// <param name="node"></param>
        public FluentExpressionTree(TreeNode node)
            : base(node)
        {
        }
        /// <summary>
        /// Initalizes a new instance of the <see cref="FluentExpressionTree"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="reference"><typeparamref name="T"/></param>
        public FluentExpressionTree(TreeNode node, T reference)
            : base(node)
        {
            this.reference = reference;
        }

        /// <summary>
        /// Invoked for starting <see cref="TreeNode"/>
        /// </summary>
        /// <param name="beginHandler"></param>
        /// <returns></returns>
        public FluentExpressionTree<T> Begin(BeginHandler beginHandler)
        {
            begin = beginHandler;
            return this;
        }

        /// <summary>
        /// Invoked for closing the <see cref="TreeNode"/>
        /// </summary>
        /// <param name="endHandler"></param>
        /// <returns></returns>
        public FluentExpressionTree<T> End(EndHandler endHandler)
        {
            end = endHandler;
            return this;
        }
        /// <summary>
        /// Invoked for root <see cref="TreeNode"/>
        /// </summary>
        /// <param name="rootHandler"></param>
        /// <returns></returns>
        public FluentExpressionTree<T> Root(RootHandler rootHandler)
        {
            root = rootHandler;
            return this;
        }
        /// <summary>
        /// Invoked foreach Leaf
        /// </summary>
        /// <param name="itemHandler"></param>
        /// <returns></returns>
        public FluentExpressionTree<T> EachLeaf(ItemHandler itemHandler)
        {
            this.itemHandler = itemHandler;
            return this;
        }
        /// <summary>
        /// Begin <see cref="TreeNode"/> handler.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void BeginHandler(T sender);
        /// <summary>
        /// Closing <see cref="TreeNode"/> handler.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void EndHandler(T sender);
        /// <summary>
        /// Root <see cref="TreeNode"/>  handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="compoundOperator"></param>
        public delegate void RootHandler(T sender, LogicalOperator compoundOperator);
        /// <summary>
        /// Leaf node handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        public delegate void ItemHandler(T sender, BucketItem item);

        private BeginHandler begin;
        private EndHandler end;
        private RootHandler root;
        private ItemHandler itemHandler;


        [DebuggerStepThrough]
        private void BuildNode(TreeNode tNode)
        {
            if (begin != null)
                begin(reference);

            if (tNode.Left != null)
            {
                BuildItem(tNode.Left);
            }

            if (tNode.Root != LogicalOperator.None)
            {
                if (root != null)
                    root(reference, tNode.Root);
            }

            if (tNode.Right != null)
            {
                BuildItem(tNode.Right);
            }

            if (end != null)
                end(reference);
        }

        private void BuildItem(TreeNode.Node leaf)
        {
            if (leaf.Value is TreeNode)
            {
                BuildNode((TreeNode)leaf.Value);
            }
            else if (leaf.Value is BucketItem)
            {
                BucketItem item = (BucketItem)leaf.Value;

                if (itemHandler != null)
                {
                    itemHandler(reference, item);
                }
            }
        }
        /// <summary>
        /// Builds the logical tree for the expression.
        /// </summary>
        public void Execute()
        {
            if (Node == null)
                throw new ProviderException(Messages.MustDefineAContainer);

            BuildNode(Node);
        }

        private readonly T reference;
    }
}
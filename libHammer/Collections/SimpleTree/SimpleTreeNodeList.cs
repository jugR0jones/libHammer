using System;
using System.Collections.Generic;
using libHammer.Collections.Tree;

namespace libHammer.Collections.SimpleTree
{

    /// <summary>
    /// List of children
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleTreeNodeList<T> : List<SimpleTreeNode<T>>
    {
        /// <summary>
        /// Store a pointer to the parent node.
        /// This pointer is used to set the parent for nodes that are
        /// added to this list.
        /// </summary>
        private SimpleTreeNode<T> _parent = null;

        #region Constructors

        public SimpleTreeNodeList()
        {
            this._parent = null;
        }

        /// <summary>
        /// Constructors require the parent node.
        /// </summary>
        /// <param name="parent"></param>
        public SimpleTreeNodeList(SimpleTreeNode<T> parent)
        {
            if(parent == null)
            {
                throw new ArgumentNullException("parent", "Cannot create a new SimpleTreeNodeList without a parent");
            }

            this._parent = parent;
        }

        #endregion

        #region Add Nodes

        /// <summary>
        /// Add a new SimpleTreeNode to the list.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public new SimpleTreeNode<T> Add(SimpleTreeNode<T> node)
        {
            base.Add(node);
            node.Parent = this._parent;
            return node;
        }

        /// <summary>
        /// Add a value to the list.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SimpleTreeNode<T> Add(T value)
        {
            return Add(new SimpleTreeNode<T>(value));
        }

        #endregion

        /// <summary>
        /// Returns the list count to aid with debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Count=" + Count.ToString();
        }
    }

}

using System;
using System.Linq;
using libHammer.Collections.SimpleTree;

namespace libHammer.Collections.Tree
{

    /// <summary>
    /// Tree Node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleTreeNode<T> : IDisposable
    {
        #region Properties

        /// <summary>
        /// Fires when this SimpleTreeNode is disposed;
        /// </summary>
        public event EventHandler Disposing;

        private SimpleTreeNode<T> _parent;
        /// <summary>
        /// Gets and Sets the parent node.
        /// </summary>
        public SimpleTreeNode<T> Parent {
            get {
                return _parent;
            }
            set
            {
                if(value == _parent)
                {
                    /* No point setting the parent value twice. */
                    return;
                }
                if(_parent != null)
                {
                    /* Remove us from the parent's children. */
                    _parent.Children.Remove(this);
                }
                if(value != null && !value.Children.Contains(this))
                {
                    /* Add us as a child of the new parent. */
                    value.Children.Add(this);
                }
                /* Point to the new parent. */
                _parent = value;
            }
        }

        /// <summary>
        /// Gets the root node of the tree of this node.
        /// Iterates through the list until we get to the root node.
        /// </summary>
        public SimpleTreeNode<T> Root
        {
            get
            {
                SimpleTreeNode<T> node = this;
                while(node.Parent != null)
                {
                    node = node.Parent;
                }
                return node;
            }
        }

        /// <summary>
        /// Gets the child nodes of this node.
        /// </summary>
        public SimpleTreeNodeList<T> Children { get; private set; }

        private T _value;
        /// <summary>
        /// Gets or sets the value contained in this SimpleTreeNode
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Gets the depth of this SimpleTreeNode in the overall tree.
        /// </summary>
        public int Depth
        {
            get
            {
                int depth = 0;

                SimpleTreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }

                return depth;
            }
        }

        public TreeTraversalMode TreeTraversalMode { get; private set; }
        /// <summary>
        /// Indicates if this SimpleTreeNode has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        #endregion

        #region Constructors

        public SimpleTreeNode(T value)
        {
            this.Value = value;
            this.Parent = null;
            this.Children = new SimpleTreeNodeList<T>();
        }

        public SimpleTreeNode(T value, SimpleTreeNode<T> parent)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new SimpleTreeNodeList<T>();
        }

        #endregion

        #region IDisposable Methods

        public void Dispose()
        {
            CheckDisposed();
            OnDisposing();

            /* Clean up contained objects (in Value property) */
            if(Value is IDisposable)
            {
                if(this.TreeTraversalMode == TreeTraversalMode.BottomUp)
                {
                    foreach(SimpleTreeNode<T> node in this.Children)
                    {
                        node.Dispose();
                    }
                }

                (this.Value as IDisposable).Dispose();

                if(this.TreeTraversalMode == TreeTraversalMode.TopDown)
                {
                    foreach(SimpleTreeNode<T> node in this.Children)
                    {
                        node.Dispose();
                    }
                }
            }

            this.IsDisposed = true;
        }

        #endregion

        #region Events

        /// <summary>
        /// Executed by the Dispose() method when this object is disposed. 
        /// </summary>
        protected void OnDisposing()
        {
            if(this.Disposing != null)
            {
                Disposing(this, EventArgs.Empty);
            }
        }

        protected void CheckDisposed()
        {
            if(this.IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion

        /// <summary>
        /// Returns a string representation of this SimpleTreeNode, its depth and its child count
        /// to aid debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string description = String.Empty;
            if(this.Value != null)
            {
                description = "[" + Value.ToString() + "] ";
            }

            return description + "Depth=" + this.Depth.ToString() + ", Children=" + this.Children.Count.ToString();
        }
    }
}

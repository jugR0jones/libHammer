using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Collections.Tree
{

    /// <summary>
    /// A simple Tree data structure structure.
    /// </summary>
    public class Tree<T> : SimpleTreeNode<T>
    {

        #region Constructors

        public Tree(T rootValue) : base(rootValue)
        {

        }

        #endregion

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{

    // We implement IEnumerable to be able to use the collection initializer
    class MenuTreeNode : IEnumerable<MenuTreeNode>
    {

        /**
         * All nodes have a reference to their parent, except for root which points to null.
         * We identify nodes by their ID, which in our case is the name of the menu.
         * When a node is "gotten", it will run its execute command
         */

        public delegate void MenuAction(MenuTreeNode parent);

        private readonly Dictionary<string, MenuTreeNode> _children = new Dictionary<string, MenuTreeNode>();
        private readonly MenuAction action;

        public readonly string ID;
        public MenuTreeNode Parent { get; set; } // Note that action does NOT get called during the get here. That's because when stepping through a tree, if you go back to your parent, you don't always want to execute it.
        public int Count { get { return _children.Count; } }

        // the default null is to get around the "must be compile time constant" requirment
        public MenuTreeNode(string ID, MenuAction action = null)
        {
            this.ID = ID;
            this.action = action;
        }

        public MenuTreeNode GetChild(string ID)
        {
            _children[ID].action(_children[ID]);
            return _children[ID];
        }

        public MenuTreeNode Add(MenuTreeNode newNode)
        {
            if(newNode.Parent != null)
            {
                newNode.Parent._children.Remove(newNode.ID);
            }

            newNode.Parent = this;
            this._children.Add(newNode.ID, newNode);

            // For method chaining
            return this;
        }

        public void Execute() => action(this);

        public IEnumerator<MenuTreeNode> GetEnumerator() => _children.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _children.Values.GetEnumerator();
    }
}

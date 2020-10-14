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
        public delegate void MenuAction(MenuTreeNode parent);

        private readonly Dictionary<string, MenuTreeNode> _children = new Dictionary<string, MenuTreeNode>();
        private readonly MenuAction action;

        public readonly string ID;
        public MenuTreeNode Parent { get; private set; }
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

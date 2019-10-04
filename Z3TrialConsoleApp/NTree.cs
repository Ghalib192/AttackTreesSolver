using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z3TrialConsoleApp
{
    // https://stackoverflow.com/a/2012855
    public delegate void TreeVisitor<T>(T nodeData);

    public class NTree<T>
    {
        private T data;
        private LinkedList<NTree<T>> children;

        public NTree(T data)
        {
            this.data = data;
            children = new LinkedList<NTree<T>>();
        }

        public NTree<T> Clone()
        {
            var tree = new NTree<T>(this.data);
            foreach (var child in this.children)
            {
                tree.AddChild(child);
            }
            return tree;
        }

        public NTree(T data, IEnumerable<T> children) : this(data)
        {
            foreach (var child in children)
            {
                AddChild(child);
            }
        }

        public NTree(T data, params T[] children) : this(data, (IEnumerable<T>)children)
        {

        }

        public void AddChild(T data)
        {
            children.AddFirst(new NTree<T>(data));
        }

        public void AddChild(NTree<T> data)
        {
            children.AddFirst(data.Clone());
        }

        public NTree<T> GetChild(int i)
        {
            foreach (NTree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public static void Traverse(NTree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.data);
            foreach (NTree<T> kid in node.children)
            {
                Traverse(kid, visitor);
            }
        }

        public void Traverse(TreeVisitor<T> visitor)
        {
            Traverse(this, visitor);
        }

        public static void Traverse(NTree<T> node, Action startAct, TreeVisitor<T> visitor, Action endAct)
        {
            startAct();
            visitor(node.data);
            foreach (NTree<T> kid in node.children)
            {
                Traverse(kid, startAct, visitor, endAct);
            }
            endAct();
        }

        public void Traverse(Action startAct, TreeVisitor<T> visitor, Action endAct)
        {
            Traverse(this, startAct, visitor, endAct);
        }
    }
}

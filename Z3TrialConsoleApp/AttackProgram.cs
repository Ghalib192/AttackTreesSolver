using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z3TrialConsoleApp
{
    public class AttackProgram
    {
        public static NTree<T> CreateLeaf<T>(T root)
        {
            return new NTree<T>(root);
        }

        public static NTree<T> CreateTree<T>(T root, params T[] children)
        {
            return new NTree<T>(root, children);
        }

        public static NTree<T> CreateTree<T>(NTree<T> root, params NTree<T>[] children)
        {
            var clone = root.Clone();
            foreach (var child in children)
            {
                clone.AddChild(child);
            }

            return clone;
        }

        public static NTree<T> CreateTree<T>(T root, params NTree<T>[] children)
        {
            var tree = new NTree<T>(root);
            foreach (var child in children)
            {
                tree.AddChild(child);
            }

            return tree;
        }


        public static NTree<T> CreateTree<T>(NTree<T> tree, params T[] children)
        {
            var clone = tree.Clone();
            foreach (var child in children)
            {
                clone.AddChild(child);
            }

            return clone;
        }

        public static void Main(string[] args)
        {
            //Leaf Nodes
            var pickLock = CreateLeaf(new Attack("Pick Lock", 30, false));
            var findWrittenCombo = CreateLeaf(new Attack("Find Written Combo", 75, false));
            var listenToConversation = CreateLeaf(new Attack("Listen To Conversation", 25, false));
            var getTargetToStateCombo = CreateLeaf(new Attack("Get Target to State Combo", 40, false));
            var bribe = CreateLeaf(new Attack("Attack", 20, true));
            var threaten = CreateLeaf(new Attack("Threaten", 60, false));
            var blackmail = CreateLeaf(new Attack("Blackmail", 100, false));
            var installImproperly = CreateLeaf(new Attack("Install Improperly", 100, false));
            var cutOpenSafe = CreateLeaf(new Attack("Cut Open Safe", 10, false));

            //Adding Attack nodes and creating SubTrees
            var eavesdropTree = CreateTree(new Attack("Eavesdrop", BooleanOperator.And), listenToConversation, getTargetToStateCombo);

            var getComboFromTargetTree = CreateTree(new Attack("Get Combo From Target", BooleanOperator.Identity), threaten, blackmail, bribe, eavesdropTree);

            var learnComboTree = CreateTree(new Attack("Learn Combo", BooleanOperator.Identity), findWrittenCombo, getComboFromTargetTree);

            var openSafe = CreateTree(new Attack("Open Safe", BooleanOperator.Identity), pickLock, cutOpenSafe, installImproperly, learnComboTree);

            openSafe.Traverse(() => Console.WriteLine("("), attack => Console.WriteLine($"{attack.Description}[{attack.Operator}]"), () => Console.WriteLine(")"));
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLtree
{
    public class AVLTree<T> : IEnumerable<T>
        where T : IComparable
    {
        Node root;
        private int count;
        
        class Node
        {
            internal T value;
            internal Node left;
            internal Node right;
            internal int height;
            public Node(T value)
            {
                this.value = value;
                this.height = 1;
            }
        }

        public int Count { get { return count; } } // the number of elements in the tree
        public int Height { get { return GetHeight(root); } } // the height of the tree

        public AVLTree() 
        {
            root = null;
            count = 0;
        }

        // INSERTION
        public void Insert(T value)
        {
            Node newItem = new Node(value);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem); 
            }

            count++;
        }

        // INSERTION METHOD
        private Node RecursiveInsert(Node current, Node newNode)
        {
            if (current == null) return newNode; // base case, we reach this when we go left or right until current is null
            int compare = newNode.value.CompareTo(current.value);
            if (compare < 0) // if the new node is less than the current node
            {
                current.left = RecursiveInsert(current.left, newNode); // go left
            }
            else if (compare > 0) // if the new node is greater than the current node
            {
                current.right = RecursiveInsert(current.right, newNode); // go right
            }

            return BalanceTree(current); // balancing tree after recursion
        }

        // BALANCING TREE
        private Node BalanceTree(Node current)
        {
            FixNodeHeight(current);
            int balanceFactor = CalculateBalanceFactor(current);
            if (balanceFactor > 1)
            {
                if (CalculateBalanceFactor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }

            else if (balanceFactor < -1)
            {
                if (CalculateBalanceFactor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            
            return current;
        }


        // SEARCHING
        public bool Contains(T value)
        {
            if (Find(value, root) == null) return false;
            else return true;
        }

        // SEARCHING METHOD
        private Node Find(T target, Node current)
        {
            if (current == null)
            {
                return null; 
            }

            int compare = target.CompareTo(current.value);
            if (compare < 0)
            {
                if (compare == 0) return current;
                else
                {
                    return Find(target, current.left);
                }
            }

            else
            {
                if (compare == 0) return current;
                else
                {
                    return Find(target, current.right);
                }
            }
        }

        private void FixNodeHeight(Node current)
        {
            int leftHeight = GetHeight(current.left);
            int rightHeight = GetHeight(current.right);
            current.height = (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
        }

        private int GetHeight(Node current)
        {
            if (current == null) return 0;
            else return current.height;
        }

        private int CalculateBalanceFactor(Node current)
        {
            int l = GetHeight(current.left);
            int r = GetHeight(current.right);
            int balanceFactor = l - r;
            return balanceFactor;
        }

        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            FixNodeHeight(parent);
            FixNodeHeight(pivot);
            return pivot;
        }

        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            FixNodeHeight(parent);
            FixNodeHeight(pivot);
            return pivot;
        }

        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }

        // REMOVAL
        public bool Remove(T value)
        {
            int currCount = count;
            root = Remove(root, value);
            return (currCount != count);
        }

        // REMOVAL METHOD
        private Node Remove(Node target, T value)
        {
            if (target == null) return target;
            
            int compare = value.CompareTo(target.value);
            if (compare < 0) // left subtree
            {
                target.left = Remove(target.left, value);
            }
            else if (compare > 0) // right subtree
            {
                target.right = Remove(target.right, value);
            }
            else // if found
            {
                count--;
                Node left = target.left;
                Node right = target.right;

                if (right == null) return left; // if right subtree does not exist return left

                // else 
                Node min = null;
                Node rightSubtreeRoot = FindAndRemoveMin(right, ref min); 
                min.right = rightSubtreeRoot;
                min.left = left;
                return BalanceTree(min); // balancing the tree with the min element as a root
            }

            return BalanceTree(target); // balancing the tree above the element after removal
        }

        private Node FindAndRemoveMin(Node target, ref Node min) // finds and removes minimum element in subtree
        {
            if (target.left == null) // when there are no more left children we are at the minimum element
            {
                min = target;
                return target.right; // return the right child because it can exists and so we prevent it from disappearing 
            }

            target.left = FindAndRemoveMin(target.left, ref min);
            return BalanceTree(target); // balancing the tree bellow the element
        }
        
        public void Clear() // removing all elements thanks to the garbage collector =)
        {
            root = null;
            count = 0;
        }

        // ENUMERATOR
        public IEnumerator<T> GetEnumerator()
        {
            // DFS
            if (root != null)
            {
                HashSet<Node> returned = new HashSet<Node>(); // keeping the all returned items
                Stack<Node> stack = new Stack<Node>(); // keeping the elements for dfs
                stack.Push(root);
                while (stack.Count > 0)
                {
                    Node current = stack.Peek();
                    if (current.left == null || returned.Contains(current.left))
                    {
                        yield return current.value; // adding elements to the enumerator
                        returned.Add(current); // adding the returned elements
                        stack.Pop(); // removing the element from the stack

                        if (current.right != null)
                        {
                            stack.Push(current.right);
                        }
                    }
                    else
                    {
                        stack.Push(current.left);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

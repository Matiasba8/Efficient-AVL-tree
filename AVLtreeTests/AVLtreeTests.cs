using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVLtree;

namespace AVLtreeTests
{
    class AVLtreeTests
    {
        public static void RunSpeedTest()
        {
            AVLTree<int> tree = new AVLTree<int>();
            const int NUMBER_OF_ELEMENTS = 1000000;

            Console.WriteLine("Adding {0} elements...", NUMBER_OF_ELEMENTS);
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < NUMBER_OF_ELEMENTS; i++)
            {
                tree.Insert(i);
            }

            watch1.Stop();
            Console.WriteLine("elapsed time for insertion: {0}ms", watch1.ElapsedMilliseconds);
            Console.WriteLine("count: {0}", tree.Count);
            Console.WriteLine("height: {0}", tree.Height);
            Console.WriteLine("Removing almost all elements...");

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = tree.Count - 1; i > 4; i--)
            {
                tree.Remove(i);
            }

            watch2.Stop();
            Console.WriteLine("elapsed time for removal: {0}ms", watch2.ElapsedMilliseconds);
            Console.WriteLine("Elements left: ");
            foreach (var item in tree)
            {
                Console.Write("({0}) ", item);
            }

            Console.WriteLine("\ncount: {0}", tree.Count);
            Console.WriteLine("height: {0}", tree.Height);
            tree.Clear();
        }
        
        // TO DO
        public static void RunRotationTests()
        {
            AVLTree<char> tree = new AVLTree<char>();
            tree.Insert('a');
            tree.Insert('b');
            tree.Insert('c');

            foreach (var c in tree)
            {
                Console.Write("({0}) ", c);
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //RunSpeedTest();
            RunRotationTests();
        }
    }
}

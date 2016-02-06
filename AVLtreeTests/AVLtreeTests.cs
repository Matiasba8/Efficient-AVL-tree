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
        private static Random RNG = new Random();
        
        public static void RunAddingAndRemovalSpeedTest()
        {
            const int NUMBER_OF_ELEMENTS = 1000000;
            AVLTree<int> tree = new AVLTree<int>();

            Console.WriteLine("Adding {0} elements...", NUMBER_OF_ELEMENTS);
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < NUMBER_OF_ELEMENTS; i++)
            {
                tree.Insert(i);
            }

            watch1.Stop();
            Console.WriteLine("Elapsed time for insertion: {0}ms", watch1.ElapsedMilliseconds);
            Console.WriteLine("Count: {0}", tree.Count);
            Console.WriteLine("Height: {0}", tree.Height);
            Console.WriteLine("Removing almost all elements...");

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = tree.Count - 1; i > 4; i--)
            {
                tree.Remove(i);
            }

            watch2.Stop();
            Console.WriteLine("Elapsed time for removal: {0}ms", watch2.ElapsedMilliseconds);
            Console.WriteLine("Elements left: ");
            foreach (var item in tree)
            {
                Console.Write("({0}) ", item); // expected output - (0) (1) (2) (3) (4)
            }

            Console.WriteLine("\nCount: {0}", tree.Count); // expected output - 5
            Console.WriteLine("Height: {0}", tree.Height); // expected output - 3
        }
        
        public static void RunSearchingSpeedTest()
        {
            const int NUMBER_OF_ELEMENTS_IN_TREE1 = 1000;
            const int NUMBER_OF_ELEMENTS_IN_TREE2 = 10000;
            const int NUMBER_OF_ELEMENTS_IN_TREE3 = 1000000;
            const int TIMES_SEARCHED = 100000;
            AVLTree<int> tree1 = new AVLTree<int>();
            AVLTree<int> tree2 = new AVLTree<int>();
            AVLTree<int> tree3 = new AVLTree<int>();
            for (int i = 0; i < NUMBER_OF_ELEMENTS_IN_TREE3; i++)
            {
                if (i < NUMBER_OF_ELEMENTS_IN_TREE1)
                {
                    tree1.Insert(i);
                }

                if (i < NUMBER_OF_ELEMENTS_IN_TREE2)
                {
                    tree2.Insert(i);
                }

                tree3.Insert(i);
            }

            Console.WriteLine("Searching in tree1...");
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < TIMES_SEARCHED; i++)
            {
                tree1.Contains(GetRandomNumber(NUMBER_OF_ELEMENTS_IN_TREE1));
            }
            
            watch1.Stop();
            Console.WriteLine("Elapsed time: {0}ms\n", watch1.ElapsedMilliseconds);

            Console.WriteLine("Searching in tree2...");
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < TIMES_SEARCHED; i++)
            {
                tree1.Contains(GetRandomNumber(NUMBER_OF_ELEMENTS_IN_TREE2));
            }

            watch2.Stop();
            Console.WriteLine("Elapsed time: {0}ms\n", watch2.ElapsedMilliseconds);

            Console.WriteLine("Searching in tree3...");
            var watch3 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < TIMES_SEARCHED; i++)
            {
                tree1.Contains(GetRandomNumber(NUMBER_OF_ELEMENTS_IN_TREE3));
            }

            watch3.Stop();
            Console.WriteLine("Elapsed time: {0}ms\n", watch3.ElapsedMilliseconds);

            int magicNumberOutside = 1230;
            int magicNumberInside = 900;
            Console.WriteLine("Tree1 contains {0}: {1}\n", magicNumberOutside, tree1.Contains(magicNumberOutside)); // expected output - false
            Console.WriteLine("Tree1 contains {0}: {1}\n", magicNumberInside, tree1.Contains(magicNumberInside));   // expected output - true
        }

        private static int GetRandomNumber(int max) 
        { 
            return RNG.Next(0, max); 
        }

        static void Main(string[] args)
        {
            //RunAddingAndRemovalSpeedTest();
            //RunSearchingSpeedTest();
        }
    }
}

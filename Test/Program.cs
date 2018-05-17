using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Program
    {
        private static readonly Dictionary<int, int[]> graph = new Dictionary<int, int[]>();

        static void Main(string[] args)
        {
            var allNodes = new List<int>();
            foreach (var y in Enumerable.Range(1, 2))
            {
                allNodes.Add(y);
            }


        }

        static int Search(int id)
        {
            var queue = new Queue<int>(graph[id]);
            while (queue.Any())
            {
                int node = queue.Dequeue();
                if (node == id) return node;

                queue = new Queue<int>(queue.ToArray().Concat(graph[id]));
            }

            return -1;
        }

        static void Neighbors(int node)
        {
            var dirs = new List<int>()
            {
                1,0 } },
                new [,] { { 1,0 } },
                new [,] { { 1,0 } },
                new [,] { { 1, } }
            };
            var result = new List<int[,]>();
            foreach (var dir in dirs)
            {
                result.Add(new[,] { { node[0] + dir[0] } });
            }
        }
    }
}

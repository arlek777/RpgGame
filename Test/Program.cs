using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Program
    {
        private static readonly Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

        static void Main(string[] args)
        {
            int id = 0;
            int[][] allNodes = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                allNodes[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    allNodes[i][j] = ++id;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var currId = allNodes[i][j];
                    graph[currId] = Neighbors(allNodes, i, j);
                }
            }

            var result = Search(3);
        }

        static int Search(int id)
        {
            var queue = new Queue<int>(graph[1]);
            while (queue.Any())
            {
                int node = queue.Dequeue();
                if (node == id) return node;

                queue = new Queue<int>(queue.ToArray().Concat(graph[node]));
            }

            return -1;
        }

        static List<int> Neighbors(int[][] allNodes, int x, int y)
        {
            int[][] dirs = new[] {new[] {1, 0}, new[] {0, 1}, new[] {-1, 0}, new[] {0, -1}};
            var result = new List<int>();

            foreach (var dir in dirs)
            {
                var neighborX = x + dir[0];
                var neighborY = y + dir[1];

                neighborX = neighborX < 0 ? 0 : neighborX;
                neighborY = neighborY < 0 ? 0 : neighborY;

                if (neighborX >= allNodes.Length || neighborY >= allNodes[y].Length)
                    continue;

                if(neighborX == x && neighborY == y)
                    continue;

                result.Add(allNodes[neighborX][neighborY]);
            }

            return result;
        }
    }
}

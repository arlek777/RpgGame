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
            const int size = 5;

            int id = 0;
            int[][] allNodes = new int[size][];
            for (int i = 0; i < size; i++)
            {
                allNodes[i] = new int[size];
                for (int j = 0; j < size; j++)
                {
                    allNodes[i][j] = ++id;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var currId = allNodes[i][j];
                    graph[currId] = Neighbors(allNodes, i, j);
                }
            }

            var result = GetPath(1, 13);
        }

        static List<int> GetPath(int startId, int targetId)
        {
            var frontier = new Queue<int>();
            frontier.Enqueue(startId);
            var cameFrom = new Dictionary<int, int>();
            int current;
            while (frontier.Any())
            {
                current = frontier.Dequeue();
                if (current == targetId)
                    break;

                foreach (var next in graph[current])
                {
                    if (!cameFrom.ContainsKey(next))
                    {
                        frontier.Enqueue(next);
                        cameFrom[next] = current;
                    }
                }
            }

            current = targetId;
            var path = new List<int>();
            while (current != startId)
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();

            return path;
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

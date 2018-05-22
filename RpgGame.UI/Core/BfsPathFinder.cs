using System.Collections.Generic;
using System.Linq;

namespace RpgGame.UI.Core
{
    public static class BfsPathFinder
    {
        public static List<int> GetPath(int startIndex, int goalIndex, int groundId, GridGraph graph)
        {
            var frontier = new Queue<int>();
            var path = new List<int>();

            if (graph.Nodes[goalIndex] != groundId) return path;

            frontier.Enqueue(startIndex);
            var cameFrom = new Dictionary<int, int>();
            int current;
            while (frontier.Any())
            {
                current = frontier.Dequeue();
                if (current == goalIndex)
                    break;

                foreach (var nextIndex in graph.GetNeighbors(current))
                {
                    if (!cameFrom.ContainsKey(nextIndex) && graph.Nodes[nextIndex] == groundId)
                    {
                        frontier.Enqueue(nextIndex);
                        cameFrom[nextIndex] = current;
                    }
                }
            }

            current = goalIndex;
            while (current != startIndex)
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();

            return path;
        }
    }
}
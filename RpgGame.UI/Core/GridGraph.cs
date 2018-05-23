using System.Collections.Generic;

namespace RpgGame.UI.Core
{
    public class GridGraph
    {
        private enum Dirs
        {
            Bottom = 0,
            Right,
            Top,
            Left
        }

        private readonly Dirs[] _dirs = new[] { Dirs.Bottom, Dirs.Left, Dirs.Right, Dirs.Top };
        private readonly int _rowSize;

        public GridGraph(int[] nodes, int rowSize)
        {
            Nodes = nodes;
            _rowSize = rowSize;
        }

        public int[] Nodes { get; set; }

        public List<int> GetNeighbors(int index)
        {
            var neighbors = new List<int>();
            foreach (var dir in _dirs)
            {
                var neighborIndex = index;
                var rowIndex = neighborIndex % _rowSize;

                if (dir == Dirs.Bottom)
                {
                    neighborIndex += _rowSize;
                }
                else if (dir == Dirs.Top)
                {
                    neighborIndex -= _rowSize;
                }
                else if (dir == Dirs.Right && rowIndex != _rowSize)
                {
                    neighborIndex += 1;
                }
                else if (dir == Dirs.Left && rowIndex != 0)
                {
                    neighborIndex -= 1;
                }

                neighborIndex = neighborIndex < 0 ? index : neighborIndex;

                // if neighbor index is bigger then length (bottom) 
                // or if it's the same index we don't want to add it
                if (neighborIndex >= Nodes.Length || neighborIndex == index)
                    continue;

                neighbors.Add(neighborIndex);
            }

            return neighbors;
        }
    }
}
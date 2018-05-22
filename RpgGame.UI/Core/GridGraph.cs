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
                var neighborsIndex = index;
                var columnIndex = neighborsIndex % _rowSize;

                if (dir == Dirs.Bottom)
                {
                    neighborsIndex += _rowSize;
                }
                else if (dir == Dirs.Top)
                {
                    neighborsIndex -= _rowSize;
                }
                else if (dir == Dirs.Right && columnIndex != _rowSize)
                {
                    neighborsIndex += 1;
                }
                else if (dir == Dirs.Left && columnIndex != 0)
                {
                    neighborsIndex -= 1;
                }

                neighborsIndex = neighborsIndex < 0 ? 0 : neighborsIndex;

                if (neighborsIndex >= Nodes.Length || neighborsIndex == index)
                    continue;

                neighbors.Add(neighborsIndex);
            }

            return neighbors;
        }
    }
}
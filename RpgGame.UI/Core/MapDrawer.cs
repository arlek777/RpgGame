using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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

        private readonly Dirs[] _dirs = { Dirs.Bottom, Dirs.Right, Dirs.Top, Dirs.Left };
        private readonly int _rowSize;
        private readonly int _groundId;

        public GridGraph(int[] nodes, int rowSize, int groundId)
        {
            Nodes = nodes;
            _rowSize = rowSize;
            _groundId = groundId;
        }

        public int[] Nodes { get; set; }

        public List<int> GetNeighbors(int index)
        {
            var neighbors = new List<int>();
            foreach (var dir in _dirs)
            {
                var neighborsIndex = index;
                var columnIndex = (neighborsIndex % _rowSize) + 1;

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

    public static class BFSPathFinder
    {
        public static List<int> GetPath(int startIndex, int goalIndex, GridGraph graph)
        {
            var frontier = new Queue<int>();
            frontier.Enqueue(startIndex);
            var cameFrom = new Dictionary<int, int>();
            int current;
            while (frontier.Any())
            {
                current = frontier.Dequeue();
                if (current == goalIndex)
                    break;

                foreach (var next in graph.GetNeighbors(current))
                {
                    if (!cameFrom.ContainsKey(next))
                    {
                        frontier.Enqueue(next);
                        cameFrom[next] = current;
                    }
                }
            }

            current = goalIndex;
            var path = new List<int>();
            while (current != startIndex)
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();

            return path;
        }
    }

    public class MapDrawer
    {
        private readonly Canvas _canvas;
        private readonly MapLoader _loader;

        public MapDrawer(Canvas canvas, MapLoader loader)
        {
            _canvas = canvas;
            _loader = loader;
        }

        public void DrawLevelMap(string levelName)
        {
            _loader.Load(levelName);
            var layers = new List<int[]>()
            {
                _loader.GetMapLayer(UiConstants.BgLayerTitle),
                _loader.GetMapLayer(UiConstants.WallsLayerTitle)
            };

            layers.ForEach(DrawLayer);
        }

        private void DrawLayer(int[] layer)
        {
            var uri = new Uri($"{UiConstants.UriPackPrefix}/{UiConstants.SpriteFilePath}", UriKind.Absolute);
            var sprite = new BitmapImage(uri);

            int windowTileCount = (int)_canvas.Width / UiConstants.TileWidth;
            int spriteTileCount = (int)sprite.Width / UiConstants.TileWidth;

            for (int i = 0; i < layer.Length; i++)
            {
                var value = layer[i];
                if (value != -1)
                {
                    var windowPoint = CordinatesConverter.ConvertToWindow(i, windowTileCount);
                    var spritePoint = CordinatesConverter.ConvertToWindow(value, spriteTileCount);

                    var cropRect = new Int32Rect((int)spritePoint.X, (int)spritePoint.Y,
                        UiConstants.TileWidth, UiConstants.TileWidth);
                    var img = new Image
                    {
                        Source = new CroppedBitmap(sprite, cropRect)
                    };

                    _canvas.Children.Add(img);
                    Canvas.SetLeft(img, windowPoint.X);
                    Canvas.SetTop(img, windowPoint.Y);
                }
            }
        }
    }
}
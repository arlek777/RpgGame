using System;
using System.Windows;

namespace RpgGame.UI.Core
{
    public class Int32Point
    {
        public int X { get; }
        public int Y { get; }

        public Int32Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static class IndexConverter
    {
        public static Int32Point ConvertTo2dIndexes(int index, int rowSize)
        {
            var x = index % rowSize; // left
            var y = (int)Math.Floor(index / (double)rowSize); // top

            return new Int32Point(x, y);
        }

        public static int ConvertTo1dIndex(int x, int y, int rowSize)
        {
            var index = x * rowSize - y;
            return index;
        }

        public static Point ConvertToWindowPoint(int index, int rowSize)
        {
            var x = index % rowSize; // left (x)
            var y = (int)Math.Floor(index / (double)rowSize); // top (y)

            return new Point(x * UiConstants.TileWidth, y * UiConstants.TileHeight);
        }
    }
}
using System;
using System.Windows;

namespace RpgGame.UI.Core
{
    public static class CordinatesConverter
    {
        public static Point ConvertToWindow(int index, int tileCount)
        {
            var x = index % tileCount; // left
            var y = (int)Math.Floor(index / (double)tileCount); // top

            return new Point(x * UiConstants.TileWidth, y * UiConstants.TileHeight);
        }
    }
}
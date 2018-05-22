using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RpgGame.UI.Core
{
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
                    var windowPoint = IndexConverter.ConvertToWindowPoint(i, windowTileCount);
                    var spritePoint = IndexConverter.ConvertToWindowPoint(value, spriteTileCount);

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
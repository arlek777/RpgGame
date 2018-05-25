using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RpgGame.UI.Core;

namespace RpgGame.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MapLoader _mapLoader;
        private MapDrawer _mapDrawer;
        private GridGraph _graph;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            _mapLoader = new MapLoader();
            _mapDrawer = new MapDrawer(MainCanvas, _mapLoader);
            _mapDrawer.DrawLevelMap("TestLevel");
            _graph = new GridGraph(_mapLoader.GetMapLayer(UiConstants.WallsLayerTitle), UiConstants.TileWidth);

            var monsters = LevelManager.FillMonsters(512, 32, _graph);
            _mapDrawer.DrawLayer2(monsters);

            //var uri = new Uri($"{UiConstants.UriPackPrefix}/{UiConstants.TilesFolder}/monsters.png", UriKind.Absolute);
            //var sprite = new BitmapImage(uri);

            //int windowTileCount = (int)MainCanvas.Width / UiConstants.TileWidth;
            //int spriteTileCount = (int)sprite.Width / UiConstants.TileWidth;

            //var windowPoint = IndexConverter.ConvertToWindowPoint(120, windowTileCount);
            //var spritePoint = IndexConverter.ConvertToWindowPoint(2, spriteTileCount);

            //var cropRect = new Int32Rect((int) spritePoint.X, (int) spritePoint.Y,
            //    UiConstants.TileWidth, UiConstants.TileWidth);
            //var img = new Image
            //{
            //    Source = new CroppedBitmap(sprite, cropRect)
            //};

            ////Rectangle rect = new Rectangle() { Height = 32, Width = 32, Fill = new SolidColorBrush(Colors.Red) };
            //MainCanvas.Children.Add(img);
            //Canvas.SetLeft(img, windowPoint.X);
            //Canvas.SetTop(img, windowPoint.Y);

            base.OnInitialized(e);
        }

        private void MainCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var left = (int) Canvas.GetLeft((UIElement) e.Source) / 32; //TODO clean
            var top = (int) Canvas.GetTop((UIElement)e.Source);

            var startIndex = 256 + 4;
            var goalIndex = top + left;

            var result = BfsPathFinder.GetPath(startIndex, goalIndex, -1, _graph);

            int windowTileCount = (int)MainCanvas.Width / UiConstants.TileWidth;

            foreach (var i in result)
            {
                var point = IndexConverter.ConvertToWindowPoint(i, windowTileCount);
                Rectangle rect = new Rectangle() { Height = 32, Width = 32, Fill = new SolidColorBrush(Colors.Red) };
                MainCanvas.Children.Add(rect);

                Canvas.SetLeft(rect, (int) point.X);
                Canvas.SetTop(rect, (int) point.Y);
            }
        }
    }
}

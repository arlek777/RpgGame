using System;
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

            var player = new BitmapImage(new Uri($"{UiConstants.UriPackPrefix}/Assets/Tiles/player.png"));
            var img = new Image() { Source = player };
            MainCanvas.Children.Add(img);
            Canvas.SetLeft(img, 256);
            Canvas.SetTop(img, 256);

            _graph = new GridGraph(_mapLoader.GetMapLayer(UiConstants.WallsLayerTitle), UiConstants.TileWidth);

            base.OnInitialized(e);
        }

        private Canvas canvas = new Canvas() { Height = 680, Width = 1024 };
        private void MainCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Clear();
            MainCanvas.Children.Remove(canvas);

            var left = (int) Canvas.GetLeft((UIElement) e.Source) / 32; //TODO clean
            var top = (int) Canvas.GetTop((UIElement)e.Source);

            var startIndex = 256 + 8;
            var goalIndex = top + left;

            var result = BfsPathFinder.GetPath(startIndex, goalIndex, -1, _graph);

            int windowTileCount = (int)MainCanvas.Width / UiConstants.TileWidth;

            foreach (var i in result)
            {
                var point = IndexConverter.ConvertToWindowPoint(i, windowTileCount);
                Rectangle rect = new Rectangle() { Height = 32, Width = 32, Fill = new SolidColorBrush(Colors.Red) };
                canvas.Children.Add(rect);

                Canvas.SetLeft(rect, (int) point.X);
                Canvas.SetTop(rect, (int) point.Y);
            }

            MainCanvas.Children.Add(canvas);
        }
    }
}

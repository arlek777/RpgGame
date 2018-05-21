using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace RpgGame.UI
{
    public class MapManager
    {
        private readonly Canvas _canvas;
        private const string LevelMapFolder = "Assets/Map";
        private const string SpriteFilePath = "Assets/Tiles/sprite.png";
        private const string UriPackPrefix = "pack://application:,,,";

        public MapManager(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void DrawLevelMap(string levelName)
        {
            var levelFullPath = new Uri($"{UriPackPrefix}/{LevelMapFolder}/{levelName}.oel").AbsolutePath;
            var document = XDocument.Load(File.OpenRead("../../" + levelFullPath));
            if(document.Root == null) throw new NullReferenceException("Level is not found.");

            var layers = new List<int[]>()
            {
                GetLayerByName(document.Root, UiConstants.BgLayerTitle),
                GetLayerByName(document.Root, UiConstants.WallsLayerTitle)
            };

            layers.ForEach(DrawLayer);
        }

        private void DrawLayer(int[] layer)
        {
            var sprite = new BitmapImage(new Uri($"{UriPackPrefix}/{SpriteFilePath}", UriKind.Absolute));
            int windowColsCount = (int)_canvas.Width / UiConstants.TileWidth;
            int spriteColsCount = (int)sprite.Width / UiConstants.TileWidth;

            for (int i = 0; i < layer.Length; i++)
            {
                var value = layer[i];
                if (value != -1)
                {
                    var windowRow = (int)Math.Floor(i / (double)windowColsCount);
                    var windowCol = i % windowColsCount;

                    var spriteRow = (int)Math.Floor(value / (double)spriteColsCount);
                    var spriteCol = value % spriteColsCount;

                    var cropRect = new Int32Rect(spriteCol * UiConstants.TileWidth, spriteRow * UiConstants.TileWidth,
                        UiConstants.TileWidth, UiConstants.TileWidth);
                    var img = new Image
                    {
                        Source = new CroppedBitmap(sprite, cropRect)
                    };

                    _canvas.Children.Add(img);
                    Canvas.SetLeft(img, UiConstants.TileWidth * windowCol);
                    Canvas.SetTop(img, UiConstants.TileWidth * windowRow);
                }
            }
        }

        private int[] GetLayerByName(XElement element, string name)
        {
            int[] layer = element.Elements().FirstOrDefault(el => el.Name == name)?
                .Value.Split(',', '\n')
                .Select(int.Parse)
                .ToArray();

            return layer;
        }
    }

    public static class UiConstants
    {
        public const int TileHeight = 32;
        public const int TileWidth = 32;

        public const string BgLayerTitle = "Bg";
        public const string WallsLayerTitle = "Walls";
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MapManager _mapManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            _mapManager = new MapManager(MainCanvas);
            _mapManager.DrawLevelMap("TestLevel");

            base.OnInitialized(e);
        }
    }
}

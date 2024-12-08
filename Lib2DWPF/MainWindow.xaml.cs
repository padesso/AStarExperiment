using Lib2D;
using SkiaSharp;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lib2DWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int GRID_BORDER_PIXELS = 2;
        private SKColor GRID_BORDER_COLOR = new SKColor(105, 105, 105);
        private SKColor START_NODE_COLOR = new SKColor(0, 105, 0);
        private SKColor GOAL_NODE_COLOR = new SKColor(105, 0, 0);
        private SKColor PATH_NODE_COLOR = new SKColor(0, 0, 105);
        private SKColor WALKABLE_NODE_COLOR = new SKColor(245, 245, 245);
        private SKColor UNWALKABLE_NODE_COLOR = new SKColor(0, 0, 0);        

        private Grid2D _grid2D;
        public WriteableBitmap Bitmap;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _grid2D = new Grid2D(10, 10);
            _grid2D.SetNode(1, 0, new Node2D(0, 0, false));
            _grid2D.SetNode(1, 1, new Node2D(0, 0, false));
            _grid2D.SetNode(1, 2, new Node2D(0, 0, false));
            _grid2D.SetNode(1, 3, new Node2D(0, 0, false));
            _grid2D.SetNode(1, 4, new Node2D(0, 0, false));
            _grid2D.SetNode(1, 5, new Node2D(0, 0, false));
            _grid2D.SetNode(1, 6, new Node2D(0, 0, false));
            _grid2D.SetNode(1, 7, new Node2D(0, 0, false));
            _grid2D.SetNode(3, 9, new Node2D(0, 0, false));
            _grid2D.SetNode(3, 8, new Node2D(0, 0, false));
            _grid2D.SetNode(3, 7, new Node2D(0, 0, false));
            _grid2D.SetNode(3, 6, new Node2D(0, 0, false));
            _grid2D.SetNode(3, 5, new Node2D(0, 0, false));
            _grid2D.SetNode(3, 4, new Node2D(0, 0, false));
            _grid2D.SetNode(3, 3, new Node2D(0, 0, false));

            Node2D start = _grid2D.GetNode(0, 0);
            start.IsStart = true;
            Node2D goal = _grid2D.GetNode(9, 9);
            goal.IsGoal = true;
            _grid2D.SolveAStar(start, goal);

            Bitmap = CreateImage((int)Grid2DImage.Width, (int)Grid2DImage.Height);
            UpdateImage(Bitmap);
        }

        public WriteableBitmap CreateImage(int width, int height)
        {
            return new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, BitmapPalettes.Halftone256Transparent);
        }

        public void UpdateImage(WriteableBitmap writeableBitmap)
        {
            int width = (int)writeableBitmap.Width, height = (int)writeableBitmap.Height;

            writeableBitmap.Lock();

            using (var surface = SKSurface.Create(new SKImageInfo(width, height), writeableBitmap.BackBuffer, width * 4))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.Clear(new SKColor(130, 130, 130));

                DrawGrid(canvas);
            }

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            writeableBitmap.Unlock();

            Grid2DImage.Source = writeableBitmap;
        }

        private void DrawGrid(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint();
            float cellWidth = (float)Grid2DImage.Width / _grid2D.Width;
            float cellHeight = (float)Grid2DImage.Height / _grid2D.Height;

            //Loop through each node in the grid
            for (int x = 0; x < _grid2D.Width; x++)
            {
                for (int y = 0; y < _grid2D.Height; y++)
                {
                    Node2D node = _grid2D.GetNode(x, y);

                    if (node.IsStart)
                    {
                        paint.Color = START_NODE_COLOR;
                    }
                    else if (node.IsGoal)
                    {
                        paint.Color = GOAL_NODE_COLOR;
                    }
                    else if (node.IsPath)
                    {
                        paint.Color = PATH_NODE_COLOR;
                    }
                    else if (node.Walkable)
                    {
                        paint.Color = WALKABLE_NODE_COLOR;
                    }
                    else // !node.Walkable
                    {
                        paint.Color = UNWALKABLE_NODE_COLOR;
                    }

                    canvas.DrawRect(new SKRect(x * cellWidth, y * cellHeight, x * cellWidth + cellWidth, y * cellHeight + cellHeight), paint);
                }
            }
        }
    }
}
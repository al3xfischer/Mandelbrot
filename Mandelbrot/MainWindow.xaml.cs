using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Mandelbrot.Converter;
using Mandelbrot.ViewModels;

using Vector = System.Windows.Vector;

namespace Mandelbrot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Rect area = new Rect(new Point(-2.4, -1.5), new Point(0.8, 1.5));
        private Rectangle selection = new Rectangle() { Stroke = Brushes.Black, StrokeThickness = 1, Visibility = Visibility.Collapsed };
        private bool mousedown = false;
        private Point mousedownpos;
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            this.viewModel = new MainViewModel();
            this.DataContext = viewModel;
            this.TriggerCalculation();
            Canvas1.Children.Add(selection);
            ((PixelConverter)Resources["PixelConverter"]).Height = (int)image1.Height;
            ((PixelConverter)Resources["PixelConverter"]).Width = (int)image1.Width;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            area = new Rect(new Point(-2.4, -1.5), new Point(0.8, 1.5));
            this.TriggerCalculation();
        }

        private void TriggerCalculation()
        {
            int pixelHeight = (int)image1.Height;
            int pixelWidth = (int)image1.Width;

            int bytesPerPixel = 32 / 8;
            double xscale = (area.Right - area.Left) / pixelWidth;
            double yscale = (area.Top - area.Bottom) / pixelHeight;

            var settings = new MandelbrotSettings
            {
                BytesPerPixel = bytesPerPixel,
                Height = pixelHeight,
                Width = pixelWidth,
                ScaleX = xscale,
                ScaleY = yscale,
                OriginX = area.Left,
                OriginY = area.Top
            };

            this.viewModel.CalculateCommand.Execute(settings);
        }

        private void Canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mousedown = true;
            mousedownpos = e.GetPosition(Canvas1);
            Canvas.SetLeft(selection, mousedownpos.X);
            Canvas.SetTop(selection, mousedownpos.Y);
            selection.Width = 0;
            selection.Height = 0;
            selection.Visibility = Visibility.Visible;
        }

        private void Canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mousedown = false;
            selection.Visibility = Visibility.Collapsed;

            double xscale = (area.Right - area.Left) / image1.Width;
            double yscale = (area.Top - area.Bottom) / image1.Height;
            Point TopLeft = new Point(area.Left + Canvas.GetLeft(selection) * xscale, area.Top - Canvas.GetTop(selection) * yscale);
            Point BottomRight = TopLeft + new Vector(selection.Width * xscale, -selection.Height * yscale);
            area = new Rect(TopLeft, BottomRight);
            this.TriggerCalculation();
        }

        private void Canvas1_MouseMove(object sender, MouseEventArgs e)
        {

            if (mousedown)
            {
                Point mousepos = e.GetPosition(Canvas1);

                Vector diff = mousepos - mousedownpos;
                Point TopLeft = mousedownpos;
                if (diff.X < 0)
                {
                    TopLeft.X = mousepos.X;
                    diff.X = -diff.X;
                }
                if (diff.Y < 0)
                {
                    TopLeft.Y = mousepos.Y;
                    diff.Y = -diff.Y;
                }
                selection.Width = diff.X;
                selection.Height = diff.Y;
                Canvas.SetLeft(selection, TopLeft.X);
                Canvas.SetTop(selection, TopLeft.Y);
            }
        }
    }
}
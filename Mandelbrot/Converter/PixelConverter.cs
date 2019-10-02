using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Mandelbrot.Converter
{
    public class PixelConverter : IValueConverter
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pixels = value as byte[];
            var x = this.Height;
            int s = Width * (32 / 8);
            var bitmap = new WriteableBitmap(Width, Height, 96, 96, PixelFormats.Bgra32, null);
            bitmap.WritePixels(new Int32Rect(0, 0, Width, Height), pixels, s, 0);

            return bitmap;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

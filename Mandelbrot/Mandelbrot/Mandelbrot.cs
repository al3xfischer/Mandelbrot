using System.Numerics;
using System.Windows.Media;

namespace Mandelbrot
{
    public class MandelbrotCalculator
    {
        /// <summary>
        /// Calculates the Mandelbrot for the given settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public byte[] Calculate(MandelbrotSettings settings) 
        {
            byte[] pixels = new byte[settings.Height * settings.Width * settings.BytesPerPixel];
            int s = settings.Width * settings.BytesPerPixel;

            for (int i = 0; i < pixels.Length; i += settings.BytesPerPixel)
            {
                var partSettings = new MandelbrotPartSettings(settings, i, s);
                var color = CalculatePart(partSettings);
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
                pixels[i + 3] = color.A;
            }

            return pixels;
        }

        /// <summary>
        /// Calculates the part.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        private Color CalculatePart(MandelbrotPartSettings settings)
        {
            int py = settings.Counter / settings.Position;
            int px = settings.Counter % settings.Position / settings.BytesPerPixel;
            double x = settings.OriginX + px * settings.ScaleX;
            double y = settings.OriginY - py * settings.ScaleY;
            var c = new Complex(x, y);
            int count = CalculateComplex(c);
            return colorMap(count);
        }

        /// <summary>
        /// Calculates the complex.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        private int CalculateComplex(Complex c)
        {
            int count = 0;
            Complex z = Complex.Zero;

            while (count < 1000 && z.Magnitude < 4)
            {
                z = z * z + c;
                count++;
            }
            return count;
        }

        /// <summary>
        /// Colors the map.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private Color colorMap(int count)
        {
            Color C = new Color();
            C.B = (byte)(count / 100 * 25);
            count = count % 100;
            C.G = (byte)(count / 10 * 25);
            C.R = (byte)(count % 10 * 25);
            C.A = 255;
            return C;
        }
    }
}

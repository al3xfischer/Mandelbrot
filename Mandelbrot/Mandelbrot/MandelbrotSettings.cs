namespace Mandelbrot
{
    public class MandelbrotSettings
    {
        public MandelbrotSettings()
        {
        }

        public MandelbrotSettings(MandelbrotSettings settings)
        {
            this.Height = settings.Height;
            this.Width = settings.Width;
            this.OriginX = settings.OriginX;
            this.OriginY = settings.OriginY;
            this.ScaleX = settings.ScaleX;
            this.ScaleY = settings.ScaleY;
            this.BytesPerPixel = settings.BytesPerPixel;
        }

        public int Height { get; set; }

        public int Width { get; set; }

        public double OriginX { get; set; }

        public double OriginY { get; set; }

        public double ScaleX { get; set; }

        public double ScaleY { get; set; }

        public int BytesPerPixel { get; set; }
    }
}

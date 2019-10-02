namespace Mandelbrot
{
    public class MandelbrotPartSettings : MandelbrotSettings
    {
        public MandelbrotPartSettings(MandelbrotSettings settings,int counter,int position) : base(settings)
        {
            this.Counter = counter;
            this.Position = position;
        }

        public int Counter { get; set; }

        public int Position { get; set; }
    }
}

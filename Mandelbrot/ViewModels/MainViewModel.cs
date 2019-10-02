using System;
using System.Windows.Input;

namespace Mandelbrot.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MandelbrotCalculator mandelbrot;

        public MainViewModel()
        {
            this.mandelbrot = new MandelbrotCalculator();
            this.Pixels = new byte[0];

            this.CalculateCommand = new RelayCommand((arg) =>
            {
                if (arg is null)
                {
                    throw new ArgumentNullException(nameof(arg));
                }

                var settings = arg as MandelbrotSettings;
                this.Pixels = mandelbrot.Calculate(settings);
                this.FirePropertyChanged(nameof(this.Pixels));
            });
        }

        public ICommand CalculateCommand { get; private set; }

        public byte[] Pixels { get; private set; }
    }
}

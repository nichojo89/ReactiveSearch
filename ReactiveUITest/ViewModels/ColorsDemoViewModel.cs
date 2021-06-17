using ReactiveUI;
using Xamarin.Forms;

namespace ReactiveUITest.ViewModels
{
    public class ColorsDemoViewModel : ReactiveObject
    {
        private int _red;
        public int Red
        {
            get => _red;
            set
            {
                _red = value;
                this.RaiseAndSetIfChanged(ref _red, value);
                this.RaisePropertyChanged(nameof(BackgroundColor));
            }
        }

        private int _blue;
        public int Blue
        {
            get => _blue;
            set
            {
                _blue = value;
                this.RaiseAndSetIfChanged(ref _blue, value);
                this.RaisePropertyChanged(nameof(BackgroundColor));
            }
        }

        private int _green;
        public int Green
        {
            get => _green;
            set
            {
                _green = value;
                this.RaiseAndSetIfChanged(ref _green, value);
                this.RaisePropertyChanged(nameof(BackgroundColor));
            }
        }

        public Color BackgroundColor
        {
            get => Color.FromRgb(Red, Green, Blue);
        }
        public ColorsDemoViewModel()
        {
        }
    }
}
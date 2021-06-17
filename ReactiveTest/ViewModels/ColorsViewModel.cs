using Splat;
using ReactiveUI;
using Xamarin.Forms;

namespace ReactiveTest.ViewModels
{
    public class ColorsViewModel : ReactiveObject, IRoutableViewModel
    {
        public Color BackgroundColor => Color.FromRgb(Red, Green, Blue);

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

        //Routables
        public IScreen HostScreen { get; }
        public string UrlPathSegment => "Colors page";

        public ColorsViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
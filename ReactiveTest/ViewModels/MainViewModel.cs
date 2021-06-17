
using Splat;
using ReactiveUI;
using System.Reactive.Linq;

namespace ReactiveTest.ViewModels
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        /// <summary>
        /// OAPH
        /// </summary>
        private readonly ObservableAsPropertyHelper<string> _firstName;
        public string FirstName => _firstName.Value;

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        public string UrlPathSegment => "Main page";

        public IScreen HostScreen { get; }

        public MainViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            //There are two ways to assign OAPH

            //assign the var
            _firstName = this.WhenAnyValue(vm => vm.Name)
                .Select(name => name.Split(' ')[0])
                .ToProperty(this, nameof(FirstName));

            //out the var
            this.WhenAnyValue(vm => vm.Name)
                .Select(name => name.Split(' ')[0])
                .ToProperty(this, nameof(FirstName), out _firstName);
        }
    }
}
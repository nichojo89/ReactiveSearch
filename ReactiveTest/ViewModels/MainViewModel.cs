using Splat;
using ReactiveUI;
using System.Reactive.Linq;

namespace ReactiveTest.ViewModels
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        /// <summary>
        /// OAPH example
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

        public IScreen HostScreen { get; }
        public const string Colors = "colors";
        public const string SignIn = "signin";
        public const string Contacts = "contacts";
        public string UrlPathSegment => "Main page";
        public ReactiveCommand<string,IRoutableViewModel> NavigateToSampleCommand { get; }
        
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

            //NavigateToSampleCommand = ReactiveCommand.CreateFromObservable<string, IRoutableViewModel>(
            //     (s) => HostScreen.Router.Navigate.Execute(new ColorsViewModel(null))
            //);

            NavigateToSampleCommand = ReactiveCommand.CreateFromObservable<string, IRoutableViewModel>((name) =>
             {

                 switch (name)
                 {
                     case Colors:
                         //TODO, Should I be passing IScreen around
                         //How is this normal done?
                         return HostScreen.Router.Navigate.Execute(new ColorsViewModel(HostScreen));
                     case Contacts:
                        return HostScreen.Router.Navigate.Execute(new ContactsViewModel(HostScreen));
                     case SignIn:
                         return HostScreen.Router.Navigate.Execute(new SignInViewModel(HostScreen));
                 }

                 return null;
             });
        }
    }
}
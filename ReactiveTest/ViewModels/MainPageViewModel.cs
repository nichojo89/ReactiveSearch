using ReactiveUI;
using System.Reactive.Linq;

namespace ReactiveTest
{
    public class MainPageViewModel : ReactiveObject
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

        public MainPageViewModel()
        {
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
using Splat;
using ReactiveUI;
using System.Reactive;
using ReactiveTest.Repositories;
using ReactiveTest.Models;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System;

namespace ReactiveTest.ViewModels
{
    public class SignInViewModel : ReactiveObject,  IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment => "Sign in";

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private readonly IUserProfileRepository _userProfileRepository;

        public SignInViewModel(IScreen screen = null, IUserProfileRepository userProfileRepository = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _userProfileRepository = userProfileRepository ?? Locator.Current.GetService<IUserProfileRepository>();

            _canSignIn = this.WhenAnyValue(
                vm => vm.UserName,
                vm => vm.Password,
                (user, pass) =>
                !string.IsNullOrWhiteSpace(user) &&
                !string.IsNullOrWhiteSpace(pass) &&
                user.Length >= 3 &&
                pass.Length >= 8)
                .DistinctUntilChanged();
        }

        private IObservable<bool> _canSignIn;
        //Is there anything wrong with creating commands like this rather than constructor?
        public ReactiveCommand<Unit, Unit> SignInCommand =>
            ReactiveCommand.CreateFromTask(SignIn, _canSignIn);

        public async Task SignIn()
        {
            //Save profile
            await _userProfileRepository.SaveAsync(new UserProfile()
            {
                Id = "10000",
                Name = UserName,
            });
            //Get profile
            var profile = await _userProfileRepository.GetAsync("10000");
        }
    }
}
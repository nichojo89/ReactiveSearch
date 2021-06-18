using Splat;
using System;
using ReactiveUI;
using System.Reactive;
using ReactiveTest.Models;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveTest.Repositories;
using System.Reactive.Disposables;
using System.Reactive.Threading.Tasks;

namespace ReactiveTest.ViewModels
{
    public class SignInViewModel : ReactiveObject,  IRoutableViewModel, IActivatableViewModel
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

        private bool _isSignInVisible;
        public bool IsSignInVisible
        {
            get => _isSignInVisible;
            set => this.RaiseAndSetIfChanged(ref _isSignInVisible, value);
        }

        private UserProfile _profile;
        public UserProfile Profile
        {
            get => _profile;
            set => this.RaiseAndSetIfChanged(ref _profile, value);
        }

        public ViewModelActivator Activator { get; }

        private IObservable<bool> _canSignIn;
        private readonly IUserProfileRepository _userProfileRepository;

        public SignInViewModel(IScreen screen = null, IUserProfileRepository userProfileRepository = null)
        {
            
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _userProfileRepository = userProfileRepository ?? Locator.Current.GetService<IUserProfileRepository>();

            Activator = new ViewModelActivator();
            //is this a better way fetch data during vm load?
            try
            {
                this.WhenActivated(disposables =>
                {
                    _userProfileRepository.GetAsync("10000")
                    .ToObservable()
                    //Set profile
                    .ToProperty(this, vm => vm.Profile)
                    .DisposeWith(disposables);
                });

                //Set visibility of profile is added
                this.WhenAnyValue(vm => vm.Profile)
                    .ObserveOn(RxApp.TaskpoolScheduler)
                    .Subscribe(profile =>
                    {
                        IsSignInVisible = profile != null;
                    });

                //input validation
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
            catch (Exception e)
            {
                //TODO is there a better way to do this?
            }
        }

        //Is there anything wrong with creating commands like this rather than constructor?
        public ReactiveCommand<Unit, Unit> SignInCommand =>
            ReactiveCommand.CreateFromTask(SignIn, _canSignIn);

        /// <summary>
        /// Adds profile record to cache
        /// </summary>
        /// <returns></returns>
        public async Task SignIn()
        {
            //Save profile
            await _userProfileRepository.SaveAsync(new UserProfile()
            {
                Id = "10000",
                Name = UserName,
            });

            //set profile
            Profile = await _userProfileRepository.GetAsync("10000");
        }
    }
}
using System;
using ReactiveTest.Repositories;
using ReactiveUI;
using Splat;

namespace ReactiveTest.ViewModels
{
    public class SignInViewModel : ReactiveObject,  IRoutableViewModel
    {
        public string UrlPathSegment => "Sign in";
        public IScreen HostScreen { get; }
        private readonly IUserProfileRepository _userProfileRepository;

        public SignInViewModel(IScreen screen = null, IUserProfileRepository userProfileRepository = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _userProfileRepository = userProfileRepository ?? Locator.Current.GetService<IUserProfileRepository>();
        }
    }
}

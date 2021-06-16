using System;
using ReactiveUITest.Services;
using Splat;

namespace ReactiveUITest
{
    public class AppBootstraper
    {
        public AppBootstraper()
        {
            RegisterServices();
        }

        public void RegisterServices()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new ContactService(), typeof(IContactService));
        }
    }
}

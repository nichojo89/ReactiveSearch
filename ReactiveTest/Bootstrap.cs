using Splat;
using System;
using ReactiveUI;
using System.Reactive;
using System.Reflection;
using ReactiveTest.Services;

namespace ReactiveTest
{
    public class Bootstrap
    {
        ReactiveCommand<Unit, Unit> RegisterServices { get; }

        public Bootstrap()
        {
            //Spmething to be  observed
            RegisterServices = ReactiveCommand.Create(Register);

            //Fire off event
            RegisterServices.Execute().Subscribe();

        }

        /// <summary>
        /// Locator Service
        /// </summary>
        private void Register()
        {
            //Register a singleton instance
            Locator.CurrentMutable.RegisterConstant(new ContactService(), typeof(IContactService));

            //Register viewmodels
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
        }
    }
}
using Splat;
using System;
using ReactiveUI;
using Xamarin.Forms;
using System.Reactive;
using System.Reflection;
using ReactiveUI.XamForms;
using ReactiveTest.Services;
using ReactiveTest.ViewModels;

namespace ReactiveTest
{
    public class Bootstrap : ReactiveObject, IScreen
    {
        ReactiveCommand<Unit, Unit> RegisterServices { get; }

        public RoutingState Router { get; }

        public Bootstrap()
        {
            Router = new RoutingState();
            //Spmething to be  observed
            RegisterServices = ReactiveCommand.Create(Register);

            //Fire off event
            RegisterServices.Execute().Subscribe();

            //Navigate to first page
            Router.Navigate.Execute(new MainViewModel());
        }

        /// <summary>
        /// Sets the Main page as a RoutedViewHost
        /// </summary>
        /// <returns></returns>
        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }

        /// <summary>
        /// Locator Service
        /// </summary>
        private void Register()
        {
            //Register Services
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            Locator.CurrentMutable.Register(() => new ContactService(), typeof(IContactService));

            //Register viewmodels
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
        }
    }
}
using System;
using ReactiveUITest.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactiveUITest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            new AppBootstraper();
            MainPage = new ContactsPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

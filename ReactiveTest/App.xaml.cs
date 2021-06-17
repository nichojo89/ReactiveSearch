using Xamarin.Forms;
using ReactiveTest.Pages;

namespace ReactiveTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Register viewmodels & services
            _ = new Bootstrap();

            MainPage = new ContactsPage();
        }
    }
}
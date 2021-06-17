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
            var bootstrap = new Bootstrap();

            MainPage = new ColorsPage();
        }
    }
}
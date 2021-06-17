using Xamarin.Forms;

namespace ReactiveTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Register viewmodels & services
            var bootstraper = new Bootstrap();

            MainPage = bootstraper.CreateMainPage();
        }
    }
}
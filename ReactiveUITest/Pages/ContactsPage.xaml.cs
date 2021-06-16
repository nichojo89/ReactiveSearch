using Xamarin.Forms;
using ReactiveUITest.ViewModels;

namespace ReactiveUITest.Pages
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = new ContactsViewModel();
        }
    }
}
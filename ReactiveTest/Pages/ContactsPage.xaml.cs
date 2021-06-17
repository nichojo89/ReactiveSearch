using ReactiveTest.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace ReactiveTest.Pages
{
    public partial class ContactsPage : ReactiveContentPage<ContactsViewModel>
    {
        public ContactsPage()
        {
            InitializeComponent();
        }
    }
}
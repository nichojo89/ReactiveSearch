using Splat;
using System;
using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUITest.Models;
using System.Collections.ObjectModel;
using ReactiveUITest.Services;


namespace ReactiveUITest.ViewModels
{
    public class ContactsViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<string> _searchResult;
        public string SearchResult => _searchResult?.Value;

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                this.RaiseAndSetIfChanged(ref _searchQuery, value);
            }
        }


        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                this.RaiseAndSetIfChanged(ref _contacts, value);
            }
        }
        private readonly IContactService _contactService;

        public ContactsViewModel(IContactService contactService = null)
        {
            _contactService = contactService ?? Locator.Current.GetService<IContactService>();

            var allContacts = _contactService.GetContacts();
            _contacts = new ObservableCollection<Contact>(allContacts);
            //Filter list
            this.WhenAnyValue(vm => vm.SearchQuery)
                //Delay
                .Throttle(TimeSpan.FromSeconds(1))
                //Do something
                .Subscribe(query =>
                {
                    if (string.IsNullOrWhiteSpace(query))
                    {
                        Contacts = new ObservableCollection<Contact>(allContacts);
                        return;
                    }
                        
                    //Filter list by search query
                    var filteredContacts = allContacts
                    .Where(x => x.FullName.ToLower().Contains(query.ToLower())
                    || x.Phone.Contains(query)
                    || x.Email.ToLower().Contains(query.ToLower()));

                    Contacts = new ObservableCollection<Contact>(filteredContacts);
                });

            //Render search subtitle
            this.WhenAnyValue(vm => vm.Contacts)
                .Select(contacts =>
                {

                    if (contacts.Count == allContacts.Count())
                        return "No results found";
                    else
                        return $"{Contacts.Count} have been found for {SearchQuery}";
                })
                .ToProperty(this, vm => vm.SearchResult, out _searchResult);

        }
    }
}

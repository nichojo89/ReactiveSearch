using Splat;
using System;
using ReactiveUI;
using System.Linq;
using DynamicData;
using ReactiveTest.Models;
using System.Reactive.Linq;
using ReactiveTest.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using DynamicData.Binding;
using System.Reactive;
using System.Diagnostics;

namespace ReactiveTest.ViewModels
{
    public class ContactsViewModel : ReactiveObject, IRoutableViewModel
    {
        /// <summary>
        /// OAPH search result
        /// </summary>
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

        private IEnumerable<Contact> _allContacts;
        //TODO how to use source cache?
        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                this.RaiseAndSetIfChanged(ref _contacts, value);
            }
        }

        //Routable events
        public IScreen HostScreen { get; }
        public string UrlPathSegment => "Contacts View";

        //Services
        private readonly IContactService _contactService;

        public ReactiveCommand<Unit, Unit> ClearCommand { get; set; }
        public ContactsViewModel(IScreen screen = null, IContactService contactService = null)
        {
            //TODO screen & service are always null
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _contactService = contactService
                ?? Locator.Current.GetService<IContactService>();

            _allContacts = _contactService.GetContacts();
            _contacts = new ObservableCollection<Contact>(_allContacts);

            //Filter list
            this.WhenAnyValue(vm => vm.SearchQuery)
                //Delay
                .Throttle(TimeSpan.FromSeconds(1))
                //TODO Is there a way to do this without causing exception?s?
                //.Select(s => s.ToLower())
                //Do something
                .Subscribe(query =>
                {
                    if (string.IsNullOrWhiteSpace(query))
                    {
                        //Reset contacts
                        Contacts = new ObservableCollection<Contact>(_allContacts);
                        return;
                    }
                    //Filter list by search query
                    var filteredContacts = _allContacts
                    .Where(x =>
                    x.FullName.ToLower().Contains(query.ToLower())
                    || x.Phone.ToLower().Contains(query.ToLower())
                    || x.Email.ToLower().Contains(query.ToLower()));

                    Contacts = new ObservableCollection<Contact>(filteredContacts);
                });

            //Render search subtitle
            this.WhenAnyValue(vm => vm.Contacts)
                .Select(c =>
                {
                    if (c.Count == _allContacts.Count())
                        return "No results found";
                    else
                        return $"{Contacts.Count} have been found for {SearchQuery}";
                })
                .ToProperty(this, vm => vm.SearchResult, out _searchResult);

            //inline OAPH
            var canExecuteClear = this
                .WhenAnyValue(vm => vm.SearchQuery)
                .Select(query => !string.IsNullOrWhiteSpace(query));

            //Create commands
            ClearCommand = ReactiveCommand.Create(ClearSearch);
            ClearCommand.ThrownExceptions.Subscribe(ex =>
            {
                Debug.WriteLine(ex);
            });
        }

        private void ClearSearch()
        {
            SearchQuery = string.Empty;
        }
    }
}
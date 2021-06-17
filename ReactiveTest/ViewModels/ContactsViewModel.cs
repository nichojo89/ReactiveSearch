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

namespace ReactiveTest.ViewModels
{
    public class ContactsViewModel : ReactiveObject
    {
        /// <summary>
        /// OAPH Search
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

        private readonly SourceCache<Contact, int> _contacts = new SourceCache<Contact, int>(c => c.ID);

        /// <summary>
        /// DynamicData uses .NET types to expose to the outside world,
        /// such as ReadOnlyObservableCollection<T>,
        /// rather than exposing their own types.
        /// </summary>
        private readonly ReadOnlyObservableCollection<Contact> _contactsList;
        public ReadOnlyObservableCollection<Contact> Contacts => _contactsList;

        //Services
        private readonly IContactService _contactService;
        
        public ContactsViewModel(IContactService contactService = null)
        {
            _contactService = contactService
                ?? Locator.Current.GetService<IContactService>();

            //TODO is this the best return type?
            var contacts = _contactService.GetContacts();

            //Add contacts to source cache
            _contacts.AddOrUpdate(contacts);

            //Bind source cache to binding
            _contacts
                .Connect()
                .Bind(out _contactsList)
                .Subscribe();

            //Filter list
            this.WhenAnyValue(vm => vm.SearchQuery)
                //Delay
                .Throttle(TimeSpan.FromSeconds(1))
                .Select(s => s.ToLower())
                //Do something
                .Subscribe(query =>
                {
                    if (string.IsNullOrWhiteSpace(query))
                    {
                        //Reset search
                        _contacts.Clear();
                        //TODO is this the best way to do this?
                        _contacts.AddOrUpdate(contacts);
                        return;
                    }
                    //Filter list by search query
                    var filteredContacts = contacts
                    .Where(x =>
                    x.FullName.ToLower().Contains(query)
                    || x.Phone.ToLower().Contains(query)
                    || x.Email.ToLower().Contains(query));

                    //Reset search
                    _contacts.Clear();
                    //TODO is this the best way to do this?
                    _contacts.AddOrUpdate(filteredContacts);
                });

            //Render search subtitle
            this.WhenAnyValue(vm => vm.Contacts)
                .Select(c =>
                {

                    if (c.Count == _contacts.Count)
                        return "No results found";
                    else
                        return $"{Contacts.Count} have been found for {SearchQuery}";
                })
                .ToProperty(this, vm => vm.SearchResult, out _searchResult);

        }
    }
}
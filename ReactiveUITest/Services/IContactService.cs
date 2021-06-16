using System;
using System.Collections.Generic;
using ReactiveUITest.Models;

namespace ReactiveUITest.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
    }

    public class ContactService : IContactService
    {
        public IEnumerable<Contact> GetContacts()
        {
            return new List<Contact>()
            {
                new Contact("One name","One num","One email"),
                new Contact("Two name","Two num","Two email"),
                new Contact("Three name","Three num","Three email"),
                new Contact("Four name","Four num","Four email"),
                new Contact("Five name","Five num","Five email"),
                new Contact("Six name","Six num","Six email"),
                new Contact("Seven name","Seven num","Seven email"),
                new Contact("Eight name","Eight num","Eight email"),
                new Contact("Nine name","Nine num","Nine email"),
                new Contact("Ten name","Ten num","Ten email")
            };
        }
    }
}

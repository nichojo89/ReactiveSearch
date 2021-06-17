using ReactiveTest.Models;
using System.Collections.Generic;

namespace ReactiveTest.Services
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
                new Contact(1,"One name","One num","One email"),
                new Contact(2,"Two name","Two num","Two email"),
                new Contact(3,"Three name","Three num","Three email"),
                new Contact(4,"Four name","Four num","Four email"),
                new Contact(5,"Five name","Five num","Five email"),
                new Contact(6,"Six name","Six num","Six email"),
                new Contact(7,"Seven name","Seven num","Seven email"),
                new Contact(8,"Eight name","Eight num","Eight email"),
                new Contact(9,"Nine name","Nine num","Nine email"),
                new Contact(10,"Ten name","Ten num","Ten email")
            };
        }
    }
}
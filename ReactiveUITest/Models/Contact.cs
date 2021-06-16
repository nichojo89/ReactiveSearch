using System;
namespace ReactiveUITest.Models
{
    public class Contact
    {
        public string FullName {get; set;}
        public string Phone {get; set;}
        public string Email {get; set;}

        public Contact(string fullname, string phone, string email)
        {
            FullName = fullname;
            Phone = phone;
            Email = email;
        }
    }
}
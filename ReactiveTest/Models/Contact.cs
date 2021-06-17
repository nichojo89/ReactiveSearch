namespace ReactiveTest.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public Contact(int id, string fullname, string phone, string email)
        {
            ID = id;
            FullName = fullname;
            Phone = phone;
            Email = email;
        }
    }
}
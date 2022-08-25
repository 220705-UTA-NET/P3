namespace server.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Customer(int id, string firstname, string lastname, string email, string phonenumber, string password)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            PhoneNumber = phonenumber;
            Password = password;

        }
    }
}

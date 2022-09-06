using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Support
    {
        public int SupportId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public Support() { }

        public Support(int customer_id, string username, string password)
        {
            SupportId = customer_id;
            UserName = username;
            Password = password;
        }

        public Support(int customer_id, string firstName, string lastName, string userName, string email, string phone)
        {
            SupportId = customer_id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Phone = phone;
        }

        public Support(string firstName, string lastName, string userName, string email, string phone, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }
    }
}

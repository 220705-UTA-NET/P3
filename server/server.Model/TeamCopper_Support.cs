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

        public Support(int support_id, string username, string password)
        {
            SupportId = support_id;
            UserName = username;
            Password = password;
        }
        public Support(int support_id, string username, string password, string firstName, string lastName)
        {
            SupportId = support_id;
            UserName = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public Support(int support_id, string firstName, string lastName, string userName, string email, string phone)
        {
            SupportId = support_id;
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

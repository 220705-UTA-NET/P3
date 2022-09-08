using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public Customer() { }

        public Customer(int customer_id, string username, string password)
        {
            CustomerId = customer_id;
            UserName = username;
            Password = password;
        }
        public Customer(int customer_id, string username, string password, string firstName, string lastName)
        {
            CustomerId = customer_id;
            UserName = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
        public Customer(int customer_id, string firstName, string lastName, string userName, string email, string phone)
        {
            CustomerId = customer_id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Phone = phone;
        }

        public Customer(string firstName, string lastName, string userName, string email, string phone, string password)
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

using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Data
{
    public interface TeamCopper_IRepo
    {
        Task<Customer> getCustomerByEmail(string email);
        Task<Customer> getCustomerByUserName(string username);
        Task<Customer> customerLogInAsync(string username, string password);
        Task<Customer> registerCustomerAsync(string FirstName, string LastName, string UserName, string Email, string Phone, string Password);
        Task<Support> getSupportByEmail(string email);
        Task<Support> getSupportByUserName(string username);
        Task<Support> supportLogInAsync(string username, string password);
        
    }
}

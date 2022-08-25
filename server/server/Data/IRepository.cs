using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Data
{
    public interface IRepository
    {
        Task<Customer> GetCustomerAsync(int id);
        Task UpdateCustomerAsync(int id, string email, int phonenumber, string password);
        Task<IEnumerable<Account>> GetCustomerAccountsAsync(int id);
    }
}

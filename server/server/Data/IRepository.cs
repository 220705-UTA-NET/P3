using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<StatusCodeResult> UpdateCustomerAsync(int id, string firstname, string lastname, string email, int phonenumber, string password);
    }
}

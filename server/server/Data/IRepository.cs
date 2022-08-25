using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Data
{
    public interface IRepository
    {
        Task<Customer> GetCustomerAsync(int id);
<<<<<<< HEAD
        Task UpdateCustomerAsync(int id, string email, int phonenumber, string password);
        Task<IEnumerable<Account>> GetCustomerAccountsAsync(int id);
=======
        Task UpdateCustomerAsync(int id, string email, string phonenumber, string password);
>>>>>>> eaa502da02347e28f84e16560b93767cb859e813
    }
}

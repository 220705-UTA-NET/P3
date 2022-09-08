using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;

namespace server.Data
{
    public interface Bronze_IRepository
    {
        Task<DMODEL_Customer> GetCustomerAsync(int id);
        Task UpdateCustomerAsync(int id, string firstName, string lastName, string email, string phoneNumber, string password);
        Task<IEnumerable<DMODEL_Account>> GetCustomerAccountsAsync(int id);
        Task<DMODEL_Account> AddAccountAsync(int customerId, int accountType);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;

namespace server.UnitTests
{
    public interface ITransactionRepository
    {

        IList<Transaction> GetAll();

        Transaction GetByTransactionId(int id);

        Transaction GetByAccountId(int id);

        IList<Transaction> GetByTransactionType(int transactionType);

        bool InsertTransaction(Transaction t);

        bool UpdateTransaction(Transaction t);

        bool DeleteTransaction(Transaction t);

        IList<Transaction> GetTransactionFromCustomer();


    }
}

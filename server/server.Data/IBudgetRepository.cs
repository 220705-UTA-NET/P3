using Microsoft.AspNetCore.Mvc;
using server.Model;
//using Server_DataModels;

namespace server.Data;

public interface IBudgetRepository
{
    Task<IEnumerable<DMODEL_Transaction>> GetAllTransactionsAsync();
    Task<int> GetTransactionsSumAsync(Budget budget);
    Task<ActionResult> InsertBudgetAsync(Budget budget);
    Task<ActionResult> UpdateBudgetAsync(Budget budget);
    Task<ActionResult> DeleteBudgetAsync(int budgetId);

    Task<IEnumerable<Budget>> GetAllBudgetsFromCustomerAsync(int customerId); 
}
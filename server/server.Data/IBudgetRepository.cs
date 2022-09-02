using Microsoft.AspNetCore.Mvc;
using server.Model;

namespace server.Data;

public interface IBudgetRepository
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    Task<int> GetTransactionsSumAsync(Budget budget);
    Task<ActionResult> InsertBudgetAsync(Budget budget);
    Task<ActionResult> UpdateBudgetAsync(Budget budget);
    Task<ActionResult> DeleteBudgetAsync(int budgetId);

    Task<IEnumerable<Budget>> GetAllBudgetsFromCustomerAsync(int customerId); 
}
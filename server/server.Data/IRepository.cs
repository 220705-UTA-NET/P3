using Microsoft.AspNetCore.Mvc;
using server.Model;

namespace server.Data;

public interface IRepository
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    Task<int> GetTransactionsSumAsync();
    Task<Budget> InsertBudgetAsync(Budget budget);
    Task<Budget> UpdateBudgetAsync(Budget budget);
    Task DeleteBudgetAsync(int budgetId);
}
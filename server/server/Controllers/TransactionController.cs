using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Model;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IRepository _repo;
    // GET

    public TransactionController(IRepository repo)
    {
        _repo = repo;
    }
  

    [HttpGet("AllTransactions")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
    {
        IEnumerable<Transaction>? transactions = null;
        
        try
        {
            transactions = await _repo.GetAllTransactionsAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }

        return transactions == null ? new BadRequestResult() : new JsonResult(transactions.ToList());
        
    }

    [HttpGet("transactionsaccount")]
    public async Task<ActionResult<int>> GetAllAccountTransactions()
    {
        int sum = 0;

        try
        {
            sum = await _repo.GetTransactionsSumAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

        }

        return sum;

    }

    [HttpPost("InsertBudget")]
    public async Task<ActionResult> InsertBudget([FromBody] Budget budget)
    {
        ActionResult s = await _repo.InsertBudgetAsync(budget);
        return s;
    }



}
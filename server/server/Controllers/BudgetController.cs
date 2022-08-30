using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Model;

namespace server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {

        private readonly IRepository _repo;

        public BudgetController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("CustomerBudgets/{customerId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetCustomerBudgets(int customerId)
        {
            IEnumerable<Budget>? budgets = null;

            try
            {
                budgets = await _repo.GetAllBudgetsFromCustomerAsync(customerId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            if (budgets == null||!budgets.Any()) return NoContent();
           
            foreach (var bud in budgets)
            {
                try
                {
                    int amount = await _repo.GetTransactionsSumAsync(bud);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            return budgets == null ? new BadRequestResult() : new JsonResult(budgets.ToList());

        }

        [HttpPost("InsertBudget")]
        public async Task<ActionResult> InsertBudget([FromBody] Budget budget)
        {
            ActionResult s = await _repo.InsertBudgetAsync(budget);
            return s;
        }


        [HttpPut("UpdateBudget")]
        public async Task<ActionResult> UpdateBudget(Budget budget)
        {
            ActionResult s = await _repo.UpdateBudgetAsync(budget);
            return s;
        }


        [HttpDelete("DeleteBudget/{budgetId}")]
        public async Task<ActionResult> DeleteBudget(int budgetId)
        {
            ActionResult s = await _repo.DeleteBudgetAsync(budgetId);
            return s;
        }

    }




    
}

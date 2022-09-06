using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Model;
using Server_DataModels;
using System.Collections;

namespace server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {

        private readonly IBudgetRepository _repo;

        public BudgetController(IBudgetRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("CustomerBudgets/{customerId}")]
        public async Task<ActionResult<IEnumerable<DMODEL_Transaction>>> GetCustomerBudgets(int customerId)
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


        [HttpGet("GetCustomerBudgetsWarning/{customerId}")]
        public async Task<ActionResult> BudgetWarning(int customerId)
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

            if (budgets == null || !budgets.Any()) return BadRequest();
      
            ArrayList list = new();
            
            foreach (var bud in budgets)
            {
                try
                {
                    int amount = await _repo.GetTransactionsSumAsync(bud);
                    if(bud.MonthlyAmount - amount < bud.WarningAmount)
                    {
                        double rem = bud.MonthlyAmount - amount;
                        var addTo = new
                        {
                            budId = bud.BudgetId,
                            remaining = rem,
                            lim = bud.MonthlyAmount,
                            accId = bud.AccountId
                        };
                        list.Add(addTo);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            return new JsonResult(list);
        }
    }
}

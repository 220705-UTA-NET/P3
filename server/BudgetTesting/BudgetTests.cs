
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using server.Controllers;
using server.Data;
using server.Model;

namespace BudgetTesting
{
    public class BudgetTests
    {
        [Fact]
        public async void GetCustomerBudgetsNull() //testing for null return 
        {
            //Creating null list of budgets
            List<Budget> budgets = new(); 
            //Fake customer
            int customerid = 1;

            //logging
            Console.WriteLine("Null return test started.");

            //creating Mock IRepo
            Mock<IBudgetRepository> mockRepo = new();

            //Fake call to the repo
            mockRepo.Setup(repo => repo.GetAllBudgetsFromCustomerAsync(customerid)).ReturnsAsync(budgets);

            //Creating the mock controller
            var bud = new BudgetController(mockRepo.Object);

            //Fake call to the endpoint
            var result = await bud.GetCustomerBudgets(customerid);

            var resultContent = result.Result as ContentResult;

            var item = result.Value;

            //Checking that the list is empty. 
            Assert.Empty(item);
        }


    }
}
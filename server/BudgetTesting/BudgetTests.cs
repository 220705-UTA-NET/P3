
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using server.Controllers;
using server.Data;
using server.Model;
using System.Data.SqlClient;


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

        [Fact]
        public async void Insert_Testing_400()
        {
            // ARRANGE
            Budget budget = new Budget();
            Mock<IBudgetRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.InsertBudgetAsync(budget)).ReturnsAsync(new StatusCodeResult(400));
            var mockController = new BudgetController(mockRepo.Object);

            // ACT
            var result = await mockController.InsertBudget(budget);
            var resultContent = result as StatusCodeResult;

            // ASSERT
            Assert.Equal(StatusCodes.Status400BadRequest, resultContent.StatusCode);


        }

        [Fact]
        public async void Insert_Testing_200()
        {
            // ARRANGE
            Budget budget = new Budget();
            Mock<IBudgetRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.InsertBudgetAsync(budget)).ReturnsAsync(new StatusCodeResult(200));
            var mockController = new BudgetController(mockRepo.Object);

            // ACT
            var result = await mockController.InsertBudget(budget);
            var resultContent = result as StatusCodeResult;

            // ASSERT
            Assert.Equal(StatusCodes.Status200OK, resultContent.StatusCode);
        }

        [Fact]
        public async void Update_Testing_500()
        {
            // ARRANGE
            Budget budget = new Budget();
            Mock<IBudgetRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.UpdateBudgetAsync(budget)).ReturnsAsync(new StatusCodeResult(500));
            var mockController = new BudgetController(mockRepo.Object);

            // ACT
            var result = await mockController.UpdateBudget(budget);
            var resultContent = result as StatusCodeResult;

            // ASSERT
            Assert.Equal(StatusCodes.Status500InternalServerError, resultContent.StatusCode);
        }

        [Fact]
        public async void Update_Testing_200()
        {
            // ARRANGE
            Budget budget = new Budget();
            Mock<IBudgetRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.UpdateBudgetAsync(budget)).ReturnsAsync(new StatusCodeResult(200));
            var mockController = new BudgetController(mockRepo.Object);

            // ACT
            var result = await mockController.UpdateBudget(budget);
            var resultContent = result as StatusCodeResult;

            // ASSERT
            Assert.Equal(StatusCodes.Status200OK, resultContent.StatusCode);
        }

        [Fact]
        public async void Delete_Testing_500()
        {
            // ARRANGE
            // Budget budget = new Budget();
            int budgetID = 1;
            Mock<IBudgetRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.DeleteBudgetAsync(budgetID)).ReturnsAsync(new StatusCodeResult(500));
            var mockController = new BudgetController(mockRepo.Object);

            // ACT
            var result = await mockController.DeleteBudget(budgetID);
            var resultContent = result as StatusCodeResult;

            // ASSERT
            Assert.Equal(StatusCodes.Status500InternalServerError, resultContent.StatusCode);
        }

        [Fact]
        public async void Delete_Testing_200()
        {
            // ARRANGE
            // Budget budget = new Budget();
            int budgetID = 1;
            Mock<IBudgetRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.DeleteBudgetAsync(budgetID)).ReturnsAsync(new StatusCodeResult(200));
            var mockController = new BudgetController(mockRepo.Object);

            // ACT
            var result = await mockController.DeleteBudget(budgetID);
            var resultContent = result as StatusCodeResult;

            // ASSERT
            Assert.Equal(StatusCodes.Status200OK, resultContent.StatusCode);
        }

    }
}
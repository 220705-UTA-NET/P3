using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using server.Controllers;
using server.Data;
//using Server_DataModels;
//using server_Database;
using server.DTOs;
using server.Model;
using System.Text.Json;

namespace server_Test
{
    public class AccountTests
    {
        private AccountsController _controller;
        private Mock<Bronze_IRepository> _repository;
        private Mock<ILogger<AccountsController>> _logger;

        public void SetUp()
        {
            _repository = new Mock<Bronze_IRepository>();
            _logger = new Mock<ILogger<AccountsController>>();
            _controller = new AccountsController(_repository.Object, _logger.Object);
        }

        [Fact]
        public async void GetAccounts_IdOf1_Return3Accounts()
        {
            // Arrange
            SetUp();
            List<DMODEL_Account> result = new List<DMODEL_Account>();
            result.Add(new(1, 1, 100.00, 1));
            result.Add(new(2, 1, 50.50, 1));
            result.Add(new(3, 1, .50, 1));

            _repository.Setup(p => p.GetCustomerAccountsAsync(1)).ReturnsAsync(result.AsEnumerable);

            // Act
            List<server.DTOs.Account> output = (await _controller.GetAccounts(1)).Value;

            // Assert
            string expected = JsonSerializer.Serialize(result, typeof(List<server.DTOs.Account>));
            string actual = JsonSerializer.Serialize(output, typeof(List<server.DTOs.Account>));

            Assert.Equal(expected, actual);
        }
    }
}
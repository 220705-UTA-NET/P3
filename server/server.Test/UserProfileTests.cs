using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using server.Controllers;
using server.Data;
//using server_Database;
//using Server_DataModels;
using server.DTOs;
using server.Model;
using System.Text.Json;
using Customer = server.DTOs.Customer;

namespace server_Test
{
    public class UserProfileTests
    {
        private UserProfileController _controller;
        private Mock<Bronze_IRepository> _repository;
        private Mock<ILogger<UserProfileController>> _logger;

        public void SetUp()
        {
            _repository = new Mock<Bronze_IRepository>();
            _logger = new Mock<ILogger<UserProfileController>>();
            _controller = new UserProfileController(_repository.Object, _logger.Object);
        }

        [Fact]
        public async void GetUserProfile_IdOf1_ReturnCustomer()
        {
            // Arrange
            SetUp();
            DMODEL_Customer result = new DMODEL_Customer(1, "FirstName", "LastName", "Email", "111-111-1111", "password");
            _repository.Setup(p => p.GetCustomerAsync(1)).ReturnsAsync(result);

            // Act

            Customer output = (await _controller.GetUserProfile(1)).Value;
            string expected = JsonSerializer.Serialize(result, typeof(Customer));
            string actual = JsonSerializer.Serialize(output, typeof(Customer));

            // Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void ModifyUserProfile_IdOf1_Success()
        {
            //Arrange
            SetUp();
            Customer customer = new Customer(1, "FirstName", "LastName", "Email", "111-111-1111", "password");
            _repository.Setup(p => p.UpdateCustomerAsync(customer.id, customer.firstName, customer.lastName, customer.email, customer.phoneNumber, customer.password));
            // Act

            var result = (StatusCodeResult)await _controller.ModifyUserProfile(customer);

            // Assert
            Assert.True(result.StatusCode.Equals(201));
        }
    }
}

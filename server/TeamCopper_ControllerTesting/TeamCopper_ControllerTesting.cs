using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using server.Controllers;
using server.Data;

namespace TeamCopper_ControllerTesting
{
    public class TeamCopper_ControllerTesting
    {
        Mock<TeamCopper_IRepo> MockRepo = new();

        Mock<ILogger<TeamCopper_CustomerController>> MockLogger = new();
        // testing login happy path 
        [Fact]
        public async Task LogIn_BadPath()
        {
            //Arrange
            var expectedException = new Exception("Test");


            MockRepo.Setup(x =>
            x.customerLogInAsync(It.IsAny<string>(), It.IsAny<string>())
            ).ThrowsAsync(expectedException);

            var controller = new TeamCopper_CustomerController(MockLogger.Object, MockRepo.Object);

            // Act 
            var result = await controller.LogIn();

            //Assert
            Assert.NotNull(result);
            // Assert.IsType<ObjectResult>(result);

            //Assert.Equal(500, ObjectResult?.StatusCode);


        }
        [Fact]
        public async Task LogIn_HappyPath()
        {
            //Arrange 
            // string username = "test";
            //string password = "test";
            //Arrange

            var controller = new TeamCopper_CustomerController(MockLogger.Object, MockRepo.Object);

            // Act
            var result = await controller.LogIn();

            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task Register_HappyPath()
        {
            /*MockRepo.Setup(x =>
             x.registerCustomerAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
            ).ReturnsAsync(); */

            var controller = new TeamCopper_CustomerController(MockLogger.Object, MockRepo.Object);

            // Act
            var result = await controller.Register();

            // Assert
            Assert.NotNull(result);



        }
        [Fact]
        public async Task Register_ExceptionPath()
        {
            var expectedException = new Exception("Test");

            /*MockRepo.Setup(x =>
             x.registerCustomerAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
            ).ReturnsAsync(new StatusCodeResult(500));*/

            var controller = new TeamCopper_CustomerController(MockLogger.Object, MockRepo.Object);

            // Act
            var result = await controller.Register();

            // Assert
            Assert.IsType<StatusCodeResult>(result);

        }
    }
}
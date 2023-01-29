using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService.
            Setup(service => service.GetAllUsers()).
            ReturnsAsync(UsersFixture.GetTestUsers());
        var controller = new UsersController(mockUserService.Object);

        //Act
        var result = (OkObjectResult)await controller.Get();

        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService.
            Setup(service => service.GetAllUsers()).
            ReturnsAsync(UsersFixture.GetTestUsers());

        var controller = new UsersController(mockUserService.Object);

        //Act
        var result = await controller.Get();

        //Assert
        mockUserService.Verify(service => service.GetAllUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        //arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService.
            Setup(service => service.GetAllUsers()).
            ReturnsAsync(UsersFixture.GetTestUsers());
        var controller = new UsersController(mockUserService.Object);

        //act
        var result = await controller.Get();

        //assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnFailed_NoUsers()
    {
        //arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService.
            Setup(service => service.GetAllUsers()).
            ReturnsAsync(new List<User>());
        var controller = new UsersController(mockUserService.Object);
        //act
        var result = await controller.Get();

        //assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }


}
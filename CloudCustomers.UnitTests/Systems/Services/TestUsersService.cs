using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public  class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpRequest()
        {
            var httpHandlerMock = MockHttpMessageHandler<User>.SetupBasicList(UsersFixture.GetTestUsers());
            var httpClient = new HttpClient(httpHandlerMock.Object);
            //Arrange
            var sut = new UserService(httpClient);
            //Act
            await sut.GetAllUsers();
            //Assert
            //Verify Http Request is made
            httpHandlerMock.Protected().
                Verify(
                "SendAsync", 
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req=>req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsLiftOfUsers()
        {
            //arrange
            var httpHandlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(httpHandlerMock.Object);

            var sut = new UserService(httpClient);
            //act
           var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(0);
            


        }
    }
}

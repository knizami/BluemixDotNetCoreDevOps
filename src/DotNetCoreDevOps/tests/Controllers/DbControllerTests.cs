using Xunit;
using Xunit.Abstractions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudantDotNet.Services;
using CloudantDotNet.Models;
using CloudantDotNet.Controllers;
using Moq;
using Newtonsoft.Json;

namespace TestControllers.Tests.UnitTests
{
    public class DbControllerTests
    {
        DbController _dbcontroller;
        Auth[] ValidUsers = new Auth[] {
            new Auth { username = "user1", password = "password" },
            new Auth { username = "knizami", password = "passw0rd" }
        };

        Auth[] InvalidUsers = new Auth[] {

        };

        public DbControllerTests()
        {
        }


        [Fact]
        public async Task Valid_Login_Should_Return_JSON_Success()
        {
            var MockDbSvc = new Mock<ICloudantService>();
            MockDbSvc.Setup(svc => svc.LoginAsync(It.Is<Auth>(u => u.username == "knizami")))
            .ReturnsAsync(Task.FromResult(GetSuccessLogin()));
            _dbcontroller = new DbController(MockDbSvc.Object);
            Auth userlogin = new Auth()
            {
                username = "knizami",
                password = "passw0rd"
            };

            var result = await _dbcontroller.Login(userlogin);
            Console.WriteLine(result);
            var loginResult = Assert.IsType<string>(result);
            Assert.IsType<AuthResult>(JsonConvert.DeserializeObject<AuthResult>(result));



        }

        private async Task<dynamic> GetSuccessLogin()
        {
            return JsonConvert.SerializeObject(new AuthResult()
            {
                status = "success"
            });
        }
    }
}
using Xunit;
using Xunit.Abstractions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudantDotNet.Services;
using CloudantDotNet.Models;
using Moq;

namespace TestControllers.Tests.UnitTests
{
    public class DbControllerTests
    {
        [Fact]
        public async Task Payees_ReturnsAJSONResult_WithAListOfPayees()
        {
            var MockDbSvc = new Mock<ICloudantService>();
            MockDbSvc.Setup(svc => svc.GetPayeesAsync(It.IsAny<Auth>()))
            .Returns(Task.FromResult(GetPayees()));

        }

        private async Task<dynamic> GetPayees()
        {
            CheckResult checks = new CheckResult();

            return checks;
        }


    }
}
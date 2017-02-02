using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudantDotNet.Models;
using CloudantDotNet.Services;

namespace CloudantDotNet.Controllers
{
    [Route("api/[controller]")]
    public class DbController : Controller
    {
        private ICloudantService _cloudantService;

        public DbController(ICloudantService cloudantService)
        {
            _cloudantService = cloudantService;
        }

        [HttpPost]
        public async Task<dynamic> Create(ToDoItem item)
        {
            return await _cloudantService.CreateAsync(item);
        }

        [HttpPost("/login")]
        public async Task<dynamic> Login(Auth user)
        {

            Console.WriteLine("User input: " + user.username);
            return await _cloudantService.LoginAsync(user);
        }


        [HttpPost("/accounts")]
        public async Task<dynamic> GetAll(Auth user)
        {

            return await _cloudantService.GetAccountsAsync(user);
        }

        [HttpPost("/accounttypes")]
        public async Task<dynamic> GetTypes(Auth user)
        {

            return await _cloudantService.GetAccountTypesAsync(user);
        }


        [HttpPost("/payees")]
        public async Task<dynamic> GetPayees(Auth user)
        {

            return await _cloudantService.GetPayeesAsync(user);
        }


        [HttpPost("/checks")]
        public async Task<dynamic> GetChecks(Auth user)
        {

            return await _cloudantService.GetChecksAsync(user);
        }

        [HttpPost("/savecheck")]
        public async Task<dynamic> SaveCheck([FromBody] SaveCheck savecheckreq)
        {
            Console.WriteLine("Received Check: " + savecheckreq.check.payee + "User is: " + savecheckreq.username);
            return await _cloudantService.SaveChecksAsync(savecheckreq);
        }



    }
}
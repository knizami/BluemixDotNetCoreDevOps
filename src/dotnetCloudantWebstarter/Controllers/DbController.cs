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
            _cloudantService._dbName = "account";
            return await _cloudantService.GetAccountsAsync(user);
        }

        [HttpPut]
        public async Task<string> Update(ToDoItem item)
        {
            return await _cloudantService.UpdateAsync(item);
        }

        [HttpDelete]
        public async Task<dynamic> Delete(ToDoItem item)
        {
            return await _cloudantService.DeleteAsync(item);
        }
    }
}
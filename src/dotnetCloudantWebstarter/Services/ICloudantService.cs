using CloudantDotNet.Models;
using System.Threading.Tasks;

namespace CloudantDotNet.Services
{
    public interface ICloudantService
    {
        string _dbName { get; set; }

        Task<dynamic> LoginAsync(Auth user);
        Task<dynamic> CreateAsync(ToDoItem item);
        Task<dynamic> DeleteAsync(ToDoItem item);
        Task<dynamic> GetAccountsAsync(Auth user);
        Task PopulateTestData();
        Task<string> UpdateAsync(ToDoItem item);
    }
}
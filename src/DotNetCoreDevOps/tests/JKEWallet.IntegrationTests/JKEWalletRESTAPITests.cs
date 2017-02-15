using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using CloudantDotNet.Models;
using Newtonsoft.Json;
using Xunit;
using System.Collections.Generic;

namespace JKEWallet.IntegrationTests
{
    #region snippet_WebDefault
    public class JKETWalletLoginShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public JKETWalletLoginShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnLoginSuccess()
        {

            Auth validlogin = new Auth()
            {
                username = "default",
                password = "passw0rd"
            };

            // Act

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", validlogin.username),
                new KeyValuePair<string, string>("password", validlogin.password)
            });

            var response = await _client.PostAsync("/login", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            AuthResult successlogin = new AuthResult()
            {
                status = "success",
                username = "default"
            };
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            AuthResult result = JsonConvert.DeserializeObject<AuthResult>(responseString);
            Assert.Equal("success", result.status);



        }
    }
    #endregion
}
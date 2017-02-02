using CloudantDotNet.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace CloudantDotNet.Services
{
    public class CloudantService : ICloudantService
    {
        public string _dbName { get; set; }
        private readonly Creds _cloudantCreds;

        public CloudantService(Creds creds)
        {
            _cloudantCreds = creds;
        }

        public async Task<dynamic> LoginAsync(Auth user)
        {
            this._dbName = "users";

            Console.WriteLine("Auth sent is: " + user.username);
            using (var client = CloudantClient())
            {
                //var query = "{ \"selector\": { \"username\": \"" + user.username + "\", \"password\": \"" + user.password + "\"}}";
                String[] filterquery = new String[] { "username", "password" };
                CloudantQuery query = new CloudantQuery(user, filterquery);

                //var response = await client.PostAsync(_dbName + "/_find", new StringContent(query, Encoding.UTF8, "application/json"));
                var response = await client.PostAsJsonAsync(_dbName + "/_find", query);

                Console.WriteLine("Query is: " + JsonConvert.SerializeObject(query));
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsAsync<CloudantAuthResult>();
                    //var responseJson = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("Response was: " + responseJson);
                    Console.WriteLine("Response was: " + responseJson.docs.Length);
                    if (responseJson.docs.Length > 0)
                    {
                        AuthResult result = new AuthResult();
                        result.status = "success";
                        result.username = user.username;
                        return JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        AuthResult result = new AuthResult();
                        result.status = "Invalid Username or Password";
                        return JsonConvert.SerializeObject(result);
                    }
                }
                string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase + "Request Message:" + response.RequestMessage;
                Console.WriteLine(msg);
                return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
            }
        }

        public async Task<dynamic> GetAccountsAsync(Auth user)
        {
            using (var client = CloudantClient())
            {
                var userlogin = await this.LoginAsync(user);
                AuthResult loginresult = JsonConvert.DeserializeObject<AuthResult>(userlogin);
                //var query = "{ \"selector\": { \"username\": \"" + user.username + "\", \"password\": \"" + user.password + "\"}}";

                if (loginresult.status.Equals("success"))
                {
                    this._dbName = "account";
                    String[] filterquery = new String[] { "name", "type", "balance" };
                    CloudantQuery query = new CloudantQuery(new { username = user.username }, filterquery);
                    //var response = await client.PostAsync(_dbName + "/_find", new StringContent(query, Encoding.UTF8, "application/json"));
                    Console.WriteLine("Query is: " + JsonConvert.SerializeObject(query));

                    var response = await client.PostAsJsonAsync(_dbName + "/_find", query);

                    //Console.WriteLine("Query is: " + query);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsAsync<CloudantAccountResult>();
                        //var responseJson = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Response was: " + responseJson);
                        Console.WriteLine("Response was: " + responseJson.docs.Length);
                        if (responseJson.docs.Length > 0)
                        {
                            AccountResult accts = new AccountResult();
                            accts.status = "success";
                            accts.accounts = responseJson.docs;
                            accts.totalaccounts = responseJson.docs.Length;
                            return JsonConvert.SerializeObject(accts);
                        }
                        else
                        {
                            AccountResult accts = new AccountResult();
                            accts.status = "No Accounts";
                            return JsonConvert.SerializeObject(accts);
                        }
                    }
                    else
                    {
                        string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase + "Request Message:" + response.RequestMessage;
                        Console.WriteLine(msg);
                        return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
                    }
                }
                else
                {
                    AccountResult accts = new AccountResult();
                    accts.status = "Invalid Credentials";
                    return JsonConvert.SerializeObject(accts);
                }

            }
        }

        public async Task<dynamic> GetAccountTypesAsync(Auth user)
        {
            using (var client = CloudantClient())
            {
                var userlogin = await this.LoginAsync(user);
                AuthResult loginresult = JsonConvert.DeserializeObject<AuthResult>(userlogin);
                //var query = "{ \"selector\": { \"username\": \"" + user.username + "\", \"password\": \"" + user.password + "\"}}";

                if (loginresult.status.Equals("success"))
                {
                    this._dbName = "account";
                    String[] filterquery = new String[] { "name", "type", "balance" };

                    CloudantQuery query = new CloudantQuery(new { username = user.username }, filterquery);
                    //var response = await client.PostAsync(_dbName + "/_find", new StringContent(query, Encoding.UTF8, "application/json"));
                    Console.WriteLine("Query is: " + JsonConvert.SerializeObject(query));

                    var response = await client.PostAsJsonAsync(_dbName + "/_find", query);

                    //Console.WriteLine("Query is: " + query);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsAsync<CloudantAccountResult>();
                        //var responseJson = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Response was: " + responseJson);
                        Console.WriteLine("Response was: " + responseJson.docs.Length);
                        if (responseJson.docs.Length > 0)
                        {
                            AccountResult accts = new AccountResult();
                            accts.status = "success";
                            accts.accounts = responseJson.docs;
                            accts.totalaccounts = responseJson.docs.Length;
                            return JsonConvert.SerializeObject(accts);
                        }
                        else
                        {
                            AccountResult accts = new AccountResult();
                            accts.status = "No Accounts";
                            return JsonConvert.SerializeObject(accts);
                        }
                    }
                    else
                    {
                        string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase + "Request Message:" + response.RequestMessage;
                        Console.WriteLine(msg);
                        return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
                    }
                }
                else
                {
                    AccountResult accts = new AccountResult();
                    accts.status = "Invalid Credentials";
                    return JsonConvert.SerializeObject(accts);
                }

            }
        }

        public async Task<dynamic> GetPayeesAsync(Auth user)
        {
            using (var client = CloudantClient())
            {
                var userlogin = await this.LoginAsync(user);
                AuthResult loginresult = JsonConvert.DeserializeObject<AuthResult>(userlogin);
                //var query = "{ \"selector\": { \"username\": \"" + user.username + "\", \"password\": \"" + user.password + "\"}}";

                if (loginresult.status.Equals("success"))
                {
                    this._dbName = "check";
                    String[] filterquery = new String[] { "payee", "name", "username", "amount", "desc", "date" };

                    CloudantQuery query = new CloudantQuery(new { username = user.username }, filterquery);

                    //var response = await client.PostAsync(_dbName + "/_find", new StringContent(query, Encoding.UTF8, "application/json"));
                    Console.WriteLine("Query is: " + JsonConvert.SerializeObject(query));

                    var response = await client.PostAsJsonAsync(_dbName + "/_find", query);

                    //Console.WriteLine("Query is: " + query);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsAsync<CloudantCheckResult>();
                        //var responseJson = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Response was: " + responseJson);
                        Console.WriteLine("Response was: " + responseJson.docs.Length);
                        if (responseJson.docs.Length > 0)
                        {
                            CheckResult checks = new CheckResult();
                            checks.status = "success";
                            checks.checks = responseJson.docs;
                            checks.totalchecks = responseJson.docs.Length;
                            return JsonConvert.SerializeObject(checks);
                        }
                        else
                        {
                            CheckResult checks = new CheckResult();
                            checks.status = "No Checks";
                            return JsonConvert.SerializeObject(checks);
                        }
                    }
                    else
                    {
                        string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase + "Request Message:" + response.RequestMessage;
                        Console.WriteLine(msg);
                        return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
                    }
                }
                else
                {
                    AccountResult accts = new AccountResult();
                    accts.status = "Invalid Credentials";
                    return JsonConvert.SerializeObject(accts);
                }

            }
        }


        public async Task<dynamic> GetChecksAsync(Auth user)
        {
            using (var client = CloudantClient())
            {
                var userlogin = await this.LoginAsync(user);
                AuthResult loginresult = JsonConvert.DeserializeObject<AuthResult>(userlogin);
                //var query = "{ \"selector\": { \"username\": \"" + user.username + "\", \"password\": \"" + user.password + "\"}}";

                if (loginresult.status.Equals("success"))
                {
                    this._dbName = "check";
                    String[] filterquery = new String[] { "payee", "name", "username", "amount", "desc", "date" };
                    CloudantQuery query = new CloudantQuery(new { username = user.username }, filterquery);
                    //var response = await client.PostAsync(_dbName + "/_find", new StringContent(query, Encoding.UTF8, "application/json"));
                    Console.WriteLine("Query is: " + JsonConvert.SerializeObject(query));

                    var response = await client.PostAsJsonAsync(_dbName + "/_find", query);

                    //Console.WriteLine("Query is: " + query);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsAsync<CloudantCheckResult>();
                        //var responseJson = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Response was: " + responseJson);
                        Console.WriteLine("Response was: " + responseJson.docs.Length);
                        if (responseJson.docs.Length > 0)
                        {
                            CheckResult checks = new CheckResult();
                            checks.status = "success";
                            checks.checks = responseJson.docs;
                            checks.totalchecks = responseJson.docs.Length;
                            return JsonConvert.SerializeObject(checks);
                        }
                        else
                        {
                            CheckResult checks = new CheckResult();
                            checks.status = "No Checks";
                            return JsonConvert.SerializeObject(checks);
                        }
                    }
                    else
                    {
                        string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase + "Request Message:" + response.RequestMessage;
                        Console.WriteLine(msg);
                        return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
                    }
                }
                else
                {
                    AccountResult accts = new AccountResult();
                    accts.status = "Invalid Credentials";
                    return JsonConvert.SerializeObject(accts);
                }

            }
        }


        public async Task<dynamic> SaveChecksAsync(SaveCheck savecheck)
        {
            using (var client = CloudantClient())
            {
                Auth user = new Auth();
                user.username = savecheck.username;
                user.password = savecheck.password;

                var userlogin = await this.LoginAsync(user);
                AuthResult loginresult = JsonConvert.DeserializeObject<AuthResult>(userlogin);
                //var query = "{ \"selector\": { \"username\": \"" + user.username + "\", \"password\": \"" + user.password + "\"}}";

                if (loginresult.status.Equals("success"))
                {
                    this._dbName = "check";
                    savecheck.check.username = savecheck.username;
                    var response = await client.PostAsJsonAsync(_dbName, savecheck.check);
                    //var response = await client.PostAsJsonAsync(_dbName + "/_find", query);

                    //Console.WriteLine("Query is: " + query);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsAsync<CloudantCreateResult>();
                        //var responseJson = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Response was: " + responseJson);
                        Console.WriteLine("Response was: " + responseJson.ok);
                        return JsonConvert.SerializeObject(responseJson);
                    }
                    else
                    {
                        string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase + "Request Message:" + response.RequestMessage;
                        Console.WriteLine(msg);
                        return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
                    }
                }
                else
                {
                    AccountResult accts = new AccountResult();
                    accts.status = "Invalid Credentials";
                    return JsonConvert.SerializeObject(accts);
                }

            }
        }


        public async Task<dynamic> CreateAsync(ToDoItem item)
        {
            using (var client = CloudantClient())
            {
                var response = await client.PostAsJsonAsync(_dbName, item);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsAsync<ToDoItem>();
                    return JsonConvert.SerializeObject(new { id = responseJson.id, rev = responseJson.rev });
                }
                string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                Console.WriteLine(msg);
                return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
            }
        }

        public async Task<dynamic> DeleteAsync(ToDoItem item)
        {
            using (var client = CloudantClient())
            {
                var response = await client.DeleteAsync(_dbName + "/" + item.id + "?rev=" + item.rev);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsAsync<ToDoItem>();
                    return JsonConvert.SerializeObject(new { id = responseJson.id, rev = responseJson.rev });
                }
                string msg = "Failure to DELETE. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                Console.WriteLine(msg);
                return JsonConvert.SerializeObject(new { msg = msg });
            }
        }


        public async Task<string> UpdateAsync(ToDoItem item)
        {
            using (var client = CloudantClient())
            {
                var response = await client.PutAsJsonAsync(_dbName + "/" + item.id + "?rev=" + item.rev, item);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsAsync<ToDoItem>();
                    return JsonConvert.SerializeObject(new { id = responseJson.id, rev = responseJson.rev });
                }
                string msg = "Failure to PUT. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                Console.WriteLine(msg);
                return JsonConvert.SerializeObject(new { msg = msg });
            }
        }

        public async Task PopulateTestData()
        {
            using (var client = CloudantClient())
            {
                // create and populate DB if it doesn't exist
                var response = await client.GetAsync(_dbName);
                if (!response.IsSuccessStatusCode)
                {
                    response = await client.PutAsync(_dbName, null);
                    if (response.IsSuccessStatusCode)
                    {
                        Task t1 = CreateAsync(JsonConvert.DeserializeObject<ToDoItem>("{ 'text': 'Sample 1' }"));
                        Task t2 = CreateAsync(JsonConvert.DeserializeObject<ToDoItem>("{ 'text': 'Sample 2' }"));
                        Task t3 = CreateAsync(JsonConvert.DeserializeObject<ToDoItem>("{ 'text': 'Sample 3' }"));
                        await Task.WhenAll(t1, t2, t3);
                    }
                    else
                    {
                        throw new Exception("Failed to create database " + _dbName + ". Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase);
                    }
                }
            }
        }

        private HttpClient CloudantClient()
        {
            if (_cloudantCreds.username == null || _cloudantCreds.password == null || _cloudantCreds.host == null)
            {
                throw new Exception("Missing Cloudant NoSQL DB service credentials");
            }

            Console.WriteLine("The username is: " + _cloudantCreds.username + " and password is: " + _cloudantCreds.password);
            var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(_cloudantCreds.username + ":" + _cloudantCreds.password));

            HttpClient client = HttpClientFactory.Create(new LoggingHandler());
            client.BaseAddress = new Uri("https://" + _cloudantCreds.host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            return client;
        }
    }

    class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            Console.WriteLine("{0}\t{1}", request.Method, request.RequestUri);
            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine(response.StatusCode);
            return response;
        }
    }
}
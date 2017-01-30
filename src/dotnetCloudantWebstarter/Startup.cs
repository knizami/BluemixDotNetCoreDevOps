using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using CloudantDotNet.Services;
using System.Threading.Tasks;
using System;
using System.IO;

public class Startup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IHostingEnvironment env)
    {

        var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./config.json", optional: false);
        Configuration = configBuilder.Build();
        string vcapservicesEnv = System.Environment.GetEnvironmentVariable("VCAP_SERVICES");



        if (vcapservicesEnv != null)
        {
            Console.WriteLine("Read VCAP...");
            dynamic json = JsonConvert.DeserializeObject(vcapservicesEnv);
            foreach (dynamic obj in json.Children())
            {
                if (((string)obj.Name).ToLowerInvariant().Contains("cloudant"))
                {
                    dynamic credentials = (((JProperty)obj).Value[0] as dynamic).credentials;
                    if (credentials != null)
                    {
                        Console.WriteLine("Setting credentials...");
                        string host = credentials.host;
                        string username = credentials.username;
                        string password = credentials.password;
                        Configuration["VCAP_SERVICES:cloudantNoSQLDB:0:credentials:username"] = username;
                        Configuration["VCAP_SERVICES:cloudantNoSQLDB:0:credentials:password"] = password;
                        Configuration["VCAP_SERVICES:cloudantNoSQLDB:0:credentials:host"] = host;
                        break;
                    }
                }
            }
        }
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        // works with VCAP_SERVICES JSON value added to config.json when running locally,
        // and works with actual VCAP_SERVICES env var based on configuration set above when running in CF
        var creds = new CloudantDotNet.Models.Creds()
        {
            username = Configuration["VCAP_SERVICES:cloudantNoSQLDB:0:credentials:username"],
            password = Configuration["VCAP_SERVICES:cloudantNoSQLDB:0:credentials:password"],
            host = Configuration["VCAP_SERVICES:cloudantNoSQLDB:0:credentials:host"]
        };
        services.AddSingleton(typeof(CloudantDotNet.Models.Creds), creds);
        services.AddTransient<ICloudantService, CloudantService>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        // Populate test data in the database
        var cloudantService = ((ICloudantService)app.ApplicationServices.GetService(typeof(ICloudantService)));
        //cloudantService.PopulateTestData().Wait();

        loggerFactory.AddConsole();
        app.UseDeveloperExceptionPage();
        app.UseStaticFiles();
        app.UseMvcWithDefaultRoute();
    }

    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

        var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseConfiguration(config)
                    .UseStartup<Startup>()
                    .Build();

        host.Run();
    }
}

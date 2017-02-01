# ASP.NET Core Demo REST service

This application demonstrates how to implement a REST service using ASP.NET Core and deploy it to IBM Bluemix.  The application uses a cloudant NoSQL backend, in our demos we used the cloudant database service hosted on Bluemix.

## Run the app locally

1. Install ASP.NET Core and the Dotnet CLI by following the [Getting Started][] instructions
+ cd into this project's root directory, then `src/dotnetCloudantWebstarter`
+ Copy the value for the VCAP_SERVICES envirionment variable from the application running in Bluemix and paste it in the config.json file
+ Run `dotnet restore`
+ Run `dotnet run`
+ Access the running app in a browser at <http://localhost:5000>

[Getting Started]: http://docs.asp.net/en/latest/getting-started/index.html

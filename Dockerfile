FROM microsoft/dotnet:1.0-sdk-projectjson
WORKDIR /dotnetapp
COPY src/DotNetCoreDevOps/project.json .
RUN dotnet restore --infer-runtimes
COPY src/DotNetCoreDevOps .
RUN dotnet test
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/dotnetapp.dll"]
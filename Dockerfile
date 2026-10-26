FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Restore as distinct layers
COPY *.sln ./
COPY AdventureWorks2019/*.csproj AdventureWorks2019/
COPY AdventureWorks2019.DataAccess/*.csproj AdventureWorks2019.DataAccess/
COPY AdventureWorks2019.Domain/*.csproj AdventureWorks2019.Domain/
COPY AdventureWorks2019.Services/*.csproj AdventureWorks2019.Services/
RUN dotnet restore

# Build and publish a release
COPY . ./
RUN dotnet publish AdventureWorks2019/AdventureWorks2019.API.csproj -c Release -o out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AdventureWorks2019.API.dll"]

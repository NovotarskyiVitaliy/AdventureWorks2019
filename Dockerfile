FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

ENV DefaultConnection=Server=sql,1433;Database=AdventureWorks2019;User=sa;Password=My_StrongPassword62

EXPOSE 8080

ENTRYPOINT ["dotnet", "AdventureWorks2019.API.dll"]


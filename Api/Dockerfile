# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# WORKDIR /usr

# # Copy everything
# COPY . ./
# # Restore as distinct layers
# RUN dotnet restore
# # Build
# RUN dotnet build ./src/PWPA.CallLogging.BackEnd.API -c Release -o out --no-restore
# # Unit Tests
# RUN dotnet test --no-restore --filter FullyQualifiedName~UnitTests
# # Build and publish a release
# RUN dotnet publish ./src/PWPA.CallLogging.BackEnd.API -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /usr/app
# COPY --from=build-env /usr/out .
COPY ./src/PWPA.CallLogging.Backend.API/bin/Release/net8.0/publish .
ENTRYPOINT ["dotnet", "PWPA.CallLogging.BackEnd.API.dll"]
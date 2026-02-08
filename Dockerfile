# See the article at https://aka.ms/customizecontainer to learn how to customize your debug container
# and how Visual Studio uses this Dockerfile to build images for faster debugging.

# Depending on the operating system of the host machines that will build or run the containers,
# the image specified in the FROM instruction may need to be changed.
# For more information, see https://aka.ms/containercompat

# This stage is used when running from Visual Studio in fast mode
# (the default for the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DataImportProj.csproj", "."]
RUN dotnet restore "./DataImportProj.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./DataImportProj.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project, which will be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DataImportProj.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in the production environment or when running from Visual Studio in normal mode
# (the default when the Debug configuration is not used)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataImportProj.dll"]
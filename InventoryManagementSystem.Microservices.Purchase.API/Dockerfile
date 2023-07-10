#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8008
EXPOSE 8008

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InventoryManagementSystem.Microservices.Purchase.API/InventoryManagementSystem.Microservices.Purchase.API.csproj", "InventoryManagementSystem.Microservices.Purchase.API/"]
COPY ["InventoryManagementSystem.Infrastructure.loC/InventoryManagementSystem.Infrastructure.loC.csproj", "InventoryManagementSystem.Infrastructure.loC/"]
COPY ["InventoryManagementSystem.Domain.Core/InventoryManagementSystem.Domain.Core.csproj", "InventoryManagementSystem.Domain.Core/"]
COPY ["InventoryManagementSystem.Infrastructure.Bus/InventoryManagementSystem.Infrastructure.Bus.csproj", "InventoryManagementSystem.Infrastructure.Bus/"]
COPY ["InventoryManagementSystem.Infrastructure.Database/InventoryManagementSystem.Infrastructure.Database.csproj", "InventoryManagementSystem.Infrastructure.Database/"]
COPY ["InventoryManagementSystem.Microservices.Purchase.Application/InventoryManagementSystem.Microservices.Purchase.Application.csproj", "InventoryManagementSystem.Microservices.Purchase.Application/"]
COPY ["InventoryManagementSystem.Microservices.Purchase.Domain/InventoryManagementSystem.Microservices.Purchase.Domain.csproj", "InventoryManagementSystem.Microservices.Purchase.Domain/"]
COPY ["InventoryManagementSystem.Microservices.Purchase.Infrastructure/InventoryManagementSystem.Microservices.Purchase.Infrastructure.csproj", "InventoryManagementSystem.Microservices.Purchase.Infrastructure/"]
RUN dotnet restore "InventoryManagementSystem.Microservices.Purchase.API/InventoryManagementSystem.Microservices.Purchase.API.csproj"
COPY . .
WORKDIR "/src/InventoryManagementSystem.Microservices.Purchase.API"
RUN dotnet build "InventoryManagementSystem.Microservices.Purchase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryManagementSystem.Microservices.Purchase.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryManagementSystem.Microservices.Purchase.API.dll"]
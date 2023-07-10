#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8007
EXPOSE 8007

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InventoryManagementSystem.Microservices.Inventory/InventoryManagementSystem.Microservices.Inventory.API.csproj", "InventoryManagementSystem.Microservices.Inventory/"]
COPY ["InventoryManagementSystem.Infrastructure.loC/InventoryManagementSystem.Infrastructure.loC.csproj", "InventoryManagementSystem.Infrastructure.loC/"]
COPY ["InventoryManagementSystem.Domain.Core/InventoryManagementSystem.Domain.Core.csproj", "InventoryManagementSystem.Domain.Core/"]
COPY ["InventoryManagementSystem.Infrastructure.Bus/InventoryManagementSystem.Infrastructure.Bus.csproj", "InventoryManagementSystem.Infrastructure.Bus/"]
COPY ["InventoryManagementSystem.Infrastructure.Database/InventoryManagementSystem.Infrastructure.Database.csproj", "InventoryManagementSystem.Infrastructure.Database/"]
COPY ["InventoryManagementSystem.Microservices.Inventory.Application/InventoryManagementSystem.Microservices.Inventory.Application.csproj", "InventoryManagementSystem.Microservices.Inventory.Application/"]
COPY ["InventoryManagementSystem.Microservices.Inventory.Domain/InventoryManagementSystem.Microservices.Inventory.Domain.csproj", "InventoryManagementSystem.Microservices.Inventory.Domain/"]
COPY ["InventoryManagementSystem.Microservices.Inventory.Infrastructure/InventoryManagementSystem.Microservices.Inventory.Infrastructure.csproj", "InventoryManagementSystem.Microservices.Inventory.Infrastructure/"]
RUN dotnet restore "InventoryManagementSystem.Microservices.Inventory/InventoryManagementSystem.Microservices.Inventory.API.csproj"
COPY . .
WORKDIR "/src/InventoryManagementSystem.Microservices.Inventory"
RUN dotnet build "InventoryManagementSystem.Microservices.Inventory.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryManagementSystem.Microservices.Inventory.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryManagementSystem.Microservices.Inventory.API.dll"]
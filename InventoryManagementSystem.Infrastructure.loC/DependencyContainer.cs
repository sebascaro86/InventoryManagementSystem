﻿using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Infrastructure.Bus;
using InventoryManagementSystem.Infrastructure.Database;
using InventoryManagementSystem.Infrastructure.Database.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace InventoryManagementSystem.Infrastructure.loC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.RegisterDatabaseServices();

            services.Configure<RabbitMQSettings>(options =>
            {
                options.HostName = Environment.GetEnvironmentVariable("HOSTNAME");
                options.Username = Environment.GetEnvironmentVariable("RBMQ_USERNAME");
                options.Password = Environment.GetEnvironmentVariable("RBMQ_PASSWORD");
            });

            services.AddTransient<InventoryDBContext>();
            services.AddSingleton<IEventBus, RabbitMQBus>(sp => 
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var optionsFactory = sp.GetRequiredService<IOptions<RabbitMQSettings>>();

                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory, optionsFactory);
            });

            return services;
        }
    }
}

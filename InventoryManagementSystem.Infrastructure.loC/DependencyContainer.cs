using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Infrastructure.Bus;
using InventoryManagementSystem.Infrastructure.Database;
using InventoryManagementSystem.Infrastructure.Database.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace InventoryManagementSystem.Infrastructure.loC
{
    /// <summary>
    /// Represents a static class for registering services in the dependency container.
    /// </summary>
    public static class DependencyContainer
    {
        /// <summary>
        /// Registers the services in the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the services in.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            Console.WriteLine("Llegamos");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

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

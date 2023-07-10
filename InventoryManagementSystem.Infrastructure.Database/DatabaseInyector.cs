using InventoryManagementSystem.Infrastructure.Database.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Infrastructure.Database
{
    /// <summary>
    /// Represents a static class for registering database services.
    /// </summary>
    public static class DatabaseInyector
    {
        /// <summary>
        /// Registers the database services in the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the services in.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RegisterDatabaseServices(this IServiceCollection services)
        {
            services.AddDbContext<InventoryDBContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("DB_ConnectionString"));
            });

            return services;
        }
    }
}

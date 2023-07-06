using InventoryManagementSystem.Infrastructure.Database.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Infrastructure.Database
{
    public static class DatabaseInyector
    {
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

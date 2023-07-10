using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing clients.
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private readonly InventoryDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ClientRepository(InventoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves all the clients.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of clients.</returns>
        public async Task<ICollection<Client>> GetClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Client> GetClient(Guid clientId)
        {
            var customer  = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

            if (customer == null)
            {
                throw new NotFoundException($"The customer with the ID {clientId} does not exist.");
            }

            return customer;
        }

        /// <inheritdoc />
        public async Task<Client> CreateClient(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }
    }
}

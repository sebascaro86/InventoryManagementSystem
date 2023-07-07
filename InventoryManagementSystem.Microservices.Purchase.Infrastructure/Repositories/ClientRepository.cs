using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly InventoryDBContext _dbContext;

        public ClientRepository(InventoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Client>> GetClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetClient(Guid clientId)
        {
            var customer  = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

            if (customer == null)
            {
                throw new NotFoundException($"The customer with the ID {clientId} does not exist.");
            }

            return customer;
        }

        public async Task<Client> CreateClient(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }
    }
}

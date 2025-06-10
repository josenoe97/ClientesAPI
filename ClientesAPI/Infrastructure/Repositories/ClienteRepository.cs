using ClientesAPI.Domain.Entities;
using ClientesAPI.Domain.Interface;
using ClientesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPI.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteContext _context;

        public ClienteRepository(ClienteContext context)
        {
            _context = context;
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> DeleteAsync(Guid id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync(); 
            }

            return cliente;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            return cliente;
        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
            return await GetByIdAsync(cliente.Id);
        }
    }
}

using ClientesAPI.Domain.Entities;
using ClientesAPI.Domain.Interface;
using ClientesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPI.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteContext _context;
        private readonly ILogger<ClienteRepository> _logger;

        public ClienteRepository(ClienteContext context, ILogger<ClienteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            _logger.LogInformation("Acessando o método AddAsync no repositório");

            try
            {
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();

                return cliente;
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao adicionar cliente no repositório.");
                throw;
            }
        }

        public async Task<Cliente> DeleteAsync(Cliente cliente)
        {
            _logger.LogInformation("Acessando o método DeleteAsync no repositório");

            try
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();

                return cliente;
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao deletar cliente no repositório.");
                throw;
            }
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            _logger.LogInformation("Acessando o método GetAllAsync no repositório");

            try
            {
                return await _context.Clientes.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Acessando o método GetByIdAsync no repositório");

            try
            {
                return await _context.Clientes.FindAsync(id);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao obter cliente por ID {Id} no repositório.", id);
                throw;
            }
        }

        public async Task<Cliente?> UpdateAsync(Cliente cliente)
        {
            _logger.LogInformation("Acessando o método UpdateAsync no repositório");

            try
            {
                var existCliente = await GetByIdAsync(cliente.Id);
                if (existCliente != null)
                {
                    _context.Entry(existCliente).CurrentValues.SetValues(cliente);
                    await _context.SaveChangesAsync();
                }

                return await GetByIdAsync(cliente.Id);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao atualizar cliente no repositório.");
                throw;
            }
        }
    }
}

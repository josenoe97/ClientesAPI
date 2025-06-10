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

        public Cliente Add(Cliente cliente)
        {
            _logger.LogInformation("Acessando o método Add no repositório");
            
            try
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();

                return cliente;
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao obter clientes no repositório.");
                throw;
            }
        }

        public Cliente Delete(Cliente cliente)
        {
            _logger.LogInformation("Acessando o método Delete no repositório");

            try
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();

                return cliente;
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao deletar cliente no repositório.");
                throw;
            }
        }

        public List<Cliente> GetAll()
        {
            _logger.LogInformation("Acessando o método GetAll no repositório");

            try
            {
                return _context.Clientes.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cliente GetById(Guid id)
        {
            _logger.LogInformation("Acessando o método GetById no repositório");

            try
            {
                var cliente = _context.Clientes.Find(id);

                return cliente;
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao obter cliente por ID {Id} no repositório.", id);
                throw;
            }
        }

        public Cliente Update(Cliente cliente)
        {
            _logger.LogInformation("Acessando o método Update no repositório");

            try
            {
                var existCliente = GetById(cliente.Id);
                if (existCliente != null)
                {
                    _context.Entry(existCliente).CurrentValues.SetValues(cliente);
                    _context.SaveChanges();
                }

                return GetById(cliente.Id);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao atualizar cliente no repositório.");
                throw;
            }
        }
    }
}

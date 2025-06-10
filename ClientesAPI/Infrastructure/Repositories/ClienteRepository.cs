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

        public Cliente Add(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public Cliente Delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public List<Cliente> GetAll()
        {
            return _context.Clientes.ToList();
        }

        public Cliente GetById(Guid id)
        {
            var cliente = _context.Clientes.FirstOrDefault(x => x.Id == id);

            return cliente;
        }

        public Cliente Update(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
            return GetById(cliente.Id);
        }
    }
}

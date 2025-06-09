using ClientesAPI.Domain.Entities;
using ClientesAPI.Domain.Interface;

namespace ClientesAPI.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public ClienteRepository()
        {
            //dbcontext
        }

        public async Task<Cliente> Add(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cliente>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> Update(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}

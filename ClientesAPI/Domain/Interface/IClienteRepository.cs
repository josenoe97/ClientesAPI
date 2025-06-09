using ClientesAPI.Domain.Entities;

namespace ClientesAPI.Domain.Interface
{
    public interface IClienteRepository
    {
        public Task<Cliente> GetById(Guid id);
        public Task<List<Cliente>> GetAll();
        public Task<Cliente> Add(Cliente cliente);
        public Task<Cliente> Update(Cliente cliente);
        public Task<Cliente> Delete(Guid id);
    }
}

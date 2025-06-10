using ClientesAPI.Domain.Entities;

namespace ClientesAPI.Domain.Interface
{
    public interface IClienteRepository
    {
        public Task<Cliente> GetByIdAsync(Guid id);
        public Task<List<Cliente>> GetAllAsync();
        public Task<Cliente> AddAsync(Cliente cliente);
        public Task<Cliente> UpdateAsync(Cliente cliente);
        public Task<Cliente> DeleteAsync(Cliente cliente);
    }
}

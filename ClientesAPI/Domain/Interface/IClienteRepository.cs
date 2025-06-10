using ClientesAPI.Domain.Entities;

namespace ClientesAPI.Domain.Interface
{
    public interface IClienteRepository
    {
        public Cliente GetById(Guid id);
        public List<Cliente> GetAll();
        public Cliente Add(Cliente cliente);
        public Cliente Update(Cliente cliente);
        public Cliente Delete(Cliente cliente);
    }
}

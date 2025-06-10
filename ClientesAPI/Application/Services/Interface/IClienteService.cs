using ClientesAPI.Application.DTOs;
using ClientesAPI.Domain.Entities;

namespace ClientesAPI.Application.Services.Interface
{
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> GetAll();
        ClienteDTO GetById(Guid id);
        ClienteDTO Create(ClienteDTO dto);
        ClienteDTO Update(Guid id, ClienteDTO dto);
        ClienteDTO Delete(Guid id);
    }
}

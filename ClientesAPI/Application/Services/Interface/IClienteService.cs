using ClientesAPI.Application.DTOs;
using ClientesAPI.Domain.Entities;

namespace ClientesAPI.Application.Services.Interface
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> GetAllAsync();
        Task<ClienteDTO> GetByIdAsync(Guid id);
        Task<ClienteDTO> CreateAsync(ClienteDTO dto);
        Task<ClienteDTO> UpdateAsync(Guid id, ClienteDTO dto);
        Task<ClienteDTO> DeleteAsync(Guid id);
    }
}

using AutoMapper;
using ClientesAPI.Application.DTOs;
using ClientesAPI.Application.Services.Interface;
using ClientesAPI.Domain.Entities;
using ClientesAPI.Domain.Interface;

namespace ClientesAPI.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, ILogger<ClienteService> logger)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ClienteDTO> CreateAsync(ClienteDTO dto)
        {
            _logger.LogInformation("Acessando o método CreateAsync no ClienteService.");

            try
            {
                var cliente = _mapper.Map<Cliente>(dto);
                await _clienteRepository.AddAsync(cliente);

                return await GetByIdAsync(cliente.Id);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao criar cliente no ClienteService.");
                throw;
            }
        }

        public async Task<ClienteDTO> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Acessando o método DeleteAsync no ClienteService.");

            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                if (cliente == null) throw new Exception("Cliente não encontrado.");

                await _clienteRepository.DeleteAsync(cliente);

                return _mapper.Map<ClienteDTO>(cliente);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao deletar cliente no ClienteService.");
                throw;
            }
        }

        public async Task<List<ClienteDTO>> GetAllAsync()
        {
            _logger.LogInformation("Acessando o método GetAllAsync no ClienteService.");

            try
            {
                var clientes = await _clienteRepository.GetAllAsync();
                return clientes.Select(c => _mapper.Map<ClienteDTO>(c)).ToList();
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao obter todos clientes no ClienteService.");
                throw;
            }
        }

        public async Task<ClienteDTO> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Acessando o método GetByIdAsync no ClienteService.");

            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                return _mapper.Map<ClienteDTO>(cliente);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao obter cliente por ID {Id} no ClienteService.", id);
                throw;
            }
        }

        public async Task<ClienteDTO> UpdateAsync(Guid id, ClienteDTO dto)
        {
            _logger.LogInformation("Acessando o método UpdateAsync no ClienteService.");

            try
            {
                var cliente = _mapper.Map<Cliente>(dto);
                await _clienteRepository.UpdateAsync(cliente);

                return await GetByIdAsync(cliente.Id);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao atualizar cliente no ClienteService.");
                throw;
            }
        }
    }
}

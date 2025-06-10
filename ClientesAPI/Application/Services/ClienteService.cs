using AutoMapper;
using ClientesAPI.Application.DTOs;
using ClientesAPI.Application.Services.Interface;
using ClientesAPI.Domain.Entities;
using ClientesAPI.Domain.Interface;
using ClientesAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public ClienteDTO Create(ClienteDTO dto)
        {
            _logger.LogInformation("Acessando o método Create no ClienteService.");

            try
            {
                var cliente = _mapper.Map<Cliente>(dto);

                _clienteRepository.Add(cliente);

                return GetById(cliente.Id);
            }
            catch (Exception) 
            {
                _logger.LogError("Falha ao criar cliente no ClienteService.");
                throw;
            }
        }

        public ClienteDTO Delete(Guid id)
        {
            _logger.LogInformation("Acessando o método Create no ClienteService.");

            try
            {
                var cliente = _clienteRepository.GetById(id);

                _clienteRepository.Delete(cliente);

                return _mapper.Map<ClienteDTO>(cliente);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao deletar cliente no ClienteService.");
                throw;
            }
        }

        public List<ClienteDTO> GetAll()
        {
            _logger.LogInformation("Acessando o método GetAll no ClienteService.");

            try
            {
                var clientesDTO = _clienteRepository.GetAll().Select(x => _mapper.Map<ClienteDTO>(x));

                return clientesDTO.ToList();
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao obter todos clientes no ClienteService.");
                throw;
            }
        }

        public ClienteDTO GetById(Guid id)
        {
            _logger.LogInformation("Acessando o método GetById no ClienteService.");

            try
            {
                var cliente = _clienteRepository.GetById(id);

                return _mapper.Map<ClienteDTO>(cliente);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao obter cliente por ID {Id} no ClienteService.", id);
                throw;
            }
        }


        public ClienteDTO Update(Guid id, ClienteDTO dto)
        {
            _logger.LogInformation("Acessando o método Update no ClienteService.");

            try
            {
                var cliente = _mapper.Map<Cliente>(dto);

                _clienteRepository.Update(cliente);

                return GetById(cliente.Id);
            }
            catch (Exception)
            {
                _logger.LogError("Falha ao atualizar cliente no ClienteService.");
                throw;
            }
        }
    }
}

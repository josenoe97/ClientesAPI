using AutoMapper;
using ClientesAPI.Application.DTOs;
using ClientesAPI.Application.Services.Interface;
using ClientesAPI.Domain.Entities;
using ClientesAPI.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClientesAPI.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public ClienteDTO Create(ClienteDTO dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);

            _clienteRepository.Add(cliente);

            return GetById(cliente.Id);
        }

        public ClienteDTO Delete(Guid id)
        {
            var cliente = _clienteRepository.GetById(id);

            _clienteRepository.Delete(cliente);

            return _mapper.Map<ClienteDTO>(cliente);
        }

        public IEnumerable<ClienteDTO> GetAll()
        {
            var clientesDTO = _clienteRepository.GetAll().Select(x => _mapper.Map<ClienteDTO>(x));

            return clientesDTO;
        }

        public ClienteDTO GetById(Guid id)
        {
            var cliente = _clienteRepository.GetById(id);

            return _mapper.Map<ClienteDTO>(cliente);
        }


        public ClienteDTO Update(Guid id, ClienteDTO dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);

            _clienteRepository.Update(cliente);

            return GetById(cliente.Id);
        }
    }
}

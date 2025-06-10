using AutoMapper;
using ClientesAPI.Application.DTOs;
using ClientesAPI.Domain.Entities;

namespace ClientesAPI.Application.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EnderecoDTO, Endereco>().ReverseMap();
            CreateMap<ClienteDTO, Cliente>().ReverseMap();
        }
    }
}

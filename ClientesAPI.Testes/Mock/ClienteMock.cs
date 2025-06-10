using ClientesAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesAPI.Testes.Mock
{
    public class ClienteMock
    {
        public static List<Application.DTOs.ClienteDTO> GetMockCliente()
        {
            var mockList = new List<Application.DTOs.ClienteDTO>
{
                new Application.DTOs.ClienteDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = "João Mockado",
                    Email = "mock@example.com",
                    Telefone = "999-9999",
                    Endereco = new EnderecoDTO
                    {
                        CEP = "21344-050",
                        Cidade = "Rio de Janeiro",
                        Estado = "Rio de Janeiro",
                        Numero = "381",
                        Rua = "Rua Alberto Freire"
                    }
                },
                new ClienteDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = "Carlos Simulado",
                    Email = "carlos.simulado@example.com",
                    Telefone = "777-7777",
                    Endereco = new EnderecoDTO
                    {
                        CEP = "40020-230",
                        Cidade = "Salvador",
                        Estado = "Bahia",
                        Numero = "45",
                        Rua = "Rua das Palmeiras"
                    }
                },
                new ClienteDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = "Maria Teste",
                    Email = "maria.teste@example.com",
                    Telefone = "888-8888",
                    Endereco = new EnderecoDTO
                    {
                        CEP = "11030-500",
                        Cidade = "São Paulo",
                        Estado = "São Paulo",
                        Numero = "120",
                        Rua = "Avenida Paulista"
                    }
                }
            };



            return mockList;
        }
    }
}

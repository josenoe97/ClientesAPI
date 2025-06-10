using ClientesAPI.Application.DTOs;
using ClientesAPI.Application.Services.Interface;
using ClientesAPI.Testes.Mock;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace ClientesAPI.Testes.Testes
{
    public class ClienteControllerV1Teste // teste de versão controle v1
    {
        private readonly API.Controllers.v1.ClienteController _controller;
        private readonly Mock<IClienteService> _clienteServiceMock = new(); // Moq - simular processos do service
        private readonly Mock<ILogger<API.Controllers.v1.ClienteController>> _loggerMock = new(); // Moq

        public ClienteControllerV1Teste()
        {
            _controller = new API.Controllers.v1.ClienteController(_clienteServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetClientesDeveRetornarOkQuandoExistemClientes() //GET
        {
            // Arrange
            var mockClientes = ClienteMock.GetMockCliente();
            _clienteServiceMock.Setup(y => y.GetAll()).Returns(mockClientes);

            // Act
            var result = _controller.GetClientes();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200); // verifica se o statuscode retorno é um 200 OK // FluentAssertion

            var clientes = okResult.Value as IEnumerable<ClienteDTO>;
            clientes.Should().HaveCount(3); // => vai retorna apenas 3, por que é a quantidade de registros que tem no mock
        }

        [Fact]
        public void GetClienteDeveRetornarClienteQuandoExistir() // GET({ID})
        {
            // Arrange
            var mockCliente = ClienteMock.GetMockCliente().First();
            _clienteServiceMock.Setup(x => x.GetById(mockCliente.Id)).Returns(mockCliente);

            // Act
            var resultado = _controller.GetCliente(mockCliente.Id);

            // Assert
            var okResult = resultado.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);

            var clienteRetornado = okResult.Value as ClienteDTO;
            clienteRetornado.Should().NotBeNull();
            clienteRetornado!.Id.Should().Be(mockCliente.Id); // ou Assert.Equal(mockCliente.Id, clienteRetornado.Id);
            clienteRetornado.Nome.Should().Be(mockCliente.Nome); // ou Assert.Equal(mockCliente.Nome, clienteRetornado.Nome);
        }

        [Fact]
        public void CreateClienteDeveRetornarCreatedQuandoValido() //POST
        {
            // Arrange
            var newCliente = ClienteMock.GetMockCliente().First();
            _clienteServiceMock.Setup(x => x.Create(It.IsAny<ClienteDTO>()));

            // Act
            var result = _controller.CreateCliente(newCliente); // criando um cliente equivale ao novoCliente

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(201);
            createdResult.Value.Should().BeEquivalentTo(newCliente);
        }

        [Fact]
        public void UpdateClienteDeveRetornarNoContentQuandoClienteExiste() //PUT
        {
            // Arrange
            var clienteExistente = ClienteMock.GetMockCliente().First();
            var id = clienteExistente.Id;

            _clienteServiceMock.Setup(x => x.GetById(id)).Returns(clienteExistente);
            _clienteServiceMock.Setup(x => x.Update(id, clienteExistente));

            // Act
            var resultado = _controller.UpdateCliente(id, clienteExistente);

            // Assert
            var noContentResult = resultado as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult!.StatusCode.Should().Be(204);
        }

        [Fact]
        public void DeleteClienteDeveRetornarNoContentQuandoClienteExiste() //DELETE
        {
            // Arrange
            var clienteExist = ClienteMock.GetMockCliente().First();
            var id = clienteExist.Id; // id que vai ser deletado

            _clienteServiceMock.Setup(s => s.GetById(id)).Returns(clienteExist);
            _clienteServiceMock.Setup(s => s.Delete(id));

            // Act
            var resultado = _controller.DeleteCliente(id);

            // Assert
            var noContentResult = resultado as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult!.StatusCode.Should().Be(204);
        }


    }
}
using ClientesAPI.Application.DTOs;
using ClientesAPI.Application.Services.Interface;
using ClientesAPI.Testes.Mock;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ClientesAPI.Testes.Testes
{
    public class ClienteControllerV2Teste
    {
        private readonly API.Controllers.v2.ClienteController _controller;
        private readonly Mock<IClienteService> _clienteServiceMock = new();
        private readonly Mock<ILogger<API.Controllers.v2.ClienteController>> _loggerMock = new();

        public ClienteControllerV2Teste()
        {
            _controller = new API.Controllers.v2.ClienteController(_clienteServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetClientesDeveRetornarOkQuandoExistemClientes()
        {
            // Arrange
            var mockClientes = ClienteMock.GetMockCliente();
            _clienteServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(mockClientes);

            // Act
            var result = await _controller.GetClientes();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);

            var clientes = okResult.Value as IEnumerable<ClienteDTO>;
            clientes.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetClienteDeveRetornarClienteQuandoExistir()
        {
            // Arrange
            var mockCliente = ClienteMock.GetMockCliente().First();
            _clienteServiceMock.Setup(x => x.GetByIdAsync(mockCliente.Id)).ReturnsAsync(mockCliente);

            // Act
            var resultado = await _controller.GetCliente(mockCliente.Id);

            // Assert
            var okResult = resultado.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);

            var clienteRetornado = okResult.Value as ClienteDTO;
            clienteRetornado.Should().NotBeNull();
            clienteRetornado!.Id.Should().Be(mockCliente.Id);
            clienteRetornado.Nome.Should().Be(mockCliente.Nome);
        }

        [Fact]
        public async Task CreateClienteDeveRetornarCreatedQuandoValido()
        {
            // Arrange
            var newCliente = ClienteMock.GetMockCliente().First();
            _clienteServiceMock.Setup(x => x.CreateAsync(It.IsAny<ClienteDTO>())).ReturnsAsync(newCliente);

            // Act
            var result = await _controller.CreateCliente(newCliente);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(201);
            createdResult.Value.Should().BeEquivalentTo(newCliente);
        }

        [Fact]
        public async Task UpdateClienteDeveRetornarNoContentQuandoClienteExiste()
        {
            // Arrange
            var clienteExistente = ClienteMock.GetMockCliente().First();
            var id = clienteExistente.Id;

            _clienteServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(clienteExistente);
            _clienteServiceMock.Setup(x => x.UpdateAsync(id, clienteExistente));//.Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.UpdateCliente(id, clienteExistente);

            // Assert
            var noContentResult = resultado as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult!.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteClienteDeveRetornarNoContentQuandoClienteExiste()
        {
            // Arrange
            var clienteExist = ClienteMock.GetMockCliente().First();
            var id = clienteExist.Id;

            _clienteServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(clienteExist);
            _clienteServiceMock.Setup(s => s.DeleteAsync(id));//.Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.DeleteCliente(id);

            // Assert
            var noContentResult = resultado as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult!.StatusCode.Should().Be(204);
        }
    }
}

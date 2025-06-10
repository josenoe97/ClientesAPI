using Asp.Versioning;
using ClientesAPI.Application.DTOs;
using ClientesAPI.Application.Services.Interface;
using ClientesAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClientesAPI.API.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        /// <response code ="200">Retorna os clientes cadastrados</response>
        /// <response code ="204">Nenhum cliente encotrado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ClienteDTO>> GetClientes()
        {
            _logger.LogInformation("Recebida requisição GET para listar todos os clientes.");

            try
            {
                var clientes = _clienteService.GetAll();
                if (!clientes.Any()) return NoContent();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de clientes.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna um cliente específico pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Cliente encontrado.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     GET /api/v2/cliente/12345678-1234-1234-1234-123456789012
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///        "id": "12345678-1234-1234-1234-123456789012",
        ///        "nome": "João Mockado",
        ///        "email": "joao.mockado@example.com",
        ///        "telefone": "999-9999",
        ///        "endereco": {
        ///           "cep": "21344-050",
        ///           "cidade": "Rio de Janeiro",
        ///           "estado": "RJ",
        ///           "numero": "381",
        ///           "rua": "Rua Alberto Freire"
        ///        }
        ///     }
        /// </remarks>
        /// <response code ="200">Retorna o cliente encontrado</response>
        /// <response code ="204">Cliente não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ClienteDTO> GetCliente(Guid id)
        {
            _logger.LogInformation("Recebida requisição GET para cliente com ID {Id}.", id);

            try
            {
                if (id == Guid.Empty) return BadRequest("ID inválido.");

                var clienteDto = _clienteService.GetById(id);
                if (clienteDto == null) return NotFound("Cliente não encontrado.");

                return Ok(clienteDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a cliente por ID {Id}.", id);
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="clienteDto">Objeto cliente.</param>
        /// <returns>Cliente criado.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST
        ///     {
        ///        "id": "12345678-1234-1234-1234-123456789012",
        ///        "nome": "João Atualizado",
        ///        "email": "joao.atualizado@example.com",
        ///        "telefone": "999-1111",
        ///        "endereco": {
        ///           "cep": "21344-050",
        ///           "cidade": "Rio de Janeiro",
        ///           "estado": "RJ",
        ///           "numero": "381",
        ///           "rua": "Rua Alberto Freire"
        ///        }
        ///     }
        /// </remarks>
        /// <response code ="201">Cliente enviado com sucesso</response>
        /// <response code ="400">Dados inválidos enviados</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ClienteDTO> CreateCliente([FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation("Recebida requisição POST para cliente");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _clienteService.Create(clienteDto);
                return CreatedAtAction(nameof(GetCliente), new { id = clienteDto.Id }, clienteDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar cadastrar cliente");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um cliente existente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <param name="cliente">Dados atualizados do cliente.</param>
        /// <returns>Confirmação de atualização.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /api/v2/cliente/12345678-1234-1234-1234-123456789012
        ///     {
        ///        "id": "12345678-1234-1234-1234-123456789012",
        ///        "nome": "João Atualizado",
        ///        "email": "joao.atualizado@example.com",
        ///        "telefone": "999-1111",
        ///        "endereco": {
        ///           "cep": "21344-050",
        ///           "cidade": "Rio de Janeiro",
        ///           "estado": "RJ",
        ///           "numero": "381",
        ///           "rua": "Rua Alberto Freire"
        ///        }
        ///     }
        /// </remarks>
        /// <response code="204">Cliente atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID inconsistente</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCliente(Guid id, [FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation("Recebida requisição PUT para cliente");

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var clienteExistente = _clienteService.GetById(id);
                if (clienteExistente == null) return NotFound("Cliente não encontrado.");

                if (clienteExistente.Id != clienteDto.Id) return BadRequest("O ID do cliente não pode ser alterado.");

                _clienteService.Update(id, clienteDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar cliente.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /api/v2/cliente/12345678-1234-1234-1234-123456789012
        ///
        /// </remarks>
        /// <response code ="204">Cliente removido com sucesso</response>
        /// <response code ="400">Cliente não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCliente(Guid id)
        {
            _logger.LogInformation("Recebida requisição DELETE para cliente ID {Id}", id);

            try
            {
                if (id == Guid.Empty) return BadRequest("ID inválido.");

                var clienteDto = _clienteService.GetById(id);
                if (clienteDto == null) return NotFound("Cliente não encontrado.");

                _clienteService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar o cliente por ID {Id}.", id);
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            _logger.LogInformation("Recebida requisição GET para listar todos os clientes.");

            try
            {
                var clientes = await _clienteService.GetAllAsync();
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
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(Guid id)
        {
            _logger.LogInformation("Recebida requisição GET para cliente com ID {Id}.", id);

            try
            {
                if (id == Guid.Empty) return BadRequest("ID inválido.");

                var clienteDto = await _clienteService.GetByIdAsync(id);
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
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> CreateCliente([FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation("Recebida requisição POST para cliente");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var clienteCriado = await _clienteService.CreateAsync(clienteDto);
                return CreatedAtAction(nameof(GetCliente), new { id = clienteCriado.Id }, clienteCriado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar cadastrar cliente");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <param name="cliente">Dados atualizados do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(Guid id, [FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation("Recebida requisição PUT para cliente");

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var clienteExistente = await _clienteService.GetByIdAsync(id);
                if (clienteExistente == null) return NotFound("Cliente não encontrado.");

                if (clienteExistente.Id != clienteDto.Id) return BadRequest("O ID do cliente não pode ser alterado.");

                await _clienteService.UpdateAsync(id, clienteDto);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            _logger.LogInformation("Recebida requisição DELETE para cliente ID {Id}", id);

            try
            {
                if (id == Guid.Empty) return BadRequest("ID inválido.");

                var clienteDto = await _clienteService.GetByIdAsync(id);
                if (clienteDto == null) return NotFound("Cliente não encontrado.");

                await _clienteService.DeleteAsync(id);

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

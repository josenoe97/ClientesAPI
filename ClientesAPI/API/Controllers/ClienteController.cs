using ClientesAPI.Domain.Entities;
using ClientesAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteContext _context;

        public ClienteController(ClienteContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        /// <summary>
        /// Retorna um cliente específico pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Cliente encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();
            return cliente;
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="cliente">Objeto cliente.</param>
        /// <returns>Cliente criado.</returns>
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <param name="cliente">Dados atualizados do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(Guid id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();

            var existente = await _context.Clientes.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nome = cliente.Nome;
            existente.Email = cliente.Email;
            existente.Telefone = cliente.Telefone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

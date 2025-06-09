using System.ComponentModel.DataAnnotations;

namespace ClientesAPI.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }
}


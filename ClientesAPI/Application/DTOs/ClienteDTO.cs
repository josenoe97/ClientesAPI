using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientesAPI.Application.DTOs
{
    public class ClienteDTO
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public EnderecoDTO Endereco { get; set; }

    }
}

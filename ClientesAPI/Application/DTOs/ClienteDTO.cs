using System.ComponentModel.DataAnnotations;

namespace ClientesAPI.Application.DTOs
{
    public class ClienteDTO
    {
        public class Clientes
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
            public Endereco Endereco { get; set; }
        }

        public class Endereco
        {
            [Required(ErrorMessage = "A rua é obrigatória.")]
            public string Rua { get; set; }

            [Required(ErrorMessage = "O número é obrigatório.")]
            public string Numero { get; set; }

            [Required(ErrorMessage = "A cidade é obrigatória.")]
            public string Cidade { get; set; }

            [Required(ErrorMessage = "O estado é obrigatório.")]
            public string Estado { get; set; }

            [Required(ErrorMessage = "O CEP é obrigatório.")]
            [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "Formato de CEP inválido.")]
            public string CEP { get; set; }
        }
    }
}

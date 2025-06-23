using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class UsuarioOng : Usuario
    {
        [Required(ErrorMessage = "Insira o CNPJ da ONG para prosseguir.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve conter 14 dígitos")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Digite apenas os números do CNPJ.")]
        [DisplayName("CNPJ")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Insira a Razão Social da ONG para prosseguir.")]
        [StringLength(100)]
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }
    }
}

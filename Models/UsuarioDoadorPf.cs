using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class UsuarioDoadorPf : Usuario
    {
        [DisplayName("CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 dígitos")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Digite apenas os números do CPF.")]
        [Required(ErrorMessage = "Insira o seu CPF para prosseguir")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Insira a sua data de nascimento para prosseguir")]
        [DisplayName("Data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace app.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Insira o seu e-mail.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira a sua senha.")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Senha { get; set; }
    }
}

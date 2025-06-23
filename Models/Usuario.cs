using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using app.ViewModels;
using app.Enums;

namespace app.Models
{
    [Table("Usuario")]
    public abstract class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o nome para prosseguir.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira um número de telefone para prosseguir.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage ="O telefone deve conter 11 dígitos")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "O telefone deve conter apenas números.")]
        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        //Dados de endereço
        [Required(ErrorMessage = "Insira um endereço para prosseguir.")]
        [StringLength(100)]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Insira o número do endereço para prosseguir.")]
        [DisplayName("Número")]
        public int Numero { get; set; }

        [StringLength(50)]
        public string Complemento { get; set; }
        
        [Required(ErrorMessage = "Insira o bairro para prosseguir.")]
        [StringLength(50)]
        public string Bairro { get; set; }
        
        [Required(ErrorMessage = "Insira a cidade para prosseguir.")]
        [StringLength(50)]
        public string Cidade { get; set; }
        
        [Required(ErrorMessage = "Selecione o estado para prosseguir.")]
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "Insira um e-mail para prosseguir.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira uma senha para prosseguir")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Insira sua senha novamente para confirmá-la.")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas inseridas são diferentes.")]
        [NotMapped]
        public string ConfirmaSenha { get; set; }

        public PerfilDeUsuario Perfil {  get; set; }
    }

    public enum PerfilDeUsuario
    {
        Doador = 1,
        ONG = 2
    }
}

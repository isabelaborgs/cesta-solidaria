using System.ComponentModel.DataAnnotations;

namespace app.Enums
{
    public enum TipoDoacao
    {
        [Display(Name ="Oferta do doador")]
        Oferta,
        [Display(Name = "Solicitação da ONG")]
        Solicitacao
    }
}

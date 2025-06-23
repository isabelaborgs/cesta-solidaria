using System.ComponentModel.DataAnnotations;

namespace app.Enums
{
    public enum TipoTransporte
    {
        [Display(Name = "Entrega pelo doador")]
        Entrega,
        [Display(Name = "Retirada pela ONG")]
        Retirada,
        Ambos
    }
}

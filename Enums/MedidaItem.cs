using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace app.Enums
{
    public enum MedidaItem
    {
        [Description("Unidade(s)")]
        un,
        [Description("Grama(s)")]
        g,
        [Description("Quilo(s)")]
        kg,
        [Description("Mililitro(s)")]
        ml,
        [Description("Litro(s)")]
        L
    }
}

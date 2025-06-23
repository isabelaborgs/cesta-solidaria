using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Enums;

namespace app.Models
{
    [Table("ItemOferta")]
    public class ItemOferta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdOferta { get; set; }

        [ForeignKey("IdOferta")]
        public OfertaDoacao Oferta { get; set; }

        [Required]
        public AlimentoNaoPerecivel Alimento { get; set; }

        [Required]
        public int QtdeItem { get; set; }

        [Required]
        public MedidaItem MedidaItem { get; set; }

        [NotMapped]
        public Guid ClientId { get; set; } = Guid.NewGuid();
    }
}

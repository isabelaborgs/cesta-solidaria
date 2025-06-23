using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using app.Enums;

namespace app.Models
{
    [Table("OpcaoAgendamentoOferta")]
    public class OpcaoAgendamentoOferta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdOferta { get; set; }
        [ForeignKey("IdOferta")]
        public OfertaDoacao Oferta { get; set; }

        [Required]
        public DiaDaSemana DiaDaSemana { get; set; }

        [Required]
        public TimeSpan HorarioInicio { get; set; }

        [Required]
        public TimeSpan HorarioFinal { get; set; }

        [NotMapped]
        public Guid ClientId { get; set; } = Guid.NewGuid();
    };
}

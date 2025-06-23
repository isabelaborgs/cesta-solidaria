using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Enums;

namespace app.Models
{
    [Table("OfertaDoacao")]
    public class OfertaDoacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UsuarioDoador")]
        public int IdUsuarioDoador { get; set; }
        public Usuario UsuarioDoador { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Required]
        public Status Status { get; set; } = (Status)0;

        [Required]
        public bool IsCestaCompleta { get; set; }

        [Required]
        public int QtdeCestas { get; set; }

        public List<ItemOferta> ItensOferta { get; set; } = new List<ItemOferta>();

        [Required(ErrorMessage = "Selecione uma forma de transporte da doação.")]
        public TipoTransporte TipoTransporte { get; set; }

        public ICollection<OpcaoAgendamentoOferta> OpcoesAgendamento { get; set; } = new List<OpcaoAgendamentoOferta>();
        public DateTime? DataAgendada { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string JustificativaCancelamento { get; set; }
    }
}

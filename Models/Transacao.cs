using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Enums;

namespace app.Models
{
    [Table("Transacao")]
    public class Transacao
    {
        [Key]
        public int Id { get; set; }

        //Doador
        [Required]
        public int IdDoador { get; set; }

        [ForeignKey("IdDoador")]
        public Usuario Doador { get; set; }

        //Ong
        [Required]
        public int IdOng { get; set; }

        [ForeignKey("IdOng")]
        public Usuario Ong { get; set; }

        //Oferta
        public int? IdOferta { get; set; }

        [ForeignKey("IdOferta")]
        public OfertaDoacao Oferta { get; set; }

        //Solicitação
        public int? IdSolicitacao { get; set; }

        [ForeignKey("IdSolicitacao")]
        public SolicitacaoDoacao Solicitacao { get; set; }

        //Tipo de Doação
        [Required]
        public TipoDoacao TipoDoacao { get; set; }

        //Tipo de Transporte
        [Required]
        public TipoTransporte TipoTransporte { get; set; }

        //Datas
        [Required]
        public DateTime DataAgendada { get; set; }

        [Required]
        public TimeSpan InicioHorarioAgendado { get; set; }

        [Required]
        public TimeSpan FimHorarioAgendado { get; set; }

        public DateTime? DataConclusao { get; set; }

        //Status da transação
        public Status Status { get; set; }

        public string JustificativaCancelamento { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Enums;

namespace app.Models
{
    [Table("OpcaoAgendamentoSolicit")]
    public class OpcaoAgendamentoSolicit
    {
        public int Id { get; set; }

        [Display(Name = "Dia da semana")]
        public string DiaSemana { get; set; }

        [Display(Name = "Horário inicial")]
        public TimeSpan HorarioInicio { get; set; }

        [Display(Name = "Horário final")]
        public TimeSpan HorarioFim { get; set; }
        public int SolicitacaoDoacaoId { get; set; }
        [ForeignKey("SolicitacaoDoacaoId")]
        public SolicitacaoDoacao SolicitacaoDoacao { get; set; }

        [NotMapped]
        public Guid ClientId { get; set; } = Guid.NewGuid();
    }
}

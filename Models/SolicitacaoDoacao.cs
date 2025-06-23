using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Enums;

namespace app.Models
{

    [Table("SolicitacaoDoacao")]
    public class SolicitacaoDoacao
    {
        public int Id { get; set; }

        public int? IdUsuarioOng { get; set; }
        [ForeignKey("IdUsuarioOng")]
        public Usuario UsuarioOng { get; set; }

        [Display(Name = "Nome da ONG")]
        public string NomeOng { get; set; }

        [Display(Name = "Data da solicitação")]
        public DateTime DataSolicitacao { get; set; }

        [Display(Name = "É cesta completa?")]
        public bool IsCestaCompleta { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser zero ou maior.")]
        [Display(Name = "Quantidade de cestas")]
        public int QtdeCestas { get; set; }

        [Display(Name = "Itens solicitados")]
        public List<ItemSolicitacao> ItensSolicitacao { get; set; } = new();

        [Display(Name = "Tipo de transporte")]
        public TipoTransporte TipoTransporte { get; set; }

        [Display(Name = "Datas para agendamento")]
        public List<OpcaoAgendamentoSolicit> AgendamentosSolicitacao { get; set; } = new();

        [Display(Name = "Data de conclusão")]
        public DateTime? DataConclusao { get; set; }

        public Status Status { get; set; } = Status.Pendente;

        [Display(Name = "Justificativa do cancelamento")]
        public string JustificativaCancelamento { get; set; }
    }
}

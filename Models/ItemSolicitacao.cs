using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Enums;

namespace app.Models
{
    [Table("ItemSolicitacao")]
    public class ItemSolicitacao
    {
        [Key]
        public int Id { get; set; } 

        [NotMapped]
        public Guid ClientId { get; set; } = Guid.NewGuid(); 

        [Display(Name = "Alimento")]
        public AlimentoNaoPerecivel Alimento { get; set; }

        [Display(Name = "Quantidade")]
        public int QtdeItem { get; set; }

        [Display(Name = "Medida")]
        public MedidaItem MedidaItem { get; set; }

        // Chave estrangeira
        public int SolicitacaoDoacaoId { get; set; }
        [ForeignKey("SolicitacaoDoacaoId")]
        public SolicitacaoDoacao SolicitacaoDoacao { get; set; }
    }
}

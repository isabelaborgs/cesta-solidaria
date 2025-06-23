using System.ComponentModel.DataAnnotations;

namespace app.Enums
{
    public enum TipoNotificacao
    {
        [Display(Name = "Nova oferta de doação disponível")]
        NovaOferta,
        [Display(Name = "Uma oferta de doação que você cadastrou foi aceita")]
        OfertaAceita,
        [Display(Name = "Nova solicitação de doação disponível")]
        NovaSolicitacao,
        [Display(Name = "Uma solicitação de doação que você cadastrou foi aceita")]
        SolicitacaoAceita,
        [Display(Name = "A entrega de uma de suas doações foi reagendada")]
        Reagendamento,
        [Display(Name = "A entrega de uma de suas doações foi cancelada")]
        AgendamentoCancelado,
        [Display(Name = "Uma de suas doações foi concluída")]
        DoacaoConcluida
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.Models;
using System.Security.Claims;
using app.Enums;
using app.ViewModels;

namespace app.Controllers
{
    public class TransacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListarAgendamentos()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var transacoes = await _context.Transacoes
                .Where(t => t.IdDoador == idUsuario || t.IdOng == idUsuario) //Exibe apenas transações em que o usuário participa
                .Include(t => t.Doador)
                .Include(t => t.Oferta)
                .Include(t => t.Ong)
                .Include(t => t.Solicitacao)
                .ToListAsync();

            return View(transacoes);
        }

        public async Task<IActionResult> AgendarDoacao(int? idDoacao)
        {
            if (idDoacao == null)
            {
                return NotFound();
            }

            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuarioLogado = await _context.Usuarios.FindAsync(idUsuario);

            if (usuarioLogado.Perfil == PerfilDeUsuario.ONG)
            {
                var oferta = await _context.OfertasDoacao
                    .Include(o => o.UsuarioDoador)
                    .Include(o => o.ItensOferta)
                    .Include(o => o.OpcoesAgendamento)
                    .FirstOrDefaultAsync(o => o.Id == idDoacao);

                if (oferta == null)
                {
                    return NotFound();
                }

                var transacao = new Transacao
                {
                    TipoDoacao = TipoDoacao.Oferta,
                    IdDoador = oferta.IdUsuarioDoador,
                    IdOng = idUsuario,
                    IdOferta = oferta.Id,
                    Oferta = oferta,
                    TipoTransporte = oferta.TipoTransporte 
                };

                ViewBag.EnderecoDoador = $"{oferta.UsuarioDoador.Rua}, nº {oferta.UsuarioDoador.Numero}, {oferta.UsuarioDoador.Complemento} {oferta.UsuarioDoador.Bairro}, {oferta.UsuarioDoador.Cidade}/{oferta.UsuarioDoador.Estado}";
                ViewBag.EnderecoOng = $"{usuarioLogado.Rua}, nº {usuarioLogado.Numero}, {usuarioLogado.Bairro}, {usuarioLogado.Cidade}/{usuarioLogado.Estado}";
                return View(transacao);
            }

            else
            {
                var solicitacao = await _context.SolicitacoesDoacao
                    .Include(s => s.UsuarioOng)
                    .Include(s => s.AgendamentosSolicitacao)
                    .FirstOrDefaultAsync(s => s.Id == idDoacao);

                if (solicitacao.IdUsuarioOng == null)
                {
                    return NotFound();
                }

                var transacao = new Transacao
                {
                    TipoDoacao = TipoDoacao.Solicitacao,
                    IdDoador = idUsuario,
                    IdOng = (int)solicitacao.IdUsuarioOng,
                    IdSolicitacao = solicitacao.Id,
                    Solicitacao = solicitacao,
                    TipoTransporte = solicitacao.TipoTransporte
                };

                ViewBag.EnderecoOng = $"{solicitacao.UsuarioOng.Rua}, nº {solicitacao.UsuarioOng.Numero}, {solicitacao.UsuarioOng.Complemento} {solicitacao.UsuarioOng.Bairro}, {solicitacao.UsuarioOng.Cidade}/{solicitacao.UsuarioOng.Estado}";
                ViewBag.EnderecoDoador = $"{usuarioLogado.Rua}, nº {usuarioLogado.Numero}, {usuarioLogado.Bairro}, {usuarioLogado.Cidade}/{usuarioLogado.Estado}";
                return View(transacao);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgendarDoacao(Transacao transacao)
        {
            if (ModelState.IsValid)
            {                
                if (transacao.TipoDoacao == TipoDoacao.Oferta)
                {
                    var oferta = await _context.OfertasDoacao.FindAsync(transacao.IdOferta);
                    oferta.Status = Status.Confirmada;
                    _context.Update(oferta);

                    GerarNotificacao(transacao.IdDoador, TipoNotificacao.OfertaAceita);
                }

                else if (transacao.TipoDoacao == TipoDoacao.Solicitacao)
                {
                    var solicitacao = await _context.SolicitacoesDoacao.FindAsync(transacao.IdSolicitacao);
                    solicitacao.Status = Status.Confirmada;
                    _context.Update(solicitacao);

                    GerarNotificacao(transacao.IdOng, TipoNotificacao.SolicitacaoAceita);
                }

                _context.Add(transacao);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Doação agendada com sucesso!";
                return RedirectToAction(nameof(ListarAgendamentos));
            }

            ViewBag.EnderecoOng = $"{transacao.Ong.Rua}, nº {transacao.Ong.Numero}, {transacao.Ong.Complemento} {transacao.Ong.Bairro}, {transacao.Ong.Cidade}/{transacao.Ong.Estado}";
            ViewBag.EnderecoDoador = $"{transacao.Doador.Rua}, nº {transacao.Doador.Numero}, {transacao.Doador.Complemento} {transacao.Doador.Bairro}, {transacao.Doador.Cidade}/{transacao.Doador.Estado}";
            return View(transacao);
        }

        public async Task<IActionResult> GerenciarAgendamento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacoes
                .Include(t => t.Doador)
                .Include(t => t.Oferta)
                .Include(t => t.Oferta.ItensOferta)
                .Include(t => t.Oferta.OpcoesAgendamento)
                .Include(t => t.Ong)
                .Include(t => t.Solicitacao)
                .Include(t => t.Solicitacao.ItensSolicitacao)
                .Include(t => t.Solicitacao.AgendamentosSolicitacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (transacao == null)
            {
                return NotFound();
            }

            ViewBag.EnderecoOng = $"{transacao.Ong.Rua}, nº {transacao.Ong.Numero}, {transacao.Ong.Complemento} {transacao.Ong.Bairro}, {transacao.Ong.Cidade}/{transacao.Ong.Estado}";
            ViewBag.EnderecoDoador = $"{transacao.Doador.Rua}, nº {transacao.Doador.Numero}, {transacao.Doador.Complemento} {transacao.Doador.Bairro}, {transacao.Doador.Cidade}/{transacao.Doador.Estado}";
            return View(transacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reagendar(Transacao transacao)
        {
            //Atualiza agendamento da doação
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransacaoExists(transacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //Gera notificações
                var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var idUsuarioNotificado = idUsuario == transacao.IdDoador ? transacao.IdOng : transacao.IdDoador; //Notifica a ONG quando o doador reagenda a doação e vice-versa
                GerarNotificacao(idUsuarioNotificado, TipoNotificacao.Reagendamento);

                TempData["Sucesso"] = "Doação reagendada com sucesso!";
                return RedirectToAction("GerenciarAgendamento", new { id = transacao.Id });
            }
            
            return View(transacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelarAgendamento(int id)
        {
            //Atualiza status da transação e adiciona justificativa de cancelamento
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return NotFound();
            }

            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuarioLogado = await _context.Usuarios.FindAsync(idUsuario);

            var registroJustificativa = $"Agendamento cancelado por {usuarioLogado.Nome}";

            transacao.Status = Status.Cancelada;
            transacao.DataConclusao = DateTime.Now; 
            transacao.JustificativaCancelamento = registroJustificativa;

            //Atualiza status da oferta ou solicitação de doação
            if (transacao.TipoDoacao == TipoDoacao.Oferta)
            {
                var oferta = await _context.OfertasDoacao.FindAsync(transacao.IdOferta);
                oferta.Status = Status.Pendente;
                _context.Update(oferta);
            }
            else if (transacao.TipoDoacao == TipoDoacao.Solicitacao)
            {
                var solicitacao = await _context.SolicitacoesDoacao.FindAsync(transacao.IdSolicitacao);
                solicitacao.Status = Status.Pendente;
                _context.Update(solicitacao);
            }

            //Gera notificações
            var idUsuarioNotificado = idUsuario == transacao.IdDoador ? transacao.IdOng : transacao.IdDoador; //Notifica a ONG quando o doador reagenda a doação e vice-versa
            GerarNotificacao(idUsuarioNotificado, TipoNotificacao.AgendamentoCancelado);

            //Atualiza a transação
            _context.Update(transacao);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Agendamento cancelado com sucesso!";
            return RedirectToAction(nameof(ListarAgendamentos));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConcluirDoacao(int id, DateTime dataConclusao)
        {
            //Atualiza status da transação e adiciona data de conclusão
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return NotFound();
            }
            transacao.Status = Status.Concluida;
            transacao.DataConclusao = dataConclusao;

            //Atualiza status da oferta ou solicitação de doação e adiciona data de conclusão
            if (transacao.TipoDoacao == TipoDoacao.Oferta)
            {
                var oferta = await _context.OfertasDoacao.FindAsync(transacao.IdOferta);
                oferta.Status = Status.Concluida;
                oferta.DataConclusao = dataConclusao;
                _context.Update(oferta);
            }
            else if (transacao.TipoDoacao == TipoDoacao.Solicitacao)
            {
                var solicitacao = await _context.SolicitacoesDoacao.FindAsync(transacao.IdSolicitacao);
                solicitacao.Status = Status.Concluida;
                solicitacao.DataConclusao = dataConclusao;
                _context.Update(solicitacao);
            }

            //Atualiza a transação no banco de dados
            _context.Update(transacao);
            await _context.SaveChangesAsync();

            //Gera notificações
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var idUsuarioNotificado = idUsuario == transacao.IdDoador ? transacao.IdOng : transacao.IdDoador; //Notifica a ONG quando o doador reagenda a doação e vice-versa
            GerarNotificacao(idUsuarioNotificado, TipoNotificacao.DoacaoConcluida);

            TempData["Sucesso"] = "Doação concluída com sucesso!";
            return RedirectToAction(nameof(ListarAgendamentos));
        }

        public void GerarNotificacao(int idUsuario, TipoNotificacao tipoNotificacao)
        {
            var notificacao = new Notificacao
            {
                DataNotificacao = DateTime.Now,
                TipoNotificacao = tipoNotificacao,
                IsLida = false,
                IdUsuario = idUsuario,
            };

            _context.Notificacoes.Add(notificacao);
            _context.SaveChanges();
        }

        private bool TransacaoExists(int id)
        {
            return _context.Transacoes.Any(e => e.Id == id);
        }
    }
}

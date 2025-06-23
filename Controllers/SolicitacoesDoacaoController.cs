using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using app.Models;
using app.Enums;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    public class SolicitacoesDoacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitacoesDoacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "ONG")]
        public IActionResult CriarSolicitacao(bool isCestaCompleta)
        {
            var solicitacao = new SolicitacaoDoacao
            {
                IsCestaCompleta = isCestaCompleta,
            };

            return View(solicitacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarSolicitacao(SolicitacaoDoacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                var idOng = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                solicitacao.IdUsuarioOng = idOng;
                solicitacao.DataSolicitacao = DateTime.Now;
                solicitacao.Status = Status.Pendente;
                _context.Add(solicitacao);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Solicitação de doação criada com sucesso!";
                return RedirectToAction("Details", new { id = solicitacao.Id });
            }

            TempData["Erro"] = "Ocorreu um erro ao criar a oferta. Verifique os dados e tente novamente.";
            return View(solicitacao);
        }

        [Authorize(Roles = "ONG")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.NomeOng = User.FindFirstValue(ClaimTypes.Name);

            var solicitacao = await _context.SolicitacoesDoacao
                .Include(s => s.UsuarioOng)
                .Include(s => s.AgendamentosSolicitacao)
                .Include(s => s.ItensSolicitacao) 
                .FirstOrDefaultAsync(s => s.Id == id);


            if (solicitacao == null)
            {
                TempData["Erro"] = "Solicitação não encontrada.";
                return RedirectToAction(nameof(GerenciarSolicitacoes));
            }

            ViewBag.NomeOng = solicitacao.UsuarioOng?.Nome ?? "ONG não encontrada";
            return View(solicitacao);
        }

        [Authorize(Roles = "ONG")]
        public async Task<IActionResult> Edit(int id)
        {
            var solicitacao = await _context.SolicitacoesDoacao
                .Include(s => s.UsuarioOng)
                .Include(s => s.AgendamentosSolicitacao)
                .Include(s => s.ItensSolicitacao)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solicitacao == null)
            {
                TempData["Erro"] = "Solicitação não encontrada.";
                return RedirectToAction(nameof(GerenciarSolicitacoes));
            }

            return View(solicitacao);
        }

        [Authorize(Roles = "ONG")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SolicitacaoDoacao solicitacao)
        {
            if (id != solicitacao.Id)
            {
                return NotFound();
            }

            var solicitacaoExistente = await _context.SolicitacoesDoacao
                .Include(s => s.AgendamentosSolicitacao)
                .Include(s => s.ItensSolicitacao)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solicitacaoExistente == null)
            {
                return NotFound();
            }

            // Atualiza campos simples 
            solicitacaoExistente.QtdeCestas = solicitacao.QtdeCestas;

            // Atualiza ItensSolicitacao
            _context.ItensSolicitacao.RemoveRange(solicitacaoExistente.ItensSolicitacao);
            solicitacaoExistente.ItensSolicitacao = solicitacao.ItensSolicitacao;

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = "Alterações salvas com sucesso!";
                    return RedirectToAction("Details", new { id = solicitacao.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitacaoDoacaoExists(solicitacao.Id))
                    {
                        TempData["Erro"] = "Solicitação não encontrada.";
                        return RedirectToAction("GerenciarSolicitacoes");
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            TempData["Erro"] = "Ocorreu um erro ao atualizar a solicitação. Verifique os dados e tente novamente.";
            return RedirectToAction("Edit", new { id = solicitacao.Id });
        }

        [Authorize(Roles = "Doador")]
        public async Task<IActionResult> ListarSolicitacoesParaDoadores()
        {
            var idUsuarioLogado = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuarioLogado = await _context.Usuarios.FindAsync(idUsuarioLogado);

            var solicitacoesDisponiveis = await _context.SolicitacoesDoacao
                .Where(s => s.UsuarioOng.Estado == usuarioLogado.Estado &&
                            s.Status == Status.Pendente)
                .Include(s => s.UsuarioOng)
                .Include(s => s.ItensSolicitacao)
                .Include(s => s.AgendamentosSolicitacao)
                .ToListAsync();

            return View(solicitacoesDisponiveis);
        }

        [HttpPost]
        public IActionResult AddOpcaoAgendamentoRow([FromBody] OpcaoAgendamentoSolicit agendamento)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Ocorreu um erro ao cadastrar sua oferta. Tente novamente.";
                return BadRequest(ModelState);
            }

            agendamento.ClientId = Guid.NewGuid();
            ViewData.TemplateInfo.HtmlFieldPrefix = $"AgendamentosSolicitacao[{agendamento.ClientId}]";
            return PartialView("_OpcaoAgendamentoRow", agendamento);
        }

        [HttpPost]
        public IActionResult AddItemSolicitacaoRow([FromBody] ItemSolicitacao item)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Ocorreu um erro ao cadastrar sua oferta. Tente novamente.";
                return BadRequest(ModelState);
            }

            item.ClientId = Guid.NewGuid();
            ViewData.TemplateInfo.HtmlFieldPrefix = $"ItensSolicitacao[{item.ClientId}]";
            return PartialView("_ItemSolicitacaoRow", item);
        }

        [Authorize(Roles = "ONG")]
        public async Task<IActionResult> GerenciarSolicitacoes()
        {
            var idOng = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var solicitacoes = await _context.SolicitacoesDoacao
                .Where(s => s.IdUsuarioOng == idOng)
                .Include(s => s.UsuarioOng)
                .Include(s => s.ItensSolicitacao)
                .Include(s => s.AgendamentosSolicitacao)
                .ToListAsync();

            return View(solicitacoes);
        }

        [HttpGet]
        [Authorize(Roles = "ONG")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var solicitacao = await _context.SolicitacoesDoacao.FindAsync(id);
            if (solicitacao == null)
                return NotFound();

            return View(solicitacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ONG")]
        public async Task<IActionResult> Cancelar(int id, string justificativaCancelamento)
        {
            var solicitacao = await _context.SolicitacoesDoacao.FindAsync(id);
            if (solicitacao == null || solicitacao.Status != Status.Pendente)
            {
                return NotFound();
            }
            
            solicitacao.Status = Status.Cancelada;
            solicitacao.JustificativaCancelamento = justificativaCancelamento;
            
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Solicitação cancelada com sucesso.";
            return RedirectToAction(nameof(GerenciarSolicitacoes));
        }

        private bool SolicitacaoDoacaoExists(int id)
        {
            return _context.SolicitacoesDoacao.Any(e => e.Id == id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.Models;
using System.Security.Claims;
using app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using app.Enums;

namespace app.Controllers
{
    public class OfertasDoacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfertasDoacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Doador")]
        public IActionResult CriarOferta(bool isCestaCompleta)
        {
            var oferta = new OfertaDoacao
            {
                IsCestaCompleta = isCestaCompleta,
            };

            return View(oferta);
        }

        [Authorize(Roles = "Doador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarOferta(OfertaDoacao ofertaDoacao)
        {
            if (ModelState.IsValid)
            {
                var idDoador = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                ofertaDoacao.IdUsuarioDoador = idDoador;
                ofertaDoacao.DataCadastro = DateTime.Now;
                ofertaDoacao.Status = Status.Pendente;
                _context.Add(ofertaDoacao);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Oferta de doação criada com sucesso!";
                return RedirectToAction("Details", new { id = ofertaDoacao.Id });
            }

            TempData["Erro"] = "Ocorreu um erro ao criar a oferta. Verifique os dados e tente novamente.";
            return View(ofertaDoacao);
        }

        [Authorize(Roles = "Doador")]
        [HttpPost]
        public IActionResult AddItemDoacaoRow([FromBody] ItemOferta item)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Selecione pelo menos um alimento válido e sua medida.";
                return BadRequest();
            }

            item.ClientId = Guid.NewGuid();
            ViewData.TemplateInfo.HtmlFieldPrefix = $"ItensOferta[{item.ClientId}]";
            return PartialView("_ItemDoacaoRow", item);
        }

        [Authorize(Roles = "Doador")]
        [HttpPost]
        public IActionResult AddOpcaoAgendamentoRow([FromBody] OpcaoAgendamentoOferta opcao)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Ocorreu um erro ao cadastrar sua oferta. Tente novamente.";
                return BadRequest(ModelState);
            }

            opcao.ClientId = Guid.NewGuid();
            ViewData.TemplateInfo.HtmlFieldPrefix = $"OpcoesAgendamento[{opcao.ClientId}]";
            return PartialView("_OpcaoAgendamentoRow", opcao);
        }

        [Authorize(Roles = "Doador")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.NomeDoador = User.FindFirstValue(ClaimTypes.Name);

            var oferta = await _context.OfertasDoacao
                .Include(o => o.UsuarioDoador)
                .Include(o => o.ItensOferta)
                .Include(o => o.OpcoesAgendamento)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (oferta == null)
            {
                TempData["Erro"] = "Oferta de doação não encontrada.";
                return RedirectToAction(nameof(GerenciarOfertas));
            }

            return View(oferta);
        }

        [Authorize(Roles = "Doador")]
        public async Task<IActionResult> Edit(int id)
        {
            var oferta = await _context.OfertasDoacao
                .Include(o => o.UsuarioDoador)
                .Include(o => o.ItensOferta)
                .Include(o => o.OpcoesAgendamento)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (oferta == null)
            {
                TempData["Erro"] = "Oferta de doação não encontrada.";
                return RedirectToAction(nameof(GerenciarOfertas));
            }

            return View(oferta);
        }

        [Authorize(Roles = "Doador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfertaDoacao ofertaDoacao)
        {
            if (id != ofertaDoacao.Id)
                return NotFound();

            var ofertaExistente = await _context.OfertasDoacao
                .Include(o => o.ItensOferta)
                .Include(o => o.OpcoesAgendamento)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ofertaExistente == null)
                return NotFound();

            // Atualiza campos simples
            ofertaExistente.QtdeCestas = ofertaDoacao.QtdeCestas;

            // Atualiza ItensOferta
            _context.ItensOferta.RemoveRange(ofertaExistente.ItensOferta);
            ofertaExistente.ItensOferta = ofertaDoacao.ItensOferta;

            // Atualiza OpcoesAgendamento
            _context.OpcoesAgendamentoOferta.RemoveRange(ofertaExistente.OpcoesAgendamento);
            ofertaExistente.OpcoesAgendamento = ofertaDoacao.OpcoesAgendamento;

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = "Oferta de doação atualizada com sucesso!";
                    return RedirectToAction("Details", new { id = ofertaDoacao.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaDoacaoExists(ofertaDoacao.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            TempData["Erro"] = "Ocorreu um erro ao atualizar a oferta. Verifique os dados e tente novamente.";
            return RedirectToAction("Edit", new { id = ofertaDoacao.Id });
        }

        [Authorize(Roles = "Doador")]
        public async Task<IActionResult> GerenciarOfertas()
        {
            var idDoador = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var ofertas = await _context.OfertasDoacao
                .Where(o => o.IdUsuarioDoador == idDoador)
                .Include(o => o.ItensOferta) // inclui os itens se quiser
                .Include(o => o.OpcoesAgendamento) // inclui opções de agendamento se quiser
                .ToListAsync();

            return View(ofertas);
        }

        //[Authorize(Roles = "Doador")]
        //public async Task<IActionResult> Cancelar(int id)
        //{
        //    var oferta = await _context.OfertasDoacao.FindAsync(id);
        //    if (oferta == null || oferta.Status != Status.Pendente)
        //    {
        //        return NotFound();
        //    }

        //    return View(oferta);
        //}

        [Authorize(Roles = "Doador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            var oferta = await _context.OfertasDoacao.FindAsync(id);
            if (oferta == null || oferta.Status != Status.Pendente)
            {
                return NotFound();
            }

            oferta.Status = Status.Cancelada;
            oferta.DataConclusao = DateTime.Now;

            _context.Update(oferta);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Solicitação cancelada com sucesso.";
            return RedirectToAction(nameof(GerenciarOfertas));
        }

        [Authorize(Roles = "ONG")]
        public async Task<IActionResult> ListarOfertasParaOngs()
        {
            var idUsuarioLogado = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuarioLogado = await _context.Usuarios.FindAsync(idUsuarioLogado);

            var ofertasDisponiveis = await _context.OfertasDoacao
                .Where(o => o.UsuarioDoador.Estado == usuarioLogado.Estado &&
                            o.Status == Status.Pendente) // Exibe apenas ofertas pendentes do mesmo estado
                .Where(o => o.Status == Status.Pendente) 
                .Include(o => o.UsuarioDoador)
                .Include(o => o.ItensOferta)
                .Include(o => o.OpcoesAgendamento)
                .ToListAsync();

            return View(ofertasDisponiveis);
        }
        private bool OfertaDoacaoExists(int id)
        {
            return _context.OfertasDoacao.Any(e => e.Id == id);
        }

    }

}
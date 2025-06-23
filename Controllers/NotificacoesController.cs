using System.Security.Claims;
using app.Enums;
using app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    public class NotificacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PartialViewResult> ListarNotificacoes()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var notificacoes = await _context.Notificacoes
                .Where(n => n.IdUsuario == idUsuario)
                .OrderByDescending(n => n.DataNotificacao)
                .ToListAsync();

            var modelList = notificacoes.Any() ? notificacoes : new List<Notificacao>();

            return PartialView("_ListarNotificacoes", modelList);
        }

        [HttpPost]
        public async Task<IActionResult> MarcarComoLida([FromBody] int id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);

            if (notificacao == null)
            {
                return NotFound();
            }

            notificacao.IsLida = true;
            _context.Update(notificacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ListarNotificacoes));
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirNotificacao([FromBody] int id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);

            if (notificacao == null)
            {
                return NotFound();
            }

            _context.Notificacoes.Remove(notificacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ListarNotificacoes));
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirTodasNotificacoes()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var notificacoes = await _context.Notificacoes
                .Where(n => n.IdUsuario == idUsuario)
                .ToListAsync();

            if (notificacoes.Count != 0)
            {
                _context.Notificacoes.RemoveRange(notificacoes);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ListarNotificacoes));
        }

        [HttpPost]
        public async Task<IActionResult> MarcarTodasComoLidas()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var notificacoes = await _context.Notificacoes
                .Where(n => n.IdUsuario == idUsuario && !n.IsLida)
                .ToListAsync();

            if (notificacoes.Count != 0)
            {
                foreach (var notificacao in notificacoes)
                {
                    notificacao.IsLida = true;
                    _context.Update(notificacao);
                }
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(ListarNotificacoes));
        }
    }
}

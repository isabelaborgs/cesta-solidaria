using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;
using app.ViewModels;
using app.Enums;

namespace app.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: Usuarios/Create
        [AllowAnonymous]
        public IActionResult Create(TipoUsuario tipo)
        {
            switch (tipo)
            {
                case TipoUsuario.DoadorPf:
                    return View("DoadorPf/Create");
                case TipoUsuario.DoadorPj:
                    return View("DoadorPj/Create");
                case TipoUsuario.Ong:
                    return View("Ong/Create");
                default:
                    return BadRequest();
            }
        }

        //POST: Usuarios/DoadorPf/Create
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDoadorPf([Bind("Nome,Cpf,DataNascimento,Id,Telefone,Rua,Numero,Complemento,Bairro,Cidade,Estado,Email,Senha,ConfirmaSenha")] UsuarioDoadorPf usuarioDoadorPf)
        {
            if (ModelState.IsValid)
            {
                usuarioDoadorPf.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDoadorPf.Senha);
                usuarioDoadorPf.Perfil = (PerfilDeUsuario)1;
                _context.Add(usuarioDoadorPf);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction(nameof(Login));
            }
            return View(usuarioDoadorPf);
        }

        //POST: Usuarios/DoadorPj/Create
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDoadorPj([Bind("Cnpj,Nome,RazaoSocial,Id,Telefone,Rua,Numero,Complemento,Bairro,Cidade,Estado,Email,Senha,ConfirmaSenha")] UsuarioDoadorPj usuarioDoadorPj)
        {
            if (ModelState.IsValid)
            {
                usuarioDoadorPj.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDoadorPj.Senha);
                usuarioDoadorPj.Perfil = (PerfilDeUsuario)1;
                _context.Add(usuarioDoadorPj);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction(nameof(Login));
            }
            return View(usuarioDoadorPj);
        }

        //POST: Usuarios/Ong/Create
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOng([Bind("Cnpj,Nome,RazaoSocial,Id,Telefone,Rua,Numero,Complemento,Bairro,Cidade,Estado,Email,Senha,ConfirmaSenha")] UsuarioOng usuarioOng)
        {
            if (ModelState.IsValid)
            {
                usuarioOng.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioOng.Senha);
                usuarioOng.Perfil = (PerfilDeUsuario)2;
                _context.Add(usuarioOng);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction(nameof(Login));
            }
            return View(usuarioOng);
        }

        [AllowAnonymous]
        //GET /Usuarios/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST /Usuarios/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                TempData["Erro"] = "Usuário e/ou senha inválidos!";
                return View();
            }

            bool isSenhaOk = BCrypt.Net.BCrypt.Verify(model.Senha, user.Senha);

            if (isSenhaOk)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Role, user.Perfil.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                var props = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.ToLocalTime().AddDays(7),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(principal, props);

                return RedirectToAction("PaginaInicial", "Home");

            }

            TempData["Erro"] = "Usuário e/ou senha inválidos!";
            return View();
        }

        //GET: Usuarios
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Usuarios");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //GET: Usuarios/Details
        public async Task<IActionResult> Details()
        {
            var userId = CurrentUserId();
            var usuarioBase = await _context.Usuarios.FindAsync(userId);

            if (usuarioBase == null)
            {
                return NotFound();
            }

            if (usuarioBase is UsuarioDoadorPf doadorPf)
            {
                return View("DoadorPf/Details", doadorPf);
            }
            else if (usuarioBase is UsuarioDoadorPj doadorPj)
            {
                return View("DoadorPj/Details", doadorPj);
            }
            else if (usuarioBase is UsuarioOng ong)
            {
                return View("Ong/Details", ong);
            }
            else
            {
                return BadRequest();
            }
        }

        //GET: Usuarios/Edit
        public async Task<IActionResult> Edit()
        {
            var userId = CurrentUserId();
            
            if (userId < 4)
            {   
                TempData["Erro"] = "Não é permitido editar informações dos usuários da demonstração.";
                return RedirectToAction("Details");
            }

            var usuarioBase = await _context.Usuarios.FindAsync(userId);

            if (usuarioBase == null)
            {
                return NotFound();
            }

            if (usuarioBase is UsuarioDoadorPf doadorPf)
            {
                return View("DoadorPf/Edit", doadorPf);
            }
            else if (usuarioBase is UsuarioDoadorPj doadorPj)
            {
                return View("DoadorPj/Edit", doadorPj);
            }
            else if (usuarioBase is UsuarioOng ong)
            {
                return View("Ong/Edit", ong);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDoadorPf(int id, [Bind("Nome,Cpf,DataNascimento,Id,Telefone,Rua,Numero,Complemento,Bairro,Cidade,Estado,Email,Senha,ConfirmaSenha")] UsuarioDoadorPf usuarioDoadorPf)
        {
            if (id != usuarioDoadorPf.Id)
            {
                return NotFound();
            }

            if (usuarioDoadorPf.Id < 4)
            {
                TempData["Erro"] = "Não é permitido editar informações dos usuários da demonstração.";
                return RedirectToAction("Details");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuarioDoadorPf.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDoadorPf.Senha);
                    usuarioDoadorPf.Perfil = (PerfilDeUsuario)1;
                    _context.Update(usuarioDoadorPf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuarioDoadorPf.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["Sucesso"] = "Alterações salvas com sucesso!";
                return RedirectToAction("Details");
            }
            return View("Details", usuarioDoadorPf);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDoadorPj(int id, [Bind("Cnpj,Nome,RazaoSocial,Id,Telefone,Rua,Numero,Complemento,Bairro,Cidade,Estado,Email,Senha,ConfirmaSenha")] UsuarioDoadorPj usuarioDoadorPj)
        {
            if (id != usuarioDoadorPj.Id)
            {
                return NotFound();
            }

            if (usuarioDoadorPj.Id < 4)
            {
                TempData["Erro"] = "Não é permitido editar informações dos usuários da demonstração.";
                return RedirectToAction("Details");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuarioDoadorPj.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDoadorPj.Senha);
                    usuarioDoadorPj.Perfil = (PerfilDeUsuario)1;
                    _context.Update(usuarioDoadorPj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuarioDoadorPj.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["Sucesso"] = "Alterações salvas com sucesso!";
                return RedirectToAction("Details");
            }
            return View("Details", usuarioDoadorPj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOng(int id, [Bind("Cnpj,Nome,RazaoSocial,Id,Telefone,Rua,Numero,Complemento,Bairro,Cidade,Estado,Email,Senha,ConfirmaSenha")] UsuarioOng usuarioOng)
        {
            if (id != usuarioOng.Id)
            {
                return NotFound();
            }

            if (usuarioOng.Id < 4)
            {
                TempData["Erro"] = "Não é permitido editar informações dos usuários da demonstração.";
                return RedirectToAction("Details");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuarioOng.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioOng.Senha);
                    usuarioOng.Perfil = (PerfilDeUsuario)2;
                    _context.Update(usuarioOng);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuarioOng.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["Sucesso"] = "Alterações salvas com sucesso!";
                return RedirectToAction("Details");
            }
            return View("Details", usuarioOng);
        }

        //GET: Usuarios/Delete
        public IActionResult Delete()
        {
            var userId = CurrentUserId();

            if (userId < 4)
            {
                TempData["Erro"] = "Não é permitido excluir um usuário da demonstração.";
                return RedirectToAction("Details");
            }

            return View();
        }

        // POST: Usuarios/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string senha)
        {
            var userId = CurrentUserId();

            if (userId < 4)
            {
                TempData["Erro"] = "Não é permitido excluir um usuário da demonstração.";
                return RedirectToAction("Details");
            }

            var usuario = await _context.Usuarios.FindAsync(userId);

            if (usuario == null)
            {
                return NotFound();
            }

            if (senha == null)
            {
                ViewBag.Message = "Digite sua senha para excluir a conta";
                return View("Delete", usuario);
            }

            bool isSenhaOk = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

            if (isSenhaOk)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                await Logout();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Senha inválida";
                return View("Delete", usuario);
            }
        }

        private int CurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}

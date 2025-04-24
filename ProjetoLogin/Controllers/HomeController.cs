using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoLogin.Areas.Identity.Pages.Account;
using ProjetoLogin.Data;
using ProjetoLogin.Models;

namespace ProjetoLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public HomeController(SignInManager<IdentityUser> signInManager, ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _signInManager = signInManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            // Verifica se o usu�rio est� logado
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }

            var userId = user.Id; // Obtem o ID do usu�rio logado
            // Verifica se o usu�rio j� completou o cadastro na tabela Usuarios
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.AppUserId == Guid.Parse(userId));

            // Se o usu�rio n�o completou o cadastro, redireciona para a p�gina de cadastro
            if (usuario == null)
            {
                return RedirectToAction("Create", "Usuarios");
            }

            // Caso contr�rio, exibe a p�gina Index normalmente
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
            // Verifica se o usuário está logado
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }

            var userId = user.Id; // Obtem o ID do usuário logado
            // Verifica se o usuário já completou o cadastro na tabela Usuarios
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.AppUserId == Guid.Parse(userId));

            // Se o usuário não completou o cadastro, redireciona para a página de cadastro
            if (usuario == null)
            {
                return RedirectToAction("Create", "Usuarios");
            }

            // Caso contrário, exibe a página Index normalmente
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

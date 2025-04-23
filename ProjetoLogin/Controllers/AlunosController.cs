using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLogin.Data;
using ProjetoLogin.Models;

namespace ProjetoLogin.Controllers
{
    [Authorize]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alunos
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alunos.ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoId,Nome,Email,Celular,CPF,DataNasc,RM,DataCadastro,CadastroAtivo,Endereco,Bairro,Cidade,UF,CEP,Numero")] Aluno aluno, IFormFile UrlFoto)
        {
            if (ModelState.IsValid)
            {
                aluno.AlunoId = Guid.NewGuid();
                aluno.DataCadastro = DateTime.Now;
                aluno.CadastroAtivo = true;
                // Verificar se o arquivo foi enviado
                if (UrlFoto != null && UrlFoto.Length > 0)
                {
                    // Definir o caminho para salvar a imagem
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Photos");
                    var uniqueFileName = $"{Guid.NewGuid()}_{UrlFoto.FileName}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Criar a pasta se não existir
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Salvar o arquivo no diretório
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await UrlFoto.CopyToAsync(fileStream);
                    }

                    // Atualizar o campo UrlFoto com o caminho relativo
                    aluno.UrlFoto = Path.Combine("Resources", "Photos", uniqueFileName).Replace("\\", "/");
                }

                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AlunoId,Nome,Email,Celular,CPF,DataNasc,RM,DataCadastro,CadastroAtivo,Endereco,Bairro,Cidade,UF,CEP,Numero,UrlFoto")] Aluno aluno)
        {
            if (id != aluno.AlunoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.AlunoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(Guid id)
        {
            return _context.Alunos.Any(e => e.AlunoId == id);
        }

        // Criar método para Ativar/Desativar Cadastro do Aluno
        // POST: Alunos/ToggleStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.CadastroAtivo = !aluno.CadastroAtivo;
            _context.Update(aluno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // Incluir um método de busca
        // GET: Alunos/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Index", await _context.Alunos.ToListAsync());
            }

            var alunos = await _context.Alunos
                .Where(a => a.Nome.Contains(searchTerm) || a.Email.Contains(searchTerm))
                .ToListAsync();

            return View("Index", alunos);
        }

    }
}

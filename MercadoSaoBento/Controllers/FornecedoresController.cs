using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercadoSaoBento.Data;
using MercadoSaoBento.Models;
using Microsoft.AspNetCore.Authorization;

namespace MercadoSaoBento.Controllers
{
    [Authorize]
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornecedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedores.ToListAsync());
        }

        // GET: Fornecedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.FornecedorID == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nome,CNPJ,Telefone")] Fornecedor fornecedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(fornecedor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível salvar as alterações." +
                "Tente novamente, e se o problema persistir" +
                "consulte o administrador do sitema.");
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores.SingleOrDefaultAsync(m => m.FornecedorID == id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FornecedorID,Nome,CNPJ,Telefone")] Fornecedor fornecedor)
        {
            if (id != fornecedor.FornecedorID)
            {
                return NotFound();
            }
            var fornecedorToUpdate = await _context.Fornecedores
                .SingleOrDefaultAsync(f => f.FornecedorID == id);
            if (await TryUpdateModelAsync<Fornecedor>(
                fornecedorToUpdate,
                "",
                f => f.Nome, f => f.CNPJ, f => f.Telefone))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Não foi possível salvar as alterações." +
                    "Tente novamente, e se o problema persistir" +
                    "consulte o administrador do sitema.");
                }
            }
            return View(fornecedorToUpdate);
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(int? id, bool?
            saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.FornecedorID == id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "A exclusão falhou. Tente novamente, e se o problema" +
                    "persistir, contate o seu administrador.";
            }

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedores
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.FornecedorID == id);
            if (fornecedor == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        private bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.FornecedorID == id);
        }
    }
}

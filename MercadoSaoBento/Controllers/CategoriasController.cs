using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercadoSaoBento.Data;
using MercadoSaoBento.Models;

namespace MercadoSaoBento.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nome")] Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(categoria);
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
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("CategoriaID,Nome")] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }
            var categoriaToUpdate = await _context.Categorias
                .SingleOrDefaultAsync(c => c.CategoriaID == id);
            if (await TryUpdateModelAsync<Categoria>(
                categoriaToUpdate,
                "",
                c => c.Nome))
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
            return View(categoriaToUpdate);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id, bool?
            saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "A exclusão falhou. Tente novamente, e se o problema" +
                    "persistir, contate o seu administrador.";
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            _context.Categorias.Remove(categoria);
            if (categoria == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }
        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.CategoriaID == id);
        }
    }
}

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
    public class MotivosSaidasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotivosSaidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MotivosSaidas
        public async Task<IActionResult> Index()
        {
            return View(await _context.MotivosSaidas.ToListAsync());
        }

        // GET: MotivosSaidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivosSaida = await _context.MotivosSaidas
                .SingleOrDefaultAsync(m => m.MotivosSaidaID == id);
            if (motivosSaida == null)
            {
                return NotFound();
            }

            return View(motivosSaida);
        }

        // GET: MotivosSaidas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MotivosSaidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MotivosSaidaID,motivo")] MotivosSaida motivosSaida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motivosSaida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motivosSaida);
        }

        // GET: MotivosSaidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivosSaida = await _context.MotivosSaidas.SingleOrDefaultAsync(m => m.MotivosSaidaID == id);
            if (motivosSaida == null)
            {
                return NotFound();
            }
            return View(motivosSaida);
        }

        // POST: MotivosSaidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MotivosSaidaID,motivo")] MotivosSaida motivosSaida)
        {
            if (id != motivosSaida.MotivosSaidaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motivosSaida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotivosSaidaExists(motivosSaida.MotivosSaidaID))
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
            return View(motivosSaida);
        }

        // GET: MotivosSaidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivosSaida = await _context.MotivosSaidas
                .SingleOrDefaultAsync(m => m.MotivosSaidaID == id);
            if (motivosSaida == null)
            {
                return NotFound();
            }

            return View(motivosSaida);
        }

        // POST: MotivosSaidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motivosSaida = await _context.MotivosSaidas.SingleOrDefaultAsync(m => m.MotivosSaidaID == id);
            _context.MotivosSaidas.Remove(motivosSaida);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotivosSaidaExists(int id)
        {
            return _context.MotivosSaidas.Any(e => e.MotivosSaidaID == id);
        }
    }
}

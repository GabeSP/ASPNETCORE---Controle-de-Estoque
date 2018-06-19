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
    public class movEntradasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public movEntradasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: movEntradas
        public async Task<IActionResult> Index(string ordem,
            string filtroAtual,
            string procurarLinha,
            int? pagina)
        {
            ViewData["OrdemAtual"] = ordem;
            ViewData["OrdemData"] = ordem == "Data" ? "data_desc" : "Data";
            ViewData["OrdemCodigo"] = ordem == "Codigo" ? "codigo_desc" : "Codigo";

            if (procurarLinha != null)
            {
                pagina = 1;
            }
            else
            {
                procurarLinha = filtroAtual;
            }

            ViewData["FiltroPesquisarAtual"] = procurarLinha;

            var entradas = from e in _context.movEntradas select e;
            if (!String.IsNullOrEmpty(procurarLinha))
            {
                entradas = entradas.Where(e => e.Produto.Nome.Contains(procurarLinha));
            }

            switch (ordem)
            {
                case "Data":
                    entradas = entradas.OrderBy(e => e.dataEntrada);
                    break;
                case "data_desc":
                    entradas = entradas.OrderByDescending(e => e.dataEntrada);
                    break;
                case "Codigo":
                    entradas = entradas.OrderBy(e => e.movEntradaID);
                    break;
                case "codigo_desc":
                    entradas = entradas.OrderByDescending(e => e.movEntradaID);
                    break;
                default:
                    entradas = entradas.OrderBy(e => e.dataEntrada);
                    break;
            }
            int tamanhoPagina = 6;
            return View(await Paginacao<movEntrada>.CreateAsync(entradas.Include(p => p.Produto).AsNoTracking(), pagina ?? 1, tamanhoPagina));
            //var applicationDbContext = _context.movEntradas.Include(m => m.Produto);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: movEntradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movEntrada = await _context.movEntradas
                .Include(m => m.Produto)
                .SingleOrDefaultAsync(m => m.movEntradaID == id);
            if (movEntrada == null)
            {
                return NotFound();
            }

            return View(movEntrada);
        }

        // GET: movEntradas/Create
        public IActionResult Create()
        {
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome");
            return View();
        }

        // POST: movEntradas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("movEntradaID,nmrDocumento,dataEntrada,Quantidade,obs,ProdutoID")] movEntrada movEntrada, int? id)
        {

            //var produto = await _context.movEntradas
            //    .Include(p => p.Produto)
                //.AsNoTracking()
                //.SingleAsync(m => m.movEntradaID == id);

            if (ModelState.IsValid)
            {
                _context.Add(movEntrada);
                //produto.Produto.QtdEstoque += movEntrada.Quantidade;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome", movEntrada.ProdutoID);
            return View(movEntrada);
        }

        // GET: movEntradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movEntrada = await _context.movEntradas.SingleOrDefaultAsync(m => m.movEntradaID == id);
            if (movEntrada == null)
            {
                return NotFound();
            }
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome", movEntrada.ProdutoID);
            return View(movEntrada);
        }

        // POST: movEntradas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("movEntradaID,nmrDocumento,dataEntrada,Quantidade,obs,ProdutoID")] movEntrada movEntrada)
        {
            if (id != movEntrada.movEntradaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movEntrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!movEntradaExists(movEntrada.movEntradaID))
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
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome", movEntrada.ProdutoID);
            return View(movEntrada);
        }

        // GET: movEntradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movEntrada = await _context.movEntradas
                .Include(m => m.Produto)
                .SingleOrDefaultAsync(m => m.movEntradaID == id);
            if (movEntrada == null)
            {
                return NotFound();
            }

            return View(movEntrada);
        }

        // POST: movEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movEntrada = await _context.movEntradas.SingleOrDefaultAsync(m => m.movEntradaID == id);
            _context.movEntradas.Remove(movEntrada);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool movEntradaExists(int id)
        {
            return _context.movEntradas.Any(e => e.movEntradaID == id);
        }
    }
}

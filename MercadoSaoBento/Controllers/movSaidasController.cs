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
    public class movSaidasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public movSaidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: movSaidas
        public async Task<IActionResult> Index(string ordem,
            string filtroAtual,
            string procurarLinha,
            int? pagina)
        {
            ViewData["OrdemAtual"] = ordem;
            ViewData["OrdeData"] = ordem == "Data" ? "data_desc" : "Data";
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

            var saidas = from s in _context.movSaidas select s;
            if (!String.IsNullOrEmpty(procurarLinha))
            {
                saidas = saidas.Where(s => s.Produto.Nome.Contains(procurarLinha));
            }

            switch (ordem)
            {
                case "Data":
                    saidas = saidas.OrderBy(s => s.dataSaida);
                    break;
                case "data_desc":
                    saidas = saidas.OrderByDescending(s => s.dataSaida);
                    break;
                case "Codigo":
                    saidas = saidas.OrderBy(s => s.movSaidaID);
                    break;
                case "codigo_desc":
                    saidas = saidas.OrderByDescending(s => s.movSaidaID);
                    break;

                default:
                    saidas = saidas.OrderBy(s => s.dataSaida);
                    break;
            }
            int tamanhoPagina = 6;
            return View(await Paginacao<movSaida>.CreateAsync(saidas.Include(m => m.MotivosSaida).Include(m => m.Produto).AsNoTracking(), pagina ?? 1, tamanhoPagina));
            
        }

        // GET: movSaidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movSaida = await _context.movSaidas
                .Include(m => m.MotivosSaida)
                .Include(m => m.Produto)
                .SingleOrDefaultAsync(m => m.movSaidaID == id);
            if (movSaida == null)
            {
                return NotFound();
            }

            return View(movSaida);
        }

        // GET: movSaidas/Create
        public IActionResult Create()
        {
            ViewData["MotivosSaidaID"] = new SelectList(_context.MotivosSaidas, "MotivosSaidaID", "motivo");
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome");
            return View();
        }

        // POST: movSaidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("movSaidaID,dataSaida,Quantidade,obs,ProdutoID,MotivosSaidaID")] movSaida movSaida)
        {
            if(movSaida.Quantidade < 1)
            {
                ModelState.AddModelError("Quantidade", "Quantidade inválida");
            }
            if (movSaida.dataSaida < DateTimeOffset.UtcNow.DateTime.Date)
            {
                ModelState.AddModelError("dataSaida", "A Data não pode ser anterior a Data Atual");
            }
            if (ModelState.IsValid)
            {
                _context.Add(movSaida);
                var produto = await _context.Produtos
                    .FirstAsync(m => m.ProdutoID == movSaida.ProdutoID);
                produto.QtdEstoque -= movSaida.Quantidade;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MotivosSaidaID"] = new SelectList(_context.MotivosSaidas, "MotivosSaidaID", "motivo", movSaida.MotivosSaidaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome", movSaida.ProdutoID);
            return View(movSaida);
        }

        // GET: movSaidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movSaida = await _context.movSaidas.SingleOrDefaultAsync(m => m.movSaidaID == id);
            if (movSaida == null)
            {
                return NotFound();
            }
            ViewData["MotivosSaidaID"] = new SelectList(_context.MotivosSaidas, "MotivosSaidaID", "motivo", movSaida.MotivosSaidaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome", movSaida.ProdutoID);
            return View(movSaida);
        }

        // POST: movSaidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("movSaidaID,dataSaida, Quantidade,obs,ProdutoID,MotivosSaidaID")] movSaida movSaida)
        {
            if (id != movSaida.movSaidaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movSaida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!movSaidaExists(movSaida.movSaidaID))
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
            ViewData["MotivosSaidaID"] = new SelectList(_context.MotivosSaidas, "MotivosSaidaID", "motivo", movSaida.MotivosSaidaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ProdutoID", "Nome", movSaida.ProdutoID);
            return View(movSaida);
        }

        // GET: movSaidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movSaida = await _context.movSaidas
                .Include(m => m.MotivosSaida)
                .Include(m => m.Produto)
                .SingleOrDefaultAsync(m => m.movSaidaID == id);
            if (movSaida == null)
            {
                return NotFound();
            }

            return View(movSaida);
        }

        // POST: movSaidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movSaida = await _context.movSaidas.SingleOrDefaultAsync(m => m.movSaidaID == id);
            _context.movSaidas.Remove(movSaida);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool movSaidaExists(int id)
        {
            return _context.movSaidas.Any(e => e.movSaidaID == id);
        }
    }
}

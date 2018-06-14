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
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index(string ordem,
            string filtroAtual,
            string procurarLinha,
            int? pagina)
        {
            ViewData["OrdemAtual"] = ordem;
            ViewData["OrdemNome"] = ordem == "Nome" ? "nome_desc" : "Nome";
            ViewData["OrdemCodigo"] = ordem == "Codigo" ? "codigo_desc" : "Codigo";
            ViewData["OrdemPreco"] = ordem == "Preco" ? "preco_desc" : "Preco";

            if (procurarLinha != null)
            {
                pagina = 1;
            }
            else
            {
                procurarLinha = filtroAtual;
            }

            ViewData["FiltroPesquisarAtual"] = procurarLinha;

            var produtos = from p in _context.Produtos select p;
            if (!String.IsNullOrEmpty(procurarLinha))
            {
                produtos = produtos.Where(p => p.Nome.Contains(procurarLinha));
            }

            switch (ordem)
            {
                case "Nome":
                    produtos = produtos.OrderBy(p => p.Nome);
                    break;
                case "nome_desc":
                    produtos = produtos.OrderByDescending(p => p.Nome);
                    break;
                case "Codigo":
                    produtos = produtos.OrderBy(p => p.Nome);
                    break;
                case "codigo_desc":
                    produtos = produtos.OrderByDescending(p => p.Nome);
                    break;
                case "Preco":
                    produtos = produtos.OrderBy(p => p.Nome);
                    break;
                case "preco_desc":
                    produtos = produtos.OrderByDescending(p => p.Nome);
                    break;

                default:
                    produtos = produtos.OrderBy(p => p.ProdutoID);
                    break;
            }
            int tamanhoPagina = 10;
            return View(await Paginacao<Produto>.CreateAsync(produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).AsNoTracking(), pagina ?? 1, tamanhoPagina));
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .SingleOrDefaultAsync(m => m.ProdutoID == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "CategoriaID", "Nome");
            ViewData["FornecedorID"] = new SelectList(_context.Fornecedores, "FornecedorID", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nome,Descricao,Preco,QtdEstoque,FornecedorID,CategoriaID")] Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(produto);
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
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "CategoriaID", "Nome", produto.CategoriaID);
            ViewData["FornecedorID"] = new SelectList(_context.Fornecedores, "FornecedorID", "Nome", produto.FornecedorID);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.SingleOrDefaultAsync(m => m.ProdutoID == id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "CategoriaID", "Nome", produto.CategoriaID);
            ViewData["FornecedorID"] = new SelectList(_context.Fornecedores, "FornecedorID", "Nome", produto.FornecedorID);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoID,Nome,Descricao,Preco,QtdEstoque,FornecedorID,CategoriaID")] Produto produto)
        {
            if (id != produto.ProdutoID)
            {
                return NotFound();
            }
            var produtoToUpdate = await _context.Produtos
                .SingleOrDefaultAsync(p => p.ProdutoID == id);
            if (await TryUpdateModelAsync<Produto>(
                produtoToUpdate,
                "",
                p => p.Nome, p => p.Descricao, p => p.Preco, p => p.QtdEstoque, p => p.FornecedorID, p => p.CategoriaID))
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
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "CategoriaID", "Nome", produtoToUpdate.CategoriaID);
            ViewData["FornecedorID"] = new SelectList(_context.Fornecedores, "FornecedorID", "Nome", produtoToUpdate.FornecedorID);
            return View(produtoToUpdate);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id, bool?
            saveChangesError)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ProdutoID == id);
            if (produto == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "A exclusão falhou. Tente novamente, e se o problema" +
                    "persistir, contate o seu administrador.";
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ProdutoID == id);
            if (produto == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        public async Task<IActionResult> DecrementaQtd(int id)
        {
            var produto = await _context.Produtos
            .SingleOrDefaultAsync(m => m.ProdutoID == id);
            produto.QtdEstoque--;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoID == id);
        }
    }
}

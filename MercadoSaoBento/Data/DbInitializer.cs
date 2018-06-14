using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoSaoBento.Models;

namespace MercadoSaoBento.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Produtos.Any())
            {
                return;
            }
            if (context.Fornecedores.Any())
            {
                return;
            }
            if (context.Categorias.Any())
            {
                return;
            }

            var fornecedores = new Fornecedor[]
            {
                new Fornecedor{Nome="Coca Cola", CNPJ="12345678901234", Telefone="(15)1234-5678"},
                new Fornecedor{Nome="Ambev", CNPJ="12345678901234", Telefone="(15)1234-5908"}

            };
            foreach (Fornecedor f in fornecedores)
            {
                context.Fornecedores.Add(f);
            }
            context.SaveChanges();

            var categorias = new Categoria[]
            {
                new Categoria{Nome="Bebida"},
                new Categoria{Nome="Padaria"},
                new Categoria{Nome="Laticínios"},
                new Categoria{Nome="Limpeza"},
                new Categoria{Nome="PetShop"},
                new Categoria{Nome="Bebida Alcoólica"},
                new Categoria{Nome="HortiFruti"},
                new Categoria{Nome="Higiene Pessoal"},
                new Categoria{Nome="Utilidades Domésticas"},
                new Categoria{Nome="Açougue"},
                new Categoria{Nome="Massas"},
                new Categoria{Nome="Enlatados"}
            };
            foreach (Categoria c in categorias)
            {
                context.Categorias.Add(c);
            }
            context.SaveChanges();

            var produtos = new Produto[]
            {
                new Produto{Nome="Fanta Uva", Descricao="Refrigerante 500ml", Preco=5.5m, QtdEstoque=200, FornecedorID=1, CategoriaID=1}
            };
            foreach (Produto p in produtos)
            {
                context.Produtos.Add(p);
            }
            context.SaveChanges();
        }
    }
}

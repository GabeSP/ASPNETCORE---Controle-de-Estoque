using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MercadoSaoBento.Models;

namespace MercadoSaoBento.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<movEntrada> movEntradas { get; set; }
        public DbSet<movSaida> movSaidas { get; set; }
        public DbSet<MotivosSaida> MotivosSaidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedor");
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<movEntrada>().ToTable("movEntrada");
            modelBuilder.Entity<movSaida>().ToTable("movSaida");
            modelBuilder.Entity<MotivosSaida>().ToTable("MotivosSaida");
        }

        
    }
}

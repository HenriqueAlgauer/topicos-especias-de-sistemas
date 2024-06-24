using Microsoft.EntityFrameworkCore;
using loja.models;

namespace loja.data
{
    public class LojaDbContext : DbContext
    {
        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                // Configura explicitamente o Id como chave primária
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                // Configura explicitamente o Id como chave primária
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
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
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>()
                .HasOne(produto => produto.Fornecedor)
                .WithMany(fornecedor => fornecedor.Produtos)
                .HasForeignKey(produto => produto.FornecedorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venda>()
                .HasOne(venda => venda.Produto)
                .WithMany(produto => produto.Vendas)
                .HasForeignKey(venda => venda.IdProduto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venda>()
                .HasOne(venda => venda.Cliente)
                .WithMany(cliente => cliente.Vendas)
                .HasForeignKey(venda => venda.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deposito>()
                .HasOne(deposito => deposito.Produto)
                .WithMany(produto => produto.Depositos)
                .HasForeignKey(deposito => deposito.IdProduto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contrato>()
                .HasOne(contrato => contrato.Servico)
                .WithMany(servico => servico.Contratos)
                .HasForeignKey(contrato => contrato.IdServico)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contrato>()
                .HasOne(contrato => contrato.Cliente)
                .WithMany(cliente => cliente.Contratos)
                .HasForeignKey(contrato => contrato.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
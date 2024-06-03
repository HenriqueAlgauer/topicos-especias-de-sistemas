using Microsoft.EntityFrameworkCore;
//using System.Data.Entity.Core.Metadata;
//using Microsoft.AspNetCore.Http.Metadata;

using Loja.Models;

namespace Loja.Data{

    public class LojaDbContext : DbContext{
        public LojaDbContext(DbContextOptions<LojaDbContext> options):base(options){}
        public DbSet<Produto> Produtos {get;set;}
    }

}
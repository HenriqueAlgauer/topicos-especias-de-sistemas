using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ProductService
    {
        private readonly LojaDbContext _dbContext;
        public ProductService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Produto>> GetAllProducts()
        {
            return await _dbContext.Produtos.ToListAsync();
        }
        public async Task<Produto> GetProductById(int id)
        {
            return await _dbContext.Produtos.FindAsync(id);
        }
        public async Task NewProduct(Produto produto)
        {
            _dbContext.Produtos.Add(produto);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateProduct(Produto produto)
        {
            _dbContext.Entry(produto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteProduct(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);
            if (produto != null)
            {
                _dbContext.Produtos.Remove(produto);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
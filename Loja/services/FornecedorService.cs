using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class FornecedorService
    {
        private readonly LojaDbContext _dbContext;
        public FornecedorService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Fornecedor>> GetAllFornecedores()
        {
            return await _dbContext.Fornecedores.ToListAsync();
        }
        public async Task<Fornecedor> GetFornecedorById(int id)
        {
            return await _dbContext.Fornecedores.FindAsync(id);
        }
        public async Task NewFornecedor(Fornecedor fornecedor)
        {
            _dbContext.Fornecedores.Add(fornecedor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateFornecedor(Fornecedor fornecedor)
        {
            _dbContext.Entry(fornecedor).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteFornecedor(int id)
        {
            var fornecedor = await _dbContext.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                _dbContext.Fornecedores.Remove(fornecedor);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
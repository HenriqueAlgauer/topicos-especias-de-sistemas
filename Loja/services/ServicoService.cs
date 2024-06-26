using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ServicoService
    {
        public readonly LojaDbContext _dbContext;

        public ServicoService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Servico>> GetAllServicos()
        {
            return await _dbContext.Servicos.ToListAsync();
        }

        public async Task<Servico> GetServicoById(int id)
        {
            return await _dbContext.Servicos.FindAsync(id);
        }

        public async Task NewServico(Servico servico)
        {
            _dbContext.Servicos.Add(servico);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateServico(int id, Servico servico)
        {
            var servicoEX = await _dbContext.Servicos.FirstOrDefaultAsync(u => u.Id == id);
            if (servicoEX == null)
            {
                throw new InvalidOperationException("Servico não encontrado !");
            }
            servicoEX.Nome = servico.Nome;
            servicoEX.Preco = servico.Preco;
            servicoEX.Status = servico.Status;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteServicoById(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);

            if (servico != null)
            {
                _dbContext.Remove(servico);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
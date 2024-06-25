using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.models;
using loja.data;
namespace loja.services
{
    public class ContratoService
    {
        public readonly LojaDbContext _dbContext;

        public ContratoService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task NewContratosAsync(Contrato contrato)
        {
            _dbContext.Contratos.Add(contrato);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<dynamic>> GetAllContratos(int id)
        {
            return await _dbContext.Contratos
            .Where(contrato => contrato.IdCliente == id)
            .Select(contrato => new
            {
                contrato.Id,
                contrato.IdCliente,
                cliente = contrato.Cliente.Nome,
                contrato.IdServico,
                servico = contrato.Servico.Nome,
                contrato.PrecoCobrado,
                contrato.DataContratacao
            })
            .ToListAsync();
        }
    }
}
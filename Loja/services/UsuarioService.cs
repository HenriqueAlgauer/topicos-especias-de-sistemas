using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class UsuarioService
    {
        public readonly LojaDbContext _dbContext;

        public UsuarioService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }
        public async Task<Usuario> GetUsuarioByLogin(string userEmail)
        {
            var usuario = await _dbContext.Usuarios.SingleOrDefaultAsync(x => x.Email == userEmail);
            return usuario;
        }
        public async Task NewUsuario(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateUsuario(int id, Usuario usuario)
        {
            var existingUser = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Não foi possível encontrar o usuário");
            }

            existingUser.Email = usuario.Email;
            existingUser.Senha = usuario.Senha;

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteUsuarioBy(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _dbContext.Remove(usuario);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
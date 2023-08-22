using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BackEnd.Domains.IRepositories;
using BackEnd.Persistence.Context;
using BackEnd.Domains.Models;

namespace BackEnd.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AplicationDbContext _context;
        public UsuarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveUser(Usuario usuario)
        {
            _context.Add(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            var validaExistence = await _context.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);

            return validaExistence;
        }

        public async Task<Usuario> ValidatePassword(int idUsuario, string passworAnterior)
        {
            var usuario = await _context.Usuarios
                .Where(x => x.Id == idUsuario && x.PassWord == passworAnterior)
                .FirstOrDefaultAsync();
            return usuario; // Si encuentra un macheo quiere decir que la contraseña y el id de usuario ya existen por lo cual la contrasena es correcta, de lo contrario si devuelve null significa que la contraseña no es correcta
        }
        public async Task UpdatePassword(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            //TODO: Verificar si es necesario validar si existe el registro para poder modificarlo.
            await _context.SaveChangesAsync();
        }

    }
}

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BackEnd.Domains.IRepositories;
using BackEnd.Domains.Models;
using BackEnd.Persistence.Context;

namespace BackEnd.Persistence.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AplicationDbContext _context;

        public LoginRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            var user = await _context.Usuarios
                .Where(x => x.NombreUsuario == usuario.NombreUsuario && x.PassWord == usuario.PassWord)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}

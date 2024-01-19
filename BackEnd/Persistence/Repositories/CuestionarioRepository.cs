using BackEnd.Domains.IRepositories;
using BackEnd.Domains.Models;
using BackEnd.Persistence.Context;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
    public class CuestionarioRepository : ICuestionarioRepository
    {
        private readonly AplicationDbContext _context;

        public CuestionarioRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateCuestionario(Cuestonario cuestonario)
        {
           _context.Add(cuestonario);
           await _context.SaveChangesAsync();
        }
    }
}

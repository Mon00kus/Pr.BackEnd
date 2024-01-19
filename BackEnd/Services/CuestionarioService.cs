using BackEnd.Domains.IRepositories;
using BackEnd.Domains.IServices;
using BackEnd.Domains.Models;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class CuestionarioService : ICuestionarioService
    {
        private readonly ICuestionarioRepository _cuestionarioRepository;

        public CuestionarioService(ICuestionarioRepository cuestionarioRepository)
        {
            _cuestionarioRepository = cuestionarioRepository;
        }

        public async Task CreateCuestionario(Cuestonario cuestonario)
        {            
            await _cuestionarioRepository.CreateCuestionario(cuestonario);
        }
    }
}

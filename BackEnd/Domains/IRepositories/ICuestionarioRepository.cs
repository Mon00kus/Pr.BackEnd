using BackEnd.Domains.Models;
using System.Threading.Tasks;

namespace BackEnd.Domains.IRepositories
{
    public interface ICuestionarioRepository
    {
        Task CreateCuestionario(Cuestonario cuestonario);
    }
}

using BackEnd.Domains.Models;
using System.Threading.Tasks;

namespace BackEnd.Domains.IServices
{
    public interface ICuestionarioService
    {
        Task CreateCuestionario(Cuestonario cuestonario);
    }
}

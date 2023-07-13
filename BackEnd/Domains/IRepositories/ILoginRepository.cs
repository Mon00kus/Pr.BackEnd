using System.Threading.Tasks;

using BackEnd.Domains.Models;

namespace BackEnd.Domains.IRepositories
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}

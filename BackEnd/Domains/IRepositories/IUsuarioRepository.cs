using System.Threading.Tasks;

using BackEnd.Domains.Models;

namespace BackEnd.Domains.IRepositories
{
    public interface IUsuarioRepository
    {
        Task SaveUser(Usuario usuario);
        Task<bool> ValidateExistence(Usuario usuario);
        Task<Usuario> ValidatePassword(int IdUsuario, string passwordAnterior);
        Task UpdatePassword(Usuario usuario);
    }
}
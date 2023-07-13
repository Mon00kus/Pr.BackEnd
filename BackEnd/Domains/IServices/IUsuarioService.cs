using BackEnd.Domains.Models;
using System.Threading.Tasks;

namespace BackEnd.Domains.IServices
{
    public interface IUsuarioService
    {
        Task SaveUser(Usuario usuario);
        Task<bool> ValidateExistence(Usuario usuario);
        Task<Usuario> ValidatePassword(int IdUsuario, string passwordAnterior);
        Task UpdatePassword(Usuario usuario);
    }
}

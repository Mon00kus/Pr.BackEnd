using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using BackEnd.Domains.IServices;
using BackEnd.Domains.Models;
using BackEnd.Utils;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // GET: api/<DefaultController>
        [HttpGet]
        public string Get()
        {
            return "... Aplicación corriendo Login ...";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Usuario usuario)
        {
            try
            {
                usuario.PassWord = Encriptar.EncriptarPassword(usuario.PassWord);
                var user = await _loginService.ValidateUser(usuario); // devuelve un usuario si 
                if (user == null)
                {
                  return BadRequest(new { message = "Usuario ó contraseña invalidos" });
                }
                return Ok(new { usuario = user });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);                     
            }
        }
    }
}

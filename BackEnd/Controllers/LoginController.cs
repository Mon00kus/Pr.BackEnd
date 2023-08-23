using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using BackEnd.Domains.IServices;
using BackEnd.Domains.Models;
using BackEnd.Utils;
using Microsoft.Extensions.Configuration;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
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
                var user = await _loginService.ValidateUser(usuario); // devuelve un usuario si existe

                if (user == null)
                {
                  return BadRequest(new { message = "Usuario ó contraseña invalidos" });
                }

                var tokenString = JwtConfigurator.GetToken(user, _configuration);
                return Ok(new { token = tokenString });                
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);                     
            }
        }
    }
}

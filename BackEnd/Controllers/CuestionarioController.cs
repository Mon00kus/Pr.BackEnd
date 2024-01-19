using System;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Domains.IServices;
using BackEnd.Utils;
using System.Security.Claims;
using BackEnd.Domains.Models;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {
        private readonly ICuestionarioService _cuestionarioService;

        public CuestionarioController(ICuestionarioService cuestionarioService)
        {
            _cuestionarioService = cuestionarioService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody]Cuestonario cuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.getTokenIdUser(identity);
                cuestionario.UsuarioId = idUsuario;
                cuestionario.Activo = 1;
                cuestionario.FechaCreacion = DateTime.Now;
                await _cuestionarioService.CreateCuestionario(cuestionario);
                return Ok(new { message = "Se agrego el cuestionario exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

﻿using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using BackEnd.Domains.IServices;
using BackEnd.Domains.Models;
using BackEnd.DTO;
using BackEnd.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService )
        {
            _usuarioService = usuarioService;
        }

        // GET: api/<DefaultController>
        [HttpGet]
        public string Get()
        {
            return "... Aplicación corriendo Usuario ...";
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] Usuario usuario)
        {
            try
            {
                var validateExistence = await _usuarioService.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = "El usuario " + usuario.NombreUsuario + " ya existe!!" });
                }
                usuario.PassWord = Encriptar.EncriptarPassword(usuario.PassWord);
                await _usuarioService.SaveUser(usuario);
                return Ok(new { message = "Usuario registrado con exito!!" });               
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CambiarPassword")]  //localhost:port/api/Usuario/CambiarPassword    
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPasswordDTO)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.getTokenIdUser(identity);
                string passwordEncryp = Encriptar.EncriptarPassword(cambiarPasswordDTO.passwordAnterior);
                var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncryp);
                if (usuario == null)
                {
                    return BadRequest(new { message = "La password es Incorrecta!!" });
                }
                usuario.PassWord = Encriptar.EncriptarPassword(cambiarPasswordDTO.nuevaPassword);
                await _usuarioService.UpdatePassword(usuario);
                return Ok(new {  Message = "La password fué actualizada con éxito!!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }    
}
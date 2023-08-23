using BackEnd.Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Utils
{
    public class JwtConfigurator
    {
        public static string GetToken(Usuario usuario, IConfiguration configuration)
        {
            string secretKey = configuration["JWT:SecretKey"];
            string issuer = configuration["JWT:Issuer"];
            string audience = configuration["JWT:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.NombreUsuario),
                new Claim("idUsuario", usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials : credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static int getTokenIdUser(ClaimsIdentity Identity)
        {
            if (Identity != null)
            {
                IEnumerable<Claim> claims = Identity.Claims;  
                foreach (Claim claim in claims)
                {
                    if (claim.Type == "idUsuario")
                    {
                      return int.Parse(claim.Value);
                    }
                }
            }
            return 0;
        }
    }    
}

﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiAuth.DTO;
using WebApiAuth.Models;
using static WebApiAuth.Repositories.IUsuario;

namespace WebApiAuth.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] Usuario model, [FromServices]IUsuarioRepository repository)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            repository.Create(model);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Read([FromBody] UsuarioDTO model, [FromServices]IUsuarioRepository repository)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Usuario usuario = repository.Read(model.Email, model.Senha);

            if (usuario == null)
            {
                return Unauthorized();
            }

            usuario.Senha = "";
            return Ok(new {
                usuario = usuario,
                token = GenerateToken(usuario)
            });
        }

        private string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("UmTokenMuitoGrandeEDiferenteParaNinguemDescobrir");

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

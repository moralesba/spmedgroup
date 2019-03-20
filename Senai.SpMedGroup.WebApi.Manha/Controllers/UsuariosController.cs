using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using Senai.SpMedGroup.WebApi.Manha.Repositorios;

namespace Senai.SpMedGroup.WebApi.Manha.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IUsuarioRepositorio UsuarioRepositorio { get; set; }

        public UsuariosController()
        {
            UsuarioRepositorio = new UsuarioRepositorio();
        }

        [Authorize (Roles = "administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(UsuarioRepositorio.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            try
            {
                UsuarioRepositorio.Cadastrar(usuario);

                return Ok(new
                {
                    mensagem = "Usuário Cadastrado"
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "administrador, medico")]
        public IActionResult Alterar(Usuario usuario)
        {
            try
            {
                UsuarioRepositorio.Alterar(usuario);
                return Ok(UsuarioRepositorio.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Remove(ctx.Usuario.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }

        // Lista um único Usuario especifico
        [Authorize(Roles = "administrador")]
        [HttpGet("{usuarioId}")]
        public IActionResult GetUsuario(int usuarioId)
        {
            try
            {
                Usuario usuarioBuscado = UsuarioRepositorio.BuscarUsuario(usuarioId);

                if (usuarioBuscado == null)
                {
                    return NotFound(new { mensagem = "Usuário não encontrado" });
                }

                return Ok(usuarioBuscado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
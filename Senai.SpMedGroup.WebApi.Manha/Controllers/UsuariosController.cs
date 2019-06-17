using System;
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

        [Authorize (Roles = "Administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(UsuarioRepositorio.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrador")]
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
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrador, Professor")]
        [HttpPut]
        public IActionResult Alterar(Usuario usuario)
        {
            try
            {
                UsuarioRepositorio.Alterar(usuario);
                return Ok(new { mensagem = "Usuario Alterado" });
            }

            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Remove(ctx.Usuario.Find(id));
                ctx.SaveChanges();
            }
            return Ok((new { mensagem = "Usuario Deletado" }));
        }

        // Lista um único Usuario especifico
        [Authorize(Roles = "Administrador")]
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using Senai.SpMedGroup.WebApi.Manha.Repositorios;

namespace Senai.SpMedGroup.WebApi.Manha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProntuariosController : ControllerBase
    {
        public IProntuarioRepositorio ProntuarioRepositorio { get; set; }

        public ProntuariosController()
        {
            ProntuarioRepositorio = new ProntuarioRepositorio();
        }

        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ProntuarioRepositorio.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(Prontuario prontuario)
        {
            try
            {
                ProntuarioRepositorio.Cadastrar(prontuario);

                return Ok(new
                {
                    mensagem = "Prontuario Cadastrado"
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "administrador, medico")]
        public IActionResult Alterar(Prontuario prontuario)
        {
            try
            {
                ProntuarioRepositorio.Alterar(prontuario);
                return Ok(ProntuarioRepositorio.Listar());
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
                ctx.Prontuario.Remove(ctx.Prontuario.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }

        [Authorize(Roles = "administrador, cliente")]
        [HttpGet("{prontuarioId}")]
        public IActionResult Get(int prontuarioId)
        {
            try
            {
                Prontuario prontuarioBuscado = ProntuarioRepositorio.BuscarProntuario(prontuarioId);

                if (prontuarioBuscado == null)
                {
                    return NotFound(new { mensagem = "Prontuario não encontrado" });
                }

                return Ok(prontuarioBuscado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
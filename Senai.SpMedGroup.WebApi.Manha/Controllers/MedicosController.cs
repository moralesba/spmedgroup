using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using Senai.SpMedGroup.WebApi.Manha.Repositorios;

namespace Senai.SpMedGroup.WebApi.Manha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private IMedicoRepositorio MedicoRepositorio { get; set; }

        public MedicosController()
        {
            MedicoRepositorio = new MedicoRepositorio();
        }

        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(MedicoRepositorio.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(Medico medico)
        {
            try
            {
                MedicoRepositorio.Cadastrar(medico);
                return Ok(new
                {
                    mensagem = "Medico Cadastrada"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "administrador, medico")]
        public IActionResult Alterar(Medico medico)
        {
            try
            {
                MedicoRepositorio.Alterar(medico);
                return Ok(MedicoRepositorio.Listar());
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
                ctx.Medico.Remove(ctx.Medico.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }

        [Authorize(Roles = "administrador, medico")]
        [HttpGet("{medicoId}")]
        public IActionResult Get(int medicoId)
        {
            try
            {
                Medico medicoBuscado = MedicoRepositorio.BuscarMedico(medicoId);

                if (medicoBuscado == null)
                {
                    return NotFound(new { mensagem = "Medico não encotrado" });
                }

                return Ok(medicoBuscado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
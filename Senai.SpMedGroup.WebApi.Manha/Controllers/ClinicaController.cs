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
    [Produces ("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        private IClinicaRepositorio ClinicaRepositorio { get; set; }

        public ClinicaController()
        {
            ClinicaRepositorio = new ClinicaRepositorio();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ClinicaRepositorio.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(Clinica clinica)
        {
            try
            {
                ClinicaRepositorio.Cadastrar(clinica);

                return Ok(new
                {
                    mensagem = "Clinica Cadastrada"
                });
            }
            catch
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
                ctx.Clinica.Remove(ctx.Clinica.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }

    }
}
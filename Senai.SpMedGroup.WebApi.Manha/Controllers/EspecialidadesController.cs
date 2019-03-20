﻿using System;
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
    public class EspecialidadesController : ControllerBase
    {
        private IEspecialidadeRepositorio EspecialidadeRepositorio { get; set; }

        public EspecialidadesController()
        {
            EspecialidadeRepositorio = new EspecialidadeRepositorio();
        }

        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(EspecialidadeRepositorio.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(Especialidade especialidade)
        {
            try
            {
                EspecialidadeRepositorio.Cadastrar(especialidade);

                return Ok(new
                {
                    mensagem = "Especialidade Cadastrada"
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
                ctx.Especialidade.Remove(ctx.Especialidade.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
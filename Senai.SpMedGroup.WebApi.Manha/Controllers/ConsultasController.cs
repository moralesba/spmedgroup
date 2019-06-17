using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using Senai.SpMedGroup.WebApi.Manha.Repositorios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Senai.SpMedGroup.WebApi.Manha.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {

        private IConsultaRepositorio ConsultaRepositorio { get; set; }

        public ConsultasController()
        {
            ConsultaRepositorio = new ConsultaRepositorio();
        }

        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                int idrecebido = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
                string tipousuario = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value.ToString();

                List<Consulta> listaConsultas = ConsultaRepositorio.Listar(idrecebido, tipousuario);
                return Ok(listaConsultas);
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(Consulta consulta)
        {
            try
            {
                ConsultaRepositorio.Cadastrar(consulta);
                return Ok(new
                {
                    mensagem = "Consulta Cadastrada"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
          //return Ok(new { mensagem = "Consulta Alterada" });
             
        [Authorize(Roles = "Administrador")]
        [HttpPut]
        public IActionResult Alterar(Consulta consulta)
        {
            try
            {
                ConsultaRepositorio.Alterar(consulta);
                return Ok(new { mensagem = "Consulta Alterada" });
            }

            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        //Deleta uma consulta
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Consulta.Remove(ctx.Consulta.Find(id));
                ctx.SaveChanges();
            }
            return Ok();
        }

        //Lista uma consulta especifica
        [Authorize(Roles = "Administrador")]
        [HttpGet("{consultaId}")]
        public IActionResult GetConsulta(int consultaId)
        {
            try
            {
                List<Consulta> consultaBuscada = ConsultaRepositorio.BuscarConsulta(consultaId);

                if (consultaBuscada == null)
                {
                    return NotFound(new { mensagem = "Consulta não encontrada!" });
                }

                return Ok(consultaBuscada);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Lista as consultas referentes a um medico
        //[Authorize(Roles = "Administrador, Medico")]
        //[HttpGet("{medicoId}")]
        //public IActionResult GetConsultasMedico(int medicoId)
        //{
        //    try
        //    {
        //        MedicoRepositorio medicosRep = new MedicoRepositorio();
        //        Medico medicoBuscado = medicosRep.BuscarMedico(medicoId);

        //        if (medicoBuscado == null)
        //        {
        //            return NotFound(new { mensagem = "Medico não encotrado" });
        //        }
                
        //        List<Consulta> consultasMedico = ConsultaRepositorio.BuscarConsultasMedico(medicoId);

        //        if (consultasMedico == null)
        //        {
        //            return NotFound(new { mensagem = "Não foi possivel encontrar consultas referentes a esse medico." });
        //        }
        //        else if (consultasMedico.Count() == 0)
        //        {
        //            return Ok(new { mensagem = "Nenhuma consulta agendada." });
        //        }
        //        return Ok(consultasMedico);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}

        //Lista as consultas referentes a um paciente
        //[Authorize(Roles = "Administrador, Paciente")]
        //[HttpGet("{usuarioId}")]
        //public IActionResult GetConsultasDePaciente(int usuarioId)
        //{
        //    try
        //    { 
        //        UsuarioRepositorio usuarioRep = new UsuarioRepositorio();
        //        Usuario usuarioBuscado = usuarioRep.BuscarUsuario(usuarioId);

        //        if (usuarioBuscado == null)
        //        {
        //            return NotFound(new { mensagem = "Paciente não encontrado" });
        //        }
                
        //        List<Consulta> consultasPaciente = ConsultaRepositorio.BuscarConsultasPaciente(usuarioId);

        //        if (consultasPaciente == null)
        //        {
        //            return NotFound(new { mensagem = "Não foi possivel encontrar consultas referentes a esse paciente" });
        //        }
        //        else if (consultasPaciente.Count() == 0)
        //        {
        //            return Ok(new { mensagem = "Nenhuma consulta agendada" });
        //        }

        //        return Ok(consultasPaciente);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}

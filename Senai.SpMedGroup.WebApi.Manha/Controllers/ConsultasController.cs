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

        //Lista todas as consultas
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ConsultaRepositorio.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Cadastra uma nova consulta
        [Authorize(Roles = "administrador")]
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
                return BadRequest();
            }
        }

        //Altera uma consulta
        [HttpPut]
        [Authorize(Roles = "administrador, medico")]
        public IActionResult Alterar(Consulta consulta)
        {
            try
            {
                ConsultaRepositorio.Alterar(consulta);
                return Ok(ConsultaRepositorio.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //Deleta uma consulta
        [Authorize(Roles = "administrador")]
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
        [Authorize(Roles = "administrador")]
        [HttpGet("{consultaId}")]
        public IActionResult GetConsulta(int consultaId)
        {
            try
            {
                Consulta consultaBuscada = ConsultaRepositorio.BuscarConsulta(consultaId);

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
        [Authorize(Roles = "administrador, medico")]
        [HttpGet("{medicoId}")]
        public IActionResult GetConsultasMedico(int medicoId)
        {
            try
            {
                MedicoRepositorio medicosRep = new MedicoRepositorio();
                Medico medicoBuscado = medicosRep.BuscarMedico(medicoId);

                if (medicoBuscado == null)
                {
                    return NotFound(new { mensagem = "Medico não encotrado" });
                }
                
                List<Consulta> consultasMedico = ConsultaRepositorio.BuscarConsultasMedico(medicoId);

                if (consultasMedico == null)
                {
                    return NotFound(new { mensagem = "Não foi possivel encontrar consultas referentes a esse medico." });
                }
                else if (consultasMedico.Count() == 0)
                {
                    return Ok(new { mensagem = "Nenhuma consulta agendada." });
                }
                return Ok(consultasMedico);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Lista as consultas referentes a um paciente
        [Authorize(Roles = "administrador, cliente")]
        [HttpGet("{usuarioId}")]
        public IActionResult GetConsultasDePaciente(int prontuarioId)
        {
            try
            { 
                ProntuarioRepositorio prontuariosRep = new ProntuarioRepositorio();
                Prontuario prontuarioBuscado = prontuariosRep.BuscarProntuario(prontuarioId);

                if (prontuarioBuscado == null)
                {
                    return NotFound(new { mensagem = "Paciente não encontrado" });
                }
                
                List<Consulta> consultasPaciente = ConsultaRepositorio.BuscarConsultasPaciente(prontuarioId);

                if (consultasPaciente == null)
                {
                    return NotFound(new { mensagem = "Não foi possivel encontrar consultas referentes a esse paciente" });
                }
                else if (consultasPaciente.Count() == 0)
                {
                    return Ok(new { mensagem = "Nenhuma consulta agendada" });
                }

                return Ok(consultasPaciente);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

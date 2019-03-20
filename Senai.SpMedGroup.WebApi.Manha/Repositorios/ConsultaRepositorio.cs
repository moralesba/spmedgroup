using Microsoft.EntityFrameworkCore;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Senai.SpMedGroup.WebApi.Manha.Repositorios
{
    public class ConsultaRepositorio : IConsultaRepositorio
    {
        public void Cadastrar(Consulta consulta)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Consulta.Add(consulta);
                ctx.SaveChanges();
            }
        }

        public List<Consulta> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Consulta.ToList();
            }
        }

        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Consulta.Remove(ctx.Consulta.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Consulta consulta)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Consulta consultaExiste = ctx.Consulta.Find(consulta.IdConsulta);

                consultaExiste.IdConsulta = consulta.IdConsulta;
                ctx.Consulta.Update(consulta);
                ctx.SaveChanges();
            }
        }

        public Consulta BuscarConsulta(int consultaId)
        {
            Consulta consultaBuscada = new Consulta();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                consultaBuscada = ctx.Consulta.ToList().Find(c => c.IdConsulta == consultaId);
            }

            return consultaBuscada;
        }

        public List<Consulta> BuscarConsultasMedico(int medicoId)
        {
            List<Consulta> consultasMedico = new List<Consulta>();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                consultasMedico = ctx.Consulta.ToList().FindAll(c => c.IdMedico == medicoId);
            }

            return consultasMedico;
        }

        public List<Consulta> BuscarConsultasPaciente(int prontuarioId)
        {
            List<Consulta> consultasPaciente = new List<Consulta>();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                consultasPaciente = ctx.Consulta.ToList().FindAll(c => c.IdProntuario == prontuarioId);
            }

            return consultasPaciente;
        }
    }
}

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

        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Consulta.Remove(ctx.Consulta.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Consulta consulta, int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Consulta consultaExiste = ctx.Consulta.Find(consulta.IdConsulta);

                consultaExiste.IdConsulta = consulta.IdConsulta;
                ctx.Consulta.Update(consultaExiste);
                ctx.SaveChanges();
            }
        }


        public void Alterar(Consulta consulta)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Consulta consultaExiste = ctx.Consulta.Find(consulta.IdConsulta);

                if
                (consultaExiste.IdConsulta == consulta.IdConsulta)
                { 
                    consultaExiste.DtConsulta = consulta.DtConsulta;
                    consultaExiste.IdMedico = consulta.IdMedico;
                    consultaExiste.IdUsuario = consulta.IdUsuario;
                    consultaExiste.Descricao = consulta.Descricao;
                    consultaExiste.Situacao = consulta.Situacao;
                    consultaExiste.IdMedicoNavigation = consulta.IdMedicoNavigation;
                    consultaExiste.IdProntuarioNavigation = consulta.IdProntuarioNavigation;

                    ctx.Consulta.Update(consultaExiste);
                    ctx.SaveChanges();
                }
            }
        }

        public List<Consulta> BuscarConsulta(int consultaId)
        {
            List<Consulta> consultaBuscada = new List<Consulta>();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                consultaBuscada = ctx.Consulta.ToList().FindAll(c => c.IdConsulta == consultaId);
                consultaBuscada = ctx.Consulta.Include(C => C.IdMedicoNavigation).ToList();
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

        public List<Consulta> BuscarConsultasPaciente(int usuarioId)
        {
            List<Consulta> consultasPaciente = new List<Consulta>();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                consultasPaciente = ctx.Consulta.ToList().FindAll(c => c.IdUsuario == usuarioId);
                consultasPaciente = ctx.Consulta.Include(C => C.IdMedicoNavigation).ToList();
            }

            return consultasPaciente;
        }

        public List<Consulta> Listar(int idrecebido, string tipousuario)
        {
            List<Consulta> listaConsultaBuscada = new List<Consulta>();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                if (tipousuario == "Administrador")
                {
                    listaConsultaBuscada = ctx.Consulta.Include(C => C.IdMedicoNavigation).ToList();
                }
                else if (tipousuario == "Medico")
                {
                    Medico medicoBuscado = ctx.Medico.ToList().Find(c => c.IdUsuario == idrecebido);
                    listaConsultaBuscada = ctx.Consulta.Where(c => c.IdMedico == medicoBuscado.IdMedico).ToList();
                }
                else
                {
                    Usuario usuarioBuscado = ctx.Usuario.ToList().Find(c => c.IdUsuario == idrecebido);
                    listaConsultaBuscada = ctx.Consulta.Where(c => c.IdUsuario == usuarioBuscado.IdUsuario).Include(X => X.IdMedicoNavigation).ToList();
                }

                return listaConsultaBuscada;
            }
        }
    }
}

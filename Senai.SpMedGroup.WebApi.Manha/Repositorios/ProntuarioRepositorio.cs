using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Repositorios
{
    public class ProntuarioRepositorio : IProntuarioRepositorio
    {
        public List<Prontuario> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Prontuario.ToList();
            }
        }

        public void Cadastrar(Prontuario prontuario)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Prontuario.Add(prontuario);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Prontuario.Remove(ctx.Prontuario.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Prontuario prontuario)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Prontuario prontuarioExiste = ctx.Prontuario.Find(prontuario.IdProntuario);

                prontuarioExiste.IdProntuario = prontuario.IdProntuario;
                ctx.Prontuario.Update(prontuario);
                ctx.SaveChanges();
            }
        }

        public Prontuario BuscarProntuario(int prontuarioId)
        {
            Prontuario prontuario = new Prontuario();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                prontuario = ctx.Prontuario.Find(prontuarioId);
            }

            return prontuario;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Repositorios
{
    public class MedicoRepositorio : IMedicoRepositorio
    {
        public void Cadastrar(Medico medico)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Medico.Add(medico);
                ctx.SaveChanges();
            }
        }

        public List<Medico> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Medico.ToList();
            }
        }

        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Medico.Remove(ctx.Medico.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Medico medico)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Medico medicoExiste = ctx.Medico.Find(medico.IdMedico);

                medicoExiste.IdMedico = medico.IdMedico;
                ctx.Medico.Update(medico);
                ctx.SaveChanges();
            }
        }

        public Medico BuscarMedico(int medicoId)
        {
            Medico medico = new Medico();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                medico = ctx.Medico.Find(medicoId);
            }

            return medico;
        }


    }
}

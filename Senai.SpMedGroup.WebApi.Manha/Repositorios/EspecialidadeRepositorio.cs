using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Repositorios
{
    public class EspecialidadeRepositorio : IEspecialidadeRepositorio
    {
        public void Cadastrar(Especialidade especialidade)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Especialidade.Add(especialidade);
                ctx.SaveChanges();
            }
        }

        public List<Especialidade> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Especialidade.ToList();
            }
        }

        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Especialidade.Remove(ctx.Especialidade.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Especialidade especialidade)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Especialidade especialidadeExiste = ctx.Especialidade.Find(especialidade.IdEspecialidade);

                especialidadeExiste.IdEspecialidade = especialidade.IdEspecialidade;
                ctx.Especialidade.Update(especialidade);
                ctx.SaveChanges();
            }
        }
    }
}

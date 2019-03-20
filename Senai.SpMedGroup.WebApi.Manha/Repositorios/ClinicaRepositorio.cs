using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Senai.SpMedGroup.WebApi.Manha.Repositorios
{
    public class ClinicaRepositorio : IClinicaRepositorio
    {
        public List<Clinica> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Clinica.ToList();
            }
        }

        public void Cadastrar(Clinica clinica)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Clinica.Add(clinica);
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

        public void Alterar(Clinica clinica)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Clinica clinicaExiste = ctx.Clinica.Find(clinica.IdClinica);

                clinicaExiste.IdClinica = clinica.IdClinica;
                ctx.Clinica.Update(clinica);
                ctx.SaveChanges();
            }
        }
    }
}

using Senai.SpMedGroup.WebApi.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Interfaces
{
    public interface IClinicaRepositorio
    {
        void Cadastrar(Clinica clinica);
        void Alterar(Clinica clinica);
        void Deletar(int id);
        List<Clinica> Listar();
    }
}

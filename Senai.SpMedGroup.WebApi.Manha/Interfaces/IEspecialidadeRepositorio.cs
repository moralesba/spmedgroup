using Senai.SpMedGroup.WebApi.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Interfaces
{
    interface IEspecialidadeRepositorio
    {
        List<Especialidade> Listar();
        void Cadastrar(Especialidade especialidade);
        void Alterar(Especialidade especialidade);
        void Deletar(int id);
    }
}

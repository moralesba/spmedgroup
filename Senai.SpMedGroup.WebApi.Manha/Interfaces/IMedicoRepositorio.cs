using Senai.SpMedGroup.WebApi.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Interfaces
{
    interface IMedicoRepositorio
    {
        List<Medico> Listar();
        void Cadastrar(Medico medico);
        void Alterar(Medico medico);
        void Deletar(int id);
        Medico BuscarMedico(int medicoId);
    }
}

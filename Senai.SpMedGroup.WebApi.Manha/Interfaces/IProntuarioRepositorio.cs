using Senai.SpMedGroup.WebApi.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Interfaces
{
    public interface IProntuarioRepositorio
    {
        List<Prontuario> Listar();
        void Cadastrar(Prontuario prontuario);
        void Alterar(Prontuario prontuario);
        void Deletar(int id);
        Prontuario BuscarProntuario(int prontuarioId);
    }
}

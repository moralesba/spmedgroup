﻿using Senai.SpMedGroup.WebApi.Manha.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SpMedGroup.WebApi.Manha.Interfaces
{
    public interface IConsultaRepositorio
    {
        List<Consulta> Listar(int idrecebido, string tipousuario);
        void Cadastrar(Consulta consulta);
        void Deletar(int id);
        void Alterar(Consulta consulta);
        List<Consulta> BuscarConsulta(int consultaId);
        List<Consulta> BuscarConsultasMedico(int medicoId);
        List<Consulta> BuscarConsultasPaciente(int prontuarioId);
    }
}

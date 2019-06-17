using System;
using System.Collections.Generic;

namespace Senai.SpMedGroup.WebApi.Manha.Domains
{
    public partial class Consulta
    {
        public int IdConsulta { get; set; }
        public DateTime? DtConsulta { get; set; }
        public int? IdMedico { get; set; }
        public int? IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }

        public Medico IdMedicoNavigation { get; set; }
        public Prontuario IdProntuarioNavigation { get; set; }
    }
}
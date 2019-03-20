using System;
using System.Collections.Generic;

namespace Senai.SpMedGroup.WebApi.Manha.Domains
{
    public partial class Prontuario
    {
        public Prontuario()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int IdProntuario { get; set; }
        public string RgPaciente { get; set; }
        public string CpfPaciente { get; set; }
        public string EndPaciente { get; set; }
        public DateTime? DtnascPaciente { get; set; }
        public string CelPaciente { get; set; }

        public ICollection<Consulta> Consulta { get; set; }
    }
}

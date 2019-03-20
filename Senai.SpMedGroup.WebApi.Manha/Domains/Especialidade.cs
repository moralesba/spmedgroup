using System.Collections.Generic;

namespace Senai.SpMedGroup.WebApi.Manha.Domains
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            Medico = new HashSet<Medico>();
        }

        public int IdEspecialidade { get; set; }
        public string Nome { get; set; }

        public ICollection<Medico> Medico { get; set; }
    }
}

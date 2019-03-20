using System.Collections.Generic;

namespace Senai.SpMedGroup.WebApi.Manha.Domains
{
    public partial class Clinica
    {
        public Clinica()
        {
            Medico = new HashSet<Medico>();
        }

        public int IdClinica { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
        public int Horario { get; set; }

        public ICollection<Medico> Medico { get; set; }
    }
}

using System.Collections.Generic;

namespace Senai.SpMedGroup.WebApi.Manha.Domains
{
    public partial class Usuario
    {
        internal int IdTipoUsuario;

        public Usuario()
        {
            Medico = new HashSet<Medico>();
        }

        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? TipoUsuario { get; set; }

        public TipoUsuario TipoUsuarioNavigation { get; set; }
        public ICollection<Medico> Medico { get; set; }
        public ICollection<Prontuario> Prontuarios { get; set; }
    }
}

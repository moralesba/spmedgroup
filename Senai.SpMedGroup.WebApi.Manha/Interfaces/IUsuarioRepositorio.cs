using Senai.SpMedGroup.WebApi.Manha.Domains;
using System.Collections.Generic;

namespace Senai.SpMedGroup.WebApi.Manha.Interfaces
{
    public interface IUsuarioRepositorio
    {
        List<Usuario> Listar();
        Usuario BuscarEmailSenha(string email, string senha);
        Usuario BuscarUsuario(int usuarioId);
        void Cadastrar(Usuario usuario);
        void Alterar(Usuario usuario);
        void Deletar(int id);
    }
}

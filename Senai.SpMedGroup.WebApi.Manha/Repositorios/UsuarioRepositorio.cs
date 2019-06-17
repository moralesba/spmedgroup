using Microsoft.EntityFrameworkCore;
using Senai.SpMedGroup.WebApi.Manha.Context;
using Senai.SpMedGroup.WebApi.Manha.Domains;
using Senai.SpMedGroup.WebApi.Manha.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Senai.SpMedGroup.WebApi.Manha.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog = SpMedGroup; user id=sa; pwd=132";

        public List<Usuario> Listar()
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                return ctx.Usuario.Include(C => C.TipoUsuarioNavigation).ToList();
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Add(usuario);
                ctx.SaveChanges();
            }
        }

        public Usuario BuscarEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // você retorna somente a tabela de usuarios
                string QuerySelect = "  select u.id_usuario, u.email, u.senha, u.tipo_usuario, t.id_tipousuario, t.nome as nometipousuario from usuario u inner join tipo_usuario t on u.tipo_usuario = t.id_tipousuario where email = @email and senha = @senha"; 
                // alem da tabela de usuarios, tipos_usuarios

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        Usuario usuario = new Usuario();

                        while (sdr.Read())
                        {
                            usuario.IdUsuario = Convert.ToInt32(sdr["id_usuario"]);
                            usuario.Email = sdr["email"].ToString();
                            usuario.TipoUsuarioNavigation = new TipoUsuario();
                            usuario.TipoUsuarioNavigation.Nome = sdr["nometipousuario"].ToString();
                        }
                        return usuario;
                    }
                }
                return null; 
            }
        }
        public void Deletar(int id)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                ctx.Usuario.Remove(ctx.Usuario.Find(id));
                ctx.SaveChanges();
            }
        }

        public void Alterar(Usuario usuario)
        {
            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                Usuario usuarioExiste = ctx.Usuario.Find(usuario.IdUsuario);

                if
                (usuarioExiste.IdUsuario == usuario.IdUsuario)
                {
                    usuarioExiste.Nome = usuario.Nome;
                    usuarioExiste.Email = usuario.Email;
                    usuarioExiste.Senha = usuario.Senha;
                    usuarioExiste.TipoUsuario = usuario.TipoUsuario;
                    usuarioExiste.TipoUsuarioNavigation = usuario.TipoUsuarioNavigation;

                    ctx.Usuario.Update(usuarioExiste);
                    ctx.SaveChanges();
                }
            }
        }

        public Usuario BuscarUsuario(int usuarioId)
        {
            Usuario usuarioBuscado = new Usuario();

            using (SpMedGroupContext ctx = new SpMedGroupContext())
            {
                usuarioBuscado = ctx.Usuario.Find(usuarioId);
            }

            return usuarioBuscado;
        }
    }
}



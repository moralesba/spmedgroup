using Microsoft.EntityFrameworkCore;
using Senai.SpMedGroup.WebApi.Manha.Domains;

namespace Senai.SpMedGroup.WebApi.Manha.Context
{
    public partial class SpMedGroupContext : DbContext
    {
        public SpMedGroupContext()
        {
        }

        public SpMedGroupContext(DbContextOptions<SpMedGroupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clinica> Clinica { get; set; }
        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<Especialidade> Especialidade { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Prontuario> Prontuario { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress; Initial Catalog = SpMedGroup; user id = sa; pwd = 132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clinica>(entity =>
            {
                entity.HasKey(e => e.IdClinica);

                entity.ToTable("clinica");

                entity.Property(e => e.IdClinica).HasColumnName("id_clinica");

                entity.Property(e => e.Endereco)
                    .HasColumnName("endereco")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasColumnName("telefone")
                    .HasMaxLength(205)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(e => e.IdConsulta);

                entity.ToTable("consulta");

                entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");

                entity.Property(e => e.Descrição)
                    .HasColumnName("descrição")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.DtConsulta)
                    .HasColumnName("dt_consulta")
                    .HasColumnType("date");

                entity.Property(e => e.IdMedico).HasColumnName("id_medico");

                entity.Property(e => e.IdProntuario).HasColumnName("id_prontuario");

                entity.Property(e => e.Situação)
                    .HasColumnName("situação")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdMedico)
                    .HasConstraintName("FK__consulta__id_med__49C3F6B7");

                entity.HasOne(d => d.IdProntuarioNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdProntuario)
                    .HasConstraintName("FK__consulta__id_pro__4AB81AF0");
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidade);

                entity.ToTable("especialidade");

                entity.Property(e => e.IdEspecialidade).HasColumnName("id_especialidade");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(205)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.IdMedico);

                entity.ToTable("medico");

                entity.Property(e => e.IdMedico).HasColumnName("id_medico");

                entity.Property(e => e.Crm)
                    .HasColumnName("crm")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.IdClinica).HasColumnName("id_clinica");

                entity.Property(e => e.IdEspecialidade).HasColumnName("id_especialidade");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClinicaNavigation)
                    .WithMany(p => p.Medico)
                    .HasForeignKey(d => d.IdClinica)
                    .HasConstraintName("FK__medico__id_clini__4316F928");

                entity.HasOne(d => d.IdEspecialidadeNavigation)
                    .WithMany(p => p.Medico)
                    .HasForeignKey(d => d.IdEspecialidade)
                    .HasConstraintName("FK__medico__id_espec__440B1D61");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Medico)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__medico__id_usuar__44FF419A");
            });

            modelBuilder.Entity<Prontuario>(entity =>
            {
                entity.HasKey(e => e.IdProntuario);

                entity.ToTable("prontuario");

                entity.Property(e => e.IdProntuario).HasColumnName("id_prontuario");

                entity.Property(e => e.CelPaciente)
                    .HasColumnName("cel_paciente")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.CpfPaciente)
                    .HasColumnName("cpf_paciente")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DtnascPaciente)
                    .HasColumnName("dtnasc_paciente")
                    .HasColumnType("date");

                entity.Property(e => e.EndPaciente)
                    .HasColumnName("end_paciente")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.RgPaciente)
                    .HasColumnName("rg_paciente")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario);

                entity.ToTable("tipo_usuario");

                entity.Property(e => e.IdTipoUsuario).HasColumnName("id_tipousuario");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(205)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .HasColumnName("senha")
                    .HasMaxLength(205)
                    .IsUnicode(false);

                entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");

                entity.HasOne(d => d.TipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuario)
                    .HasConstraintName("FK__usuario__tipo_us__3D5E1FD2");
            });
        }
    }
}

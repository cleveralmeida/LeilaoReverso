using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LR.Models
{
    public partial class LeilaoReversoContext : DbContext
    {
        public LeilaoReversoContext()
        {
        }

        public LeilaoReversoContext(DbContextOptions<LeilaoReversoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Avaliacao> Avaliacao { get; set; }
        public virtual DbSet<CompradorFornecedor> CompradorFornecedor { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<DeviceCodes> DeviceCodes { get; set; }
        public virtual DbSet<FornecedorLeilao> FornecedorLeilao { get; set; }
        public virtual DbSet<FornecedorLeilaoLance> FornecedorLeilaoLance { get; set; }
        public virtual DbSet<Instituicao> Instituicao { get; set; }
        public virtual DbSet<Leilao> Leilao { get; set; }
        public virtual DbSet<NivelEnsino> NivelEnsino { get; set; }
        public virtual DbSet<PersistedGrants> PersistedGrants { get; set; }
        public virtual DbSet<PesoPrioridade> PesoPrioridade { get; set; }
        public virtual DbSet<PesoValorLeilao> PesoValorLeilao { get; set; }
        public virtual DbSet<RespostaAvaliacao> RespostaAvaliacao { get; set; }
        public virtual DbSet<StatusGame> StatusGame { get; set; }
        public virtual DbSet<TipoLeilao> TipoLeilao { get; set; }
        public virtual DbSet<UsuarioInstituicao> UsuarioInstituicao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=desktop-6jm3iq1;Initial Catalog=LeilaoReverso;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Nome)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.IdCurso)
                    .HasConstraintName("FK_AspNetUsers_Curso");

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.IdInstituicao)
                    .HasConstraintName("FK_AspNetUsers_Instituicao");

                entity.HasOne(d => d.IdNivelEnsinoNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.IdNivelEnsino)
                    .HasConstraintName("FK_AspNetUsers_NivelEnsino");

                entity.HasOne(d => d.IdStatusGameNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.IdStatusGame)
                    .HasConstraintName("FK_AspNetUsers_IdStatusGame");
            });

            modelBuilder.Entity<Avaliacao>(entity =>
            {
                entity.HasKey(e => e.IdAvalidacao);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Avaliacao)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Avaliacao_AspNetUsers");
            });

            modelBuilder.Entity<CompradorFornecedor>(entity =>
            {
                entity.HasKey(e => e.IdCompradorFornecedor);

                entity.Property(e => e.IdComprador)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdFornecedor).HasMaxLength(450);

                entity.HasOne(d => d.IdCompradorNavigation)
                    .WithMany(p => p.CompradorFornecedorIdCompradorNavigation)
                    .HasForeignKey(d => d.IdComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompradorFornecedor_AspNetUsers");

                entity.HasOne(d => d.IdFornecedorNavigation)
                    .WithMany(p => p.CompradorFornecedorIdFornecedorNavigation)
                    .HasForeignKey(d => d.IdFornecedor)
                    .HasConstraintName("FK_CompradorFornecedor_AspNetUsers1");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.IdCurso);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeviceCodes>(entity =>
            {
                entity.HasKey(e => e.UserCode);

                entity.HasIndex(e => e.DeviceCode)
                    .IsUnique();

                entity.HasIndex(e => e.Expiration);

                entity.Property(e => e.UserCode).HasMaxLength(200);

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.DeviceCode)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SubjectId).HasMaxLength(200);
            });

            modelBuilder.Entity<FornecedorLeilao>(entity =>
            {
                entity.HasKey(e => e.IdFornecedorLeilao);

                entity.Property(e => e.CustoServicoFrete).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.IdFornecedor)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Lance).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PontuacaoPeso).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdFornecedorNavigation)
                    .WithMany(p => p.FornecedorLeilao)
                    .HasForeignKey(d => d.IdFornecedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FornecedorLeilao_AspNetUsers");

                entity.HasOne(d => d.IdLeilaoNavigation)
                    .WithMany(p => p.FornecedorLeilao)
                    .HasForeignKey(d => d.IdLeilao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FornecedorLeilao_Leilao");
            });

            modelBuilder.Entity<FornecedorLeilaoLance>(entity =>
            {
                entity.HasKey(e => e.IdFornecedorLeilaoLance);

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.HasOne(d => d.IdFornecedorLeilaoNavigation)
                    .WithMany(p => p.FornecedorLeilaoLance)
                    .HasForeignKey(d => d.IdFornecedorLeilao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FornecedorLeilaoLance_FornecedorLeilao1");
            });

            modelBuilder.Entity<Instituicao>(entity =>
            {
                entity.HasKey(e => e.IdInstituicao);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Leilao>(entity =>
            {
                entity.HasKey(e => e.IdLeilao);

                entity.Property(e => e.CustoCompraEsperado).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.DataAberturaLance).HasColumnType("datetime");

                entity.Property(e => e.DataApuracao).HasColumnType("datetime");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.DataFechamento).HasColumnType("datetime");

                entity.Property(e => e.DataFechamentoRodada1).HasColumnType("datetime");

                entity.Property(e => e.DataFechamentoRodada2).HasColumnType("datetime");

                entity.Property(e => e.DataFechamentoRodada3).HasColumnType("datetime");

                entity.Property(e => e.IdComprador).HasMaxLength(450);

                entity.Property(e => e.JustificativaCancelamento)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Requisicao)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ValorIncremento).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ValorInicial).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ValorMaximo).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IdCompradorNavigation)
                    .WithMany(p => p.Leilao)
                    .HasForeignKey(d => d.IdComprador)
                    .HasConstraintName("FK_Leilao_AspNetUsers");

                entity.HasOne(d => d.IdTipoLeilaoNavigation)
                    .WithMany(p => p.Leilao)
                    .HasForeignKey(d => d.IdTipoLeilao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Leilao_TipoLeilao");
            });

            modelBuilder.Entity<NivelEnsino>(entity =>
            {
                entity.HasKey(e => e.IdNivelEnsino);

                entity.Property(e => e.IdNivelEnsino).ValueGeneratedNever();

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersistedGrants>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Expiration);

                entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type });

                entity.Property(e => e.Key).HasMaxLength(200);

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.SubjectId).HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PesoPrioridade>(entity =>
            {
                entity.HasKey(e => e.IdPesoPrioridade);

                entity.Property(e => e.IdPesoPrioridade).ValueGeneratedNever();

                entity.Property(e => e.Preco).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<PesoValorLeilao>(entity =>
            {
                entity.HasKey(e => e.IdPesoValorLeilao);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Valor).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IdLeilaoNavigation)
                    .WithMany(p => p.PesoValorLeilao)
                    .HasForeignKey(d => d.IdLeilao)
                    .HasConstraintName("FK_PesoValorLeilao_Leilao");
            });

            modelBuilder.Entity<RespostaAvaliacao>(entity =>
            {
                entity.HasKey(e => e.IdRespostaAvaliacao);

                entity.HasOne(d => d.IdAvaliacaoNavigation)
                    .WithMany(p => p.RespostaAvaliacao)
                    .HasForeignKey(d => d.IdAvaliacao)
                    .HasConstraintName("FK_RespostaAvaliacao_Avaliacao");
            });

            modelBuilder.Entity<StatusGame>(entity =>
            {
                entity.HasKey(e => e.IdStatusGame)
                    .HasName("PK_IdStatusGame");

                entity.Property(e => e.IdStatusGame).ValueGeneratedNever();

                entity.Property(e => e.Descricao)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoLeilao>(entity =>
            {
                entity.HasKey(e => e.IdTipoLeilao);

                entity.HasIndex(e => e.Descricao)
                    .HasName("IX_TipoLeilao")
                    .IsUnique();

                entity.Property(e => e.IdTipoLeilao).ValueGeneratedNever();

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioInstituicao>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioInstituicao);

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.UsuarioInstituicao)
                    .HasForeignKey(d => d.IdInstituicao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioInstituicao_Instituicao");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioInstituicao)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_UsuarioInstituicao_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

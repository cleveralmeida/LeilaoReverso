using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Avaliacao = new HashSet<Avaliacao>();
            CompradorFornecedorIdCompradorNavigation = new HashSet<CompradorFornecedor>();
            CompradorFornecedorIdFornecedorNavigation = new HashSet<CompradorFornecedor>();
            FornecedorLeilao = new HashSet<FornecedorLeilao>();
            Leilao = new HashSet<Leilao>();
            UsuarioInstituicao = new HashSet<UsuarioInstituicao>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? IdInstituicao { get; set; }
        public int? IdCurso { get; set; }
        public int? IdNivelEnsino { get; set; }
        public string Nome { get; set; }
        public short? IdStatusGame { get; set; }
        public int QtdComoComprador { get; set; }
        public int QtdComoFornecedor { get; set; }

        public virtual Curso IdCursoNavigation { get; set; }
        public virtual Instituicao IdInstituicaoNavigation { get; set; }
        public virtual NivelEnsino IdNivelEnsinoNavigation { get; set; }
        public virtual StatusGame IdStatusGameNavigation { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<Avaliacao> Avaliacao { get; set; }
        public virtual ICollection<CompradorFornecedor> CompradorFornecedorIdCompradorNavigation { get; set; }
        public virtual ICollection<CompradorFornecedor> CompradorFornecedorIdFornecedorNavigation { get; set; }
        public virtual ICollection<FornecedorLeilao> FornecedorLeilao { get; set; }
        public virtual ICollection<Leilao> Leilao { get; set; }
        public virtual ICollection<UsuarioInstituicao> UsuarioInstituicao { get; set; }
    }
}

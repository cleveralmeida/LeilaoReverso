using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class Instituicao
    {
        public Instituicao()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            UsuarioInstituicao = new HashSet<UsuarioInstituicao>();
        }

        public int IdInstituicao { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<UsuarioInstituicao> UsuarioInstituicao { get; set; }
    }
}

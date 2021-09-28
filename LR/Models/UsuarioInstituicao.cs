using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class UsuarioInstituicao
    {
        public int IdUsuarioInstituicao { get; set; }
        public string IdUsuario { get; set; }
        public int IdInstituicao { get; set; }

        public virtual Instituicao IdInstituicaoNavigation { get; set; }
        public virtual AspNetUsers IdUsuarioNavigation { get; set; }
    }
}

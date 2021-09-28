using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class Comprador
    {
        public Comprador()
        {
            Leilao = new HashSet<Leilao>();
        }

        public int IdComprador { get; set; }
        public string IdUsuario { get; set; }

        public virtual AspNetUsers IdUsuarioNavigation { get; set; }
        public virtual ICollection<Leilao> Leilao { get; set; }
    }
}

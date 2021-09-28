using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class NivelEnsino
    {
        public NivelEnsino()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int IdNivelEnsino { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}

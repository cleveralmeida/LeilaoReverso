using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class TipoLeilao
    {
        public TipoLeilao()
        {
            Leilao = new HashSet<Leilao>();
        }

        public int IdTipoLeilao { get; set; }
        public string Descricao { get; set; }
        public int? Tempo { get; set; }

        public virtual ICollection<Leilao> Leilao { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class PesoValorLeilao
    {
        public int IdPesoValorLeilao { get; set; }
        public decimal? Valor { get; set; }
        public int? Peso { get; set; }
        public string Tipo { get; set; }
        public int? IdLeilao { get; set; }

        public virtual Leilao IdLeilaoNavigation { get; set; }
    }
}

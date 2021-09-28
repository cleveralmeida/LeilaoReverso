using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class HistoricoLeilaoMV
    {
        public int IdLeilao { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int? IdOpcaoComprador { get; set; }
        public int? AtributoPrecoPrazo { get; set; }
        public decimal? CustoEsperado { get; set; }
        public string TipoLeilao { get; set; }
        public string DescAtributoPrecoPrazo { get; set; }
    }
}

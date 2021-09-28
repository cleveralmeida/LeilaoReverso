using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class FornecedorLeilaoMV
    {
        public string IdFornecedor { get; set; }
        public int IdLeilao { get; set; }
        public string IdTipoLeilao { get; set; }
        public int? Sequencia { get; set; }
        public int? Classificacao { get; set; }
        public short? Rodada { get; set; }
        public decimal? Lance { get; set; }
        public string ValorInicial { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string DescAtributoPrecoPrazo { get; set; }

    }
}

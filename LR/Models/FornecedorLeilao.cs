using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class FornecedorLeilao
    {
        public FornecedorLeilao()
        {
            FornecedorLeilaoLance = new HashSet<FornecedorLeilaoLance>();
        }

        public int IdFornecedorLeilao { get; set; }
        public string IdFornecedor { get; set; }
        public int IdLeilao { get; set; }
        public decimal? Lance { get; set; }
        public DateTime? Data { get; set; }
        public int? Classificacao { get; set; }
        public decimal? CustoServicoFrete { get; set; }
        public short? NivelRisco { get; set; }
        public int? AtributoPrecoPrazo { get; set; }
        public bool? IndicadorAtivo { get; set; }
        public decimal? PontuacaoPeso { get; set; }
        public bool? Vendedor { get; set; }
        public short? IdJustificativa { get; set; }

        public virtual AspNetUsers IdFornecedorNavigation { get; set; }
        public virtual Leilao IdLeilaoNavigation { get; set; }
        public virtual ICollection<FornecedorLeilaoLance> FornecedorLeilaoLance { get; set; }
    }
}

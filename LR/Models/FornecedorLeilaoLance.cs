using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class FornecedorLeilaoLance
    {
        public int IdFornecedorLeilaoLance { get; set; }
        public int? Sequencia { get; set; }
        public int IdFornecedorLeilao { get; set; }
        public DateTime? Data { get; set; }
        public short? Rodada { get; set; }
        public int? AtributoPrecoPrazo { get; set; }

        public virtual FornecedorLeilao IdFornecedorLeilaoNavigation { get; set; }
    }
}

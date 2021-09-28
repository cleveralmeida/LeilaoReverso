using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class PesoPrioridade
    {
        public int IdPesoPrioridade { get; set; }
        public int? IdOpcaoComprador { get; set; }
        public int? IdOpcaoFornecedor { get; set; }
        public decimal? Preco { get; set; }
        public int? PesoPreco { get; set; }
        public int? Prazo { get; set; }
        public int? PesoPrazo { get; set; }
        public int? Classificacao { get; set; }
    }
}

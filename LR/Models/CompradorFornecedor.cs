using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class CompradorFornecedor
    {
        public int IdCompradorFornecedor { get; set; }
        public string IdComprador { get; set; }
        public string IdFornecedor { get; set; }

        public virtual AspNetUsers IdCompradorNavigation { get; set; }
        public virtual AspNetUsers IdFornecedorNavigation { get; set; }
    }
}

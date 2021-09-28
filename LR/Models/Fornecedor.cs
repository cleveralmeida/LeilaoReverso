using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class Fornecedor
    {
        public int IdFornecedor { get; set; }
        public string IdUsuario { get; set; }

        public virtual AspNetUsers IdUsuarioNavigation { get; set; }
    }
}

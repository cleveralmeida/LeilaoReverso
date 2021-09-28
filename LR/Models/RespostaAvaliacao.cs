using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class RespostaAvaliacao
    {
        public int IdRespostaAvaliacao { get; set; }
        public int? IdAvaliacao { get; set; }
        public int? Resposta { get; set; }

        public virtual Avaliacao IdAvaliacaoNavigation { get; set; }
    }
}

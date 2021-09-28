using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class Avaliacao
    {
        public Avaliacao()
        {
            RespostaAvaliacao = new HashSet<RespostaAvaliacao>();
        }

        public int IdAvalidacao { get; set; }
        public string UserId { get; set; }
        public int? Pergunta { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<RespostaAvaliacao> RespostaAvaliacao { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class ResumoJogoMV
    {
        public string RoleId { get; set; }
        public int QtdLeilaoAberto { get; set; }
        public int QtdLeilaoCancelado { get; set; }
        public int QtdJogos { get; set; }
        public int QtdLancesValidos { get; set; }
        public int QtdLancesCancelados { get; set; }
        public int QtdGanhou { get; set; }
        public int? IdStatusGame { get; set; }
    }
}

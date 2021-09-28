using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LR.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? DataCadastro { get; set; }
        public int? IdInstituicao { get; set; }
        public int? IdCurso { get; set; }
        public int? IdNivelEnsino { get; set; }
        public string Nome { get; set; }
  //      public short? IdStatusGame { get; set; }
        public int QtdComoComprador { get; set; }
        public int QtdComoFornecedor { get; set; }

    }
}

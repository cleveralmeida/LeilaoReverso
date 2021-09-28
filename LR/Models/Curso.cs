using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class Curso
    {
        public Curso()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int IdCurso { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}

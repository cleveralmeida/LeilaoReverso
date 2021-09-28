using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class IdStatusGame
    {
        public IdStatusGame()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public short IdStatusGame1 { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}

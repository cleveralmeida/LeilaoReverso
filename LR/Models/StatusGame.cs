using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class StatusGame
    {
        public StatusGame()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public short IdStatusGame { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}

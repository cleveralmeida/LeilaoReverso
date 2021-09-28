using System;
namespace LR.Models
{
    public class LeilaoLanceVM
    {
        public string ConnectionId { get; set; }
        public int IdLeilao { get; set; }
        public int IdComprador { get; set; }
        public string UserGroup { get; set; }
        public string FgOcupado { get; set; }
        public string ConnectionIdOperador { get; set; }
        public string NomeOperador { get; set; }
        public string Data { get; set; }
        public string HoraMinuto { get; set; }
    }
}

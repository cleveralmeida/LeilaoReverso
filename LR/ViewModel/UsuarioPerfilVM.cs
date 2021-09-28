using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class UsuarioPerfilVM
    {
       public string UserId { get; set; }
       public string ProfileId { get; set; }
       public string UserName { get; set; }
       public string TipoUsuario { get; set; }
       public short? IdStatusGame { get; set; }
 
    }
}

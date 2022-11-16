using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EComm2022.Web.Models.Usuario
{
    public class UsuarioListVm
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }

    }
}
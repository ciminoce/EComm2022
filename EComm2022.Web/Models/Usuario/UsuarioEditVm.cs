using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Web.Models.Usuario
{
    public class UsuarioEditVm
    {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50,ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres",MinimumLength = 3)]
        
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]

        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]

        public string Correo { get; set; }
        public string Clave { get; set; }
        public Rol RolId { get; set; }
        public bool Activo { get; set; }
        public bool Reestablecer { get; set; }

    }
}
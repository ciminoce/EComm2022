using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EComm2022.Web.Models.Marca
{
    public class MarcaEditVm
    {
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 2)]
        [DisplayName("Marca")]
        public string NombreMarca { get; set; }
        public bool Activo { get; set; }

    }
}
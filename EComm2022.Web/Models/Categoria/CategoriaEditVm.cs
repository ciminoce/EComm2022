using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EComm2022.Web.Models.Categoria
{
    public class CategoriaEditVm
    {
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50,ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres",MinimumLength = 2)]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public bool Activa { get; set; }
    }
}
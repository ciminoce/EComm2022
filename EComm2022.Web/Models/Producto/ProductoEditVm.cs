using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace EComm2022.Web.Models.Producto
{
    public class ProductoEditVm
    {
        public int ProductoId { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50,ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres",MinimumLength = 2)]
        public string NombreProducto { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(256,ErrorMessage = "El campo {0} no puede contener más de {1} caracteres")]
        public string Descripcion { get; set; }

        [DisplayName("Marca")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1,int.MaxValue,ErrorMessage = "Debe seleccionar una marca")]
        public int MarcaId { get; set; }

        [DisplayName("Categoría")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]


        public int CategoriaId { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        public bool Activo { get; set; }

        public List<Entidades.Entidades.Marca> Marcas { get; set; }
        public List<Entidades.Entidades.Categoria> Categorias { get; set; }
        public HttpPostedFileBase ImagenFile { get; set; }
    }
}
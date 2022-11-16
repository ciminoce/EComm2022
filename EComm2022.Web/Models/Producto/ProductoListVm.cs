using System.ComponentModel;

namespace EComm2022.Web.Models.Producto
{
    public class ProductoListVm
    {
        public int ProductoId { get; set; }
        [DisplayName("Producto")]
        public string NombreProducto { get; set; }
        [DisplayName("Marca")]
        public string Marca { get; set; }
        [DisplayName("Categoría")]
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }
    }
}
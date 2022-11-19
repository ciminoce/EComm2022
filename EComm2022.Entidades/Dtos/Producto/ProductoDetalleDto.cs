namespace EComm2022.Entidades.Dtos.Producto
{
    public class ProductoDetalleDto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
        public string base64 { get; set; }
        public string extensionArchivo { get; set; }
        public string Descripcion { get; set; }

    }
}
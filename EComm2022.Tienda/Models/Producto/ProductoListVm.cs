using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace EComm2022.Tienda.Models.Producto
{
    public class ProductoListVm
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
    }
}
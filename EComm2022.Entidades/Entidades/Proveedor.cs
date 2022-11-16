using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComm2022.Entidades.Entidades
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public string Direccion { get; set; }
        public int ProvinciaId { get; set; }
        public int CiudadId { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public Ciudad Ciudad { get; set; }
        public Provincia Provincia { get; set; }
    }
}

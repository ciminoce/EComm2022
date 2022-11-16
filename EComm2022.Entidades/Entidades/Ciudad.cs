using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComm2022.Entidades.Entidades
{
    public class Ciudad
    {
        public int CiudadId { get; set; }
        public string NombreCiudad { get; set; }
        public int ProvinciaId { get; set; }
        public bool Activo { get; set; }
        public Provincia Provincia { get; set; }
    }
}

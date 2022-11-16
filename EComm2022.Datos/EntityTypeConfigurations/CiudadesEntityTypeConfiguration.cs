using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.EntityTypeConfigurations
{
    public class CiudadesEntityTypeConfiguration:EntityTypeConfiguration<Ciudad>
    {
        public CiudadesEntityTypeConfiguration()
        {
            ToTable("Ciudades");
        }
    }
}

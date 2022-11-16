using System.Data.Entity.ModelConfiguration;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.EntityTypeConfigurations
{
    public class ProductoEntityTypeConfiguration:EntityTypeConfiguration<Producto>
    {
        public ProductoEntityTypeConfiguration()
        {
            ToTable("Productos");
        }

    }
}

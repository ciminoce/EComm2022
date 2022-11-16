using System.Data.Entity.ModelConfiguration;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.EntityTypeConfigurations
{   
    public class CategoriaEntityTypeConfiguration:EntityTypeConfiguration<Categoria>
    {
        public CategoriaEntityTypeConfiguration()
        {
            ToTable("Categorias");
            Property(c => c.Activa).HasColumnName("Activo");
        }
    }
}

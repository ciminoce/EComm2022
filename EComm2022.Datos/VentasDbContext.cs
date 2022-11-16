using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos
{
    public class VentasDbContext:DbContext
    {
        public VentasDbContext():base("MiConexion")
        {
            Database.CommandTimeout = 45;
            // es muy importante para evitar que EF convierta la query en un SQL que compare contra null tanto a la col y al valor 
            // es decir, sin esta linea hace esto: Col is null OR @v is null OR Col=@v
            Configuration.UseDatabaseNullSemantics = true;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<VentasDbContext>(null);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

    }
}

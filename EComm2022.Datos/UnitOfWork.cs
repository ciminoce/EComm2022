namespace EComm2022.Datos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VentasDbContext context;
        public UnitOfWork(VentasDbContext context)
        {
            this.context = context;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}

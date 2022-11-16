using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComm2022.Datos
{
    public class UnifOfWork:IUnitOfWork
    {
        private readonly VentasDbContext _context;
        public UnifOfWork(VentasDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

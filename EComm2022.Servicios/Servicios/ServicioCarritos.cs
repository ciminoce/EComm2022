using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Servicios.Servicios
{
    public class ServicioCarritos:IServicioCarritos
    {
        private readonly IRepositorioCarritos repositorioCarritos;
        private readonly IRepositorioProductos repositorioProductos;
        private readonly IUnitOfWork unitOfWork;

        public ServicioCarritos(IRepositorioCarritos repositorioCarritos, IRepositorioProductos repositorioProductos, IUnitOfWork unitOfWork)
        {
            this.repositorioCarritos = repositorioCarritos;
            this.repositorioProductos = repositorioProductos;
            this.unitOfWork = unitOfWork;
        }
        public void AgregarAlCarrito(int clienteId, int productoId)
        {
            try
            {
                using (var tran=new TransactionScope(TransactionScopeOption.Required))
                {
                    repositorioCarritos.AgregarAlCarrito(clienteId, productoId);
                    unitOfWork.Save();
                    repositorioProductos.ActualizarStock(productoId, 1, false);
                    unitOfWork.Save();
                    tran.Complete();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int CantidadEnCarrito(int clienteId)
        {
            try
            {
                return repositorioCarritos.CantidadEnCarrito(clienteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

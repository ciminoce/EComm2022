using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComm2022.Servicios.Servicios.Facades
{
    public interface IServicioCarritos
    {
        void AgregarAlCarrito(int clienteId, int productoId);
        int CantidadEnCarrito(int clienteId);

    }
}

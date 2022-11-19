using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComm2022.Datos.Repositorios.Facades
{
    public interface IRepositorioCarritos
    {
        void AgregarAlCarrito(int clienteId, int productoId);
        int CantidadEnCarrito(int clienteId);


    }
}

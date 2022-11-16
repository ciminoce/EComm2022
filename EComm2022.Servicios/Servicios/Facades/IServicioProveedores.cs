using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Servicios.Servicios.Facades
{
    public interface IServicioProveedores
    {
        List<Proveedor> GetLista();
        Proveedor GetProveedorPorId(int id);
        void Guardar(Proveedor proveedor);
        void Borrar(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        bool EstaRelacionada(Proveedor proveedor);

    }
}

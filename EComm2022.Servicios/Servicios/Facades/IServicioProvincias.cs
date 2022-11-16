using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Servicios.Servicios.Facades
{
    public interface IServicioProvincias
    {
        List<Provincia> GetLista();
        Provincia GetProvinciaPorId(int id);
        void Guardar(Provincia provincia);
        bool Existe(Provincia provincia);
        bool EstaRelacionado(Provincia provincia);

        void Borrar(Provincia provincia);
    }
}

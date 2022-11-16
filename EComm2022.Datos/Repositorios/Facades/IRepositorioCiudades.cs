using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios.Facades
{
    public interface IRepositorioCiudades
    {
        List<Ciudad> GetLista();
        Ciudad GetCiudadPorId(int id);
        void Guardar(Ciudad ciudad);
        void Borrar(Ciudad ciudad);
        bool Existe(Ciudad ciudad);
        bool EstaRelacionada(Ciudad ciudad);
        List<Ciudad> GetListaPorPais(int id);
    }
}

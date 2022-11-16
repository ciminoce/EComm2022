using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Servicios.Servicios.Facades
{
    public interface IServicioMarcas
    {
        List<Marca> GetLista();
        Marca GetMarcaPorId(int id);
        void Guardar(Marca marca);
        bool Existe(Marca marca);
        bool EstaRelacionado(Marca marca);

        void Borrar(Marca marca);
        List<Marca> GetMarcasPorCategoria(int categoriaId);
    }
}

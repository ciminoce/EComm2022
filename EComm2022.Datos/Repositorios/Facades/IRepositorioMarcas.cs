using System.Collections.Generic;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios.Facades
{
    public interface IRepositorioMarcas
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
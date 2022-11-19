using System.Collections.Generic;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios.Facades
{
    public interface IRepositorioProductos
    {
        List<ProductoListDto> GetLista();
        bool Existe(Producto producto);
        void Guardar(Producto producto);
        Producto GetProductoPorId(int id);
        List<ProductoListDto> GetLista(int categoriaId, int marcaId);
        void ActualizarStock(int productoId, int cantidad, bool suma);
    }
}

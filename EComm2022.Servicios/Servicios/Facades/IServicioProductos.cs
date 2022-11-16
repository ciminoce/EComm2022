using System.Collections.Generic;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Servicios.Servicios.Facades
{
    public interface IServicioProductos
    {
        List<ProductoListDto> GetLista();
        bool Existe(Producto producto);
        void Guardar(Producto producto);
        Producto GetProductoPorId(int value);
        List<ProductoListDto> GetLista(int categoriaId, int marcaId);
    }
}

using System.Collections.Generic;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios.Facades
{
    public interface IRepositorioCategorias
    {
        List<Categoria> GetLista();
        Categoria GetCategoriaPorId(int id);
        void Guardar(Categoria categoria);
        bool Existe(Categoria categoria);
        bool EstaRelacionado(Categoria categoria);

    }
}

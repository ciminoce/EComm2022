using System;
using System.Collections.Generic;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Servicios.Servicios
{
    public class ServicioProductos:IServicioProductos
    {
        private readonly VentasDbContext context;
        private readonly IRepositorioProductos repositorio;
        private readonly IUnitOfWork unitOfWork;

        public ServicioProductos(RepositorioProductos repositorio, VentasDbContext context, UnitOfWork unitOfWork)
        {
            this.context = context;
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }
        public List<ProductoListDto> GetLista()
        {
            try
            {
                return repositorio.GetLista();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Existe(Producto producto)
        {
            try
            {
                return repositorio.Existe(producto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Producto producto)
        {
            try
            {
                repositorio.Guardar(producto);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Producto GetProductoPorId(int id)
        {
            try
            {
                return repositorio.GetProductoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductoListDto> GetLista(int categoriaId, int marcaId)
        {
            try
            {
                return repositorio.GetLista(categoriaId, marcaId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

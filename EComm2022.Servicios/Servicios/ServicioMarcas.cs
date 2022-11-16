using System;
using System.Collections.Generic;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Servicios.Servicios
{
    public class ServicioMarcas:IServicioMarcas
    {
        private readonly IRepositorioMarcas repositorio;
        private readonly VentasDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public ServicioMarcas(RepositorioMarcas repositorio, VentasDbContext context, UnitOfWork unitOfWork)
        {
            this.context = context;
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }

        public List<Marca> GetLista()
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

        public Marca GetMarcaPorId(int id)
        {
            try
            {
                return repositorio.GetMarcaPorId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Marca marca)
        {
            try
            {
                repositorio.Guardar(marca);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Existe(Marca marca)
        {
            try
            {
                return repositorio.Existe(marca);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EstaRelacionado(Marca marca)
        {
            try
            {
                return repositorio.EstaRelacionado(marca);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Borrar(Marca marca)
        {
            try
            {
                repositorio.Borrar(marca);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Marca> GetMarcasPorCategoria(int categoriaId)
        {
            try
            {
                return repositorio.GetMarcasPorCategoria(categoriaId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

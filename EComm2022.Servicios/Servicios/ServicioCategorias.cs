using System;
using System.Collections.Generic;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Servicios.Servicios
{
    public class ServicioCategorias:IServicioCategorias
    {
        private readonly IRepositorioCategorias repositorio;
        private readonly VentasDbContext context;
        private readonly IUnitOfWork unitOfWork;
        public ServicioCategorias(RepositorioCategorias repositorio, VentasDbContext context, UnitOfWork unitOfWork )
        {
            this.context = context;
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }

        public List<Categoria> GetLista()
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

        public Categoria GetCategoriaPorId(int id)
        {
            try
            {
                return repositorio.GetCategoriaPorId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Categoria categoria)
        {
            try
            {
                repositorio.Guardar(categoria);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Existe(Categoria categoria)
        {
            try
            {
                return repositorio.Existe(categoria);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EstaRelacionado(Categoria categoria)
        {
            try
            {
                return repositorio.EstaRelacionado(categoria);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

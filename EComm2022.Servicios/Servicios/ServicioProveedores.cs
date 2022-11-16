using System;
using System.Collections.Generic;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Servicios.Servicios
{
    public class ServicioProveedores:IServicioProveedores
    {
        private readonly IRepositorioProveedores repositorio;
        private readonly IUnitOfWork unitOfWork;

        public ServicioProveedores(IRepositorioProveedores repositorio, IUnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }
        public List<Proveedor> GetLista()
        {
            try
            {
                return repositorio.GetLista();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Proveedor GetProveedorPorId(int id)
        {
            try
            {
                return repositorio.GetProveedorPorId(id);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Guardar(Proveedor proveedor)
        {
            try
            {
                repositorio.Guardar(proveedor);
                unitOfWork.Save();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Borrar(Proveedor proveedor)
        {
            try
            {
                repositorio.Borrar(proveedor);
                unitOfWork.Save();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                return repositorio.Existe(proveedor);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Proveedor proveedor)
        {
            return false;
        }

    }
}

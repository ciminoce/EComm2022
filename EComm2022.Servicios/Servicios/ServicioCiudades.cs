using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Servicios.Servicios
{
    public class ServicioCiudades:IServicioCiudades
    {
        private readonly IRepositorioCiudades repositorio;
        private readonly IUnitOfWork unitOfWork;

        public ServicioCiudades(IRepositorioCiudades repositorio, IUnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }
        public List<Ciudad> GetLista()
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

        public Ciudad GetCiudadPorId(int id)
        {
            try
            {
                return repositorio.GetCiudadPorId(id);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void Guardar(Ciudad ciudad)
        {
            try
            {
                repositorio.Guardar(ciudad);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
               
                throw;
            }
        }

        public void Borrar(Ciudad ciudad)
        {
            try
            {
                repositorio.Borrar(ciudad);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
               
                throw;
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            try
            {
                return repositorio.Existe(ciudad);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public bool EstaRelacionada(Ciudad ciudad)
        {
            return false;
        }

        public List<Ciudad> GetListaPorPais(int id)
        {
            try
            {
                return repositorio.GetListaPorPais(id);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}

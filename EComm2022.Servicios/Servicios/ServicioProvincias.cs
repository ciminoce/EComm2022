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
    public class ServicioProvincias : IServicioProvincias
    {
        private IRepositorioProvincias repositorio;
        private IUnitOfWork unitOfWork;

        public ServicioProvincias(IRepositorioProvincias repositorio, IUnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }

        public List<Provincia> GetLista()
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

        public Provincia GetProvinciaPorId(int id)
        {
            try
            {
                return repositorio.GetProvinciaPorId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Guardar(Provincia provincia)
        {
            try
            {
                repositorio.Guardar(provincia);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool Existe(Provincia provincia)
        {
            try
            {
                return repositorio.Existe(provincia);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EstaRelacionado(Provincia provincia)
        {
            return false;
        }

        public void Borrar(Provincia provincia)
        {
            try
            {
                repositorio.Borrar(provincia);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

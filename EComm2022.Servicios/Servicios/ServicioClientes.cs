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
    public class ServicioClientes:IServicioClientes
    {
        private readonly IRepositorioClientes repositorio;
        private readonly IUnitOfWork unitOfWork;

        public ServicioClientes(IRepositorioClientes repositorio, IUnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }

        public bool Existe(Cliente cliente)
        {
            try
            {
                return repositorio.Existe(cliente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Cliente GetClientePorCorreo(string correo, string clave)
        {
            try
            {
                return repositorio.GetClientePorCorreo(correo,clave);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Cliente GetClientePorCorreo(string correo)
        {
            try
            {
                return repositorio.GetClientePorCorreo(correo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Existe(string correo)
        {
            try
            {
                return repositorio.Existe(correo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Cliente GetClientePorId(int id)
        {
            try
            {
                return repositorio.GetClientePorId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Cliente cliente)
        {
            try
            {
                repositorio.Guardar(cliente);
                unitOfWork.Save();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}

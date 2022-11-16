using System;
using System.Collections.Generic;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Dtos.Usuario;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Servicios.Servicios
{
    public class ServicioUsuarios:IServicioUsuarios
    {
        private readonly IRepositorioUsuarios repositorio;
        private readonly IUnitOfWork unitOfWork;

        public ServicioUsuarios(IRepositorioUsuarios repositorio, IUnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.unitOfWork = unitOfWork;
        }
        public List<UsuarioListDto> GetLista()
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

        public bool Existe(Usuario usuario)
        {
            try
            {
                return repositorio.Existe(usuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Usuario usuario)
        {
            try
            {
                repositorio.Guardar(usuario);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetUsuarioPorCorreo(string correo, string clave)
        {
            try
            {
                return repositorio.GetUsuarioPorCorreo(correo, clave);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetUsuarioPorId(int id)
        {
            try
            {
                return repositorio.GetUsuarioPorId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetUsuarioPorCorreo(string correo)
        {
            try
            {
                return repositorio.GetUsuarioPorCorreo(correo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

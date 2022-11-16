using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Dtos.Usuario;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioUsuarios:IRepositorioUsuarios
    {
        private readonly VentasDbContext context;

        public RepositorioUsuarios(VentasDbContext context)
        {
            this.context = context;
        }
        public List<UsuarioListDto> GetLista()
        {
            try
            {
                return context.Usuarios
                    .Select(u=>new UsuarioListDto()
                    {
                        UsuarioId = u.UsuarioId,
                        Nombre = u.Nombre,
                        Apellido = u.Apellido,
                        Correo = u.Correo,
                        RolId =(int) u.RolId,
                        Activo = u.Activo
                    }).ToList();
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
                if (usuario.UsuarioId==0)
                {
                    return context.Usuarios.Any(u => u.Correo == usuario.Correo);
                }

                return context.Usuarios.Any(u => u.Correo == usuario.Correo &&
                                                 u.UsuarioId != usuario.UsuarioId);
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
                if (usuario.UsuarioId == 0)
                {
                    context.Usuarios.Add(usuario);
                }
                else
                {
                    var usuarioInDb = context.Usuarios.SingleOrDefault(u => u.UsuarioId == usuario.UsuarioId);
                    usuarioInDb.UsuarioId = usuario.UsuarioId;
                    usuarioInDb.Nombre = usuario.Nombre;
                    usuarioInDb.Apellido = usuario.Apellido;
                    usuarioInDb.Correo = usuario.Correo;
                    usuarioInDb.Activo = usuario.Activo;
                    usuarioInDb.RolId = usuario.RolId;
                    usuarioInDb.Reestablecer = usuario.Reestablecer;
                    usuarioInDb.Clave = usuario.Clave;

                    context.Entry(usuarioInDb).State = EntityState.Modified;

                }
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
                return context.Usuarios
                    .SingleOrDefault(u => u.Correo == correo && u.Clave == clave);
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
                return context.Usuarios
                    .SingleOrDefault(u => u.UsuarioId == id);
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
                return context.Usuarios
                    .SingleOrDefault(u => u.Correo == correo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

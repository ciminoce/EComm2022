using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioProveedores:IRepositorioProveedores
    {
        private readonly VentasDbContext context;

        public RepositorioProveedores(VentasDbContext context)
        {
            this.context = context;
        }
        public List<Proveedor> GetLista()
        {
            try
            {
                return context.Proveedores
                    .Include(p => p.Provincia)
                    .Include(p => p.Ciudad)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Proveedor GetProveedorPorId(int id)
        {
            try
            {
                return context.Proveedores
                    .Include(p => p.Provincia)
                    .Include(p => p.Ciudad)
                    .SingleOrDefault(p => p.ProveedorId == id);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void Guardar(Proveedor proveedor)
        {
            try
            {
                //TODO:Explicar esto
                if (proveedor.Provincia != null)
                {
                    proveedor.Provincia = null;
                }
                if (proveedor.Ciudad != null)
                {
                    proveedor.Ciudad = null;
                }


                if (proveedor.ProveedorId == 0)
                {
                    context.Proveedores.Add(proveedor);
                }

                else
                {
                    var proveedorInDb = context.Proveedores.SingleOrDefault(p => p.ProveedorId == proveedor.ProveedorId);
                    proveedorInDb.NombreProveedor = proveedor.NombreProveedor;
                    proveedorInDb.CiudadId = proveedor.CiudadId;
                    proveedorInDb.ProvinciaId = proveedor.ProvinciaId;
                    proveedorInDb.Activo = proveedor.Activo;
                    proveedorInDb.Direccion = proveedor.Direccion;
                    proveedorInDb.Telefono = proveedor.Telefono;
                    proveedorInDb.Activo = proveedor.Activo;
                    context.Entry(proveedorInDb).State = EntityState.Modified;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Borrar(Proveedor proveedor)
        {
            try
            {
                var proveedorInDb = context.Proveedores.SingleOrDefault(p => p.ProveedorId == proveedor.ProveedorId);

                context.Entry(proveedorInDb).State = EntityState.Deleted;
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
                if (proveedor.ProveedorId == 0)
                {
                    return context.Proveedores
                        .Any(c => c.NombreProveedor == proveedor.NombreProveedor);
                }

                return context.Proveedores
                    .Any(c => c.NombreProveedor == proveedor.NombreProveedor && 
                              c.ProveedorId!=proveedor.ProveedorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EstaRelacionada(Proveedor proveedor)
        {
            try
            {
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

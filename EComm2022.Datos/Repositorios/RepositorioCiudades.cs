using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioCiudades:IRepositorioCiudades
    {
        private readonly VentasDbContext context;

        public RepositorioCiudades(VentasDbContext context)
        {
            this.context = context;
        }

        public List<Ciudad> GetLista()
        {
            try
            {
                return context.Ciudades
                    .Include(c=>c.Provincia)
                    .OrderBy(c=>c.NombreCiudad)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Borrar(Ciudad ciudad)
        {
            try
            {
                var ciudadInDb = context.Ciudades.SingleOrDefault(p => p.CiudadId == ciudad.CiudadId);

                context.Entry(ciudadInDb).State = EntityState.Deleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            try
            {
                if (ciudad.CiudadId == 0)
                {
                    return context.Ciudades
                        .Any(c =>c.NombreCiudad == ciudad.NombreCiudad && c.ProvinciaId==ciudad.ProvinciaId );
                }

                return context.Ciudades
                    .Any(c => c.NombreCiudad == ciudad.NombreCiudad && c.ProvinciaId == ciudad.ProvinciaId
                                                                    && c.CiudadId != ciudad.CiudadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EstaRelacionada(Ciudad ciudad)
        {
            try
            {
                return false;
            }
            catch (Exception e)
            {
               
                throw;
            }
        }

        public List<Ciudad> GetListaPorPais(int id)
        {
            try
            {
                return context.Ciudades
                    .OrderBy(c => c.NombreCiudad)
                    .Where(c=>c.ProvinciaId==id)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Ciudad ciudad)
        {
            try
            {
                //TODO:Explicar esto
                if (ciudad.Provincia!=null)
                {
                    ciudad.Provincia = null;
                }

                if (ciudad.CiudadId == 0)
                {
                    context.Ciudades.Add(ciudad);
                }

                else
                {
                    var ciudadInDb = context.Ciudades.SingleOrDefault(p => p.CiudadId == ciudad.CiudadId);
                    ciudadInDb.CiudadId = ciudad.CiudadId;
                    ciudadInDb.NombreCiudad = ciudad.NombreCiudad;
                    ciudadInDb.ProvinciaId =ciudad.ProvinciaId;
                    ciudadInDb.Activo = ciudad.Activo;

                    context.Entry(ciudadInDb).State = EntityState.Modified;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ciudad GetCiudadPorId(int id)
        {
            try
            {
                return context.Ciudades
                    .Include(c => c.Provincia)
                    .SingleOrDefault(c => c.CiudadId == id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}

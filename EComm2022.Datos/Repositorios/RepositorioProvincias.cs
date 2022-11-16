using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioProvincias:IRepositorioProvincias
    {
        private readonly VentasDbContext context;

        public RepositorioProvincias(VentasDbContext context)
        {
            this.context = context;
        }
        public List<Provincia> GetLista()
        {
            try
            {
                return context.Provincias.OrderBy(p => p.NombreProvincia).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Provincia GetProvinciaPorId(int id)
        {
            try
            {
                return context.Provincias.SingleOrDefault(p => p.ProvinciaId == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Provincia provincia)
        {
            try
            {
                if (provincia.ProvinciaId == 0)
                {
                    context.Provincias.Add(provincia);
                }
                else
                {
                    var provinciaInDb = context.Provincias
                        .SingleOrDefault(p => p.ProvinciaId == provincia.ProvinciaId);
                    provinciaInDb.ProvinciaId = provincia.ProvinciaId;
                    provinciaInDb.NombreProvincia = provincia.NombreProvincia;
                    provinciaInDb.Activo = provincia.Activo;

                    context.Entry(provinciaInDb).State = EntityState.Modified;
                    //context.Entry(provincia).State = EntityState.Modified;
                }

          
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Borrar(Provincia provincia)
        {
            try
            {
                context.Entry(provincia).State = EntityState.Deleted;
                context.SaveChanges();
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
                if (provincia.ProvinciaId == 0)
                {
                    return context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia);
                }

                return context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia
                                               && p.ProvinciaId != provincia.ProvinciaId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EstaRelacionado(Provincia provincia)
        {
            //TODO: Hacer este método luego de completar las ciudades
            return false;
        }


    }
}

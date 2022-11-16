using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioMarcas:IRepositorioMarcas
    {
        private VentasDbContext context;

        public RepositorioMarcas(VentasDbContext context)
        {
            this.context = context;
        }
        public List<Marca> GetLista()
        {
            try
            {
                return context.Marcas.OrderBy(m=>m.NombreMarca).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Marca GetMarcaPorId(int id)
        {
            try
            {
                return context.Marcas.SingleOrDefault(c => c.MarcaId == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Marca marca)
        {
            try
            {
                if (marca.MarcaId == 0)
                {
                    context.Marcas.Add(marca);
                }
                else
                {
                    var marcaInDb = context.Marcas
                        .SingleOrDefault(m => m.MarcaId == marca.MarcaId);

                    marcaInDb.MarcaId = marca.MarcaId;
                    marcaInDb.NombreMarca = marca.NombreMarca;
                    marcaInDb.Activo = marca.Activo;
                    context.Entry(marcaInDb).State = EntityState.Modified;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Existe(Marca marca)
        {
            try
            {
                if (marca.MarcaId == 0)
                {
                    return context.Marcas.Any(m => m.NombreMarca == marca.NombreMarca);
                }

                return context.Marcas.Any(m => m.NombreMarca == marca.NombreMarca
                                                   && m.MarcaId != marca.MarcaId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EstaRelacionado(Marca marca)
        {
            return false;
        }

        public void Borrar(Marca marca)
        {
            try
            {
                context.Entry(marca).State = EntityState.Deleted;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Marca> GetMarcasPorCategoria(int categoriaId)
        {
            try
            {
                if (categoriaId==0)
                {
                    return context.Marcas.ToList();
                }
                return context.Productos
                    .Include(p => p.Marca)
                    .Where(p => p.CategoriaId == categoriaId)
                    .Select(p => p.Marca).Distinct().ToList();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }
    }
}

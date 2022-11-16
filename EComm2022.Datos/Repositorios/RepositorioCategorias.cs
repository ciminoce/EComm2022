using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioCategorias:IRepositorioCategorias
    {
        private VentasDbContext context;

        public RepositorioCategorias(VentasDbContext context)
        {
            this.context = context;
        }
        public List<Categoria> GetLista()
        {
            try
            {
                return context.Categorias.OrderBy(c=>c.Descripcion).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Categoria GetCategoriaPorId(int id)
        {
            try
            {
                return context.Categorias.SingleOrDefault(c => c.CategoriaId == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Guardar(Categoria categoria)
        {
            try
            {
                if (categoria.CategoriaId==0)
                {
                    context.Categorias.Add(categoria);
                }
                else
                {
                    var categoriaInDb = context.Categorias
                        .SingleOrDefault(c => c.CategoriaId == categoria.CategoriaId);

                    categoriaInDb.CategoriaId = categoria.CategoriaId;
                    categoriaInDb.Descripcion = categoria.Descripcion;
                    categoriaInDb.Activa = categoria.Activa;
                    context.Entry(categoriaInDb).State = EntityState.Modified;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Existe(Categoria categoria)
        {
            try
            {
                if (categoria.CategoriaId==0)
                {
                    return context.Categorias.Any(c => c.Descripcion == categoria.Descripcion);
                }

                return context.Categorias.Any(c => c.Descripcion == categoria.Descripcion
                                                   && c.CategoriaId != categoria.CategoriaId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EstaRelacionado(Categoria categoria)
        {
            return false;
        }
    }
}

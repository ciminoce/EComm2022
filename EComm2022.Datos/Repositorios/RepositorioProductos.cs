using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioProductos : IRepositorioProductos
    {
        private VentasDbContext context;

        public RepositorioProductos(VentasDbContext context)
        {
            this.context = context;
        }

        public List<ProductoListDto> GetLista()
        {
            try
            {
                return context.Productos
                    .Include(p => p.Marca)
                    .Include(p => p.Categoria)
                    .Select(p => new ProductoListDto()
                    {
                        ProductoId = p.ProductoId,
                        NombreProducto = p.NombreProducto,
                        Marca = p.Marca.NombreMarca,
                        Categoria = p.Categoria.Descripcion,
                        Precio = p.Precio,
                        Stock = p.Stock,
                        Activo = p.Activo,
                        Imagen = p.Imagen

                    }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Existe(Producto producto)
        {
            try
            {
                if (producto.ProductoId == 0)
                {
                    return context.Productos
                        .Any(p => p.NombreProducto == producto.NombreProducto);
                }

                return context.Productos
                    .Any(p => p.NombreProducto == producto.NombreProducto
                              && p.ProductoId != producto.ProductoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Guardar(Producto producto)
        {
            try
            {
                if (producto.ProductoId == 0)
                {
                    context.Productos.Add(producto);
                }

                else
                {
                    var productoInDb = context.Productos.SingleOrDefault(p => p.ProductoId == producto.ProductoId);
                    productoInDb.ProductoId = producto.ProductoId;
                    productoInDb.NombreProducto = producto.NombreProducto;
                    productoInDb.Descripcion = producto.Descripcion;
                    productoInDb.CategoriaId = producto.CategoriaId;
                    productoInDb.MarcaId = producto.MarcaId;
                    productoInDb.Imagen = producto.Imagen;
                    productoInDb.Activo = producto.Activo;
                    productoInDb.Precio = producto.Precio;
                    productoInDb.Stock = producto.Stock;
                    context.Entry(productoInDb).State = EntityState.Modified;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Producto GetProductoPorId(int id)
        {
            try
            {
                return context.Productos
                    .Include(p => p.Categoria)
                    .Include(p => p.Marca)
                    .SingleOrDefault(p => p.ProductoId == id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<ProductoListDto> GetLista(int categoriaId, int marcaId)
        {
            try
            {
                IQueryable<Producto> query = context.Productos
                    .Where(p => p.Stock > 0 && p.Activo);
                if (categoriaId>0)
                {
                    query = query.Where(p => p.CategoriaId == categoriaId);
                }

                if (marcaId>0)
                {
                    query = query.Where(p => p.MarcaId == marcaId);
                }

                query = query.Include(p => p.Marca).Include(p => p.Categoria);
                var lista = query.Select(p => new ProductoListDto()
                {
                    ProductoId = p.ProductoId,
                    NombreProducto = p.NombreProducto,
                    Marca = p.Marca.NombreMarca,
                    Categoria = p.Categoria.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Activo = p.Activo,
                    Imagen = p.Imagen

                }).ToList();
                return lista;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ActualizarStock(int productoId, int cantidad, bool suma)
        {
            try
            {
                var productoInDb = context.Productos
                    .SingleOrDefault(p => p.ProductoId == productoId);
                if (suma)
                {
                    productoInDb.Stock -= cantidad;
                }
                else
                {
                    productoInDb.Stock += cantidad;
                }

                context.Entry(productoInDb).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

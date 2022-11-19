using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioCarritos:IRepositorioCarritos
    {
        private readonly VentasDbContext context;

        public RepositorioCarritos(VentasDbContext context)
        {
            this.context = context;
        }
        public void AgregarAlCarrito(int clienteId, int productoId)
        {
            try
            {
                var productoInCarrito =
                    context.Carritos
                        .SingleOrDefault(c => c.ClienteId == clienteId && c.ProductoId == productoId);
                if (productoInCarrito==null)
                {
                    Carrito carrito = new Carrito()
                    {
                        ProductoId = productoId,
                        ClienteId = clienteId,
                        Cantidad = 1
                    };
                    context.Carritos.Add(carrito);
                }
                else
                {
                    productoInCarrito.Cantidad += 1;
                    context.Entry(productoInCarrito).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int CantidadEnCarrito(int clienteId)
        {
            try
            {
                return context.Carritos.Count(c => c.ClienteId == clienteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

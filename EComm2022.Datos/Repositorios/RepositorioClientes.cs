using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Entidades.Entidades;
using System;
using System.Data.Entity;
using System.Linq;

namespace EComm2022.Datos.Repositorios
{
    public class RepositorioClientes : IRepositorioClientes
    {
        private readonly VentasDbContext context;

        public RepositorioClientes(VentasDbContext context)
        {
            this.context = context;
        }

        public bool Existe(Cliente cliente)
        {
            try
            {
                if (cliente.ClienteId==0)
                {
                    return context.Clientes.Any(c => c.Nombres == cliente.Nombres && c.Apellido == cliente.Apellido);
                }
                return context.Clientes.Any(c => c.Nombres == cliente.Nombres && c.Apellido == cliente.Apellido && c.ClienteId==cliente.ClienteId) ;
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
                
                return context.Clientes.Any(c => c.Correo == correo);
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
                return context.Clientes.SingleOrDefault(c => c.Correo == correo);
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
                return context.Clientes.SingleOrDefault(c => c.ClienteId == id);
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
                return context.Clientes.SingleOrDefault(c => c.Correo == correo && c.Clave==clave);
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
                if (cliente.ClienteId == 0)
                {
                    context.Clientes.Add(cliente);
                }
                else
                {
                    var clienteInDb = context.Clientes.SingleOrDefault(c => c.ClienteId == cliente.ClienteId);
                    clienteInDb.ClienteId = cliente.ClienteId;
                    clienteInDb.Nombres = cliente.Nombres;
                    clienteInDb.Apellido = cliente.Apellido;
                    clienteInDb.Correo = cliente.Correo;
                    clienteInDb.Clave = cliente.Clave;
                    clienteInDb.Reestablecer = cliente.Reestablecer;

                    context.Entry(clienteInDb).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}

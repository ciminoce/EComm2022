using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Servicios.Servicios.Facades
{
    public interface IServicioClientes
    {
        void Guardar(Cliente cliente);
        bool Existe(Cliente cliente);
        Cliente GetClientePorCorreo(string correo);
        bool Existe(string correo);
        Cliente GetClientePorId(int id);
        Cliente GetClientePorCorreo(string correo, string claveEncriptada);
    }
}

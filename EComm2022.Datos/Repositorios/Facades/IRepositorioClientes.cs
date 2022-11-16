using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios.Facades
{
    public interface IRepositorioClientes
    {
        void Guardar(Cliente cliente);
        bool Existe(Cliente cliente);
        bool Existe(string correo);

        Cliente GetClientePorCorreo(string correo);

        Cliente GetClientePorId(int id);
        Cliente GetClientePorCorreo(string correo, string clave);
    }
}

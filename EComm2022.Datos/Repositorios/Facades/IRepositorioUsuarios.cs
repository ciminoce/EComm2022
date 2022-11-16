using System.Collections.Generic;
using EComm2022.Entidades.Dtos.Usuario;
using EComm2022.Entidades.Entidades;

namespace EComm2022.Datos.Repositorios.Facades
{
    public interface IRepositorioUsuarios
    {
        List<UsuarioListDto> GetLista();
        bool Existe(Usuario usuario);
        void Guardar(Usuario usuario);
        Usuario GetUsuarioPorCorreo(string correo, string clave);
        Usuario GetUsuarioPorId(int id);
        Usuario GetUsuarioPorCorreo(string correo);
    }
}

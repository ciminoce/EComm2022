using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Web.Helpers;
using EComm2022.Web.Models.Usuario;

namespace EComm2022.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        private readonly IServicioUsuarios servicio;
        private readonly IMapper mapper;

        public UsuarioController(IServicioUsuarios servicio)
        {
            this.servicio = servicio;
            this.mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index()
        {
            var lista = servicio.GetLista();
            var listaVm = mapper.Map<List<UsuarioListVm>>(lista);
            return View(listaVm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UsuarioEditVm usuarioVm)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioVm);
            }

            Usuario usuario = mapper.Map<Usuario>(usuarioVm);
            if (!servicio.Existe(usuario))
            {
                usuario.RolId = Rol.Administrador;
                usuario.Activo = true;
                usuario.Reestablecer = true;
                string clave = HelperUsuario.GenerarClave();

                string asunto = "Creación de Cuenta";
                string mensaje = $"<h3>Su cuenta se ha creado satisfactoriamente.<h3><br/><p>Su contraseña es {clave}</p>";
                bool respuesta = HelperUsuario.EnviarCorreo(usuarioVm.Correo, asunto, mensaje);

                if (respuesta)
                {
                    try
                    {
                        usuario.Clave = HelperUsuario.ConvertirSha256(clave);
                        servicio.Guardar(usuario);
                        return RedirectToAction("Index", "Usuario");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError(string.Empty, e.Message);
                        return View(usuarioVm);
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo enviar el correo!!!");
                    return View(usuarioVm);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Correo existente!!!");
                return View(usuarioVm);
            }

        }
    }
}
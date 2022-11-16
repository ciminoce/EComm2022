using System;
using System.Web.Mvc;
using System.Web.Security;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Web.Helpers;

namespace EComm2022.Web.Controllers
{
    public class AccesoController : Controller
    {
        private IServicioUsuarios servicio;

        public AccesoController(ServicioUsuarios servicio)
        {
            this.servicio = servicio;
        }
        // GET: Acceso
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string correo, string clave)
        {
            try
            {
                var claveEncriptada = HelperUsuario.ConvertirSha256(clave);
                Usuario usuario = servicio.GetUsuarioPorCorreo(correo, claveEncriptada);
                if (usuario == null)
                {
                    ViewBag.Error = "Usuario o clave mal ingresadas";
                    return View();
                }
                else
                {
                    if (usuario.Reestablecer)
                    {
                        TempData["usuarioId"] = usuario.UsuarioId;
                        return RedirectToAction("CambiarClave");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(usuario.Correo,false);
                        TempData["usuarioId"] = null;
                        return RedirectToAction("Index", "Home");
                        
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(string usuarioId, string claveActual, string nuevaClave, string confirmarClave)
        {
            try
            {
                Usuario u = servicio.GetUsuarioPorId(int.Parse(usuarioId));
                if (u.Clave != HelperUsuario.ConvertirSha256(claveActual))
                {
                    TempData["usuarioId"] = u.UsuarioId;
                    ViewBag.Error = "Clave errónea!!!";
                    return View();
                }else if (nuevaClave != confirmarClave)
                {
                    TempData["usuarioId"] = u.UsuarioId;
                    ViewData["vClave"] = claveActual;
                    ViewBag.Error = "La nueva clave y su confirmación no coinciden!!!";
                    return View();
                }

                u.Clave = HelperUsuario.ConvertirSha256(nuevaClave);
                u.Reestablecer = false;
                servicio.Guardar(u);
                return RedirectToAction("LogIn");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            
        }

        public ActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarCuenta(string correo)
        {
            try
            {
                Usuario usuario = servicio.GetUsuarioPorCorreo(correo);
                if (usuario == null)
                {
                    ViewBag.Error = "Correo ingresado inexistente!!!";
                    return View();
                }
                usuario.Reestablecer = true;
                string clave = HelperUsuario.GenerarClave();

                string asunto = "Recuperación de Cuenta";
                string mensaje = $"<h3>Su cuenta se ha recuperado satisfactoriamente.<h3><br/><p>Su nueva contraseña es {clave}</p>";
                bool respuesta = HelperUsuario.EnviarCorreo(usuario.Correo, asunto, mensaje);

                if (respuesta)
                {
                    try
                    {
                        usuario.Clave = HelperUsuario.ConvertirSha256(clave);
                        servicio.Guardar(usuario);
                        return RedirectToAction("LogIn", "Acceso");
                    }
                    catch (Exception e)
                    {
                        ViewBag.Error = e.Message;
                        return View();
                    }

                }
                else
                {
                    ViewBag.Error= "No se pudo enviar el correo!!!";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }

            
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            TempData["usuarioId"] = null;
            return RedirectToAction("LogIn");
        }
    }
}
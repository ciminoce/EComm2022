using System;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Tienda.Helpers;
using EComm2022.Tienda.Models.Cliente;

namespace EComm2022.Tienda.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IServicioClientes servicio;
        private readonly IMapper mapper;

        public AccesoController(IServicioClientes servicio)
        {
            this.servicio = servicio;
            this.mapper = AutoMapperConfig.Mapper;
        }

        // GET: Acceso
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string correo, string clave)
        {
            try
            {
                var claveEncriptada = HelperCliente.ConvertirSha256(clave);
                Cliente cliente = servicio.GetClientePorCorreo(correo, claveEncriptada);
                if (cliente == null)
                {
                    ViewBag.Error = "Usuario o clave mal ingresadas";
                    return View();
                }
                else
                {
                    if (cliente.Reestablecer)
                    {
                        TempData["clienteId"] = cliente.ClienteId;
                        return RedirectToAction("CambiarClave");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(cliente.Correo, false);
                        Session["cliente"] = cliente;
                        TempData["clienteId"] = null;
                        ViewBag.Error = null;
                        return RedirectToAction("Index", "Tienda");

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost]
        public ActionResult Registrarse(ClienteEditVm clienteVm)
        {
            if (!ModelState.IsValid)
            {
                return View(clienteVm);
            }

            ViewData["Nombres"] = clienteVm.Nombres;
            ViewData["Apellido"] = clienteVm.Apellido;
            ViewData["correo"] = clienteVm.Correo;
            try
            {
                if (clienteVm.Clave!=clienteVm.ConfirmarClave)
                {
                    ViewBag.Error = "Clave y confirmación no coinciden!!!";
                    return View(clienteVm);
                }

                Cliente cliente = mapper.Map<Cliente>(clienteVm);
                if (servicio.Existe(cliente))
                {
                    ViewBag.Error = "Cliente existente!!!";
                    return View(clienteVm);
                }

                if (servicio.Existe(cliente.Correo))
                {
                    ViewBag.Error = "Correo duplicado!!!";
                    return View(clienteVm);

                }

                cliente.Clave = HelperCliente.ConvertirSha256(cliente.Clave);
                servicio.Guardar(cliente);
                ViewData["Nombres"] = null;
                ViewData["Apellido"] = null;
                ViewData["correo"] = null;

                return RedirectToAction("LogIn");

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(clienteVm);
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
                Cliente cliente = servicio.GetClientePorCorreo(correo);
                if (cliente == null)
                {
                    ViewBag.Error = "Correo ingresado inexistente!!!";
                    return View();
                }
                cliente.Reestablecer = true;
                string clave = HelperCliente.GenerarClave();

                string asunto = "Recuperación de Cuenta";
                string mensaje = $"<h3>Su cuenta se ha recuperado satisfactoriamente.<h3><br/><p>Su nueva contraseña es {clave}</p>";
                bool respuesta = HelperCliente.EnviarCorreo(cliente.Correo, asunto, mensaje);

                if (respuesta)
                {
                    try
                    {
                        cliente.Clave = HelperCliente.ConvertirSha256(clave);
                        servicio.Guardar(cliente);
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
                    ViewBag.Error = "No se pudo enviar el correo!!!";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }


        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(string clienteId, string claveActual, string nuevaClave, string confirmarClave)
        {
            try
            {
                Cliente u = servicio.GetClientePorId(int.Parse(clienteId));
                if (u.Clave != HelperCliente.ConvertirSha256(claveActual))
                {
                    TempData["clienteId"] = u.ClienteId;
                    ViewBag.Error = "Clave errónea!!!";
                    return View();
                }
                else if (nuevaClave != confirmarClave)
                {
                    TempData["clienteId"] = u.ClienteId;
                    ViewData["vClave"] = claveActual;
                    ViewBag.Error = "La nueva clave y su confirmación no coinciden!!!";
                    return View();
                }

                u.Clave = HelperCliente.ConvertirSha256(nuevaClave);
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
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            TempData["clienteId"] = null;
            return RedirectToAction("LogIn");
        }


    }
}
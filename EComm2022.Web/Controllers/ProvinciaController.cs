using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;

namespace EComm2022.Web.Controllers
{
    public class ProvinciaController : Controller
    {
        private readonly IServicioProvincias servicio;
        // GET: Provincia
        public ProvinciaController(IServicioProvincias servicio)
        {
            this.servicio = servicio;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarProvincias()
        {
            var lista = servicio.GetLista();
            return Json(new{data=lista}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProvincia(Provincia provinciaRecibida)
        {
            object resultado = null;
            string mensaje = string.Empty;
            try
            {
                mensaje = ValidarProvincia(provinciaRecibida);
                if (mensaje==String.Empty)
                {
                    if (!servicio.Existe(provinciaRecibida))
                    {
                        servicio.Guardar(provinciaRecibida);
                        resultado = provinciaRecibida.ProvinciaId;
                        mensaje = "Provincia agregada/editada";
                    }
                    else
                    {
                        resultado = 0;
                        mensaje = "Provincia existente!!!";
                    }
                }
                else
                {
                    resultado = 0;
                }
            }
            catch (Exception e)
            {
                resultado = 0;
                mensaje = e.Message;

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        private string ValidarProvincia(Provincia provincia)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(provincia.NombreProvincia))
            {
                sb.AppendLine("Nombre de la provincia requerido");
            }

            return sb.ToString();
        }
        [HttpPost]
        public JsonResult EliminarProvincia(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            try
            {
                Provincia p = servicio.GetProvinciaPorId(id);
                if (!servicio.EstaRelacionado(p))
                {
                    servicio.Borrar(p);
                    respuesta = true;
                    mensaje = "Registro eliminado";
                }
                else
                {
                    respuesta = false;
                    mensaje = "Registro relacionado... Baja denegada";
                }
            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = e.Message;
            }

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}
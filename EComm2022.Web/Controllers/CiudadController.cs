using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;
using Newtonsoft.Json;

namespace EComm2022.Web.Controllers
{
    public class CiudadController : Controller
    {
        private IServicioCiudades servicio;

        public CiudadController(IServicioCiudades servicio)
        {
            this.servicio = servicio;
        }
        // GET: Ciudad
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCiudadesPorPais(int id)
        {
            var lista = servicio.GetListaPorPais(id);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult ListarCiudades()
        {
            var lista = servicio.GetLista();
            return Json(new {data=lista}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GuardarCiudad(string objeto)
        {
            object resultado = null;
            string mensaje = string.Empty;
            try
            {
                Ciudad ciudadRecibida = new Ciudad();
                ciudadRecibida = JsonConvert.DeserializeObject<Ciudad>(objeto);

                mensaje = ValidarCiudad(ciudadRecibida);
                if (mensaje == String.Empty)
                {
                    if (!servicio.Existe(ciudadRecibida))
                    {
                        servicio.Guardar(ciudadRecibida);
                        resultado = ciudadRecibida.CiudadId;
                        mensaje = "Ciudad agregada/editada";
                    }
                    else
                    {
                        resultado = 0;
                        mensaje = "Ciudad existente!!!";
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

        private string ValidarCiudad(Ciudad ciudad)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(ciudad.NombreCiudad))
            {
                sb.AppendLine("Nombre de la ciudad requerido");
            }

            if (ciudad.ProvinciaId == 0)
            {
                sb.AppendLine("Debe seleccionar una provincia");
            }
            return sb.ToString();
        }

        [HttpPost]
        public JsonResult EliminarCiudad(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            try
            {
                Ciudad c = servicio.GetCiudadPorId(id);
                if (!servicio.EstaRelacionada(c))
                {
                    servicio.Borrar(c);
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

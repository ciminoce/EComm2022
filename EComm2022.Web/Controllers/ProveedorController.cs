using System;
using System.Text;
using System.Web.Mvc;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;
using Newtonsoft.Json;

namespace EComm2022.Web.Controllers
{
    public class ProveedorController : Controller
    {
        private IServicioProveedores servicio;

        public ProveedorController(IServicioProveedores servicio)
        {
            this.servicio = servicio;
        }
        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarProveedores()
        {
            var lista = servicio.GetLista();
            return Json(new {data=lista}, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GuardarProveedor(string objeto)
        {
            object resultado = null;
            string mensaje = string.Empty;
            try
            {
                Proveedor proveedorRecibido = new Proveedor();
                proveedorRecibido = JsonConvert.DeserializeObject<Proveedor>(objeto);

                mensaje = ValidarProveedor(proveedorRecibido);
                if (mensaje == String.Empty)
                {
                    if (!servicio.Existe(proveedorRecibido))
                    {
                        servicio.Guardar(proveedorRecibido);
                        resultado = proveedorRecibido.CiudadId;
                        mensaje = "Proveedor agregada/editada";
                    }
                    else
                    {
                        resultado = 0;
                        mensaje = "Proveedor existente!!!";
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

        private string ValidarProveedor(Proveedor proveedor)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(proveedor.NombreProveedor))
            {
                sb.AppendLine("Nombre del proveedor requerido" + Environment.NewLine);
            }
            if (string.IsNullOrEmpty(proveedor.Direccion))
            {
                sb.AppendLine("Dirección requerida" + Environment.NewLine);
            }


            if (proveedor.ProvinciaId == 0)
            {
                sb.AppendLine("Debe seleccionar una provincia" + Environment.NewLine);
            }
            if (proveedor.CiudadId == 0)
            {
                sb.AppendLine("Debe seleccionar una ciudad" + Environment.NewLine);
            }
            if (string.IsNullOrEmpty(proveedor.Telefono))
            {
                sb.AppendLine("Teléfono requerido" + Environment.NewLine);
            }

            return sb.ToString();

        }

    }
}
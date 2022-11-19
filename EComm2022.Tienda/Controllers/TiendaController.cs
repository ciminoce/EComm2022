using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Dtos.Producto;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Tienda.Models.Producto;

namespace EComm2022.Tienda.Controllers
{
    public class TiendaController : Controller
    {
        private IServicioCarritos servicioCarritos;
        private IServicioCategorias servicioCategorias;

        private IServicioMarcas servicioMarcas;
        private IServicioProductos servicioProductos;
        private IMapper mapper;

        public TiendaController(IServicioCategorias servicioCategorias, IServicioMarcas servicioMarcas, IServicioProductos servicioProductos,IServicioCarritos servicioCarritos)
        {
            this.servicioCarritos = servicioCarritos;
            this.servicioCategorias = servicioCategorias;
            this.servicioMarcas = servicioMarcas;
            this.servicioProductos = servicioProductos;
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            var lista = servicioCategorias.GetLista();

            return Json(new {data = lista}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMarcas(int categoriaId)
        {
            var lista = servicioMarcas.GetMarcasPorCategoria(categoriaId);
            return Json(new {data = lista}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProductos(int categoriaId, int marcaId)
        {
            var lista = servicioProductos.GetLista(categoriaId, marcaId);
            var listaVm = mapper.Map<List<ProductoListVm>>(lista);
            var jsonResultado= Json(new {data = listaVm}, JsonRequestBehavior.AllowGet);
            jsonResultado.MaxJsonLength = int.MaxValue;
            return jsonResultado;
        }

        public ActionResult DetalleProducto(int productoId)
        {
            ProductoDetalleDto producto =mapper
                .Map<ProductoDetalleDto>(servicioProductos
                    .GetProductoPorId(productoId));
            ProductoDetalleVm productoVm = mapper.Map<ProductoDetalleVm>(producto);
            return View(productoVm);
        }

        [HttpGet]
        public JsonResult CantidadEnCarrito()
        {
            var clienteId =((Cliente) Session["cliente"]).ClienteId;
            var cantidad = servicioCarritos.CantidadEnCarrito(clienteId);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarAlCarrito(int productoId)
        {
            var clienteId = ((Cliente)Session["cliente"]).ClienteId;
            string mensaje = string.Empty;
            bool resultado = false;
            try
            {
                servicioCarritos.AgregarAlCarrito(clienteId,productoId);
                mensaje = "Producto agregado al Carrito";
                resultado = true;
            }
            catch (Exception e)
            {
                mensaje = e.Message;
                resultado = false;

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}
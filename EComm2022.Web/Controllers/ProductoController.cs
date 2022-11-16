using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Web.Helpers;
using EComm2022.Web.Models.Producto;

namespace EComm2022.Web.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly IServicioProductos servicio;
        private readonly IServicioCategorias servicioCategorias;
        private readonly IServicioMarcas servicioMarcas;
        private readonly IMapper mapper;
        private string file = "";
        private readonly string folder = "~/Content/Imagenes/Productos/";
        public ProductoController(ServicioProductos servicio, ServicioMarcas servicioMarcas, ServicioCategorias servicioCategorias)
        {
            this.servicio = servicio;
            this.servicioCategorias = servicioCategorias;
            this.servicioMarcas = servicioMarcas;
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Producto
        public ActionResult Index()
        {
            var lista = servicio.GetLista();
            var listaVm = mapper.Map<List<ProductoListVm>>(lista);
            return View(listaVm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductoEditVm productoVm = new ProductoEditVm()
            {
                Marcas = servicioMarcas.GetLista(),
                Categorias = servicioCategorias.GetLista(),
                Activo = true,
            };
            return View(productoVm);
        }

        [HttpPost]
        public ActionResult Create(ProductoEditVm productoVm)
        {
            if (!ModelState.IsValid)
            {
                productoVm.Marcas = servicioMarcas.GetLista();
                productoVm.Categorias = servicioCategorias.GetLista();
                return View(productoVm);
            }

            try
            {
                var producto = mapper.Map<Producto>(productoVm);

                if (!servicio.Existe(producto))
                {
                    servicio.Guardar(producto);
                    //Pregunto si el modelo viene con archivo de imagen
                    if (productoVm.ImagenFile != null)
                    {
                        //Le pongo como nombre del archivo el id del producto
                        string nombreImagen = producto.ProductoId.ToString();
                        //Tomo la extensión del archivo que viene en el modelo
                        string extensionImagen = Path.GetExtension(productoVm.ImagenFile.FileName);
                        //Concateno el nombre del archivo con su extensión
                        file = $"{nombreImagen}{extensionImagen}";
                        //Uso el Helper para subir el archivo al servidor y traigo una respuesta
                        var response = HelperFile.UploadPhoto(productoVm.ImagenFile, folder, file);
                        //Si no se pudo subir asigno sin imagen al archivo
                        if (!response)
                        {
                            file = "SinImagenDisponible.jpg";
                        }
                    }
                    else
                    {
                        //Si viene sin imagen asigno sin imagen al archivo
                        file = "SinImagenDisponible.jpg";
                    }
                    //asigno el contenido al atributo imagen de la clase producto
                    producto.Imagen = $"{folder}{file}";
                    //Actualizo el producto con los datos de la imagen
                    servicio.Guardar(producto);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Producto existente!!!");
                    productoVm.Marcas = servicioMarcas.GetLista();
                    productoVm.Categorias = servicioCategorias.GetLista();

                    return View(productoVm);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                productoVm.Marcas = servicioMarcas.GetLista();
                productoVm.Categorias = servicioCategorias.GetLista();

                return View(productoVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = servicio.GetProductoPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de producto no encontrado");
            }
            ProductoEditVm productoVm = mapper.Map<ProductoEditVm>(producto);
            productoVm.Marcas = servicioMarcas.GetLista();
            productoVm.Categorias = servicioCategorias.GetLista();
            return View(productoVm);
        }
        [HttpPost]
        public ActionResult Edit(ProductoEditVm productoVm)
        {
            if (!ModelState.IsValid)
            {
                productoVm.Marcas = servicioMarcas.GetLista();
                productoVm.Categorias = servicioCategorias.GetLista();

                return View(productoVm);
            }
            try
            {
                var producto = mapper.Map<Producto>(productoVm);

                if (!servicio.Existe(producto))
                {
                    servicio.Guardar(producto);
                    //Pregunto si el modelo viene con archivo de imagen
                    if (productoVm.ImagenFile != null)
                    {
                        //Le pongo como nombre del archivo el id del producto
                        string nombreImagen = producto.ProductoId.ToString();
                        //Tomo la extensión del archivo que viene en el modelo
                        string extensionImagen = Path.GetExtension(productoVm.ImagenFile.FileName);
                        //Concateno el nombre del archivo con su extensión
                        file = $"{nombreImagen}{extensionImagen}";
                        //Uso el Helper para subir el archivo al servidor y traigo una respuesta
                        var response = HelperFile.UploadPhoto(productoVm.ImagenFile, folder, file);
                        //Si no se pudo subir asigno sin imagen al archivo
                        if (!response)
                        {
                            file = "SinImagenDisponible.jpg";
                        }
                    }
                    else
                    {
                        //Si viene sin imagen asigno sin imagen al archivo
                        file = "SinImagenDisponible.jpg";
                    }
                    //asigno el contenido al atributo imagen de la clase producto
                    producto.Imagen = $"{folder}{file}";
                    //Actualizo el producto con los datos de la imagen
                    servicio.Guardar(producto);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Producto existente!!!");
                    productoVm.Marcas = servicioMarcas.GetLista();
                    productoVm.Categorias = servicioCategorias.GetLista();

                    return View(productoVm);
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                productoVm.Marcas = servicioMarcas.GetLista();
                productoVm.Categorias = servicioCategorias.GetLista();

                return View(productoVm);

            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

                Producto producto = servicio.GetProductoPorId(id.Value);
                if (producto == null)
                {
                    return HttpNotFound("Código de marca inexistente!!!");
                }

                ProductoListVm productoVm = mapper.Map<ProductoListVm>(producto);
                return View(productoVm);

            }
        }
    }
}
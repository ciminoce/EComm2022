using System;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Web.Models.Categoria;

namespace EComm2022.Web.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly IServicioCategorias servicio;
        private readonly IMapper mapper;

        public CategoriaController(ServicioCategorias servicio)
        {
            this.servicio = servicio;
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Categoria
        public ActionResult Index()
        {
            var lista = servicio.GetLista();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaEditVm categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }

            try
            {
                Categoria categoria = mapper.Map<Categoria>(categoriaVm);
                if (servicio.Existe(categoria))
                {
                    ModelState.AddModelError(string.Empty,"Categoría existente!!!");
                    return View(categoriaVm);
                }

                categoria.Activa = true;
                servicio.Guardar(categoria);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,e.Message);
                return View(categoriaVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = servicio.GetCategoriaPorId(id.Value);
            if (categoria==null)
            {
                return HttpNotFound("Código de categoría inexistente!!!");
            }

            CategoriaEditVm categoriaVm = mapper.Map<CategoriaEditVm>(categoria);
            return View(categoriaVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaEditVm categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }

            Categoria categoria = mapper.Map<Categoria>(categoriaVm);
            try
            {
                if (servicio.Existe(categoria))
                {
                    ModelState.AddModelError(string.Empty,"Categoría existente!!!");
                    return View(categoriaVm);
                }
                servicio.Guardar(categoria);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,e.Message);
                return View(categoriaVm);
            }
        }

    }
}
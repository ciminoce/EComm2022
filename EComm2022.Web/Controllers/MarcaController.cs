using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EComm2022.Entidades.Entidades;
using EComm2022.Servicios.Servicios;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Web.Models.Marca;
using PagedList;

namespace EComm2022.Web.Controllers
{
    [Authorize]
    public class MarcaController : Controller
    {
        private IServicioMarcas servicio;
        private IMapper mapper;

        public MarcaController(ServicioMarcas servicio)
        {
            this.servicio =servicio;
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Marca
        public ActionResult Index(int? pageSize,
            int? page)
        {
            page = (page ?? 1);
            ViewBag.CurrentItemsPerPage = pageSize ?? 10; // default 10
            var lista = servicio.GetLista();
            return View(lista.ToPagedList((int)page, pageSize ?? 10));

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MarcaEditVm marcaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(marcaVm);
            }

            Marca marca = mapper.Map<Marca>(marcaVm);
            try
            {
                if (servicio.Existe(marca))
                {
                    ModelState.AddModelError(string.Empty,"Marca repetida!!!");
                    return View(marcaVm);
                }

                marca.Activo = true;
                servicio.Guardar(marca);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,e.Message);
                return View(marcaVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {


                Marca marca = servicio.GetMarcaPorId(id.Value);
                if (marca==null)
                {
                    return HttpNotFound("Código de marca inexistente!!!");
                }

                MarcaEditVm marcaVm = mapper.Map<MarcaEditVm>(marca);
                return View(marcaVm);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MarcaEditVm marcaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(marcaVm);
            }

            Marca marca = mapper.Map<Marca>(marcaVm);
            try
            {
                if (servicio.Existe(marca))
                {
                    ModelState.AddModelError(string.Empty,"Marca existente!!!");
                    return View(marcaVm);
                }
                servicio.Guardar(marca);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(marcaVm);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Marca marca = servicio.GetMarcaPorId(id.Value);
            if (marca==null)
            {
                return HttpNotFound("Código de marca inexistente!!!");
            }

            MarcaEditVm marcaVm = mapper.Map<MarcaEditVm>(marca);
            return View(marcaVm);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Marca marca = servicio.GetMarcaPorId(id);
            try
            {
                if (servicio.EstaRelacionado(marca))
                {
                    MarcaEditVm marcaVm = mapper.Map<MarcaEditVm>(marca);
                    ModelState.AddModelError(string.Empty,"Marca relacionada!!!");
                    return View(marcaVm);
                }

                servicio.Borrar(marca);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                MarcaEditVm marcaVm = mapper.Map<MarcaEditVm>(marca);
                ModelState.AddModelError(string.Empty, e.Message);
                return View(marcaVm);
            }
        }
    }
}
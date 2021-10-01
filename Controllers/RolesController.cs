using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolRepository _repository;

        public RolesController(IRolRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var listaRoles = await _repository.ObtenerListaRoles();
            return View(listaRoles);
        }

        public IActionResult NuevoRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoRol(RolCreacionModel model)
        {
            if (ModelState.IsValid)
            {
                await _repository.InsertarRol(model);
                //guardar en la bd
                return RedirectToAction("Index","Roles");
            }
            return View("NuevoRol", model);
        }

        public async Task<IActionResult> ActualizarRol(int id)
        {
            var rol = await _repository.ObtenerRolPorId(id);
            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarRol(RolEdicionModel model)
        {
            if(ModelState.IsValid)
            {
                await _repository.ActualizarRol(model);
                //guardar bd
                return RedirectToAction("Index");

            }

            return View(model);
        }

        public async Task<IActionResult> EliminarRol(int id)
        {
            var rol = await _repository.ObtenerRolPorId(id);
            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarRol(RolEdicionModel model)
        {
            await _repository.EliminarRol(model.Id);
            return RedirectToAction("Index");
        }
    }
    
}

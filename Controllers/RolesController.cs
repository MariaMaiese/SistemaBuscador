using Microsoft.AspNetCore.Mvc;
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
    }
}

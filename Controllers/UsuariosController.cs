using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class UsuariosController:Controller
    {
        private readonly IUsuarioRepository _repository;

        public UsuariosController(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NuevoUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NuevoUsuario(UsuarioCreacionModel model) //metodo para recibir la informcion de los usuarios y recibe como parametro el modelo
        {
            if(ModelState.IsValid) //si los datos estan correctos segun las validaciones del modelo
            {
                //guardar el usuario en la bd
                _repository.InsertarUsuario(model);
                return View("Index");
            }


            return View(model);
        }
    }
}

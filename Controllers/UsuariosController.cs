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
        public async Task< IActionResult> Index()
        {
            var listaUaurio = await _repository.ObtenerListaUsuarios();
            return View(listaUaurio);
        }

        public IActionResult NuevoUsuario()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevoUsuario(UsuarioCreacionModel model) //metodo para recibir la informcion de los usuarios y recibe como parametro el modelo
        {
            if(ModelState.IsValid) //si los datos estan correctos segun las validaciones del modelo
            {
                //guardar el usuario en la bd
                await _repository.InsertarUsuario(model);
                return RedirectToAction("Index"); //como la vista del index esta usando el modelo, tenemos que pasarle un modelo con los usuarios de la bd
            }


            return View(model);
        }
        public async Task<IActionResult> ActualizarUsuario([FromRoute] int id) //metodo para editar usuarios, capturamos de la url el id del usuario
        {
            var usuario = await _repository.ObtenerUsuarioPorId(id); //en la variable usuario guardará el objeto del usuario por el id

            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(UsuarioEdicionModel model)
        {
            await _repository.ActualizarUsuario(model);
            return RedirectToAction("Index");
        }
        public  IActionResult  CambiarPassword(int id) 
        {
            ViewBag.idUsuario = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(UsuarioCambioPasswordModel model)
        {
            await _repository.ActualizarPassword(model);
            return RedirectToAction("Index");
        }
    }
}

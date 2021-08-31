using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] // aqui se indica que nuestro form su metodo es post
        public IActionResult Login(LoginViewModel model) // aqui se indica que tipo de dato recibe el metodo, en el index ingresamos que el modelo es LoginViewModel
        {
            var repo = new LoginRepository();
            if(ModelState.IsValid)
            {
                if (repo.UserExist(model.Usuario, model.Password))
                {
                    Guid sesionId = Guid.NewGuid();
                    HttpContext.Session.SetString("sessionId", sesionId.ToString());
                    Response.Cookies.Append("sessionId", sesionId.ToString());
                    return View("Privacy");

                }
                else //error personalizado
                {
                    ModelState.AddModelError(string.Empty, "El usuario o contraseña no es válido");
                }
            }
            return View("Index", model);

        }

        public IActionResult Privacy()
        {
            string sessionId = Request.Cookies["sessionId"];
            if(string.IsNullOrEmpty(sessionId) || !sessionId.Equals(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Prueba()
        {
            string sessionId = Request.Cookies["sessionId"];
            if (string.IsNullOrEmpty(sessionId) || !sessionId.Equals(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToAction("Index");
            }
            return View();
      
        }
            

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

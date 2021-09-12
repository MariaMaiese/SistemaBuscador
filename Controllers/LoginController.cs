using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] // aqui se indica que nuestro form su metodo es post
        public async Task<IActionResult> Login(LoginViewModel model) // aqui se indica que tipo de dato recibe el metodo, en el index ingresamos que el modelo es LoginViewModel
        {
            //var repo = new LoginRepository();
            if (ModelState.IsValid)
            {
                if (await _loginRepository.UserExist(model.Usuario, model.Password))
                {
                    _loginRepository.SetSessionAndCookie(HttpContext);
                    //esta funcionalidad no corresponde al controlador de home, porque es asignacion de variable de sesión
                    //Guid sesionId = Guid.NewGuid();
                    //HttpContext.Session.SetString("sessionId", sesionId.ToString());
                    //Response.Cookies.Append("sessionId", sesionId.ToString());
                    return RedirectToAction("Index","Home");

                }
                else //error personalizado
                {
                    ModelState.AddModelError(string.Empty, "El usuario o contraseña no es válido");
                }
            }
            return View("Index", model);

        }
    }
}

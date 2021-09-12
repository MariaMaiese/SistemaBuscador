using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Filters
{
    public class SessionFilter : IActionFilter
    {
        //se ejecuta ANTES de que calquier controlador ejecute la accion
        public void OnActionExecuting(ActionExecutingContext context)  // el contexto tiene todas las variables de una peticion hppt
        {
            string sessionId = context.HttpContext.Request.Cookies["sessionId"];
            if (string.IsNullOrEmpty(sessionId) || !sessionId.Equals(context.HttpContext.Session.GetString("sessionId")))
            {
                //return RedirectToAction("Index"); // no es un controlador, no se puede hacer redirect
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }
        //se ejecuta DESOUÉS de que el controlador ejecute la accion
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

    
    }
}

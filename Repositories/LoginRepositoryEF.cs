using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class LoginRepositoryEF : ILoginRepository
    {
        private readonly ApplicationDbContext _context; // contexto de la bd

        public LoginRepositoryEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SetSessionAndCookie(HttpContext context)
        {         
                Guid sesionId = Guid.NewGuid();
                context.Session.SetString("sessionId", sesionId.ToString());
            context.Response.Cookies.Append("sessionId", sesionId.ToString());   
        }

        public async Task<bool> UserExist(string usuario, string password)
        {
            var resultado = false;

            //logica de EF

            var usuarioBD = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.NombreUsuario == usuario && x.Password == password);

            if (usuarioBD != null)
            {
                resultado = true;
            }

            return resultado;
        }
    }
}

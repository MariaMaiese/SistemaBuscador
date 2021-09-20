using Microsoft.EntityFrameworkCore;
using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class RolRepository :IRolRepository
    {
        private readonly ApplicationDbContext _context;

        public RolRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RolListaModel>> ObtenerListaUsuarios()
        {
            var respuesta = new List<RolListaModel>(); //creando la variable de respuesta que es un listado
            var listaDelaBd = await _context.Roles.ToListAsync(); //nos traemos los objetos de la bd gracias al _context

            //hacemos un mapeo de entidades: a partir de una entidad la paso a otra entidad
            foreach (var rolBd in listaDelaBd)
            {
                var newRolLista = new RolListaModel()
                {
                    Id = rolBd.Id,
                    Nombre = rolBd.Nombre,
                };
                respuesta.Add(newRolLista); //luego de mapear lo que viene de la BD con nuestro modelo, agrego el usuario a la respuesta que es una lista de usuarios
            }

            return respuesta;

        }
    }
}

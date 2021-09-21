using Microsoft.EntityFrameworkCore;
using SistemaBuscador.Entities;
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
        public async Task<List<RolListaModel>> ObtenerListaRoles()
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

        public async Task InsertarRol(RolCreacionModel model)
        {
            var nuevoRol = new Rol() //estamos creando un objeto que tendrá las propiedades de la entidad usuario
            {
                Nombre = model.Nombre,  //el nombre del rol será igual a lo que venga en el modelo
                
            };
            _context.Roles.Add(nuevoRol);  //agrego al rol en la bd
            await _context.SaveChangesAsync();  //guardo los cambios en la bd
        }
        public async Task<RolEdicionModel> ObtenerRolPorId(int id)
        {
            //en respuesta guardamos el objeto del modelo 
            var respuesta = new RolEdicionModel() { };
            //en usuariodb guardamos el objeto de la bd que haga match con el id del parametro
            var roldb = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id); //lenguaje linq que emula sentencias sql
            if (roldb != null)
            {
                respuesta.Id = roldb.Id;
                respuesta.Nombre = roldb.Nombre;
            }

            return respuesta;
        }
        public async Task ActualizarRol(RolEdicionModel model)
        {
            var RolDb = await _context.Roles.FirstOrDefaultAsync(x => x.Id == model.Id);
            RolDb.Nombre = model.Nombre;
            
            await _context.SaveChangesAsync();
        }

        public async Task EliminarRol(int id)
        {
            var rol = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }

    }
     
}

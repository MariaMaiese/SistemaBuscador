using Microsoft.EntityFrameworkCore;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using SistemaBuscador.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class UsuarioRepository :IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISeguridad _seguridad;

        public UsuarioRepository(ApplicationDbContext context, ISeguridad seguridad) //estamos inyectando la dependencia de la interfaz ISeguridad junto con el dbcontext
        {
            _context = context;
            _seguridad = seguridad;
        }
        public async Task InsertarUsuario(UsuarioCreacionModel model)
        {
            var nuevoUsuario = new Usuario() //estamos creando un objeto que tendrá las propiedades de la entidad usuario
            {
                NombreUsuario = model.NombreUsuario,  //el nombre usuario será igual a lo que venga en el modelo
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                RolId = (int)model.RolId,  //se hizo un convert implicito porque en el modeo acpta nulos pero en la entidad no
                Password = _seguridad.Encriptar(model.Password) //aqi estamos usando el metodo encriptar de nuestra interfaz de seguridad
                                                                //y le estamos pasando la contraseña que viene del modelo, es decir: la que ingresa el usuario
                
            };
            _context.Usuarios.Add(nuevoUsuario);  //agrego al usuario en la bd
            await _context.SaveChangesAsync();  //guardo los cambios en la bd
        }
        public async Task<List<UsuarioListaModel>> ObtenerListaUsuarios()
        {
            var respuesta = new List<UsuarioListaModel>(); //creando la variable de respuesta que es un listado
            var listaDelaBd = await _context.Usuarios.ToListAsync(); //nos traemos los objetos de la bd gracias al _context

            //hacemos un mapeo de entidades: a partir de una entidad la paso a otra entidad
            foreach (var usuariobd in listaDelaBd)
            {
                var newUsuarioLista = new UsuarioListaModel()
                {
                    Id = usuariobd.Id,
                    Nombres = usuariobd.Nombres,
                    Apellidos = usuariobd.Apellidos,
                    NombreUsuario = usuariobd.NombreUsuario,
                    Rol = usuariobd.RolId
                };
                respuesta.Add(newUsuarioLista); //luego de mapear lo que viene de la BD con nuestro modelo, agrego el usuario a la respuesta que es una lista de usuarios
            }

            return respuesta;

        }

        public async Task<UsuarioEdicionModel> ObtenerUsuarioPorId(int id)
        {
            //en respuesta guardamos el objeto del modelo 
            var respuesta = new UsuarioEdicionModel() { };
            //en usuariodb guardamos el objeto de la bd que haga match con el id del parametro
            var usuariodb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id); //lenguaje linq que emula sentencias sql
            if (usuariodb!=null)
            {
                respuesta.Id = usuariodb.Id;
                respuesta.Nombres = usuariodb.Nombres;
                respuesta.Apellidos = usuariodb.Apellidos;
                respuesta.NombreUsuario = usuariodb.NombreUsuario;
                respuesta.RolId = usuariodb.RolId;
            }

            return respuesta;
        }

        public async Task ActualizarUsuario(UsuarioEdicionModel model)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(x=>x.Id == model.Id);
            usuarioDb.Nombres = model.Nombres;
            usuarioDb.Apellidos = model.Apellidos;
            usuarioDb.RolId = (int)model.RolId;
            await _context.SaveChangesAsync();
        }
        public async Task ActualizarPassword(UsuarioCambioPasswordModel model)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == model.Id);
            usuarioDb.Password =_seguridad.Encriptar(model.Password);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id); //buscamos el usuario que tenga el mmismo id en la bd
            _context.Usuarios.Remove(usuario); //eliminamos el usuario
            await _context.SaveChangesAsync(); // guardamos los cambios
        }
    }
    
}

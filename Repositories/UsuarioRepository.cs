using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class UsuarioRepository :IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    public async Task InsertarUsuario(UsuarioCreacionModel model)
        {
            var nuevoUsuario = new Usuario() //estamos creando un objeto que tendrá las propiedades de la entidad usuario
            {
                NombreUsuario = model.NombreUsuario,  //el nombre usuario será igual a lo que venga en el modelo
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                RolId = (int)model.RolId,  //se hizo un convert implicito porque en el modeo acpta nulos pero en la entidad no
                Password = model.Password
            };
            _context.Usuarios.Add(nuevoUsuario);  //agrego al usuario en la bd
            await _context.SaveChangesAsync();  //guardo los cambios en la bd
        }
    }
}

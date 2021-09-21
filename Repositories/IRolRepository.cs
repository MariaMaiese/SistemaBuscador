using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public interface IRolRepository
    {
        Task ActualizarRol(RolEdicionModel model);
        Task EliminarRol(int id);
        Task InsertarRol(RolCreacionModel model);
        Task<List<RolListaModel>> ObtenerListaRoles();
        Task<RolEdicionModel> ObtenerRolPorId(int id);
    }
}

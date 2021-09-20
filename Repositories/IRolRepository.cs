using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public interface IRolRepository
    {
        Task<List<RolListaModel>> ObtenerListaRoles();
    }
}

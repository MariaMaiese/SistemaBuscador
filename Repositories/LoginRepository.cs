using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        public void SetSessionAndCookie(HttpContext context)
        {
            Guid sesionId = Guid.NewGuid();
            context.Session.SetString("sessionId", sesionId.ToString());
            context.Response.Cookies.Append("sessionId", sesionId.ToString());
        }

        public async Task<bool> UserExist(string usuario, string password ) //bool porque la funcion devolvera un booleano
        {
            bool result = false;

            // conexion a la bd para que entregue un 0 o un 1

            string connectionString = "server=localhost;database=SistemaBuscador;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_check_user", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@user", usuario));
            cmd.Parameters.Add(new SqlParameter("@password", password));

            //abrir la conexion
            await sql.OpenAsync();
            int bdResult = (int)cmd.ExecuteScalar();   //ejecutar y que te devuelva un numero

            if(bdResult > 0)
            {
                result = true;
            }

            return result;
        }
    }
}

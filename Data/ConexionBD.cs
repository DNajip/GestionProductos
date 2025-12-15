using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace GestionProductos.Data
{
    internal class ConexionBD
    {
        // Cadena de conexión tomada desde App.config
        private static readonly string cadenaConexion =
            ConfigurationManager.ConnectionStrings["conexionBD"].ConnectionString;

        // Devuelve una nueva conexión lista para usarse
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}

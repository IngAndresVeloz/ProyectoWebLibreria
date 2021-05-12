using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
//conexion con el servidor sql (base de datos)
namespace DA
{
    public class Conexion
    {
        private static SqlConnection objConexion;
        private static string error;

        public static SqlConnection getConexion()
        {
            if (objConexion != null)
                return objConexion;
            objConexion = new SqlConnection();
            objConexion.ConnectionString = "Data Source = [localhost]; Initial Catalog = [dblibrary]; ID = [root]; Password = [05122019]";
            try
            {
                objConexion.Open(); //conexion sin problemas
                return objConexion;
            }
            catch (Exception e)
            {
                error = e.Message; //problema en la conexion
                return null;
            }
        }

        public static void cerrarConexion()
        {
            if (objConexion != null)
                objConexion.Close();
        }

    }
}

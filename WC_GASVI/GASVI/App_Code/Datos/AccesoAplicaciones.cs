using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// <summary>
	/// Descripci�n breve de Idiomas.
	/// </summary>
	public partial class AccesoAplicaciones
    {
        #region M�todos

        public static SqlDataReader ComprobarAcceso()
        {
            return SqlHelper.ExecuteSqlDataReader(Conexion.GetCadenaConexion,
                "P034_ACCESOAPLICACION", ConfigurationManager.AppSettings["CODIGO_APLICACION"]);
        }
        #endregion
    }
}

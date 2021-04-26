using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : MONEDAGV
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T422_MONEDA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/03/2011 10:02:40	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class MONEDAGV
	{
		#region Metodos

        public static SqlDataReader ObtenerCatalogo(bool bSoloActivas)
		{
			SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@bSoloActivas", SqlDbType.Bit, 1, bSoloActivas);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("GVT_MONEDA_CAT", aParam);
		}

        public static void UpdateMoneda(SqlTransaction tr, string t422_idmoneda, int t422_estado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t422_estado", SqlDbType.Bit, 1, t422_estado);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_MONEDAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_MONEDA_UPD", aParam);
        }

        public static void UpdateDefectoMoneda(SqlTransaction tr, string t422_idmoneda, string t422_idmoneda_old)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_old", SqlDbType.VarChar, 5, t422_idmoneda_old);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_MONEDA_DEFAULT_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_MONEDA_DEFAULT_UPD", aParam);
        }

		#endregion
	}
}

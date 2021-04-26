using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ESCENARIOSPAR
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T790_ESCENARIOSPAR
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/05/2012 9:16:04	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ESCENARIOSPAR
	{
		#region Metodos

        public static void InsertarEnCreacionEscenario(SqlTransaction tr, int t789_idescenario, string sPartidas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);
            aParam[i++] = ParametroSql.add("@sPartidas", SqlDbType.VarChar, 1000, sPartidas);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOSPAR_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOSPAR_INS", aParam);
        }

        public static int InsertarPartida(SqlTransaction tr, int t789_idescenario, int t437_idpartidaeco)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);
            aParam[i++] = ParametroSql.add("@t437_idpartidaeco", SqlDbType.Int, 4, t437_idpartidaeco);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ESCENARIOSPAR_INSPAR", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESCENARIOSPAR_INSPAR", aParam));
        }


		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T790_ESCENARIOSPAR.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/05/2012 9:16:04
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader ObtenerPartidas(SqlTransaction tr, int t789_idescenario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESCENARIO_PARTIDAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESCENARIO_PARTIDAS", aParam);
        }

        public static SqlDataReader ObtenerCatalogoEscenario(SqlTransaction tr, int t789_idescenario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESCENARIOPARTIDAS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESCENARIOPARTIDAS_CAT", aParam);
        }

		#endregion
	}
}

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
    /// Class	 : MOTIVOEX
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T424_MOTIVOEX
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	24/06/2011 10:02:40	
	/// </history>
	/// -----------------------------------------------------------------------------
    public partial class MOTIVOEX
    {
        #region Metodos 

        public static SqlDataReader ObtenerSN2()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GVT_SUPERNODO2_CAT", aParam);
        }

        public static SqlDataReader ObtenerMotivos(Nullable<bool> t423_estado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t423_estado", SqlDbType.Bit, 1, t423_estado);

            return SqlHelper.ExecuteSqlDataReader("GVT_MOTIVO_CAT", aParam);
        }

        public static SqlDataReader ObtenerSN2Ex(bool bSoloActivos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@bSoloActivos", SqlDbType.Bit, 1, bSoloActivos);
            return SqlHelper.ExecuteSqlDataReader("GVT_SN2EX_CAT", aParam);
        }

        public static SqlDataReader ObtenerMotivosEx(int t392_idsupernodo2)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, t392_idsupernodo2);

            return SqlHelper.ExecuteSqlDataReader("GVT_SN2MOTIVOEX_CAT", aParam);
        }

        public static void DeleteSN2MotivoExAll(SqlTransaction tr, int t392_idsupernodo2)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, t392_idsupernodo2);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_MOTIVOEX_DEL_ALL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_MOTIVOEX_DEL_ALL", aParam);
        }


        public static void DeleteSN2MotivoEx(SqlTransaction tr, int t392_idsupernodo2, short t423_idmotivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, t392_idsupernodo2);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_MOTIVOEX_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_MOTIVOEX_DEL", aParam);
        }

        public static int InsertSN2MotivoEx(SqlTransaction tr, int t392_idsupernodo2, short t423_idmotivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, t392_idsupernodo2);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_MOTIVOEX_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_MOTIVOEX_INS", aParam));
        }


		#endregion
	}
}

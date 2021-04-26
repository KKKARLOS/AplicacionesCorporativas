using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
    /// Class	 : EXCEPCIONAUTO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T429_EXCEPCIONAUTO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/07/2011 14:25:40	
	/// </history>
	/// -----------------------------------------------------------------------------
    public partial class EXCEPCIONAUTO
	{
		#region Metodos

        public static SqlDataReader CatalogoIntegrantes()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("GVT_EXCEPCIONAUTO_CAT", aParam);
        }

        public static SqlDataReader CatalogoPersonas(string t001_apellido1, string t001_apellido2, string t001_nombre, string excluidos, bool todos)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 20, t001_nombre);
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@excluidos", SqlDbType.VarChar, 8000, excluidos);
            aParam[i++] = ParametroSql.add("@todos", SqlDbType.Bit, 1, todos);

            return SqlHelper.ExecuteSqlDataReader("GVT_FICEPI_SEL", aParam);

        }

        public static SqlDataReader ObtenerMotivosExcepcionAuto(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            return SqlHelper.ExecuteSqlDataReader("GVT_EXCEPCIONAUTO_SEL", aParam);
        }

        public static void InsertExcepcionAuto(SqlTransaction tr, int t001_idficepi_prof, short t423_idmotivo, int t001_idficepi_resp, 
                                            int t001_idficepi_mod)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi_prof", SqlDbType.Int, 4, t001_idficepi_prof);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t001_idficepi_resp", SqlDbType.Int, 4, t001_idficepi_resp);
            aParam[i++] = ParametroSql.add("@t001_idficepi_mod", SqlDbType.Int, 4, t001_idficepi_mod);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_EXCEPCIONAUTO_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_EXCEPCIONAUTO_INS", aParam);
        }

        public static void DeleteExcepcionAuto(SqlTransaction tr, int t001_idficepi_prof, Nullable<short> t423_idmotivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi_prof", SqlDbType.Int, 4, t001_idficepi_prof);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_EXCEPCIONAUTO_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_EXCEPCIONAUTO_DEL", aParam);
        }

        public static int UpdateExcepcionAuto(SqlTransaction tr, int t001_idficepi_prof, short t423_idmotivo, int t001_idficepi_resp, int t001_idficepi_mod)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi_prof", SqlDbType.Int, 4, t001_idficepi_prof);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t001_idficepi_resp", SqlDbType.Int, 4, t001_idficepi_resp);
            aParam[i++] = ParametroSql.add("@t001_idficepi_mod", SqlDbType.Int, 4, t001_idficepi_mod);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_EXCEPCIONAUTO_UPD", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_EXCEPCIONAUTO_UPD", aParam));
        }


		#endregion
	}
}

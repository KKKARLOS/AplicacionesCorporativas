using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
    /// Class	 : MOTIVO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T423_MOTIVO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	24/06/2011 10:02:40	
	/// </history>
	/// -----------------------------------------------------------------------------
    public partial class MOTIVO
	{
		#region Metodos

        public static SqlDataReader CatalogoAprobadores(short t423_idmotivo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("GVT_APROBADORMOTIVO_SEL", aParam);
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

        public static SqlDataReader ObtenerMotivos(Nullable<bool> t423_estado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t423_estado", SqlDbType.Bit, 1, t423_estado);
            return SqlHelper.ExecuteSqlDataReader("GVT_MOTIVO_CAT", aParam);
        }

        public static void UpdateMotivo(SqlTransaction tr, short t423_idmotivo, string t423_denominacion, short t423_estado,
                                        int t423_cuenta, string t175_idcc)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t423_denominacion", SqlDbType.VarChar, 50, t423_denominacion);
            aParam[i++] = ParametroSql.add("@t423_estado", SqlDbType.Bit, 1, t423_estado);
            aParam[i++] = ParametroSql.add("@t423_cuenta", SqlDbType.Int, 4, t423_cuenta);
            aParam[i++] = ParametroSql.add("@t175_idcc", SqlDbType.Char, 4, t175_idcc);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_MOTIVO_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_MOTIVO_UPD", aParam);
        }

        public static void DeleteAprobadores(SqlTransaction tr, short t423_idmotivo, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_APROBADORMOTIVO_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_APROBADORMOTIVO_DEL", aParam);
        }

        public static int InsertAprobadores(SqlTransaction tr, short t423_idmotivo, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_APROBADORMOTIVO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_APROBADORMOTIVO_INS", aParam));
        }

        public static SqlDataReader ObtenerMotivosSolicitud(SqlTransaction tr, int t314_idusuario, string t431_idestado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t431_idestado", SqlDbType.Char, 1, t431_idestado);

            return SqlHelper.ExecuteSqlDataReader("GVT_MOTIVO_SOLICITUD", aParam);
        }
        public static SqlDataReader ObtenerMotivosNodoBeneficiario(SqlTransaction tr, int t314_idusuario, string t431_idestado, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t431_idestado", SqlDbType.Char, 1, t431_idestado);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);

            return SqlHelper.ExecuteSqlDataReader("GVT_MOTIVO_SOLICITUD_NODO", aParam);
        }
        
		#endregion
	}
}

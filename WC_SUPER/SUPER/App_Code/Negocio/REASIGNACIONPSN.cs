using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class REASIGNACIONPSN
	{
		#region Metodos

		public static void Insertar(SqlTransaction tr, int t305_idproyectosubnodo , int t314_idusuario , int t314_idusuario_responsable , int t304_idsubnodo , Nullable<bool> t475_procesado , string t475_excepcion)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t314_idusuario_responsable;
			aParam[3] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[3].Value = t304_idsubnodo;
			aParam[4] = new SqlParameter("@t475_procesado", SqlDbType.Bit, 1);
			aParam[4].Value = t475_procesado;
			aParam[5] = new SqlParameter("@t475_excepcion", SqlDbType.Text, 2147483647);
			aParam[5].Value = t475_excepcion;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_REASIGNACIONPSN_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_REASIGNACIONPSN_I", aParam);
		}
        public static void Insertar(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, int t314_idusuario_responsable, Nullable<bool> t706_procesado, string t706_excepcion)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario_responsable;
            aParam[3] = new SqlParameter("@t706_procesado", SqlDbType.Bit, 1);
            aParam[3].Value = t706_procesado;
            aParam[4] = new SqlParameter("@t706_excepcion", SqlDbType.Text, 2147483647);
            aParam[4].Value = t706_excepcion;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_REASIGNACIONPSN_RES_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_REASIGNACIONPSN_RES_I", aParam);
        }
		public static int Modificar(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, int t314_idusuario_responsable, int t304_idsubnodo, Nullable<bool> t475_procesado, string t475_excepcion)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t314_idusuario_responsable;
			aParam[3] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[3].Value = t304_idsubnodo;
			aParam[4] = new SqlParameter("@t475_procesado", SqlDbType.Bit, 1);
			aParam[4].Value = t475_procesado;
			aParam[5] = new SqlParameter("@t475_excepcion", SqlDbType.Text, 2147483647);
			aParam[5].Value = t475_excepcion;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_REASIGNACIONPSN_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_REASIGNACIONPSN_U", aParam);
		}
        public static int Modificar(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, int t314_idusuario_responsable, Nullable<bool> t706_procesado, string t706_excepcion)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario_responsable;
            aParam[3] = new SqlParameter("@t706_procesado", SqlDbType.Bit, 1);
            aParam[3].Value = t706_procesado;
            aParam[4] = new SqlParameter("@t706_excepcion", SqlDbType.Text, 2147483647);
            aParam[4].Value = t706_excepcion;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_REASIGNACIONPSN_RES_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_REASIGNACIONPSN_RES_U", aParam);
        }
        public static void DeleteAll(SqlTransaction tr, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;

			if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_REASIGNACIONPSN_DEL", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_REASIGNACIONPSN_DEL", aParam);
		}

        public static void DeleteReasig(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_REASIGNACIONPSN_RES_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_REASIGNACIONPSN_RES_DEL", aParam);
        }
        public static SqlDataReader CatalogoDestino(SqlTransaction tr, int t314_idusuario) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_REASIGNACIONPSN_CAT", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_REASIGNACIONPSN_CAT", aParam);
		}
        public static SqlDataReader CatalogoDestinoResp(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_REASIGNACIONPSN_RES_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_REASIGNACIONPSN_RES_CAT", aParam);
        }
		#endregion
	}
}

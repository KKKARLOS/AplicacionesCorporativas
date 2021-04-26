using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASUPERNODO2
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T396_FIGURASUPERNODO2
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	04/02/2008 15:19:27	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASUPERNODO2
    {
        #region Metodos

        public static SqlDataReader Figuras(Nullable<int> t392_idsupernodo2)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
            aParam[0].Value = t392_idsupernodo2;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO2_F", aParam);
        }
        public static SqlDataReader FigurasUsuario(Nullable<int> t392_idsupernodo2, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
            aParam[0].Value = t392_idsupernodo2;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO2_FU", aParam);
        }

        public static SqlDataReader CatalogoFiguras(int t392_idsupernodo2)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
            aParam[0].Value = t392_idsupernodo2;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO2_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t392_idsupernodo2, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
            aParam[0].Value = t392_idsupernodo2;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO2_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO2_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t392_idsupernodo2, int t314_idusuario, string t396_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
            aParam[0].Value = t392_idsupernodo2;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t396_figura", SqlDbType.Char, 1);
            aParam[2].Value = t396_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO2_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO2_D", aParam);
        }

        #endregion
    }
}

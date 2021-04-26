using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASUPERNODO4
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T398_FIGURASUPERNODO4
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	04/02/2008 15:19:27	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASUPERNODO4
    {
        #region Metodos

        public static SqlDataReader Figuras(Nullable<int> t394_idsupernodo4)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
            aParam[0].Value = t394_idsupernodo4;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO4_F", aParam);
        }
        public static SqlDataReader FigurasUsuario(Nullable<int> t394_idsupernodo4, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
            aParam[0].Value = t394_idsupernodo4;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO4_FU", aParam);
        }

        public static SqlDataReader CatalogoFiguras(int t394_idsupernodo4)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
            aParam[0].Value = t394_idsupernodo4;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO4_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t394_idsupernodo4, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
            aParam[0].Value = t394_idsupernodo4;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO4_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO4_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t394_idsupernodo4, int t314_idusuario, string t398_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
            aParam[0].Value = t394_idsupernodo4;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t398_figura", SqlDbType.Char, 1);
            aParam[2].Value = t398_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO4_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO4_D", aParam);
        }

        #endregion
    }
}

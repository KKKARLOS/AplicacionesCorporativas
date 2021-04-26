using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASUPERNODO1
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T395_FIGURASUPERNODO1
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	04/02/2008 15:19:27	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASUPERNODO1
    {
        #region Metodos

        public static SqlDataReader Figuras(Nullable<int> t391_idsupernodo1)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO1_F", aParam);
        }
        public static SqlDataReader FigurasUsuario(Nullable<int> t391_idsupernodo1, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO1_FU", aParam);
        }

        public static SqlDataReader CatalogoFiguras(int t391_idsupernodo1)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO1_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t391_idsupernodo1, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO1_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO1_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t391_idsupernodo1, int t314_idusuario, string t395_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t395_figura", SqlDbType.Char, 1);
            aParam[2].Value = t395_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO1_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO1_D", aParam);
        }

        #endregion
    }
}

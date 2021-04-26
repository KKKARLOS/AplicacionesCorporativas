using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASUPERNODO3
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T397_FIGURASUPERNODO3
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	04/02/2008 15:19:27	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASUPERNODO3
    {
        #region Metodos

        public static SqlDataReader Figuras(Nullable<int> t393_idsupernodo3)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO3_F", aParam);
        }
        public static SqlDataReader FigurasUsuario(Nullable<int> t393_idsupernodo3, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO3_FU", aParam);
        }

        public static SqlDataReader CatalogoFiguras(int t393_idsupernodo3)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO3_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t393_idsupernodo3, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO3_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO3_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t393_idsupernodo3, int t314_idusuario, string t397_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t397_figura", SqlDbType.Char, 1);
            aParam[2].Value = t397_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO3_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO3_D", aParam);
        }


        #endregion
    }
}

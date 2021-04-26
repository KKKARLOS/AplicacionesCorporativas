using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASUBNODO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T309_FIGURASUBNODO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	19/12/2007 15:07:28	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASUBNODO
    {
        #region Metodos

        public static SqlDataReader Figuras(Nullable<int> t304_idsubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUBNODO_F", aParam);
        }
        public static SqlDataReader FigurasUsuario(Nullable<int> t304_idsubnodo, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUBNODO_FU", aParam);
        }

        public static SqlDataReader CatalogoFiguras(int t304_idsubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUBNODO_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t304_idsubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUBNODO_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUBNODO_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t304_idsubnodo, int t314_idusuario, string t309_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t309_figura", SqlDbType.Char, 1);
            aParam[2].Value = t309_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASUBNODO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUBNODO_D", aParam);
        }
        #endregion
    }
}

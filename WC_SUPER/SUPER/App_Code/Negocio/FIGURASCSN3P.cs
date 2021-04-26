using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASCSN3P
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T490_FIGURASCSN3P
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	23/06/2009 12:50:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASCSN3P
    {
        #region Metodos

        public static SqlDataReader CatalogoFiguras(int t489_idcsn3p)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
            aParam[0].Value = t489_idcsn3p;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASCSN3P_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t489_idcsn3p, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
            aParam[0].Value = t489_idcsn3p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN3P_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN3P_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t489_idcsn3p, int t314_idusuario, string t490_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
            aParam[0].Value = t489_idcsn3p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t490_figura", SqlDbType.Char, 1);
            aParam[2].Value = t490_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN3P_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN3P_D", aParam);
        }

        #endregion
    }
}

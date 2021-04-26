using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASCSN1P
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T486_FIGURASCSN1P
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	23/06/2009 12:50:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASCSN1P
    {
        #region Metodos

        public static SqlDataReader CatalogoFiguras(int t485_idcsn1p)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
            aParam[0].Value = t485_idcsn1p;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASCSN1P_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t485_idcsn1p, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
            aParam[0].Value = t485_idcsn1p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN1P_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN1P_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t485_idcsn1p, int t314_idusuario, string t486_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
            aParam[0].Value = t485_idcsn1p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t486_figura", SqlDbType.Char, 1);
            aParam[2].Value = t486_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN1P_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN1P_D", aParam);
        }

        #endregion
    }
}

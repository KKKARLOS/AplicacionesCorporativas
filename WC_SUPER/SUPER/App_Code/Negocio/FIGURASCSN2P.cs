using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASCSN2P
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T488_FIGURASCSN2P
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	23/06/2009 12:50:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASCSN2P
    {
        #region Metodos

        public static SqlDataReader CatalogoFiguras(int t487_idcsn2p)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
            aParam[0].Value = t487_idcsn2p;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASCSN2P_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t487_idcsn2p, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
            aParam[0].Value = t487_idcsn2p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN2P_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN2P_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t487_idcsn2p, int t314_idusuario, string t488_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
            aParam[0].Value = t487_idcsn2p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t488_figura", SqlDbType.Char, 1);
            aParam[2].Value = t488_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN2P_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN2P_D", aParam);
        }

        #endregion
    }
}

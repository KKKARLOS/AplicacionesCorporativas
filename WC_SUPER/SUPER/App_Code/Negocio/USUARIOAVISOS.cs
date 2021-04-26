using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : USUARIOAVISOS
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T449_USUARIOAVISOS
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	28/04/2009 11:29:20	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class USUARIOAVISOS
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta en la tabla T449_USUARIOAVISOS todos los profesionales activos.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/09/2009 11:29:20
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertarTodos(SqlTransaction tr, int t448_idaviso)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
            aParam[0].Value = t448_idaviso;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIOAVISOS_TODOS_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOAVISOS_TODOS_I", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Borra de la tabla T449_USUARIOAVISOS todos los profesionales
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/09/2009 11:29:20
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void BorrarTodos(SqlTransaction tr, int t448_idaviso)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
            aParam[0].Value = t448_idaviso;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIOAVISOS_TODOS_D", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOAVISOS_TODOS_D", aParam);
        }

        #endregion
    }
}

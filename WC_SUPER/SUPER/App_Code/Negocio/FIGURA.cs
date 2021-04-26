using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURACLIENTE
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T401_FIGURACLIENTE
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	19/12/2007 15:07:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURA
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene las figuras de un usuario en un proyectosubnodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader getFigurasPSN(SqlTransaction tr, int nPSN, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[1].Value = nUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GETFIGURA_PSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETFIGURA_PSN", aParam);
        }

        public static SqlDataReader getCatalogoAsigFiguras(SqlTransaction tr, int nUsuario, int nNivel)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nNivel", SqlDbType.Int, 4);
            aParam[1].Value = nNivel;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAASIGNACION_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURAASIGNACION_C", aParam);
        }

        
        #endregion
    }
}

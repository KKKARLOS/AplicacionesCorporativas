using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : CONSUCONTA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T478_CONSUCONTA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	26/01/2010 16:49:27	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class CONSUCONTA
    {
        #region Metodos

        public static void DeleteByAnno(SqlTransaction tr, int nAnno)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[0].Value = nAnno;


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CONSUCONTA_DByAnno", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSUCONTA_DByAnno", aParam);
        }

        public static int ObtenerMesMaximo(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CONSUCONTA_MESMAX", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONSUCONTA_MESMAX", aParam));
        }

        #endregion
    }
}

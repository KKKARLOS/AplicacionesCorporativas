using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : NODOMODALIDADCONTRATO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T636_NODOMODALIDADCONTRATO
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class NODOMODALIDADCONTRATO
    {
        public static int Duplicar(SqlTransaction tr, int t303_idnodoOrigen, int t303_idnodoDestino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@idNodoOri", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodoOrigen;
            aParam[1] = new SqlParameter("@idNodoDes", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodoDestino;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NODOMODALIDADCONTRATO_I_COP", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODOMODALIDADCONTRATO_I_COP", aParam));
        }
    }
}

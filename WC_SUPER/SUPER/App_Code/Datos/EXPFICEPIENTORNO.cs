using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de EXPFICEPIENTORNO
    /// </summary>
    public partial class EXPFICEPIENTORNO
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reasigna entornos tecnologicos
        /// </summary>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public static void Reasignar(SqlTransaction tr, int idOrigen, string sDestino)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@idOrigen", SqlDbType.Int, 4, idOrigen),
                ParametroSql.add("@sDestino", SqlDbType.VarChar, 500, sDestino)
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_REASIGNAR_ENTORNOTEC", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_REASIGNAR_ENTORNOTEC", aParam);
        }

        #endregion
    }
}
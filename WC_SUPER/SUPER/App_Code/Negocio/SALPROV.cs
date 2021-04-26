using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class SALPROV
    {
        #region Metodos

        public static int DeleteGlobal(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SALPROV_D_ALL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SALPROV_D_ALL", aParam);
        }

        #endregion
    }
}

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class SALPROVCORO
	{
		#region Metodos

        public static SqlDataReader GetCatalogo()
		{
			SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_SALPROVCORO_CAT", aParam);
		}
        public static DataSet GetDatosParaValidacion()
        {
            return SqlHelper.ExecuteDataset("SUP_DATOSVALIDACIONSALPROV");
        }
        public static int numFilas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SALPROVCORO_Count", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SALPROVCORO_Count", aParam));
        }

		#endregion
	}
}

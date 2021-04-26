using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class CONSUCONTACORO
	{
		#region Metodos

        public static DataSet GetCatalogo(SqlTransaction tr)
		{
			SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CONSUCONTACORO_CAT", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CONSUCONTACORO_CAT", aParam);
		}

        public static DataSet GetDatosParaValidacion()
        {
            return SqlHelper.ExecuteDataset("SUP_DATOSVALIDACIONCONSUCONTA");
        }
        public static int numFilas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CONSUCONTACORO_Count", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONSUCONTACORO_Count", aParam));
        }

		#endregion
	}
}

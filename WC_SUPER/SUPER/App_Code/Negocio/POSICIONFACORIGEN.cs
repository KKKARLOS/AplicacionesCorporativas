using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class POSICIONFACORIGEN
	{
		#region Metodos

        public static SqlDataReader CatalogoByOrdenFac(SqlTransaction tr, int t610_idordenfac) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t619_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_POSICIONFACORIGEN_CATOF", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_POSICIONFACORIGEN_CATOF", aParam);
		}

		#endregion
	}
}

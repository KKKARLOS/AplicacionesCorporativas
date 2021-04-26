using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	public partial class MEDIOS
	{
		#region Metodos

        public static DataSet ObtenerMedios(int @t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.VarChar, 50);
            aParam[0].Value = @t001_idficepi;

            return SqlHelper.ExecuteDataset("GEM_CONSULTA_FIC_MED", aParam);
        }

        #endregion
	}
}

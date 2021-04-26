using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IB.Services.Super.Globales;

namespace IB.Services.Super.DAL
{
    public class PARAMETRIZACIONSUPER
    {
        public static SqlDataReader Select(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PARAMETRIZACIONSUPER_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_S", aParam);
        }
    }
}

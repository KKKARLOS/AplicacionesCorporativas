using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IB.Services.Super.Globales;

namespace IB.Services.Super.DAL
{
    public class PARAMETRO
    {
        public static SqlDataReader GetParametros(SqlTransaction tr, int t472_idconsulta)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametrosSql.add("@t472_idconsulta", SqlDbType.Int, 4, t472_idconsulta),
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PARAMETROCONSULTAPERSONAL_SByIdconsulta", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PARAMETROCONSULTAPERSONAL_SByIdconsulta", aParam);
        }
    }
}

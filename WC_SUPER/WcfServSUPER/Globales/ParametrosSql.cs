using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace IB.Services.Super.Globales
{
    public class ParametrosSql
    {
        public static SqlParameter add(string sNombre, SqlDbType oTipo, int nLongitud, object oValor)
        {
            SqlParameter oParam = new SqlParameter(sNombre, oTipo, (oTipo == SqlDbType.Text) ? 2147483647 : nLongitud);
            oParam.Value = oValor;
            return oParam;
        }
        public static SqlParameter add(string sNombre, SqlDbType oTipo, ParameterDirection oDirection)
        {
            SqlParameter oParam = new SqlParameter(sNombre, oTipo);
            oParam.Direction = oDirection;
            return oParam;
        }
    }
}

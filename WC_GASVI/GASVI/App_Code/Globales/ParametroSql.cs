using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public class ParametroSql
    {
        public static SqlParameter add(string sNombre, SqlDbType oTipo, int nLongitud, object oValor)
        {
            SqlParameter oParam = new SqlParameter(sNombre, oTipo, (oTipo == SqlDbType.Text)? 2147483647:nLongitud);
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
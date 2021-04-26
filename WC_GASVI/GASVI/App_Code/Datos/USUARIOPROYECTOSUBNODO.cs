using System;
using System.Configuration;
using System.Data;
//using System.Web;
using System.Data.SqlClient;
using System.Collections;

namespace GASVI.DAL
{
    public partial class USUARIOPROYECTOSUBNODO
    {
        public static SqlDataReader ObtenerFechasAsociacion(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETFECHASUSUARIOPSN_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETFECHASUSUARIOPSN_CAT", aParam);
        }
    }
}
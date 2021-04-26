using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Collections;

namespace GASVI.DAL
{
    public partial class DIALIMITECONTABILIZACIONGV
    {
        public static SqlDataReader Obtener(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_DIALIMITECONTABILIZACIONGV_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_DIALIMITECONTABILIZACIONGV_S", aParam);
        }

        public static void UpdateParametrizacion(SqlTransaction tr, byte t670_dialimitecontanoanterior, byte t670_dialimitecontmesanterior,
                                                byte t670_diapago, byte t670_vigenciaaparcadas, byte t670_avisoaparcadas)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t670_dialimitecontanoanterior", SqlDbType.TinyInt, 1, t670_dialimitecontanoanterior);
            aParam[i++] = ParametroSql.add("@t670_dialimitecontmesanterior", SqlDbType.TinyInt, 1, t670_dialimitecontmesanterior);
            aParam[i++] = ParametroSql.add("@t670_diapago", SqlDbType.TinyInt, 1, t670_diapago);
            aParam[i++] = ParametroSql.add("@t670_vigenciaaparcadas", SqlDbType.TinyInt, 1, t670_vigenciaaparcadas);
            aParam[i++] = ParametroSql.add("@t670_avisoaparcadas", SqlDbType.TinyInt, 1, t670_avisoaparcadas);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_DIALIMITECONTABILIZACIONGV_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_DIALIMITECONTABILIZACIONGV_UPD", aParam);
        }
    }

}
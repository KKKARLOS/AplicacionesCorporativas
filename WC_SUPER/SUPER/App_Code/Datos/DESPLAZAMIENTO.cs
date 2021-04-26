using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Collections;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    public partial class DESPLAZAMIENTO
    {
        public static SqlDataReader ObtenerDesplazamientos(SqlTransaction tr, int t314_idusuario, DateTime fec_desde, DateTime fec_hasta, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@fec_desde", SqlDbType.SmallDateTime, 4, fec_desde);
            aParam[i++] = ParametroSql.add("@fec_hasta", SqlDbType.SmallDateTime, 4, fec_hasta);
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_DESPLAZAMIENTOSECO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_DESPLAZAMIENTOSECO_CAT", aParam);
        }
    }
}
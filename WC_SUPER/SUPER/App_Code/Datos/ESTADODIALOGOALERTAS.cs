using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
    public class ESTADODIALOGOALERTAS
    {
        public static SqlDataReader ObtenerCatalogoEstados(SqlTransaction tr, bool bSoloCerrados)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@bSoloCerrados", SqlDbType.Bit, 1, bSoloCerrados);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESTADODIALOGOALERTAS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESTADODIALOGOALERTAS_CAT", aParam);
        }
    }

}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    public class HISTORICOEJECUTAALERTAS
    {
        public static SqlDataReader CatalogoUltimasEjecuciones(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_HISTORICOEJECUTAALERTAS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_HISTORICOEJECUTAALERTAS_CAT", aParam);
        }
    }
}
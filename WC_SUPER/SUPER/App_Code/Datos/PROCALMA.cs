using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de PROCALMA
    /// </summary>
    public class PROCALMA
    {
        public static SqlDataReader Catalogo(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROCALMA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROCALMA_CAT", aParam);
        }

        public static void Ejecutar(string sProc, SqlParameter[] parametros)
        {
            SqlHelper.ExecuteNonQuery(sProc, parametros);
        }

        public static DataSet AltaGestion(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            //aParam[0] = new SqlParameter("@nAnomes", SqlDbType.Int, 4);
            //aParam[0].Value = @nAnomes;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_ADM_ALTAGES_CAT", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_ADM_ALTAGES_CAT", aParam);
        }

    }
}

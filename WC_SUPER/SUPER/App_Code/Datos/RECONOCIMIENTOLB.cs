using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
    public partial class RECONOCIMIENTOLB
    {
        #region Metodos

        public static SqlDataReader ObtenerDatosReconocimiento(SqlTransaction tr, int t685_idlineabase, bool bSoloPendiente, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase),
                ParametroSql.add("@bSoloPendiente", SqlDbType.Bit, 1, bSoloPendiente),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_LINEABASE_GETRECONO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_LINEABASE_GETRECONO", aParam);
        }
        public static DataSet ObtenerDatosReconocimientoDS(SqlTransaction tr, int t685_idlineabase, bool bSoloPendiente, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase),
                ParametroSql.add("@bSoloPendiente", SqlDbType.Bit, 1, bSoloPendiente),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_LINEABASE_GETRECONO", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_LINEABASE_GETRECONO", aParam);
        }

        public static void ActualizarMesReconocimiento(SqlTransaction tr, int t688_idreconocimiento, Nullable<int> t688_anomes_recono)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t688_idreconocimiento", SqlDbType.Int, 4, t688_idreconocimiento);
            aParam[i++] = ParametroSql.add("@t688_anomes_recono", SqlDbType.Int, 4, t688_anomes_recono);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_RECONOCIMIENTOLB_UPDMES", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RECONOCIMIENTOLB_UPDMES", aParam);
        }

        
        #endregion
    }
}

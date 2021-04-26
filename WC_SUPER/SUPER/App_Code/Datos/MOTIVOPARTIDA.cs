using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
    public partial class MOTIVOPARTIDA
    {
        #region Metodos

        public static SqlDataReader ObtenerCatalogoEscenario(SqlTransaction tr, int t789_idescenario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_MOTIVOPARTIDA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_MOTIVOPARTIDA_CAT", aParam);
        }

        public static int InsertarMotivo(SqlTransaction tr, int t790_idescenariopar, Nullable<int> t303_idnodo, Nullable<int> t315_idproveedor, string t806_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t790_idescenariopar", SqlDbType.Int, 4, t790_idescenariopar);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t315_idproveedor", SqlDbType.Int, 4, t315_idproveedor);
            aParam[i++] = ParametroSql.add("@t806_motivo", SqlDbType.VarChar, 50, t806_motivo);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_MOTIVOPARTIDA_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_MOTIVOPARTIDA_INS", aParam));
        }
        public static void ActualizarMotivo(SqlTransaction tr, int t806_idmotivopartida, int t790_idescenariopar, Nullable<int> t303_idnodo, Nullable<int> t315_idproveedor, string t806_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t806_idmotivopartida", SqlDbType.Int, 4, t806_idmotivopartida);
            aParam[i++] = ParametroSql.add("@t790_idescenariopar", SqlDbType.Int, 4, t790_idescenariopar);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t315_idproveedor", SqlDbType.Int, 4, t315_idproveedor);
            aParam[i++] = ParametroSql.add("@t806_motivo", SqlDbType.VarChar, 50, t806_motivo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_MOTIVOPARTIDA_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MOTIVOPARTIDA_UPD", aParam);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    public class MESESCIERRE
    {
        #region Metodos


        public static SqlDataReader Obtener(SqlTransaction tr, int anno)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@anno", SqlDbType.Int, 4, anno)   
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_MESESCIERRE_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_MESESCIERRE_CAT", aParam);
        }

        public static int UpdatePrevCierreECO(SqlTransaction tr, int t855_anomes, Nullable<DateTime> t855_prevcierreeco)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t855_anomes", SqlDbType.Int, 4, t855_anomes),
                ParametroSql.add("@t855_prevcierreeco", SqlDbType.SmallDateTime, 4, t855_prevcierreeco)
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PREVISIONCIERREECO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREVISIONCIERREECO_U", aParam);
        }

        #endregion
    }
}
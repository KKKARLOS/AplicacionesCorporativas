using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de GestorSAP
    /// </summary>
    public class LIMITEALERTAS
    {
        #region Metodos


        public static SqlDataReader Obtener(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_LIMITEALERTAS_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_LIMITEALERTAS_C", aParam);
        }
        public static void Update(SqlTransaction tr, int t828_anomes, Nullable<DateTime> t828_limitealertas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t828_anomes", SqlDbType.Int, 4, t828_anomes);
            aParam[i++] = ParametroSql.add("@t828_limitealertas", SqlDbType.SmallDateTime, 4, t828_limitealertas);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_LIMITEALERTAS_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_LIMITEALERTAS_U", aParam);
        }
        #endregion
    }
}
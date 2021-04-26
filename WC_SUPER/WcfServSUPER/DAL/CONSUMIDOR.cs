using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IB.Services.Super.Globales;

namespace IB.Services.Super.DAL
{
    public class CONSUMIDOR
    {
        public static SqlDataReader Select(SqlTransaction tr, string t145_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametrosSql.add("@t145_idusuario", SqlDbType.VarChar, 10, t145_idusuario)
                };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_APPORIGENWS_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_APPORIGENWS_S", aParam);
        }
        /// <summary>
        /// Establece el nº de accesos erróneos de un consumidor de servicios web
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t145_idusuario"></param>
        /// <param name="t145_intentos"></param>
        public static void SetIntentos(SqlTransaction tr, string t145_idusuario, short t145_intentos)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametrosSql.add("@t145_idusuario", SqlDbType.VarChar, 10, t145_idusuario),
                ParametrosSql.add("@t145_intentos", SqlDbType.SmallInt, 2, t145_intentos)
                };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_APPORIGENWS_INTENTOS_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_APPORIGENWS_INTENTOS_U", aParam);
        }
    }
}

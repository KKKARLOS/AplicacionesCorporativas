using System;
using System.Configuration;
using System.Data;
//using System.Web;
using System.Data.SqlClient;
using System.Collections;

namespace GASVI.DAL
{
    public partial class Tooltips
    {
        public static SqlDataReader ObtenerTexto()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GVT_TEXTOSTOOLTIPGV_CAT", aParam);
        }
        
        public static void UpdateTooltips(SqlTransaction tr, string texto, short sOrigen)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@texto", SqlDbType.Text, 16, texto);
            aParam[i++] = ParametroSql.add("@origen", SqlDbType.SmallInt, 2, sOrigen);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_TEXTOSTOOLTIPGV_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_TEXTOSTOOLTIPGV_UPD", aParam);
        }

	}
}
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class OFICINA
    {
        public static SqlDataReader ObtenerOficinas()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_OFICINAS_CAT", aParam);
        }

        public static void UpdateOficinaLiquidadora(SqlTransaction tr, int t010_idoficina, int t010_idoficina_liquidadora)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.Int, 4, t010_idoficina);
            aParam[i++] = ParametroSql.add("@t010_idoficina_liquidadora", SqlDbType.Int, 4, t010_idoficina_liquidadora);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_OFICINA_LIQUIDADORA_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_OFICINA_LIQUIDADORA_UPD", aParam);
        }
    }
}


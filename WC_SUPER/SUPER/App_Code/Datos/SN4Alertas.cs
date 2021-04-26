using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de SN4Alertas
    /// </summary>
    public partial class SN4Alertas
    {
        //public NodoAlertas(){}
        public static void Update(SqlTransaction tr, int t822_idsn4alertas,
                                  Nullable<int> t822_parametro1, Nullable<int> t822_parametro2, Nullable<int> t822_parametro3)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t822_idsn4alertas", SqlDbType.Int, 4, t822_idsn4alertas);
            aParam[i++] = ParametroSql.add("@t822_parametro1", SqlDbType.Int, 4, t822_parametro1);
            aParam[i++] = ParametroSql.add("@t822_parametro2", SqlDbType.Int, 4, t822_parametro2);
            aParam[i++] = ParametroSql.add("@t822_parametro3", SqlDbType.Int, 4, t822_parametro3);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SN4ALERTAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SN4ALERTAS_UPD", aParam);
        }

        public static SqlDataReader CatalogoById(SqlTransaction tr, int t394_idsupernodo4)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t394_idsupernodo4", SqlDbType.Int, 4, t394_idsupernodo4);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SN4ALERTAS_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SN4ALERTAS_C2", aParam);
        }
    }
}
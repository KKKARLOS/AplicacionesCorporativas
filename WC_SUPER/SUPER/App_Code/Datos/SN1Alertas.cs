using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de SN1Alertas
    /// </summary>
    public partial class SN1Alertas
    {
        //public NodoAlertas(){}
        public static void Update(SqlTransaction tr, int t825_idsn1alertas,
                                  Nullable<int> t825_parametro1, Nullable<int> t825_parametro2, Nullable<int> t825_parametro3)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t825_idsn1alertas", SqlDbType.Int, 4, t825_idsn1alertas);
            aParam[i++] = ParametroSql.add("@t825_parametro1", SqlDbType.Int, 4, t825_parametro1);
            aParam[i++] = ParametroSql.add("@t825_parametro2", SqlDbType.Int, 4, t825_parametro2);
            aParam[i++] = ParametroSql.add("@t825_parametro3", SqlDbType.Int, 4, t825_parametro3);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SN1ALERTAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SN1ALERTAS_UPD", aParam);
        }

        public static SqlDataReader CatalogoById(SqlTransaction tr, int t391_idsupernodo1)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t391_idsupernodo1", SqlDbType.Int, 4, t391_idsupernodo1);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SN1ALERTAS_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SN1ALERTAS_C2", aParam);
        }
    }
}
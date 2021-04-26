using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de SN2Alertas
    /// </summary>
    public partial class SN2Alertas
    {
        //public NodoAlertas(){}
        public static void Update(SqlTransaction tr, int t824_idsn2alertas,
                                  Nullable<int> t824_parametro1, Nullable<int> t824_parametro2, Nullable<int> t824_parametro3)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t824_idsn2alertas", SqlDbType.Int, 4, t824_idsn2alertas);
            aParam[i++] = ParametroSql.add("@t824_parametro1", SqlDbType.Int, 4, t824_parametro1);
            aParam[i++] = ParametroSql.add("@t824_parametro2", SqlDbType.Int, 4, t824_parametro2);
            aParam[i++] = ParametroSql.add("@t824_parametro3", SqlDbType.Int, 4, t824_parametro3);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SN2ALERTAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SN2ALERTAS_UPD", aParam);
        }

        public static SqlDataReader CatalogoById(SqlTransaction tr, int t392_idsupernodo2)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, t392_idsupernodo2);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SN2ALERTAS_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SN2ALERTAS_C2", aParam);
        }
    }
}
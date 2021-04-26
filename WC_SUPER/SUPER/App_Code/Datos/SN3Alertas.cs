using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de SN3Alertas
    /// </summary>
    public partial class SN3Alertas
    {
        //public NodoAlertas(){}
        public static void Update(SqlTransaction tr, int t823_idsn3alertas,
                                  Nullable<int> t823_parametro1, Nullable<int> t823_parametro2, Nullable<int> t823_parametro3)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t823_idsn3alertas", SqlDbType.Int, 4, t823_idsn3alertas);
            aParam[i++] = ParametroSql.add("@t823_parametro1", SqlDbType.Int, 4, t823_parametro1);
            aParam[i++] = ParametroSql.add("@t823_parametro2", SqlDbType.Int, 4, t823_parametro2);
            aParam[i++] = ParametroSql.add("@t823_parametro3", SqlDbType.Int, 4, t823_parametro3);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SN3ALERTAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SN3ALERTAS_UPD", aParam);
        }

        public static SqlDataReader CatalogoById(SqlTransaction tr, int t393_idsupernodo3)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t393_idsupernodo3", SqlDbType.Int, 4, t393_idsupernodo3);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SN3ALERTAS_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SN3ALERTAS_C2", aParam);
        }
    }
}
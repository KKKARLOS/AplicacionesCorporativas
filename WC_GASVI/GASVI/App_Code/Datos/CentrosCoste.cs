using System;
using System.Configuration;
using System.Data;
//using System.Web;
using System.Data.SqlClient;
using System.Collections;

namespace GASVI.DAL
{
    public partial class CentrosCoste
    {
        public static SqlDataReader CatalogoEstructura()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GVT_ESTRUCTURA_CENCOS_CAT", aParam);
        }

        public static SqlDataReader CatalogoCenCos(Nullable<int> t303_idnodo, Nullable<int> t304_idsubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t304_idsubnodo", SqlDbType.Int, 4, t304_idsubnodo);

            return SqlHelper.ExecuteSqlDataReader("GVT_CENCOS_CAT", aParam);
        }

        public static void UpdateCenCos(SqlTransaction tr, string t175_idcc, short t175_estadogasvi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t175_idcc", SqlDbType.VarChar, 4, t175_idcc);
            aParam[i++] = ParametroSql.add("@t175_estadogasvi", SqlDbType.Bit, 1, t175_estadogasvi);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CENCOS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CENCOS_UPD", aParam);
        }

        public static SqlDataReader ObtenerNodosCCIberper(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETNODOS_CCIB", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETNODOS_CCIB", aParam);
        }

        public static SqlDataReader CatalogoCenCosEstructura()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GVT_CENCOS_ESTRUC_CAT", aParam);
        }

        public static SqlDataReader UpdateEstrucCenCos(SqlTransaction tr, Nullable<int> idNodo, Nullable<int> idSubNodo, string idCC)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, idNodo),
                ParametroSql.add("@t304_idsubnodo", SqlDbType.Int, 4, idSubNodo),
                ParametroSql.add("@t175_idcc", SqlDbType.VarChar, 4, idCC)
                
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_ESTRUORGA_CENCOS_UPD", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_ESTRUORGA_CENCOS_UPD", aParam);

        }
    }
}
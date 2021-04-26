using System;
using System.Configuration;
using System.Data;
//using System.Web;
using System.Data.SqlClient;
using System.Collections;

namespace GASVI.DAL
{
    public partial class DIETAKM
    {
        public static SqlDataReader CatalogoDietaKm()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GVT_DIETAKM_CAT", aParam);
        }

        public static int InsertImporteConvenio(SqlTransaction tr, string t069_descripcion, float t069_icdc,
                                                float t069_icmd, float t069_icda, float t069_icde,
                                                float t069_ick, short t069_estado)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t069_descripcion", SqlDbType.VarChar, 50, t069_descripcion);
            aParam[i++] = ParametroSql.add("@t069_icdc", SqlDbType.Money, 8, t069_icdc);
            aParam[i++] = ParametroSql.add("@t069_icmd", SqlDbType.Money, 8, t069_icmd);
            aParam[i++] = ParametroSql.add("@t069_icda", SqlDbType.Money, 8, t069_icda);
            aParam[i++] = ParametroSql.add("@t069_icde", SqlDbType.Money, 8, t069_icde);
            aParam[i++] = ParametroSql.add("@t069_ick", SqlDbType.Money, 8, t069_ick);
            aParam[i++] = ParametroSql.add("@t069_estado", SqlDbType.Bit, 1, t069_estado);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_DIETAKM_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_DIETAKM_INS", aParam));
        }

        public static void DeleteImporteConvenio(SqlTransaction tr, short t069_iddietakm)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t069_iddietakm", SqlDbType.TinyInt, 1, t069_iddietakm);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_DIETAKM_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_DIETAKM_DEL", aParam);
        }

        public static void UpdateImporteConvenio(SqlTransaction tr, short t069_iddietakm, string t069_descripcion, float t069_icdc,
                                                float t069_icmd, float t069_icda, float t069_icde,
                                                float t069_ick, short t069_estado)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t069_iddietakm", SqlDbType.TinyInt, 1, t069_iddietakm);
            aParam[i++] = ParametroSql.add("@t069_descripcion", SqlDbType.VarChar, 50, t069_descripcion);
            aParam[i++] = ParametroSql.add("@t069_icdc", SqlDbType.Money, 8, t069_icdc);
            aParam[i++] = ParametroSql.add("@t069_icmd", SqlDbType.Money, 8, t069_icmd);
            aParam[i++] = ParametroSql.add("@t069_icda", SqlDbType.Money, 8, t069_icda);
            aParam[i++] = ParametroSql.add("@t069_icde", SqlDbType.Money, 8, t069_icde);
            aParam[i++] = ParametroSql.add("@t069_ick", SqlDbType.Money, 8, t069_ick);
            aParam[i++] = ParametroSql.add("@t069_estado", SqlDbType.Bit, 1, t069_estado);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_DIETAKM_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_DIETAKM_UPD", aParam);
        }

	}
}
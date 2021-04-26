using System;
using System.Configuration;
using System.Data;
//using System.Web;
using System.Data.SqlClient;
using System.Collections;

namespace GASVI.DAL
{
    public partial class AnnoGasto
    {
        public static SqlDataReader CatalogoAnnogasto(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_ANNOGASTO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_ANNOGASTO_CAT", aParam);
        }

        public static void InsertAnnoGasto(SqlTransaction tr, int t419_anno, DateTime t419_fdesde, DateTime t419_fhasta, int t001_idficepi, DateTime t419_fmodif)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t419_anno", SqlDbType.SmallInt, 2, t419_anno);
            aParam[i++] = ParametroSql.add("@t419_fdesde", SqlDbType.SmallDateTime, 4, t419_fdesde);
            aParam[i++] = ParametroSql.add("@t419_fhasta", SqlDbType.SmallDateTime, 4, t419_fhasta);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t419_fmodif", SqlDbType.SmallDateTime, 4, t419_fmodif);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ANNOGASTO_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ANNOGASTO_INS", aParam);
        }

        public static void DeleteAnnoGasto(SqlTransaction tr, int t419_anno)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t419_anno", SqlDbType.SmallInt, 2, t419_anno);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ANNOGASTO_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ANNOGASTO_DEL", aParam);
        }

        public static void UpdateAnnoGasto(SqlTransaction tr, int idAnno, int t419_anno, DateTime t419_fdesde, DateTime t419_fhasta, int t001_idficepi, DateTime t419_fmodif)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idAnno", SqlDbType.SmallInt, 2, idAnno);
            aParam[i++] = ParametroSql.add("@t419_anno", SqlDbType.SmallInt, 2, t419_anno);
            aParam[i++] = ParametroSql.add("@t419_fdesde", SqlDbType.SmallDateTime, 4, t419_fdesde);
            aParam[i++] = ParametroSql.add("@t419_fhasta", SqlDbType.SmallDateTime, 4, t419_fhasta);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t419_fmodif", SqlDbType.SmallDateTime, 4, t419_fmodif);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ANNOGASTO_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ANNOGASTO_UPD", aParam);
        }

	}
}
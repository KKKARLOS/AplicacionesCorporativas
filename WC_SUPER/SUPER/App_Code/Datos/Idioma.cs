using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de EntornoTecno
    /// </summary>
    public partial class Idioma
    {
        #region Metodos

        public static SqlDataReader obtenerIdioma(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_NUEVOSIDIOMASFICEPI", aParam);
        }

        public static SqlDataReader obtenerNivelIdioma()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_NIVELIDIOMAS", aParam);
        }

        public static SqlDataReader MiCvIdiomas(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_IDIOMAFIC_CAT", aParam);
        }

        public static SqlDataReader CatalogoIdiomas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_IDIOMAS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr,"SUP_CVT_IDIOMAS_CAT", aParam);
        }

        public static int Insert(SqlTransaction tr, string T020_DESCRIPCION)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T020_DESCRIPCION", SqlDbType.VarChar, 100, T020_DESCRIPCION);
            
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_IDIOMAS_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_IDIOMAS_INS", aParam));
        }

        public static void Delete(SqlTransaction tr, int T020_IDCODIDIOMA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T020_IDCODIDIOMA", SqlDbType.Int, 4, T020_IDCODIDIOMA);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_IDIOMAS_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_IDIOMAS_DEL", aParam);
        }

        public static void Update(SqlTransaction tr, int T020_IDCODIDIOMA, string T020_DESCRIPCION)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T020_IDCODIDIOMA", SqlDbType.Int, 4, T020_IDCODIDIOMA);
            aParam[i++] = ParametroSql.add("@T020_DESCRIPCION", SqlDbType.VarChar, 100, T020_DESCRIPCION);
            
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_IDIOMAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_IDIOMAS_UPD", aParam);
        }

        public static SqlDataReader MiCvIdiomasHTML(SqlTransaction tr, int t001_idficepi, int bFiltros, Nullable<int> t020_idcodidioma, Nullable<int> nivelidioma)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@bfiltros", SqlDbType.Int, 4, bFiltros);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.SmallInt, 2, t020_idcodidioma);
            aParam[i++] = ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_IDIOMAFICRP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_IDIOMAFICRP", aParam);
        }

        /// <summary>
        /// Obtiene una lista de los titulos de idiomas cuyo código de idioma se pasa en sListaIds 
        /// + los titulos de idiomas cuya denominación está en sListaDens y existe algun profesional de slFicepis que lo tiene
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="slFicepis"></param>
        /// <param name="sListaIds"></param>
        /// <param name="sListaDens"></param>
        /// <returns></returns>
        public static SqlDataReader GetListaPorProfesional(SqlTransaction tr, string slFicepis, string sListaIds, string sListaDens)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TMP_FICEPI", SqlDbType.Structured, SqlHelper.GetDataTableCod(slFicepis));
            aParam[i++] = ParametroSql.add("@TMP_COD_IDIOMA", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaIds));
            aParam[i++] = ParametroSql.add("@TMP_DEN_TITIDIOMA", SqlDbType.Structured, SqlHelper.GetDataTableDen(sListaDens));

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITIDIOMA_EXPORT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TITIDIOMA_EXPORT", aParam);
        }
        public static SqlDataReader GetDocsExportacion(SqlTransaction tr, string slFicepis, string slDenominaciones)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TMP_FICEPI", SqlDbType.Structured, SqlHelper.GetDataTableCod(slFicepis));
            aParam[i++] = ParametroSql.add("@TMP_DEN_TITIDIOMA", SqlDbType.Structured, SqlHelper.GetDataTableDen(slDenominaciones));

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITIDIOMA_EXPORT_DOCS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TITIDIOMA_EXPORT_DOCS", aParam);
        }
        #endregion
    }
}






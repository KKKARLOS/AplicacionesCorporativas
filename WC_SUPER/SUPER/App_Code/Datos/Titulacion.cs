using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de Titulacion
    /// </summary>
    public class Titulacion
    {
	    #region Metodos

        public static SqlDataReader SelectDoc(SqlTransaction tr, int t012_idtituloficepi, string tipo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t012_idtituloficepi", SqlDbType.Int, 4, t012_idtituloficepi);
            aParam[i++] = ParametroSql.add("@tipo", SqlDbType.VarChar, 4, tipo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVTITULACIONDOC_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVTITULACIONDOC_SEL", aParam);
        }
        /// <summary>
        /// Obtiene los dos documentos asociados a una titulación académica-> Título y Expediente
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t012_idtituloficepi"></param>
        /// <returns></returns>
        public static SqlDataReader SelectDocs(SqlTransaction tr, int t012_idtituloficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t012_idtituloficepi", SqlDbType.Int, 4, t012_idtituloficepi)
                };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVTITULACIONDOC_SEL2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVTITULACIONDOC_SEL2", aParam);
        }

        public static SqlDataReader CatalogoByNombre(SqlTransaction tr,int t001_idficepi, string t019_descripcion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULACIONES_CAT_ByNombre", aParam);//Titulaciones
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TITULACIONES_CAT_ByNombre", aParam);//Titulaciones
        }

        public static SqlDataReader consultaTitulaciones(SqlTransaction tr, string t019_descripcion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_TITULACIONES", aParam);//Titulaciones
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CONSULTA_TITULACIONES", aParam);//Titulaciones
        }

        public static SqlDataReader Catalogo(SqlTransaction tr, string t019_descripcion, Nullable<byte> t019_estado, string sTipoBusqueda, bool bExcluirRH)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_estado", SqlDbType.Bit, 1, t019_estado),
                ParametroSql.add("@sTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda),
                ParametroSql.add("@bExcluirRH", SqlDbType.Bit, 1, bExcluirRH)
            };
            
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULACIONES_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TITULACIONES_CAT", aParam);
        }

        public static void Delete(SqlTransaction tr, int T019_IDCODTITULO)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T019_IDCODTITULO", SqlDbType.Int, 4, T019_IDCODTITULO);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_TITULACIONES_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_TITULACIONES_DEL", aParam);
        }

        public static void Update(SqlTransaction tr, int T019_IDCODTITULO, string T019_DESCRIPCION, bool T019_ESTADO, int T001_IDFICEPI_I, byte T019_TIPO, Nullable<byte> T019_MODALIDAD, bool T019_TIC, bool t019_rh)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T019_IDCODTITULO", SqlDbType.Int, 4, T019_IDCODTITULO);
            aParam[i++] = ParametroSql.add("@T019_DESCRIPCION", SqlDbType.VarChar, 100, T019_DESCRIPCION);
            aParam[i++] = ParametroSql.add("@T019_ESTADO", SqlDbType.Bit, 1, T019_ESTADO);
            aParam[i++] = ParametroSql.add("@T001_IDFICEPI_I", SqlDbType.Int, 4, T001_IDFICEPI_I);
            aParam[i++] = ParametroSql.add("@T019_TIC", SqlDbType.Bit, 1, T019_TIC);
            aParam[i++] = ParametroSql.add("@T019_TIPO", SqlDbType.TinyInt, 1, T019_TIPO);
            aParam[i++] = ParametroSql.add("@T019_MODALIDAD", SqlDbType.TinyInt, 1, T019_MODALIDAD);
            aParam[i++] = ParametroSql.add("@t019_rh", SqlDbType.Bit, 1, t019_rh);
            
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_TITULACIONES_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_TITULACIONES_UPD", aParam);
        }

        public static short Insert(SqlTransaction tr, string T019_DESCRIPCION, int T001_IDFICEPI_I, bool T019_ESTADO, byte T019_TIPO, Nullable<byte> T019_MODALIDAD, bool T019_TIC)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, T019_DESCRIPCION);
            aParam[i++] = ParametroSql.add("@t001_idficepi_i", SqlDbType.Int, 4, T001_IDFICEPI_I);
            aParam[i++] = ParametroSql.add("@t019_estado", SqlDbType.Bit, 1, T019_ESTADO);
            aParam[i++] = ParametroSql.add("@T019_TIC", SqlDbType.Bit, 1, T019_TIC);
            aParam[i++] = ParametroSql.add("@T019_TIPO", SqlDbType.TinyInt, 1, T019_TIPO);
            aParam[i++] = ParametroSql.add("@T019_MODALIDAD", SqlDbType.TinyInt, 1, T019_MODALIDAD);

            if (tr == null)
                return Convert.ToInt16(SqlHelper.ExecuteScalar("SUP_CVT_TITULACIONES_INS", aParam));
            else
                return Convert.ToInt16(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_TITULACIONES_INS", aParam));
        }

        public static SqlDataReader Obtener(SqlTransaction tr, int t019_idcodtitulo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo);


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULACION_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TITULACION_SEL", aParam);
        }

        /// <summary>
        /// Obtiene los Profesionales Asociados asociados a un título
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t036_idcodentorno">Código de título</param>
        /// <returns></returns>
        public static SqlDataReader ProfAsociados(SqlTransaction tr, int t019_idcodtitulo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PROF_TITULO_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PROF_TITULO_C", aParam);
        }
        public static SqlDataReader ElementosAsociadoAReasignar(SqlTransaction tr, int t019_idcodtitulo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_REASIG_TITULACION_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_REASIG_TITULACION_CAT", aParam);
        }
        /// <summary>
        /// Obtiene un titulación pero con una transacción serializable. Sino existe devuelve -1
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t019_idcodtitulo"></param>
        /// <returns></returns>
        public static short GetSerializable(SqlTransaction tr, string t019_descripcion)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion)
            };

            if (tr == null)
                return Convert.ToInt16(SqlHelper.ExecuteScalar("SUP_CVT_TITULACION_SEL2", aParam));
            else
                return Convert.ToInt16(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_TITULACION_SEL2", aParam));
        }

        /// <summary>
        /// Obtiene una lista de los titulos cuyo código se pasa en sListaIds + los titulos cuya denominación está en sListaDens
        /// y existe algun profesional de slFicepis que lo tiene
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
            aParam[i++] = ParametroSql.add("@TMP_COD_TIT", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaIds));
            aParam[i++] = ParametroSql.add("@TMP_DEN_TIT", SqlDbType.Structured, SqlHelper.GetDataTableDen(sListaDens));

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULO_EXPORT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TITULO_EXPORT", aParam);
        }

        public static SqlDataReader GetDocsExportacion(SqlTransaction tr, string slFicepis, string slCodigos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TMP_FICEPI", SqlDbType.Structured, SqlHelper.GetDataTableCod(slFicepis));
            aParam[i++] = ParametroSql.add("@TMP_COD_TIT", SqlDbType.Structured, SqlHelper.GetDataTableDen(slCodigos));

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULO_EXPORT_DOCS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TITULO_EXPORT_DOCS", aParam);
        }
        #endregion
    }
}
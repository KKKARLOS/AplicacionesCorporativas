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
    /// Descripción breve de TituloIdiomaFic
    /// </summary>
    public class TituloFicepi
    {
        #region Metodos

        public static SqlDataReader CatalogoCentro(string sCentro)//, short idcodtitulo
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            //aParam[i++] = ParametroSql.add("@t019_idcodtitulo", SqlDbType.SmallInt, 2, idcodtitulo);
            aParam[i++] = ParametroSql.add("@t012_centro", SqlDbType.VarChar, 100, sCentro);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOCENTRO_CAT", aParam);
        }

        public static SqlDataReader CatalogoTituloIdiomas(string sTitulo, short idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.SmallInt, 4, idcodidioma);
            aParam[i++] = ParametroSql.add("@t021_titulo", SqlDbType.VarChar, 100, sTitulo);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOIDIOMA_CAT", aParam);

        }

        public static SqlDataReader CatalogoEspecialidad(string sEspecialidad, short idcodtitulo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t019_idcodtitulo", SqlDbType.SmallInt, 2, idcodtitulo);
            aParam[i++] = ParametroSql.add("@t012_especialidad", SqlDbType.VarChar, 100, sEspecialidad);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOESPECIALIDAD_CAT", aParam);
        }

        public static SqlDataReader obtenerTipos()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOTIPOS", aParam);
        }

        public static SqlDataReader obtenerModalidades()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOMODALIDADES", aParam);
        }

        public static SqlDataReader obtenerAños()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOAÑOS", aParam);
        }

        public static int Insert(SqlTransaction tr, short idcodtitulo, int idficepi, bool finalizado, string especialidad, string centro, 
                                 Nullable<short> inicio, Nullable<short> fin, string ndoctitulo,  
                                 string ndocexpdte, char estado, string motivort, string observa, int idficepiu,
                                 Nullable<long> idContentServer, Nullable<long> idContentServerExpte)
        {
            SqlParameter[] aParam = new SqlParameter[15];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t019_idcodtitulo", SqlDbType.SmallInt, 2, idcodtitulo);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idficepi);
            aParam[i++] = ParametroSql.add("@t012_finalizado", SqlDbType.Bit, 1, finalizado);
            aParam[i++] = ParametroSql.add("@t012_especialidad", SqlDbType.VarChar, 100, especialidad);
            aParam[i++] = ParametroSql.add("@t012_centro", SqlDbType.VarChar, 100, centro);
            aParam[i++] = ParametroSql.add("@t012_inicio", SqlDbType.SmallInt, 2, inicio);
            aParam[i++] = ParametroSql.add("@t012_fin", SqlDbType.SmallInt, 2, fin);
            //aParam[i++] = ParametroSql.add("@t012_doctitulo", SqlDbType.Image, 2147483647, doctitulo);
            aParam[i++] = ParametroSql.add("@t012_ndoctitulo", SqlDbType.VarChar, 250, ndoctitulo);
            //aParam[i++] = ParametroSql.add("@t012_docexpdte", SqlDbType.Image, 2147483647, docexpdte);
            aParam[i++] = ParametroSql.add("@t012_ndocexpdte", SqlDbType.VarChar, 250, ndocexpdte);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, estado);
            aParam[i++] = ParametroSql.add("@t597_motivort", SqlDbType.Text, 16, motivort);
            aParam[i++] = ParametroSql.add("@t012_observa", SqlDbType.Text, 16, observa);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, idficepiu);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, idContentServer);
            aParam[i++] = ParametroSql.add("@t2_iddocumentoExpte", SqlDbType.BigInt, 8, idContentServerExpte);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_TITULOFICEPI_INS", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_TITULOFICEPI_INS", aParam);
        }

        public static int Update(SqlTransaction tr, int t012_idtituloficepi, short idcodtitulo, int idficepi, bool finalizado, 
                                 string especialidad, string centro, Nullable<short> inicio, Nullable<short> fin, 
                                 string ndoctitulo, string ndocexpdte, char estado, string motivort, string observa, 
                                 int idficepiU, bool cambioDoc, bool cambioExp, char estadoIni,
                                 Nullable<long> idContentServer, Nullable<long> idContentServerExpte)

        {
            SqlParameter[] aParam = new SqlParameter[19];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t012_idtituloficepi", SqlDbType.Int, 4, t012_idtituloficepi);
            aParam[i++] = ParametroSql.add("@t019_idcodtitulo", SqlDbType.SmallInt, 2, idcodtitulo);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idficepi);
            aParam[i++] = ParametroSql.add("@t012_finalizado", SqlDbType.Bit, 1, finalizado);
            aParam[i++] = ParametroSql.add("@t012_especialidad", SqlDbType.VarChar, 100, especialidad);
            aParam[i++] = ParametroSql.add("@t012_centro", SqlDbType.VarChar, 100, centro);
            aParam[i++] = ParametroSql.add("@t012_inicio", SqlDbType.SmallInt, 2, inicio);
            aParam[i++] = ParametroSql.add("@t012_fin", SqlDbType.SmallInt, 2, fin);
            //aParam[i++] = ParametroSql.add("@t012_doctitulo", SqlDbType.Image, 2147483647, doctitulo);
            aParam[i++] = ParametroSql.add("@t012_ndoctitulo", SqlDbType.VarChar, 250, ndoctitulo);
            //aParam[i++] = ParametroSql.add("@t012_docexpdte", SqlDbType.Image, 2147483647, docexpdte);
            aParam[i++] = ParametroSql.add("@t012_ndocexpdte", SqlDbType.VarChar, 250, ndocexpdte);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, estado);
            aParam[i++] = ParametroSql.add("@t597_motivort", SqlDbType.Text, 16, motivort);
            aParam[i++] = ParametroSql.add("@t012_observa", SqlDbType.Text, 16, observa);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, idficepiU);
            aParam[i++] = ParametroSql.add("@cambioDoc", SqlDbType.Bit, 1, cambioDoc);
            aParam[i++] = ParametroSql.add("@cambioExp", SqlDbType.Bit, 1, cambioExp);
            aParam[i++] = ParametroSql.add("@t839_idestado_ini", SqlDbType.Char, 1, estadoIni);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, idContentServer);
            aParam[i++] = ParametroSql.add("@t2_iddocumentoExpte", SqlDbType.BigInt, 8, idContentServerExpte);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_TITULOFICEPI_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_TITULOFICEPI_UPD", aParam);
        }

        public static int Delete(SqlTransaction tr, int idTituloFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t012_idtituloficepi", SqlDbType.Int, 4, idTituloFicepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_TITULOFICEPI_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_TITULOFICEPI_D", aParam);
        }
        public static SqlDataReader Select(int idtituloFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t012_idtituloficepi", SqlDbType.Int, 2, idtituloFicepi);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOFICEPI_SEL", aParam);
        }

        public static SqlDataReader MiCvTitulacion(SqlTransaction tr, int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVTITULACION_CAT", aParam);//Titulaciones
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVTITULACION_CAT", aParam);//Titulaciones
        }

        public static SqlDataReader MiCvTitulacionHTML(SqlTransaction tr, int idFicepi, int bFiltros, string t019_descripcion, 
                                                        Nullable<int> t019_idcodtitulo, Nullable<int> t019_tipo, Nullable<bool> t019_tic, 
                                                        Nullable<int> t019_modalidad)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);
            aParam[i++] = ParametroSql.add("@bfiltros", SqlDbType.Int, 1, bFiltros);
            aParam[i++] = ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion);
            aParam[i++] = ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo);
            aParam[i++] = ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 4, t019_tipo);
            aParam[i++] = ParametroSql.add("@t019_tic", SqlDbType.Bit, 1, t019_tic);
            aParam[i++] = ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 4, t019_modalidad);
            

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVTITULACIONRP", aParam);//Titulaciones
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVTITULACIONRP", aParam);//Titulaciones
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reasigna títulos
        /// </summary>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public static void Reasignar(SqlTransaction tr, int idOrigen, int idDestino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idOrigen", SqlDbType.Int, 4, idOrigen);
            aParam[i++] = ParametroSql.add("@idDestino", SqlDbType.Int, 4, idDestino);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_REASIGNAR_TITULACION", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_REASIGNAR_TITULACION", aParam);
        }

        public static void UpdatearDoc(SqlTransaction tr, int t012_idtituloficepi, int t001_idficepi,
                                        string sDenDoc, Nullable<long> idDoc)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t012_idtituloficepi", SqlDbType.Int, 4, t012_idtituloficepi),
                    ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                    ParametroSql.add("@denDoc", SqlDbType.VarChar, 250, sDenDoc),
                    ParametroSql.add("@idDoc", SqlDbType.BigInt, 8, idDoc)
                };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_TITULOFICEPI_DOC_U", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_TITULOFICEPI_DOC_U", aParam);
        }
        public static void UpdatearExpte(SqlTransaction tr, int t012_idtituloficepi, int t001_idficepi,
                                        string sDenDoc, Nullable<long> idDoc)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t012_idtituloficepi", SqlDbType.Int, 4, t012_idtituloficepi),
                    ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                    ParametroSql.add("@denDoc", SqlDbType.VarChar, 250, sDenDoc),
                    ParametroSql.add("@idDoc", SqlDbType.BigInt, 8, idDoc)
                };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_TITULOFICEPI_EXP_U", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_TITULOFICEPI_EXP_U", aParam);
        }

        #endregion
    }
}

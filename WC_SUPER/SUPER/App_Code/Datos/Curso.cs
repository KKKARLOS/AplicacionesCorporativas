using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de Curso
    /// </summary>
    public partial class Curso
    {
        #region Metodos

        public static int Insert(SqlTransaction tr, string t574_titulo, int t574_origen, Nullable<int> t574_tipo, DateTime? t574_finicio, 
                                 DateTime? t574_ffin, Nullable<int> t173_idprovincia, float t574_horas, string t574_direccion, 
                                 Nullable<int> t036_idcodentorno, string t574_contenido, int t574_tecnicoc, int t001_idficepi,
                                 int t001_idficepiu, Nullable<int> t576_idcriteriom)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_titulo", SqlDbType.VarChar, 100, t574_titulo);
            aParam[i++] = ParametroSql.add("@t574_origen", SqlDbType.Int, 1, t574_origen);
            aParam[i++] = ParametroSql.add("@t574_tipo", SqlDbType.Int, 1, t574_tipo);
            aParam[i++] = ParametroSql.add("@t574_finicio", SqlDbType.SmallDateTime, 10, t574_finicio);
            aParam[i++] = ParametroSql.add("@t574_ffin", SqlDbType.SmallDateTime, 10, t574_ffin);
            aParam[i++] = ParametroSql.add("@t173_idprovincia", SqlDbType.Int, 4, t173_idprovincia);
            aParam[i++] = ParametroSql.add("@t574_horasfuera", SqlDbType.Float, 10, t574_horas);
            aParam[i++] = ParametroSql.add("@t574_direccion", SqlDbType.VarChar, 100, t574_direccion);
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);
            aParam[i++] = ParametroSql.add("@t574_tecnicoc", SqlDbType.Int, 1, t574_tecnicoc);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t574_contenido", SqlDbType.Text, 2147483647, t574_contenido);
            aParam[i++] = ParametroSql.add("@t576_idcriteriom", SqlDbType.Int, 8, t576_idcriteriom);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_CURSO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CURSO_INS", aParam));
        }

        public static void Update(SqlTransaction tr, int t574_idcurso, string t574_titulo, int t574_origen, Nullable<int> t574_tipo,
                                    DateTime? t574_finicio, DateTime? t574_ffin, Nullable<int> t173_idprovincia, float t574_horas, 
                                    string t574_direccion, Nullable<int> t036_idcodentorno, string t574_contenido, int t574_tecnicoc,
                                    int t001_idficepi, int t001_idficepiu, Nullable<int> t576_idcriteriom)
        {
            SqlParameter[] aParam = new SqlParameter[14];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 8, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t574_titulo", SqlDbType.VarChar, 100, t574_titulo);
            aParam[i++] = ParametroSql.add("@t574_origen", SqlDbType.Int, 1, t574_origen);
            aParam[i++] = ParametroSql.add("@t574_tipo", SqlDbType.Int, 1, t574_tipo);
            aParam[i++] = ParametroSql.add("@t574_finicio", SqlDbType.SmallDateTime, 4, t574_finicio);
            aParam[i++] = ParametroSql.add("@t574_ffin", SqlDbType.SmallDateTime, 4, t574_ffin);
            aParam[i++] = ParametroSql.add("@t173_idprovincia", SqlDbType.Int, 4, t173_idprovincia);
            aParam[i++] = ParametroSql.add("@t574_horasfuera", SqlDbType.Float, 10, t574_horas);
            aParam[i++] = ParametroSql.add("@t574_direccion", SqlDbType.VarChar, 100, t574_direccion);
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);
            aParam[i++] = ParametroSql.add("@t574_tecnicoc", SqlDbType.Int, 1, t574_tecnicoc);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t574_contenido", SqlDbType.Text, 2147483647, t574_contenido);
            aParam[i++] = ParametroSql.add("@t576_idcriteriom", SqlDbType.Int, 8, t576_idcriteriom);

            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_CURSO_UPD", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CURSO_UPD", aParam);
        }

        public static int InsertAsis(SqlTransaction tr, int t574_idcurso, string t575_observaciones, int t001_idficepi,  
                                        string t575_ndoc, char t839_idestado, string t575_motivo, int t001_idficepiu, bool bVisibleCV,
                                        Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 8, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t575_observa", SqlDbType.Text, 2147483647, t575_observaciones);
            //aParam[i++] = ParametroSql.add("@t575_doc", SqlDbType.Image, 2147483647, t575_doc);
            aParam[i++] = ParametroSql.add("@t575_ndoc", SqlDbType.VarChar, 250, t575_ndoc);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@t575_motivo", SqlDbType.Text, 2147483647, t575_motivo);
            aParam[i++] = ParametroSql.add("@t575_visibleCV", SqlDbType.Bit, 1, bVisibleCV);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURASISTENTE_INS", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURASISTENTE_INS", aParam);
        }

        public static int DeleteAsis(SqlTransaction tr, int t574_idcurso, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURASISTENTE_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURASISTENTE_D", aParam);
        }

        public static int UpdateAsis(SqlTransaction tr, int t574_idcurso, string t575_observaciones, int t001_idficepi,  
                                     string t575_ndoc, char @t839_idestado, bool cambioDoc, string t575_motivo, int t001_idficepiu,
                                     char @t839_idestado_ini, bool bVisibleCV, Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 8, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t575_observa", SqlDbType.Text, 2147483647, t575_observaciones);
            //aParam[i++] = ParametroSql.add("@t575_doc", SqlDbType.Image, 2147483647, t575_doc);
            aParam[i++] = ParametroSql.add("@t575_ndoc", SqlDbType.VarChar, 250, t575_ndoc);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@cambioDoc", SqlDbType.Bit, 1, cambioDoc);
            aParam[i++] = ParametroSql.add("@t575_motivo", SqlDbType.Text, 2147483647, t575_motivo);
            aParam[i++] = ParametroSql.add("@t839_idestado_ini", SqlDbType.Char, 1, t839_idestado_ini);
            aParam[i++] = ParametroSql.add("@t575_visibleCV", SqlDbType.Bit, 1, bVisibleCV);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURASISTENTE_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURASISTENTE_UPD", aParam);
        }

        public static int InsertCurMonitorOld(SqlTransaction tr, int t001_idficepi, int t574_idcurso, char t839_idestado, string t843_motivo, int t001_idficepiu)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 8, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@t843_motivo", SqlDbType.VarChar, 500, t843_motivo);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);


            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURMONITOR_INS", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURMONITOR_INS", aParam);
        }

        public static int InsertCurMonitor(SqlTransaction tr, int t574_idcurso, string t580_observaciones, int t001_idficepi, 
                                            string t580_ndoc, char @t839_idestado, string t843_motivo, int t001_idficepiu, bool bVisibleCV,
                                            Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 8, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t580_observa", SqlDbType.Text, 2147483647, t580_observaciones);
            //aParam[i++] = ParametroSql.add("@t580_doc", SqlDbType.Image, 2147483647, t580_doc);
            aParam[i++] = ParametroSql.add("@t580_ndoc", SqlDbType.VarChar, 250, t580_ndoc);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, @t839_idestado);
            aParam[i++] = ParametroSql.add("@t843_motivo", SqlDbType.VarChar, 500, t843_motivo);
            aParam[i++] = ParametroSql.add("@t580_visibleCV", SqlDbType.Bit, 1, bVisibleCV);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURMONITOR_INS2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURMONITOR_INS2", aParam);
        }

        public static int UpdateCurMonitorOld(SqlTransaction tr, int t580_idcurmonitor, int t001_idficepi, int t574_idcurso, 
                                                char @t839_idestado, string t843_motivo, int t001_idficepiu, char t839_idestado_ini)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t580_idcurmonitor", SqlDbType.Int, 8, t580_idcurmonitor);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 8, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@t843_motivo", SqlDbType.VarChar, 500, t843_motivo);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t839_idestado_ini", SqlDbType.Char, 1, t839_idestado_ini);


            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURMONITOR_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURMONITOR_UPD", aParam);
        }

        public static int UpdateCurMonitor(SqlTransaction tr, int t580_idcurmonitor, int t574_idcurso, string t580_observaciones, 
                                            int t001_idficepi,string t580_ndoc, char @t839_idestado, bool cambioDoc,
                                            string t843_motivo, int t001_idficepiu, char @t839_idestado_ini, bool bVisibleCV,
                                            Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t580_idcurmonitor", SqlDbType.Int, 8, t580_idcurmonitor);
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 8, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 8, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t580_observa", SqlDbType.Text, 2147483647, t580_observaciones);
            //aParam[i++] = ParametroSql.add("@t580_doc", SqlDbType.Image, 2147483647, t580_doc);
            aParam[i++] = ParametroSql.add("@t580_ndoc", SqlDbType.VarChar, 250, t580_ndoc);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@cambioDoc", SqlDbType.Bit, 1, cambioDoc);
            aParam[i++] = ParametroSql.add("@t843_motivo", SqlDbType.VarChar, 500, t843_motivo);
            aParam[i++] = ParametroSql.add("@t839_idestado_ini", SqlDbType.Char, 1, t839_idestado_ini);
            aParam[i++] = ParametroSql.add("@t580_visibleCV", SqlDbType.Bit, 1, bVisibleCV);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento);
            
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURMONITOR_UPD2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURMONITOR_UPD2", aParam);
        }

        public static int DeleteMonitor(SqlTransaction tr, int t580_idcurmonitor)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t580_idcurmonitor", SqlDbType.Int, 4, t580_idcurmonitor);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURMONITOR_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURMONITOR_D", aParam);
        }
        public static void InsertCurProv(SqlTransaction tr, int t574_idcurso, Nullable<int> t315_idproveedor, int t581_estado)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t315_idproveedor", SqlDbType.Int, 4, t315_idproveedor);
            aParam[i++] = ParametroSql.add("@t581_estado", SqlDbType.Int, 8, t581_estado);

            if (tr == null)
                SqlHelper.ExecuteScalar("FRM_ADMCURPROVEEDOR_INS", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "FRM_ADMCURPROVEEDOR_INS", aParam);
        }
        
        public static void DeleteProveedor(SqlTransaction tr, int idcurso, Nullable<int> proveedor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;

            aParam[i++] = ParametroSql.add("@T574_IDCURSO", SqlDbType.Int, 4, idcurso);
            aParam[i++] = ParametroSql.add("@T315_IDPROVEEDOR", SqlDbType.Int, 4, proveedor);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("FRM_ADMCURPROVEEDOR_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "FRM_ADMCURPROVEEDOR_DEL", aParam);
        }

        public static SqlDataReader obtenerProvincia(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROVINCIA", aParam);//Provincias
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROVINCIA", aParam);//Provincias
        }

        //public static SqlDataReader obtenerTipoCurso()
        //{
        //    SqlParameter[] aParam = new SqlParameter[0];
        //    return SqlHelper.ExecuteSqlDataReader("FRM_TIPOCURSOS", aParam);
        //}

        public static SqlDataReader MiCVFormacionRecibida(SqlTransaction tr, int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_FICEPICURSO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_FICEPICURSO_CAT", aParam);


        }

        public static SqlDataReader MiCVFormacionImpartida(SqlTransaction tr, int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MONITORCURSO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MONITORCURSO_CAT", aParam);


        }

        public static SqlDataReader SelectDoc(int t574_idcurso, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CURSODOC_SEL", aParam);
        }

        public static SqlDataReader SelectDoc2(int t574_idcurso, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CURSODOC_IMP_SEL", aParam);
        }
        public static SqlDataReader Detalle(SqlTransaction tr, int t574_idcurso, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CURSO_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CURSO_SEL", aParam);
        }

        public static SqlDataReader DetalleMonitor(SqlTransaction tr, int t580_idcurmonitor)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t580_idcurmonitor", SqlDbType.Int, 4, t580_idcurmonitor);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CURMONITOR_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CURMONITOR_SEL", aParam);
        }

        public static SqlDataReader MiCVFormacionRecibidaHTML(SqlTransaction tr, int idFicepi, int bFiltros, string lft036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, idFicepi);
            aParam[i++] = ParametroSql.add("@bFiltros", SqlDbType.Int, 1, bFiltros);
            aParam[i++] = ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_FICEPICURSORP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_FICEPICURSORP", aParam);


        }

        public static SqlDataReader MiCVFormacionImpartidaHTML(SqlTransaction tr, int idFicepi, int bFiltros, string lft036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);
            aParam[i++] = ParametroSql.add("@bFiltros", SqlDbType.Int, 1, bFiltros);
            aParam[i++] = ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MONITORCURSORP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MONITORCURSORP", aParam);


        }

        public static int PedirBorradoRecibido(SqlTransaction tr, int t574_idcurso, int t001_idficepi, int t001_idficepi_petbor, string t575_motivo_petbor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, t574_idcurso);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepi_petbor", SqlDbType.Int, 4, t001_idficepi_petbor);
            aParam[i++] = ParametroSql.add("@t575_motivo_petbor", SqlDbType.Text, 500, t575_motivo_petbor);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURASISTENTE_PD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURASISTENTE_PD", aParam);
        }
        public static int PedirBorradoImpartido(SqlTransaction tr, int t580_idcurmonitor, int t001_idficepi_petbor, string t580_motivo_petbor)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t580_idcurmonitor", SqlDbType.Int, 4, t580_idcurmonitor);
            aParam[i++] = ParametroSql.add("@t001_idficepi_petbor", SqlDbType.Int, 4, t001_idficepi_petbor);
            aParam[i++] = ParametroSql.add("@t580_motivo_petbor", SqlDbType.Text, 500, t580_motivo_petbor);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CURMONITOR_PD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURMONITOR_PD", aParam);
        }

        public static void SetVisibilidadCV_Impartido(SqlTransaction tr, int idcurso, bool bVisibleCV)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@t580_idcurmonitor", SqlDbType.Int, 4, idcurso),
                ParametroSql.add("@t580_visibleCV", SqlDbType.Bit, 1, bVisibleCV)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_CURMONITOR_VISIBILIDAD_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURMONITOR_VISIBILIDAD_U", aParam);
        }
        public static void SetVisibilidadCV_Recibido(SqlTransaction tr, int idcurso, int idFicepi, bool bVisibleCV)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, idcurso),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi),
                ParametroSql.add("@t575_visibleCV", SqlDbType.Bit, 1, bVisibleCV)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_CURASISTENTE_VISIBILIDAD_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CURASISTENTE_VISIBILIDAD_U", aParam);
        }

        /// <summary>
        /// Obtiene una lista de los cursos (impartidos y recibidos) cuyo código se pasa en sListaIds 
        /// + los cursos (impartidos y recibidos) cuya denominación está en sListaDens
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
            aParam[i++] = ParametroSql.add("@TMP_COD_CURSO", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaIds));
            aParam[i++] = ParametroSql.add("@TMP_DEN_CURSO", SqlDbType.Structured, SqlHelper.GetDataTableDen(sListaDens));

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CURSO_EXPORT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CURSO_EXPORT", aParam);
        }
        public static SqlDataReader GetDocsExportacion(SqlTransaction tr, string slFicepis, string slCodigos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TMP_FICEPI", SqlDbType.Structured, SqlHelper.GetDataTableCod(slFicepis));
            aParam[i++] = ParametroSql.add("@TMP_COD_CURSO", SqlDbType.Structured, SqlHelper.GetDataTableDen(slCodigos));

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CURSO_EXPORT_DOCS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CURSO_EXPORT_DOCS", aParam);
        }

        public static void UpdatearDoc(SqlTransaction tr, string sTipo, int idCurso, int t001_idficepi, string sDenDoc, Nullable<long> t2_iddocumento)
        {
            if (sTipo == "R")
            {//Curso recibido
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t574_idcurso", SqlDbType.Int, 4, idCurso),
                    ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                    ParametroSql.add("@t575_ndoc", SqlDbType.VarChar, 250, sDenDoc),
                    ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
                };
                if (tr == null)
                    SqlHelper.ExecuteScalar("SUP_CVT_CURASISTENTE_DOC_U", aParam);
                else
                    SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CURASISTENTE_DOC_U", aParam);
            }
            else
            {//Curso impartido
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t580_idcurmonitor", SqlDbType.Int, 4, idCurso),
                    ParametroSql.add("@t580_ndoc", SqlDbType.VarChar, 250, sDenDoc),
                    ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
                };
                if (tr == null)
                    SqlHelper.ExecuteScalar("SUP_CVT_CURMONITOR_DOC_U", aParam);
                else
                    SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CURMONITOR_DOC_U", aParam);
            }
        }

        #endregion
    }
}






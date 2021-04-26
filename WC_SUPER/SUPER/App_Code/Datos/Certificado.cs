using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
//Para usar ArraList
using System.Collections;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de Curvit
    /// </summary>
    public class Certificado
    {
        #region Certificado. Tabla T582_CERTIFICADO
        public static int InsertarCertificado(SqlTransaction tr, string t582_nombre, string t582_abrev, Nullable<int> t036_idcodentorno,
                                                Nullable<int> t576_idcriterio, string t582_observa, bool t582_valido)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre.Trim()),
                ParametroSql.add("@t582_abrev", SqlDbType.VarChar, 30, t582_abrev),
                ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno),
                ParametroSql.add("@t576_idcriterio", SqlDbType.Int, 4, t576_idcriterio),
                ParametroSql.add("@t582_observa", SqlDbType.VarChar, 500, t582_observa),
                ParametroSql.add("@t582_valido", SqlDbType.Bit, 1, t582_valido)
            };
            if (tr == null)
                 return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_CERTIFICADO_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CERTIFICADO_I", aParam));
        }
        public static void UpdateCertificado(SqlTransaction tr, int t582_idcertificado, string t582_nombre, string t582_abrev, 
                                             Nullable<int> t036_idcodentorno, Nullable<int> t576_idcriterio)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@t582_abrev", SqlDbType.VarChar, 30, t582_abrev.Trim()),
                ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno),
                ParametroSql.add("@t576_idcriterio", SqlDbType.Int, 4, t576_idcriterio),
            };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_CERTIFICADO_U", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CERTIFICADO_U", aParam);
        }
        /// <summary>
        /// Obtiene una lista de certificados en base a una búsqueda avanzada con criterios de nombre, entorno tecnólógico y entidad certificadora
        /// </summary>
        /// <param name="t036_idcodentorno"></param>
        /// <param name="t576_idcriterio"></param>
        /// <param name="t582_nombre"></param>
        /// <param name="t582_valido"></param>
        /// <returns></returns>
        public static SqlDataReader GetCertificados(SqlTransaction tr, Nullable<int> t576_idcriterio, Nullable<int> t036_idcodentorno,  
                                                    string sDenominacion)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@sbusqueda", SqlDbType.VarChar, 8000, sDenominacion.Trim()),
                ParametroSql.add("@t576_idcriterio", SqlDbType.Int, 4, t576_idcriterio),
                ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_BUSAVANZADA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_BUSAVANZADA", aParam);
        }
        public static SqlDataReader GetCertificado(SqlTransaction tr, int t582_idcertificado)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_S2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_S2", aParam);
        }

        #endregion

        #region Exportación de certificados
        /// <summary>
        /// Dada una lista de denominaciones separadas por ; devuelve todos los códigos de certificados cuya enominación coincide
        /// en todo o en parte con cada una de las denominaciones 
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="sListaDenominaciones"></param>
        /// <returns></returns>
        //public static SqlDataReader GetIdsCertificado(SqlTransaction tr, string sListaDenominaciones)
        //{
        //    SqlParameter[] aParam = new SqlParameter[]{  
        //        ParametroSql.add("@TMP_DEN_CERT", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaDenominaciones))
        //    };
        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_GETIDS", aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_GETIDS", aParam);
        //}

        /// <summary>
        /// Obtiene una lista de los certificados cuyo código se pasa en sListaIds + los certificados cuya denominación está en sListaDens
        /// y existe algun profesional de slFicepis que lo tiene
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="slFicepis"></param>
        /// <param name="sListaIds"></param>
        /// <param name="sListaDens"></param>
        /// <returns></returns>
        public static SqlDataReader GetListaPorProfesional(SqlTransaction tr, string slFicepis, string sListaIds, string sListaDens)
        {
            //SqlParameter[] aParam = new SqlParameter[]{  
            //    ParametroSql.add("@TMP_COD_CERT", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaIds)),
            //    ParametroSql.add("@TMP_DEN_CERT", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaDens))
            //};
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TMP_FICEPI", SqlDbType.Structured, SqlHelper.GetDataTableCod(slFicepis));
            aParam[i++] = ParametroSql.add("@TMP_COD_CERT", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaIds));
            aParam[i++] = ParametroSql.add("@TMP_DEN_CERT", SqlDbType.Structured, SqlHelper.GetDataTableDen(sListaDens));

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_EXPORT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_EXPORT", aParam);
        }
        public static SqlDataReader GetCertificadosExportacion(SqlTransaction tr, string slFicepis, string slCerts)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TMP_FICEPI", SqlDbType.Structured, SqlHelper.GetDataTableCod(slFicepis));
            aParam[i++] = ParametroSql.add("@TMP_COD_CERT", SqlDbType.Structured, SqlHelper.GetDataTableDen(slCerts));

            if (tr == null)//SUP_CVT_CERTIFICADO_CRITERIO_EXPORT
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_EXPORT_DOCS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_EXPORT_DOCS", aParam);
        }
        #endregion

        #region Certificado del profesional. Tabla T593_FICEPICERT
        //Obtengo los certificados de un profesional
        public static SqlDataReader MiCVFormacionCertExam(SqlTransaction tr, int idFicepi, bool esEncargado)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi),
                ParametroSql.add("@esEncargado", SqlDbType.Bit, 1, esEncargado)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVCERTEXAM_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVCERTEXAM_CAT", aParam);


        }
        //Obtengo las vías de un certificado. Las válidas + las propias
        public static SqlDataReader Vias(SqlTransaction tr, int t582_idcertificado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_VIAS2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_VIAS2", aParam);


        }
        //public static SqlDataReader Select(int t582_idcertificado, int caso, int t001_idficepi)
        public static SqlDataReader Select(int t582_idcertificado, int t001_idficepi)
        {
            //SqlParameter[] aParam = new SqlParameter[3];
            //int i = 0;
            //aParam[i++] = ParametroSql.add("@idcertificado", SqlDbType.Int, 4, idCertificado);
            //aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            //aParam[i++] = ParametroSql.add("@caso", SqlDbType.Int, 4, caso);
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_S", aParam);
        }
        public static SqlDataReader Datos(SqlTransaction tr, int t582_idcertificado)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_DATOS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_DATOS", aParam);
        }
        
        public static SqlDataReader SelectDoc(SqlTransaction tr, int idCvtExamenCert, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, idCvtExamenCert);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADODOC_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADODOC_SEL", aParam);

        }

        /// <summary>
        /// Se usa para el autocomplete en la pantalla de detalle de certificado
        /// Obtiene un catalogo de certificados asociados a una entidad certificadora 
        /// 14/03/2014
        /// Antes se controlaba que el profesional no tenga un examen que pertenezca a alguna vía, es decir, que no lo tenga ya ni sea sugerido
        /// Ahora lo cambio para que no saque certificados que están en FICEPICERT
        /// </summary>
        /// <param name="paramEntidadCert"></param>
        /// <param name="paramCert"></param>
        /// <param name="valido"></param>
        /// <param name="idFicepi"></param>
        /// <returns></returns>
        public static SqlDataReader obtenerCertificadoEntidadCert(Nullable<int> paramEntidadCert, string paramCert, Nullable<bool> valido,int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t576_idcriterio", SqlDbType.Int, 4, paramEntidadCert);
            aParam[i++] = ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, paramCert);
            aParam[i++] = ParametroSql.add("@t582_valido", SqlDbType.Bit, 1, valido);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTENTCERT_CAT", aParam);
        }

        //public static int tramitarFicepiCert(SqlTransaction tr, int t582_idcertificadoold, int t582_idcertificadonew, string t582_nombre, 
        //                                     string t582_abrev, byte[] t593_doc, string t593_ndoc, Nullable<int> t576_idcriterio, 
        //                                     Nullable<int> t036_idcodentorno, int t001_idficepi, bool cambioDoc, string t593_motivort)
        //{
        //    SqlParameter[] aParam = new SqlParameter[11];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t582_idcertificadoold", SqlDbType.Int, 4, t582_idcertificadoold);
        //    aParam[i++] = ParametroSql.add("@t582_idcertificadonew", SqlDbType.Int, 4, t582_idcertificadonew);
        //    aParam[i++] = ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre);
        //    aParam[i++] = ParametroSql.add("@t582_abrev", SqlDbType.VarChar, 30, t582_abrev);
        //    aParam[i++] = ParametroSql.add("@t593_ndoc", SqlDbType.VarChar, 100, t593_ndoc);
        //    aParam[i++] = ParametroSql.add("@t593_doc", SqlDbType.Image, 2147483647, t593_doc);
        //    aParam[i++] = ParametroSql.add("@t576_idcriterio", SqlDbType.Int, 4, t576_idcriterio);
        //    aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);
        //    //aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
        //    aParam[i++] = ParametroSql.add("@t593_motivort", SqlDbType.VarChar, 500, t593_motivort);
        //    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
        //    aParam[i++] = ParametroSql.add("@cambioDoc", SqlDbType.Bit, 1, cambioDoc);

        //    if (tr == null)
        //        return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_CERTIFICADOFICEPI_TRAMITA", aParam));
        //    else
        //        return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CERTIFICADOFICEPI_TRAMITA", aParam));
        //}

        public static void insertarVia(SqlTransaction tr, int idCertificado, string examenes, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, idCertificado);
            aParam[i++] = ParametroSql.add("@examenes", SqlDbType.VarChar, 8000, examenes);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            //if (tr == null)
            //    SqlHelper.ExecuteNonQuery("SUP_CVT_INSERTARVIACERT", aParam);
            //else
            //    SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_INSERTARVIACERT", aParam);
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_VIA_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_VIA_I", aParam);
        }
        /// <summary>
        /// Borra la vía del certificado sólo si la vía la ha generado el profesional y RRHH no la ha validado todavía
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="idCertificado"></param>
        public static void BorrarVia(SqlTransaction tr, int idCertificado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, idCertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_EXAMCERT_D", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXAMCERT_D", aParam);
        }
        /// <summary>
        /// Elimina el certificado asociado a un profesional
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="idCertificado"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static int DeleteProf(SqlTransaction tr, int idCertificado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, idCertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPICERT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPICERT_D", aParam);
        }
        public static int PedirBorrado(SqlTransaction tr, int idCertificado, int t001_idficepi, int t001_idficepi_petbor, string t593_motivo_petbor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, idCertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepi_petbor", SqlDbType.Int, 4, t001_idficepi_petbor);
            aParam[i++] = ParametroSql.add("@t593_motivo_petbor", SqlDbType.Text, 500, t593_motivo_petbor);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPICERT_PD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPICERT_PD", aParam);
        }
        public static int DeleteNoValido(SqlTransaction tr, int t582_idcertificado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_CERTNOVALIDO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CERTNOVALIDO_D", aParam);
        }
        public static bool EsValido(int t582_idcertificado)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);

            int returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_CERTIFICADO_VALIDO", aParam));

            if (returnValue > 0)
                bRes = true;

            return bRes;
        }
        public static void Insertar(SqlTransaction tr, int t582_idcertificado, int t001_idficepi, string t593_ndoc, 
                                    Nullable<bool> t593_estadoDoc, string t593_motivort, Nullable<bool> t593_origencvt, 
                                    Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t593_ndoc", SqlDbType.VarChar, 250, t593_ndoc),
                //ParametroSql.add("@t593_doc", SqlDbType.Image, 2147483647, t593_doc),
                //ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado),
                ParametroSql.add("@t593_estadoDoc", SqlDbType.Bit, 1, t593_estadoDoc),
                ParametroSql.add("@t593_motivort", SqlDbType.VarChar, 500, t593_motivort),
                ParametroSql.add("@t593_origencvt", SqlDbType.Bit, 1, t593_origencvt),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_FICEPICERT_INS", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_FICEPICERT_INS", aParam);
        }
        /// <summary>
        /// Si ha habido cambio de documento el certificado queda en estado Pdte De Validar
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t593_ndoc"></param>
        /// <param name="t593_doc"></param>
        /// <param name="t593_motivort"></param>
        /// <param name="bCambioDoc"></param>
        public static void UpdatearDoc(SqlTransaction tr, int t582_idcertificado, int t001_idficepi, string t593_ndoc, 
                                    string t593_motivort, bool bCambioDoc, Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t593_ndoc", SqlDbType.VarChar, 250, t593_ndoc),
                //ParametroSql.add("@t593_doc", SqlDbType.Image, 2147483647, t593_doc),
                ParametroSql.add("@t593_motivort", SqlDbType.VarChar, 500, t593_motivort),
                ParametroSql.add("@bCambioDoc", SqlDbType.Bit, 1, bCambioDoc),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_FICEPICERT_DOC_U", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_FICEPICERT_DOC_U", aParam);
        }
        public static SqlDataReader GetDatos(SqlTransaction tr, int t582_idcertificado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_FICEPICERT_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_FICEPICERT_S", aParam);

        }
        /// <summary>
        /// Obtiene vías válidas de un certificado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static SqlDataReader GetVias(SqlTransaction tr, int t582_idcertificado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CERTIFICADO_VIAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CERTIFICADO_VIAS", aParam);

        }


        public static bool HayExamenValidado(int t001_idficepi, int t582_idcertificado)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            int returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_CERTEXAMEN_VALIDADO", aParam));

            if (returnValue > 0)
                bRes = true;

            return bRes;//ArrayList slLista = new ArrayList();
        }
        /// <summary>
        /// Lista de examenes de un profesional en un certificado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static ArrayList Examenes(SqlTransaction tr, int t001_idficepi, int t582_idcertificado)
        {
            ArrayList aExam = new ArrayList();
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMCERTFICEPI_CAT2", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMCERTFICEPI_CAT2", aParam);
            while (dr.Read())
                aExam.Add(dr["t583_idexamen"].ToString());
            dr.Close();
            dr.Dispose();

            return aExam;
        }
        /// <summary>
        /// Lista de examenes de un profesional en un certificado que tambien se usan en otro certificado de ese profesional
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static ArrayList ExamenesAjenos(SqlTransaction tr, int t001_idficepi, int t582_idcertificado)
        {
            ArrayList aExam = new ArrayList();
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMCERTAJENOS_C", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMCERTAJENOS_C", aParam);
            while (dr.Read())
                aExam.Add(dr["t583_idexamen"].ToString());
            dr.Close();
            dr.Dispose();

            return aExam;
        }
        /// <summary>
        /// Examenes del certificado que no pertenecen a otro certificado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static ArrayList ExamenesPropios(SqlTransaction tr,int t001_idficepi, int t582_idcertificado)
        {
            ArrayList aExam = new ArrayList();
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMCERTPROPIOS_C", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMCERTPROPIOS_C", aParam);
            while (dr.Read())
                aExam.Add(dr["t583_idexamen"].ToString());
            dr.Close();
            dr.Dispose();

            return aExam;
        }
        //public static SqlDataReader ExamenesPropios(SqlTransaction tr, int t582_idcertificado)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);

        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMCERTPROPIOS_C", aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMCERTPROPIOS_C", aParam);

        //}
        /// <summary>
        /// Borra los exámenes de un certificado para un profesional
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static int BorrarExamenesProfesional(SqlTransaction tr, int t582_idcertificado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXAMCERTFICEPI_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXAMCERTFICEPI_D", aParam);
        }
        public static int BorrarExamenesNoValidos(SqlTransaction tr, int t582_idcertificado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXAMENCERTNOVALIDO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXAMENCERTNOVALIDO_D", aParam);
        }
        public static SqlDataReader DatosExamenes(SqlTransaction tr, int t582_idcertificado, int t001_idficepi, int camino)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);
            aParam[i++] = ParametroSql.add("@t585_camino", SqlDbType.Int, 4, camino);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("FRM_EXAMENESCERT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "FRM_EXAMENESCERT", aParam);

        }

        public static bool TieneProfesionales(SqlTransaction tr, int t582_idcertificado)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0, returnValue = 0;
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);

            if (tr == null)
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_CERT_PROF_COUNT", aParam));
            else
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CERT_PROF_COUNT", aParam));

            if (returnValue > 0)
                bRes = true;

            return bRes;
        }
        /// <summary>
        /// Revisa los examenes que tiene un profesional e inserta en T593_FICEPICERT aquellos certificados para los que:
		///		1.- Tiene una via validada completa 
		///		2.- Sea un certificado vigente
		///		3.- Sea un certificado válido
        ///		4.- No esten ya en T593_FICEPICERT
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t593_origencvt"></param>
        public static void ConseguirAutomatico(SqlTransaction tr, int t001_idficepi, bool t593_origencvt)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t593_origencvt", SqlDbType.Bit, 1, t593_origencvt)
            };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_FICEPICERT_ADD", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_FICEPICERT_ADD", aParam);
        }
        /// <summary>
        /// Indica si un profesional tiene un certificado(Existe registro en T593_FICEPICERT)
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static bool LoTiene(SqlTransaction tr, int t001_idficepi, int t582_idcertificado)
        {
            bool bRes = false;
            SqlDataReader dr = GetDatos(tr, t582_idcertificado, t001_idficepi);
            if (dr.Read())
                bRes = true;
            dr.Close();
            dr.Dispose();

            return bRes;
        }

        public static void PonerMotivo(SqlTransaction tr, int t582_idcertificado, int t001_idficepi, string t593_motivort)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t593_motivort", SqlDbType.VarChar, 500, t593_motivort),
            };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_FICEPICERT_MOTIVO_U", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_FICEPICERT_MOTIVO_U", aParam);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
using System.Data;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de Curvit
    /// </summary>
    public class Examen
    {
        public static SqlDataReader SelectDoc(SqlTransaction tr, int idCvtExamenCert, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, idCvtExamenCert);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMENDOC_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMENDOC_SEL", aParam);

        }
        /// <summary>
        /// Asocia un identificador de documento en Atenea al examen de un profesional
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t583_idexamen"></param>
        /// <param name="t2_iddocumento"></param>
        public static void PonerDocumento(SqlTransaction tr, int t001_idficepi, int t583_idexamen, long t2_iddocumento, string t591_ndoc)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen),
                ParametroSql.add("@t591_ndoc", SqlDbType.VarChar, 250, t591_ndoc),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPIEXAMEN_DOC_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPIEXAMEN_DOC_U", aParam);
        }

        public static SqlDataReader Datos(SqlTransaction tr, int t583_idexamen)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen),
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMEN_DATOS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMEN_DATOS", aParam);
        }
        //Obtengo los certificado a los que pertenece un exámen. Las válidas + las propias
        public static SqlDataReader GetCertificados(SqlTransaction tr, int t583_idexamen, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMEN_VIAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMEN_VIAS", aParam);


        }
        //Obtengo los exámenes de un profesional
        public static SqlDataReader MiCVFormacionExam(SqlTransaction tr, int idFicepi)//, bool esEncargado)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi)
                //,ParametroSql.add("@esEncargado", SqlDbType.Bit, 1, esEncargado)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVEXAM_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVEXAM_CAT", aParam);
        }
        //Se usa en el autocomplete de examenes en la pantalla de detalle de exámenes
        public static SqlDataReader obtenerExamenEntidadCertEntorno(Nullable<int> paramEntidadCert, Nullable<int> paramEntorno,
                                                                    string paramExam, int idFicepi, Nullable<bool> valido, Nullable<int> idCert)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t576_idcriterio", SqlDbType.Int, 4, paramEntidadCert),
                ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, paramEntorno),
                ParametroSql.add("@t583_nombre", SqlDbType.VarChar, 100, paramExam),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi),
                ParametroSql.add("@t583_valido", SqlDbType.Bit, 1, valido),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, idCert)
            };
            //return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMENENTCERTENTORNO_CAT", aParam);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMEN_AUTOCOMPLETE", aParam);
        }

        public static SqlDataReader CatalogoFicepi(SqlTransaction tr, int idCertificado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idcertificado", SqlDbType.Int, 4, idCertificado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXAMCERTFICEPI_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXAMCERTFICEPI_CAT", aParam);

        }

        public static SqlDataReader MiCVFormacionCertExamHTML(SqlTransaction tr, int idFicepi, int bFiltros, Nullable<int> t582_idcertificado, 
                                                              string t582_nombre, string lft036_idcodentorno, Nullable<int> origenConsulta)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);
            aParam[i++] = ParametroSql.add("@bFiltros", SqlDbType.Int, 1, bFiltros);
            aParam[i++] = ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado);
            aParam[i++] = ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre);
            aParam[i++] = ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno);
            aParam[i++] = ParametroSql.add("@origenConsulta", SqlDbType.Int, 1, origenConsulta);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVCERTEXAMRP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVCERTEXAMRP", aParam);
        }
       
        public static SqlDataReader obtenerEntCert(int tipo, int activo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t576_tipo", SqlDbType.VarChar, 8000, tipo);
            aParam[i++] = ParametroSql.add("@t576_activo", SqlDbType.VarChar, 8000, activo);
            return SqlHelper.ExecuteSqlDataReader("FRM_CRITERIO_CAT", aParam);


        }
        /// <summary>
        /// Borra un examen si no es válido (t583_valido=0)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t583_idexamen"></param>
        /// <returns></returns>
        public static int DeleteNoValido(SqlTransaction tr, int t583_idexamen)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXAMENNOVALIDO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXAMENNOVALIDO_D", aParam);
        }
        /// <summary>
        /// Borra el examen de la vía sólo si la vía la ha generado el profesional y RRHH no la ha validado todavía
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t583_idexamen"></param>
        public static void BorrarVia(SqlTransaction tr, int t583_idexamen, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_EXAMCERT_D2", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXAMCERT_D2", aParam);
        }

        public static int DeleteAsistente(SqlTransaction tr, int t583_idexamen, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPIEXAMEN_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPIEXAMEN_D", aParam);
        }
        public static int PedirBorrado(SqlTransaction tr, int t583_idexamen, int t001_idficepi, int t001_idficepi_petbor, string t591_motivo_petbor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen);
            aParam[i++] = ParametroSql.add("@t001_idficepi_petbor", SqlDbType.Int, 4, t001_idficepi_petbor);
            aParam[i++] = ParametroSql.add("@t591_motivo_petbor", SqlDbType.Text, 2147483647, t591_motivo_petbor);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPIEXAMEN_PD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPIEXAMEN_PD", aParam);
        }

        //public static bool ExisteEnOtroCertificado(SqlTransaction tr, int t001_idficepi, int idCertificado, int t583_idexamen)
        //{
        //    bool bExiste = false;
        //    int returnValue;
        //    SqlParameter[] aParam = new SqlParameter[3];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@idcertificado", SqlDbType.Int, 4, idCertificado);
        //    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
        //    aParam[i++] = ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen);

        //    if (tr == null)
        //        returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXAMENFICEPI_USADO", aParam));
        //    else
        //        returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXAMENFICEPI_USADO", aParam));

        //    if (returnValue > 0)
        //        bExiste = true;

        //    return bExiste;
        //}

        /// <summary>
        /// Inserta un registro en T583_EXAMEN y devuelve el código del registro recien insertado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t583_nombre"></param>
        /// <param name="t583_valido"></param>
        public static int Insertar(SqlTransaction tr, string t583_nombre, bool t583_valido)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@t583_nombre", SqlDbType.VarChar, 100, t583_nombre),
                ParametroSql.add("@t583_valido", SqlDbType.Bit, 1, t583_valido)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXAMEN_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXAMEN_I", aParam));
        }
        /// <summary>
        /// Inserta un registro en T591_FICEPIEXAMEN
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t583_idexamen"></param>
        /// <param name="t583_nombre"></param>
        /// <param name="doc"></param>
        /// <param name="ndoc"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="fechaO"></param>
        /// <param name="fechaC"></param>
        /// <param name="t839_idestado"></param>
        /// <param name="t591_origencvt"></param>
        public static void InsertarProf(SqlTransaction tr, int t001_idficepi, int t583_idexamen, string ndoc,
                                        Nullable<DateTime> fechaO, Nullable<DateTime> fechaC, char t839_idestado, bool t591_origencvt,
                                        Nullable<long> t2_iddocumento, string t595_motivort)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen),
                //ParametroSql.add("@t583_nombre", SqlDbType.VarChar, 100, t583_nombre),
                ParametroSql.add("@t591_ndoc", SqlDbType.VarChar, 250, ndoc),
                ParametroSql.add("@fechaO", SqlDbType.SmallDateTime, 4, fechaO),
                ParametroSql.add("@fechaC", SqlDbType.SmallDateTime, 4, fechaC),
                ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado),
                ParametroSql.add("@t591_origencvt", SqlDbType.Bit, 1, t591_origencvt),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento),
                ParametroSql.add("@t595_motivort", SqlDbType.Text, 2147483647, t595_motivort)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPIEXAMEN_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPIEXAMEN_I", aParam);
        }

        public static void ModificarProf(SqlTransaction tr, int t001_idficepi, int t583_idexamen, Nullable<DateTime> t591_fecha,
                                         Nullable<DateTime> t591_fcaducidad, char t839_idestado
                                        //, string ndoc, bool bCambioDoc, Nullable<long> t2_iddocumento
                                        )
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t583_idexamen", SqlDbType.Int, 4, t583_idexamen),
                ParametroSql.add("@t591_fecha", SqlDbType.SmallDateTime, 4, t591_fecha),
                ParametroSql.add("@t591_fcaducidad", SqlDbType.SmallDateTime, 4, t591_fcaducidad),
                ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado)
                //ParametroSql.add("@t591_ndoc", SqlDbType.VarChar, 250, ndoc),
                //ParametroSql.add("@t591_doc", SqlDbType.Image, 2147483647, doc),
                //ParametroSql.add("@cambioDoc", SqlDbType.Bit, 1, bCambioDoc),
                //ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPIEXAMEN_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPIEXAMEN_U", aParam);
        }

    }
}

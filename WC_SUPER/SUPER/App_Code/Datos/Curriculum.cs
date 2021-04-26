using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Descripción breve de Curriculum
/// </summary>
namespace SUPER.DAL
{
    public class Curriculum
    {
        public Curriculum()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static SqlDataReader MiCVPersonalesOrganizativos(SqlTransaction tr, int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, idFicepi);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PROFESIONAL_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PROFESIONAL_SEL", aParam);


        }

        public static SqlDataReader consultaFicepi(int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_FICEPI", aParam);
        }
        /// <summary>
        /// Recoje los diferentes tipos de preferencias por defecto que tiene un profesional en la pantalla de Consultas de CVT
        /// </summary>
        /// <param name="idFicepi"></param>
        /// <returns></returns>
        public static SqlDataReader GetPreferenciasDefecto(int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PREFERENCIAUSUARIO_CONSULTAS_RELACION", aParam);
        }

        public static SqlDataReader obtenerMovilidad()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MOVILIDADGEO", aParam);
        }

        public static void UpdateDatosOrg(SqlTransaction tr, int T001_IDFICEPI, Nullable<bool> T001_CVINTERNACIONAL, Nullable<int> T001_CVMOVILIDAD, string T001_CVOBSERVA)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T001_IDFICEPI", SqlDbType.Int, 4, T001_IDFICEPI);
            aParam[i++] = ParametroSql.add("@T001_CVINTERNACIONAL", SqlDbType.Bit, 1, T001_CVINTERNACIONAL);
            aParam[i++] = ParametroSql.add("@T001_CVMOVILIDAD", SqlDbType.Int, 4, T001_CVMOVILIDAD);
            aParam[i++] = ParametroSql.add("@T001_CVOBSERVA", SqlDbType.Text, 16, T001_CVOBSERVA);
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_DATOSORG_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_DATOSORG_UPD", aParam);
        }

        public static SqlDataReader MiCVPendientes(SqlTransaction tr, int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVACCIONESPENDIENTES_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVACCIONESPENDIENTES_SEL", aParam);
        }

        public static SqlDataReader MiCVTareasPendientes(SqlTransaction tr, int iPantalla, int idFicepi, Nullable<int> Id, Nullable<int> Id2)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@pantalla", SqlDbType.Int, 4, iPantalla),
                ParametroSql.add("@T001_IDFICEPI", SqlDbType.Int, 4, idFicepi),
                ParametroSql.add("@ID", SqlDbType.Int, 4, Id),
                ParametroSql.add("@ID2", SqlDbType.Int, 4, Id2)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MICVTAREASPENDIENTES_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MICVTAREASPENDIENTES_SEL", aParam);
        }
  
        public static SqlDataReader ObtenerCriterio(int nTipo, string sDenominacion, char sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nTipo", SqlDbType.Int, 4, nTipo);
            aParam[i++] = ParametroSql.add("@sDenominacion", SqlDbType.VarChar, 100, sDenominacion);
            aParam[i++] = ParametroSql.add("@cTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CRITERIO", aParam);
        }
        public static SqlDataReader consultaCuenta(string sDenominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@denominacion", SqlDbType.VarChar, 100, sDenominacion);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_CUENTA", aParam);
        }

        public static SqlDataReader consultaCertificado(string sDenominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@denominacion", SqlDbType.VarChar, 100, sDenominacion);


            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_CERTIFICADOS", aParam);
        }

        public static SqlDataReader getProfesionalesConsultaCadena(SqlTransaction tr, int idFicepi, 
                        bool bTitulaciones, bool bIdiomas, bool bExperiencia, bool bCursos, bool bOtros, bool bCertExam, bool bCondicion,
                        Nullable<char> tipoProf, Nullable<char> estadoProf, 
                        Nullable<int> idNodo, Nullable<int> sn1, Nullable<int> sn2, Nullable<int> sn3, Nullable<int> sn4, 
                        Nullable<short> idCentrab, Nullable<short> cvMovilidad, Nullable<bool> cvInternacional, 
                        Nullable<int> idCodPerfil, Nullable<byte> disponibilidad, Nullable<decimal> costeJornada, 
                        string sCadena)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi),
                ParametroSql.add("@titulaciones", SqlDbType.Bit, 1, bTitulaciones),
                ParametroSql.add("@idiomas", SqlDbType.Bit, 1, bIdiomas),
                ParametroSql.add("@experiencia", SqlDbType.Bit, 1, bExperiencia),
                ParametroSql.add("@cursos", SqlDbType.Bit, 1, bCursos),
                ParametroSql.add("@otros", SqlDbType.Bit, 1, bOtros),
                ParametroSql.add("@certexam", SqlDbType.Bit, 1, bCertExam),
                ParametroSql.add("@condicion", SqlDbType.Bit, 1, bCondicion),
                //Datos personales - Organizativos
                ParametroSql.add("@tipoProf", SqlDbType.Char, 1, tipoProf),
                ParametroSql.add("@estado", SqlDbType.Char, 1, estadoProf),
                ParametroSql.add("@nodo", SqlDbType.Int, 4, idNodo),
                ParametroSql.add("@SN1", SqlDbType.Int, 4, sn1),
                ParametroSql.add("@SN2", SqlDbType.Int, 4, sn2),
                ParametroSql.add("@SN3", SqlDbType.Int, 4, sn3),
                ParametroSql.add("@SN4", SqlDbType.Int, 4, sn4),
                ParametroSql.add("@idCentro", SqlDbType.SmallInt, 2, idCentrab),
                ParametroSql.add("@movilidad", SqlDbType.SmallInt, 2, cvMovilidad),
                ParametroSql.add("@internacional", SqlDbType.Bit, 1, cvInternacional),
                ParametroSql.add("@idPerfil", SqlDbType.Int, 4, idCodPerfil),
                ParametroSql.add("@disponibilidad", SqlDbType.TinyInt, 1, disponibilidad),
                ParametroSql.add("@coste", SqlDbType.Money, 8, costeJornada),
                ParametroSql.add("@cadena", SqlDbType.VarChar, 500, sCadena)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSCADENA", 120, aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CONSCADENA", 120, aParam);
        }


        public static SqlDataReader getProfesionalesConsultaAvanzada(SqlTransaction tr,
                int idFicepi,
            //Datos personales - Organizativos
                string tipo,
                string estado,
                int? CR,
                int? SN1,
                int? SN2,
                int? SN3,
                int? SN4,
                short? centro,
                short? movilidad,
                bool? inttrayint,
                byte? gradodisp,
                decimal? limcoste,
                ArrayList profesionales,
                ArrayList perfiles,
            //Titulación
                byte? tipologia,
                bool? tics,
                byte? modalidad,
                ArrayList titulo_obl_cod,
                ArrayList titulo_obl_den,
                ArrayList titulo_opc_cod,
                ArrayList titulo_opc_den,
            //Idiomas
                ArrayList idioma_obl_cod,
                ArrayList idioma_obl_den,
                ArrayList idioma_opc_cod,
                ArrayList idioma_opc_den,
            //Formación
                int? num_horas,
                int? anno,
            ////Certificados
                ArrayList cert_obl_cod,
                ArrayList cert_obl_den,
                ArrayList cert_opc_cod,
                ArrayList cert_opc_den,
            ////Entidades certificadoras
                ArrayList entcert_obl_cod,
                ArrayList entcert_obl_den,
                ArrayList entcert_opc_cod,
                ArrayList entcert_opc_den,
            ////Entornos tecnológicos
                ArrayList entfor_obl_cod,
                ArrayList entfor_obl_den,
                ArrayList entfor_opc_cod,
                ArrayList entfor_opc_den,
            ////Cursos
                ArrayList curso_obl_cod,
                ArrayList curso_obl_den,
                ArrayList curso_opc_cod,
                ArrayList curso_opc_den,
            ////Experiencias profesionales
            //Cliente / Sector
                string cliente,
                int? sector,
                short? cantidad_expprof,
                byte? unidad_expprof,
                short? anno_expprof,
            //Contenido de Experiencias / Funciones
                ArrayList term_expfun,
                string op_logico,
                short? cantidad_expfun,
                byte? unidad_expfun,
                short? anno_expfun,
            //Experiencia profesional Perfil
                //string op_logico_perfil,
                ArrayList tbl_bus_perfil,
            //Experiencia profesional Perfil / Entorno tecnológico
                ArrayList tbl_bus_perfil_entorno,
            //Experiencia profesional Entorno tecnológico
                //string op_logico_entorno,
                ArrayList tbl_bus_entorno,
            //Experiencia profesional Entorno tecnológico / Perfil
                ArrayList tbl_bus_entorno_perfil
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi),
                //Datos personales - Organizativos
                ParametroSql.add("@t001_tiporecurso", SqlDbType.Char, 1, tipo),
                ParametroSql.add("@estadorecurso", SqlDbType.Char, 1, estado),
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, CR),
                ParametroSql.add("@t391_idsupernodo1", SqlDbType.Int, 4, SN1),
                ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, SN2),
                ParametroSql.add("@t393_idsupernodo3", SqlDbType.Int, 4, SN3),
                ParametroSql.add("@t394_idsupernodo4", SqlDbType.Int, 4, SN4),
                ParametroSql.add("@t009_idcentrab", SqlDbType.SmallInt, 2, centro),
                ParametroSql.add("@t001_cvmovilidad", SqlDbType.SmallInt, 2, movilidad),
                ParametroSql.add("@t001_cvinternacional", SqlDbType.Bit, 1, inttrayint),
                ParametroSql.add("@t535_porcentaje", SqlDbType.TinyInt, 1, gradodisp),
                ParametroSql.add("@t314_costejornada", SqlDbType.Money, 8, limcoste),
                ParametroSql.add("@TMP_PROF", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(profesionales)),
                ParametroSql.add("@TMP_PERF", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(perfiles)),
                //Titulación
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, tipologia),
                ParametroSql.add("@t019_tic", SqlDbType.Bit, 1, tics),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, modalidad),
                ParametroSql.add("@TMP_TITULO_OBL_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(titulo_obl_cod)),
                ParametroSql.add("@TMP_TITULO_OBL_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(titulo_obl_den)),
                ParametroSql.add("@TMP_TITULO_OPC_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(titulo_opc_cod)),
                ParametroSql.add("@TMP_TITULO_OPC_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(titulo_opc_den)),
                //Idiomas
                ParametroSql.add("@TMP_IDIOMA_OBL_COD", SqlDbType.Structured, GetDataTableIdiomaFromArrayList(idioma_obl_cod)),
                ParametroSql.add("@TMP_IDIOMA_OBL_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(idioma_obl_den)),
                ParametroSql.add("@TMP_IDIOMA_OPC_COD", SqlDbType.Structured, GetDataTableIdiomaFromArrayList(idioma_opc_cod)),
                ParametroSql.add("@TMP_IDIOMA_OPC_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(idioma_opc_den)),
                //Formación
                ParametroSql.add("@num_horas", SqlDbType.Int, 4, num_horas),
                ParametroSql.add("@anno", SqlDbType.SmallInt, 2, anno),
                ////Certificados
                ParametroSql.add("@TMP_CERT_OBL_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(cert_obl_cod)),
                ParametroSql.add("@TMP_CERT_OBL_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(cert_obl_den)),
                ParametroSql.add("@TMP_CERT_OPC_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(cert_opc_cod)),
                ParametroSql.add("@TMP_CERT_OPC_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(cert_opc_den)),
                //Entidades Certificadoras
                ParametroSql.add("@TMP_ENTCERT_OBL_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(entcert_obl_cod)),
                ParametroSql.add("@TMP_ENTCERT_OBL_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(entcert_obl_den)),
                ParametroSql.add("@TMP_ENTCERT_OPC_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(entcert_opc_cod)),
                ParametroSql.add("@TMP_ENTCERT_OPC_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(entcert_opc_den)),
                //Entornos tecnológicos
                ParametroSql.add("@TMP_ENTFOR_OBL_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(entfor_obl_cod)),
                ParametroSql.add("@TMP_ENTFOR_OBL_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(entfor_obl_den)),
                ParametroSql.add("@TMP_ENTFOR_OPC_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(entfor_opc_cod)),
                ParametroSql.add("@TMP_ENTFOR_OPC_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(entfor_opc_den)),
                //Cursos
                ParametroSql.add("@TMP_CURSO_OBL_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(curso_obl_cod)),
                ParametroSql.add("@TMP_CURSO_OBL_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(curso_obl_den)),
                ParametroSql.add("@TMP_CURSO_OPC_COD", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(curso_opc_cod)),
                ParametroSql.add("@TMP_CURSO_OPC_DEN", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(curso_opc_den)),
                ////Experiencias profesionales
                //Cliente / Sector
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, cliente),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, sector),
                ParametroSql.add("@unidad_expprof", SqlDbType.TinyInt, 1, unidad_expprof),
                ParametroSql.add("@cantidad_expprof", SqlDbType.SmallInt, 2, cantidad_expprof),
                ParametroSql.add("@anno_expprof", SqlDbType.SmallInt, 2, anno_expprof),
                //Contenido de Experiencias / Funciones
                ParametroSql.add("@TMP_EXPFUNCION", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListDen(term_expfun)),
                ParametroSql.add("@bOperadorLogico", SqlDbType.Char, 1, op_logico),
                ParametroSql.add("@unidad_expfun", SqlDbType.TinyInt, 1, unidad_expfun),
                ParametroSql.add("cantidad_expfun", SqlDbType.SmallInt, 2, cantidad_expfun),
                ParametroSql.add("@anno_expfun", SqlDbType.SmallInt, 2, anno_expfun),
                //Experiencia profesional Perfil
                //ParametroSql.add("@bOperadorLogicoPerfil", SqlDbType.Char, 1, op_logico_perfil),
                ParametroSql.add("@tbl_bus_perfil", SqlDbType.Structured, GetDataTablePerfilEntornoFromArrayList(tbl_bus_perfil)),
                //Experiencia profesional Perfil / Entorno tecnológico
                ParametroSql.add("@tbl_bus_perfil_entorno", SqlDbType.Structured, GetDataTablePerfilEntornoFromArrayList(tbl_bus_perfil_entorno)),
                //Experiencia profesional Entorno tecnológico
                //ParametroSql.add("@bOperadorLogicoEntorno", SqlDbType.Char, 1, op_logico_perfil),
                ParametroSql.add("@tbl_bus_entorno", SqlDbType.Structured, GetDataTablePerfilEntornoFromArrayList(tbl_bus_entorno)),
                //Experiencia profesional Entorno tecnológico / Perfil
                ParametroSql.add("@tbl_bus_entorno_perfil", SqlDbType.Structured, GetDataTablePerfilEntornoFromArrayList(tbl_bus_entorno_perfil))
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSAVANZADA", 300, aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CONSAVANZADA", 300, aParam);
        }

        /// <summary>
        /// Dada una cadena de datos asociados a un idioma separada por , devuelve un DataTable con el contenido
        /// </summary>
        /// <param name="sLista"></param>
        /// <returns></returns>
        private static DataTable GetDataTableIdioma(string sLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("t020_idcodidioma", typeof(int)));
            dt.Columns.Add(new DataColumn("t013_lectura", typeof(byte)));
            dt.Columns.Add(new DataColumn("t013_escritura", typeof(byte)));
            dt.Columns.Add(new DataColumn("t013_oral", typeof(byte)));
            dt.Columns.Add(new DataColumn("tieneTitulo", typeof(bool)));
            if (sLista != "")
            {
                string[] aList = System.Text.RegularExpressions.Regex.Split(sLista, "/#/");
                for (int x = 0; x < aList.Length; x++)
                {
                    if (aList[x] != "")
                    {
                        string[] aL = System.Text.RegularExpressions.Regex.Split(aList[x], ",");
                        if (aL[0] != "")
                        {
                            DataRow row = dt.NewRow();
                            row["t020_idcodidioma"] = int.Parse(aL[0]);
                            row["t013_lectura"] = byte.Parse(aL[1]);
                            row["t013_escritura"] = byte.Parse(aL[2]);
                            row["t013_oral"] = byte.Parse(aL[3]);
                            if (aL[4] == "1")
                                row["tieneTitulo"] = true;
                            else
                                row["tieneTitulo"] = false;
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            return dt;
        }
        private static DataTable GetDataTableIdiomaFromArrayList(ArrayList aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("t020_idcodidioma", typeof(int)));
            dt.Columns.Add(new DataColumn("t013_lectura", typeof(byte)));
            dt.Columns.Add(new DataColumn("t013_escritura", typeof(byte)));
            dt.Columns.Add(new DataColumn("t013_oral", typeof(byte)));
            dt.Columns.Add(new DataColumn("tieneTitulo", typeof(bool)));

            foreach (Dictionary<string, object> oElem in aLista)
            {
                if (oElem["id"].ToString() != "")
                {
                    DataRow row = dt.NewRow();
                    row["t020_idcodidioma"] = oElem["id"];
                    if (oElem["lectura"] != null)
                        row["t013_lectura"] = oElem["lectura"];
                    if (oElem["escritura"] != null)
                        row["t013_escritura"] =  oElem["escritura"];
                    if (oElem["oral"] != null)
                        row["t013_oral"] = oElem["oral"];
                    row["tieneTitulo"] = ((int)oElem["titulo"] == 1) ? true : false;

                    dt.Rows.Add(row);
                }
            }

            return dt;
        }
        private static DataTable GetDataTablePerfilEntornoFromArrayList(ArrayList aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("tipo", typeof(string)));
            dt.Columns.Add(new DataColumn("id_pri", typeof(int)));
            dt.Columns.Add(new DataColumn("id_sec", typeof(int)));
            dt.Columns.Add(new DataColumn("unidad", typeof(byte)));
            dt.Columns.Add(new DataColumn("cantidad", typeof(short)));
            dt.Columns.Add(new DataColumn("anno", typeof(short)));
            dt.Columns.Add(new DataColumn("obligatorio", typeof(bool)));
            if (aLista.Count > 0)
            {
                foreach (Dictionary<string, object> oElem in aLista)
                {
                    if (oElem["id_pri"].ToString() != "")
                    {
                        DataRow row = dt.NewRow();
                        row["tipo"] = oElem["tipo"];
                        row["id_pri"] = oElem["id_pri"];
                        if (oElem["id_sec"] != null) row["id_sec"] = oElem["id_sec"];

                        //row["tipo_tm"] = (oElem["tm"] != null) ? oElem["tm"] : 3;  //3 días
                        row["unidad"] = (oElem["unidad"] != null) ? oElem["unidad"] : 3;  //3 días

                        //row["unidad_tm"] = (oElem["unidad"] != null) ? oElem["unidad"] : 0;  //0
                        row["cantidad"] = (oElem["cantidad"] != null) ? oElem["cantidad"] : 0;  //0

                        if (oElem["anno"] != null) row["anno"] = oElem["anno"];
                        row["obligatorio"] = ((int)oElem["obl"] == 1) ? true : false;

                        dt.Rows.Add(row);
                    }
                }
            }
            return dt;
        }

        public static SqlDataReader ConsultaQuery(SqlTransaction tr, string sqlSelect)
        {
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader(sqlSelect, 120);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sqlSelect, 120);
        }

        #region Combos
        public static SqlDataReader obtenerTipoProfesional()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TIPOPROFS", aParam);
        }


        public static SqlDataReader obtenerTipoExperiencia()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TIPOEXPS", aParam);
        }

        public static SqlDataReader obtenerMedidasTiempo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MEDIDATIEMPO", aParam);
        }

        public static SqlDataReader obtenerSituacionLaboral()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_SITLABS", aParam);
        }

        public static SqlDataReader obtenerCentros(Nullable<bool> cvConsultaExternos, Nullable<bool> cvConsultaBaja)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_cvconsultaexternos", SqlDbType.Bit, 1, cvConsultaExternos);
            aParam[i++] = ParametroSql.add("@t001_cvconsultabaja", SqlDbType.Bit, 1, cvConsultaBaja);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_CENTRO", aParam);
        }
        public static SqlDataReader obtenerIdioma(Nullable<bool> cvConsultaExternos, Nullable<bool> cvConsultaBaja)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_cvconsultaexternos", SqlDbType.Bit, 1, cvConsultaExternos);
            aParam[i++] = ParametroSql.add("@t001_cvconsultabaja", SqlDbType.Bit, 1, cvConsultaBaja);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_IDIOMA", aParam);
        }

        public static SqlDataReader obtenerPerfil(Nullable<bool> cvConsultaExternos, Nullable<bool> cvConsultaBaja)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_cvconsultaexternos", SqlDbType.Bit, 1, cvConsultaExternos);
            aParam[i++] = ParametroSql.add("@t001_cvconsultabaja", SqlDbType.Bit, 1, cvConsultaBaja);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_PERFIL", aParam);
        }

        //public static SqlDataReader obtenerCuenta(string sCuenta)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@denominacion", SqlDbType.VarChar, 100, sCuenta);

        //    return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_CUENTA", aParam);
        //}
        public static SqlDataReader obtenerSector()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_SECTOR", aParam);
        }
        public static SqlDataReader obtenerEntidadesCertificadoras()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTA_ENTCERT", aParam);
        }
        #endregion

        public static SqlDataReader CvSinCompletar_C(bool bNoCV, bool bExcluirExternos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@bNoCV", SqlDbType.Bit, 1, bNoCV);
            aParam[i++] = ParametroSql.add("@bExcluirExternos", SqlDbType.Bit, 1, bExcluirExternos);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_FICEPICV_CAT", aParam);
        }
        public static void FinalizacionCv(int T001_IDFICEPI)//, string T165_COMENTARIO)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T001_IDFICEPI", SqlDbType.Int, 4, T001_IDFICEPI);
            //aParam[i++] = ParametroSql.add("@T165_COMENTARIO", SqlDbType.Text, 16, T165_COMENTARIO);
            SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPICV_UPD", aParam);
        }

        public static SqlDataReader CatalogoPendienteValidar(SqlTransaction tr, int t001_idficepi, bool bEsECV)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@bEsECV", SqlDbType.Bit, 1, bEsECV);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PENDIENTEVALIDAR_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PENDIENTEVALIDAR_CAT", aParam);
        }
        /// <summary>
        /// Catálogo de items de un CV pendientes de validar, cumplimentar o borrador
        /// 06/08/2015 PPOO nos pide que no figuren las Pdtes Validar 
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static SqlDataReader CatalogoPendiente(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PENDIENTE_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PENDIENTE_CAT", aParam);
        }

        public static SqlDataReader CatalogoAdministradores()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_ADMINISTRADORES_CAT", aParam);
        }

        public static SqlDataReader CatalogoConsultores()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CONSULTORES_CAT", aParam);
        }
        public static SqlDataReader CatalogoEncargadosCV()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_ENCARGADOS_CAT", aParam);
        }
        public static SqlDataReader CatalogoValidadoresEnForma()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_VALIDADORES_ENFORMA_CAT", aParam);
        }

        public static void updateFigurasFicepi(SqlTransaction tr, int t001_idficepi, bool esAdministrador, bool esConsultor, bool esEncargado, string sNodo, string sSN1, string sSN2, string sSN3, string sSN4)
        {
            SqlParameter[] aParam = new SqlParameter[9];

            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@esAdministrador", SqlDbType.Bit, 1, esAdministrador);
            aParam[i++] = ParametroSql.add("@esEncargado", SqlDbType.Bit, 1, esEncargado);
            aParam[i++] = ParametroSql.add("@esConsultor", SqlDbType.Bit, 1, esConsultor);
            aParam[i++] = ParametroSql.add("@sNodo", SqlDbType.VarChar, 8000, sNodo);
            aParam[i++] = ParametroSql.add("@sSN1", SqlDbType.VarChar, 8000, sSN1);
            aParam[i++] = ParametroSql.add("@sSN2", SqlDbType.VarChar, 8000, sSN2);
            aParam[i++] = ParametroSql.add("@sSN3", SqlDbType.VarChar, 8000, sSN3);
            aParam[i++] = ParametroSql.add("@sSN4", SqlDbType.VarChar, 8000, sSN4);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_FIGURASFICEPI_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FIGURASFICEPI_UPD", aParam);
        }
        public static void updateDatosVisiblesCVTFicepi(SqlTransaction tr, int t001_idficepi, bool cvConCoste, bool cvConExternos, bool cvConBaja)
        {
            SqlParameter[] aParam = new SqlParameter[4];

            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_cvconsultacoste", SqlDbType.Bit, 1, cvConCoste);
            aParam[i++] = ParametroSql.add("@t001_cvconsultaexternos", SqlDbType.Bit, 1, cvConExternos);
            aParam[i++] = ParametroSql.add("@t001_cvconsultabaja", SqlDbType.Bit, 1, cvConBaja);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_DATOSVISIBLES_FICEPI_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_DATOSVISIBLES_FICEPI_U", aParam);
            //Si me estoy modificando a mi mismo actualizo las variables de sesión
            if (int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString()) == t001_idficepi)
            {
                HttpContext.Current.Session["CVCONSULTAEXTERNOS"] = cvConExternos;
                HttpContext.Current.Session["CVCONSULTABAJA"] = cvConBaja;
                HttpContext.Current.Session["CVCONSULTACOSTE"] = cvConCoste;
            }
        }

        public static SqlDataReader MisEvaluados(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_MISEVALUADOS_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_MISEVALUADOS_C", aParam);
        }

        public static void setCompletadoProf(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_MISEVALUADOS_U_PROF", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_MISEVALUADOS_U_PROF", aParam);
        }
        public static void ActualizadoCV(SqlTransaction tr, int idFicepi)
        {

            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_ACTUALIZADO_CV", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_ACTUALIZADO_CV", aParam);
        }
        public static void setRevisadoActualizadoCV(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_MICVREVISADO_U_PROF", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_MICVREVISADO_U_PROF", aParam);
        }

        public static SqlDataReader RealizarValidacion(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_VAL_MICV_PROF", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_VAL_MICV_PROF", aParam);
        }


        //pestaña sinopsis
        public static SqlDataReader getSinopsis(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_SINOPSIS_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_SINOPSIS_SEL", aParam);
        }

        public static void Grabar(SqlTransaction tr, int t001_idficepi, string sinopsis)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t185_sinopsis", SqlDbType.Text, 2147483647, sinopsis)
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_SINOPSIS_INSUPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_SINOPSIS_INSUPD", aParam);
        }


        public static DataSet ObtenerPlantillaAT(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_PLANTILLA_AT", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_PLANTILLA_AT", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "PerfilExperiencia";
            ds.Tables[5].TableName = "Idiomas";
            ds.Tables[6].TableName = "CursosRecibidos";
            ds.Tables[7].TableName = "CursosImpartidos";
            ds.Tables[8].TableName = "Examenes";

            return ds;
        }

        public static DataSet ObtenerPlantillaCVC(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_PLANTILLA_CVC", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_PLANTILLA_CVC", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "PerfilExperiencia";
            ds.Tables[5].TableName = "Idiomas";
            ds.Tables[6].TableName = "CursosRecibidos";
            ds.Tables[7].TableName = "CursosImpartidos";
            ds.Tables[8].TableName = "Examenes";

            return ds;
        }

        public static DataSet ObtenerPlantillaPPTR(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_PLANTILLA_PPTR", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_PLANTILLA_PPTR", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "PerfilExperiencia";
            ds.Tables[5].TableName = "Idiomas";
            ds.Tables[6].TableName = "CursosRecibidos";

            return ds;
        }

        public static DataSet ObtenerPlantillaPPTC(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_PLANTILLA_PPTC", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_PLANTILLA_PPTC", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "PerfilExperiencia";
            ds.Tables[5].TableName = "Idiomas";
            ds.Tables[6].TableName = "CursosRecibidos";
            ds.Tables[7].TableName = "CursosImpartidos";

            return ds;
        }

        public static DataSet ObtenerPlantillaEP(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_PLANTILLA_EP", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_PLANTILLA_EP", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "PerfilExperiencia";
            ds.Tables[5].TableName = "EntornoExperiencia";
            ds.Tables[6].TableName = "Idiomas";
            ds.Tables[7].TableName = "CursosRecibidos";
            ds.Tables[8].TableName = "CursosImpartidos";

            return ds;
        }

        public static DataSet ObtenerProfParaCVWord02(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_WORD_02", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_WORD_02", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "Idiomas";
            ds.Tables[5].TableName = "CompetenciasPersonales";
            ds.Tables[6].TableName = "CompetenciasTecnicas";

            return ds;
        }

        public static DataSet ObtenerProfParaCVWord03(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_WORD_03", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_WORD_03", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "Idiomas";
            ds.Tables[5].TableName = "CompetenciasPersonales";
            ds.Tables[6].TableName = "CompetenciasTecnicas";

            return ds;
        }

        public static DataSet ObtenerProfParaCVWord04(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_WORD_04", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_WORD_04", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "Idiomas";
            ds.Tables[5].TableName = "CompetenciasPersonales";
            ds.Tables[6].TableName = "CompetenciasTecnicas";

            return ds;
        }

        public static DataSet ObtenerProfParaCVWord05(SqlTransaction tr, string t001_idficepi,
            bool bfiltros,
            string t019_descripcion,
            Nullable<int> t019_idcodtitulo,
            Nullable<byte> t019_tipo,
            Nullable<byte> t019_tic,
            Nullable<byte> t019_modalidad,
            Nullable<int> t582_idcertificado,
            string t582_nombre,
            string lft036_idcodentorno,
            Nullable<int> origenConsulta,
            string nombrecuenta,
            Nullable<int> idcuenta,
            Nullable<int> t483_idsector,
            Nullable<int> t035_codperfile,
            string let036_idcodentorno,
            Nullable<byte> t020_idcodidioma,
            Nullable<byte> nivelidioma
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, t001_idficepi),
                ParametroSql.add("@bfiltros", SqlDbType.Bit, 1, bfiltros),
                ParametroSql.add("@t019_descripcion", SqlDbType.VarChar, 100, t019_descripcion),
                ParametroSql.add("@t019_idcodtitulo", SqlDbType.Int, 4, t019_idcodtitulo),
                ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, t019_tipo),
                ParametroSql.add("@t019_tic", SqlDbType.TinyInt, 1, t019_tic),
                ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, t019_modalidad),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t582_nombre", SqlDbType.VarChar, 200, t582_nombre),
                ParametroSql.add("@lft036_idcodentorno", SqlDbType.VarChar, 8000, lft036_idcodentorno),
                ParametroSql.add("@origenConsulta", SqlDbType.Int, 4, origenConsulta),
                ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta),
                ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta),
                ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector),
                ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile),
                ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno),
                ParametroSql.add("@t020_idcodidioma", SqlDbType.TinyInt, 1, t020_idcodidioma),
                ParametroSql.add("@nivelidioma", SqlDbType.TinyInt, 1, nivelidioma)
            };

            DataSet ds = null;
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_CVT_WORD_05", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CVT_WORD_05", aParam);

            ds.Tables[0].TableName = "DatosPersonales";
            ds.Tables[1].TableName = "FormacionAcademica";
            ds.Tables[2].TableName = "Certificados";
            ds.Tables[3].TableName = "Experiencia";
            ds.Tables[4].TableName = "Idiomas";
            ds.Tables[5].TableName = "CompetenciasPersonales";
            ds.Tables[6].TableName = "CompetenciasTecnicas";

            return ds;
        }

        public static SqlDataReader obtenerPlantillas()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PLANTILLA_CAT", aParam);
        }
        public static DataSet exportarExcelAvanzada(string sListaProfSeleccionados, string sFiltros, Dictionary<string, string> htCampos)
        {
            DataSet ds = null;
            SqlParameter[] aParam = null;
            JObject oCriterios = JObject.Parse(sFiltros);
            #region Pruebas
            /*
            int iAux = (int)oCriterios["bfiltros"];
            //Titulación  
            byte? bAux = (oCriterios["tipologia"].ToString() == "") ? null : (byte?)oCriterios["tipologia"];
            bool? bAux2 = (oCriterios["tics"].ToString() == "") ? null : (bool?)oCriterios["tics"];
            bAux = (oCriterios["modalidad"].ToString() == "") ? null : (byte?)oCriterios["modalidad"];

            DataTable dt = GetDataTableFromListCod((JArray)oCriterios["titulo_obl_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["titulo_obl_den"]);
            dt = GetDataTableFromListCod((JArray)oCriterios["titulo_opc_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["titulo_opc_den"]);
            //Idiomas
            dt = GetDataTableIdiomaFromList((JArray)oCriterios["idioma_obl_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["idioma_obl_den"]);
            dt = GetDataTableIdiomaFromList((JArray)oCriterios["idioma_opc_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["idioma_opc_den"]);
            //Formación
            int? iAux2 = (oCriterios["num_horas"].ToString() == "") ? null : (int?)oCriterios["num_horas"];
            short? iAux3 = (oCriterios["anno"].ToString() == "") ? null : (short?)oCriterios["anno"];
            ////Certificados
            dt = GetDataTableFromListCod((JArray)oCriterios["cert_obl_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["cert_obl_den"]);
            dt = GetDataTableFromListCod((JArray)oCriterios["cert_opc_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["cert_opc_den"]);
            //Entidades Certificadoras
            dt = GetDataTableFromListCod((JArray)oCriterios["entcert_obl_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["entcert_obl_den"]);
            dt = GetDataTableFromListCod((JArray)oCriterios["entcert_opc_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["entcert_opc_den"]);
            //Entornos tecnológicos
            dt = GetDataTableFromListCod((JArray)oCriterios["entfor_obl_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["entfor_obl_den"]);
            dt = GetDataTableFromListCod((JArray)oCriterios["entfor_opc_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["entfor_opc_den"]);
            //Cursos
            dt = GetDataTableFromListCod((JArray)oCriterios["curso_obl_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["curso_obl_den"]);
            dt = GetDataTableFromListCod((JArray)oCriterios["curso_opc_cod"]);
            dt = GetDataTableFromListDen((JArray)oCriterios["curso_opc_den"]);
            ////Experiencias profesionales
            //Cliente / Sector
            string sAux = oCriterios["cliente"].ToString();
            iAux2 = (oCriterios["sector"].ToString() == "") ? null : (int?)oCriterios["sector"];
            iAux3 = (oCriterios["unidad_tm_expprof"].ToString() == "") ? null : (short?)oCriterios["unidad_tm_expprof"];
            bAux = (oCriterios["tipo_tm_expprof"].ToString() == "") ? null : (byte?)oCriterios["tipo_tm_expprof"];
            iAux3 = (oCriterios["anno_expprof"].ToString() == "") ? null : (short?)oCriterios["anno_expprof"];
            //Contenido de Experiencias / Funciones
            dt = SUPER.DAL.Curriculum.GetDataTableFromListDen((JArray)oCriterios["term_expfun"]);
            sAux = oCriterios["op_logico"].ToString();
            iAux3 = (oCriterios["unidad_tm_expfun"].ToString() == "") ? null : (short?)oCriterios["unidad_tm_expfun"];
            bAux = (oCriterios["tipo_tm_expfun"].ToString() == "") ? null : (byte?)oCriterios["tipo_tm_expfun"];
            iAux3 = (oCriterios["anno_expfun"].ToString() == "") ? null : (short?)oCriterios["anno_expfun"];

            //Experiencia profesional Perfil
            dt = GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_perfil"]);
            //Experiencia profesional Perfil / Entorno tecnológico
            dt = GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_perfil_entorno"]);
            //Experiencia profesional Entorno tecnológico
            dt = GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_entorno"]);
            //Experiencia profesional Entorno tecnológico / Perfil
            dt = GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_entorno_perfil"]);
            */
            #endregion

            aParam = new SqlParameter[]{
                    ParametroSql.add("@tbl_idficepi", SqlDbType.Structured, SqlHelper.GetDataTableCod(sListaProfSeleccionados)),
                    ParametroSql.add("@bfiltros", SqlDbType.Int, 4, (int)oCriterios["bfiltros"]),
                    //Titulación  
                    //(oCriterios[""].ToString() == "")? null: (?)oCriterios[""])),
                    ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, (oCriterios["tipologia"].ToString() == "")? null: (byte?)oCriterios["tipologia"]),
                    ParametroSql.add("@t019_tic", SqlDbType.Bit, 1, (oCriterios["tics"].ToString() == "")? null: (bool?)oCriterios["tics"]),
                    ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, (oCriterios["modalidad"].ToString() == "")? null: (byte?)oCriterios["modalidad"]),
                    ParametroSql.add("@TMP_TITULO_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["titulo_obl_cod"])),
                    ParametroSql.add("@TMP_TITULO_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["titulo_obl_den"])),
                    ParametroSql.add("@TMP_TITULO_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["titulo_opc_cod"])),
                    ParametroSql.add("@TMP_TITULO_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["titulo_opc_den"])),
                    //Idiomas
                    ParametroSql.add("@TMP_IDIOMA_OBL_COD", SqlDbType.Structured, GetDataTableIdiomaFromList((JArray)oCriterios["idioma_obl_cod"])),
                    ParametroSql.add("@TMP_IDIOMA_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["idioma_obl_den"])),
                    ParametroSql.add("@TMP_IDIOMA_OPC_COD", SqlDbType.Structured, GetDataTableIdiomaFromList((JArray)oCriterios["idioma_opc_cod"])),
                    ParametroSql.add("@TMP_IDIOMA_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["idioma_opc_den"])),
                    //Formación
                    ParametroSql.add("@num_horas", SqlDbType.Int, 4, (oCriterios["num_horas"].ToString() == "")? null: (int?)oCriterios["num_horas"]),
                    ParametroSql.add("@anno", SqlDbType.SmallInt, 2, (oCriterios["anno"].ToString() == "")? null: (short?)oCriterios["anno"]),
                    ////Certificados
                    ParametroSql.add("@TMP_CERT_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["cert_obl_cod"])),
                    ParametroSql.add("@TMP_CERT_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["cert_obl_den"])),
                    ParametroSql.add("@TMP_CERT_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["cert_opc_cod"])),
                    ParametroSql.add("@TMP_CERT_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["cert_opc_den"])),
                    //Entidades Certificadoras
                    ParametroSql.add("@TMP_ENTCERT_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entcert_obl_cod"])),
                    ParametroSql.add("@TMP_ENTCERT_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entcert_obl_den"])),
                    ParametroSql.add("@TMP_ENTCERT_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entcert_opc_cod"])),
                    ParametroSql.add("@TMP_ENTCERT_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entcert_opc_den"])),
                    //Entornos tecnológicos
                    ParametroSql.add("@TMP_ENTFOR_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entfor_obl_cod"])),
                    ParametroSql.add("@TMP_ENTFOR_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entfor_obl_den"])),
                    ParametroSql.add("@TMP_ENTFOR_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entfor_opc_cod"])),
                    ParametroSql.add("@TMP_ENTFOR_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entfor_opc_den"])),
                    //Cursos
                    ParametroSql.add("@TMP_CURSO_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["curso_obl_cod"])),
                    ParametroSql.add("@TMP_CURSO_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["curso_obl_den"])),
                    ParametroSql.add("@TMP_CURSO_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["curso_opc_cod"])),
                    ParametroSql.add("@TMP_CURSO_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["curso_opc_den"])),
                    ////Experiencias profesionales
                    //Cliente / Sector
                    ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, oCriterios["cliente"].ToString()),
                    ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, (oCriterios["sector"].ToString() == "")? null: (int?)oCriterios["sector"]),
                    ParametroSql.add("@unidad_expprof", SqlDbType.TinyInt, 1, (oCriterios["unidad_expprof"].ToString() == "")? null: (byte?)oCriterios["unidad_expprof"]),
                    ParametroSql.add("@cantidad_expprof", SqlDbType.SmallInt, 2, (oCriterios["cantidad_expprof"].ToString() == "")? null: (short?)oCriterios["cantidad_expprof"]),
                    ParametroSql.add("@anno_expprof", SqlDbType.SmallInt, 2, (oCriterios["anno_expprof"].ToString() == "")? null: (short?)oCriterios["anno_expprof"]),
                    //Contenido de Experiencias / Funciones
                    ParametroSql.add("@TMP_EXPFUNCION", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["term_expfun"])),
                    ParametroSql.add("@bOperadorLogico", SqlDbType.Char, 1, oCriterios["op_logico"].ToString()),
                    ParametroSql.add("@unidad_expfun", SqlDbType.TinyInt, 1, (oCriterios["unidad_expfun"].ToString() == "")? null: (byte?)oCriterios["unidad_expfun"]),
                    ParametroSql.add("@cantidad_expfun", SqlDbType.SmallInt, 2, (oCriterios["cantidad_expfun"].ToString() == "")? null: (short?)oCriterios["cantidad_expfun"]),
                    ParametroSql.add("@anno_expfun", SqlDbType.SmallInt, 2, (oCriterios["anno_expfun"].ToString() == "")? null: (short?)oCriterios["anno_expfun"]),
                    //Experiencia profesional Perfil
                    ParametroSql.add("@tbl_bus_perfil", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_perfil"])),
                    //Experiencia profesional Perfil / Entorno tecnológico
                    ParametroSql.add("@tbl_bus_perfil_entorno", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_perfil_entorno"])),
                    //Experiencia profesional Entorno tecnológico
                    ParametroSql.add("@tbl_bus_entorno", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_entorno"])),
                    //Experiencia profesional Entorno tecnológico / Perfil
                    ParametroSql.add("@tbl_bus_entorno_perfil", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_entorno_perfil"])),
                    
                    ParametroSql.add("@bDP", SqlDbType.Bit, 1, ((htCampos["bDP"] == "") ? null : (bool?)((htCampos["bDP"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bFA", SqlDbType.Bit, 1, ((htCampos["bFA"] == "") ? null : (bool?)((htCampos["bFA"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bCR", SqlDbType.Bit, 1, ((htCampos["bCR"] == "") ? null : (bool?)((htCampos["bCR"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bCI", SqlDbType.Bit, 1, ((htCampos["bCI"] == "") ? null : (bool?)((htCampos["bCI"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bCERT", SqlDbType.Bit, 1, ((htCampos["bCERT"] == "") ? null : (bool?)((htCampos["bCERT"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bID", SqlDbType.Bit, 1, ((htCampos["bID"] == "") ? null : (bool?)((htCampos["bID"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bEXPCLI", SqlDbType.Bit, 1, ((htCampos["bEXPCLI"] == "") ? null : (bool?)((htCampos["bEXPCLI"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bEXPCLIPERF", SqlDbType.Bit, 1, ((htCampos["bEXPCLIPERF"] == "") ? null : (bool?)((htCampos["bEXPCLIPERF"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bPERF", SqlDbType.Bit, 1, ((htCampos["bPERF"] == "") ? null : (bool?)((htCampos["bPERF"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bENT", SqlDbType.Bit, 1, ((htCampos["bENT"] == "") ? null : (bool?)((htCampos["bENT"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bENTPERF", SqlDbType.Bit, 1, ((htCampos["bENTPERF"] == "") ? null : (bool?)((htCampos["bENTPERF"].ToString()=="1")? true:false))),
                    ParametroSql.add("@bENTEXP", SqlDbType.Bit, 1, ((htCampos["bENTEXP"] == "") ? null : (bool?)((htCampos["bENTEXP"].ToString()=="1")? true:false)))
                };
                //Pongo 10h de timeout para ver qué ocurre (la consulta de todo Ibermática tarda más de 4 horas)
                //ds = SqlHelper.ExecuteDataset("SUP_CVT_CONSULTA_EXCEL_AVANZADA", 300, aParam);
                ds = SqlHelper.ExecuteDataset("[SUP_CVT_CONSAVANZADA_EXCEL]", 36000, aParam);
            int indice = 0;

            if (htCampos["bDP"].ToString() == "1")
                ds.Tables[indice++].TableName = "Pestaña general";
            if (htCampos["bFA"].ToString() == "1")
                ds.Tables[indice++].TableName = "Formación académica";
            if (htCampos["bCR"].ToString() == "1" || htCampos["bCI"].ToString() == "1")
                ds.Tables[indice++].TableName = "Cursos";
            if (htCampos["bCERT"].ToString() == "1")
                ds.Tables[indice++].TableName = "Certificados";
            if (htCampos["bID"].ToString() == "1")
                ds.Tables[indice++].TableName = "Idiomas";
            if (htCampos["bEXPCLI"].ToString() == "1")
                ds.Tables[indice++].TableName = "Cliente-experiencias";
            if (htCampos["bEXPCLIPERF"].ToString() == "1")
                ds.Tables[indice++].TableName = "Cliente-experiencia-perfil";
            if (htCampos["bPERF"].ToString() == "1")
                ds.Tables[indice++].TableName = "Perfil";
            if (htCampos["bENT"].ToString() == "1")
                ds.Tables[indice++].TableName = "Entornos";
            if (htCampos["bENTPERF"].ToString() == "1")
                ds.Tables[indice++].TableName = "Entornos-perfil";
            if (htCampos["bENTEXP"].ToString() == "1")
                ds.Tables[indice++].TableName = "Entornos-experiencia";

            return ds;
        }
        //
        public static DataSet pruebaInsertIberdok(int t001_idficepi)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] aParam = null;
                aParam = new SqlParameter[] { ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi) };

                ds = SqlHelper.ExecuteDataset("ZZZ_MIKEL_CARGA_IBERDOK_PRUEBA_PASO1", 36000, aParam);
                ds.Tables[0].TableName = "Profesional";
            }
            catch (Exception e)
            {
                string sError = e.Message;
            }
            return ds;
        }



        private static DataTable GetDataTableFromListCod(JArray aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CODIGOINT", typeof(int)));

            foreach (JValue oValue in aLista)
            //foreach (Newtonsoft.Json.Linq.JObject oValue in aLista)
            {
                DataRow row = dt.NewRow();
                row["CODIGOINT"] = (int)oValue;
                dt.Rows.Add(row);
            }

            return dt;
        }
        private static DataTable GetDataTableFromListDen(JArray aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DENOMINACION", typeof(string)));

            foreach (JValue oValue in aLista)
            //foreach (Newtonsoft.Json.Linq.JObject oValue in aLista)
            {
                DataRow row = dt.NewRow();
                row["DENOMINACION"] = (string)oValue;
                dt.Rows.Add(row);
            }

            return dt;
        }
        private static DataTable GetDataTableIdiomaFromList(JArray aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("t020_idcodidioma", typeof(int)));
            dt.Columns.Add(new DataColumn("t013_lectura", typeof(byte)));
            dt.Columns.Add(new DataColumn("t013_escritura", typeof(byte)));
            dt.Columns.Add(new DataColumn("t013_oral", typeof(byte)));
            dt.Columns.Add(new DataColumn("tieneTitulo", typeof(bool)));

            //foreach (JValue o in aLista)
            foreach (Newtonsoft.Json.Linq.JObject o in aLista)
            {
                DataRow row = dt.NewRow();
                if (o["id"].ToString() != "") row["t020_idcodidioma"] = (int)o["id"];
                if (o["lectura"].ToString() != "") row["t013_lectura"] = (byte)o["lectura"];
                if (o["escritura"].ToString() != "") row["t013_escritura"] = (byte)o["escritura"];
                if (o["oral"].ToString() != "") row["t013_oral"] = (byte)o["oral"];
                row["tieneTitulo"] = ((int)o["titulo"] == 1) ? true : false;

                dt.Rows.Add(row);
            }

            return dt;
        }
        private static DataTable GetDataTablePerfilEntornoFromList(JArray aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("tipo", typeof(string)));
            dt.Columns.Add(new DataColumn("id_pri", typeof(int)));
            dt.Columns.Add(new DataColumn("id_sec", typeof(int)));
            //dt.Columns.Add(new DataColumn("unidad_tm", typeof(short)));
            //dt.Columns.Add(new DataColumn("tipo_tm", typeof(byte)));
            dt.Columns.Add(new DataColumn("unidad", typeof(short)));
            dt.Columns.Add(new DataColumn("cantidad", typeof(byte)));
            dt.Columns.Add(new DataColumn("anno", typeof(short)));
            dt.Columns.Add(new DataColumn("obligatorio", typeof(bool)));

            //foreach (JValue o in aLista)
            foreach (Newtonsoft.Json.Linq.JObject o in aLista)
            {
                DataRow row = dt.NewRow();
                row["tipo"] = o["tipo"];
                row["id_pri"] = o["id_pri"];
                if (o["id_sec"].ToString() != "") row["id_sec"] = (int)o["id_sec"];
                row["cantidad"] = (o["cantidad"].ToString() != "") ? (short)o["cantidad"] : 0;  //0
                row["unidad"] = (o["unidad"].ToString() != "") ? (byte)o["unidad"] : 3;  //1->años, 2->Meses, 3->días
                if (o["anno"].ToString() != "") row["anno"] = (short)o["anno"];
                row["obligatorio"] = ((int)o["obl"] == 1) ? true : false;

                dt.Rows.Add(row);
            }

            return dt;
        }
        public static SqlDataReader getTextoLegal(SqlTransaction tr, int t182_idcondiciones)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t182_idcondiciones", SqlDbType.Int, 4, t182_idcondiciones)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TEXTOLEGAL_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_TEXTOLEGAL_S", aParam);
        }
    }
}

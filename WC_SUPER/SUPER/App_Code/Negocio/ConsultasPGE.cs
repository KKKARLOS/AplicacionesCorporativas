using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for ConsultasPGE
    /// </summary>
    public class ConsultasPGE
    {
        public ConsultasPGE()
        {

        }

        #region Metodos

        /// <summary>
        /// Devuelve tres tablas:
        /// 1- Clientes
        /// 2- Naturalezas
        /// 3- Responsables
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static DataSet ObtenerCombosDatosResumidos(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            return SqlHelper.ExecuteDataset("SUP_CONS_DATOSRESUMIDOS_COMBOS", aParam);
        }

        public static SqlDataReader ObtenerDatosResumidos(int nOpcion,
                int t314_idusuario,
                int nDesde,
                int nHasta,
                Nullable<int> nNivelEstructura,
                Nullable<int> nID,
                string t301_categoria,
                string t305_cualidad,
                Nullable<int> t301_idproyecto,
                Nullable<int> t302_idcliente,
                Nullable<int> t314_idusuario_responsable,
                Nullable<int> t323_idnaturaleza
            )
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@nID", SqlDbType.Int, 4);
            aParam[5].Value = nID;
            aParam[6] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[6].Value = t301_categoria;
            aParam[7] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[7].Value = t305_cualidad;
            aParam[8] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[8].Value = t301_idproyecto;
            aParam[9] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[9].Value = t302_idcliente;
            aParam[10] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[10].Value = t314_idusuario_responsable;
            aParam[11] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
            aParam[11].Value = t323_idnaturaleza;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_DATOSRESUMIDOS", aParam);
        }

        public static SqlDataReader ObtenerProfFigCriterios(int t314_idusuario, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[1].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_FIGU_CRITERIOS", aParam);
        }

        public static SqlDataReader ObtenerCombosDatosResumidosCriterios(int t314_idusuario, int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_DATOSRESUMIDOS_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerDisponibilidadesCriterios(int t314_idusuario, int nDesde, int nHasta, int nFilasMax, short iTipo)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;
            aParam[4] = new SqlParameter("@opcion", SqlDbType.SmallInt, 2);
            aParam[4].Value = iTipo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_DISPONIBILIDADES_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerGarantiasCriterios(int t314_idusuario, int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_GARANTIAS_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerDatosConsumosProveedCriterios(int t314_idusuario, int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSPROVEEDOR_CRITERIOS", aParam);
        }        
        public static SqlDataReader ObtenerProfesionalesCRCriterios(int t314_idusuario,  int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[1].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALESCR_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerDatosCuadreContratos(int t314_idusuario, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[1].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_CUADRE_CONTRATOS_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerCombosInfNoCerrados(int t314_idusuario, int nAnomes, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nAnomes;
            aParam[2] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[2].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_INF_NOCERRADOS_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerCombosInfNoCerradosV2(int t314_idusuario, int nAnomes, int nFilasMax, short iTipo)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@nDesde", SqlDbType.Int, 4, nAnomes),
                ParametroSql.add("@nFilasMax", SqlDbType.Int, 4, nFilasMax),
                ParametroSql.add("@opcion", SqlDbType.SmallInt, 2, iTipo)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_INF_NOCERRADOS_CRITERIOS_V2", aParam);
        }
        public static SqlDataReader ObtenerConsuContaSalProvCriterios(int t314_idusuario, int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUCONTASALPROV_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerGarantias(int t314_idusuario,
                                                    Nullable<int> nNivelEstructura,
                                                    string sSituacionGarantia,
                                                    int    iNDiasGarantia,
                                                    string sClientes,
                                                    string sResponsables,
                                                    string sNaturalezas,
                                                    string sModeloContrato,
                                                    string sContrato,
                                                    string sIDEstructura
                                                )
        {
            SqlParameter[] aParam = new SqlParameter[10];

            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[1].Value = nNivelEstructura;
            aParam[2] = new SqlParameter("@SituacionGarantia", SqlDbType.Char, 1);
            aParam[2].Value = sSituacionGarantia;
            aParam[3] = new SqlParameter("@NDiasGarantia", SqlDbType.Int, 4);
            aParam[3].Value = iNDiasGarantia;
            aParam[4] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[4].Value = sClientes;
            aParam[5] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[5].Value = sResponsables;
            aParam[6] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[6].Value = sNaturalezas;
            aParam[7] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[7].Value = sModeloContrato;
            aParam[8] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[8].Value = sContrato;
            aParam[9] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[9].Value = sIDEstructura;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_GARANTIAS_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_GARANTIAS", aParam);
        }
        public static SqlDataReader ObtenerDatosResumidos(int nOpcion,
                int t314_idusuario,
                int nDesde,
                int nHasta,
                Nullable<int> nNivelEstructura,
                string t301_categoria,
                string t305_cualidad,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN,
                string sFormulas,
                string t422_idmoneda
            )
        {
            SqlParameter[] aParam = new SqlParameter[25];//25
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[16].Value = bComparacionLogica;
            aParam[17] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCNP;
            aParam[18] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN1P;
            aParam[19] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN2P;
            aParam[20] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN3P;
            aParam[21] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN4P;
            aParam[22] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[22].Value = sPSN;
            aParam[23] = new SqlParameter("@sFormulas", SqlDbType.VarChar, 100);
            aParam[23].Value = sFormulas;
            aParam[24] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[24].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_DATOSRESUMIDOS_ADMIN" : "SUP_CONS_DATOSRESUMIDOS_7AM", aParam);  //SUP_CONS_DATOSRESUMIDOS_ADMIN_7AM, 09/04/2013 Ya no tiene sentido, porque la tabla T177 ya contempla el ámbito de visión de los administradores.
            else
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_DATOSRESUMIDOS" : "SUP_CONS_DATOSRESUMIDOS_7AM", aParam);
        }
        public static SqlDataReader ObtenerDatosResumidosGrafico(int t314_idusuario,
                int nAnno,
                string t301_estado,
                string t301_categoria,
                string t305_cualidad,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN
            )
        {
            SqlParameter[] aParam = new SqlParameter[21];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[1].Value = nAnno;
            aParam[2] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[2].Value = t301_estado;
            aParam[3] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[3].Value = t301_categoria;
            aParam[4] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[4].Value = t305_cualidad;
            aParam[5] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[5].Value = sClientes;
            aParam[6] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[6].Value = sResponsables;
            aParam[7] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[7].Value = sNaturalezas;
            aParam[8] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[8].Value = sHorizontal;
            aParam[9] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[9].Value = sModeloContrato;
            aParam[10] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[10].Value = sContrato;
            aParam[11] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[11].Value = sIDEstructura;
            aParam[12] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[12].Value = sSectores;
            aParam[13] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[13].Value = sSegmentos;
            aParam[14] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[14].Value = bComparacionLogica;
            aParam[15] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[15].Value = sCNP;
            aParam[16] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[16].Value = sCSN1P;
            aParam[17] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCSN2P;
            aParam[18] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN3P;
            aParam[19] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN4P;
            aParam[20] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[20].Value = sPSN;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_DATOSRESUMIDOS_GRAFICO_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_DATOSRESUMIDOS_GRAFICO", aParam);
        }

        public static SqlDataReader ObtenerDatosResumidosGraficosNodo(int t303_idnodo, int nAnno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[1].Value = nAnno;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_DATOSRESUMIDOS_GRAFICO_NODO", aParam);
        }
        /*
        public static SqlDataReader ObtenerObraEnCurso(int nOpcion,
                int t314_idusuario,
                int nDesde,
                int nHasta,
                Nullable<int> nNivelEstructura,
                string t301_categoria,
                string t305_cualidad,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN
            )
        {
            SqlParameter[] aParam = new SqlParameter[23];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[16].Value = bComparacionLogica;
            aParam[17] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCNP;
            aParam[18] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN1P;
            aParam[19] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN2P;
            aParam[20] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN3P;
            aParam[21] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN4P;
            aParam[22] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[22].Value = sPSN;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_OBRA_EN_CURSO_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_OBRA_EN_CURSO", aParam);
        }
        */
        public static SqlDataReader ObtenerObraEnCurso(int nOpcion,
                int t314_idusuario,
                int nDesde,
                int nHasta,
                Nullable<int> nNivelEstructura,
                string t301_categoria,
                string t301_estado,
                string t305_cualidad,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN,
                string sCualif,
                string t422_idmoneda
            )
        {
            SqlParameter[] aParam = new SqlParameter[26];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[6].Value = t301_estado;
            aParam[7] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[7].Value = t305_cualidad;
            aParam[8] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[8].Value = sClientes;
            aParam[9] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[9].Value = sResponsables;
            aParam[10] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[10].Value = sNaturalezas;
            aParam[11] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[11].Value = sHorizontal;
            aParam[12] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sModeloContrato;
            aParam[13] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[13].Value = sContrato;
            aParam[14] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[14].Value = sIDEstructura;
            aParam[15] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSectores;
            aParam[16] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[16].Value = sSegmentos;
            aParam[17] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[17].Value = bComparacionLogica;
            aParam[18] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCNP;
            aParam[19] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN1P;
            aParam[20] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN2P;
            aParam[21] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN3P;
            aParam[22] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCSN4P;
            aParam[23] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[23].Value = sPSN;
            aParam[24] = new SqlParameter("@scalifOCFA", SqlDbType.VarChar, 8000);
            aParam[24].Value = sCualif;
            aParam[25] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[25].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_OBRA_EN_CURSO_ADMIN" : "SUP_CONS_OBRA_EN_CURSO_7AM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_OBRA_EN_CURSO" : "SUP_CONS_OBRA_EN_CURSO_7AM", aParam);

        }
        public static DataSet ObtenerDatosFichaEconomica(int t314_idusuario,
                int nDesde,
                int nHasta,
                Nullable<int> nNivelEstructura,
                string t301_categoria,
                string t305_cualidad,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN,
                string t422_idmoneda
            )
        {
            SqlParameter[] aParam = new SqlParameter[23];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[3].Value = nNivelEstructura;
            aParam[4] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[4].Value = t301_categoria;
            aParam[5] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[5].Value = t305_cualidad;
            aParam[6] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[6].Value = sClientes;
            aParam[7] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[7].Value = sResponsables;
            aParam[8] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[8].Value = sNaturalezas;
            aParam[9] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[9].Value = sHorizontal;
            aParam[10] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[10].Value = sModeloContrato;
            aParam[11] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sContrato;
            aParam[12] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[12].Value = sIDEstructura;
            aParam[13] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[13].Value = sSectores;
            aParam[14] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSegmentos;
            aParam[15] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[15].Value = bComparacionLogica;
            aParam[16] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[16].Value = sCNP;
            aParam[17] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCSN1P;
            aParam[18] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN2P;
            aParam[19] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN3P;
            aParam[20] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN4P;
            aParam[21] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[21].Value = sPSN;
            aParam[22] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[22].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteDataset(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_FICHAECONOMICA_ADMIN" : "SUP_CONS_FICHAECONOMICA_7AM", aParam);
            else
                return SqlHelper.ExecuteDataset(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_FICHAECONOMICA" : "SUP_CONS_FICHAECONOMICA_7AM", aParam);

        }
        public static DataSet ObtenerDatosSegRenta(int t314_idusuario,
        int nDesde,
        int nHasta,
        Nullable<int> nNivelEstructura,
        string t301_categoria,
        string t305_cualidad,
        string sClientes,
        string sResponsables,
        string sNaturalezas,
        string sHorizontal,
        string sModeloContrato,
        string sContrato,
        string sIDEstructura,
        string sSectores,
        string sSegmentos,
        bool bComparacionLogica,
        string sCNP,
        string sCSN1P,
        string sCSN2P,
        string sCSN3P,
        string sCSN4P,
        string sPSN,
        string t422_idmoneda
    )
        {
            SqlParameter[] aParam = new SqlParameter[23];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[3].Value = nNivelEstructura;
            aParam[4] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[4].Value = t301_categoria;
            aParam[5] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[5].Value = t305_cualidad;
            aParam[6] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[6].Value = sClientes;
            aParam[7] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[7].Value = sResponsables;
            aParam[8] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[8].Value = sNaturalezas;
            aParam[9] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[9].Value = sHorizontal;
            aParam[10] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[10].Value = sModeloContrato;
            aParam[11] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sContrato;
            aParam[12] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[12].Value = sIDEstructura;
            aParam[13] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[13].Value = sSectores;
            aParam[14] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSegmentos;
            aParam[15] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[15].Value = bComparacionLogica;
            aParam[16] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[16].Value = sCNP;
            aParam[17] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCSN1P;
            aParam[18] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN2P;
            aParam[19] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN3P;
            aParam[20] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN4P;
            aParam[21] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[21].Value = sPSN;
            aParam[22] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[22].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteDataset(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_SEGRENTA_ADMIN" : "SUP_CONS_SEGRENTA_7AM", aParam);
            else
                return SqlHelper.ExecuteDataset(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_CONS_SEGRENTA" : "SUP_CONS_SEGRENTA_7AM", aParam);

        }

        public static SqlDataReader InformeProyectosAsignados(string t301_estado,
                int t314_idusuario,
                int nDesde,
                int nHasta,
                Nullable<int> nNivelEstructura,
                string t301_categoria,
                string t305_cualidad,
                Nullable<int> t301_idproyecto,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN,
                string sOrgComercial
            )
        {
            SqlParameter[] aParam = new SqlParameter[25];
            aParam[0] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[0].Value = t301_estado;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
            aParam[7] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[7].Value = t301_idproyecto;
            aParam[8] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[8].Value = sClientes;
            aParam[9] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[9].Value = sResponsables;
            aParam[10] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[10].Value = sNaturalezas;
            aParam[11] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[11].Value = sHorizontal;
            aParam[12] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sModeloContrato;
            aParam[13] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[13].Value = sContrato;
            aParam[14] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[14].Value = sIDEstructura;
            aParam[15] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSectores;
            aParam[16] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[16].Value = sSegmentos;
            aParam[17] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[17].Value = bComparacionLogica;
            aParam[18] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCNP;
            aParam[19] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN1P;
            aParam[20] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN2P;
            aParam[21] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN3P;
            aParam[22] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCSN4P;
            aParam[23] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[23].Value = sPSN;
            aParam[24] = new SqlParameter("@sOrgComercial", SqlDbType.VarChar, 8000);
            aParam[24].Value = sOrgComercial;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_INF_PROYASIG_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_INF_PROYASIG", aParam);
        }

        public static SqlDataReader ObtenerConsuConta(int nOpcion,
            int t314_idusuario,
            int nDesde,
            int nHasta,
            Nullable<int> nNivelEstructura,
            string t301_categoria,
            string t305_cualidad,
            string sClientes,
            string sResponsables,
            string sNaturalezas,
            string sHorizontal,
            string sModeloContrato,
            string sContrato,
            string sIDEstructura,
            string sSectores,
            string sSegmentos,
            bool bComparacionLogica,
            string sCNP,
            string sCSN1P,
            string sCSN2P,
            string sCSN3P,
            string sCSN4P,
            string sPSN,
            Nullable<int> t329_idclaseeco,
            string sProveedores,
            string t422_idmoneda
        )
        {
            SqlParameter[] aParam = new SqlParameter[26];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[16].Value = bComparacionLogica;
            aParam[17] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCNP;
            aParam[18] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN1P;
            aParam[19] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN2P;
            aParam[20] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN3P;
            aParam[21] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN4P;
            aParam[22] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[22].Value = sPSN;
            aParam[23] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[23].Value = t329_idclaseeco;
            aParam[24] = new SqlParameter("@sProveedores", SqlDbType.VarChar, 8000);
            aParam[24].Value = sProveedores;
            aParam[25] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[25].Value = t422_idmoneda;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUCONTA", aParam);
        }
        public static SqlDataReader ObtenerSaldoProveedores(int nOpcion,
            int t314_idusuario,
            int nDesde,
            int nHasta,
            Nullable<int> nNivelEstructura,
            string t301_categoria,
            string t305_cualidad,
            string sClientes,
            string sResponsables,
            string sNaturalezas,
            string sHorizontal,
            string sModeloContrato,
            string sContrato,
            string sIDEstructura,
            string sSectores,
            string sSegmentos,
            bool bComparacionLogica,
            string sCNP,
            string sCSN1P,
            string sCSN2P,
            string sCSN3P,
            string sCSN4P,
            string sPSN,
            Nullable<int> t329_idclaseeco,
            string sProveedores,
            string t422_idmoneda
        )
        {
            SqlParameter[] aParam = new SqlParameter[26];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[16].Value = bComparacionLogica;
            aParam[17] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCNP;
            aParam[18] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN1P;
            aParam[19] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN2P;
            aParam[20] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN3P;
            aParam[21] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN4P;
            aParam[22] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[22].Value = sPSN;
            aParam[23] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[23].Value = t329_idclaseeco;
            aParam[24] = new SqlParameter("@sProveedores", SqlDbType.VarChar, 8000);
            aParam[24].Value = sProveedores;
            aParam[25] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[25].Value = t422_idmoneda;

            return SqlHelper.ExecuteSqlDataReader("SUP_SALPROV", aParam);
        }

        public static SqlDataReader ObtenerConsumosProfesionalesGrafico(int t314_idusuario,
            int nAnno,
            string t301_estado,
            string t301_categoria,
            string t305_cualidad,
            string sClientes,
            string sResponsables,
            string sNaturalezas,
            string sHorizontal,
            string sModeloContrato,
            string sContrato,
            string sIDEstructura,
            string sSectores,
            string sSegmentos,
            bool bComparacionLogica,
            string sCNP,
            string sCSN1P,
            string sCSN2P,
            string sCSN3P,
            string sCSN4P,
            string sPSN
        )
        {
            SqlParameter[] aParam = new SqlParameter[21];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[1].Value = nAnno;
            aParam[2] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[2].Value = t301_estado;
            aParam[3] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[3].Value = t301_categoria;
            aParam[4] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[4].Value = t305_cualidad;
            aParam[5] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[5].Value = sClientes;
            aParam[6] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[6].Value = sResponsables;
            aParam[7] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[7].Value = sNaturalezas;
            aParam[8] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[8].Value = sHorizontal;
            aParam[9] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[9].Value = sModeloContrato;
            aParam[10] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[10].Value = sContrato;
            aParam[11] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[11].Value = sIDEstructura;
            aParam[12] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[12].Value = sSectores;
            aParam[13] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[13].Value = sSegmentos;
            aParam[14] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[14].Value = bComparacionLogica;
            aParam[15] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[15].Value = sCNP;
            aParam[16] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[16].Value = sCSN1P;
            aParam[17] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCSN2P;
            aParam[18] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN3P;
            aParam[19] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN4P;
            aParam[20] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[20].Value = sPSN;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_CONSPERMES_GRAFICO_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_CONSPERMES_GRAFICO", aParam);
        }

        public static SqlDataReader ObtenerCriteriosSIB(int t314_idusuario, int nFilasMax, short iTipo)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
            ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
            ParametroSql.add("@nFilasMax", SqlDbType.Int, 4, nFilasMax),
            ParametroSql.add("@opcion", SqlDbType.SmallInt, 2, iTipo)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_BBII_CRITERIOS", aParam);
        }

        /// <summary>
        /// Obtiene los datos de la consulta de auditoría económica
        /// </summary>
        public static DataSet getDatosAuditoria(int idFicepiEntrada, int t314_idusuario, string sAccion,
                                                Nullable<int> nDesde, Nullable<int> nHasta, DateTime dtDesde, DateTime dtHasta,
                                                Nullable<int> nNivelEstructura, string t301_estado, 
                                                string t301_categoria, string t305_cualidad, bool bComparacionLogica,
                                                string sClientes, string sResponsables, string sNaturalezas,
                                                string sHorizontal, string sModeloContrato, string sContrato,
                                                string sIDEstructura, string sSectores, string sSegmentos,
                                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P,
                                                string sPSN)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepiEntrada),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde),
                ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta),
                //ParametroSql.add("@dDesde", SqlDbType.SmallDateTime, 4, dtDesde),
                ParametroSql.add("@dDesde", SqlDbType.DateTime, 8, dtDesde),
                ParametroSql.add("@dHasta", SqlDbType.DateTime, 8, dtHasta),
                ParametroSql.add("@nNivelEstructura", SqlDbType.SmallInt, 2, nNivelEstructura),
                ParametroSql.add("@t301_categoria", SqlDbType.Char, 1, t301_categoria),
                ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado),
                ParametroSql.add("@t305_cualidad", SqlDbType.Char, 1, t305_cualidad),
                ParametroSql.add("@bComparacionLogica", SqlDbType.Bit, 1, bComparacionLogica),
                ParametroSql.add("@sClientes", SqlDbType.VarChar, 8000, sClientes),
                ParametroSql.add("@sResponsables", SqlDbType.VarChar, 8000, sResponsables),
                ParametroSql.add("@sNaturalezas", SqlDbType.VarChar, 8000, sNaturalezas),
                ParametroSql.add("@sHorizontal", SqlDbType.VarChar, 8000, sHorizontal),
                ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, 8000, sModeloContrato),
                ParametroSql.add("@sContrato", SqlDbType.VarChar, 8000, sContrato),
                ParametroSql.add("@sIDEstructura", SqlDbType.VarChar, 8000, sIDEstructura),
                ParametroSql.add("@sSectores", SqlDbType.VarChar, 8000, sSectores),
                ParametroSql.add("@sSegmentos", SqlDbType.VarChar, 8000, sSegmentos),
                ParametroSql.add("@sCNP", SqlDbType.VarChar, 8000, sCNP),
                ParametroSql.add("@sCSN1P", SqlDbType.VarChar, 8000, sCSN1P),
                ParametroSql.add("@sCSN2P", SqlDbType.VarChar, 8000, sCSN2P),
                ParametroSql.add("@sCSN3P", SqlDbType.VarChar, 8000, sCSN3P),
                ParametroSql.add("@sCSN4P", SqlDbType.VarChar, 8000, sCSN4P),
                ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN),
                ParametroSql.add("@sAccion", SqlDbType.VarChar, 3, sAccion)
            };

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteDataset("SUP_CONS_AUDIT_DATOECO_ADM", aParam);
            else
                return SqlHelper.ExecuteDataset("SUP_CONS_AUDIT_DATOECO_USU", aParam);

        }

        #endregion
    }
}

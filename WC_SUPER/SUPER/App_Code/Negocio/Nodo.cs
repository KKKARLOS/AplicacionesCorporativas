using System;
using System.Text;
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
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class NODO
    {
        #region Propiedades y Atributos complementarios

        private string _t313_denominacion;
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }

        private string _DesResponsable;
        public string DesResponsable
        {
            get { return _DesResponsable; }
            set { _DesResponsable = value; }
        }

        private int? _ultrecalculoGF;
        public int? ultrecalculoGF
        {
            get { return _ultrecalculoGF; }
            set { _ultrecalculoGF = value; }
        }

        private string _t391_denominacion_CSN1P;
        public string t391_denominacion_CSN1P
        {
            get { return _t391_denominacion_CSN1P; }
            set { _t391_denominacion_CSN1P = value; }
        }

        private bool _t391_obligatorio_CSN1P;
        public bool t391_obligatorio_CSN1P
        {
            get { return _t391_obligatorio_CSN1P; }
            set { _t391_obligatorio_CSN1P = value; }
        }

        private string _t392_denominacion_CSN2P;
        public string t392_denominacion_CSN2P
        {
            get { return _t392_denominacion_CSN2P; }
            set { _t392_denominacion_CSN2P = value; }
        }

        private bool _t392_obligatorio_CSN2P;
        public bool t392_obligatorio_CSN2P
        {
            get { return _t392_obligatorio_CSN2P; }
            set { _t392_obligatorio_CSN2P = value; }
        }

        private string _t393_denominacion_CSN3P;
        public string t393_denominacion_CSN3P
        {
            get { return _t393_denominacion_CSN3P; }
            set { _t393_denominacion_CSN3P = value; }
        }

        private bool _t393_obligatorio_CSN3P;
        public bool t393_obligatorio_CSN3P
        {
            get { return _t393_obligatorio_CSN3P; }
            set { _t393_obligatorio_CSN3P = value; }
        }

        private string _t394_denominacion_CSN4P;
        public string t394_denominacion_CSN4P
        {
            get { return _t394_denominacion_CSN4P; }
            set { _t394_denominacion_CSN4P = value; }
        }

        private bool _t394_obligatorio_CSN4P;
        public bool t394_obligatorio_CSN4P
        {
            get { return _t394_obligatorio_CSN4P; }
            set { _t394_obligatorio_CSN4P = value; }
        }
        private bool _t303_noalertas;
        public bool t303_noalertas
        {
            get { return _t303_noalertas; }
            set { _t303_noalertas = value; }
        }
        private bool _t303_cualificacionCVT;
        public bool t303_cualificacionCVT
        {
            get { return _t303_cualificacionCVT; }
            set { _t303_cualificacionCVT = value; }
        }

        private int? _t055_idcalifOCFA;
        public int? t055_idcalifOCFA
        {
            get { return _t055_idcalifOCFA; }
            set { _t055_idcalifOCFA = value; }
        }
        private string _t055_denominacion;
        public string t055_denominacion
        {
            get { return _t055_denominacion; }
            set { _t055_denominacion = value; }
        }


        private int? _ta212_idorganizacioncomercial;
        public int? ta212_idorganizacioncomercial
        {
            get { return _ta212_idorganizacioncomercial; }
            set { _ta212_idorganizacioncomercial = value; }
        }
        private string _ta212_denominacion;
        public string ta212_denominacion
        {
            get { return _ta212_denominacion; }
            set { _ta212_denominacion = value; }
        }
        #endregion

        public NODO(int nIdNodo, string sDenominacion)
        {
            this.t303_idnodo = nIdNodo;
            this.t303_denominacion = sDenominacion;
        }
        public static List<NODO> ListaGlobal()
        {
            if (HttpContext.Current.Cache["Lista_Nodos"] == null)
            {
                List<NODO> oLista = new List<NODO>();
                SqlDataReader dr = NODO.Catalogo(false);
                while (dr.Read())
                {
                    oLista.Add(new NODO((int)dr["t303_idnodo"], dr["t303_denominacion"].ToString()));
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Nodos", oLista, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<NODO>)HttpContext.Current.Cache["Lista_Nodos"];
            }
        }

        public static SqlDataReader Obtener(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_Nodo_SELECT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_Nodo_SELECT", aParam);
        }
        public static SqlDataReader ObtenerNodosCalendario(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_NODOSCALENDARIO", aParam);
        }
        public static NODO ObtenerNodo(SqlTransaction tr, int t303_idnodo)
        {
            NODO o = new NODO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_NODO_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODO_O", aParam);

            if (dr.Read())
            {
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t303_denabreviada"] != DBNull.Value)
                    o.t303_denabreviada = (string)dr["t303_denabreviada"];
                if (dr["t303_masdeungf"] != DBNull.Value)
                    o.t303_masdeungf = (bool)dr["t303_masdeungf"];
                if (dr["t391_idsupernodo1"] != DBNull.Value)
                    o.t391_idsupernodo1 = int.Parse(dr["t391_idsupernodo1"].ToString());
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t303_cierreECOestandar"] != DBNull.Value)
                    o.t303_cierreECOestandar = (bool)dr["t303_cierreECOestandar"];
                if (dr["t303_ultcierreeco"] != DBNull.Value)
                    o.t303_ultcierreeco = int.Parse(dr["t303_ultcierreeco"].ToString());
                if (dr["t303_estado"] != DBNull.Value)
                    o.t303_estado = (bool)dr["t303_estado"];
                if (dr["t303_modelocostes"] != DBNull.Value)
                    o.t303_modelocostes = (string)dr["t303_modelocostes"];
                if (dr["t303_modelotarifas"] != DBNull.Value)
                    o.t303_modelotarifas = (string)dr["t303_modelotarifas"];
                if (dr["t303_generareplica"] != DBNull.Value)
                    o.t303_generareplica = (bool)dr["t303_generareplica"];
                if (dr["t303_pgrcg"] != DBNull.Value)
                    o.t303_pgrcg = (bool)dr["t303_pgrcg"];
                if (dr["t303_porctolerancia"] != DBNull.Value)
                    o.t303_porctolerancia = byte.Parse(dr["t303_porctolerancia"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["t303_orden"] != DBNull.Value)
                    o.t303_orden = int.Parse(dr["t303_orden"].ToString());
                if (dr["t303_representativo"] != DBNull.Value)
                    o.t303_representativo = (bool)dr["t303_representativo"];
                if (dr["t303_interfacehermes"] != DBNull.Value)
                    o.t303_interfacehermes = (string)dr["t303_interfacehermes"];
                if (dr["t303_defectocalcularGF"] != DBNull.Value)
                    o.t303_defectocalcularGF = (bool)dr["t303_defectocalcularGF"];
                if (dr["ultrecalculoGF"] != DBNull.Value)
                    o.ultrecalculoGF = int.Parse(dr["ultrecalculoGF"].ToString());
                if (dr["t303_defectomailiap"] != DBNull.Value)
                    o.t303_defectomailiap = (bool)dr["t303_defectomailiap"];
                if (dr["t303_cierreIAPestandar"] != DBNull.Value)
                    o.t303_cierreIAPestandar = (bool)dr["t303_cierreIAPestandar"];
                if (dr["t303_cierreIAPestandar"] != DBNull.Value)
                    o.t303_cierreIAPestandar = (bool)dr["t303_cierreIAPestandar"];
                if (dr["t303_ultcierreIAP"] != DBNull.Value)
                    o.t303_ultcierreIAP = int.Parse(dr["t303_ultcierreIAP"].ToString());
                if (dr["t303_compcontprod"] != DBNull.Value)
                    o.t303_compcontprod = (bool)dr["t303_compcontprod"];
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];
                if (dr["Responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["Responsable"];
                if (dr["t303_defectoPIG"] != DBNull.Value)
                    o.t303_defectoPIG = (bool)dr["t303_defectoPIG"];
                if (dr["t303_margencesionprof"] != DBNull.Value)
                    o.t303_margencesionprof = float.Parse(dr["t303_margencesionprof"].ToString());
                if (dr["t303_interesGF"] != DBNull.Value)
                    o.t303_interesGF = float.Parse(dr["t303_interesGF"].ToString());
                if (dr["t303_denominacion_CNP"] != DBNull.Value)
                    o.t303_denominacion_CNP = (string)dr["t303_denominacion_CNP"];
                if (dr["t303_obligatorio_CNP"] != DBNull.Value)
                    o.t303_obligatorio_CNP = (bool)dr["t303_obligatorio_CNP"];
                if (dr["t303_asignarperfiles"] != DBNull.Value)
                    o.t303_asignarperfiles = (string)dr["t303_asignarperfiles"];
                if (dr["t303_desglose"] != DBNull.Value)
                    o.t303_desglose = (bool)dr["t303_desglose"];
                if (dr["t303_pgrcg"] != DBNull.Value)
                    o.t303_pgrcg = (bool)dr["t303_pgrcg"];
                if (dr["t303_controlhuecos"] != DBNull.Value)
                    o.t303_controlhuecos = (bool)dr["t303_controlhuecos"];
                if (dr["t303_tipolinterna"] != DBNull.Value)
                    o.t303_tipolinterna = (bool)dr["t303_tipolinterna"];
                if (dr["t303_tipolespecial"] != DBNull.Value)
                    o.t303_tipolespecial = (bool)dr["t303_tipolespecial"];
                if (dr["t303_tipolproductivaSC"] != DBNull.Value)
                    o.t303_tipolproductivaSC = (bool)dr["t303_tipolproductivaSC"];
                if (dr["t303_defectoadmiterecursospst"] != DBNull.Value)
                    o.t303_defectoadmiterecursospst = (bool)dr["t303_defectoadmiterecursospst"];
                if (dr["t621_idovsap"] != DBNull.Value)
                    o.t621_idovsap = (string)dr["t621_idovsap"];
                if (dr["t303_msa"] != DBNull.Value)
                    o.t303_msa = (bool)dr["t303_msa"];
                if (dr["t303_noalertas"] != DBNull.Value)
                    o.t303_noalertas = (bool)dr["t303_noalertas"];
                if (dr["t303_cualificacionCVT"] != DBNull.Value)
                    o.t303_cualificacionCVT = (bool)dr["t303_cualificacionCVT"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t055_idcalifOCFA"] != DBNull.Value)
                    o.t055_idcalifOCFA = int.Parse(dr["t055_idcalifOCFA"].ToString());
                if (dr["t055_denominacion"] != DBNull.Value)
                    o.t055_denominacion = (string)dr["t055_denominacion"];

                if (dr["ta212_idorganizacioncomercial"] != DBNull.Value)
                    o.ta212_idorganizacioncomercial = int.Parse(dr["ta212_idorganizacioncomercial"].ToString());
                if (dr["ta212_denominacion"] != DBNull.Value)
                    o.ta212_denominacion = (string)dr["ta212_denominacion"];

                if (dr["t303_activoqeq"] != DBNull.Value)
                    o.activoqeq = (bool)dr["t303_activoqeq"];

                if (dr["t303_soloinstrumental"] != DBNull.Value)
                    o.instrumental = (bool)dr["t303_soloinstrumental"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de NODO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T303_NODO,
        /// y devuelve una instancia u objeto del tipo NODO
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	24/06/2009 11:31:47
        /// </history>
        /// -----------------------------------------------------------------------------
        public static NODO SelectEnTransaccion(SqlTransaction tr, int t303_idnodo)
        {
            NODO o = new NODO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODO_S_INTR", aParam);

            if (dr.Read())
            {
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t303_masdeungf"] != DBNull.Value)
                    o.t303_masdeungf = (bool)dr["t303_masdeungf"];
                if (dr["t391_idsupernodo1"] != DBNull.Value)
                    o.t391_idsupernodo1 = int.Parse(dr["t391_idsupernodo1"].ToString());
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t303_cierreECOestandar"] != DBNull.Value)
                    o.t303_cierreECOestandar = (bool)dr["t303_cierreECOestandar"];
                if (dr["t303_ultcierreeco"] != DBNull.Value)
                    o.t303_ultcierreeco = int.Parse(dr["t303_ultcierreeco"].ToString());
                if (dr["t303_estado"] != DBNull.Value)
                    o.t303_estado = (bool)dr["t303_estado"];
                if (dr["t303_modelocostes"] != DBNull.Value)
                    o.t303_modelocostes = (string)dr["t303_modelocostes"];
                if (dr["t303_modelotarifas"] != DBNull.Value)
                    o.t303_modelotarifas = (string)dr["t303_modelotarifas"];
                if (dr["t303_generareplica"] != DBNull.Value)
                    o.t303_generareplica = (bool)dr["t303_generareplica"];
                if (dr["t303_pgrcg"] != DBNull.Value)
                    o.t303_pgrcg = (bool)dr["t303_pgrcg"];
                if (dr["t303_porctolerancia"] != DBNull.Value)
                    o.t303_porctolerancia = byte.Parse(dr["t303_porctolerancia"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["t303_orden"] != DBNull.Value)
                    o.t303_orden = int.Parse(dr["t303_orden"].ToString());
                if (dr["t303_representativo"] != DBNull.Value)
                    o.t303_representativo = (bool)dr["t303_representativo"];
                if (dr["t303_interfacehermes"] != DBNull.Value)
                    o.t303_interfacehermes = (string)dr["t303_interfacehermes"];
                if (dr["t303_defectocalcularGF"] != DBNull.Value)
                    o.t303_defectocalcularGF = (bool)dr["t303_defectocalcularGF"];
                if (dr["t303_defectomailiap"] != DBNull.Value)
                    o.t303_defectomailiap = (bool)dr["t303_defectomailiap"];
                if (dr["t303_cierreIAPestandar"] != DBNull.Value)
                    o.t303_cierreIAPestandar = (bool)dr["t303_cierreIAPestandar"];
                if (dr["t303_ultcierreIAP"] != DBNull.Value)
                    o.t303_ultcierreIAP = int.Parse(dr["t303_ultcierreIAP"].ToString());
                if (dr["t303_compcontprod"] != DBNull.Value)
                    o.t303_compcontprod = (bool)dr["t303_compcontprod"];
                if (dr["t303_defectoPIG"] != DBNull.Value)
                    o.t303_defectoPIG = (bool)dr["t303_defectoPIG"];
                if (dr["t303_margencesionprof"] != DBNull.Value)
                    o.t303_margencesionprof = float.Parse(dr["t303_margencesionprof"].ToString());
                if (dr["t303_interesGF"] != DBNull.Value)
                    o.t303_interesGF = float.Parse(dr["t303_interesGF"].ToString());
                if (dr["t303_denominacion_CNP"] != DBNull.Value)
                    o.t303_denominacion_CNP = (string)dr["t303_denominacion_CNP"];
                if (dr["t303_obligatorio_CNP"] != DBNull.Value)
                    o.t303_obligatorio_CNP = (bool)dr["t303_obligatorio_CNP"];
                if (dr["t303_asignarperfiles"] != DBNull.Value)
                    o.t303_asignarperfiles = (string)dr["t303_asignarperfiles"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de NODO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static bool bMasDeUnGF(int nIdCR)
        {
            bool bRes = false;
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "SUP_CRS", nIdCR);

            if (dr.Read())
            {
                bRes = bool.Parse(dr["t303_masdeungf"].ToString());
            }
            dr.Close();
            dr.Dispose();
            return bRes;
        }

        public static SqlDataReader CatalogoFiguras(int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURANODO_CF", aParam);
        }
        public static SqlDataReader UsuarioVisibilidad(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_USU_VISI", aParam);
        }

        //Catálogo de Nodos activos.
        public static SqlDataReader Catalogo(bool bMostrarInstrumentales)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@bMostrarInstrumentales", SqlDbType.Bit, 1);
            aParam[0].Value = bMostrarInstrumentales;
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_C1", aParam);
        }
        public static SqlDataReader CatalogoInterno(int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_INTERNO", aParam);
        }
        public static SqlDataReader CatalogoGrupo(int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_GRUPO", aParam);
        }
        public static SqlDataReader CatalogoConEstructura()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_NODOESTR_CAT", aParam);
        }
        public static SqlDataReader CatalogoParaReasignacion(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@bAdmin", SqlDbType.Bit, 1);
            aParam[1].Value = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();

            return SqlHelper.ExecuteSqlDataReader("SUP_NODOREASIGNACION_CAT", aParam);
        }
        public static SqlDataReader CatalogoCierreMensual(int t303_ultcierreeco)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_ultcierreeco", SqlDbType.Int, 4);
            aParam[0].Value = t303_ultcierreeco;
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_CIERREMENSUAL", aParam);
        }
        public static SqlDataReader CatalogoObraEnCurso(int nAnno)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[0].Value = nAnno;
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_OBRAENCURSO", aParam);
        }
        public static SqlDataReader CatalogoObraEnCursoDotacion(int nAnnoMes, int nMeses)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnnoMes", SqlDbType.Int, 4);
            aParam[0].Value = nAnnoMes;
            aParam[1] = new SqlParameter("@nMeses", SqlDbType.Int, 4);
            aParam[1].Value = nMeses;

            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_OBRAENCURSO_DOTACION", aParam);
        }

        public static int NumNodosAdministrables(int t314_idusuario)//, bool bAccesoOT)
        {//bAccesoOT indica si los miembros de oficina técnica tienen acceso o no
            object obj;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {//Si es administrador no merece la pena investigar que figuras tiene
                SqlParameter[] aParam = new SqlParameter[0];
                obj = SqlHelper.ExecuteScalar("SUP_NODO_C2_NUM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = t314_idusuario;
                //if (!bAccesoOT)
                //    return int.Parse(SqlHelper.ExecuteScalar("SUP_GETVISION_NODOS_ADMIN_NUM", aParam));
                //else
                //    return int.Parse(SqlHelper.ExecuteScalar("SUP_GETVISION_NODOS_ADMIN_OT_NUM", aParam));
                obj = SqlHelper.ExecuteScalar("SUP_GETVISION_NODOS_ADMIN_NUM", aParam);
            }
            return int.Parse(obj.ToString());
        }
        public static SqlDataReader CatalogoAdministrables(int t314_idusuario, bool bAccesoOT)
        {//bAccesoOT indica si los miembros de oficina técnica tienen acceso o no
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {//Si es administrador no merece la pena investigar que figuras tiene
                SqlParameter[] aParam = new SqlParameter[0];
                return SqlHelper.ExecuteSqlDataReader("SUP_NODO_C2", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = t314_idusuario;
                if (!bAccesoOT)
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_NODOS_ADMIN", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_NODOS_ADMIN_OT", aParam);
            }
        }
        public static SqlDataReader CatalogoBySuperNodo1(int t391_idsupernodo1)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;

            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_C_BySuperNodo1", aParam);
        }
        public static SqlDataReader Excel(string strCodigos, string sEstado, string t422_idmoneda,int t314_idusuario)
        {

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                SqlParameter[] aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@CODIGO", SqlDbType.VarChar, 4200);
                aParam[0].Value = strCodigos;
                aParam[1] = new SqlParameter("@ESTADO", SqlDbType.VarChar, 1);
                aParam[1].Value = sEstado;
                aParam[2] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
                aParam[2].Value = t422_idmoneda;
                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_C_ADM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[4];
                aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
                aParam[0].Value = t314_idusuario;
                aParam[1] = new SqlParameter("@CODIGO", SqlDbType.VarChar, 4200);
                aParam[1].Value = strCodigos;
                aParam[2] = new SqlParameter("@ESTADO", SqlDbType.VarChar, 1);
                aParam[2].Value = sEstado;
                aParam[3] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
                aParam[3].Value = t422_idmoneda;

                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_C_USU", aParam);
            }
        }
        public static int Update(SqlTransaction tr, int t303_idnodo, string t303_denominacion_cnp, bool t303_obligatorio_cnp)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t303_denominacion_CNP", SqlDbType.Text, 15);
            aParam[1].Value = t303_denominacion_cnp;
            aParam[2] = new SqlParameter("@t303_obligatorio_CNP", SqlDbType.Bit, 1);
            aParam[2].Value = t303_obligatorio_cnp;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_NODO_UCU", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_UCU", aParam);
        }

        public static int UpdateCierreIAP(SqlTransaction tr, int t303_idnodo, int t303_ultcierreIAP)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t303_ultcierreIAP", SqlDbType.Int, 4);
            aParam[1].Value = t303_ultcierreIAP;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_NODO_U1", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_U1", aParam);
        }
        public static int UpdateCierreEco(SqlTransaction tr, int t303_idnodo, int t303_ultcierreECO)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t303_ultcierreECO", SqlDbType.Int, 4);
            aParam[1].Value = t303_ultcierreECO;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_NODO_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_U2", aParam);
        }
        public static int UpdateAutoTraspasoIAP(SqlTransaction tr, int t303_idnodo, bool t303_autotraspasoIAP)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t303_autotraspasoIAP", SqlDbType.Bit, 1);
            aParam[1].Value = t303_autotraspasoIAP;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_NODO_AUTOTRASIAP_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_AUTOTRASIAP_U", aParam);
        }
        public static int UpdateUltTraspasoIAPPorNodo(SqlTransaction tr, string sNodos, bool bTraspasoEstandar, int idUser)
        {
            //int idUser=int.Parse(HttpContext.Current.Session["usuario_entrada"].ToString());
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@sNodos", SqlDbType.VarChar, 4000, sNodos),
                ParametroSql.add("@t314_idusuario_utpn", SqlDbType.Int, 4, idUser),
                ParametroSql.add("@t303_fecha_utpn", SqlDbType.SmallDateTime, 4, DateTime.Now),
                ParametroSql.add("@bTraspasoEstandar", SqlDbType.Bit, 1, bTraspasoEstandar)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_NODO_TRASIAPBYNODO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_TRASIAPBYNODO_U", aParam);
        }

        
        //Nodos a los que tiene acceso el usuario por figuras en la estructura
        public static SqlDataReader ObtenerNodos(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_NODOS", aParam);
        }
        public static SqlDataReader ObtenerNodosGestor(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (nUsuario == -1)
            {
                aParam = new SqlParameter[0];
                return SqlHelper.ExecuteSqlDataReader("SUP_NODOGESTOR_ADM", aParam);
            }
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_NODOGESTOR", aParam);
        }
        public static SqlDataReader ObtenerNodosUsuarioSegunVisionProyectosECO(SqlTransaction tr, int nUsuario, bool bSoloActivos)
        {
            SqlParameter[] aParam;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[0].Value = bSoloActivos;
            }
            else
            {
                aParam = new SqlParameter[2];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[1].Value = bSoloActivos;
            }
            if (tr == null)
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReader("SUP_NODO_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_NODOS_PROY_USUARIO_ECO", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODO_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_NODOS_PROY_USUARIO_ECO", aParam);
        }
        public static SqlDataReader ObtenerNodosUsuarioEsRespDelegColab(SqlTransaction tr, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nUsuario", SqlDbType.Int, 4, nUsuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_NODOS_USU_RDC", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODOS_USU_RDC", aParam);
        }
        public static SqlDataReader ObtenerNodosUsuarioEsRespDelegColab(SqlTransaction tr, int nUsuario, string t303_denominacion, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nUsuario", SqlDbType.Int, 4, nUsuario);
            aParam[i++] = ParametroSql.add("@t303_denominacion", SqlDbType.Text, 50, t303_denominacion);
            aParam[i++] = ParametroSql.add("@sTipoBusq", SqlDbType.Char, 1, sTipoBusqueda);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_NODOS_USU_RDC_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODOS_USU_RDC_CAT", aParam);
        }

        public static SqlDataReader CatalogoAdministrador(string t303_denominacion, string sTipoBusqueda)
		{
			SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = @t303_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;         

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_NODOESTR_REL", aParam);
		}
        public static SqlDataReader ObtenerNodosUsuarioSegunVisionProyectosTEC(SqlTransaction tr, int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
        {
            SqlParameter[] aParam = null;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[0].Value = bSoloActivos;
            }
            else 
            {
                aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
                aParam[1].Value = bMostrarBitacoricos;
                aParam[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[2].Value = bSoloActivos;
            }


            if (tr == null)
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_NODOS_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_NODOS_PROY_USUARIO_TEC", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_NODOS_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_NODOS_PROY_USUARIO_TEC", aParam);
        }		

        public static int ObtenerEmpresaNodoMes(SqlTransaction tr, int t303_idnodo, int t325_anomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EMPRESANODOMES_SEL", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EMPRESANODOMES_SEL", aParam));

        }

        public static SqlDataReader CatalogoGastosFinancieros()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_GASTOSFINANCIEROSNODOS_CAT", aParam);
        }
        public static SqlDataReader CatalogoNodosNaturalezas()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZASNODOS_CAT", aParam);
        }
        public static SqlDataReader CatalogoNodosNaturalezasMarcados()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZASNODOSMARCADOS_CAT", aParam);
        }

        public static int UpdateDefectoPIG(SqlTransaction tr, int t303_idnodo, bool t303_defectoPIG)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t303_defectoPIG", SqlDbType.Bit, 1);
            aParam[1].Value = t303_defectoPIG;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_NODO_UPIG", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_UPIG", aParam);
        }

        public void ObtenerCualificadoresEstructura()
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_NODO_CUALIFICADORES", aParam);
            if (dr.Read())
            {
                this.t303_denominacion_CNP = dr["t303_denominacion_CNP"].ToString();
                this.t303_obligatorio_CNP = (bool)dr["t303_obligatorio_CNP"];
                this.t391_denominacion_CSN1P = dr["t391_denominacion_CSN1P"].ToString();
                this.t391_obligatorio_CSN1P = (bool)dr["t391_obligatorio_CSN1P"];
                this.t392_denominacion_CSN2P = dr["t392_denominacion_CSN2P"].ToString();
                this.t392_obligatorio_CSN2P = (bool)dr["t392_obligatorio_CSN2P"];
                this.t393_denominacion_CSN3P = dr["t393_denominacion_CSN3P"].ToString();
                this.t393_obligatorio_CSN3P = (bool)dr["t393_obligatorio_CSN3P"];
                this.t394_denominacion_CSN4P = dr["t394_denominacion_CSN4P"].ToString();
                this.t394_obligatorio_CSN4P = (bool)dr["t394_obligatorio_CSN4P"];
            }
            dr.Close();
            dr.Dispose();
        }
        public static bool bExistenMesesAbiertos(SqlTransaction tr, int t303_idnodo, int t303_ultcierreeco)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t303_ultcierreeco", SqlDbType.Int, 4);
            aParam[1].Value = t303_ultcierreeco;

            return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODO_MESESABIERTOS", aParam)) == 0) ? false : true;
        }
        public static SqlDataReader ObtenerNodosUsuarioCualificadores(SqlTransaction tr, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_NODOS_USU", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODOS_USU", aParam);
        }

        public static SqlDataReader CEEC_RESUMEN(bool bValorAsignado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@bValorAsignado", SqlDbType.Bit, 1);
            aParam[0].Value = @bValorAsignado;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CEEC_RESUMEN", aParam);
        }

        public static SqlDataReader ObtenerCualificadores(int nUsuario, int nIdEstructura, bool bActivo)
        {

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                SqlParameter[] aParam = new SqlParameter[2];

                aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
                aParam[0].Value = nIdEstructura;
                aParam[1] = new SqlParameter("@t476_activo", SqlDbType.Bit, 1);
                aParam[1].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESNODO_ADM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
                aParam[1].Value = nIdEstructura;
                aParam[2] = new SqlParameter("@t476_activo", SqlDbType.Bit, 1);
                aParam[2].Value = bActivo;


                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESNODO_USU", aParam);
            }
        }

        public static void GenerarReplicasMesesCerrados()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            SqlHelper.ExecuteNonQuery("SUP_GENERARREPLICASMESESCERRADOS", aParam);
        }
        public static string fgGetCadenaID(string sTipo, string sId)
        {
            string sRes = "";
            if (sTipo == "1")
                sRes = sId + "-0-0-0-0";
            else
            {
                string sProcAlm = "";
                SqlParameter[] aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@idElem", SqlDbType.Int, 4);
                aParam[0].Value = int.Parse(sId);
                switch (sTipo)
                {
                    case "2":
                        sProcAlm = "SUP_SUPERNODO3_GETID";
                        break;
                    case "3":
                        sProcAlm = "SUP_SUPERNODO2_GETID";
                        break;
                    case "4":
                        sProcAlm = "SUP_SUPERNODO1_GETID";
                        break;
                    case "5":
                        sProcAlm = "SUP_NODO_GETID";
                        break;
                }
                SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
                if (dr.Read())
                {
                    sRes = dr["cadId"].ToString();
                    sRes = sRes.Substring(0, sRes.Length - 2);
                }
                dr.Close();
                dr.Dispose();
            }
            return sRes;
        }

        //public static void TraspasarConsumosIAP(SqlTransaction tr, string sNodosATraspasarIAP, string sSobreescribir, bool bTraspasoEstandar, int idUser)
        public static void TraspasarConsumosIAP(SqlTransaction tr, int anomes, int t314_idusuario, bool bCerrarIAPNodos, bool bTraspasarEsfuerzos, string sNodosATraspasarIAP)
        {
            //string sEstadoMes = "";
            //int nSMPSN = 0, iUltCierreECO=0;
            //DataSet dsProf = null;
            //int idPE = 0, idUsuario = 0;
            //try
            //{
            #region OLD
            /*
            DataSet ds = CONSPERMES.ObtenerPSNaTraspasarByNodoDS(tr, sNodosATraspasarIAP);
            foreach (DataRow oPSN in ds.Tables[0].Rows)
            {
                idPE = (int)oPSN["t301_idproyecto"];

                iUltCierreECO = PROYECTOSUBNODO.ObtenerUltCierreEcoNodoPSN(tr, (int)oPSN["t305_idproyectosubnodo"]);
                if (iUltCierreECO >= (int)oPSN["annomes_traspaso"])
                {
                    throw (new Exception("No se ha realizado el traspaso, debido a que el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " '" + oPSN["t303_denominacion"].ToString() + "' se encuentra cerrado en el mes a traspasar."));
                }
                else
                {
                    nSMPSN = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"]);



                    if (nSMPSN == 0)
                    {
                        if (oPSN["tiene_consumos"].ToString() == "1")
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"]);
                            if (sEstadoMes == "C")
                                continue;

                            nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"], sEstadoMes, 0, 0, false, 0, 0);
                        }
                    }
                    else
                    {
                        //01/08/2011. Por petición de Xabi Antoñana se traspasan los consumos aunque el mes esté cerrado
                        //SEGMESPROYECTOSUBNODO oSegMes = SEGMESPROYECTOSUBNODO.Obtener(tr, nSMPSN);
                        //if (oSegMes.t325_estado == "C")
                        //    continue;

                        if (sSobreescribir == "1") CONSPERMES.DeleteByT325_idsegmesproy(tr, nSMPSN);
                    }

                    #region Datos Profesionales
                    if (oPSN["tiene_consumos"].ToString() == "1") //si tiene consumos técnicos (IAP)
                    {
                        dsProf = CONSPERMES.ObtenerDatosPSNaTraspasarDS(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"], oPSN["t301_modelocoste"].ToString(), true, (sSobreescribir == "1") ? false : true);

                        foreach (DataRow oProf in dsProf.Tables[0].Rows)
                        {
                            idUsuario = (int)oProf["t314_idusuario"];

                            double nUnidades = (oPSN["t301_modelocoste"].ToString() == "J") ? double.Parse(oProf["jornadas_adaptadas"].ToString()) : double.Parse(oProf["horas_reportadas_proy"].ToString());
                            if (nUnidades != 0)
                            {
                                CONSPERMES.Insert(tr, nSMPSN,
                                                (int)oProf["t314_idusuario"],
                                                (oPSN["t301_modelocoste"].ToString() == "J") ? double.Parse(oProf["jornadas_adaptadas"].ToString()) : double.Parse(oProf["horas_reportadas_proy"].ToString()),
                                                decimal.Parse(oProf["t330_costecon"].ToString()),
                                                decimal.Parse(oProf["t330_costerep"].ToString()),
                                                (oProf["t303_idnodo"] != DBNull.Value) ? (int?)oProf["t303_idnodo"] : null,
                                                (oProf["t313_idempresa"] != DBNull.Value) ? (int?)oProf["t313_idempresa"] : null);
                            }
                        }
                        dsProf.Dispose();
                    }

                    #endregion

                    SEGMESPROYECTOSUBNODO.UpdateTraspasoIAP(tr, nSMPSN, true);
                }
            }

            NODO.UpdateUltTraspasoIAPPorNodo(tr, sNodosATraspasarIAP, bTraspasoEstandar, idUser);
            */
            #endregion
            
            ArrayList tbl_nodos = new ArrayList();
            string[] aNodos = Regex.Split(sNodosATraspasarIAP, "/");
            for (int i = 0; i < aNodos.Length; i++)
            {
                if (aNodos[i] != "")
                {
                    tbl_nodos.Add(aNodos[i]);
                }
            }

            DataTable dtNodos = GetTablaNodosCierre(tbl_nodos);
            SUPER.Capa_Datos.NODO.TraspasarConsumosIAP(tr, anomes, t314_idusuario, bCerrarIAPNodos, bTraspasarEsfuerzos, dtNodos);
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Proyecto=" + idPE.ToString() + " Usuario=" + idUsuario.ToString() + " " + e.Message);
            //}
        }

        public static int Duplicar(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NODO_I_COP", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODO_I_COP", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// devuelve el nº de empleados activos de un nodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int GetNumEmpleados(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NUMEMPLEADOS_NODO", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NUMEMPLEADOS_NODO", aParam));
        }
        /// <summary>
        /// Obtiene el nº de empleados asignados a un nodo estén o no de baja
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t303_idnodo"></param>
        /// <returns></returns>
        public static int GetNumEmpleadosTotal(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EMPLEADOS_NODO_COUNT", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EMPLEADOS_NODO_COUNT", aParam));
        }
        public static int GetNumProyectos(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROYECTOS_NODO_COUNT", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTOS_NODO_COUNT", aParam));
        }

        public static decimal getTipocambioMonedaNodoMes(SqlTransaction tr, int t303_idnodo, int anomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@anomes", SqlDbType.Int, 4, anomes);

            if (tr == null)
                return Convert.ToDecimal(SqlHelper.ExecuteScalar("SUP_TIPOCAMBIONODO_S", aParam));
            else
                return Convert.ToDecimal(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TIPOCAMBIONODO_S", aParam));
        }

        public static string obtenerNodos()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.NODO.ObtenerNodos(null);

            sb.Append("<table id='tblDatos' class='texto MM' style='width: 450px;'>");
            sb.Append("<colgroup><col style='width:450px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("onclick='mm(event);' ondblclick='insertarItem(this);' onmousedown='DD(event)' style='height:20px'>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W440' ondblclick='insertarItem(this.parentNode.parentNode);' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Código:</label>" + dr["t303_idnodo"].ToString() + "<br><label style='width:60px;'>Denom.:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        public static string obtenerNodosExcepciones()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.NODO.ObtenerNodosExcepciones(null);

            sb.Append("<table id='tblDatos2' style='width: 450px;' class='texto MM' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px' /><col style='width:435px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' bd='' style='height:20px;' onmousedown='DD(event);' onclick='mm(event)'>");
                sb.Append("<td style='padding-left:2px;'><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgFN.gif'></td>");
                sb.Append("</td><td><div class='NBR' style='width:435px'>" + dr["t303_denominacion"].ToString() + "</div></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        public static string Grabar(string strDatos)
        {
            string sElementosInsertados = "";

            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión. " + ex.Message));
            }
            #endregion

            try
            {
                if (strDatos != "") //No se ha modificado nada 
                {
                    string[] aDatos = Regex.Split(strDatos, "///");
                    foreach (string oDatos in aDatos)
                    {
                        if (oDatos == "") continue;
                        string[] aValores = Regex.Split(oDatos, "##");

                        ///aValores[0] = bd
                        ///aValores[1] = t001_idficepi

                        switch (aValores[0])
                        {
                            case "I":
                                SUPER.Capa_Datos.NODO.Update(tr, int.Parse(aValores[1]), true);
                                if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                                else sElementosInsertados += "//" + aValores[1];
                                break;
                            case "D":
                                SUPER.Capa_Datos.NODO.Update(tr, int.Parse(aValores[1]), false);
                                break;
                        }

                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al actualizar las alertas del profesional.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            sResul = "OK@#@" + sElementosInsertados;
            return sResul;
        }
        public static DataSet FicherosIAP(SqlTransaction tr, int @nAnomes, string @sIdNodos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnomes", SqlDbType.Int, 4);
            aParam[0].Value = @nAnomes;
		    aParam[1] = new SqlParameter("@sIdNodos", SqlDbType.Text, 2147483647);
		    aParam[1].Value = sIdNodos;
			
            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_NODO_ANOMES_FIC", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_NODO_ANOMES_FIC", aParam);
        }
        public static SqlDataReader FavoritosIAP(string @sIdNodos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sIdNodos", SqlDbType.Text, 2147483647);
            aParam[0].Value = @sIdNodos;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_FAVORITOS", aParam);
        }

        public static DataTable GetTablaNodosCierre(ArrayList aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("numero", typeof(int)));
            dt.Columns.Add(new DataColumn("estandar", typeof(bool)));

            for (int x = 0; x < aLista.Count; x++)
            {
                if (aLista[x].ToString() != "")
                {
                    string[] aElems = Regex.Split(aLista[x].ToString(), ",");

                    DataRow row = dt.NewRow();
                    row["numero"] = aElems[0];
                    if (aElems[1] == "S")
                        row["estandar"] = true;
                    else
                        row["estandar"] = false;

                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

    }
}
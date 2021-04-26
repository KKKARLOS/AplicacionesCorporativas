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
    /// Summary description for Calendario
    /// </summary>
    public partial class TAREAPSP
    {
        #region Atributos y Propiedades complementarios

        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }
        private string _t305_cualidad;
        public string t305_cualidad
        {
            get { return _t305_cualidad; }
            set { _t305_cualidad = value; }
        }
        private string _t305_seudonimo;
        public string t305_seudonimo
        {
            get { return _t305_seudonimo; }
            set { _t305_seudonimo = value; }
        }
        private short _t303_idnodo;
        public short t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }
        private string _t303_denominacion;
        public string t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }
        private int _num_proyecto;
        public int num_proyecto
        {
            get { return _num_proyecto; }
            set { _num_proyecto = value; }
        }

        private string _nom_proyecto;
        public string nom_proyecto
        {
            get { return _nom_proyecto; }
            set { _nom_proyecto = value; }
        }

        private string _t331_despt;
        public string t331_despt
        {
            get { return _t331_despt; }
            set { _t331_despt = value; }
        }

        private int _t334_idfase;
        public int t334_idfase
        {
            get { return _t334_idfase; }
            set { _t334_idfase = value; }
        }

        private string _t334_desfase;
        public string t334_desfase
        {
            get { return _t334_desfase; }
            set { _t334_desfase = value; }
        }

        private string _t335_desactividad;
        public string t335_desactividad
        {
            get { return _t335_desactividad; }
            set { _t335_desactividad = value; }
        }

        private int _cod_cliente;
        public int cod_cliente
        {
            get { return _cod_cliente; }
            set { _cod_cliente = value; }
        }

        private string _nom_cliente;
        public string nom_cliente
        {
            get { return _nom_cliente; }
            set { _nom_cliente = value; }
        }

        private int _t346_idpst;
        public int t346_idpst
        {
            get { return _t346_idpst; }
            set { _t346_idpst = value; }
        }

        private string _t346_codpst;
        public string t346_codpst
        {
            get { return _t346_codpst; }
            set { _t346_codpst = value; }
        }

        private string _t346_despst;
        public string t346_despst
        {
            get { return _t346_despst; }
            set { _t346_despst = value; }
        }

        private string _sPromotor;
        public string sPromotor
        {
            get { return _sPromotor; }
            set { _sPromotor = value; }
        }

        private string _sModificador;
        public string sModificador
        {
            get { return _sModificador; }
            set { _sModificador = value; }
        }

        private string _sFinalizador;
        public string sFinalizador
        {
            get { return _sFinalizador; }
            set { _sFinalizador = value; }
        }
        private string _sCerrador;
        public string sCerrador
        {
            get { return _sCerrador; }
            set { _sCerrador = value; }
        }

        private DateTime? _dPrimerConsumo;
        public DateTime? dPrimerConsumo
        {
            get { return _dPrimerConsumo; }
            set { _dPrimerConsumo = value; }
        }

        private DateTime? _dUltimoConsumo;
        public DateTime? dUltimoConsumo
        {
            get { return _dUltimoConsumo; }
            set { _dUltimoConsumo = value; }
        }

        private DateTime? _dFinEstimado;
        public DateTime? dFinEstimado
        {
            get { return _dFinEstimado; }
            set { _dFinEstimado = value; }
        }

        private double _nTotalEstimado;
        public double nTotalEstimado
        {
            get { return _nTotalEstimado; }
            set { _nTotalEstimado = value; }
        }

        private double _nConsumidoHoras;
        public double nConsumidoHoras
        {
            get { return _nConsumidoHoras; }
            set { _nConsumidoHoras = value; }
        }

        private double _nPendienteEstimado;
        public double nPendienteEstimado
        {
            get { return _nPendienteEstimado; }
            set { _nPendienteEstimado = value; }
        }

        private double _nConsumidoJornadas;
        public double nConsumidoJornadas
        {
            get { return _nConsumidoJornadas; }
            set { _nConsumidoJornadas = value; }
        }

        private double _nAvanceTeorico;
        public double nAvanceTeorico
        {
            get { return _nAvanceTeorico; }
            set { _nAvanceTeorico = value; }
        }

        private bool _bUnicaEnActividad;
        public bool bUnicaEnActividad
        {
            get { return _bUnicaEnActividad; }
            set { _bUnicaEnActividad = value; }
        }

        private bool _bOTCHeredada;
        public bool bOTCHeredada
        {
            get { return _bOTCHeredada; }
            set { _bOTCHeredada = value; }
        }
        private bool _bOTCerror;
        public bool bOTCerror
        {
            get { return _bOTCerror; }
            set { _bOTCerror = value; }
        }

        private bool _heredaCR;
        public bool heredaCR
        {
            get { return _heredaCR; }
            set { _heredaCR = value; }
        }
        private bool _heredaPE;
        public bool heredaPE
        {
            get { return _heredaPE; }
            set { _heredaPE = value; }
        }

        private bool _t305_admiterecursospst;
        public bool t305_admiterecursospst
        {
            get { return _t305_admiterecursospst; }
            set { _t305_admiterecursospst = value; }
        }

        private bool _t305_avisorecursopst;
        public bool t305_avisorecursopst
        {
            get { return _t305_avisorecursopst; }
            set { _t305_avisorecursopst = value; }
        }
        //Para los datos relativos a la tabla t336
        private DateTime? _t336_ffp;
        public DateTime? t336_ffp
        {
            get { return _t336_ffp; }
            set { _t336_ffp = value; }
        }

        private DateTime? _t336_ffe;
        public DateTime? t336_ffe
        {
            get { return _t336_ffe; }
            set { _t336_ffe = value; }
        }

        private double _t336_etp;
        public double t336_etp
        {
            get { return _t336_etp; }
            set { _t336_etp = value; }
        }

        private double _t336_ete;
        public double t336_ete
        {
            get { return _t336_ete; }
            set { _t336_ete = value; }
        }

        private byte _nCompletado;
        public byte nCompletado
        {
            get { return _nCompletado; }
            set { _nCompletado = value; }
        }

        private string _t336_indicaciones;
        public string t336_indicaciones
        {
            get { return _t336_indicaciones; }
            set { _t336_indicaciones = value; }
        }

        private string _t336_comentario;
        public string t336_comentario
        {
            get { return _t336_comentario; }
            set { _t336_comentario = value; }
        }

        //Estado del proyecto económico
        private string _t301_estado;
        public string t301_estado
        {
            get { return _t301_estado; }
            set { _t301_estado = value; }
        }
        //Modo de facturación
        private string _t324_denominacion;
        public string t324_denominacion
        {
            get { return _t324_denominacion; }
            set { _t324_denominacion = value; }
        }

        private bool _t301_esreplicable;
        public bool t301_esreplicable
        {
            get { return _t301_esreplicable; }
            set { _t301_esreplicable = value; }
        }

        private bool _t336_estado;
        public bool t336_estado
        {
            get { return _t336_estado; }
            set { _t336_estado = value; }
        }

        private bool _t305_opd;
        public bool t305_opd
        {
            get { return _t305_opd; }
            set { _t305_opd = value; }
        }

        private string _t305_nivelpresupuesto;
        public string t305_nivelpresupuesto
        {
            get { return _t305_nivelpresupuesto; }
            set { _t305_nivelpresupuesto = value; }
        }

        public bool t332_horascomplementarias { get; set; }
        #endregion

        #region	Métodos públicos

        /// <summary>
        /// 
        /// Obtiene los datos generales de una tarea determinada,
        /// correspondientes a la tabla t332_TAREAPSP, y devuelve una
        /// instancia u objeto del tipo TAREAPSP
        /// </summary>
        public static TAREAPSP Obtener(SqlTransaction tr, int nIdTarea)
        {
            TAREAPSP o = new TAREAPSP();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = int.Parse(nIdTarea.ToString());

            SqlDataReader dr;
            if (tr == null)
                //dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAPSPS", aParam);
                dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_P0_GRAL_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_P0_GRAL_S", aParam);

            if (dr.Read())
            {
                if (dr["t332_idtarea"] != DBNull.Value)
                    o.t332_idtarea = (int)dr["t332_idtarea"];
                if (dr["t332_destarea"] != DBNull.Value)
                    o.t332_destarea = (string)dr["t332_destarea"];
                //o.t332_destarea = Utilidades.unescape(dr["t332_destarea"]);
                if (dr["t332_destarealong"] != DBNull.Value)
                    o.t332_destarealong = (string)dr["t332_destarealong"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = short.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t331_idpt"] != DBNull.Value)
                    o.t331_idpt = (int)dr["t331_idpt"];
                if (dr["t334_idfase"] != DBNull.Value)
                    o.t334_idfase = (int)dr["t334_idfase"];
                if (dr["t335_idactividad"] != DBNull.Value)
                    o.t335_idactividad = (int)dr["t335_idactividad"];
                if (dr["t332_notificable"] != DBNull.Value)
                    o.t332_notificable = (bool)dr["t332_notificable"];
                if (dr["t332_fiv"] != DBNull.Value)
                    o.t332_fiv = (DateTime)dr["t332_fiv"];
                if (dr["t332_ffv"] != DBNull.Value)
                    o.t332_ffv = (DateTime)dr["t332_ffv"];
                if (dr["t332_estado"] != DBNull.Value)
                    o.t332_estado = byte.Parse(dr["t332_estado"].ToString());
                if (dr["t332_fipl"] != DBNull.Value)
                    o.t332_fipl = (DateTime)dr["t332_fipl"];
                if (dr["t332_ffpl"] != DBNull.Value)
                    o.t332_ffpl = (DateTime)dr["t332_ffpl"];
                if (dr["t332_etpl"] != DBNull.Value)
                    o.t332_etpl = double.Parse(dr["t332_etpl"].ToString());
                if (dr["t332_ffpr"] != DBNull.Value)
                    o.t332_ffpr = (DateTime)dr["t332_ffpr"];
                if (dr["t332_etpr"] != DBNull.Value)
                    o.t332_etpr = double.Parse(dr["t332_etpr"].ToString());
                if (dr["t332_cle"] != DBNull.Value)
                    o.t332_cle = double.Parse(dr["t332_cle"].ToString());
                if (dr["t332_orden"] != DBNull.Value)
                    o.t332_orden = short.Parse(dr["t332_orden"].ToString());
                if (dr["t332_facturable"] != DBNull.Value)
                    o.t332_facturable = (bool)dr["t332_facturable"];
                if (dr["t332_tipocle"] != DBNull.Value)
                    o.t332_tipocle = (string)dr["t332_tipocle"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
                if (dr["t305_cualidad"] != DBNull.Value)
                    o.t305_cualidad = (string)dr["t305_cualidad"];
                if (dr["num_proyecto"] != DBNull.Value)
                    o.num_proyecto = (int)dr["num_proyecto"];
                if (dr["nom_proyecto"] != DBNull.Value)
                    o.nom_proyecto = (string)dr["nom_proyecto"];
                if (dr["t331_despt"] != DBNull.Value)
                    o.t331_despt = (string)dr["t331_despt"];
                if (dr["t334_desfase"] != DBNull.Value)
                    o.t334_desfase = (string)dr["t334_desfase"];
                if (dr["t335_desactividad"] != DBNull.Value)
                    o.t335_desactividad = (string)dr["t335_desactividad"];
                if (dr["t332_presupuesto"] != DBNull.Value)
                    o.t332_presupuesto = decimal.Parse(dr["t332_presupuesto"].ToString());
                if (dr["t332_incidencia"] != DBNull.Value)
                    o.t332_incidencia = (string)dr["t332_incidencia"];
                if (dr["t332_avance"] != DBNull.Value)
                    o.t332_avance = double.Parse(dr["t332_avance"].ToString());
                if (dr["t332_avanceauto"] != DBNull.Value)
                    o.t332_avanceauto = (bool)dr["t332_avanceauto"];

                if (dr["cod_cliente"] != DBNull.Value)
                    o.cod_cliente = (int)dr["cod_cliente"];
                if (dr["nom_cliente"] != DBNull.Value)
                    o.nom_cliente = (string)dr["nom_cliente"];

                if (dr["t353_idorigen"] != DBNull.Value)
                    o.t353_idorigen = short.Parse(dr["t353_idorigen"].ToString());
                //if (dr["t332_finalizada"] != DBNull.Value)
                //    o.t332_finalizada = (bool)dr["t332_finalizada"];
                if (dr["t332_impiap"] != DBNull.Value)
                    o.t332_impiap = (bool)dr["t332_impiap"];
                //Herencia de recursos del CR
                if (dr["t332_heredanodo"] != DBNull.Value)
                    o.t332_heredanodo = (bool)dr["t332_heredanodo"];
                //Lo puede estar heredando bien del PT bien de la Fase o bien de la Actividad
                o.heredaCR = false;
                if (dr["t331_heredanodo"] != DBNull.Value)
                    o.heredaCR = (bool)dr["t331_heredanodo"];
                if (!o.heredaCR)
                {
                    if (dr["t334_heredanodo"] != DBNull.Value)
                        o.heredaCR = (bool)dr["t334_heredanodo"];
                }
                if (!o.heredaCR)
                {
                    if (dr["t335_heredanodo"] != DBNull.Value)
                        o.heredaCR = (bool)dr["t335_heredanodo"];
                }
                if (dr["t305_admiterecursospst"] != DBNull.Value)
                    o.t305_admiterecursospst = (bool)dr["t305_admiterecursospst"];
                if (dr["t305_avisorecursopst"] != DBNull.Value)
                    o.t305_avisorecursopst = (bool)dr["t305_avisorecursopst"];

                //Herencia de recursos del Proyecto económico
                if (dr["t332_heredaproyeco"] != DBNull.Value)
                    o.t332_heredaproyeco = (bool)dr["t332_heredaproyeco"];
                //Lo puede estar heredando bien del PT bien de la Fase o bien de la Actividad
                o.heredaPE = false;
                if (dr["t331_heredaproyeco"] != DBNull.Value)
                    o.heredaPE = (bool)dr["t331_heredaproyeco"];
                if (!o.heredaPE)
                {
                    if (dr["t334_heredaproyeco"] != DBNull.Value)
                        o.heredaPE = (bool)dr["t334_heredaproyeco"];
                }
                if (!o.heredaPE)
                {
                    if (dr["t335_heredaproyeco"] != DBNull.Value)
                        o.heredaPE = (bool)dr["t335_heredaproyeco"];
                }
                if (dr["t332_mensaje"] != DBNull.Value)
                    o.t332_mensaje = (string)dr["t332_mensaje"];
                if (dr["t332_notif_prof"] != DBNull.Value)
                    o.t332_notif_prof = (bool)dr["t332_notif_prof"];
                if (dr["t301_estado"] != DBNull.Value)
                    o.t301_estado = (string)dr["t301_estado"];
                if (dr["t332_acceso_iap"] != DBNull.Value)
                    o.t332_acceso_bitacora_iap = (string)dr["t332_acceso_iap"];
                if (dr["t324_idmodofact"] != DBNull.Value)
                    o.t324_idmodofact = (int)dr["t324_idmodofact"];
                if (dr["t324_denominacion"] != DBNull.Value)
                    o.t324_denominacion = (string)dr["t324_denominacion"];
                if (dr["t301_esreplicable"] != DBNull.Value)
                    o.t301_esreplicable = (bool)dr["t301_esreplicable"];
                if (dr["t305_opd"] != DBNull.Value)
                    o.t305_opd = (bool)dr["t305_opd"];
                if (dr["t305_nivelpresupuesto"] != DBNull.Value)
                    o.t305_nivelpresupuesto = (string)dr["t305_nivelpresupuesto"];
                o.t332_horascomplementarias = false;
                if (dr["t332_horascomplementarias"] != DBNull.Value)
                    o.t332_horascomplementarias = (bool)dr["t332_horascomplementarias"];
            }
            else
            {
                //throw (new NullReferenceException("No se ha obtenido ningún dato de la tarea indicada"));
            }
            dr.Close();
            dr.Dispose();

            return o;
        }

        /// <summary>
        /// 
        /// Obtiene los datos relativos a IAP de una tarea determinada,
        /// correspondientes a la tabla t332_TAREAPSP, y devuelve una
        /// instancia u objeto del tipo TAREAPSP
        /// </summary>
        public static TAREAPSP ObtenerDatosIAP(SqlTransaction tr, int nIdTarea)
        {
            TAREAPSP o = new TAREAPSP();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAIAPS", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAIAPS", aParam);

            if (dr.Read())
            {
                if (dr["t332_idtarea"] != DBNull.Value)
                    o.t332_idtarea = (int)dr["t332_idtarea"];
                if (dr["dPrimerConsumo"] != DBNull.Value)
                    o.dPrimerConsumo = (DateTime)dr["dPrimerConsumo"];
                if (dr["dUltimoConsumo"] != DBNull.Value)
                    o.dUltimoConsumo = (DateTime)dr["dUltimoConsumo"];
                if (dr["dFinEstimado"] != DBNull.Value)
                    o.dFinEstimado = (DateTime)dr["dFinEstimado"];
                if (dr["nTotalEstimado"] != DBNull.Value)
                    o.nTotalEstimado = double.Parse(dr["nTotalEstimado"].ToString());
                if (dr["nConsumidoHoras"] != DBNull.Value)
                    o.nConsumidoHoras = double.Parse(dr["nConsumidoHoras"].ToString());
                if (dr["nPendienteEstimado"] != DBNull.Value)
                    o.nPendienteEstimado = double.Parse(dr["nPendienteEstimado"].ToString());
                if (dr["nConsumidoJornadas"] != DBNull.Value)
                    o.nConsumidoJornadas = double.Parse(dr["nConsumidoJornadas"].ToString());
                if (dr["nAvanceTeorico"] != DBNull.Value)
                    o.nAvanceTeorico = double.Parse(dr["nAvanceTeorico"].ToString());

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningún dato de la tarea indicada"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        /// <summary>
        /// 
        /// Obtiene los datos relativos a la pestaña de NOTAS de una tarea determinada,
        /// correspondientes a la tabla t332_TAREAPSP, y devuelve una
        /// instancia u objeto del tipo TAREAPSP
        /// </summary>
        public static TAREAPSP ObtenerNotas(SqlTransaction tr, int nIdTarea)
        {
            TAREAPSP o = new TAREAPSP();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_P3_NOTAS_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_P3_NOTAS_S", aParam);

            if (dr.Read())
            {
                o.t332_idtarea = nIdTarea;
                if (dr["t332_notasiap"] != DBNull.Value)
                    o.t332_notasiap = (bool)dr["t332_notasiap"];
                if (dr["t332_notas1"] != DBNull.Value)
                    o.t332_notas1 = (string)dr["t332_notas1"];
                if (dr["t332_notas2"] != DBNull.Value)
                    o.t332_notas2 = (string)dr["t332_notas2"];
                if (dr["t332_notas3"] != DBNull.Value)
                    o.t332_notas3 = (string)dr["t332_notas3"];
                if (dr["t332_notas4"] != DBNull.Value)
                    o.t332_notas4 = (string)dr["t332_notas4"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningún dato de la pestaña Notas de la tarea indicada"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
/*
        public static void Prueba()
        {
            DataView dvCurRec = new DataView();
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, 1);
            DataSet ds = SqlHelper.ExecuteDataset("ZZZ_MIK_BIT1", aParam);

            //dvCurRec = new DataView(ds.Tables["CursosRecibidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            dvCurRec = new DataView(ds.Tables[0]);
            CargarCursosCVCCE(dvCurRec);
            dvCurRec.Dispose();
            ds.Dispose();
        }
        private static string GetFechaCorta(string sFecha)
        {
            string sRes = sFecha;
            if (sFecha != "")
            {
                if (sFecha.Length > 10)
                {
                    sRes = sFecha.Substring(0, 10);
                }
            }
            return sRes;
        }
        private static void CargarCursosCVCCE(DataView dvCursosRec)
        {

            #region CursosRecibidos
                foreach (DataRowView oFila in dvCursosRec)
                {
                    ReemplazarDatosCursosRCVCCE(oFila);
                }
            #endregion
        }
        private static void ReemplazarDatosCursosRCVCCE(DataRowView oFila)
        {
            string sTexto = "";
            bool bPendiente = (bool)oFila["Pendiente"];

            if (oFila["TIPO"].ToString() != "")
                sTexto=oFila["TIPO"].ToString();
            if (oFila["T574_HORAS"].ToString() != "")
                sTexto=oFila["T574_HORAS"].ToString();

            if (oFila["FINICIO"].ToString() != "" || oFila["FFIN"].ToString() != "")
            {
                sTexto=oFila["FFIN"].ToString();
                sTexto=GetFechaCorta(oFila["FINICIO"].ToString());
                sTexto=GetFechaCorta(oFila["FINICIO"].ToString()) + " - " + GetFechaCorta(oFila["FFIN"].ToString());
            }

            else
            {
                if (oFila["PROVEEDOR"].ToString() != "")
                    sTexto=oFila["PROVEEDOR"].ToString();

                if (oFila["PROVINCIA"].ToString() != "")
                    sTexto=oFila["PROVINCIA"].ToString();
            }
           if (oFila["ENTORNO"].ToString() != "")
                sTexto=oFila["ENTORNO"].ToString();
            if (oFila["MODALIDAD"].ToString() != "")
                sTexto=oFila["MODALIDAD"].ToString();
            if (oFila["T574_CONTENIDO"].ToString() != "")
                sTexto=oFila["T574_CONTENIDO"].ToString();
        }
*/

        public static TAREAPSP ObtenerOTC(SqlTransaction tr, int nIdTarea)
        {
            TAREAPSP o = new TAREAPSP();
            if (nIdTarea == 0) return o;

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_P2_OTC_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_P2_OTC_S", aParam);

            if (dr.Read())
            {
                o.t332_idtarea = nIdTarea;
                o.t331_idpt = int.Parse(dr["t331_idpt"].ToString());
                o.bOTCerror = false;
                if (dr["t332_otl"] != DBNull.Value)
                    o.t332_otl = (string)dr["t332_otl"];

                if ((dr["A_t346_idpst"] != DBNull.Value) || (dr["F_t346_idpst"] != DBNull.Value) || (dr["PT_t346_idpst"] != DBNull.Value))
                    o.bOTCHeredada = true;
                else
                    o.bOTCHeredada = false;
                if (dr["t346_idpst"] == DBNull.Value)
                {//Si no tiene PST a nivel de TAREA, miro si tiene a nivel de Actividad, Fase o PT
                    //if (dr["A_t346_idpst"] != DBNull.Value)
                    //{//La PST es de la actividad

                    //    o.t346_idpst = (int)dr["A_t346_idpst"];
                    //    if (dr["A_t346_codpst"] != DBNull.Value)
                    //        o.t346_codpst = (string)dr["A_t346_codpst"];
                    //    if (dr["A_t346_despst"] != DBNull.Value)
                    //        o.t346_despst = (string)dr["A_t346_despst"];
                    //}
                    //else
                    //{
                    //    if (dr["F_t346_idpst"] != DBNull.Value)
                    //    {//La PST es de la fase
                    //        o.t346_idpst = (int)dr["F_t346_idpst"];
                    //        if (dr["F_t346_codpst"] != DBNull.Value)
                    //            o.t346_codpst = (string)dr["F_t346_codpst"];
                    //        if (dr["F_t346_despst"] != DBNull.Value)
                    //            o.t346_despst = (string)dr["F_t346_despst"];
                    //    }
                    //    else
                    //    {//La PST es del PT
                    //        if (dr["PT_t346_idpst"] != DBNull.Value)
                    //        {
                    //            o.t346_idpst = (int)dr["PT_t346_idpst"];
                    //            if (dr["PT_t346_codpst"] != DBNull.Value)
                    //                o.t346_codpst = (string)dr["PT_t346_codpst"];
                    //            if (dr["PT_t346_despst"] != DBNull.Value)
                    //                o.t346_despst = (string)dr["PT_t346_despst"];
                    //        }
                    //    }
                    //}
                }
                else
                {//La PST es de la tarea (suya o heredada)
                    o.t346_idpst = (int)dr["t346_idpst"];
                    if (dr["t346_codpst"] != DBNull.Value)
                        o.t346_codpst = (string)dr["t346_codpst"];
                    if (dr["t346_despst"] != DBNull.Value)
                        o.t346_despst = (string)dr["t346_despst"];
                }
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningún dato de la pestaña Avanzado de la tarea indicada"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        /// <summary>
        /// 
        /// Obtiene los datos relativos a la pestaña de CONTROL de una tarea determinada,
        /// correspondientes a la tabla t332_TAREAPSP, y devuelve una
        /// instancia u objeto del tipo TAREAPSP
        /// </summary>
        public static TAREAPSP ObtenerControl(SqlTransaction tr, int nIdTarea)
        {
            TAREAPSP o = new TAREAPSP();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_P4_CONTROL_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_P4_CONTROL_S", aParam);

            if (dr.Read())
            {
                o.t332_idtarea = nIdTarea;

                if (dr["t314_idusuario_promotor"] != DBNull.Value)
                    o.t314_idusuario_promotor = (int)dr["t314_idusuario_promotor"];
                if (dr["sPromotor"] != DBNull.Value)
                    o.sPromotor = (string)dr["sPromotor"];
                if (dr["t332_falta"] != DBNull.Value)
                    o.t332_falta = (DateTime)dr["t332_falta"];

                if (dr["t314_idusuario_ultmodif"] != DBNull.Value)
                    o.t314_idusuario_ultmodif = (int)dr["t314_idusuario_ultmodif"];
                if (dr["sModificador"] != DBNull.Value)
                    o.sModificador = (string)dr["sModificador"];
                if (dr["t332_fultmodif"] != DBNull.Value)
                    o.t332_fultmodif = (DateTime)dr["t332_fultmodif"];

                if (dr["t314_idusuario_fin"] != DBNull.Value)
                    o.t314_idusuario_fin = (int)dr["t314_idusuario_fin"];
                if (dr["sFinalizador"] != DBNull.Value)
                    o.sFinalizador = (string)dr["sFinalizador"];
                //o.t332_ffin = DateTime.Parse("01/01/1900");
                o.t332_ffin = null;
                if (dr["t332_ffin"] != DBNull.Value)
                    o.t332_ffin = (DateTime)dr["t332_ffin"];

                if (dr["sCerrador"] != DBNull.Value)
                    o.sCerrador = (string)dr["sCerrador"];
                if (dr["t314_idusuario_cierre"] != DBNull.Value)
                    o.t314_idusuario_cierre = (int)dr["t314_idusuario_cierre"];
                //o.t332_fcierre = DateTime.Parse("01/01/1900");
                o.t332_fcierre = null;
                if (dr["t332_fcierre"] != DBNull.Value)
                    o.t332_fcierre = (DateTime)dr["t332_fcierre"];

                if (dr["t332_observaciones"] != DBNull.Value)
                    o.t332_observaciones = (string)dr["t332_observaciones"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningún dato de la pestaña Control de la tarea indicada"));
            }
            dr.Close();
            dr.Dispose();
            return o;
        }

        //Para insertar la tarea desde la pantalla de desglose
        public static int Insertar(SqlTransaction tr, string sDesTarea, int nIdPT, Nullable<int> nIdActividad, int nOrden,
                                   string sFiniPl, string sFfinPl, double fDuracion, string sFiniV, string sFfinV,
                                   decimal fPresupuesto, byte iEstado, bool bFacturable, bool bAvanceAutomatico, string sObs)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@sDesTarea", SqlDbType.VarChar, 100);
            aParam[1] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nIdActividad", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nIdPromotor", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@dFalta", SqlDbType.SmallDateTime, 4);
            aParam[5] = new SqlParameter("@dFiniPl", SqlDbType.SmallDateTime, 4);
            aParam[6] = new SqlParameter("@dFfinPl", SqlDbType.SmallDateTime, 4);
            aParam[7] = new SqlParameter("@rEtpl", SqlDbType.Float, 8);
            aParam[8] = new SqlParameter("@rPresupuesto", SqlDbType.Real, 4);
            aParam[9] = new SqlParameter("@dFiniV", SqlDbType.SmallDateTime, 4);
            aParam[10] = new SqlParameter("@dFfinV", SqlDbType.SmallDateTime, 4);
            aParam[11] = new SqlParameter("@nOrden", SqlDbType.SmallInt, 2);
            aParam[12] = new SqlParameter("@t332_estado", SqlDbType.TinyInt, 1);
            aParam[13] = new SqlParameter("@t332_facturable", SqlDbType.Bit, 1);
            aParam[14] = new SqlParameter("@t332_avanceauto", SqlDbType.Bit, 1);
            aParam[15] = new SqlParameter("@t332_destarealong", SqlDbType.Text, 2147483647);

            aParam[0].Value = sDesTarea;
            aParam[1].Value = nIdPT;
            if (nIdActividad == -1) aParam[2].Value = null;
            else aParam[2].Value = nIdActividad;
            //aParam[2].Value = nIdActividad;
            aParam[3].Value = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            aParam[4].Value = DateTime.Now;
            if (sFiniPl == "") aParam[5].Value = null;
            else aParam[5].Value = DateTime.Parse(sFiniPl);
            if (sFfinPl == "") aParam[6].Value = null;
            else aParam[6].Value = DateTime.Parse(sFfinPl);
            aParam[7].Value = fDuracion;
            aParam[8].Value = fPresupuesto;
            if (sFiniV == "") aParam[9].Value = null;
            else aParam[9].Value = DateTime.Parse(sFiniV);
            if (sFfinV == "") aParam[10].Value = null;
            else aParam[10].Value = DateTime.Parse(sFfinV);
            aParam[11].Value = nOrden;
            aParam[12].Value = iEstado;
            aParam[13].Value = bFacturable;
            aParam[14].Value = bAvanceAutomatico;
            aParam[15].Value = sObs;

            return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREAPSPI_ESTR", aParam));
        }
        //Despues de insertar la tarea le asignamos los recursos derivables del Proyecto economico
        //public static void HeredarRecursos(SqlTransaction tr, short nIdCR, int nIdPE, int nIdTarea, string sFfp)
        //{
        //    SqlParameter[] aParam = new SqlParameter[4];
        //    aParam[0] = new SqlParameter("@nIdCR", SqlDbType.SmallInt, 2);
        //    aParam[1] = new SqlParameter("@nIdPE", SqlDbType.Int, 4);
        //    aParam[2] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
        //    aParam[3] = new SqlParameter("@dtFfp", SqlDbType.SmallDateTime, 4);

        //    aParam[0].Value = nIdCR;
        //    aParam[1].Value = nIdPE;
        //    aParam[2].Value = nIdTarea;
        //    if (sFfp == "") aParam[3].Value = null;
        //    else aParam[3].Value = DateTime.Parse(sFfp);
        //    //el 04/12/2006 comenta Andoni que la herencia se realizará por trigger por lo que quitamos el código
        //    //SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREARECURSO_HEREDAR", aParam);
        //}
        //Para modificar la tarea desde la pantalla de desglose
        public static int Modificar(SqlTransaction tr, int nIdTarea, string sDesTarea, int nIdPT, Nullable<int> nIdActividad, int nOrden,
                                    string sFiniPl, string sFfinPl, double fDuracion, string sFiniV, string sFfinV, int nIdUltmodif,
                                    decimal fPresupuesto, Nullable<byte> iEstado, bool bFacturable)
        {
            SqlParameter[] aParam = new SqlParameter[14];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesTarea", SqlDbType.VarChar, 100);
            aParam[2] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nIdActividad", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@dFiniPl", SqlDbType.SmallDateTime, 4);
            aParam[5] = new SqlParameter("@dFfinPl", SqlDbType.SmallDateTime, 4);
            aParam[6] = new SqlParameter("@rEtpl", SqlDbType.Float, 8);
            aParam[7] = new SqlParameter("@rPresupuesto", SqlDbType.Money, 8);
            aParam[8] = new SqlParameter("@dFiniV", SqlDbType.SmallDateTime, 4);
            aParam[9] = new SqlParameter("@dFfinV", SqlDbType.SmallDateTime, 4);
            aParam[10] = new SqlParameter("@nOrden", SqlDbType.SmallInt, 2);
            aParam[11] = new SqlParameter("@nIdultmodif", SqlDbType.Int, 4);
            aParam[12] = new SqlParameter("@t332_estado", SqlDbType.TinyInt, 1);
            aParam[13] = new SqlParameter("@t332_facturable", SqlDbType.Bit, 1);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = sDesTarea;
            aParam[2].Value = nIdPT;
            //if (nIdActividad == -1) aParam[3].Value = null;
            //else aParam[3].Value = nIdActividad;
            aParam[3].Value = nIdActividad;
            if (sFiniPl == "") aParam[4].Value = null;
            else aParam[4].Value = DateTime.Parse(sFiniPl);
            if (sFfinPl == "") aParam[5].Value = null;
            else aParam[5].Value = DateTime.Parse(sFfinPl);
            aParam[6].Value = fDuracion;
            aParam[7].Value = fPresupuesto;
            if (sFiniV == "") aParam[8].Value = null;
            else aParam[8].Value = DateTime.Parse(sFiniV);
            if (sFfinV == "") aParam[9].Value = null;
            else aParam[9].Value = DateTime.Parse(sFfinV);
            aParam[10].Value = nOrden;
            aParam[11].Value = nIdUltmodif;
            aParam[12].Value = iEstado;
            aParam[13].Value = bFacturable;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPU_ESTR", aParam);
        }
        //Para modificar la tarea desde la pantalla de reestructuración de tarea
        public static int ModificarPadre(SqlTransaction tr, int nIdTarea, int nIdPT, Nullable<int> nIdActividad, int nOrden, int nIdUltmodif)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nIdActividad", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.SmallInt, 2);
            aParam[4] = new SqlParameter("@nIdultmodif", SqlDbType.Int, 4);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdPT;
            //if (nIdActividad == -1) aParam[3].Value = null;
            //else aParam[3].Value = nIdActividad;
            aParam[2].Value = nIdActividad;
            aParam[3].Value = nOrden;
            aParam[4].Value = nIdUltmodif;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSP_U_PADRE", aParam);
        }
        /// <summary>
        /// 
        /// Modifica los datos de seguimiento de la Tarea,
        /// correspondientes a la tabla t332_TAREAPSP,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int ModificarDatosSeguimiento(SqlTransaction tr,
                                int nIdTarea,
                                Nullable<double> nEtpr,
                                Nullable<DateTime> dFfpr,
                                int nIdUltmodif,
                                Nullable<double> nAvance,
                                string sDenominacion,
                                byte nEstado,
                                Nullable<double> nEtpl,
                                Nullable<DateTime> dFechaIniPl,
                                Nullable<DateTime> dFechaFinPl,
                                decimal nImporte
            )
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nEtpr", SqlDbType.Float, 8);
            aParam[2] = new SqlParameter("@dFfpr", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@nIdultmodif", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@nAvance", SqlDbType.Float, 8);
            aParam[5] = new SqlParameter("@sDenominacion", SqlDbType.VarChar, 100);
            aParam[6] = new SqlParameter("@nEstado", SqlDbType.TinyInt, 1);
            aParam[7] = new SqlParameter("@nEtpl", SqlDbType.Float, 8);
            aParam[8] = new SqlParameter("@dFipl", SqlDbType.SmallDateTime, 4);
            aParam[9] = new SqlParameter("@dFfpl", SqlDbType.SmallDateTime, 4);
            aParam[10] = new SqlParameter("@nImporte", SqlDbType.Money, 8);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = nEtpr;
            aParam[2].Value = dFfpr;
            aParam[3].Value = nIdUltmodif;
            aParam[4].Value = nAvance;
            aParam[5].Value = sDenominacion;
            aParam[6].Value = nEstado;
            aParam[7].Value = nEtpl;
            aParam[8].Value = dFechaIniPl;
            aParam[9].Value = dFechaFinPl;
            aParam[10].Value = nImporte;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPU_SEG", aParam);
        }
        /// <summary>
        /// 
        /// Comprueba si una tarea tiene consumos para poder borrarla o no.
        /// </summary>
        public static bool bTieneConsumo(SqlTransaction tr, int nIdTarea)
        {
            bool bConsumo = false;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            int returnValue;
            if (tr == null)
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EXISTECONSUMO", aParam));
            else
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTECONSUMO", aParam));

            if (returnValue > 0)
                bConsumo = true;

            return bConsumo;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Borra registros de la tabla T344_PERFILPSTUSUARIOMC a traves del código de tarea
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	17/05/2010
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int BorrarPerfil(SqlTransaction tr, int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PERFILPSTUSUARIOMC_DByTarea", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PERFILPSTUSUARIOMC_DByTarea", aParam);
        }

        /// <summary>
        /// 
        /// Calcula el orden que le corresponde como último elemento dentro de un proyecto técnico
        /// Se usa cuando se crea tarea desde la pantalla de detalle y en la reestructuración de tareas
        /// </summary>
        public static short flCalcularOrden2(SqlTransaction tr, int nIdPT)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;

            if (tr == null)
                return System.Convert.ToInt16(SqlHelper.ExecuteScalar("SUP_ORDEN_TAREA2", aParam));
            else
                return System.Convert.ToInt16(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ORDEN_TAREA2", aParam));
        }
        /// <summary>
        /// 
        /// Comprueba si una tarea es de avance automatico,
        /// </summary>
        public static bool bAvanceAutomatico(SqlTransaction tr, int nIdTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            if (tr == null)
                return (bool)SqlHelper.ExecuteScalar("SUP_TAREA_AVANCE", aParam);
            else
                return (bool)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREA_AVANCE", aParam);
        }
        /// <summary>
        /// 
        /// Devuelve el tipo de control y su magnitud del límite de esfuerzo de una tarea
        /// </summary>
        public static SqlDataReader flContolLimiteEsfuerzos(SqlTransaction tr, int nIdTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_CLE", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_CLE", aParam);
        }
        /// <summary>
        /// 
        /// Devuelve los campos asociados a la tarea indicada
        /// </summary>
        public static SqlDataReader obtenerCamposValor(SqlTransaction tr, int nIdTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CAMPOS_TAREA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMPOS_TAREA_CAT", aParam);
        }
        /// <summary>
        /// 
        /// Comprueba si una tarea tiene cuelga de una actividad y/o fase y en su caso si es la única tarea,
        /// para poder cambarle de padres o no.
        /// </summary>
        public static bool bHijoUnico(SqlTransaction tr, int nFase, int nAct)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nFase", SqlDbType.Int, 4);
            if (nFase > 0) aParam[0].Value = nFase;
            else aParam[0].Value = null;
            aParam[1] = new SqlParameter("@nAct", SqlDbType.Int, 4);
            if (nAct > 0) aParam[1].Value = nAct;
            else aParam[1].Value = null;

            int returnValue;
            if (tr == null)
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_HIJOUNICO_TAREA", aParam));
            else
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HIJOUNICO_TAREA", aParam));

            if (returnValue == 1)
                bRes = true;

            return bRes;
        }
        /// <summary>
        /// Comprueba si una tarea es modificable en función del perfil del recurso que la está accediendo
        /// Devuelve el modo de acceso a una Tarea: N-> sin acceso, R->lectura, W->escritura
        /// </summary>
        public static string getAcceso(SqlTransaction tr, int nIdTarea, int iUser)
        {
            string sRes = "N";
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = iUser;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                sRes = "W";
            else
            {
                object obj;
                if (tr == null)
                    obj = SqlHelper.ExecuteScalar("SUP_PERMISO_TAREA", aParam);
                else
                    obj = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PERMISO_TAREA", aParam);
                if (obj != null)
                {
                    if ((bool)obj) sRes = "R";
                    else sRes = "W";
                }
            }
            return sRes;
        }
        /// <summary>
        /// Comprueba si una tarea tiene valores en todos los atributos estadisticos obligatorios del CR
        /// </summary>
        public static bool bFaltanValoresAE(SqlTransaction tr, short nCodCR, Nullable<int> nIdTarea)
        {
            //Si viene código de tarea miramos si tiene valores en los atributos estadisticos obligatorios del CR
            //Si viene a nulo solo miramos si el CR tiene atributos estadísticos obligatorios
            bool bFaltanValores = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
            aParam[0].Value = nCodCR;

            int returnValue;
            if (nIdTarea == null)
            {
                //aParam[1] = new SqlParameter("@sAmbito", SqlDbType.Text, 1);
                //aParam[1].Value = "T";
                aParam[1] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
                aParam[1].Value = nIdTarea;
                if (tr == null)
                    returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AE_OBLIGATORIO", aParam));
                else
                    returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AE_OBLIGATORIO", aParam));

                if (returnValue > 0)
                    bFaltanValores = true;
            }
            else
            {
                aParam[1] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
                aParam[1].Value = nIdTarea;
                if (tr == null)
                    returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AE_TAREA_OBLIGATORIO", aParam));
                else
                    returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AE_TAREA_OBLIGATORIO", aParam));

                if (returnValue > 0)
                    bFaltanValores = true;
            }
            return bFaltanValores;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta en la tabla T351_AETAREAPSP sacando los valores de T371_AEITEMSPLANTILLA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [doarhumi]	26/04/2007 14:56:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertarAE(SqlTransaction tr, int idElemento, int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@idElemento", SqlDbType.Int, 4);
            aParam[1].Value = idElemento;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_AE_TAREA_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AE_TAREA_I", aParam);
        }

        /// <summary>
        /// 
        /// Modifica las fecha de vigencia de la Tarea,
        /// correspondientes a la tabla t332_TAREAPSP,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int ModificarFechasVigencia(SqlTransaction tr,
                                int nIdTarea,
                                Nullable<DateTime> dFVIni,
                                Nullable<DateTime> dFVFin
                                )
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@dFiniV", SqlDbType.SmallDateTime, 4);
            aParam[2] = new SqlParameter("@dFfinV", SqlDbType.SmallDateTime, 4);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = dFVIni;
            aParam[2].Value = dFVFin;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPU_VIG", aParam);
        }
        /// <summary>
        /// 
        /// Modifica el estado de la Tarea, correspondientes a la tabla t332_TAREAPSP,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int ModificarEstado(SqlTransaction tr, int nIdTarea, byte iEstado, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nEstado", SqlDbType.TinyInt, 1);
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = iEstado;
            aParam[2].Value = t314_idusuario;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPU_EST", aParam);
        }
        /// <summary>
        /// 
        /// Modifica el mensaje genérico a todos los recursos asociados a la Tarea
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int ModificarMensaje(SqlTransaction tr, int nIdTarea, string sMensaje)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t332_mensaje", SqlDbType.Text, 2147483647);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = sMensaje;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPU_MENS", aParam);
        }
        /// <summary>
        /// 
        /// Obtiene la lista de hitos que no estén en estado F (INACTIVO) en los que está englobada la tarea
        /// </summary>
        public static SqlDataReader CatalogoHitos(int nIdTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int);
            aParam[0].Value = nIdTarea;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_HITOS", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla tareas.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(Nullable<int> cod_tarea, string nom_tarea, Nullable<int> cod_pt, Nullable<int> cod_fase, Nullable<int> cod_actividad, byte nOrden, byte nAscDesc, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@cod_tarea", SqlDbType.Int, 4);
            aParam[0].Value = cod_tarea;
            aParam[1] = new SqlParameter("@nom_tarea", SqlDbType.Text, 50);
            aParam[1].Value = nom_tarea;
            aParam[2] = new SqlParameter("@cod_pt", SqlDbType.Int, 4);
            aParam[2].Value = cod_pt;
            aParam[3] = new SqlParameter("@cod_fase", SqlDbType.Int, 4);
            aParam[3].Value = cod_fase;
            aParam[4] = new SqlParameter("@cod_actividad", SqlDbType.Int, 4);
            aParam[4].Value = cod_actividad;

            aParam[5] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[5].Value = nOrden;
            aParam[6] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[6].Value = nAscDesc;
            aParam[7] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[7].Value = sTipoBusqueda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas de un CR y proyecto económico
        /// </summary>
        /// -----------------------------------------------------------------------------
        //public static SqlDataReader Catalogo3(string nom_tarea, int cod_pe, byte nOrden, byte nAscDesc, string sTipoBusqueda, int iUser, string sPerfil)
        public static SqlDataReader Catalogo3(string nom_tarea, int t305_id, byte nOrden, byte nAscDesc, string sTipoBusqueda, int iUser)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@nom_tarea", SqlDbType.Text, 50);
            aParam[0].Value = nom_tarea;
            aParam[1] = new SqlParameter("@t305_id", SqlDbType.Int, 4);
            aParam[1].Value = t305_id;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = iUser;
            aParam[3] = new SqlParameter("@sAdmin", SqlDbType.Char, 2);
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                aParam[3].Value = "A";
            else
                aParam[3].Value = "";

            aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[4].Value = nOrden;
            aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[5].Value = nAscDesc;
            aParam[6] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[6].Value = sTipoBusqueda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas  
        /// </summary>
        /// -----------------------------------------------------------------------------
        //public static SqlDataReader Catalogo4(string nom_tarea, int t303_idnodo, byte nOrden, byte nAscDesc, string sTipoBusqueda, int iUser, string sPerfil)
        public static SqlDataReader Catalogo4(string nom_tarea, byte nOrden, byte nAscDesc, string sTipoBusqueda, int iUser)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@nom_tarea", SqlDbType.Text, 50);
            aParam[0].Value = nom_tarea;
            //aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            //aParam[1].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = iUser;
            aParam[2] = new SqlParameter("@sAdmin", SqlDbType.Char, 2);
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                aParam[2].Value = "A";
            else
                aParam[2].Value = "";
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[3].Value = nOrden;
            aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[4].Value = nAscDesc;
            aParam[5] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[5].Value = sTipoBusqueda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C3", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los consumos del informe "Consumos por Tarea"
        /// </summary>
        /// -----------------------------------------------------------------------------
        /// Nullable<int> nNodo,
        public static SqlDataReader ConsumosTareaMensual_T2(int num_empleado, Nullable<int> nNodo, Nullable<int> nPE,
                                                           Nullable<int> nPT, Nullable<int> nFase, Nullable<int> nActividad,
                                                           Nullable<int> nTarea, string sDesTarea, Nullable<int> nCliente,
                                                           DateTime dDesde, DateTime dHasta, string sVAE, string sEstado, string sAE,
                                                           Nullable<int> nPST, bool bCamposLibres)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = nNodo;
            aParam[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[2].Value = nPE;
            aParam[3] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[3].Value = nPT;
            aParam[4] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[4].Value = nTarea;
            aParam[5] = new SqlParameter("@sTarea", SqlDbType.VarChar, 50);
            aParam[5].Value = sDesTarea;
            aParam[6] = new SqlParameter("@nCliente", SqlDbType.Int, 4);
            aParam[6].Value = nCliente;
            aParam[7] = new SqlParameter("@FechaDesde", SqlDbType.DateTime, 8);
            aParam[7].Value = dDesde;
            aParam[8] = new SqlParameter("@FechaHasta", SqlDbType.DateTime, 8);
            aParam[8].Value = dHasta;
            aParam[9] = new SqlParameter("@sVAE", SqlDbType.VarChar, 2000);
            aParam[9].Value = sVAE;
            aParam[10] = new SqlParameter("@nFase", SqlDbType.Int, 4);
            aParam[10].Value = nFase;
            aParam[11] = new SqlParameter("@nActividad", SqlDbType.Int, 4);
            aParam[11].Value = nActividad;
            aParam[12] = new SqlParameter("@sEstado", SqlDbType.VarChar, 10);
            aParam[12].Value = sEstado;
            aParam[13] = new SqlParameter("@sAE", SqlDbType.VarChar, 2000);
            aParam[13].Value = sAE;
            aParam[14] = new SqlParameter("@nPST", SqlDbType.Int, 4);
            aParam[14].Value = nPST;
            aParam[15] = new SqlParameter("@camposlibres", SqlDbType.Bit, 1);
            aParam[15].Value = bCamposLibres;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_T2_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_T2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los consumos de la consultas "Consumos por Tarea"
        /// </summary>
        /// -----------------------------------------------------------------------------
        /// Nullable<int> nNodo, 
        public static SqlDataReader ConsumosTareaMensual_T(int num_empleado, Nullable<int> nNodo, Nullable<int> nPE,
                                                           Nullable<int> nPT, Nullable<int> nFase, Nullable<int> nActividad,
                                                           Nullable<int> nTarea, string sDesTarea, Nullable<int> nCliente,
                                                           DateTime dDesde, DateTime dHasta, string sVAE, string sEstado, string sAE,
                                                           Nullable<int> nPST, bool bCamposLibres)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = nNodo;
            aParam[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[2].Value = nPE;
            aParam[3] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[3].Value = nPT;
            aParam[4] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[4].Value = nTarea;
            aParam[5] = new SqlParameter("@sTarea", SqlDbType.VarChar, 50);
            aParam[5].Value = sDesTarea;
            aParam[6] = new SqlParameter("@nCliente", SqlDbType.Int, 4);
            aParam[6].Value = nCliente;
            aParam[7] = new SqlParameter("@FechaDesde", SqlDbType.DateTime, 8);
            aParam[7].Value = dDesde;
            aParam[8] = new SqlParameter("@FechaHasta", SqlDbType.DateTime, 8);
            aParam[8].Value = dHasta;
            aParam[9] = new SqlParameter("@sVAE", SqlDbType.VarChar, 2000);
            aParam[9].Value = sVAE;
            aParam[10] = new SqlParameter("@nFase", SqlDbType.Int, 4);
            aParam[10].Value = nFase;
            aParam[11] = new SqlParameter("@nActividad", SqlDbType.Int, 4);
            aParam[11].Value = nActividad;
            aParam[12] = new SqlParameter("@sEstado", SqlDbType.VarChar, 10);
            aParam[12].Value = sEstado;
            aParam[13] = new SqlParameter("@sAE", SqlDbType.VarChar, 2000);
            aParam[13].Value = sAE;
            aParam[14] = new SqlParameter("@nPST", SqlDbType.Int, 4);
            aParam[14].Value = nPST;
            aParam[15] = new SqlParameter("@camposlibres", SqlDbType.Bit, 1);
            aParam[15].Value = bCamposLibres;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_T_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_T", aParam);
        }
        public static SqlDataReader ConsumosTareaMensual_P(int num_empleado, Nullable<int> t303_idnodo, int nTarea, DateTime dDesde, DateTime dHasta, string sCodigo)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[2].Value = nTarea;
            aParam[3] = new SqlParameter("@FechaDesde", SqlDbType.DateTime, 8);
            aParam[3].Value = dDesde;
            aParam[4] = new SqlParameter("@FechaHasta", SqlDbType.DateTime, 8);
            aParam[4].Value = dHasta;
            aParam[5] = new SqlParameter("@sCodigo", SqlDbType.Char, 1);
            aParam[5].Value = sCodigo;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_P_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_P", aParam);
        }
        public static SqlDataReader ConsumosTareaMensual_C(int nTarea, int num_empleado, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[0].Value = nTarea;
            aParam[1] = new SqlParameter("@nTecnico", SqlDbType.Int, 4);
            aParam[1].Value = num_empleado;
            aParam[2] = new SqlParameter("@FechaDesde", SqlDbType.DateTime, 8);
            aParam[2].Value = dDesde;
            aParam[3] = new SqlParameter("@FechaHasta", SqlDbType.DateTime, 8);
            aParam[3].Value = dHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_C", aParam);
        }

        public static SqlDataReader ConsumosTareaMensual_Tot(string sNivel, int num_empleado, Nullable<int> nNodo, Nullable<int> nPE,
                                                           Nullable<int> nPT, Nullable<int> nFase, Nullable<int> nActividad,
                                                           Nullable<int> nTarea, string sDesTarea, Nullable<int> nCliente,
                                                           DateTime dDesde, DateTime dHasta, string sVAE, string sEstado, string sAE,
                                                           Nullable<int> nPST, bool bCamposLibres)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = nNodo;
            aParam[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[2].Value = nPE;
            aParam[3] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[3].Value = nPT;
            aParam[4] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[4].Value = nTarea;
            aParam[5] = new SqlParameter("@sTarea", SqlDbType.VarChar, 50);
            aParam[5].Value = sDesTarea;
            aParam[6] = new SqlParameter("@nCliente", SqlDbType.Int, 4);
            aParam[6].Value = nCliente;
            aParam[7] = new SqlParameter("@FechaDesde", SqlDbType.DateTime, 8);
            aParam[7].Value = dDesde;
            aParam[8] = new SqlParameter("@FechaHasta", SqlDbType.DateTime, 8);
            aParam[8].Value = dHasta;
            aParam[9] = new SqlParameter("@sVAE", SqlDbType.VarChar, 2000);
            aParam[9].Value = sVAE;
            aParam[10] = new SqlParameter("@nFase", SqlDbType.Int, 4);
            aParam[10].Value = nFase;
            aParam[11] = new SqlParameter("@nActividad", SqlDbType.Int, 4);
            aParam[11].Value = nActividad;
            aParam[12] = new SqlParameter("@sEstado", SqlDbType.VarChar, 10);
            aParam[12].Value = sEstado;
            aParam[13] = new SqlParameter("@sAE", SqlDbType.VarChar, 2000);
            aParam[13].Value = sAE;
            aParam[14] = new SqlParameter("@nPST", SqlDbType.Int, 4);
            aParam[14].Value = nPST;
            aParam[15] = new SqlParameter("@camposlibres", SqlDbType.Bit, 1);
            aParam[15].Value = bCamposLibres;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_N" + sNivel + "_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSTAREA_N" + sNivel, aParam);
        }

        /// <summary>
        /// 
        /// Modifica las fecha de vigencia de la Tarea,
        /// correspondientes a la tabla t332_TAREAPSP,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int ModificarDatosGantt(SqlTransaction tr,
                                int nIdTarea,
                                Nullable<DateTime> dFiniPL,
                                Nullable<DateTime> dFfinPL,
                                Nullable<double> nETPR,
                                Nullable<DateTime> dFfinPR
                                )
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@dFiniPL", SqlDbType.SmallDateTime, 4);
            aParam[2] = new SqlParameter("@dFfinPL", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@nETPR", SqlDbType.Float, 4);
            aParam[4] = new SqlParameter("@dFfinPR", SqlDbType.SmallDateTime, 4);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = dFiniPL;
            aParam[2].Value = dFfinPL;
            aParam[3].Value = nETPR;
            aParam[4].Value = dFfinPR;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPU_GANTT", aParam);
        }

        //Métodos para grabación por pestañas
        //Mikel 14/02/2012 Las fechas y usuarios de de finalización y cierre las haremos por trigger
        public static int Update_P0(SqlTransaction tr, int t332_idtarea, string t332_destarea, string t332_destarealong, int t331_idpt,
                                Nullable<int> t335_idactividad, Nullable<int> t314_idusuario_ultmodif, Nullable<DateTime> t332_fultmodif,
                                Nullable<DateTime> t332_fiv, Nullable<DateTime> t332_ffv, byte t332_estado,
                                Nullable<DateTime> t332_fipl, Nullable<DateTime> t332_ffpl, Nullable<double> t332_etpl,
                                Nullable<DateTime> t332_ffpr, Nullable<double> t332_etpr, Nullable<double> t332_cle,
                                string t332_tipocle, short t332_orden, bool t332_facturable, decimal t332_presupuesto,
                                bool t332_notificable, Nullable<double> t332_avance, bool t332_avanceauto,
            //Nullable<int> t314_idusuario_fin, Nullable<DateTime> t332_ffin,
            //Nullable<int> t314_idusuario_cierre, Nullable<DateTime> t332_fcierre,
                                bool t332_impiap, bool t332_heredanodo, bool t332_heredaproyeco, bool t332_notif_prof,
                                string t332_acceso_bitacora_iap, Nullable<int> t324_idmodofact, bool bHorasComplementarias)
        {
            SqlParameter[] aParam = new SqlParameter[30];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t332_destarea", SqlDbType.Text, 100);
            aParam[1].Value = t332_destarea;
            aParam[2] = new SqlParameter("@t332_destarealong", SqlDbType.Text, 2147483647);
            aParam[2].Value = t332_destarealong;
            aParam[3] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[3].Value = t331_idpt;
            aParam[4] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            //if (t335_idactividad <= 0) t335_idactividad = null;
            aParam[4].Value = t335_idactividad;
            aParam[5] = new SqlParameter("@t314_idusuario_ultmodif", SqlDbType.Int, 4);
            aParam[5].Value = t314_idusuario_ultmodif;
            aParam[6] = new SqlParameter("@t332_fultmodif", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t332_fultmodif;
            aParam[7] = new SqlParameter("@t332_fiv", SqlDbType.SmallDateTime, 4);
            aParam[7].Value = t332_fiv;
            aParam[8] = new SqlParameter("@t332_ffv", SqlDbType.SmallDateTime, 4);
            aParam[8].Value = t332_ffv;
            aParam[9] = new SqlParameter("@t332_estado", SqlDbType.TinyInt, 1);
            aParam[9].Value = t332_estado;
            aParam[10] = new SqlParameter("@t332_fipl", SqlDbType.SmallDateTime, 4);
            aParam[10].Value = t332_fipl;
            aParam[11] = new SqlParameter("@t332_ffpl", SqlDbType.SmallDateTime, 4);
            aParam[11].Value = t332_ffpl;
            aParam[12] = new SqlParameter("@t332_etpl", SqlDbType.Float, 8);
            aParam[12].Value = t332_etpl;
            aParam[13] = new SqlParameter("@t332_ffpr", SqlDbType.SmallDateTime, 4);
            aParam[13].Value = t332_ffpr;
            aParam[14] = new SqlParameter("@t332_etpr", SqlDbType.Float, 8);
            aParam[14].Value = t332_etpr;
            aParam[15] = new SqlParameter("@t332_cle", SqlDbType.Float, 8);
            aParam[15].Value = t332_cle;
            aParam[16] = new SqlParameter("@t332_tipocle", SqlDbType.Text, 1);
            aParam[16].Value = t332_tipocle;
            aParam[17] = new SqlParameter("@t332_orden", SqlDbType.SmallInt, 2);
            aParam[17].Value = t332_orden;
            aParam[18] = new SqlParameter("@t332_facturable", SqlDbType.Bit, 1);
            aParam[18].Value = t332_facturable;
            aParam[19] = new SqlParameter("@t332_presupuesto", SqlDbType.Money, 8);
            aParam[19].Value = t332_presupuesto;
            aParam[20] = new SqlParameter("@t332_notificable", SqlDbType.Bit, 1);
            aParam[20].Value = t332_notificable;
            aParam[21] = new SqlParameter("@t332_avance", SqlDbType.Float, 8);
            aParam[21].Value = t332_avance;
            aParam[22] = new SqlParameter("@t332_avanceauto", SqlDbType.Bit, 1);
            aParam[22].Value = t332_avanceauto;
            //aParam[23] = new SqlParameter("@t314_idusuario_fin", SqlDbType.Int, 4);
            //aParam[23].Value = t314_idusuario_fin;
            //aParam[24] = new SqlParameter("@t332_ffin", SqlDbType.SmallDateTime, 4);
            //aParam[24].Value = t332_ffin;
            //aParam[25] = new SqlParameter("@t314_idusuario_cierre", SqlDbType.Int, 4);
            //aParam[25].Value = t314_idusuario_cierre;
            //aParam[26] = new SqlParameter("@t332_fcierre", SqlDbType.SmallDateTime, 4);
            //aParam[26].Value = t332_fcierre;
            aParam[23] = new SqlParameter("@t332_impiap", SqlDbType.Bit, 1);
            aParam[23].Value = t332_impiap;
            aParam[24] = new SqlParameter("@t332_heredanodo", SqlDbType.Bit, 1);
            aParam[24].Value = t332_heredanodo;
            aParam[25] = new SqlParameter("@t332_heredaproyeco", SqlDbType.Bit, 1);
            aParam[25].Value = t332_heredaproyeco;
            aParam[26] = new SqlParameter("@t332_notif_prof", SqlDbType.Bit, 1);
            aParam[26].Value = t332_notif_prof;
            aParam[27] = new SqlParameter("@t332_acceso_iap", SqlDbType.Char, 1);
            aParam[27].Value = t332_acceso_bitacora_iap;
            aParam[28] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
            aParam[28].Value = t324_idmodofact;
            aParam[29] = new SqlParameter("@t332_horascomplementarias", SqlDbType.Bit, 1);
            aParam[29].Value = bHorasComplementarias;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREA_P0_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREA_P0_U", aParam);
        }
        public static int Update_P2(SqlTransaction tr, int t332_idtarea, Nullable<int> t314_idusuario_ultmodif,
                                Nullable<DateTime> t332_fultmodif, Nullable<int> t346_idpst,
                                Nullable<short> t353_idorigen, string t332_otl, string t332_incidencia)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t314_idusuario_ultmodif", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_ultmodif;
            aParam[2] = new SqlParameter("@t332_fultmodif", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t332_fultmodif;
            aParam[3] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
            aParam[3].Value = t346_idpst;
            aParam[4] = new SqlParameter("@t353_idorigen", SqlDbType.SmallInt, 2);
            aParam[4].Value = t353_idorigen;
            aParam[5] = new SqlParameter("@t332_otl", SqlDbType.Text, 25);
            aParam[5].Value = t332_otl;
            aParam[6] = new SqlParameter("@t332_incidencia", SqlDbType.Text, 25);
            aParam[6].Value = t332_incidencia;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREA_P2_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREA_P2_U", aParam);
        }

        public static int Update_P3(SqlTransaction tr, int t332_idtarea,
                               Nullable<int> t314_idusuario_ultmodif,
                               Nullable<DateTime> t332_fultmodif,
                               string t332_notas1, string t332_notas2, string t332_notas3, string t332_notas4,
                               bool t332_notasiap)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t314_idusuario_ultmodif", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_ultmodif;
            aParam[2] = new SqlParameter("@t332_fultmodif", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t332_fultmodif;
            aParam[3] = new SqlParameter("@t332_notas1", SqlDbType.Text, 2147483647);
            aParam[3].Value = t332_notas1;
            aParam[4] = new SqlParameter("@t332_notas2", SqlDbType.Text, 2147483647);
            aParam[4].Value = t332_notas2;
            aParam[5] = new SqlParameter("@t332_notas3", SqlDbType.Text, 2147483647);
            aParam[5].Value = t332_notas3;
            aParam[6] = new SqlParameter("@t332_notas4", SqlDbType.Text, 2147483647);
            aParam[6].Value = t332_notas4;
            aParam[7] = new SqlParameter("@t332_notasiap", SqlDbType.Bit, 1);
            aParam[7].Value = t332_notasiap;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREA_P3_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREA_P3_U", aParam);
        }
        public static int Update_P4(SqlTransaction tr, int t332_idtarea,
                                Nullable<int> t314_idusuario_ultmodif,
                                Nullable<DateTime> t332_fultmodif,
                                string t332_observaciones)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t314_idusuario_ultmodif", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_ultmodif;
            aParam[2] = new SqlParameter("@t332_fultmodif", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t332_fultmodif;
            aParam[3] = new SqlParameter("@t332_observaciones", SqlDbType.Text, 2147483647);
            aParam[3].Value = t332_observaciones;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREA_P4_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREA_P4_U", aParam);
        }


        #region Metodos para catalogos de tarea en pantalla de reestructuración
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas de un proyecto económico en las que FFPR < FFPL
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoFFPRmenorFFPL(int idPSN, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = idPSN;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_FFPRmenorFFPL_C2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas de un proyecto económico en las que FFPR < FFPL
        /// Solo para aquellas tareas que cuelgan de PTs de los que el usuario es RTPT
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoFFPRmenorFFPL(int idPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = idPSN;
            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_FFPRmenorFFPL_C", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas de un proyecto económico
        /// </summary>
        /// -----------------------------------------------------------------------------
        //public static SqlDataReader CatalogoPE(int cod_pe, int iUser, string sPerfil)
        public static SqlDataReader CatalogoPE(int cod_pe, int iUser)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@cod_pe", SqlDbType.Int, 4);
            aParam[0].Value = cod_pe;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = iUser;
            //aParam[2] = new SqlParameter("@sPerfil", SqlDbType.Char, 2);
            //aParam[2].Value = sPerfil;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C_PE_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C_PE", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas de un proyecto técnico
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoPT(int cod_pt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@cod_pt", SqlDbType.Int, 4);
            aParam[0].Value = cod_pt;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C_PT", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas de una fase
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoF(int cod_fase)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@cod_fase", SqlDbType.Int, 4);
            aParam[0].Value = cod_fase;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C_F", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas de una actividad
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoA(int cod_activ)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@cod_activ", SqlDbType.Int, 4);
            aParam[0].Value = cod_activ;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_C_A", aParam);
        }

        #endregion

        public static TAREAPSP ObtenerDatosRecurso(SqlTransaction tr, int nIdTarea, int nUsuario)
        {
            TAREAPSP o = new TAREAPSP();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;
            aParam[1].Value = nUsuario;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAIAP_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAIAP_S", aParam);

            if (dr.Read())
            {
                if (dr["t332_idtarea"] != DBNull.Value)
                    o.t332_idtarea = (int)dr["t332_idtarea"];
                if (dr["t332_destarea"] != DBNull.Value)
                    o.t332_destarea = (string)dr["t332_destarea"];
                if (dr["t332_destarealong"] != DBNull.Value)
                    o.t332_destarealong = (string)dr["t332_destarealong"];
                if (dr["t331_idpt"] != DBNull.Value)
                    o.t331_idpt = (int)dr["t331_idpt"];
                if (dr["t335_idactividad"] != DBNull.Value)
                    o.t335_idactividad = (int)dr["t335_idactividad"];
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.num_proyecto = (int)dr["t301_idproyecto"];
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.nom_proyecto = (string)dr["t301_denominacion"];
                if (dr["t305_seudonimo"] != DBNull.Value)
                    o.t305_seudonimo = (string)dr["t305_seudonimo"];
                if (dr["t331_despt"] != DBNull.Value)
                    o.t331_despt = (string)dr["t331_despt"];
                if (dr["t334_desfase"] != DBNull.Value)
                    o.t334_desfase = (string)dr["t334_desfase"];
                if (dr["t335_desactividad"] != DBNull.Value)
                    o.t335_desactividad = (string)dr["t335_desactividad"];

                if (dr["dPrimerConsumo"] != DBNull.Value)
                    o.dPrimerConsumo = DateTime.Parse(dr["dPrimerConsumo"].ToString());
                if (dr["dUltimoConsumo"] != DBNull.Value)
                    o.dUltimoConsumo = DateTime.Parse(dr["dUltimoConsumo"].ToString());
                if (dr["t336_etp"] != DBNull.Value)
                    o.t336_etp = double.Parse(dr["t336_etp"].ToString());
                if (dr["t336_ete"] != DBNull.Value)
                    o.t336_ete = double.Parse(dr["t336_ete"].ToString());
                if (dr["t336_ffp"] != DBNull.Value)
                    o.t336_ffp = DateTime.Parse(dr["t336_ffp"].ToString());
                if (dr["t336_ffe"] != DBNull.Value)
                    o.t336_ffe = DateTime.Parse(dr["t336_ffe"].ToString());
                if (dr["t336_indicaciones"] != DBNull.Value)
                    o.t336_indicaciones = (string)dr["t336_indicaciones"];
                if (dr["t336_comentario"] != DBNull.Value)
                    o.t336_comentario = (string)dr["t336_comentario"];
                if (dr["t336_completado"] != DBNull.Value)
                    o.nCompletado = byte.Parse(dr["t336_completado"].ToString());
                if (dr["esfuerzo"] != DBNull.Value)
                    o.nConsumidoHoras = double.Parse(dr["esfuerzo"].ToString());
                if (dr["esfuerzoenjor"] != DBNull.Value)
                    o.nConsumidoJornadas = double.Parse(dr["esfuerzoenjor"].ToString());
                if (dr["nPendienteEstimado"] != DBNull.Value)
                    o.nPendienteEstimado = double.Parse(dr["nPendienteEstimado"].ToString());
                if (dr["nAvanceTeorico"] != DBNull.Value)
                    o.nAvanceTeorico = double.Parse(dr["nAvanceTeorico"].ToString());
                if (dr["t332_notas1"] != DBNull.Value)
                    o.t332_notas1 = (string)dr["t332_notas1"];
                if (dr["t332_notas2"] != DBNull.Value)
                    o.t332_notas2 = (string)dr["t332_notas2"];
                if (dr["t332_notas3"] != DBNull.Value)
                    o.t332_notas3 = (string)dr["t332_notas3"];
                if (dr["t332_notas4"] != DBNull.Value)
                    o.t332_notas4 = (string)dr["t332_notas4"];
                if (dr["t332_impiap"] != DBNull.Value)
                    o.t332_impiap = (bool)dr["t332_impiap"];
                if (dr["t332_notasiap"] != DBNull.Value)
                    o.t332_notasiap = (bool)dr["t332_notasiap"];
                if (dr["t332_mensaje"] != DBNull.Value)
                    o.t332_mensaje = (string)dr["t332_mensaje"];
                if (dr["t324_idmodofact"] != DBNull.Value)
                    o.t324_idmodofact = (int)dr["t324_idmodofact"];
                if (dr["t324_denominacion"] != DBNull.Value)
                    o.t324_denominacion = (string)dr["t324_denominacion"];
                if (dr["t336_estado"] != DBNull.Value)
                    o.t336_estado = (bool)dr["t336_estado"];

                dr.Close();
                dr.Dispose();
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningún dato de la tarea indicada"));
            }

            return o;
        }

        /// <summary>
        /// 
        /// Modifica los datos básicos del Consumo,
        /// correspondientes a la tabla consumo_tar.
        /// </summary>
        public static void ActualizarNotas(SqlTransaction tr, int t332_idtarea, string t332_notas1, string t332_notas2, string t332_notas3, string t332_notas4)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t332_notas1", SqlDbType.Text, 2147483647);
            aParam[2] = new SqlParameter("@t332_notas2", SqlDbType.Text, 2147483647);
            aParam[3] = new SqlParameter("@t332_notas3", SqlDbType.Text, 2147483647);
            aParam[4] = new SqlParameter("@t332_notas4", SqlDbType.Text, 2147483647);

            aParam[0].Value = t332_idtarea;
            aParam[1].Value = t332_notas1;
            aParam[2].Value = t332_notas2;
            aParam[3].Value = t332_notas3;
            aParam[4].Value = t332_notas4;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_NOTASIAP_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NOTASIAP_U", aParam);
        }

        public static int UpdateVigenciaByPSN(SqlTransaction tr, int t305_idproyectosubnodo,
                               Nullable<DateTime> t332_fiv,
                               Nullable<DateTime> t332_ffv)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t332_fiv", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t332_fiv;
            aParam[2] = new SqlParameter("@t332_ffv", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t332_ffv;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSP_UVIG_SBy_PSN", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSP_UVIG_SBy_PSN", aParam);
        }

        public static int GetNodo(SqlTransaction tr, int nTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[0].Value = nTarea;
            if (tr != null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GET_NODO_TAREA", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_GET_NODO_TAREA", aParam));
        }
        /// <summary>
        /// 
        /// Obtiene un catalogo de Tareas accesibles por el usuario y que tienen asociados datos en la Bitacora de Tarea (T600_ASUNTOT)
        /// </summary>
        public static SqlDataReader CatalogoBitacora(Nullable<int> cod_pt)//, int iUser
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = cod_pt;
            //aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            //aParam[1].Value = iUser;

            //// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            //{//Si es administrador no merece la pena investigar que figuras tiene
            //    return SqlHelper.ExecuteSqlDataReader("SUP_T_BIT_C_ADMIN", aParam);
            //}
            //else
            return SqlHelper.ExecuteSqlDataReader("SUP_T_BIT_C", aParam);
        }
        /// <summary>
        /// Obtiene el estado del proyecto que engloba la tarea que se le pasa como parametro
        /// </summary>
        public static string getEstado(SqlTransaction tr, int nIdT)
        {
            string sRes = "C";
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdT;
            object obj;
            if (tr == null)
                obj = SqlHelper.ExecuteScalar("SUP_TAREA_ESTADO", aParam);
            else
                obj = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREA_ESTADO", aParam);
            if (obj != null)
                sRes = obj.ToString();
            return sRes;
        }

        public static SqlDataReader flContolTraspasoIAP(SqlTransaction tr, int t332_idtarea, DateTime t337_fecha)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t337_fecha", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t337_fecha;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_CTIAP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_CTIAP", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSPUSUARIO_D_USU", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPUSUARIO_D_USU", aParam);
        }

        public static SqlDataReader ObtenerAuditoriaPrevisiones(int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t332_idtarea", SqlDbType.Int, 4, t332_idtarea);

            return SqlHelper.ExecuteSqlDataReader("SUP_AUDITSUPER_PRTAREA", aParam);
        }

        public static void ActualizarETPRByPSN(SqlTransaction tr, int t305_idproyectosubnodo, int nUsuario, int anomes, bool bRTPT)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@nUsuario", SqlDbType.Int, 4, nUsuario);
            aParam[i++] = ParametroSql.add("@nAnomes", SqlDbType.Int, 4, anomes);
            aParam[i++] = ParametroSql.add("@bRTPT", SqlDbType.Bit, 1, bRTPT);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SETETPRTAREA_PSN", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SETETPRTAREA_PSN", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza el estado de la tabla T332_TAREAPSP.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/02/2016 08:29:19
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Cierre(SqlTransaction tr, int t305_idproyectosubnodo, int nRecurso, bool bRTPT, bool bParalizada, bool bActiva, bool bPendiente, bool bFinalizada, Nullable<DateTime> t332_fiv, Nullable<DateTime> t332_ffv)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[1].Value = nRecurso;
            aParam[2] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[2].Value = bRTPT;
            aParam[3] = new SqlParameter("@bParalizada", SqlDbType.Bit, 1);
            aParam[3].Value = bParalizada;
            aParam[4] = new SqlParameter("@bActiva", SqlDbType.Bit, 1);
            aParam[4].Value = bActiva;
            aParam[5] = new SqlParameter("@bPendiente", SqlDbType.Bit, 1);
            aParam[5].Value = bPendiente;
            aParam[6] = new SqlParameter("@bFinalizada", SqlDbType.Bit, 1);
            aParam[6].Value = bFinalizada;
            aParam[7] = new SqlParameter("@t332_fiv", SqlDbType.SmallDateTime, 4);
            aParam[7].Value = t332_fiv;
            aParam[8] = new SqlParameter("@t332_ffv", SqlDbType.SmallDateTime, 4);
            aParam[8].Value = t332_ffv;

            // Ejecuta la query y devuelve el numero de registros modificados.
            object oRes;
            if (tr == null)
                oRes = SqlHelper.ExecuteReturn("SUP_TAREAS_CIERRE", aParam);
            else
                oRes = SqlHelper.ExecuteReturnTransaccion(tr, "SUP_TAREAS_CIERRE", aParam);

            return Convert.ToInt32(oRes);
        }

        #endregion
    }

}
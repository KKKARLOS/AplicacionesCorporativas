using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SUPER.Capa_Negocio
{
    public class DIALOGOALERTAS
    {
        #region Propiedades y Atributos

        private int _t831_iddialogoalerta;
        public int t831_iddialogoalerta
        {
            get { return _t831_iddialogoalerta; }
            set { _t831_iddialogoalerta = value; }
        }

        private string _t820_denominacion;
        public string t820_denominacion
        {
            get { return _t820_denominacion; }
            set { _t820_denominacion = value; }
        }

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private string _t301_denominacion;
        public string t301_denominacion
        {
            get { return _t301_denominacion; }
            set { _t301_denominacion = value; }
        }

        private DateTime? _t831_flr;
        public DateTime? t831_flr
        {
            get { return _t831_flr; }
            set { _t831_flr = value; }
        }

        private string _t302_denominacion;
        public string t302_denominacion
        {
            get { return _t302_denominacion; }
            set { _t302_denominacion = value; }
        }

        private int? _t831_anomesdecierre;
        public int? t831_anomesdecierre
        {
            get { return _t831_anomesdecierre; }
            set { _t831_anomesdecierre = value; }
        }

        private byte _t827_idestadodialogoalerta;
        public byte t827_idestadodialogoalerta
        {
            get { return _t827_idestadodialogoalerta; }
            set { _t827_idestadodialogoalerta = value; }
        }

        private string _t827_denominacion;
        public string t827_denominacion
        {
            get { return _t827_denominacion; }
            set { _t827_denominacion = value; }
        }

        private string _t827_descripcion;
        public string t827_descripcion
        {
            get { return _t827_descripcion; }
            set { _t827_descripcion = value; }
        }

        private string _responsable_proyecto;
        public string responsable_proyecto
        {
            get { return _responsable_proyecto; }
            set { _responsable_proyecto = value; }
        }

        private string _t303_denominacion;
        public string t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }

        private string _t831_entepromotor;
        public string t831_entepromotor
        {
            get { return _t831_entepromotor; }
            set { _t831_entepromotor = value; }
        }

        private int? _t314_idusuario_responsable;
        public int? t314_idusuario_responsable
        {
            get { return _t314_idusuario_responsable; }
            set { _t314_idusuario_responsable = value; }
        }


        private bool _bTieneAlertas;
        public bool bTieneAlertas
        {
            get { return _bTieneAlertas; }
            set { _bTieneAlertas = value; }
        }
        private bool _bUsuarioEsInterlocutor;
        public bool bUsuarioEsInterlocutor
        {
            get { return _bUsuarioEsInterlocutor; }
            set { _bUsuarioEsInterlocutor = value; }
        }
        private int _nDialogosAbiertos;
        public int nDialogosAbiertos
        {
            get { return _nDialogosAbiertos; }
            set { _nDialogosAbiertos = value; }
        }
        private int _nPendienteLeerInterlocutor;
        public int nPendienteLeerInterlocutor
        {
            get { return _nPendienteLeerInterlocutor; }
            set { _nPendienteLeerInterlocutor = value; }
        }
        private int _nPendienteResponderInterlocutor;
        public int nPendienteResponderInterlocutor
        {
            get { return _nPendienteResponderInterlocutor; }
            set { _nPendienteResponderInterlocutor = value; }
        }
        private int _nPendienteLeerGestor;
        public int nPendienteLeerGestor
        {
            get { return _nPendienteLeerGestor; }
            set { _nPendienteLeerGestor = value; }
        }
        private int _nPendienteResponderGestor;
        public int nPendienteResponderGestor
        {
            get { return _nPendienteResponderGestor; }
            set { _nPendienteResponderGestor = value; }
        }

        #endregion

        #region Constructor

        public DIALOGOALERTAS()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static DIALOGOALERTAS ObtenerDatosDialogoAlerta(int t831_iddialogoalerta)
        {
            DIALOGOALERTAS o = new DIALOGOALERTAS();

            SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerDatosDialogoAlerta(null, t831_iddialogoalerta);
            if (dr.Read())
            {
                if (dr["t831_iddialogoalerta"] != DBNull.Value)
                    o.t831_iddialogoalerta = int.Parse(dr["t831_iddialogoalerta"].ToString());
                if (dr["t820_denominacion"] != DBNull.Value)
                    o.t820_denominacion = (string)dr["t820_denominacion"];
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t831_flr"] != DBNull.Value)
                    o.t831_flr = (DateTime)dr["t831_flr"];
                if (dr["t302_denominacion"] != DBNull.Value)
                    o.t302_denominacion = (string)dr["t302_denominacion"];
                if (dr["t831_anomesdecierre"] != DBNull.Value)
                    o.t831_anomesdecierre = int.Parse(dr["t831_anomesdecierre"].ToString());
                if (dr["t827_idestadodialogoalerta"] != DBNull.Value)
                    o.t827_idestadodialogoalerta = byte.Parse(dr["t827_idestadodialogoalerta"].ToString());
                if (dr["t827_denominacion"] != DBNull.Value)
                    o.t827_denominacion = (string)dr["t827_denominacion"];
                if (dr["t827_descripcion"] != DBNull.Value)
                    o.t827_descripcion = (string)dr["t827_descripcion"];
                if (dr["Responsable"] != DBNull.Value)
                    o.responsable_proyecto = (string)dr["Responsable"];
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t831_entepromotor"] != DBNull.Value)
                    o.t831_entepromotor = (string)dr["t831_entepromotor"];
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de DIALOGOALERTAS"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static DataSet ObtenerDialogoAlerta(int t831_iddialogoalerta, int t314_idusuario, bool bEsGestor)
        {
            return SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerDialogoAlerta(null, t831_iddialogoalerta, t314_idusuario, bEsGestor);
        }

        public static bool EsGestorDeDialogoAlerta(int t831_iddialogoalerta, int t314_idusuario)
        {
            bool bRes = false;

            bRes = SUPER.Capa_Datos.DIALOGOALERTAS.EsGestorDEF_AlertaOC_FA(null, t831_iddialogoalerta, t314_idusuario);
            if (!bRes)
            {
                bRes = SUPER.Capa_Datos.DIALOGOALERTAS.EsGestorDeDialogoAlerta(null, t831_iddialogoalerta, t314_idusuario);
            }
            return bRes;
        }

        public static string ObtenerOtrosDialogosAbiertos(int t831_iddialogoalerta, int t314_idusuario)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append(@"<table id='tblOtrosDialogos' style='width:900px; margin-left:2px;' border='0'>
                <colgroup>
                <col style='width: 300px;' />
                <col style='width: 300px;' />
                <col style='width: 90px;' />
                <col style='width: 100px;' />
                <col style='width: 110px;' />
                </colgroup>");

            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerOtrosDialogosAbiertos(null, t831_iddialogoalerta, t314_idusuario);
            //int i = 0;
            while (dr.Read())
            {
                sb.Append("<tr bd='' ");
                sb.Append("idDialogo='" + dr["t831_iddialogoalerta"].ToString() + "'>");

                sb.Append("<td><nobr class='NBR W330' onmouseover='TTip(event)'>" + dr["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W330' onmouseover='TTip(event)'>" + dr["t827_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((dr["t831_anomesdecierre"] != DBNull.Value)? Fechas.AnnomesAFechaDescLarga((int)dr["t831_anomesdecierre"]) :"") + "</td>");
                sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' style='cursor:pointer;' onclick='setCierre(this, 1)' /></td>");
                sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' style='cursor:pointer;' onclick='setCierre(this, 0)' /></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static string CerrarDialogo(string sDatosAlertaActual, string sDatosOtrasAlertas, string sDatosDialogoActual, string sOtrosDialogos)
        {
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            string sResul = "";

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            }
            #endregion

            try
            {
                #region Datos Alerta Actual
                string[] aDatosAA = Regex.Split(sDatosAlertaActual, "{sep}");
                ///aDatosAA[0] = idPSN
                ///aDatosAA[1] = idAlerta
                ///aDatosAA[2] = Habilitada
                ///aDatosAA[3] = inistandby
                ///aDatosAA[4] = finstandby
                ///aDatosAA[5] = Seguimiento

                SUPER.Capa_Datos.PSNALERTAS.Update(tr, int.Parse(aDatosAA[0]),
                    byte.Parse(aDatosAA[1]),
                    (aDatosAA[2] == "1") ? true : false,
                    (aDatosAA[3] == "") ? null : (int?)int.Parse(aDatosAA[3]),
                    (aDatosAA[4] == "") ? null : (int?)int.Parse(aDatosAA[4]),
                    Utilidades.unescape(aDatosAA[5]));

                #endregion

                #region Datos Otras Alertas
                if (sDatosOtrasAlertas != "")
                {
                    string[] aDatosOA = Regex.Split(sDatosOtrasAlertas, "{sepReg}");
                    foreach (string oDatosOA in aDatosOA)
                    {
                        if (oDatosOA == "") continue;
                        string[] aValoresOA = Regex.Split(oDatosOA, "{sep}");

                        SUPER.Capa_Datos.PSNALERTAS.Update(tr, int.Parse(aValoresOA[0]),
                            byte.Parse(aValoresOA[1]),
                            (aValoresOA[2] == "1") ? true : false,
                            (aValoresOA[3] == "") ? null : (int?)int.Parse(aValoresOA[3]),
                            (aValoresOA[4] == "") ? null : (int?)int.Parse(aDatosAA[4]),
                            Utilidades.unescape(aValoresOA[5]));
                    }

                }
                #endregion

                #region Datos Dialogo Actual
                string[] aDatosDA = Regex.Split(sDatosDialogoActual, "{sep}");
                ///aDatosDA[0] = idDialogo
                ///aDatosDA[1] = hdnIdEstado
                ///aDatosDA[2] = OC justificada
                ///aDatosDA[3] = txtCausa
                ///aDatosDA[4] = txtAcciones
                ///aDatosDA[5] = cboMotivo
                SUPER.Capa_Datos.DIALOGOALERTAS.CerrarDialogo(tr, (int)HttpContext.Current.Session["IDFICEPI_ENTRADA"],
                                                                int.Parse(aDatosDA[0]),
                                                                byte.Parse(aDatosDA[1]),
                                                                (aDatosDA[2]=="")? null: (bool?)((aDatosDA[2]=="1")? true:false),
                                                                Utilidades.unescape(aDatosDA[3]),
                                                                Utilidades.unescape(aDatosDA[4]),
                                                                (aDatosDA[5] == "") ? null : (int?)int.Parse(aDatosDA[5])
                                                                );
                #endregion

                #region Datos Otros Dialogos
                if (sOtrosDialogos != "")
                {
                    string[] aDatosOD = Regex.Split(sOtrosDialogos, "{sepReg}");
                    foreach (string oDatosOD in aDatosOD)
                    {
                        if (oDatosOD == "") continue;
                        string[] aValoresOD = Regex.Split(oDatosOD, "{sep}");
                        ///aValoresOD[0] = idDialogo
                        ///aValoresOD[1] = Conforme
                        ///aValoresOD[2] = No conforme

                        SUPER.Capa_Datos.DIALOGOALERTAS.CerrarDialogo(tr, (int)HttpContext.Current.Session["IDFICEPI_ENTRADA"],
                            int.Parse(aValoresOD[0]),
                            (aValoresOD[1] == "1") ? (byte)4 : (byte)5,
                            null, "", "", null);
                    }

                }
                #endregion

                sResul = "OK";
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (Errores.EsErrorIntegridad(ex)) sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al grabar los datos del proyecto", ex, false); //ex.Message;
                else sResul = "Error@#@" + Errores.mostrarError("Error al cerrar el diálogo.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        public static void ActualizarFechaTipo(int t831_iddialogoalerta, byte t820_idalerta, Nullable<DateTime> t831_flr)
        {
            SUPER.Capa_Datos.DIALOGOALERTAS.ActualizarFechaTipo(null, t831_iddialogoalerta, t820_idalerta, t831_flr);
        }

        public static string ObtenerDialogosPendientes(int t314_idusuario)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerDialogosPendientes(null, t314_idusuario);

            #region Tabla Usuario
            sb.Append(@"<table id='tblDatosUsuario' class='MA' style='width:970px;' cellpadding='0' cellspacing='0' border='0'>
                <colgroup>
                <col style='width: 60px;' />
                <col style='width: 240px;' />
                <col style='width: 200px;' />
                <col style='width: 200px;' />
                <col style='width: 200px;' />
                <col style='width: 70px;' />
                </colgroup>");

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("<tr ");
                sb.Append("id='" + oFila["t831_iddialogoalerta"].ToString() + "' ");
                sb.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(oFila["Responsable"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(oFila["t302_denominacion"].ToString()) + "\" ");

                sb.Append("onclick=\"ms(this);habCarrusel();\" ");
                sb.Append("ondblclick='getDialogoAlerta(" + oFila["t831_iddialogoalerta"].ToString() + ")'>");

                sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + "</td>");
                //sb.Append("<td><nobr class='NBR W230'>" + oFila["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W230'");

                string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString().Replace("'", "&#39;") + "<br><label style=width:70px;>Responsable:</label>" + oFila["Responsable"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString();
                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\")\' onMouseout=\"hideTTE()\" ");

                sb.Append(">" + oFila["t301_denominacion"].ToString().Replace("'", "&#39;") + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190'>" + oFila["t302_denominacion"].ToString().Replace("'", "&#39;") + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190' ");
                sTooltip = "";
                if (oFila["t820_parametro1"] != DBNull.Value)
                {
                    sTooltip = " - <label style='text-decoration:underline;'>" + oFila["t820_descparam1"].ToString() + ":</label><br><label style='width:60px; margin-left:30px;'>Referencia:</label>" + ((oFila["t831_vreferencia1"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreferencia1"].ToString()).ToString("N") : "") + "<br><label style='width:60px; margin-left:30px;'>Real:</label>" + ((oFila["t831_vreal1"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreal1"].ToString()).ToString("N") : "");
                }
                if (oFila["t820_parametro2"] != DBNull.Value)
                {
                    sTooltip += "<br> - <label style='text-decoration:underline;'>" + oFila["t820_descparam2"].ToString() + ":</label><br><label style='width:60px; margin-left:30px;'>Referencia:</label>" + ((oFila["t831_vreferencia2"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreferencia2"].ToString()).ToString("N") : "") + "<br><label style='width:60px; margin-left:30px;'>Real:</label>" + ((oFila["t831_vreal2"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreal2"].ToString()).ToString("N") : "");
                }
                if (oFila["t820_parametro3"] != DBNull.Value)
                {
                    sTooltip += "<br> - <label style='text-decoration:underline;'>" + oFila["t820_descparam3"].ToString() + ":</label><br><label style='width:60px; margin-left:30px;'>Referencia:</label>" + ((oFila["t831_vreferencia3"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreferencia3"].ToString()).ToString("N") : "") + "<br><label style='width:60px; margin-left:30px;'>Real:</label>" + ((oFila["t831_vreal3"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreal3"].ToString()).ToString("N") : "");
                }
                if (sTooltip != "")
                {
                    sb.Append("onmouseover=showTTE(\"" + Utilidades.escape(sTooltip) + "\") onMouseout=\"hideTTE()\" ");
                }
                sb.Append(">" + oFila["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190'>" + oFila["t827_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center;'>" + ((oFila["t831_flr"] == DBNull.Value) ? "" : ((DateTime)oFila["t831_flr"]).ToShortDateString()) + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            #endregion

            sb.Append("{septabla}");

            #region Tabla Gestor
            sb.Append(@"<table id='tblDatosGestor' class='MA' style='width:970px;' cellpadding='0' cellspacing='0' border='0'>
                <colgroup>
                <col style='width: 60px;' />
                <col style='width: 240px;' />
                <col style='width: 160px;' />
                <col style='width: 160px;' />
                <col style='width: 160px;' />
                <col style='width: 120px;' />
                <col style='width: 70px;' />
                </colgroup>");

            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                sb.Append("<tr ");
                sb.Append("id='" + oFila["t831_iddialogoalerta"].ToString() + "' ");
                sb.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(oFila["Responsable"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(oFila["t302_denominacion"].ToString()) + "\" ");

                sb.Append("onclick=\"ms(this);setOp($I('btnCarruselGes'), 100);\" ");
                sb.Append("ondblclick='getDialogoAlerta(" + oFila["t831_iddialogoalerta"].ToString() + ")'>");

                sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W230'");

                string sTooltip = "<label style=width:60px;>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString().Replace("'", "&#39;") + "<br><label style=width:60px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString();
                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\")\' onMouseout=\"hideTTE()\" ");

                sb.Append(">" + oFila["t301_denominacion"].ToString().Replace("'", "&#39;") + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W150'>" + oFila["Responsable"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W150'>" + oFila["t302_denominacion"].ToString().Replace("'", "&#39;") + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W150' ");
                sTooltip = "";
                if (oFila["t820_parametro1"] != DBNull.Value)
                {
                    sTooltip = " - <label style='text-decoration:underline;'>" + oFila["t820_descparam1"].ToString() + ":</label><br><label style='width:60px; margin-left:30px;'>Referencia:</label>" + ((oFila["t831_vreferencia1"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreferencia1"].ToString()).ToString("N") : "") + "<br><label style='width:60px; margin-left:30px;'>Real:</label>" + ((oFila["t831_vreal1"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreal1"].ToString()).ToString("N") : "");
                }
                if (oFila["t820_parametro2"] != DBNull.Value)
                {
                    sTooltip += "<br> - <label style='text-decoration:underline;'>" + oFila["t820_descparam2"].ToString() + ":</label><br><label style='width:60px; margin-left:30px;'>Referencia:</label>" + ((oFila["t831_vreferencia2"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreferencia2"].ToString()).ToString("N") : "") + "<br><label style='width:60px; margin-left:30px;'>Real:</label>" + ((oFila["t831_vreal2"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreal2"].ToString()).ToString("N") : "");
                }
                if (oFila["t820_parametro3"] != DBNull.Value)
                {
                    sTooltip += "<br> - <label style='text-decoration:underline;'>" + oFila["t820_descparam3"].ToString() + ":</label><br><label style='width:60px; margin-left:30px;'>Referencia:</label>" + ((oFila["t831_vreferencia3"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreferencia3"].ToString()).ToString("N") : "") + "<br><label style='width:60px; margin-left:30px;'>Real:</label>" + ((oFila["t831_vreal3"] != DBNull.Value) ? decimal.Parse(oFila["t831_vreal3"].ToString()).ToString("N") : "");
                }
                if (sTooltip != "")
                {
                    sb.Append("onmouseover=showTTE(\"" + Utilidades.escape(sTooltip) + "\") onMouseout=\"hideTTE()\" ");
                }
                sb.Append(">" + oFila["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W120' onmouseover='TTip(event)'>" + oFila["t827_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center;'>" + ((oFila["FUR"] == DBNull.Value) ? "" : ((DateTime)oFila["FUR"]).ToShortDateString()) + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            #endregion

            sb.Append("{septabla}");
            sb.Append((ds.Tables[0].Rows.Count > 0) ? "1" : "0");
            sb.Append("{septabla}");
            sb.Append((ds.Tables[1].Rows.Count > 0) ? "1" : "0");

            ds.Dispose();

            return sb.ToString();
        }

        public static string ObtenerDialogosByPSN(int t305_idproyectosubnodo, bool bSoloActivos, bool bRestringirOCyFACerrados)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append(@"<table id='tblDatos' class='MA' style='width: 960px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
			                <col style='width:350px;' />
			                <col style='width:120px;' />
			                <col style='width:250px;' />
			                <col style='width:80px;' />
			                <col style='width:80px;' />
			                <col style='width:80px;' />
                        </colgroup>");

            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerDialogosByPSN(null, t305_idproyectosubnodo, bSoloActivos, bRestringirOCyFACerrados);

            while (dr.Read())
            {
                sb.Append("<tr bd='' ");
                sb.Append("id='" + dr["t831_iddialogoalerta"].ToString() + "' ");
                sb.Append("onclick='ms(this)' ");
                sb.Append("ondblclick='getDialogoAlerta(" + dr["t831_iddialogoalerta"].ToString() + ")'>");

                sb.Append("<td><nobr class='NBR W330' ");

                string sTooltip = "<label style=width:70px;>Asunto:</label>" + dr["t820_denominacion"].ToString() + "<br><label style=width:70px;>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>Interlocutor:</label>" + dr["Interlocutor"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\")\' onMouseout=\"hideTTE()\" ");

                sb.Append(">" + dr["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["anomesdecierre"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W330' onmouseover='TTip(event)'>" + dr["t827_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((DateTime)dr["t831_fcreacion"]).ToShortDateString() + "</td>");
                sb.Append("<td>" + ((dr["t831_flr"]==DBNull.Value)?"": ((DateTime)dr["t831_flr"]).ToShortDateString()) + "</td>");
                sb.Append("<td>" + ((DateTime)dr["FUM"]).ToShortDateString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static bool TienePermisoCreacion(int t305_idproyectosubnodo, int t314_idusuario)
        {
            return SUPER.Capa_Datos.DIALOGOALERTAS.TienePermisoCreacion(null, t305_idproyectosubnodo, t314_idusuario);
        }

        public static string ObtenerMisDialogosAbiertos(int t314_idusuario)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append(@"<table id='tblDatos' class='MA' style='width: 960px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
	                        <col style='width:60px;' />
	                        <col style='width:240px;' />
	                        <col style='width:210px;' />
	                        <col style='width:120px;' />
	                        <col style='width:250px;' />
	                        <col style='width:80px;' />
                        </colgroup>");

            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerMisDialogosAbiertos(null, t314_idusuario);

            while (dr.Read())
            {
                sb.Append("<tr bd='' ");
                sb.Append("id='" + dr["t831_iddialogoalerta"].ToString() + "' ");

                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["Responsable"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString()) + "\" ");

                sb.Append("onclick=\"ms(this);setOp($I('btnCarrusel'), 100);\" ");
                sb.Append("ondblclick='getDialogoAlerta(" + dr["t831_iddialogoalerta"].ToString() + ")'>");

                sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W230'");

                string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\")\' onMouseout=\"hideTTE()\" ");

                sb.Append(">" + dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W200' onmouseover='TTip(event)'>" + dr["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["anomesdecierre"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W240' onmouseover='TTip(event)'>" + dr["t827_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((dr["t831_flr"] == DBNull.Value) ? "" : ((DateTime)dr["t831_flr"]).ToShortDateString()) + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static string ObtenerDialogosMiGestion(int t314_idusuario,
           bool bSoloAbiertos,
           Nullable<byte> t821_idgrupoalerta,
           Nullable<byte> t820_idalerta,
           Nullable<int> t305_idproyectosubnodo,
           Nullable<int> t001_idficepi_interlocutor,
           Nullable<byte> t827_idestadodialogoalerta,
           Nullable<int> t303_idnodo,
           Nullable<int> t302_idcliente,
           Nullable<int> t831_iddialogoalerta,
           Nullable<DateTime> t831_flr,
           Nullable<int> t314_idusuario_gestor,
           Nullable<int> t314_idusuario_responsable,
           Nullable<int> nMesDesde,
           Nullable<int> nMesHasta,
           byte nOrden,
           byte nAscDesc
            )
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append(@"<table id='tblDatos' class='MANO' style='width: 960px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
	                        <col style='width:20px;' />
	                        <col style='width:60px;' />
	                        <col style='width:240px;' />
	                        <col style='width:240px;' />
	                        <col style='width:120px;' />
	                        <col style='width:190px;' />
	                        <col style='width:65px;' />
                            <col style='width:25px;' />
                        </colgroup>");

            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerDialogosMiGestion(null, t314_idusuario,
                                   bSoloAbiertos,
                                   t821_idgrupoalerta,
                                   t820_idalerta,
                                   t305_idproyectosubnodo,
                                   t001_idficepi_interlocutor,
                                   t827_idestadodialogoalerta,
                                   t303_idnodo,
                                   t302_idcliente,
                                   t831_iddialogoalerta,
                                   t831_flr,
                                   t314_idusuario_gestor,
                                   t314_idusuario_responsable,
                                   nMesDesde,
                                   nMesHasta,
                                   nOrden,
                                   nAscDesc
                                   );

            bool bExcede = false;
            int i = 0;
            while (dr.Read())
            {
                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
                sb.Append("<tr bd='' ");
                sb.Append("id='" + dr["t831_iddialogoalerta"].ToString() + "' ");
                sb.Append("estado='" + (((bool)dr["bAbierto"]) ? "1" : "0") + "' ");

                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["Responsable"].ToString()) + "\" ");
                sb.Append("interlocutor=\"" + Utilidades.escape(dr["Interlocutor"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString()) + "\" ");
                sb.Append("gc=\"" + Utilidades.escape(dr["GestorCirculante"].ToString()) + "\" ");                

                sb.Append("ref1=\"" + ((dr["t831_vreferencia1"] == DBNull.Value) ? "" : decimal.Parse(dr["t831_vreferencia1"].ToString()).ToString("N")) + "\" ");
                sb.Append("ref2=\"" + ((dr["t831_vreferencia2"] == DBNull.Value) ? "" : decimal.Parse(dr["t831_vreferencia2"].ToString()).ToString("N")) + "\" ");
                sb.Append("ref3=\"" + ((dr["t831_vreferencia3"] == DBNull.Value) ? "" : decimal.Parse(dr["t831_vreferencia3"].ToString()).ToString("N")) + "\" ");
                sb.Append("real1=\"" + ((dr["t831_vreal1"] == DBNull.Value) ? "" : decimal.Parse(dr["t831_vreal1"].ToString()).ToString("N")) + "\" ");
                sb.Append("real2=\"" + ((dr["t831_vreal2"] == DBNull.Value) ? "" : decimal.Parse(dr["t831_vreal2"].ToString()).ToString("N")) + "\" ");
                sb.Append("real3=\"" + ((dr["t831_vreal3"] == DBNull.Value) ? "" : decimal.Parse(dr["t831_vreal3"].ToString()).ToString("N")) + "\" ");

                string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>Interlocutor:</label>" + dr["Interlocutor"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
                if (int.Parse(dr["mostrarGC"].ToString())==1)
                    sTooltip += "<br><label style=width:70px;>G.Circulante:</label>" + dr["GestorCirculante"].ToString();
                sTooltip += "<br><label style=width:70px;>Mod.contrat.:</label>" + dr["t316_denominacion"].ToString();
                sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t820_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["anomesdecierre"].ToString() + "</td>");
                sb.Append("<td>" + dr["t827_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + ((dr["t831_flr"] == DBNull.Value) ? "" : ((DateTime)dr["t831_flr"]).ToShortDateString()) + "</td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            if (bExcede) return "EXCEDE";
            else return sb.ToString();
        }

        public static string ObtenerDialogosOCyFA(int t314_idusuario,
           Nullable<byte> t820_idalerta,
           Nullable<byte> t827_idestadodialogoalerta,
           Nullable<int> t314_idusuario_gestor,
           Nullable<int> t305_idproyectosubnodo,
           Nullable<int> t303_idnodo,
           Nullable<int> t302_idcliente,
           Nullable<int> t001_idficepi_interlocutor,
           Nullable<bool> t831_justificada,
           Nullable<int> t314_idusuario_responsable,
           Nullable<int> nMesDesde,
           Nullable<int> nMesHasta
           )
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append(@"<table id='tblDatos' class='MA' style='width: 960px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                            <col style='width:120px;' />
                            <col style='width:60px;' />
                            <col style='width:140px;' />
                            <col style='width:135px;' />
                            <col style='width:110px;' />
	                        <col style='width:100px;' />
                            <col style='width:100px;' />
                            <col style='width:20px;' />
                            <col style='width:120px;' />
                            <col style='width:25px;' />
                        </colgroup>");

            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerDialogosOCyFA(null, t314_idusuario,
                                t820_idalerta,
                                t827_idestadodialogoalerta,
                                t314_idusuario_gestor,
                                t305_idproyectosubnodo,
                                t303_idnodo,
                                t302_idcliente,
                                t001_idficepi_interlocutor,
                                t831_justificada,
                                t314_idusuario_responsable,
                                nMesDesde,
                                nMesHasta
                                );

            while (dr.Read())
            {
                sb.Append("<tr bd='' ");
                sb.Append("id='" + dr["t831_iddialogoalerta"].ToString() + "' ");

                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["Responsable"].ToString()) + "\" ");
                sb.Append("interlocutor=\"" + Utilidades.escape(dr["Interlocutor"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString()) + "\" ");
                sb.Append("anomesdecierre=\"" + Utilidades.escape(dr["anomesdecierre"].ToString()) + "\" ");
                sb.Append("gestorcierre=\"" + Utilidades.escape(dr["GestorCierre"].ToString()) + "\" ");
                sb.Append("fcierre=\"" + ((dr["t831_fcierre"] != DBNull.Value) ? ((DateTime)dr["t831_fcierre"]).ToString().Substring(0, ((DateTime)dr["t831_fcierre"]).ToString().Length - 3) : "") + "\" ");
                sb.Append("bjust=\"" + ((dr["t831_justificada"] != DBNull.Value && (bool)dr["t831_justificada"]) ? "1" : "0") + "\" ");

                string sTooltip = "<label style=width:70px;>Diálogo:</label>" + ((int)dr["t831_iddialogoalerta"]).ToString("#,###") + "<br><label style=width:70px;>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>Interlocutor:</label>" + dr["Interlocutor"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
                if (dr["GestorCierre"].ToString() != "")
                {
                    sTooltip += "<br><label style=width:70px;>Gestor:</label>" + dr["GestorCierre"].ToString() + "<br><label style=width:70px;>F. Cierre:</label>" + ((dr["t831_fcierre"] != DBNull.Value) ? ((DateTime)dr["t831_fcierre"]).ToString().Substring(0, ((DateTime)dr["t831_fcierre"]).ToString().Length - 3) : "");
                }
                sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");
                sb.Append(">");

                sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t820_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["anomesdecierre"].ToString() + "</td>");
                sb.Append("<td>" + dr["t827_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t831_causamotivoOC"].ToString() + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td>" + dr["t831_accionesacordadas"].ToString() + "</td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        #region DOCUMENTOS

        public static string ObtenerDocumentos(string sTipo, int IdElem, string sPermiso)
        {
            StringBuilder sb = new StringBuilder();
            string sTabla = "", sNomArchivo = "", sKey = "", sPathImg = "../../../../images/", sLenColAutor = "100", sAnchoTabla = "850";
            string sMano = "MA";
            bool bModificable = false;

            try
            {
                SqlDataReader dr = SUPER.Capa_Datos.DOCDIALOGO.CatalogoDocs(IdElem);
                sTabla = "t837";
                sKey = "t837_id";

                if (sPermiso == "R")//Modo lectura
                {
                    #region Modo Lectura
                    sb.Append("<table id='tblDocumentos' class='texto MANO' style='width: " + sAnchoTabla + "px;' cellpadding='0' cellspacing='0' border='0'>");
                    sb.Append("<colgroup><col style='width:290px;' /><col style='width:235px;' /><col style='width:225px;' /><col style='width:" + sLenColAutor + "px;' /></colgroup>");
                    sb.Append("<tbody>");
                    while (dr.Read())
                    {
                        sNomArchivo = dr[sTabla + "_nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                        sb.Append("<tr style='height:20px;' id='" + dr[sKey].ToString() + "'");
                        sb.Append(" onclick='mm(event);' sTipo='" + sTipo + "' sAutor='" + dr["t001_idficepi_autor"].ToString() + "' onmouseover='TTip(event)'>");
                        sb.Append("<td style='padding-left:5px;'><nobr class='NBR' style='width:280px'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");
                        if (sNomArchivo == "")
                            sb.Append("<td></td>");
                        else
                        {
                            sb.Append("<td><img src='" + sPathImg + "imgDescarga.gif' width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                            sb.Append("&nbsp;<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
                        }

                        if (dr[sTabla + "_weblink"].ToString() == "")
                            sb.Append("<td></td>");
                        else
                        {
                            string sHTTP = "";
                            if (dr[sTabla + "_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                            sb.Append("<td><a href='" + sHTTP + dr[sTabla + "_weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr[sTabla + "_weblink"].ToString() + "</nobr></a></td>");
                        }
                        sb.Append("<td><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
                    }
                    #endregion
                }
                else
                {
                    #region Acceso total
                    sb.Append("<table id='tblDocumentos' class='texto MANO' style='width: " + sAnchoTabla + "px;' cellpadding='0' cellspacing='0' border='0'>");
                    //sb.Append("<colgroup><col style='width:290px; background-color:Red;' /><col style='width:235px;' /><col style='width:225px; background-color:Lime;' /><col style='width:" + sLenColAutor + "px; background-color:Red;' /></colgroup>");
                    //sb.Append("<tbody>");
                    //sb.Append("<tr style='height:20px;'><td>AA</td><td>BB</td><td>CC</td><td>DD</td></tr>");

                    while (dr.Read())
                    {   //Si el archivo NO es sólo lectura, o si el usuario es el autor del archivo, o es administrador, se permite modificar.
                        if (dr["t001_idficepi_autor"].ToString() == HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString())
                            bModificable = true;
                        else
                            bModificable = false;

                        sb.Append("<tr style='height:20px;' id='" + dr[sKey].ToString() + "' onclick='mm(event);' sTipo='" + sTipo + "' sAutor='" + dr["t001_idficepi_autor"].ToString() + "' onmouseover='TTip(event)'>");

                        if (bModificable)
                            sb.Append("<td class='" + sMano + "' style='width:290px;' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR' style='width:280px'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");
                        else
                            sb.Append("<td style='width:290px;'><nobr class='NBR' style='width:280px'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");

                        if (dr[sTabla + "_nombrearchivo"].ToString() == "")
                        {
                            if (bModificable)
                                sb.Append("<td class='" + sMano + "' style='width:235px;' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                            else
                                sb.Append("<td style='width:235px;'></td>");
                        }
                        else
                        {
                            sNomArchivo = dr[sTabla + "_nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                            sb.Append("<td style='width:235px;'><img src='" + sPathImg + "imgDescarga.gif' width='16px' height='16px'");
                            sb.Append(" onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" ");
                            sb.Append(" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                            sb.Append("&nbsp;");
                            if (bModificable)
                            {
                                sb.Append("<nobr class='NBR " + sMano + "' style='width:205px;' ");
                                sb.Append(" ondblclick=\"modificarDoc(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                            }
                            else
                                sb.Append("<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
                        }

                        if (dr[sTabla + "_weblink"].ToString() == "")
                        {
                            if (bModificable)
                                sb.Append("<td class='" + sMano + "' style='width:225px;' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                            else
                                sb.Append("<td style='width:225px;'></td>");
                        }
                        else
                        {
                            string sHTTP = "";
                            if (dr[sTabla + "_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                            sb.Append("<td style='width:225px;'><a href='" + sHTTP + dr[sTabla + "_weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr[sTabla + "_weblink"].ToString() + "</nobr></a></td>");
                        }
                        if (bModificable)
                            sb.Append("<td class='" + sMano + "' style='width:" + sLenColAutor + "px;' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
                        else
                            sb.Append("<td style='width:" + sLenColAutor + "px;'><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
                    }

                    #endregion
                }
                dr.Close();
                dr.Dispose();
                //sb.Append("</tbody>");
                sb.Append("</table>");
            }
            catch (Exception ex)
            {
                sb.Append("Error@#@" + Errores.mostrarError("Error al obtener documentos", ex));
            }
            return sb.ToString();
        }
        public static string EliminarDocumentos(string strIdsDocs)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion
            try
            {
                #region eliminar documentos
                if (strIdsDocs != "")
                {
                    string[] aDocs = Regex.Split(strIdsDocs, "##");

                    foreach (string oDoc in aDocs)
                    {
                        SUPER.Capa_Datos.DOCDIALOGO.Delete(tr, int.Parse(oDoc));
                    }
                }
                #endregion

                Conexion.CommitTransaccion(tr);
                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }
        public static int NumDocs(int t831_iddialogoalerta)
        {
            return SUPER.Capa_Datos.DIALOGOALERTAS.NumDocs(null, t831_iddialogoalerta);
        }

        #endregion

        public static void crearDialogo(byte t820_idalerta, 
            Nullable<int> t831_anomesdecierre,
            string t832_texto,
            int t305_idproyectosubnodo,
            string t831_entepromotor,
            Nullable<DateTime> t831_flr
            )
        {
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw ex;
            }
            #endregion

            try
            {
                int nDialogo = SUPER.Capa_Datos.DIALOGOALERTAS.CrearDialogo(tr, t305_idproyectosubnodo,
                                t820_idalerta, t831_entepromotor, t831_anomesdecierre,
                                (int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"], t831_flr);

                TEXTODIALOGOALERTAS.InsertarMensaje(tr, nDialogo,
                            (int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"],
                            Utilidades.unescape(t832_texto),
                            "I",
                            (t831_entepromotor == "R") ? true : false);

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }


        public static DIALOGOALERTAS CountDialogosAbiertos(int t305_idproyectosubnodo, int t001_idficepi)
        {
            DIALOGOALERTAS o = new DIALOGOALERTAS();

            SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.CountDialogosAbiertos(null, t305_idproyectosubnodo, t001_idficepi);
            if (dr.Read())
            {
                if (dr["tiene_alertas"] != DBNull.Value)
                    o.bTieneAlertas = (bool)dr["tiene_alertas"];
                if (dr["esInterlocutor"] != DBNull.Value)
                    o.bUsuarioEsInterlocutor = (bool)dr["esInterlocutor"];
                if (dr["Abiertos"] != DBNull.Value)
                    o.nDialogosAbiertos = int.Parse(dr["Abiertos"].ToString());
                if (dr["PenLeerInterlocutor"] != DBNull.Value)
                    o.nPendienteLeerInterlocutor = int.Parse(dr["PenLeerInterlocutor"].ToString());
                if (dr["PenResponderInterlocutor"] != DBNull.Value)
                    o.nPendienteResponderInterlocutor = int.Parse(dr["PenResponderInterlocutor"].ToString());
                if (dr["PenLeerGestor"] != DBNull.Value)
                    o.nPendienteLeerGestor = int.Parse(dr["PenLeerGestor"].ToString());
                if (dr["PenResponderGestor"] != DBNull.Value)
                    o.nPendienteResponderGestor = int.Parse(dr["PenResponderGestor"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de DIALOGOALERTAS"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static SqlDataReader ObtenerDatosDialogoCerrado(int t831_iddialogoalerta)
        {
            return SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerDatosDialogoCerrado(null, t831_iddialogoalerta);
        }

        #endregion

    }
}
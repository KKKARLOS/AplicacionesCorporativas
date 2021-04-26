using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Tarea_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, gsAcceso, gsModo, strIdCliente, sLectura, sNodo = "", sRecPST = "F", gsNumEmpleadosNodo = "0";
    public int nIdTarea = 0, nPSN = 0, nModoFact = 0, nPE, nCR, nIdPT, nIdFase, nIdActividad;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaRecursos, strArrayRecursos, strTablaAECR, strArrayVAE, strArrayOrigenes, strTablaPoolGF, strTablaHTML;
    public string strTablaAET  = "<table id='tblAET'></table>";
    public string strTablaAEPT = "<table id='tblAEPT'></table>";
    public string strTablaCamposPT = "<table id='tblCamposPT'></table>";
    public string strTablaCampoValor = "<table id='tblCampos' style='width: 550px;' mantenimiento='1'><colgroup><col style='width:20px' /><col style='width:430px' /><col style='width:100px' /></colgroup></table>";
    //private cLog mLog;
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtDesTarea.Attributes["maxlength"] = "100";

        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

        string sIdNodo = "";
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            //mLog = new cLog("Tarea.txt");
            //mLog.put("Entro en Page_Load");
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            sErrores = "";
            strTablaRecursos = "<table id='tblAsignados'></table>";
            strTablaPoolGF = "<table id='tblPoolGF'></table>";
            strArrayRecursos = "";
            strTablaAECR = "";
            strArrayVAE = "";
            strArrayOrigenes = "";
            gsModo = "";
            sLectura = "false";
            try
            {
                Utilidades.SetEventosFecha(this.txtValIni);
                Utilidades.SetEventosFecha(this.txtValFin);
                Utilidades.SetEventosFecha(this.txtPLIni);
                Utilidades.SetEventosFecha(this.txtPLFin);
                Utilidades.SetEventosFecha(this.txtPRFin);

                Utilidades.SetEventosFecha(this.txtFIPRes);
                Utilidades.SetEventosFecha(this.txtFFPRes);

                sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                this.chkHeredaCR.Text = " " + Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                rdbAmbito.Items[1].Text = sNodo + "  ";
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener las descripciones de la estructura", ex);
            }

            if (Request.QueryString["sm"] != null)//sModo
            {
                gsModo = Utilidades.decodpar(Request.QueryString["sm"].ToString());
            }
            if (Request.QueryString["es"] != null)//Estr
            {
                this.hdnEstr.Text = Utilidades.decodpar(Request.QueryString["es"].ToString());
            }
            if (Request.QueryString["o"] != null)//origen
            {
                this.hdnOrigen.Text = Utilidades.decodpar(Request.QueryString["o"].ToString());
            }
            if (Request.QueryString["cr"] == null)//nCR
            {
                if (Request.Form["nCR"] != null)
                    if (Request.Form["nCR"] != "" && Request.Form["nCR"].ToString() != "undefined")
                        sIdNodo = Request.Form["nCR"].ToString();
            }
            else
            {
                sIdNodo = Utilidades.decodpar(Request.QueryString["cr"].ToString());
            }
            if (Request.QueryString["t"] != null || Request.Form["nIdTarea"] != null)//nIdTarea
            {
                string sIdTarea = "";
                if (Request.Form["nIdTarea"] != null)
                    sIdTarea = Request.Form["nIdTarea"].ToString();
                else
                    sIdTarea = Utilidades.decodpar(Request.QueryString["t"].ToString());

                sIdTarea = sIdTarea.Replace(".", "");
                if (sIdTarea != "")
                {
                    try { nIdTarea = int.Parse(sIdTarea); }
                    catch (Exception)
                    {
                        sErrores += Errores.mostrarError("El código de tarea no es numérico.");
                        return;
                    }
                    if (gsAcceso != "" && gsAcceso != null)
                        this.hdnAcceso.Text = gsAcceso;
                    else
                    {
                        if (Request.Form["Permiso"] != null)
                            this.hdnAcceso.Text = Request.Form["Permiso"].ToString();
                        else
                            this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["pm"].ToString());//Permiso
                    }
                    try
                    {
                        //

                        bool bRes = ObtenerDatosTarea();
                        if (!bRes) return;
                        else
                        {
                            if (sIdNodo == "")
                                sIdNodo = this.hdnCRActual.Value;

                            ObtenerModosFacturacion();
                            cboModoFacturacion.SelectedValue = nModoFact.ToString();

                        }

                    }
                    catch (Exception ex)
                    {
                        sErrores += Errores.mostrarError("Error al obtener los datos de la tarea o la tarea no existe", ex);
                    }
                    try
                    {
                        ObtenerDatosIAPTarea();
                    }
                    catch (NullReferenceException)
                    {
                        //No hay imputaciones realizadas en IAP para la tarea.
                    }
                    catch (Exception ex)
                    {
                        sErrores += Errores.mostrarError("Error al obtener los datos de IAP de la tarea " + nIdTarea.ToString(), ex);
                    }

                    try
                    {
                        //CrearCalendario();
                        ObtenerTarifas(nPE);
                        //ObtenerRecursosAsociados();
                        ObtenerAtributosEstadisticosCR(sIdNodo);
                        ObtenerValoresAtributosEstadisticosCR(sIdNodo);
                    }
                    catch (Exception ex)
                    {
                        sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
                    }
                    if (this.hdnAcceso.Text == "R")
                    {
                        ModoLectura.Poner(this.Controls);
                        //sLectura = "true";
                    }
                }
            }
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad = "", sCad2 = "", sCad3 = "", sCad4 = "", sCad5 = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                //   Basicos,  Tarea,   Recursos, PoolGF,   MensGen,   OTC,      AE,  Campos,      Notas,   Control, Apertura,   Cierre
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12]);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("tarearecurso"):
                sResultado += ObtenerDatosTareaRecurso(aArgs[1], aArgs[2]);
                break;
            case ("documentos"):
                string sModoAcceso = "W", sEstadoProyecto = "A";
                //sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), "W", "A");
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
                break;
            case ("getDatosProy"):
            case ("getDatosProy2"):
                sResultado += getDatosProy(aArgs[1]);
                break;
            case ("tarifas"):
                sResultado += ObtenerTarifas2(aArgs[1]);
                break;
            case ("borrar"):
                sResultado += Borrar(aArgs[1]);
                break;
            case ("aept"):
                //sResultado += "OK@#@" + ObtenerAtributosEstadisticosPT2(int.Parse(aArgs[1]));
                //Los AE del PT los cargo solo cuando acceda a la pestaña
                sResultado += "OK@#@";
                sResultado += "@#@" + ObtenerPST_PT(int.Parse(aArgs[1]));
                break;
            case ("aept2"):
                sResultado += "OK@#@" + ObtenerAtributosEstadisticosPT2(int.Parse(aArgs[1]));
                sResultado += "@#@" + ObtenerPST_PT(int.Parse(aArgs[1]));
                sResultado += "@#@" + ObtenerAtributosEstadisticosCR(aArgs[2]);
                sResultado += "@#@" + ObtenerValoresAtributosEstadisticosCR(aArgs[2]);
                break;
            case ("getRecursos"):
                bool bMostrarBajas = false;
                if (aArgs[3] == "S") bMostrarBajas = true;
                sResultado += "OK@#@" + ObtenerRecursosAsociados(aArgs[1], aArgs[2], bMostrarBajas);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://PROFESIONALES por defecto no mostramos los de baja
                        sCad = ObtenerRecursosAsociados(aArgs[2], aArgs[4], false);
                        if (sCad.IndexOf("Error@#@") >= 0)
                            sResultado += sCad;
                        else
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 2://AVANZADO
                        sCad2 = "<table id='tblAEPT' class='texto' style='width:330px;'><colgroup><col style='width:15px' /><col style='width:215px' /><col style='width:100px' /></colgroup></table>";
                        sCad3 = "<table id='tblAET' class='texto MM' style='width: 330px;' mantenimiento='1'><colgroup><col style='width:10px' /><col style='width:15px' /><col style='width:205px' /><col style='width:100px' /></colgroup></table>";
                        sCad4 = "";
                        sCad5 = ObtenerValoresAtributosEstadisticosCR(aArgs[4]);
                        sCad = ObtenerAtributosEstadisticosCR(aArgs[4]);
                        //ObtenerValoresAtributosEstadisticosCR();
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else
                        {
                            if (aArgs[3] != "")
                                sCad2 = ObtenerAtributosEstadisticosPT2(int.Parse(aArgs[3]));
                            if (sCad2.IndexOf("Error@#@") >= 0) sResultado += sCad2;
                            else
                            {
                                if (aArgs[2] != "")
                                {
                                    //if ((aArgs[2]) == "0" || (aArgs[2]) == "")
                                    //    sCad3 = "";
                                    //else
                                    //{
                                    if (aArgs[2] != "0" && aArgs[2] != "")
                                    {
                                        sCad3 = ObtenerAtributosEstadisticosTarea(aArgs[2]);
                                        if (sCad3.IndexOf("Error@#@") >= 0) sResultado += sCad3;
                                        else
                                        {
                                            sCad4 = ObtenerOTC(aArgs[2]);
                                            if (sCad4.IndexOf("Error@#@") >= 0) sResultado += sCad4;
                                        }
                                    }
                                }
                            }
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad + "///" + sCad2 + "///" + sCad3 + "///" + sCad4 + "///" + sCad5;
                        }
                        break;
                    case 3://NOTAS
                        sCad = ObtenerNotas(aArgs[2]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 4://CONTROL
                        sCad = ObtenerControl(aArgs[2]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 5://DOCUMENTACION
                        sCad = Utilidades.ObtenerDocumentos("T", int.Parse(aArgs[2]), aArgs[5], aArgs[6]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else
                        {
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        }
                        break;
                }
                break;
            case ("getDatosPestanaProf"):
                //sResultado += "OK@#@" + aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera subpestaña se carga directamente al cargar la pestña principal
                        break;
                    case 1://Pool
                        sCad = ObtenerPoolGF(aArgs[3], aArgs[4], aArgs[5], aArgs[2]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        //11/10/2001 Además devolvemos el nº de empleados del Nodo
                        int iNumEmp = NODO.GetNumEmpleados(null, int.Parse(aArgs[6]));
                        sResultado += "@#@" + iNumEmp.ToString("#,###") + ".";
                        break;
                }
                break;
            case ("getDatosPestanaAvan"):
                //sResultado += "OK@#@" + aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera subpestaña se carga directamente al cargar la pestña principal
                        break;
                    case 1://Campos
                        sCad = ObtenerCamposValor(aArgs[2]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;                        

                        //Campos heredados del PT
                        sCad2 = ObtenerCamposPT(int.Parse(aArgs[3]));
                        int pos = sCad2.IndexOf("///") + 3;
                        var listaPTs = sCad2.Substring(pos, sCad2.Length - pos);
                        sResultado += "///" + sCad2;
                        //El método getCamposPorAmbito necesita la lista de los campos heredados del PT para no mostrarlos en la lista de los posibles campos
                        sResultado += "@#@" + getCamposPorAmbito(99, listaPTs);
                        
                        break;
                }
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("getMF"):
                sResultado += getModosFacturacion(aArgs[1]);
                break;
            case ("getValPrevision"):
                sResultado += getValoresPrevision(int.Parse(aArgs[1]));
                break;
            case ("cargarCamposPorAmbito"):
                sResultado += "OK@#@" + getCamposPorAmbito(int.Parse(aArgs[1]), aArgs[2]);
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    protected string getValoresPrevision(int nIdTarea)
    {
        string sResul = "";
        StringBuilder strBuilder = new StringBuilder();

        try
        {
            TAREAPSP o = TAREAPSP.Obtener(null, nIdTarea);

            string sFIPL = "";
            string sFFPL = "";
            string sETPL = "";
            string sFFPR = "";
            string sETPR = "";
            string sUltCon = "";
            string sConHoras = "";

            if (o.t332_fipl != null) sFIPL = ((DateTime)o.t332_fipl).ToShortDateString();
            if (o.t332_ffpl != null) sFFPL = ((DateTime)o.t332_ffpl).ToShortDateString();
            if (o.t332_etpl > 0) sETPL = o.t332_etpl.ToString("N");

            if (o.t332_ffpr != null) sFFPR = ((DateTime)o.t332_ffpr).ToShortDateString();
            if (o.t332_etpr > 0) sETPR = o.t332_etpr.ToString("N");
            try
            {
                o = TAREAPSP.ObtenerDatosIAP(null, nIdTarea);
                sUltCon = (o.dUltimoConsumo.HasValue) ? ((DateTime)o.dUltimoConsumo).ToShortDateString() : "";
                sConHoras = o.nConsumidoHoras.ToString("N");
            }
            catch(Exception)
            {
                sUltCon = "";
                sConHoras = "";
            }

            sResul = "OK@#@" + sFIPL + "@#@" + sFFPL + "@#@" + sETPL + "@#@" + sFFPR + "@#@" + sETPR + "@#@" + sUltCon + "@#@" + sConHoras;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los valores de previsión de la tarea " + nIdTarea.ToString(), ex);
        }

        return sResul;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private bool ObtenerDatosTarea()
    {
        

        //if (nCR == 0) nCR = int.Parse(Session["NodoActivo"].ToString());
        TAREAPSP o = TAREAPSP.Obtener(null, nIdTarea);
        this.hdnCRActual.Value = o.t303_idnodo.ToString();
        this.hdnDesCRActual.Value = o.t303_denominacion;

        if (o.t332_idtarea.ToString() == "" || o.t332_idtarea.ToString() == "0")
        {
            sErrores += "La tarea " + nIdTarea.ToString("#,###") + " no existe.";
            nIdTarea = 0;
            return false;
        }
        this.hdnModoAcceso.Value = TAREAPSP.getAcceso(null, nIdTarea, int.Parse(Session["UsuarioActual"].ToString()));

        if (this.hdnAcceso.Text == "")
        {
            //this.hdnAcceso.Text = Permiso(o.t305_idproyectosubnodo.ToString(), nCR.ToString(), o.num_proyecto.ToString(), nIdTarea.ToString());
            this.hdnAcceso.Text = Permiso(o.t305_idproyectosubnodo.ToString(), o.t303_idnodo.ToString(), o.num_proyecto.ToString(), nIdTarea.ToString());
        }
        if ((this.hdnAcceso.Text == "") || (this.hdnAcceso.Text == "N"))
        {
            txtIdTarea.Text = "";
            //System.Exception objError=new Exception();
            //sErrores += Errores.mostrarError("La tarea " + nIdTarea.ToString() + " no existe o no es accesible", objError);
            //sErrores += Errores.mostrarError("La tarea " + nIdTarea.ToString() + " no existe o no es accesible");
            sErrores += "La tarea " + nIdTarea.ToString("#,###") + " está fuera de su ámbito de responsabilidad.";
            nIdTarea = 0;
            return false;
        }
        this.hdnT305IdProy.Value = o.t305_idproyectosubnodo.ToString();
        txtIdTarea.Text = o.t332_idtarea.ToString("#,###");
        //this.txtIdTarea.Enabled = false;
        this.txtCualidad.Text = o.t305_cualidad.ToString();

        txtDesTarea.Text = o.t332_destarea;
        txtDesTarea.ToolTip = o.t332_destarea;
        txtDescripcion.Text = o.t332_destarealong;
        //if (o.t332_fiv.Year > 1900) txtValIni.Text = o.t332_fiv.ToShortDateString();
        if (o.t332_fiv != null) txtValIni.Text = ((DateTime)o.t332_fiv).ToShortDateString();
        else txtValIni.Text = "";

        if (o.t332_ffv != null) txtValFin.Text = ((DateTime)o.t332_ffv).ToShortDateString();
        else txtValFin.Text = "";
        hdnEstado.Text = o.t332_estado.ToString();

        if (o.t332_cle > 0) txtCLE.Text = o.t332_cle.ToString("N");

        if (this.hdnAcceso.Text == "W")
        {
            rdbCLE.Enabled = true;
            chkFacturable.Enabled = true;
            chkAvanceAuto.Enabled = true;
        }

        if (o.t332_tipocle == "B") {
            rdbCLE.Items[0].Selected = true;
            cboInformativo.Enabled = false;
        }else{            
            rdbCLE.Items[1].Selected = true;
            cboInformativo.SelectedValue = o.t332_tipocle.ToString();
            cboInformativo.Enabled = true;

        }
        if (o.t332_fipl != null) txtPLIni.Text = ((DateTime)o.t332_fipl).ToShortDateString();
        else txtPLIni.Text = "";

        if (o.t332_ffpl != null) txtPLFin.Text = ((DateTime)o.t332_ffpl).ToShortDateString();
        else txtPLFin.Text = "";

        //if (o.t332_etpl > 0) txtPLEst.Text = o.t332_etpl.ToString("N");
        if (o.t332_etpl > 0) txtPLEst.Text = o.t332_etpl.ToString("N");
        if (o.t332_ffpr != null) txtPRFin.Text = ((DateTime)o.t332_ffpr).ToShortDateString();
        else txtPRFin.Text = "";
        //if (o.t332_etpr > 0) txtPREst.Text = o.t332_etpr.ToString("N");
        if (o.t332_etpr > 0) txtPREst.Text = o.t332_etpr.ToString("N");

        strIdCliente = o.cod_cliente.ToString();
        chkCliente.Text += o.nom_cliente;

        ObtenerOrigen(o.t353_idorigen, o.t303_idnodo.ToString());
        cboOrigen.SelectedValue = o.t353_idorigen.ToString();

        if (o.t332_facturable) chkFacturable.Checked = true;
        else chkFacturable.Checked = false;

        txtNumPE.Text = o.num_proyecto.ToString("#,###");
        txtPE.Text = o.nom_proyecto;
        txtPT.Text = o.t331_despt;
        txtPT.ToolTip = o.t331_despt;
        txtFase.Text = o.t334_desfase;
        txtFase.ToolTip = o.t334_desfase;
        txtActividad.Text = o.t335_desactividad;
        txtActividad.ToolTip = o.t335_desactividad;
        nPE = o.num_proyecto;

        txtIncidencia.Text = o.t332_incidencia;

        txtPresupuesto.Text = 0.ToString("N");
        this.hdnNivelPresupuesto.Value = o.t305_nivelpresupuesto.ToString();
        if (o.t305_nivelpresupuesto == "T")
        {
            txtPresupuesto.Text = o.t332_presupuesto.ToString("N");
            txtAvanReal.Text = o.t332_avance.ToString("N");
            if (o.t332_avanceauto) chkAvanceAuto.Checked = true;
            else chkAvanceAuto.Checked = false;
        }

        hdnOrden.Text = o.t332_orden.ToString();
        hdnIDPT.Text = o.t331_idpt.ToString();

        nIdPT = o.t331_idpt;
        nIdFase = o.t334_idfase;
        nIdActividad = o.t335_idactividad;

        if (o.t334_idfase == 0)
            hdnIDFase.Text = "";
        else
            hdnIDFase.Text = o.t334_idfase.ToString();
        if (o.t335_idactividad == 0)
            hdnIDAct.Text = "";
        else
            hdnIDAct.Text = o.t335_idactividad.ToString();
        //Miro si la tarea cuelga de una actividad y/o fase y en ese caso si es la única
        //Esto lo necesitaremos para no permitir cambiar la cabecera a esa tarea
        o.bUnicaEnActividad = TAREAPSP.bHijoUnico(null, o.t334_idfase, o.t335_idactividad);
        if (o.bUnicaEnActividad) hdnUnicaEnActividad.Text = "T";
        else hdnUnicaEnActividad.Text = "F";

       
        if (o.t332_impiap) chkImpIAP.Checked = true;
        else chkImpIAP.Checked = false;

        if (o.t332_heredanodo) chkHeredaCR.Checked = true;
        else
        {
            chkHeredaCR.Checked = false;
            chkHeredaCR.ToolTip = "Asigna de forma automática a todos los profesionales presentes y futuros del " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + ". La opción está deshabilitada por bloqueo desde el proyecto económico.";
        }

        //Si el PT o la Fase o la Actividad ya heredan recursos del CR el campo debe quedar desactivado en la tarea
        if (o.heredaCR)
        {
            chkHeredaCR.Checked = true;
            chkHeredaCR.Enabled = false;
        }
        //Si el proyectosubnodo no admite recursos desde PST el campo debe estar deshabilitado
        if (!o.t305_admiterecursospst)
        {
            chkHeredaCR.Enabled = false;
            imgDelBajaPE.Visible = false;
        }
        else
            sRecPST = "T";

        if (!o.t305_avisorecursopst)
        {
            imgCorreo.ImageUrl = "../../../Images/imgCorreoBloqueado.gif";
            imgCorreo.ToolTip = "La notificación de asignación de profesionales a la tarea, está bloqueada por parametrización a nivel de proyecto económico.";
        }
        if (o.t332_heredaproyeco) chkHeredaPE.Checked = true;
        else chkHeredaPE.Checked = false;

        //Si el PT o la Fase o la Actividad ya heredan recursos del PE el campo debe quedar desactivado en la tarea
        if (o.heredaPE)
        {
            chkHeredaPE.Checked = true;
            chkHeredaPE.Enabled = false;
        }else
        {
            chkHeredaPE.Checked = false;
            chkHeredaPE.Enabled = true;
        }
        //if (o.t332_finalizada) chkFinalizada.Checked = true;
        if (o.t332_notificable) chkNotificable.Checked = true;
        else chkNotificable.Checked = false;

        if (o.t332_notif_prof) chkNotifProf.Checked = true;
        else chkNotifProf.Checked = false;
        //Acceso desde IAP
        cboAccesoIAP.Text = o.t332_acceso_bitacora_iap.ToString();

        this.txtIndGen.Text = o.t332_mensaje;
        //Estado del proyecto económico
        this.hdnEstProy.Value = o.t301_estado;
        switch (o.t301_estado.ToString())
        {
            case "A":
                imgEstProy.ImageUrl = "~/images/imgIconoProyAbierto.gif";
                imgEstProy.Attributes.Add("title", "Proyecto abierto");
                break;
            case "C":
                imgEstProy.ImageUrl = "~/images/imgIconoProyCerrado.gif";
                imgEstProy.Attributes.Add("title", "Proyecto cerrado");
                break;
            case "P":
                imgEstProy.ImageUrl = "~/images/imgIconoProyPresup.gif";
                imgEstProy.Attributes.Add("title", "Proyecto presupuestado");
                break;
            case "H":
                imgEstProy.ImageUrl = "~/images/imgIconoProyHistorico.gif";
                imgEstProy.Attributes.Add("title", "Proyecto histórico");
                break;
        }
        nModoFact = o.t324_idmodofact;
        nPSN = o.t305_idproyectosubnodo;
        hdnEsReplicable.Value = (o.t301_esreplicable) ? "1" : "0";

        if (o.t305_opd)
        {
            imgHistorial.Style.Add("visibility", "hidden");
            trUM.Style.Add("visibility", "hidden");
            btnAuditoria.Style.Add("visibility", "hidden");
        }else
        {
            imgHistorial.Style.Remove("visibility");
            trUM.Style.Remove("visibility");
            btnAuditoria.Style.Remove("visibility");
        }

        if (o.t332_horascomplementarias) chkHorasComplementarias.Checked = true;
        else chkHorasComplementarias.Checked = false;

        return true;
    }
    private void ObtenerDatosIAPTarea()
    {
        TAREAPSP o = TAREAPSP.ObtenerDatosIAP(null, nIdTarea);

        txtPriCon.Text = (o.dPrimerConsumo.HasValue) ? ((DateTime)o.dPrimerConsumo).ToShortDateString() : "";
        txtUltCon.Text = (o.dUltimoConsumo.HasValue) ? ((DateTime)o.dUltimoConsumo).ToShortDateString() : "";
        txtFinEst.Text = (o.dFinEstimado.HasValue) ? ((DateTime)o.dFinEstimado).ToShortDateString() : "";
        txtTotEst.Text = o.nTotalEstimado.ToString("N");

        if (txtPREst.Text != "" && double.Parse(txtPREst.Text) < o.nTotalEstimado)
        {
            txtPREst.CssClass = "txtNumMR";
            txtTotEst.CssClass = "txtNumMR";
        }

        txtConHor.Text = o.nConsumidoHoras.ToString("N");

        double dAux = 0;

        if (txtPREst.Text != "") dAux = double.Parse(txtPREst.Text);

        if (o.nConsumidoHoras > 0 && o.nConsumidoHoras > dAux)
            txtConHor.CssClass = "txtNumMR";

        txtConJor.Text = o.nConsumidoJornadas.ToString("N");
        if (o.nTotalEstimado != 0)
            txtPteEst.Text = o.nPendienteEstimado.ToString("N");

        if (o.nConsumidoHoras > 0 && txtPREst.Text != "")
        {
            txtAvanTeo.Text = ((o.nConsumidoHoras * 100 / double.Parse(txtPREst.Text))).ToString("N");
        }
        if (o.nConsumidoHoras > 0 && o.nTotalEstimado > 0)
        {
            txtAvanEst.Text = ((o.nConsumidoHoras * 100 / o.nTotalEstimado)).ToString("N");
        }
    }

    private string ObtenerNotas(string sIdTarea)
    {
        string sRes = "";
        if (sIdTarea != "")
        {
            int nIdTarea = int.Parse(sIdTarea);
            TAREAPSP o = TAREAPSP.ObtenerNotas(null, nIdTarea);

            if (o.t332_notasiap) sRes = "S##";
            else sRes = "N##";
            sRes += Utilidades.escape(o.t332_notas1) + "##";
            sRes += Utilidades.escape(o.t332_notas2) + "##";
            sRes += Utilidades.escape(o.t332_notas3) + "##";
            sRes += Utilidades.escape(o.t332_notas4);
        }
        return sRes;
    }
    private string ObtenerControl(string sIdTarea)
    {
        string sRes = "", sAux;
        if (sIdTarea != "")
        {
            int nIdTarea = int.Parse(sIdTarea);
            TAREAPSP o = TAREAPSP.ObtenerControl(null, nIdTarea);

            sRes = o.t332_observaciones + "##";

            sRes += o.t314_idusuario_promotor.ToString("#,###.##") + "##";
            sRes += o.sPromotor + "##";
            sAux = o.t332_falta.ToString();
            sAux = sAux.Substring(0, sAux.Length - 3);
            sRes += sAux + "##";

            sRes += o.t314_idusuario_ultmodif.ToString("#,###.##") + "##";
            sRes += o.sModificador + "##";
            sAux = o.t332_fultmodif.ToString();
            sAux = sAux.Substring(0, sAux.Length - 3);
            sRes += sAux + "##";

            sRes += o.t314_idusuario_fin.ToString("#,###.##") + "##";
            sRes += o.sFinalizador + "##";
            //if (o.t332_ffin.ToString() == "01/01/1900 0:00:00") sRes += "##";
            if (o.t332_ffin.ToString() == "")
                sRes += "##";
            else
            {
                sAux = o.t332_ffin.ToString();
                sAux = sAux.Substring(0, sAux.Length - 3);
                sRes += sAux + "##";
            }

            sRes += o.t314_idusuario_cierre.ToString("#,###.##") + "##";
            sRes += o.sCerrador + "##";
            //if (o.t332_fcierre.ToString() == "01/01/1900 0:00:00") sRes += "##";
            if (o.t332_fcierre.ToString() == "")
                sRes += "##";
            else
            {
                sAux = o.t332_fcierre.ToString();
                sAux = sAux.Substring(0, sAux.Length - 3);
                sRes += sAux + "##";
            }

        }
        return sRes;
    }
    private string ObtenerOTC(string sIdTarea)
    {
        string sRes = "";
        if (sIdTarea != "")
        {
            int nIdTarea = int.Parse(sIdTarea);
            TAREAPSP o = TAREAPSP.ObtenerOTC(null, nIdTarea);

            sRes = o.t332_otl + "##";
            if (o.t346_idpst > 0) sRes += o.t346_idpst.ToString() + "##";
            else sRes += "##";
            sRes += o.t346_codpst + "##";
            sRes += o.t346_despst + "##";
            if (o.bOTCHeredada) sRes += "T##";
            else sRes += "F##";
            //if (o.t332_notificable) sRes += "T##";
            //else sRes += "F##";
            if (o.bOTCerror) sRes += "No coinciden la OTC de la tarea con la del Proyecto Técnico.\nPóngase en contacto con el CAU";
        }
        return sRes;
    }
    private string ObtenerRecursosAsociados(string sIdTarea, string sCodUne, bool bMostrarBajas)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";

        sb.Append("<table id='tblAsignados' class='texto MM' style='width: 360px;'>");
        sb.Append("<colgroup><col style='width:15px' /><col style='width:20px' /><col style='width:305px' /><col style='width:20px' /></colgroup>");
        sb.Append("<tbody id='tbodyDestino'>");
        try
        {
            if (sIdTarea != "")
            {
                int nIdTarea = int.Parse(sIdTarea);
                SqlDataReader dr = TareaRecurso.Catalogo(nIdTarea, bMostrarBajas);
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px' bd='' id='" + dr["t314_idusuario"].ToString() + "' ");
                    sb.Append("estado='" + dr["t336_estado"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");

                    if (this.hdnAcceso.Text == "R")
                        sb.Append(">"); // ondblclick='eliminarRecurso(this)'
                    else
                    {
                        //sb.Append("id='" + dr["t314_idusuario"].ToString()); // ondblclick='eliminarRecurso(this)'
                        sb.Append(" onclick='mm(event);mostrarDatosAsignacion(this.id);' onmousedown='mostrarDatosAsignacion(this.id);DD(event)'>");
                    }
                    sb.Append("<td></td><td></td>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    //if (dr["t336_estado"].ToString() == "1")
                    //    sb.Append("<td><img src='../../../images/imgActivoTareaOn.gif' title='Profesional activo en la tarea (le figura en su IAP)' ondblclick='setEstado(this)' style='height:16px;CURSOR: url(../../../images/imgManoAzul2.cur);'></td>");
                    //else
                    //    sb.Append("<td><img src='../../../images/imgActivoTareaOff.gif' title='Profesional inactivo en la tarea (no le figura en su IAP)' ondblclick='setEstado(this)' style='height:16px;CURSOR: url(../../../images/imgManoAzul2.cur);'></td>");
                    sb.Append("<td><nobr class='NBR W320'>" + dr["empleado"].ToString() + "</nobr></td>");


                    if (dr["t336_completado"].ToString()=="1")	
                        sb.Append("<td align='center'><img src='../../../images/imgOk.gif' /></td>");
                    else
                        sb.Append("<td><img src='../../../images/imgSeparador.gif' /></td>");
                    sb.Append("</tr>");

                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody></table>");
            strTablaRecursos = sb.ToString();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales asociados al proyecto técnico.", ex);
        }
        return sResul;
    }

    private string ObtenerAtributosEstadisticosCR(string sNodo)
    {
        StringBuilder sbuilder = new StringBuilder();

        sbuilder.Append("<table id='tblAECR' class='texto MAM' style='width: 180px;'>");
        sbuilder.Append("<colgroup><col style='width:15px;' /><col style='width:165px;' /></colgroup>");
        sbuilder.Append("<tbody>");
        if (sNodo != "")
        {
            SqlDataReader dr = AE.Catalogo(null, "", true, null, null, short.Parse(sNodo), null, "T", 4, 0);
            int i = 0;
            while (dr.Read())
            {
                string sObl = "0";
                if ((bool)dr["t341_obligatorio"]) sObl = "1";

                sbuilder.Append("<tr style='height:16px;noWrap:true;'");
                if (this.hdnAcceso.Text == "R")
                {
                    sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' cliente='" + dr["cod_cliente"].ToString() + "' obl='" + sObl + "'>");
                }
                else
                {
                    sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' cliente='" + dr["cod_cliente"].ToString() + "' obl='" + sObl + "' onclick='ms(this);' ondblclick='asociarAE(this, true)' onmousedown='DD(event);'>");
                }
                if ((bool)dr["t341_obligatorio"])
                    sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                else
                    sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                sbuilder.Append("<td><nobr class='NBR W160'>" + dr["t341_nombre"].ToString() + "</nobr></td></tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();
        }
        sbuilder.Append("</tbody>");
        sbuilder.Append("</table>");
        strTablaAECR = sbuilder.ToString();
        return sbuilder.ToString();
    }
    private string ObtenerAtributosEstadisticosTarea(string sIdTarea)
    {
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append("<table id='tblAET' class='texto MM' style='width: 330px;' mantenimiento='1'>");
        sbuilder.Append("<colgroup><col style='width:10px;' /><col style='width:15px;' /><col style='width:205px;' /><col style='width:100px;' /></colgroup>");
        sbuilder.Append("<tbody>");
        if (sIdTarea != "")
        {
            int nIdTarea = int.Parse(sIdTarea);
            SqlDataReader dr = AETAREAPSP.CatalogoByTarea(nIdTarea);
            while (dr.Read())
            {
                string sObl = "0";
                if ((bool)dr["t341_obligatorio"]) sObl = "1";

                sbuilder.Append("<tr ");
                if (this.hdnAcceso.Text == "R")
                {
                    sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl + "' bd=''>");
                    sbuilder.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    if ((bool)dr["t341_obligatorio"])
                        sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    sbuilder.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
                    sbuilder.Append("<td><NOBR class='NBR W90' title='" + dr["t340_valor"].ToString() + "'>" + dr["t340_valor"].ToString() + "</NOBR></td></tr>");
                }
                else
                {
                    sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl + "' bd='' onclick='ms(this);mostrarValoresAE(this);' onmousedown='DD(event);'>");
                    sbuilder.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    if ((bool)dr["t341_obligatorio"])
                        sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    sbuilder.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
                    sbuilder.Append("<td><NOBR class='NBR W90' title='" + dr["t340_valor"].ToString() + "'>" + dr["t340_valor"].ToString() + "</NOBR></td></tr>");
                }
            }
            dr.Close();
            dr.Dispose();
        }
        sbuilder.Append("</tbody>");
        sbuilder.Append("</table>");
        strTablaAET = sbuilder.ToString();
        return sbuilder.ToString();
    }
    private string ObtenerAtributosEstadisticosPT2(int iPT)
    {//Para recargas desde código cliente
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append("<table id='tblAEPT' class='texto' style='width: 330px;'>");
        sbuilder.Append("<colgroup><col style='width:15px' /><col style='width:215px' /><col style='width:100px' /></colgroup>");
        sbuilder.Append("<tbody>");
        SqlDataReader dr = AEPTPSP.CatalogoByPT(iPT);

        while (dr.Read())
        {
            string sObl = "0";
            if ((bool)dr["t341_obligatorio"]) sObl = "1";

            sbuilder.Append("<tr style='height:16px' id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl + "' bd=''>");
            if ((bool)dr["t341_obligatorio"])
                sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
            else
                sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

            sbuilder.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
            sbuilder.Append("<td><NOBR class='NBR W90' title='" + dr["t340_valor"].ToString() + "'>" + dr["t340_valor"].ToString() + "</NOBR></td></tr>");
        }
        dr.Close();
        dr.Dispose();
        sbuilder.Append("</tbody>");
        sbuilder.Append("</table>");

        //return "OK@#@" + sbuilder.ToString();
        return sbuilder.ToString();
    }
    private string ObtenerValoresAtributosEstadisticosCR(string sNodo)
    {
        StringBuilder sbuilder = new StringBuilder();

        sbuilder.Append(" aVAE_js = new Array();\n");
        if (sNodo != "")
        {
            SqlDataReader dr = VAE.CatalogoByUne(int.Parse(sNodo), "T");
            int i = 0;
            while (dr.Read())
            {
                sbuilder.Append("\taVAE_js[" + i.ToString() + "] = new Array(\"" + dr["t341_idae"].ToString() + "\",\"" +
                                dr["t340_idvae"].ToString() + "\",\"" + dr["t340_valor"].ToString() + "\");\n");
                i++;
            }
            dr.Close();
            dr.Dispose();
        }
        strArrayVAE = sbuilder.ToString();
        return strArrayVAE;
    }

    private string ObtenerPST_PT(int iPT)
    {//Para recargas desde código cliente
        string sRes;

        ProyTec o = ProyTec.Obtener(iPT);

        sRes = o.t346_idpst.ToString() + "##" + o.t346_codpst + "##" + o.t346_despst;

        return sRes;
    }

    private void ObtenerTarifas(int nProyEco)
    {
        cboTarifa.DataValueField = "t333_idperfilproy";
        cboTarifa.DataTextField = "t333_denominacion";
        cboTarifa.DataSource = TARIFAPROY.SelectByt301_idproyecto(nProyEco);
        cboTarifa.DataBind();
    }
    private string ObtenerTarifas2(string sProyEco)
    {
        string sResul = "";
        try
        {
            SqlDataReader dr = TARIFAPROY.SelectByt301_idproyecto(int.Parse(sProyEco));
            while (dr.Read())
            {
                sResul += dr["t333_idperfilproy"].ToString() + "##" + dr["t333_denominacion"].ToString() + "@#@";
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las tarifas.", ex);
        }
        return sResul;
    }

    private void ObtenerOrigen(short nIdOrigen, string sNodo)
    {
        bool bActivo;
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aOrigen_js = new Array();\n");

        SqlDataReader dr = TAREAORIGEN.SelectByt303_idnodo(null, short.Parse(sNodo));

        ListItem Elemento;
        string sNot = "";
        int i = 0;
        while (dr.Read())
        {
            bActivo = (bool)dr["t353_estado"];
            if (bActivo)
            {
                Elemento = new ListItem(dr["t353_desorigen"].ToString(), dr["t353_idorigen"].ToString());
                this.cboOrigen.Items.Add(Elemento);
                sNot = "0";
                if ((bool)dr["t353_notificable"]) sNot = "1";
                sbuilder.Append("\taOrigen_js[" + i.ToString() + "] = new Array(\"" + dr["t353_idorigen"].ToString() + "\",\"" + sNot + "\",\"" + dr["t353_email"].ToString() + "\");\n");
                i++;
            }
            else
            {//Para preservar el origen que tuviera la tarea aunque ese origen ya no esté activo
                if (nIdOrigen.ToString() == dr["t353_idorigen"].ToString())
                {
                    Elemento = new ListItem(dr["t353_desorigen"].ToString(), dr["t353_idorigen"].ToString());
                    this.cboOrigen.Items.Add(Elemento);
                    sNot = "0";
                    if ((bool)dr["t353_notificable"]) sNot = "1";
                    sbuilder.Append("\taOrigen_js[" + i.ToString() + "] = new Array(\"" + dr["t353_idorigen"].ToString() + "\",\"" + sNot + "\",\"" + dr["t353_email"].ToString() + "\");\n");
                    i++;
                }
            }
        }
        dr.Close();
        dr.Dispose();

        strArrayOrigenes = sbuilder.ToString();
    }

    protected string Grabar(string strDatosBasicos, string strDatosTarea, string strDatosRecursos, string strDatosPoolGF,
                            string strDatosMensGen, string strDatosOTC, string strDatosAE, string strDatosCampos, string strDatosNotas, string strDatosControl,
                            string strDatosApertura, string strDatosCierre)
    {
        string sResul = "", sEstadoAnt, sEstadoAct, sIdTareaAnterior = "", sResulMails = "", sETPR = "", sFFPR = "";
        double? fAvanceReal = null, fETPL = null, fETPR = null, fCLE = null;
        int nIdTarea, iT305IdProy, nIdPT;
        //int? nUsuFin = null;
        //int? nUsuCierre = null;
        int? nFase = null;
        int? nAct = null;
        short nOrden;
        int nCR;
        bool bHayCambioPadre = false, bDuplicar = false, bTareaNueva = false, bNotifExceso = false;
        DateTime? dValIni = null;
        DateTime? dValFin = null;
        DateTime? dPLIni = null;
        DateTime? dPLFin = null;
        DateTime? dPRFin = null;
        //DateTime? dFecFin = null;
        //DateTime? dFecCierre = null;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
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
            string[] aDatosBasicos = Regex.Split(strDatosBasicos, "##");
            if (aDatosBasicos[0] == "") nIdTarea = -1;
            else nIdTarea = int.Parse(aDatosBasicos[0]);

            //if (aDatosBasicos[1] == "") nCR = short.Parse(Session["NodoActivo"].ToString());
            //else nCR = short.Parse(aDatosBasicos[1]);
            nCR = int.Parse(aDatosBasicos[1]);

            if (aDatosBasicos[2] == "") iT305IdProy = -1;
            else iT305IdProy = int.Parse(aDatosBasicos[2]);
            if (aDatosBasicos[3] == "") nIdPT = -1;
            else nIdPT = int.Parse(aDatosBasicos[3]);
            if (aDatosBasicos[4] == "") nOrden = 1;
            else nOrden = short.Parse(aDatosBasicos[4]);
            if (aDatosBasicos[5] != "")
            {
                bDuplicar = true;
                sIdTareaAnterior = aDatosBasicos[5].Replace(".", "");
            }
            bool bNotificable = false;
            if (aDatosBasicos[6] == "1") bNotificable = true;
            bool bNotifProf = false;
            if (aDatosBasicos[7] == "1") bNotifProf = true;

            #region datos generales Tarea
            ///aDatosTarea[0] = Des. tarea;
            ///aDatosTarea[1] = Des. larga de tarea;
            ///aDatosTarea[2] = Facturable o no (1 / 0);
            ///aDatosTarea[3] = Inicio Validez;
            ///aDatosTarea[4] = Fin Validez;
            ///aDatosTarea[5] = Estado;
            ///aDatosTarea[6] = Control límite de esfuerzo;
            ///aDatosTarea[7] = Tipo CLE (Bloqueante o informativo);
            ///aDatosTarea[8] = Inicio Planificación;
            ///aDatosTarea[9] = Fin Planificación;
            ///aDatosTarea[10] = Estimación Planificación;
            ///aDatosTarea[11] = Fin Previsión;
            ///aDatosTarea[12] = Estimación Previsión;
            ///aDatosTarea[13] = Presupuesto;
            ///aDatosTarea[14] = ID Actividad;
            ///aDatosTarea[15] = Avance;
            ///aDatosTarea[16] = Avance Automático;
            ///aDatosTarea[17] = Num Proyecto económico;
            ///aDatosTarea[18] = CR
            ///aDatosTarea[19] = Notificar apertura tarea
            ///aDatosTarea[20] = Notificar cierre tarea
            ///aDatosTarea[21] = Código de fase
            ///aDatosTarea[22] = Hay cambio de padre (T/F) -> hay que recalcular orden
            ///aDatosTarea[23] = Notificar cambio de estado
            ///aDatosTarea[24] = estado anterior
            ///aDatosTarea[25] = estado actual
            ///aDatosTarea[26] = chkImpIAP, para permitir imputar en IAP aunque la tarea esté Finalizada o Cerrada
            ///aDatosTarea[27] = chkHeredaCR
            ///aDatosTarea[28] = chkHeredaPE
            ///aDatosTarea[29] = cboAccesoIAP Acceso a bitacora de tarea desde IAP
            ///aDatosTarea[30] = cboModoFacturacion
            ///
            if (strDatosTarea != "")
            {
                string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
                if (aDatosTarea[6] == "") aDatosTarea[6] = "0";
                if (aDatosTarea[10] == "") aDatosTarea[10] = "0";
                if (aDatosTarea[12] == "") aDatosTarea[12] = "0";
                if (aDatosTarea[22] == "T") bHayCambioPadre = true;
                else bHayCambioPadre = false;
                sEstadoAnt = aDatosTarea[24];
                sEstadoAct = aDatosTarea[25];

                if (aDatosTarea[3] != "") dValIni = DateTime.Parse(aDatosTarea[3]);
                if (aDatosTarea[4] != "") dValFin = DateTime.Parse(aDatosTarea[4]);
                if (aDatosTarea[8] != "") dPLIni = DateTime.Parse(aDatosTarea[8]);
                if (aDatosTarea[9] != "") dPLFin = DateTime.Parse(aDatosTarea[9]);
                if (aDatosTarea[11] != "") dPRFin = DateTime.Parse(aDatosTarea[11]);

                fETPL = double.Parse((aDatosTarea[10] == "") ? "0" : aDatosTarea[10]);
                fETPR = double.Parse((aDatosTarea[12] == "") ? "0" : aDatosTarea[12]);
                fCLE = double.Parse((aDatosTarea[6] == "") ? "0" : aDatosTarea[6]);

                //if (sEstadoAct == "3")
                //{
                //    if (sEstadoAnt != sEstadoAct)
                //    {
                //        nUsuFin = int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString());
                //        dFecFin = DateTime.Now;
                //    }
                //}
                //else
                //{
                //    if (sEstadoAct == "4" || sEstadoAct == "5")
                //    {
                //        if (sEstadoAnt != sEstadoAct)
                //        {
                //            nUsuCierre = int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString());
                //            dFecCierre = DateTime.Now;
                //        }
                //    }
                //}

                bool bFacturable = false;
                if (aDatosTarea[2] == "1") bFacturable = true;

                if ((aDatosTarea[14] != "") && (aDatosTarea[14] != "-1")) nAct = int.Parse(aDatosTarea[14]);

                if ((aDatosTarea[21] != "") && (aDatosTarea[21] != "-1")) nFase = int.Parse(aDatosTarea[21]);
                //Si el avance es automatico el avance real debe ser null
                bool bAvanceAuto = false;
                if (aDatosTarea[16] == "1") bAvanceAuto = true;

                bool bImpIAP = (aDatosTarea[26] == "1") ? true : false;
                bool bHeredaNodo = (aDatosTarea[27] == "1") ? true : false;
                bool bHeredaPE = (aDatosTarea[28] == "1") ? true : false;

                if (bAvanceAuto)
                {
                    fAvanceReal = null;
                }
                else
                {
                    if (aDatosTarea[15] != "") fAvanceReal = double.Parse(aDatosTarea[15]);
                }

                bool bHorasComplementarias = false;
                if (aDatosTarea[31] == "1") bHorasComplementarias = true;

                //Si entro desde la estructura updateo registro existente, sino hay que insertar
                if (nIdTarea > 0)
                {
                    if (bHayCambioPadre)
                    {
                        nOrden = TAREAPSP.flCalcularOrden2(null, nIdPT);
                    }
                    TAREAPSP.Update_P0(tr,
                        nIdTarea, //ID TAREA;
                        Utilidades.unescape(aDatosTarea[0]), //Des. tarea;
                        Utilidades.unescape(aDatosTarea[1]), //Des. larga de tarea;
                        nIdPT,
                        nAct, //IDACTIVIDAD
                        int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()),
                        DateTime.Now,
                        dValIni,
                        dValFin,
                        byte.Parse(aDatosTarea[5]), //Estado
                        dPLIni,
                        dPLFin,
                        fETPL,//ETPL
                        dPRFin,
                        fETPR,//ETPR
                        fCLE, //CLE
                        aDatosTarea[7],//TIPO CLE
                        nOrden,//21 orden
                        bFacturable,
                        decimal.Parse(aDatosTarea[13]),//PRESUPUESTO
                        bNotificable,
                        fAvanceReal,
                        bAvanceAuto,
                        //nUsuFin, dFecFin, nUsuCierre, dFecCierre, 
                        bImpIAP, bHeredaNodo,
                        bHeredaPE, bNotifProf, aDatosTarea[29],
                        //(aDatosTarea[30] == "0") ? null : (int?)int.Parse(aDatosTarea[30]));
                        (aDatosTarea[30] == "" || aDatosTarea[30] == "0") ? null : (int?)int.Parse(aDatosTarea[30]),
                        bHorasComplementarias);

                    if ((sEstadoAct == "3" || sEstadoAct == "4" || sEstadoAct == "5") && sEstadoAnt != sEstadoAct)
                    {
                        TAREAPSP oTarea = TAREAPSP.Obtener(tr, nIdTarea);
                        sETPR = oTarea.t332_etpr.ToString("N");
                        sFFPR = (oTarea.t332_ffpr.HasValue) ? ((DateTime)oTarea.t332_ffpr).ToShortDateString() : "";
                    }

                }
                else
                {
                    bTareaNueva = true;
                    //if (nCR == 0) nCR = short.Parse(Session["NodoActivo"].ToString());
                    nPE = int.Parse(aDatosTarea[17]);
                    nOrden = TAREAPSP.flCalcularOrden2(null, nIdPT);

                    decimal decPresup;
                    string sAux = aDatosTarea[13];
                    if (sAux == "") decPresup = 0;
                    else decPresup = decimal.Parse(sAux);
                    nIdTarea = TAREAPSP.Insert(tr,
                                             Utilidades.unescape(aDatosTarea[0]) //Des. tarea;
                                            , Utilidades.unescape(aDatosTarea[1]) //Des. larga de tarea;
                                            , nIdPT//Id PT
                                            , nAct//IdActividad
                                            , int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString())//promotor
                                            , int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString())//último modificador
                                            , DateTime.Now//F.Alta
                                            , DateTime.Now//F.Ultima modificacion
                                            , dValIni//F.Inicio Vigencia
                                            , dValFin//F.Fin vigencia
                                            , byte.Parse(aDatosTarea[5])//estado
                                            , dPLIni//F.Inicio Planificada
                                            , dPLFin//F.Fin Planificada
                                            , fETPL//Esfuerzo total estimado
                                            , dPRFin//F.Fin Prevista
                                            , fETPR//Esfuerzo total previsto
                                            , null//PST
                                            , fCLE//Control límite de esfuerzos
                                            , aDatosTarea[7]//tipo de control de límite de esfuerzo
                                            , nOrden//orden
                                            , bFacturable//facturable
                                            , decPresup//presupuesto
                                            , null, //24
                                            "",//OTL
                                            "",//INCIDENCIA
                                            "",//OBSERVACIONES
                                            bNotificable,
                                            "",//NOTAS1
                                            "",//NOTAS2
                                            "",//NOTAS3
                                            "",//NOTAS4
                                            fAvanceReal,
                                            bAvanceAuto,
                                            bImpIAP, false, bHeredaNodo, bHeredaPE,
                                            "", bNotifProf, aDatosTarea[29],
                                            (aDatosTarea[30] == "" || aDatosTarea[30] == "0") ? null : (int?)int.Parse(aDatosTarea[30]),
                                            bHorasComplementarias
                                            );
                }
                #region Notificación apertura de tarea
                if (bNotificable && bTareaNueva)
                {
                    if (strDatosApertura != "")
                    {
                        string sAux = EnviarCorreoApertura(strDatosApertura, nIdTarea.ToString());
                        string[] aAux = Regex.Split(sAux, "@#@");
                        if (aAux[0] != "OK")
                            sResulMails += aAux[1];
                    }
                }
                #endregion

                #region Notificación de cambio de estado de tarea
                bool bEstado = false;
                //Para que haya cambiado el estado de la tarea ha habido que cambiar la pestña general
                if (strDatosTarea != "" && strDatosCierre != "" && !bTareaNueva)
                {
                    if (aDatosTarea[23] == "1") bEstado = true;

                    if (bEstado)
                    {
                        string sAux = EnviarCorreoCierre(strDatosCierre, nIdTarea.ToString(), sEstadoAnt, sEstadoAct, bEstado);
                        string[] aAux = Regex.Split(sAux, "@#@");
                        if (aAux[0] != "OK")
                            sResulMails += aAux[1];
                    }
                }
                #endregion
            }
            #endregion
            #region OTC
            byte? nOrigen = null;
            int? nPST = null;
            if (strDatosOTC != "")
            {
                string[] aDatosOTC = Regex.Split(strDatosOTC, "##");
                if ((aDatosOTC[1] != "0") && (aDatosOTC[1] != "")) nOrigen = byte.Parse(aDatosOTC[1]);
                if (aDatosOTC[0] != "") nPST = int.Parse(aDatosOTC[0]);
                TAREAPSP.Update_P2(tr, nIdTarea,
                    int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()),
                    DateTime.Now,
                    nPST,
                    nOrigen,
                    Utilidades.unescape(aDatosOTC[2]),//OTL
                    Utilidades.unescape(aDatosOTC[3]));//INCIDENCIA
            }
            #endregion
            #region Notas
            bool bNotasIAP = false;
            if (strDatosNotas != "")
            {
                string[] aDatosNotas = Regex.Split(strDatosNotas, "##");
                if (aDatosNotas[0] == "1") bNotasIAP = true;
                TAREAPSP.Update_P3(tr, nIdTarea,
                    int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()),
                    DateTime.Now,
                    Utilidades.unescape(aDatosNotas[1]),//NOTAS1
                    Utilidades.unescape(aDatosNotas[2]),//NOTAS2
                    Utilidades.unescape(aDatosNotas[3]),//NOTAS3
                    Utilidades.unescape(aDatosNotas[4]),//NOTAS4
                    bNotasIAP
                    );
            }
            #endregion
            #region datos control
            if (strDatosControl != "")
            {
                string[] aDatosControl = Regex.Split(strDatosControl, "##");
                if (aDatosControl[0] == "T")
                {
                    TAREAPSP.Update_P4(tr, nIdTarea,
                                        int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()),
                                        DateTime.Now,
                                        Utilidades.unescape(aDatosControl[1]));
                }
            }
            #endregion

            #region datos Recursos
            if (bDuplicar)
            {
                bHayCambioPadre = true;
                POOL_GF_TAREA.DuplicarPoolGF(tr, int.Parse(sIdTareaAnterior), nIdTarea);
                //El 11/12/2007 Victor dice que no hay que copiar recursos. La tarea tendrá los que deriven del proyecto economico (por trigger)
                int nRes = AETAREAPSP.DuplicarAEs(tr, int.Parse(sIdTareaAnterior), nIdTarea);
            }
            else
            {
                string[] aRecursos = Regex.Split(strDatosRecursos, "///");
                string[] aMensGen = Regex.Split(strDatosMensGen, "##");
                double? fEtp = null;
                DateTime? dFip = null;
                DateTime? dFfp = null;
                DateTime dtFechaAlta = System.DateTime.Today;
                int? iIdTarifa = null;
                string sObs = "", sMensGen = "", sTO = "";
                bool bEnvMensGen = false, bAdmiteRecursoPSTCalculado = false, bAdmiteRecursoPST = false, bFechaAltaCalculada = false, bRecursoAsignado;
                int iUltCierreEco = 0;
                if (strDatosMensGen != "")
                {
                    if (aMensGen[0] == "T")
                    {
                        if (bNotifProf)
                        {
                            bEnvMensGen = true;
                            sTO = flListaRecursosActivos(nIdTarea);
                        }
                        sMensGen = Utilidades.unescape(aMensGen[1]);
                        TAREAPSP.ModificarMensaje(tr, nIdTarea, sMensGen);
                    }
                }
                foreach (string oRec in aRecursos)
                {
                    #region recorrer recursos
                    string[] aValores = Regex.Split(oRec, "##");
                    ///aValores[0] = opcionBD;
                    ///aValores[1] = idTarea;
                    ///aValores[2] = idRecurso;
                    ///aValores[3] = etp;
                    ///aValores[4] = ffp;
                    ///aValores[5] = idTarifa;
                    ///aValores[6] = estado;
                    ///aValores[7] = indicaciones;
                    ///aValores[8] = nombre;
                    ///aValores[9] = fip;
                    ///aValores[20] = bNotifExceso;
                    if (aValores[0] != "")
                    {
                        if (aValores[3] != "") fEtp = double.Parse(aValores[3]);
                        else fEtp = null;
                        if (aValores[9] != "") dFip = DateTime.Parse(aValores[9]);
                        else dFip = null;
                        if (aValores[4] != "") dFfp = DateTime.Parse(aValores[4]);
                        else dFfp = null;
                        if (aValores[5] != "") iIdTarifa = int.Parse(aValores[5]);
                        else iIdTarifa = null;
                        sObs = Utilidades.unescape(aValores[7]);
                        //sObs = aValores[7];
                        if (aValores[20] == "1") bNotifExceso = true;
                        else bNotifExceso = false;
                        switch (aValores[0])
                        {
                            case "I":
                                //int iNumAsig;
                                //iNumAsig=TareaRecurso.InsertarSNE(tr, nIdTarea, int.Parse(aValores[2]), null, null,
                                //                      fEtp, dFip, dFfp, iIdTarifa, int.Parse(aValores[6]), null, sObs, bNotifExceso);
                                if (!bAdmiteRecursoPSTCalculado)
                                {
                                    PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(tr, iT305IdProy);
                                    bAdmiteRecursoPST = oPSN.t305_admiterecursospst;
                                    bAdmiteRecursoPSTCalculado = true;
                                }
                                if (bAdmiteRecursoPST)
                                {
                                    if (!bFechaAltaCalculada)
                                    {//lA FECHA DE alta en el proyecto será la siguiente al último mes cerrado del nodo
                                        NODO o = NODO.Select(tr, nCR);
                                        iUltCierreEco = o.t303_ultcierreeco;
                                        bFechaAltaCalculada = true;
                                    }
                                }
                                bRecursoAsignado = TareaRecurso.InsertarTEC(tr, nIdTarea, int.Parse(aValores[2]), null, null, fEtp, dFip, dFfp,
                                                                            iIdTarifa, int.Parse(aValores[6]), null, sObs, bNotifExceso,
                                                                            bAdmiteRecursoPST, iT305IdProy, nCR, iUltCierreEco);
                                if (aValores[6] == "1" && bNotifProf && bRecursoAsignado)//el recurso está activo en la tarea y se notifica a profesionales y no estaba ya asignado a la tarea
                                {
                                    TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, fEtp.ToString(), dFip.ToString(), dFfp.ToString(), sObs, sMensGen);
                                    if (bEnvMensGen)
                                    {
                                        //Como ya le envío correo lo quito de la lista general
                                        sTO = flQuitarDeLista(sTO, aValores[2]);
                                    }
                                }
                                break;
                            case "U":
                                TareaRecurso.Modificar(tr, nIdTarea, int.Parse(aValores[2]), null, fEtp, dFip, dFfp, iIdTarifa,
                                                       int.Parse(aValores[6]), null, sObs, bNotifExceso);
                                if (aValores[6] == "1" && bNotifProf)
                                {
                                    TareaRecurso.EnviarCorreoRecurso(tr, "U", oRec, fEtp.ToString(), dFip.ToString(), dFfp.ToString(), sObs, sMensGen);
                                    if (bEnvMensGen)
                                    {
                                        //Como ya le envío correo lo quito de la lista general
                                        sTO = flQuitarDeLista(sTO, aValores[2]);
                                    }
                                }
                                //TareaRecurso.ReAsociarAProyecto(tr, int.Parse(aValores[2]), iT305IdProy);
                                break;
                            case "D":
                                //ANTES DE ELIMINAR, COMPROBAR SI TIENE CONSUMOS EN ESA TAREA:
                                //EN CASO AFIRMATIVO, NO PERMITIR EL BORRADO
                                bool bConsumos = TareaRecurso.ExistenConsumos(tr, nIdTarea, int.Parse(aValores[2]));
                                if (bConsumos)
                                {
                                    Conexion.CerrarTransaccion(tr);
                                    sResul = "Error@#@No se puede eliminar la asociación de\n" + Utilidades.unescape(aValores[8]) + "\nde la tarea, debido a que tiene consumos imputados en IAP.@#@" + aValores[2];
                                    return sResul;
                                }
                                TareaRecurso.Eliminar(tr, nIdTarea, int.Parse(aValores[2]));
                                break;
                        }
                    }//if (aValores[0] != "")
                    #endregion
                }
                if (bEnvMensGen && sTO != "")
                {
                    EnviarCorreoMensajeRecurso(sTO, sMensGen, strDatosMensGen, fEtp.ToString(), dFip.ToString(), dFfp.ToString(), sObs);
                }
            }
            #endregion

            #region datos Atributos Estadísticos

            string[] aAE = Regex.Split(strDatosAE, "///");

            foreach (string oAE in aAE)
            {
                string[] aValores = Regex.Split(oAE, "##");
                ///aValores[0] = opcionBD;
                ///aValores[1] = idTarea;
                ///aValores[2] = idAE;
                ///aValores[3] = idVAE;

                switch (aValores[0])
                {
                    case "I":
                        AETAREAPSP.Insert(tr, nIdTarea, int.Parse(aValores[2]), int.Parse(aValores[3]));
                        break;
                    case "U":
                        AETAREAPSP.Update(tr, nIdTarea, int.Parse(aValores[2]), int.Parse(aValores[3]));
                        break;
                    case "D":
                        AETAREAPSP.Delete(tr, nIdTarea, int.Parse(aValores[3]));
                        break;
                }
            }

            //Se controla por trigger
            //AETAREAPSP.DeleteDuplicados(tr, nIdTarea, int.Parse(aDatosTarea[23]));
            #endregion

            #region datos Campos

            string[] aCampos = Regex.Split(strDatosCampos, "///");

            foreach (string oCampos in aCampos)
            {
                string[] aValores = Regex.Split(oCampos, "##");
                ///aValores[0] = opcionBD;
                ///aValores[1] = idCampo;
                ///aValores[2] = Valor;
                ///aValores[3] = TipoDeDatos;
                ///aValores[4] = Horas;
                ///aValores[5] = Minutos;
                ///aValores[6] = Segundos;

                switch (aValores[0])
                {
                    case "I":
                        switch (aValores[3])
                        {
                            case "I":
                                decimal? dAux = null;
                                if (aValores[2] != "") dAux = decimal.Parse(aValores[2]);
                                TAREAPSPCAMPOIMPORTE.Insert(tr, nIdTarea, int.Parse(aValores[1]), dAux);
                                break;
                            case "F":
                                DateTime? dtAux = null;
                                if (aValores[2] != "") dtAux = DateTime.Parse(aValores[2]);
                                TAREAPSPCAMPOFECHA.Insert(tr, nIdTarea, int.Parse(aValores[1]), dtAux, aValores[3]);
                                break;
                            case "H":
                                DateTime? dFechaHora = null;
                                if (aValores[2] != "")
                                    dFechaHora = DateTime.Parse(aValores[2] + " " + aValores[4] + ":" + aValores[5] + ":" + aValores[6]);
                                TAREAPSPCAMPOFECHA.Insert(tr, nIdTarea, int.Parse(aValores[1]), dFechaHora, aValores[3]);
                                break;

                            case "T":                                
                               TAREAPSPCAMPOTEXTO.Insert(tr, nIdTarea, int.Parse(aValores[1]), aValores[2]);                               
                                break;
                        }

                        break;

                    case "U":
                        switch (aValores[3])
                        {
                            case "I":
                                decimal? dAux = null;
                                if (aValores[2] != "") dAux = decimal.Parse(aValores[2]);
                                TAREAPSPCAMPOIMPORTE.Update(tr, nIdTarea, int.Parse(aValores[1]), dAux);
                                break;
                            case "F":
                                DateTime? dtAux = null;
                                if (aValores[2] != "") dtAux = DateTime.Parse(aValores[2]);
                                TAREAPSPCAMPOFECHA.Update(tr, nIdTarea, int.Parse(aValores[1]), dtAux, aValores[3]);
                                break;
                            case "H":
                                DateTime? dFechaHora = null;
                                if (aValores[2] != "")
                                    dFechaHora = DateTime.Parse(aValores[2] + " " + aValores[4] + ":" + aValores[5] + ":" + aValores[6]);
                                TAREAPSPCAMPOFECHA.Update(tr, nIdTarea, int.Parse(aValores[1]), dFechaHora, aValores[3]);
                                break;
                            case "T":
                                TAREAPSPCAMPOTEXTO.Update(tr, nIdTarea, int.Parse(aValores[1]), aValores[2]);
                                break;
                        }
                        break;

                    case "D":
                        switch (aValores[3])
                        {
                            case "I":
                                TAREAPSPCAMPOIMPORTE.Delete(tr, nIdTarea, int.Parse(aValores[1]));
                                break;
                            case "F":
                            case "H":
                                TAREAPSPCAMPOFECHA.Delete(tr, nIdTarea, int.Parse(aValores[1]));
                                break;
                            case "T":
                                TAREAPSPCAMPOTEXTO.Delete(tr, nIdTarea, int.Parse(aValores[1]));
                                break;
                        }
                        break;
                }
            }


            #endregion
            #region Pool
            //Ahora recojo los grupos funcionales del pool de la actividad
            string[] aPoolGF = Regex.Split(strDatosPoolGF, "///");
            string sCad, sOpcionBD;
            int iPos, iCodGF;
            for (int i = 0; i < aPoolGF.Length - 1; i++)
            {
                sCad = aPoolGF[i];
                iPos = sCad.IndexOf("##");
                sOpcionBD = sCad.Substring(0, iPos);
                sCad = sCad.Substring(iPos + 2);
                iCodGF = int.Parse(sCad);
                switch (sOpcionBD)
                {
                    case "I":
                        POOL_GF_TAREA.Insert(tr, nIdTarea, iCodGF);
                        break;
                    case "D":
                        POOL_GF_TAREA.Delete(tr, nIdTarea, iCodGF);
                        break;
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + DateTime.Now.ToString() + "@#@" + Session["NUM_EMPLEADO_ENTRADA"].ToString() + "@#@" +
                      Session["DES_EMPLEADO_ENTRADA"].ToString() + "@#@" + sResulMails + "@#@" + nIdTarea.ToString();//.ToString("#,###")
            if (bHayCambioPadre) sResul += "@#@T";
            else sResul += "@#@F";

            sResul += "@#@" + sETPR + "@#@" + sFFPR;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la tarea", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string Borrar(string sIdTarea)
    {
        string sResul = "OK@#@";
        int nIdTarea;

        #region abrir conexión y transacción
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
            sIdTarea = sIdTarea.Replace(".", "");
            nIdTarea = int.Parse(sIdTarea);
            if (TAREAPSP.bTieneConsumo(null, nIdTarea))
            {
                sResul += "Error@#@No se puede borrar porque tiene consumos";
            }
            else
            {//Compruebo que no tenga registros en la T433_PRODUCFACTPROF
                //Si existen no permito borrado, 
                //sino borro de T344_PERFILPSTUSUARIOMC pues no tiene delete cascada con tarea
                int iNumReg = PRODUCFACTPROF.GetFilasTarea(null, nIdTarea);
                if (iNumReg > 0)
                {
                    sResul = "Error@#@No se puede borrar porque tiene producción por profesional";
                }
                else
                {//borro de T344_PERFILPSTUSUARIOMC pues no tiene delete cascada con tarea
                    TAREAPSP.BorrarPerfil(null, nIdTarea);
                    TAREAPSP.Delete(null, nIdTarea);
                }
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar la tarea", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3, string sCodUne,
                                    string t305_idProyectoSubnodo, string sCualidad, string sIdTarea)
    {
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;

        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);

            dr = Recurso.GetUsuariosPSN(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, sIdTarea);

            sb.Append("<table id='tblRelacion' class='texto MAM' style='width: 340px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:320px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                //sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'> ");
                //sb.Append("Información] body=[Profesional:&nbsp;");
                //sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                //sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");
                //sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                //else sb.Append("tipo='N' ");

                //if (this.hdnAcceso.Text == "R")
                //{
                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' idTarifa='" + dr["IDTARIFA"].ToString() +
                                  "'><td></td><td><nobr class='NBR W320'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
                //}
                //else
                //{
                //    sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' idTarifa='" + dr["IDTARIFA"].ToString() +
                //                      "' onmousedown='DD(event)' onclick='mm(event);' ondblclick='insertarRecurso(this);'>" +
                //                      "<td></td><td><nobr class='NBR W320'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
                //}
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody></table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }

    protected string ObtenerDatosTareaRecurso(string strIdTarea, string strIdRecurso)
    {
        string sResul = "";
        StringBuilder strBuilder = new StringBuilder();

        try
        {
            TareaRecurso o = TareaRecurso.Obtener(int.Parse(strIdTarea), int.Parse(strIdRecurso));

            string sFFEaux = "";
            string sFIPaux = "";
            string sFFPaux = "";
            string sPriCons = "";
            string sUltCons = "";
            string sNotifExceso = "0";

            if (o.dFfe != DateTime.Parse("01/01/0001"))
                sFFEaux = o.dFfe.ToShortDateString();
            if (o.dFip != DateTime.Parse("01/01/0001"))
                sFIPaux = o.dFip.ToShortDateString();
            if (o.dFfp != DateTime.Parse("01/01/0001"))
                sFFPaux = o.dFfp.ToShortDateString();
            if (o.dPrimerConsumo != DateTime.Parse("01/01/0001"))
                sPriCons = o.dPrimerConsumo.ToShortDateString();
            if (o.dUltimoConsumo != DateTime.Parse("01/01/0001"))
                sUltCons = o.dUltimoConsumo.ToShortDateString();
            if (o.bNotifExceso)
                sNotifExceso = "1";
            strBuilder.Append("insertarRecursoEnArray(\"\",\"" + o.nIdTarea.ToString() + "\",\"" + o.nIdRecurso.ToString() + "\",\"" +
                              o.sNombreCompleto + "\",\"" + o.nEte.ToString() + "\",\"" + sFFEaux + "\",\"" + o.nEtp.ToString() + "\",\"" +
                              sFIPaux + "\",\"" + sFFPaux + "\",\"" + o.nIdTarifa.ToString() + "\",\"" + o.nEstado.ToString() + "\",\"" +
                              Utilidades.escape(o.sComentario.ToString()) + "\",\"" + Utilidades.escape(o.sIndicaciones.ToString()) +
                              "\",\"" + o.nPendiente.ToString("N") + "\",\"" + o.nCompletado.ToString() + "\",\"" +
                              o.nAcumulado.ToString() + "\",\"" + sPriCons + "\",\"" + sUltCons + "\",\"" + sNotifExceso + "\");"
                             );

            sResul = "OK@#@" + o.nIdRecurso.ToString() + "@#@" + strBuilder.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos del profesional.", ex);
        }

        return sResul;
    }

    protected string EnviarCorreoApertura(string strDatosTarea, string sIdTarea)
    {
        string sResul = "";
        //string sResul = "", sIdPT, sIdTarea, sDesHito, sDatosTareaHito, sAvance;
        //double dAvance;
        //int iCodHito, iNumHitos = 0;
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sbuilder = new StringBuilder();
        string sAsunto = "";
        string sTexto = "";
        string sTO = "";

        try
        {
            sAsunto = "Apertura de tarea.";

            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            //aDatosTarea[0] = hdnIdTarea
            //aDatosTarea[1] = txtDesTarea
            //aDatosTarea[2] = txtNumPE
            //aDatosTarea[3] = txtPE
            //aDatosTarea[4] = txtPT
            //aDatosTarea[5] = txtFase
            //aDatosTarea[6] = txtActividad

            //aDatosTarea[7] = txtCodPST
            //aDatosTarea[8] = txtDesPST
            //aDatosTarea[9] = txtOTL
            //aDatosTarea[10] = txtIncidencia
            //aDatosTarea[11] = strMail

            //Hay que notificar el fin de la tarea al origen de la misma.
            sbuilder.Append(@"<BR>SUPER le informa de la apertura de la siguiente tarea:<BR><BR>");
            sbuilder.Append("<label style='width:120px'>Proyecto económico: </label>" + aDatosTarea[2] + @" - " + Utilidades.unescape(aDatosTarea[3]) + "<br>");
            sbuilder.Append("<label style='width:120px'>Proyecto Técnico: </label>" + Utilidades.unescape(aDatosTarea[4]) + "<br>");

            if (aDatosTarea[5] != "") sbuilder.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(aDatosTarea[5]) + "<br>");
            if (aDatosTarea[6] != "") sbuilder.Append("<label style='width:120px'>Actividad: " + Utilidades.unescape(aDatosTarea[6]) + "<br>");

            //sbuilder.Append("<label style='width:120px'>Tarea: </label><b>" + aDatosTarea[0] + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
            sbuilder.Append("<label style='width:120px'>Tarea: </label><b>" + int.Parse(sIdTarea.Replace(".", "")).ToString("#,###") + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
            sbuilder.Append("<b>Información de la tarea:</b><br><br>");

            if (aDatosTarea[7] != "")
                sbuilder.Append("<label style='width:120px'>OTC: </label>" + Utilidades.unescape(aDatosTarea[7]) + " - " + Utilidades.unescape(aDatosTarea[8]) + "<br>");
            if (aDatosTarea[9] != "")
                sbuilder.Append("<label style='width:120px'>OTL: </label>" + Utilidades.unescape(aDatosTarea[9]) + "<br>");
            if (aDatosTarea[10] != "")
                sbuilder.Append("<label style='width:120px'>Incidencia/Petición: </label>" + Utilidades.unescape(aDatosTarea[10]) + "<br>");

            sTO = Utilidades.unescape(aDatosTarea[11]);
            //sTO = sTO.Replace(";", @"/");
            sTexto = sbuilder.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            aListCorreo.Add(aMail);

            Correo.EnviarCorreos(aListCorreo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de apertura de tarea.", ex);
        }
        return sResul;
    }
    protected string EnviarCorreoCierre(string strDatosTarea, string sIdTarea, string sEstadoAnt, string sEstadoAct, bool bEstado)
    {
        string sResul = "", sIdPT, sDesHito, sDatosTareaHito, sAvance, sAnt = "", sAct = "";
        double dAvance;
        int iCodHito, iNumHitos = 0;
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sbuilder = new StringBuilder();
        try
        {
            //Desgloso la cadena que se pasa como parametro para recoger los datos de la tarea
            //Recojo la lista de RTPT´s asignados al proyecto técnico que engloba la tarea
            //Recojo la lista de hitos que contienen esa tarea
            //Genero el texto de la comunicación
            //Envío el correo
            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            //aDatosTarea[0] = hdnIdTarea
            //aDatosTarea[1] = txtDesTarea
            //aDatosTarea[2] = txtNumPE
            //aDatosTarea[3] = txtPE
            //aDatosTarea[4] = txtPT
            //aDatosTarea[5] = txtFase
            //aDatosTarea[6] = txtActividad
            //aDatosTarea[7] = txtPLEst
            //aDatosTarea[8] = txtPLIni
            //aDatosTarea[9] = txtPLFin
            //aDatosTarea[10] = txtPriCon
            //aDatosTarea[11] = txtPREst
            //aDatosTarea[12] = txtPRFin
            //aDatosTarea[13] = txtUltCon
            //aDatosTarea[14] = txtConHor
            //aDatosTarea[15] = txtAvanReal
            //aDatosTarea[16] = hdnIDPT
            //aDatosTarea[17] = Notificar al origen (Sí 1, No 0)
            //aDatosTarea[18] = txtCodPST
            //aDatosTarea[19] = txtDesPST
            //aDatosTarea[20] = txtOTL
            //aDatosTarea[21] = txtIncidencia
            //aDatosTarea[22] = strMail

            //sIdTarea = aDatosTarea[0];
            sIdPT = aDatosTarea[16];

            string sAsunto = "";
            string sTexto = "";
            string sTO = "";

            sTO = flListaRTPTs(int.Parse(sIdPT));
            //sTO = "DOARHUMI";
            //TAREAPSP oTarea = TAREAPSP.Obtener(null, int.Parse(sIdTarea));

            sAsunto = "Cambio de estado de tarea.";
            //sbuilder.Append("PSP le informa de la finalización de la siguiente tarea:<br><br>");
            switch (sEstadoAnt)
            {
                case "0":
                    sAnt = "Paralizada";
                    break;
                case "1":
                    sAnt = "Activa";
                    break;
                case "2":
                    sAnt = "Pendiente";
                    break;
                case "3":
                    sAnt = "Finalizada";
                    break;
                case "4":
                    sAnt = "Cerrada";
                    break;
                case "5":
                    sAnt = "Anulada";
                    break;
                default:
                    sAnt = "Desconocido";
                    break;
            }
            switch (sEstadoAct)
            {
                case "0":
                    sAct = "Paralizada";
                    break;
                case "1":
                    sAct = "Activa";
                    break;
                case "2":
                    sAct = "Pendiente";
                    break;
                case "3":
                    sAct = "Finalizada";
                    break;
                case "4":
                    sAct = "Cerrada";
                    break;
                case "5":
                    sAct = "Anulada";
                    break;
                default:
                    sAct = "Desconocido";
                    break;
            }
            if (bEstado)
            {
                sbuilder.Append("SUPER le informa de que la tarea ha pasado de estar " + sAnt + " a " + sAct + ":<br><br>");
                sbuilder.Append(@"<label style='width:120px'>Proyecto económico: </label>" + aDatosTarea[2] + @" - " + Utilidades.unescape(aDatosTarea[3]) + "<br>");
                sbuilder.Append(@"<label style='width:120px'>Proyecto Técnico: </label>" + Utilidades.unescape(aDatosTarea[4]) + "<br>");
                if (aDatosTarea[5] != "")
                    sbuilder.Append(@"<label style='width:120px'>Fase: </label>" + Utilidades.unescape(aDatosTarea[5]) + "<br>");
                if (aDatosTarea[6] != "")
                    sbuilder.Append(@"<label style='width:120px'>Actividad: </label>" + Utilidades.unescape(aDatosTarea[6]) + "<br>");

                sbuilder.Append(@"<label style='width:120px'>Tarea: </label><b>" + int.Parse(sIdTarea.Replace(".", "")).ToString("#,###") + @" - " +
                                Utilidades.unescape(aDatosTarea[1]) + @"</b><br><br>");

                sbuilder.Append(@"<table style='width:800px' class='TBLINI'>");
                sbuilder.Append(@"<colgroup><col style='width:90px;' /><col style='width:80px;' /><col style='width:80px;' /><col style='width:110px;' /><col style='width:110px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:60px;' /></colgroup>");
                sbuilder.Append(@"<TR><td title='Esfuerzo total planificado' style='text-align-right;'>Esf. total plan.</td>");
                sbuilder.Append(@"<td title='Fecha inicio planificada' style='text-align-right;'>F/ini. plan.</td>");
                sbuilder.Append(@"<td title='Fecha fin planificada' style='text-align-right;'>F/fin plan.</td>");
                sbuilder.Append(@"<td title='Fecha primer consumo' style='text-align-right;'>F/1º consumo</td>");
                sbuilder.Append(@"<td title='Esfuerzo total previsto' style='text-align-right;'>Esf. total prev.</td>");
                sbuilder.Append(@"<td title='Fecha fin prevista' style='text-align-right;'>F/fin prevista</td>");
                sbuilder.Append(@"<td title='Fecha último consumo' style='text-align-right;'>F/último cons.</td>");
                sbuilder.Append(@"<td style='text-align-right;'>Consumido</td>");
                sbuilder.Append(@"<td style='text-align-right;'>Avance</td></tr></table>");

                sbuilder.Append(@"<table style='width:800px' class='texto'>");
                sbuilder.Append(@"<colgroup><col style='width:90px;' /><col style='width:80px;' /><col style='width:80px;' /><col style='width:110px;' /><col style='width:110px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:60px;' /></colgroup>");
                sbuilder.Append(@"<tr><td style='text-align-right;'> " + aDatosTarea[7] + @"</td>");//ETPL
                sbuilder.Append(@"<td style='text-align-right;'> " + aDatosTarea[8] + @"</td>");//FIPL
                sbuilder.Append(@"<td style='text-align-right;'> " + aDatosTarea[9] + @"</td>");//FFPL
                sbuilder.Append(@"<td style='text-align-right;'> " + aDatosTarea[10] + @"</td>");//F1C
                //sbuilder.Append(@"<td bgcolor='red'> " + aDatosTarea[11] + @"</td>");//ETPR
                sbuilder.Append(@"<td style='text-align-right;'> " + aDatosTarea[11] + @"</td>");//ETPR
                sbuilder.Append(@"<td style='text-align-right;'> " + aDatosTarea[12] + @"</td>");//FFPR
                sbuilder.Append(@"<td style='text-align-right;'> " + aDatosTarea[13] + @"</td>");//FUC
                sbuilder.Append(@"<td style='text-align-right;'> " + aDatosTarea[14] + @"</td>");//CONSUMIDO
                dAvance = 0;
                if (aDatosTarea[15] != "") dAvance = double.Parse(aDatosTarea[15]);
                sAvance = dAvance.ToString("N");
                sbuilder.Append(@"<td style='text-align-right;'> " + sAvance + @"</td></tr></table>");//AVANCE

                sbuilder.Append(@"<table style='width:800px' class='textoResultadoTabla'><tr><td>&nbsp;</td></tr></table>");

                //Recojo los hitos que contienen esa tarea y que no estén Inactivos (estado=F)
                SqlDataReader dr2 = TAREAPSP.CatalogoHitos(int.Parse(sIdTarea.Replace(".", "")));
                while (dr2.Read())
                {
                    iCodHito = int.Parse(dr2["t349_idhito"].ToString());
                    sDesHito = dr2["t349_deshito"].ToString();
                    iNumHitos++;
                    if (iNumHitos == 1)
                    {
                        sbuilder.Append(@"<br><br>Estado de las tareas de los hitos relacionados<br><br>");
                    }
                    sbuilder.Append(@"<b>Hito: " + sDesHito + @"</b>");
                    //Para cada hito recojo todas las tareas que lo componen
                    sDatosTareaHito = flDatosTareaHito(iCodHito, sIdTarea.Replace(".", ""), aDatosTarea[11], aDatosTarea[14], sAvance);
                    sbuilder.Append(sDatosTareaHito);
                }
                dr2.Close();
                dr2.Dispose();

                sbuilder.Append(@"<br><br><a href='http://super.intranet.ibermatica/default.aspx'>Para acceder a SUPER pinche aquí (Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</a>");
                sTexto = sbuilder.ToString();
                sbuilder.Length = 0;
                string[] aMail = { sAsunto, sTexto, sTO };
                if (sTO != "") aListCorreo.Add(aMail);
            }
            //if (aDatosTarea[17] == "1" && bCierre)
            if (aDatosTarea[17] == "1" && bEstado && (sEstadoAct == "3" || sEstadoAct == "4" || sEstadoAct == "5"))
            {
                //Hay que notificar el fin de la tarea al origen de la misma si ha habido cambio de estado y el estado
                //actual es Cerrada o Finalizada o Anulada
                //sbuilder.Append(@"<BR>PSP le informa de la finalización de la siguiente tarea:<BR><BR>");
                sbuilder.Append(@"<BR>SUPER le informa de que la tarea a pasado de estar " + sAnt + " a " + sAct + ":<BR><BR>");
                sbuilder.Append("<label style='width:120px'>Proyecto económico: </label>" + aDatosTarea[2] + @" - " + Utilidades.unescape(aDatosTarea[3]) + "<br>");
                sbuilder.Append("<label style='width:120px'>Proyecto Técnico: </label>" + Utilidades.unescape(aDatosTarea[4]) + "<br>");

                if (aDatosTarea[5] != "") sbuilder.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(aDatosTarea[5]) + "<br>");
                if (aDatosTarea[6] != "") sbuilder.Append("<label style='width:120px'>Actividad: " + Utilidades.unescape(aDatosTarea[6]) + "<br>");

                sbuilder.Append("<label style='width:120px'>Tarea: </label><b>" + int.Parse(sIdTarea.Replace(".", "")).ToString("#,###") + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
                //*******************
                sbuilder.Append(@"<table style='width:800px' class='texto'>");
                sbuilder.Append(@"<colgroup><col style='width:120px;' /><col style='width:680px;' /></colgroup>");
                sbuilder.Append(@"<tr><td colspan=2><b>Información de la tarea:</b><br><br></td></tr>");

                if (aDatosTarea[19] != "")
                    sbuilder.Append(@"<tr><td><b>OTC:</b></td><td>" + Utilidades.unescape(aDatosTarea[18]) + " - " + Utilidades.unescape(aDatosTarea[19]) + @"</td></tr>");
                if (aDatosTarea[20] != "")
                    sbuilder.Append(@"<tr><td><b>OTL:</b></td><td>" + Utilidades.unescape(aDatosTarea[20]) + "</td></tr>");
                if (aDatosTarea[21] != "")
                    sbuilder.Append("<tr><td><b>Incidencia/Petición:</b></td><td>" + Utilidades.unescape(aDatosTarea[21]) + "</td></tr></table>");
                //********************
                //sbuilder.Append("<b>Información de la tarea:</b><br><br>");

                //if (aDatosTarea[19] != "")
                //    sbuilder.Append("<label style='width:120px'>OTC: </label>" + Utilidades.unescape(aDatosTarea[18]) + " - " + Utilidades.unescape(aDatosTarea[19]) + "<br>");
                //if (aDatosTarea[20] != "")
                //    sbuilder.Append("<label style='width:120px'>OTL: </label>" + Utilidades.unescape(aDatosTarea[20]) + "<br>");
                //if (aDatosTarea[21] != "")
                //    sbuilder.Append("<label style='width:120px'>Incidencia/Petición: </label>" + Utilidades.unescape(aDatosTarea[21]) + "<br>");

                sTO = Utilidades.unescape(aDatosTarea[22]);
                //sTO = sTO.Replace(";", @"/");
                sTexto = sbuilder.ToString();

                string[] aMail2 = { sAsunto, sTexto, sTO };
                if (sTO != "") aListCorreo.Add(aMail2);
            }

            Correo.EnviarCorreos(aListCorreo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de tarea finalizada.", ex);
        }
        return sResul;
    }
    public static string EnviarCorreoMensajeRecurso(string sTO, string sMensGen, string strDatosMensGen, string sEtp,
                                                    string sIni, string sFin, string sObs)
    {
        string sResul = "";
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sbuilder = new StringBuilder();
        string sAsunto = "", sTexto = "";

        try
        {
            sAsunto = "Modificación de asignación de profesional a tarea.";
            sbuilder.Append(@"<BR>SUPER le informa de la modificación de las indicaciones generales de la siguiente tarea:<BR><BR>");

            string[] aDatosTarea = Regex.Split(strDatosMensGen, "##");
            //aDatosTarea[2] = hdnIdTarea
            //aDatosTarea[3] = txtDesTarea
            //aDatosTarea[4] = txtNumPE
            //aDatosTarea[5] = txtPE
            //aDatosTarea[6] = txtPT
            //aDatosTarea[7] = txtFase
            //aDatosTarea[8] = txtActividad

            sbuilder.Append("<label style='width:120px'>Proyecto económico: </label>" + aDatosTarea[4] + @" - " + Utilidades.unescape(aDatosTarea[5]) + "<br>");
            sbuilder.Append("<label style='width:120px'>Proyecto Técnico: </label>" + Utilidades.unescape(aDatosTarea[6]) + "<br>");

            if (aDatosTarea[7] != "") sbuilder.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(aDatosTarea[7]) + "<br>");
            if (aDatosTarea[8] != "") sbuilder.Append("<label style='width:120px'>Actividad: </label>" + Utilidades.unescape(aDatosTarea[8]) + "<br>");

            sbuilder.Append("<label style='width:120px'>Tarea: </label><b>" + int.Parse(aDatosTarea[2].Replace(".", "")).ToString("#,###") + @" - " + Utilidades.unescape(aDatosTarea[3]) + "</b><br><br>");
            sbuilder.Append("<b>Indicaciones generales:</b><br><br>");

            sbuilder.Append(Utilidades.unescape(sMensGen) + "<br>");

            sTexto = sbuilder.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            if (sTO != "") aListCorreo.Add(aMail);

            Correo.EnviarCorreos(aListCorreo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de mensaje genérico a recursos de tarea.", ex);
        }
        return sResul;
    }

    private string flDatosTareaHito(int iCodHito, string sIdTarea, string sETPR, string sConHor, string sAvance)
    {
        //Devuelve el HTML de los datos de las tareas englobadas por el hito
        string sResul = "", sCad, sCodTarea;
        double fPrev = 0, fCons = 0, fAvance = 0;//, fPlan;
        int iCodTarea;
        StringBuilder sbuilder = new StringBuilder();
        try
        {
            //sbuilder.Append("<table id='tblTareas' class='texto' style='WIDTH: 800px; BORDER-COLLAPSE: collapse;' cellSpacing='0' border='0'>");
            sbuilder.Append(@"<table  style='width:800px' class='TBLINI'>");
            sbuilder.Append(@"<colgroup><col style='width:70px;' /><col style='width:580px;' /><col style='width:65px;' /><col style='width:85px;'</colgroup>");
            sbuilder.Append(@"<tr><td style='text-align:right;'>Código</td><td style='padding-left:10px;'>Descripción</td><td style='text-align:right;'>Avance</td><td style='padding-left:10px;'>Estado</td></tr></table>");

            sbuilder.Append(@"<table  style='width:800px' class='texto'>");
            sbuilder.Append(@"<colgroup><col style='width:70px;' /><col style='width=580px;' /><col style='width:65px;' /><col style='width:85px;'</colgroup>");
            SqlDataReader dr = HITOPSP.CatalogoTareas(iCodHito);
            while (dr.Read())
            {
                sbuilder.Append("<tr>");
                iCodTarea = int.Parse(dr["t332_idtarea"].ToString());
                sCodTarea = iCodTarea.ToString("#,###");

                sbuilder.Append(@"<td style='text-align:right;'>" + sCodTarea + @"</td>");
                sbuilder.Append(@"<td style='padding-left:10px;'>" + dr["t332_destarea"].ToString() + @"</td>");
                //Si la tarea actual es la que tenemos en pantalla ponemos sus valores de avance (ya que todavía no está grabada)
                if (sCodTarea == sIdTarea)
                {
                    sbuilder.Append(@"<td style='text-align:right;'>" + sAvance + @"</td>");
                    sbuilder.Append(@"<td style='padding-left:10px;' class='check'>&nbsp;</td></tr>");
                }
                else
                {
                    //%Avance
                    fPrev = double.Parse(dr["t332_etpr"].ToString());
                    fCons = double.Parse(dr["consumo"].ToString());
                    if (fPrev == 0) fAvance = 0;
                    else fAvance = (fCons * 100) / fPrev;
                    sCad = fAvance.ToString("N");

                    sbuilder.Append(@"<td style='text-align:right;'>" + sCad + @"</td>");
                    //if (bool.Parse(dr["Finalizada"].ToString()))
                    //    sbuilder.Append(@"<td class='check'>&nbsp;</td></tr>");
                    //else
                    //    sbuilder.Append(@"<td>&nbsp;</td></tr>");
                    sbuilder.Append(@"<td style='padding-left:10px;'>" + dr["Estado"].ToString() + @"</td></tr>");
                }
            }
            dr.Close(); dr.Dispose();
            sbuilder.Append(@"</table>");
            sbuilder.Append(@"<table style='width:800px' class='textoResultadoTabla'><tr><td>&nbsp;</td></tr></table><br>");
            sResul = sbuilder.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener tareas del hito " + iCodHito.ToString(), ex);
        }
        return sResul;
    }
    private string flListaRTPTs(int nIdPT)
    {
        string sResul = "";
        try
        {
            SqlDataReader dr = RTPT.SelectMailByt331_idpt(null, nIdPT);
            while (dr.Read())
            {
                if (dr["MAIL"].ToString() != "")
                {
                    if (sResul != "") sResul += ";" + dr["MAIL"].ToString();
                    else sResul = dr["MAIL"].ToString();
                }
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al generar la lista de RTPTs del PT. " + nIdPT.ToString(), ex);
        }
        return sResul;
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Obtiene una lista de códigos de red de los recursos asociados a la tarea que estén activos en la tarea
    /// </summary>
    /// -----------------------------------------------------------------------------
    private string flListaRecursosActivos(int nIdTarea)
    {
        string sResul = "";
        try
        {
            SqlDataReader dr = TareaRecurso.MailActivos(nIdTarea);
            while (dr.Read())
            {
                if (dr["MAIL"].ToString() != "")
                {
                    if (sResul != "") sResul += ";" + dr["MAIL"].ToString();
                    else sResul = dr["MAIL"].ToString();
                }
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" +
                     Errores.mostrarError("Error al generar la lista de mails de los recursos de la tarea. " + nIdTarea.ToString(), ex);
        }
        return sResul;
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Obtiene una lista de códigos de red de los recursos asociados a la tarea que estén activos en la tarea
    /// </summary>
    /// -----------------------------------------------------------------------------
    private string flQuitarDeLista(string sLista, string t314_idusuario)
    {
        string sResul = sLista;
        try
        {
            string sCodRed = Recurso.CodigoRed(int.Parse(t314_idusuario));
            if (sCodRed != "")
            {
                if (sLista == sCodRed)
                    sResul = "";
                else
                {
                    if (sLista.IndexOf(sCodRed + @";") > -1)
                    {
                        sCodRed = sCodRed + @";";
                        sResul = sLista.Replace(sCodRed, "");
                    }
                    else
                    {
                        if (sLista.IndexOf(@";" + sCodRed) > -1)
                        {
                            sCodRed = @";" + sCodRed;
                            sResul = sLista.Replace(sCodRed, "");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" +
                     Errores.mostrarError("Error al quitar elemento de la lista de mails de los recursos de la tarea. " + t314_idusuario, ex);
        }
        return sResul;
    }

    private string Permiso(string sT305IdProy, string sCodUne, string sNumProyEco, string sIdTarea)
    {
        string sResul = "N", sEstProy;
        try
        {
            //1º miramos si hay acceso sobre la tarea
            string sUserAct = Session["UsuarioActual"].ToString();
            int iUserAct = int.Parse(sUserAct);
            sIdTarea = sIdTarea.Replace(".", "");
            sResul = TAREAPSP.getAcceso(null, int.Parse(sIdTarea), iUserAct);
            //N-> no hay acceso R-> acceso en lectura W-> acceso en escritura
            if (sResul != "N")
            {
                //Miramos el estado del proyecto economico. Por que si está cerrado aunque tenga permiso solo se podrá leer
                sEstProy = EstrProy.estadoProyecto(sT305IdProy);
                if (sEstProy == "C" || sEstProy == "H")
                {
                    if (sResul == "W") sResul = "R";
                }
                if (sResul == "R")
                {
                    ModoLectura.Poner(this.Controls);
                }
            }
            gsAcceso = sResul;
            this.hdnAcceso.Text = sResul;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener permisos sobre la tarea " + sIdTarea, ex);
        }
        //return "OK@#@" + sResul;
        return sResul;
    }
    private string getDatosProy(string sT305IdProy)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, int.Parse(sT305IdProy));

            if (oPSN.t320_facturable) sb.Append("1@#@");
            else sb.Append("0@#@");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos del ProyectoSubNodo " + sT305IdProy, ex);
        }
        return sResul;
    }
    protected string EliminarDocumentos(string strIdsDocs)
    {
        string sResul = "";

        #region abrir conexión y transacción
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

            string[] aDocs = Regex.Split(strIdsDocs, "##");

            foreach (string oDoc in aDocs)
            {
                DOCUT.Delete(tr, int.Parse(oDoc));
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

    private string ObtenerPoolGF(string sIdPT, string sIdFase, string sIdActividad, string sCodTarea)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";
        try
        {
            sb.Append("<table id='tblPoolGF' class='texto' style='width: 370px;'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:355px;' /></colgroup>");
            sb.Append("<tbody>");
            if (sCodTarea != "")
            {
                int nIdPT = -1, nIdFase = -1, nIdActividad = -1, iCodTarea;
                if (sIdPT != "") nIdPT = int.Parse(sIdPT);
                if (sIdFase != "") nIdFase = int.Parse(sIdFase);
                if (sIdActividad != "") nIdActividad = int.Parse(sIdActividad);
                iCodTarea = int.Parse(sCodTarea.Replace(".", ""));
                SqlDataReader dr = POOL_GF_TAREA.Catalogo(nIdPT, nIdFase, nIdActividad, iCodTarea);
                while (dr.Read())
                {
                    sb.Append("<tr id='");
                    sb.Append(dr["idGF"].ToString());
                    if (int.Parse(dr["heredado"].ToString()) == 0)
                        sb.Append("' bd='N' h='N' onclick='mm(event)' style='height:16px;'>");
                    else
                        sb.Append("' bd='N' h='S' style='height:16px;color=gray'>");
                    sb.Append("<td><img src='../../../images/imgFN.gif'></td><td>");
                    sb.Append(dr["desGF"].ToString());
                    sb.Append("</td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaPoolGF = sb.ToString();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener el Pool de GF´s", ex);
        }
        return sResul;
    }
    private string ObtenerCamposValor(string sCodTarea)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";
        string sComboHoras;
        string sComboMinutos;
        string sComboSegundos;

        int iSel;

        try
        {
            sb.Append("<table id='tblCampos' class='texto MM' mantenimiento='1' style='width:600px;'>");
              sb.Append(@"<colgroup><col style='width:20px;' /><col style='width:150px;' /><col style='width:60px'/>
                        <col style='width:80px;' /><col style='width:40px;' /><col style='width:40px;' />
                        <col style='width:40px;' /><col style='width:170px;' /></colgroup>");
            SqlDataReader dr = TAREAPSP.obtenerCamposValor(null, int.Parse(sCodTarea.Replace(".", "")));
            while (dr.Read())
            {
                sb.Append("<tr id='");
                sb.Append(dr["IDENTIFICADOR"].ToString());
                sb.Append("' tipodato='" + dr["t291_idtipodato"].ToString());
                sb.Append("' bd='' onclick='ms(this)' onKeyUp=\"mfa(this,'U')\" target='true' onmousedown='DD(event);' style='height:20px;'>");

                sb.Append("<td style='padding-left:5px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W150' onmouseover='TTip(event)'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t291_denominacion"].ToString() + "</td>");
                switch (dr["t291_idtipodato"].ToString())
                {
                    case "I":
                        string strImporte = "";
                        if (dr["valor_money"] == System.DBNull.Value) strImporte = "";
                        else strImporte = decimal.Parse(dr["valor_money"].ToString()).ToString("N");

                        sb.Append("<td align='left' colspan='5'><input type='text' class='txtNumL' onblur=\"this.className='txtNumL'\" onfocus=\"this.className='txtNumM';this.select();fn(this,7,2)\" style='width:70px;' value=\"" + strImporte + "\" onkeyup='fm(event);aGAvanza(1);'>");
                        break;
                    case "T":
                        sb.Append("<td align='left' colspan='5'><input type='text' class='txtL' onfocus=\"this.className='txtM';this.select();\" onblur=\"this.className='txtL'\" style='padding-left:5px;width:360px' onkeyup=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" value='" + dr["valor_texto"].ToString() + "' MaxLength='50'>");
                        break;
                    case "F":
                    case "H":
                        string strFecha = "";
                        if (dr["valor_fecha"] == System.DBNull.Value) strFecha = "";
                        else strFecha = DateTime.Parse(dr["valor_fecha"].ToString()).ToShortDateString();

                        if ((dr["t291_idtipodato"].ToString()) == "F") sb.Append("<td align='left' colspan='5'>");
                        else sb.Append("<td align='left'>");

                        if (Session["BTN_FECHA"].ToString() == "I")
                            sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' style='width:60px; cursor:pointer' value='" + strFecha + "' Calendar='oCal' onclick='mc(event);' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" />");
                        //sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' onFocus=\"this.className='txtFecM';\" onblur=\"this.className='txtFecL'\" style='width:60px; cursor: pointer' value='" + strFecha + "' Calendar='oCal' onclick='mc(event);' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" />");
                        else
                            sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' style='width:60px; cursor:pointer' value='" + strFecha + "' Calendar='oCal' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" onfocus='focoFecha(event)' onmousedown='mc1(event)' />");
                        //sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' onFocus=\"this.className='txtFecM';\" onblur=\"this.className='txtFecL'\" style='width:60px; cursor: pointer' value='" + strFecha + "' Calendar='oCal' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" onfocus='focoFecha(event)' onmousedown='mc1(event)' />");
                        break;
                }
                string sCad = "";
                if ((dr["t291_idtipodato"].ToString()) == "H")
                {
                    sb.Append("</td><td>");
                    #region Hora
                    if (dr["valor_fecha"] == System.DBNull.Value) iSel = 0;
                    else iSel = DateTime.Parse(dr["valor_fecha"].ToString()).Hour;

                    sComboHoras = "<select disabled class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";

                    for (int i = 0; i < 24; i++)
                    {
                        if (i < 10) sCad = "0" + i.ToString();
                        else sCad = i.ToString();
                        sComboHoras += "<option value='" + sCad + "'";
                        if (i == iSel) sComboHoras += "selected";
                        sComboHoras += ">" + sCad + "</option>";
                    }
                    sComboHoras += "</select>";
                    sb.Append(sComboHoras + "</td>");
                    #endregion
                    #region Minuto
                    sb.Append("<td>");


                    if (dr["valor_fecha"] == System.DBNull.Value) iSel = 0;
                    else iSel = DateTime.Parse(dr["valor_fecha"].ToString()).Minute;

                    sComboMinutos = "<select disabled class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";

                    for (int i = 0; i < 60; i++)
                    {
                      
                        if (i < 10) sCad = "0" + i.ToString();
                        else sCad = i.ToString();

                        sComboMinutos += "<option value='" + sCad + "'";
                        if (i == iSel) sComboMinutos += "selected";
                        sComboMinutos += ">" + sCad + "</option>";
                    }
                    sComboMinutos += "</select>";
                    sb.Append(sComboMinutos + "</td>");
                    #endregion
                    #region segundo
                    sb.Append("<td>");

                    if (dr["valor_fecha"] == System.DBNull.Value) iSel = 0;
                    else iSel = DateTime.Parse(dr["valor_fecha"].ToString()).Second;

                    sComboSegundos = "<select disabled class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";

                    for (int i = 0; i < 60; i++)
                    {
                        
                        if (i < 10) sCad = "0" + i.ToString();
                        else sCad = i.ToString();

                        sComboSegundos += "<option value='" + sCad + "'";
                        if (i == iSel) sComboSegundos += "selected";
                        sComboSegundos += ">" + sCad + "</option>";
                    }
                    sComboSegundos += "</select>";
                    sb.Append(sComboSegundos);
                    sb.Append("</td><td>");
                    #endregion
                }
                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            strTablaCampoValor = sb.ToString();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los campos-valor asociados a la tarea", ex);
        }
        return sResul;
    }
    private string ObtenerCamposPT(int idPT)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";
        string sComboHoras;
        string sComboMinutos;
        string sComboSegundos;
        string slCamposPT = "";
        int iSel;

        try
        {
            sb.Append("<table id='tblCamposPT' class='texto' mantenimiento='0' style='width: 870px;'>");
            sb.Append(@"<colgroup><col style='width:20px;' /><col style='width:320px;' /><col style='width:60px'/>
                        <col style='width:80px;' /><col style='width:40px;' /><col style='width:40px;' />
                        <col style='width:40px;' /><col style='width:250px;' /></colgroup>");
            SqlDataReader dr = SUPER.DAL.CampoPT.obtenerCamposValor(null, idPT);
            while (dr.Read())
            {
                slCamposPT += dr["IDENTIFICADOR"].ToString() + ",";
                sb.Append("<tr id='");
                sb.Append(dr["IDENTIFICADOR"].ToString());
                sb.Append("' tipodato='" + dr["t291_idtipodato"].ToString());
                sb.Append("' bd='' onclick='ms(this)' style='height:20px;'>");

                sb.Append("<td><img src='../../../images/imgFN.gif'></td><td>");
                sb.Append("<nobr class='NBR W320' onmouseover='TTip(event)'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t291_denominacion"].ToString() + "</td>");
                switch (dr["t291_idtipodato"].ToString())
                {
                    case "I":
                        string strImporte = "";
                        if (dr["valor_money"] == System.DBNull.Value) strImporte = "";
                        else strImporte = decimal.Parse(dr["valor_money"].ToString()).ToString("N");

                        sb.Append("<td align='left' colspan='5'><input type='text' class='txtNumL' style='width:70px;text-align:left; margin-left:4px;' value=\"" + strImporte + "\" >");
                        break;
                    case "T":
                        sb.Append("<td align='left' colspan='5'><input type='text' class='txtL' style='padding-left:5px;width:440px'  value='" + dr["valor_texto"].ToString() + "' MaxLength='50'>");
                        break;
                    case "F":
                    case "H":
                        string strFecha = "";
                        if (dr["valor_fecha"] == System.DBNull.Value) strFecha = "";
                        else strFecha = DateTime.Parse(dr["valor_fecha"].ToString()).ToShortDateString();

                        if ((dr["t291_idtipodato"].ToString()) == "F") sb.Append("<td align='left' colspan='5'>");
                        else sb.Append("<td align='left'>");

                        if (Session["BTN_FECHA"].ToString() == "I")
                            sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' style='width:60px; cursor:pointer' value='" + strFecha + "' />");
                        else
                            sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' style='width:60px; cursor:pointer' value='" + strFecha + "' />");
                        break;
                }
                string sCad = "";
                if ((dr["t291_idtipodato"].ToString()) == "H")
                {
                    sb.Append("</td><td>");
                    #region Hora
                    if (dr["valor_fecha"] == System.DBNull.Value) iSel = 0;
                    else iSel = DateTime.Parse(dr["valor_fecha"].ToString()).Hour;

                    sComboHoras = "<select disabled class='combo' style='width:39px;' >";

                    for (int i = 0; i < 24; i++)
                    {
                        sComboHoras += "<option value='" + i.ToString() + "'";
                        if (i == iSel) sComboHoras += "selected";

                        if (i < 10) sCad = "0" + i.ToString();
                        else sCad = i.ToString();

                        sComboHoras += ">" + sCad + "</option>";
                    }
                    sComboHoras += "</select>";
                    sb.Append(sComboHoras + "</td>");
                    #endregion
                    #region Minuto
                    sb.Append("<td>");


                    if (dr["valor_fecha"] == System.DBNull.Value) iSel = 0;
                    else iSel = DateTime.Parse(dr["valor_fecha"].ToString()).Minute;

                    sComboMinutos = "<select disabled class='combo' style='width:39px;'>";

                    for (int i = 0; i < 60; i++)
                    {
                        sComboMinutos += "<option value='" + i.ToString() + "'";
                        if (i == iSel) sComboMinutos += "selected";

                        if (i < 10) sCad = "0" + i.ToString();
                        else sCad = i.ToString();

                        sComboMinutos += ">" + sCad + "</option>";
                    }
                    sComboMinutos += "</select>";
                    sb.Append(sComboMinutos + "</td>");
                    #endregion
                    #region segundo
                    sb.Append("<td>");

                    if (dr["valor_fecha"] == System.DBNull.Value) iSel = 0;
                    else iSel = DateTime.Parse(dr["valor_fecha"].ToString()).Second;

                    sComboSegundos = "<select disabled class='combo' style='width:39px;'>";

                    for (int i = 0; i < 60; i++)
                    {
                        sComboSegundos += "<option value='" + i.ToString() + "'";

                        if (i == iSel) sComboSegundos += "selected";

                        if (i < 10) sCad = "0" + i.ToString();
                        else sCad = i.ToString();

                        sComboSegundos += ">" + sCad + "</option>";
                    }
                    sComboSegundos += "</select>";
                    sb.Append(sComboSegundos);
                    sb.Append("</td><td>");
                    #endregion
                }
                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            strTablaCampoValor = sb.ToString();
            sResul = sb.ToString() + "///" + slCamposPT;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los campos-valor asociados al proyecto técnico", ex);
        }
        return sResul;
    }
    private string recuperarPSN(string nPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nPSN), (int)Session["UsuarioActual"], "PST");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0 t305_admiterecursospst
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //4
                sb.Append(dr["estado"].ToString() + "@#@");  //5
                if ((bool)dr["t305_admiterecursospst"])
                    sb.Append("T");  //6
                else
                    sb.Append("F");  //6
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }
    public void ObtenerModosFacturacion()
    {
        SqlDataReader dr = MODOFACTSN2.ObtenerActivosMasActual(nModoFact, nPSN);
        cboModoFacturacion.Items.Clear();
        cboModoFacturacion.Items.Add(new ListItem("", "0"));
        ListItem oLI = null;
        while (dr.Read())
        {
            oLI = new ListItem(dr["t324_denominacion"].ToString(), dr["t324_idmodofact"].ToString());
            cboModoFacturacion.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();
    }
    private string getModosFacturacion(string nPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = MODOFACTSN2.ObtenerActivosMasActual(0, int.Parse(nPSN));
            while (dr.Read())
            {
                sb.Append(dr["t324_idmodofact"].ToString() + "##" + dr["t324_denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar los modos de facturación.", ex);
        }
    }

    private string getCamposPorAmbito(int codAmbito, string sCamposPT)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        var lstCamposPT = new ArrayList();
        sb.Append("<table id='tblDatos' class='texto MAM' style='width:180px;'>");
        sb.Append("<colgroup><col style='width:140px;' /></colgroup><tbody>");
        try
        {
            string[] aTabla = Regex.Split(sCamposPT, ",");
            foreach (string sElem in aTabla)
            {
                if (sElem != "")
                    lstCamposPT.Add(sElem);
            }

            SqlDataReader dr = SUPER.Capa_Datos.CAMPOS.Catalogo(int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()),
                                                                     codAmbito,
                                                                     "9",
                                                                     int.Parse(hdnT305IdProy.Value),
                                                                     lstCamposPT);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' title='" + dr["DENOMINACION"].ToString() + "' ");
                sb.Append("tipodato='" + dr["t291_idtipodato"].ToString() + "' descdato='" + dr["t291_denominacion"].ToString() + "' ");
                sb.Append(" onclick='ms(this);' ondblclick='asociarCampo(this, true)' onmousedown='DD(event);' style='height:20px;'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W325'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
            strTablaHTML = sb.ToString();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los campos del catálogo.", ex);
        }
        return sResul;
    }
}

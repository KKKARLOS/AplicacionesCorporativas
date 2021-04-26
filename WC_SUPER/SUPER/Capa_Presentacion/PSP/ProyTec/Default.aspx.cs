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

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, sLectura, gsAcceso, gsModo, sNodo = "";
    public int nIdPT, nPE;//, nCR;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaRecursos, strTablaTareas, strTablaAECR, strTablaAET, strArrayVAE, strIdCliente,
                  strTablaPoolGF, strTablaPoolProf, strTablaRTPTs, strTablaHTML = "";
    public string strTablaCampoValor = "<table id='tblCampos' style='width: 600px;' mantenimiento='1'><colgroup><col style='width:20px' /><col style='width:430px' /><col style='width:100px' /></colgroup></table>";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sIdNodo = "";
        try
        {
            #region Registrar el CallBack
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            #endregion
            if (!Page.IsCallback)
            {
                #region Control e acceso
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                #endregion
                #region Inicialización de variables
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //    this.imgForaneo2.Visible = false;
                //    this.lblForaneo2.Visible = false;
                //    this.imgForaneo3.Visible = false;
                //    this.lblForaneo3.Visible = false;
                //}
                sErrores = "";
                strTablaRecursos = "<table id='tblAsignados'></table>";
                strTablaRTPTs = "<table id='tblAsignados2'></table>";
                strTablaTareas = "";
                strTablaAECR = "";
                strArrayVAE = "";
                strTablaPoolGF = "<table id='tblPoolGF'></table>";
                strTablaPoolProf = "<table id='tblAsignados3'></table>";
                sLectura = "false";
                gsModo = "";
                try
                {
                    Utilidades.SetEventosFecha(this.txtValIni);
                    Utilidades.SetEventosFecha(this.txtValFin);
                    Utilidades.SetEventosFecha(this.txtFIPRes);
                    Utilidades.SetEventosFecha(this.txtFFPRes);
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblCR.InnerText = sNodo;
                    this.lblCR.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    //              this.lblCR.Attributes.Add("class", "enlace");

                    this.chkHeredaCR.Text = " " + Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    rdbAmbito.Items[1].Text = sNodo + "  ";
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener los datos del proyecto técnico", ex);
                }
                #endregion
                #region Recoger parámetros de entrada
                if (Request.QueryString["sm"] != null)//sModo
                {
                    gsModo = Utilidades.decodpar(Request.QueryString["sm"].ToString());
                    if (gsModo == "B" || gsModo == "C")
                    {
                        this.hdnIdPE.ReadOnly = false;
                        this.txtIdPT.ReadOnly = false;
                    }
                    else
                    {
                        this.hdnIdPE.ReadOnly = true;
                        this.txtIdPT.ReadOnly = true;
                    }
                }
                if (Request.QueryString["es"] != null)//Estr
                    this.hdnEstr.Text = Utilidades.decodpar(Request.QueryString["es"].ToString());
                if (Request.QueryString["cr"] == null)//nCR
                {
                    if (Request.Form["nCR"] != null)
                    {
                        if (Request.Form["nCR"] != "" && Request.Form["nCR"].ToString() != "undefined")
                            sIdNodo = Request.Form["nCR"].ToString();
                    }
                }
                else
                    sIdNodo = Utilidades.decodpar(Request.QueryString["cr"].ToString());
                if (Request.QueryString["pt"] != null || Request.Form["nIdPT"] != null)
                {
                    string sIdPT = "";

                    //this.hdnIdPE.ReadOnly = true;
                    //this.txtIdPT.ReadOnly = true;

                    if (Request.Form["nIdPT"] != null)
                    {
                        if (Request.Form["nIdPT"] != "")
                            sIdPT = Request.Form["nIdPT"].ToString();
                    }
                    else
                        sIdPT = Utilidades.decodpar(Request.QueryString["pt"].ToString());

                    sIdPT = sIdPT.Replace(".", "");
                    if (sIdPT != "")
                    {
                        try { nIdPT = int.Parse(sIdPT); }
                        catch (Exception)
                        {
                            sErrores += Errores.mostrarError("El código de proyecto técnico no es numérico.");
                            return;
                        }
                        if (gsAcceso != "" && gsAcceso != null)
                            this.hdnAcceso.Text = gsAcceso;
                        else
                        {
                            if (Request.Form["Permiso"] != null)
                            {
                                if (Request.Form["Permiso"] != "")
                                    this.hdnAcceso.Text = Request.Form["Permiso"].ToString();
                            }
                            else
                            {
                                if (Request.QueryString["pm"] != null)//Permiso
                                    this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["pm"].ToString());
                            }
                        }
                        //if (this.hdnAcceso.Text == "") this.hdnAcceso.Text = "R";
                        try
                        {
                            ObtenerDatosPT();
                            if (sIdNodo == "")
                                sIdNodo = this.hdnCRActual.Value;
                            ObtenerTarifas();
                            ObtenerValoresAtributosEstadisticosCR(sIdNodo);
                            this.hdnLstAEsTareas.Value = ObtenerAtributosEstadisticosTareas(nIdPT);
                            this.hdnLstCamposTareas.Value = ObtenerCamposTareas(nIdPT);
                        }
                        catch (Exception ex)
                        {
                            sErrores += Errores.mostrarError("Error al obtener los datos del proyecto técnico", ex);
                        }
                    }
                }
                else
                {//Hemos entrado a dar de alta un proyecto técnico
                    try
                    {
                        ObtenerValoresAtributosEstadisticosCR(sIdNodo);
                        PonerRecursosAsociados();
                        PonerRTPT();
                    }
                    catch (Exception ex)
                    {
                        sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
                    }
                }
                #endregion
                #region Establecer modo de acceso
                if (this.hdnAcceso.Text == "R")
                {
                    ModoLectura.Poner(this.Controls);
                    sLectura = "true";
                }
                else
                {//Si estoy en modo V es que soy el RTPT del proyecto técnico luego debe estar todo en modo lectura menos la asignación de técnicos y las observaciones
                    if (this.hdnAcceso.Text == "V")
                    {
                        //sLectura = "true";
                        ModoLectura.Poner(this.Controls);
                        //Activo los controles de la pestaña de asignación de técnicos
                        this.rdbAmbito.Enabled = true;
                        this.txtApellido.ReadOnly = false;
                        this.txtApellido2.ReadOnly = false;
                        this.txtNombre.ReadOnly = false;
                        this.cboAmbito.Enabled = true;
                        this.txtObservaciones.ReadOnly = false;
                    }
                }
                #endregion
            }
        }
        catch (Exception eGen)
        {
            throw (new Exception("Error al cargar la página de detalle de PT.\r\n" + eGen.Message));
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad = "", sCad2 = "", sCad3 = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("tecnicosPool"):
                sResultado += ObtenerTecnicosPool(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("getRecursos"):
                bool bMostrarBajas = false;
                if (aArgs[3] == "S") bMostrarBajas = true;
                sResultado += "OK@#@" + ObtenerRecursosAsociados(aArgs[1], aArgs[2], bMostrarBajas);
                break;
            case ("getRecursosPool"):
                bool bMostrarBajasPool = false;
                if (aArgs[3] == "S") bMostrarBajasPool = true;
                sResultado += "OK@#@" + ObtenerPoolProf(aArgs[1], aArgs[2], bMostrarBajasPool);
                break;
            case ("ponerRTPT")://Pongo el pool de RTPT´s del PE o (si es vacío) el usuario actual como RTPT por defecto
                sResultado += PonerRTPT2(aArgs[1], aArgs[2]);
                break;
            case ("rtpt")://carga lista de candidatos a RTPT
                sResultado += ObtenerTecnicos2(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("getFase")://carga las actividades de una fase en la pestaña de tareas
                sResultado += "OK@#@" + ObtenerTareas(aArgs[1], "F");
                break;
            case ("getActiv")://carga las tareas de una actividad en la pestaña de tareas
                sResultado += "OK@#@" + ObtenerTareas(aArgs[1], "A");
                break;
            case ("documentos"):
                //sCad = ObtenerDocumentos(aArgs[1]);
                //if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                //else sResultado += "OK@#@" + sCad;
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
                break;
            case ("borrar"):
                sResultado += Borrar(aArgs[1]);
                break;
            case ("getDatosPestana"):
                //sResultado += "OK@#@" + aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://PROFESIONALES
                        sCad = ObtenerRecursosAsociados(aArgs[2], aArgs[4],false);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 2://RESPONSABLES
                        if (aArgs[2] != "")
                        {
                            sCad = ObtenerRTPTs(aArgs[2], aArgs[4]);
                            if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                            else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        }
                        else sResultado += "OK@#@";
                        break;
                    case 3://AVANZADO
                        sCad = ObtenerAtributosEstadisticosCR(aArgs[4]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else
                        {
                            sCad2 = ObtenerAtributosEstadisticosPT(aArgs[2]);
                            if (sCad2.IndexOf("Error@#@") >= 0) sResultado += sCad2;
                            else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad + "///" + sCad2;
                        }
                        sCad3 = ObtenerValoresAtributosEstadisticosCR(aArgs[4]);
                        sResultado += "///" + sCad3;
                        break;
                    case 4://TAREAS
                        sCad = ObtenerTareas(aArgs[2], "PT");
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 5://CONTROL
                        break;
                    case 6://DOCUMENTACION
                        //sCad = ObtenerDocumentos(aArgs[2]);
                        sCad = Utilidades.ObtenerDocumentos("PT", int.Parse(aArgs[2]), aArgs[5], aArgs[6]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else
                        {
                            sCad2 = ObtenerListaDocumentos(aArgs[2], int.Parse(Session["UsuarioActual"].ToString()));
                            if (sCad2.IndexOf("Error@#@") >= 0) sResultado += sCad2;
                            else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad + "///" + sCad2;
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
                        sCad = ObtenerPoolProf(aArgs[2], aArgs[3],false);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else
                        {
                            sCad2 = ObtenerPoolGF(aArgs[2]);
                            if (sCad2.IndexOf("Error@#@") >= 0) sResultado += sCad2;
                            else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad + "///" + sCad2;
                        }
                        //11/10/2001 Además devolvemos el nº de empleados del Nodo
                        int iNumEmp = NODO.GetNumEmpleados(null, int.Parse(aArgs[3]));
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
                        sResultado += "@#@" + getCamposPorAmbito(99);
                        break;
                }
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("buscarPT"):
                sResultado += buscarPT(aArgs[1]);
                break;
            case ("mostrarTareasConAE"):
                sResultado += "OK@#@" + mostrarTareasConAE(aArgs[1], aArgs[2]);
                break;
            case ("cargarCamposPorAmbito"):
                sResultado += "OK@#@" + getCamposPorAmbito(int.Parse(aArgs[1]));
                break;
            case ("mostrarTareasConCampo"):
                sResultado += "OK@#@" + mostrarTareasConCampo(aArgs[1], aArgs[2]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private void ObtenerDatosPT()
    {
        //this.hdnAcceso.Text = Permiso(o.t305_idproyectosubnodo.ToString(), nIdPT.ToString());
        if (gsModo != "B")
        {
            if ((this.hdnAcceso.Text == "") || (this.hdnAcceso.Text == "N"))
            {
                txtIdPT.Text = "";
                sErrores += Errores.mostrarError("El proyecto técnico " + nIdPT.ToString() + " no es accesible");
                return;
            }
        }

        ProyTec o = ProyTec.Obtener(nIdPT);

        hdnT305IdProy.Value = o.t305_idproyectosubnodo.ToString();
        hdnIdPE.Text = o.num_proyecto.ToString("#,###");
        hdnOrden.Text = o.t331_orden.ToString();
        txtIdPT.Text = o.t331_idpt.ToString("#,###");
        txtDesPT.Text = o.t331_despt;
        txtDesPT.ToolTip = o.t331_despt;
        txtDescripcion.Text = o.t331_desptlong;
        if (o.t346_idpst == 0) txtIdPST.Text = "";
        else txtIdPST.Text = o.t346_idpst.ToString();
        txtCodPST.Text = o.t346_codpst;
        txtDesPST.Text = o.t346_despst;
        //Estado del proyecto económico
        this.txtEstado.Text = o.t301_estado.ToString();
        switch (this.txtEstado.Text)
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

        this.txtCualidad.Text = o.t305_cualidad.ToString();
        //Estado del proyecto técnico
        cboEstado.Text = o.t331_estado.ToString();
        //Acceso desde IAP
        cboAccesoIAP.Text = o.t331_acceso_iap.ToString();

        if (o.t331_obligaest) chkObligaEst.Checked = true;
        if (o.t331_heredanodo) chkHeredaCR.Checked = true;
        else chkHeredaCR.ToolTip = "Asigna de forma automática a todos los profesionales presentes y futuros del " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + ". La opción está deshabilitada por bloqueo desde el proyecto económico.";
        if (o.t331_heredaproyeco) chkHeredaPE.Checked = true;
        //Si el proyectosubnodo no admite recursos desde PST el campo debe estar deshabilitado
        if (!o.t305_admiterecursospst)
        {
            chkHeredaCR.Enabled = false;
            imgDelBajaPE.Visible=false;
        }
        if (!o.t305_avisorecursopst)
        {
            imgCorreo.ImageUrl = "../../../Images/imgCorreoBloqueado.gif";
            imgCorreo.ToolTip = "La notificación de asignación de profesionales a la tarea, está bloqueada por parametrización a nivel de proyecto económico.";
        }
        this.hdnCRActual.Value = o.cod_une.ToString();
        this.hdnDesCRActual.Value = o.t303_denominacion;
        this.hdnAccBitacora.Value = o.t305_accesobitacora_pst;
        this.hdnNivelPresupuesto.Value = o.t305_nivelpresupuesto.ToString();

        if (o.t305_nivelpresupuesto == "P")
        {
            txtAvanReal.Text = o.t331_avance.ToString("N");
            if (o.t331_avanceauto) chkAvanceAuto.Checked = true;
            else chkAvanceAuto.Checked = false;
        }

        txtObservaciones.Text = o.t331_observaciones;

        //VIGENCIA
        //Obtengo la menor de las fechas de vigencia de sus tareas
        if (o.t332_fiv.Year > 1900) txtValIni.Text = o.t332_fiv.ToShortDateString();
        //Obtengo la mayor de las fechas de vigencia de sus tareas
        if (o.t332_ffv.Year > 1900) txtValFin.Text = o.t332_ffv.ToShortDateString();
        //PLANIFICACION
        //Obtengo la menor de las fechas de inicio planificadas de sus tareas
        if (o.t332_fipl.Year > 1900) txtPLIni.Text = o.t332_fipl.ToShortDateString();
        //Obtengo la mayor de las fechas de inicio planificadas de sus tareas
        if (o.t332_ffpl.Year > 1900) txtPLFin.Text = o.t332_ffpl.ToShortDateString();
        //if (o.t332_etpl > 0) txtPLEst.Text = o.t332_etpl.ToString("N");
        if (o.t332_etpl > 0) txtPLEst.Text = o.t332_etpl.ToString("N");
        //PREVISION
        //Obtengo la mayor de las fechas de inicio planificadas de sus tareas
        if (o.t332_ffpr.Year > 1900) txtPRFin.Text = o.t332_ffpr.ToShortDateString();
        //if (o.t332_etpr > 0) txtPREst.Text = o.t332_etpr.ToString("N");
        if (o.t332_etpr > 0) txtPREst.Text = o.t332_etpr.ToString("N");

        txtPE.Text = o.nom_proyecto;
        nPE = o.num_proyecto;
        strIdCliente = o.cod_cliente.ToString();
        chkCliente.Text += o.nom_cliente;
        //Presupuesto
        txtPresupuesto.Text = o.nPresupuesto.ToString("N");

        //Datos de IAP
        if (o.dPrimerConsumo.Year > 1900) txtPriCon.Text = o.dPrimerConsumo.ToShortDateString();
        if (o.dUltimoConsumo.Year > 1900) txtUltCon.Text = o.dUltimoConsumo.ToShortDateString();
        if (o.dFinEstimado.Year > 1900) txtFinEst.Text = o.dFinEstimado.ToShortDateString();
        txtTotEst.Text = o.nTotalEstimado.ToString("N");
        txtConHor.Text = o.nConsumidoHoras.ToString("N");
        txtConJor.Text = o.nConsumidoJornadas.ToString("N");
        if (o.nTotalEstimado != 0)
            txtPteEst.Text = o.nPendienteEstimado.ToString("N");

        if (o.nConsumidoHoras > 0 && txtPREst.Text != "")
        {
            txtAvanTeo.Text = ((o.nConsumidoHoras * 100 / double.Parse(txtPREst.Text))).ToString("N");
        }
        if (o.nConsumidoHoras > 0 && o.nTotalEstimado > 0)
        {
            txtAvanPrev.Text = ((o.nConsumidoHoras * 100 / o.nTotalEstimado)).ToString("N");
        }
        //Modo de acceso
        this.hdnModoAcceso.Value = ProyTec.getAcceso(null, nIdPT, int.Parse(Session["UsuarioActual"].ToString()));
        if (this.hdnAcceso.Text == "")
            this.hdnAcceso.Text = this.hdnModoAcceso.Value;

        hdnEsReplicable.Value = (o.t301_esreplicable) ? "1" : "0";
    }
    private string ObtenerRecursosAsociados(string sCodPT, string sCodUne, bool bMostrarBajas)
    {
        //Relacion de tecnicos asignados al proyecto tecnico
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAsignados' class='texto MANO' style='width:390px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:20px;' /><col style='width:305px;' /><col style='width:25px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            if (sCodPT != "")
            {
                int iCodPT = int.Parse(sCodPT.Replace(".", ""));
                SqlDataReader dr = ProyTec.CatalogoRecursos(iCodPT, bMostrarBajas);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;' bd='' ac='' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");
                    sb.Append(">");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W300'>" + dr["empleado"].ToString() + "</nobr></td>");
                    sb.Append("<td style='text-align:right;padding-right:2px;cursor: url(../../../images/imgManoAzul2.cur),pointer;' ondblclick='mostrarTareasRecurso(" + dr["t314_idusuario"].ToString() + ");'>" + dr["num_tareas"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                dr.Close(); dr.Dispose();
            }
            sb.Append("</tbody></table>");
            strTablaRecursos = sb.ToString();
            sResul = strTablaRecursos;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales asociados al proyecto técnico.", ex);
        }

        return sResul;
    }
    private void PonerRecursosAsociados()
    {
        //Relacion de tecnicos asignados al proyecto tecnico
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAsignados' class='texto' style='width:390px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:20px' /><col style='width:20px' /><col style='width:20px' /><col style='width:315px' /><col style='width:15px;' /></colgroup>");
            sb.Append("</table>");
            strTablaRecursos = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales asociados al proyecto técnico.", ex);
        }

    }
    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3, string sCodUne,
                                     string t305_idProyectoSubnodo, string sCualidad)
    {
        //Relacion de técnicos candidatos a ser asignados al proyecto técnico
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();

        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);

            //SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "");
            SqlDataReader dr = Recurso.GetUsuariosPSN(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "");
            sb.Append("<table id='tblRelacion' class='texto MAM' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr  id='" + dr["t314_idusuario"].ToString() + "' idTarifa='" + dr["IDTARIFA"].ToString());
                sb.Append("' style='height:20px;noWrap:true;' ");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                //else sb.Append("tipo='N' ");

                //if (this.hdnAcceso.Text != "R")
                //{
                //    sb.Append("onclick='mm(event);' ondblclick='insertarRecurso(this);' onmousedown='DD(event)'");
                //}
                sb.Append("><td></td><td><nobr class='NBR W330'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }
    /// <summary>
    /// Obtiene relación de técnicos para su asignación como RTPTs
    /// </summary>
    /// <param name="strOpcion"></param>
    /// <param name="strValor1"></param>
    /// <param name="strValor2"></param>
    /// <param name="strValor3"></param>
    /// <param name="sCodUne"></param>
    /// <param name="t305_idProyectoSubnodo"></param>
    /// <param name="sCualidad"></param>
    /// <returns></returns>
    protected string ObtenerTecnicos2(string strOpcion, string strValor1, string strValor2, string strValor3, string sCodUne,
                                      string t305_idProyectoSubnodo, string sCualidad)
    {
        //Relacion de técnicos candidatos a ser asignados al proyecto técnico como responsables técnicos
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            bool bForaneos = false;
            //if ((bool)Session["FORANEOS"])
            //{
                if (Session["FIGURASFORANEOS"].ToString().IndexOf("W") > -1)
                    bForaneos = true;
            //}
            SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, Utilidades.unescape(strValor1),
                                                   Utilidades.unescape(strValor2), Utilidades.unescape(strValor3),
                                                   sCodUne, t305_idProyectoSubnodo, sCualidad, "",true);
            sb.Append("<table id='tblRelacion2' class='texto MAM' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                if (dr["tipo"].ToString() != "F" || bForaneos)
                {
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;noWrap:true;' ");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                    if (this.hdnAcceso.Text != "R")
                    {
                        sb.Append(" onclick='mm(event);' ondblclick='insertarRecurso2(this);' onmousedown='DD(event)'");
                    }
                    sb.Append("><td></td><td><nobr class='NBR W330'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }

    protected string ObtenerTareas(string sCodElem, string sNivelEst)
    {
        //Relacion de tareas del proyecto tecnico
        string sResul = "", sCad, sFecha;
        int iCodElem;
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            if (sCodElem != "")//Si estoy creando un elemento no tiene sentido obtener sus tareas
            {
                iCodElem = int.Parse(sCodElem.Replace(".", ""));
                #region leer tareas con estructura
                string sDesTipo, sWidth = "";
                int iMargen;
                if (sNivelEst == "PT")
                {
                    sb.Append("<table id='tblTareas' class='texto' style='width: 850px; text-align:left'>");
                    sb.Append("<colgroup>");
                    sb.Append("    <col style='width:285px;'/>");
                    sb.Append("    <col style='width:55px;'/>");
                    sb.Append("    <col style='width:65px;'/>");
                    sb.Append("    <col style='width:65px;' />");
                    sb.Append("    <col style='width:55px;'/>");
                    sb.Append("    <col style='width:65px;'/>");
                    sb.Append("    <col style='width:65px;'/>");
                    sb.Append("    <col style='width:65px;'/>");
                    sb.Append("    <col style='width:65px;'/>");
                    sb.Append("    <col style='width:65px;'/>");
                    sb.Append("</colgroup>");
                    sb.Append("<tbody>");
                }
                switch (sNivelEst)
                {
                    case "PT":
                        dr = ProyTec.CatalogoTareasPT(iCodElem);
                        sWidth = "230";
                        break;
                    case "F":
                        dr = FASEPSP.CatalogoTareasFase(iCodElem);
                        sWidth = "210";
                        break;
                    case "A":
                        dr = ACTIVIDADPSP.CatalogoTareas(iCodElem);
                        sWidth = "190";
                        break;
                    default:
                        dr = null;
                        break;
                }
                while (dr.Read())
                {
                    sDesTipo = dr["Tipo"].ToString();

                    sb.Append("<tr id='" + int.Parse(dr["codElem"].ToString()) + "' otc='" + dr["idotc"].ToString() +
                              "' nivel='1' desplegado='0' tipo='" + sDesTipo + "' style='height:20px;'><td style='padding-left:3px;text-align:left;'>");
                    iMargen = int.Parse(dr["margen"].ToString());// -20;
                    switch (sDesTipo)
                    {
                        case "F":
                            sb.Append("<img src='../../../Images/plus.gif' onclick='mostrar(this);' style='margin-left:" + iMargen.ToString() + "px;cursor:pointer;'>");
                            sb.Append("<img src='../../../Images/imgFaseOff.gif' border='0' title='Fase' class='ICO'>");
                            sb.Append("<nobr onmouseover='TTip(event)' class='NBR W" + sWidth + "'>" + dr["Nombre"].ToString() + "</nobr></td>");
                            break;
                        case "A":
                            sb.Append("<img src='../../../Images/plus.gif' onclick='mostrar(this);' style='margin-left:" + iMargen.ToString() + "px;cursor:pointer;'>");
                            sb.Append("<img src='../../../Images/imgActividadOff.gif' border='0' title='Actividad' class='ICO'>");
                            sb.Append("<nobr onmouseover='TTip(event)' class='NBR W" + sWidth + "'>" + dr["Nombre"].ToString() + "</nobr></td>");
                            break;
                        case "T":
                            sb.Append("<img src='../../../Images/imgTrans9x9.gif' style='margin-left:" + iMargen.ToString() + "px;cursor:auto;'>");
                            sb.Append("<img src='../../../Images/imgTareaOff.gif' border='0' title='Tarea' class='ICO'>");
                            sb.Append("<nobr onmouseover='TTip(event)' class='NBR W" + sWidth + "'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["Nombre"].ToString() + "</nobr></td>");
                            break;
                    }

                    //sb.Append("</td><td>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + "</td>");
                    //sMargen = " style='margin-left:" + dr["margen"].ToString() + "px' ";
                    //sb.Append("<td onmouseover='TTip(event);'><nobr" + sMargen + " class='NBR W220'>" + dr["Nombre"].ToString() + "</nobr></td>");
                    if (dr["duracion"] != DBNull.Value)
                        sb.Append("<td style='text-align:right;padding-right:5px;'>" + double.Parse(dr["duracion"].ToString()).ToString("N") + "</td>");
                    else
                        sb.Append("<td style='text-align:right;padding-right:5px;'></td>");
                    sFecha = dr["inicio"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["inicio"].ToString()).ToShortDateString();
                    sb.Append("<td>" + sFecha + "</td>");
                    sCad = dr["fin"].ToString();
                    if (sCad != "")
                    {
                        sFecha = DateTime.Parse(sCad).ToShortDateString();
                    }
                    else sFecha = "";
                    sb.Append("<td style='text-align:right;padding-right:5px;'>" + sFecha + "</td>");
                    if (dr["duracionPr"] != DBNull.Value)
                        sb.Append("<td style='text-align:right;padding-right:5px;'>" + double.Parse(dr["duracionPr"].ToString()).ToString("N") + "</td>");
                    else
                        sb.Append("<td></td>");
                    sFecha = dr["finPr"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["finPr"].ToString()).ToShortDateString();
                    sb.Append("<td>" + sFecha + "</td>");
                    sFecha = dr["iniVig"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["iniVig"].ToString()).ToShortDateString();
                    sb.Append("<td>" + sFecha + "</td>");
                    sFecha = dr["finVig"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["finVig"].ToString()).ToShortDateString();
                    sb.Append("<td>" + sFecha + "</td>");
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["Consumo"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td><nobr class='NBR W60'>" + dr["otc"].ToString() + "</nobr></td></tr>");
                }
                if (sNivelEst == "PT")
                {
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                }
                #endregion

                dr.Close(); 
                dr.Dispose();
                strTablaTareas = sb.ToString();
                sResul = strTablaTareas;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de tareas.", ex);
        }
        return sResul;
    }

    protected string Grabar(string strDatosBasicos, string strDatosTarea, string strDatosRecursos, string strDatosPoolGF,
                            string strDatosPoolProf, string strDatosRTPT, string strDatosAE, string strDatosCampos)
    {
        string sResul = "", sCad, sOpcionBD, sIndicaciones, sAux, sIdPST, sRecTareas = "", sDesc, sObservaciones, sDescLong, sPresupuesto = "0.00", sAvance = "0";
        bool bObligaEst = false, bInsert, bHeredaNodo = false, bHeredaPE = false, bNotifExceso = false, bAvanceAuto = false;
        
        int iPos = 0, iCodPT, iCodRecurso, iTarifa, iT305IdProy, iCodCR;
        byte iEstado;
        short iOrden;
        DateTime dtFechaAlta = System.DateTime.Today;
        DateTime? dtFip=null, dtFfp=null;
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
            if (aDatosBasicos[0] == "") iCodPT = -1;
            else iCodPT = int.Parse(aDatosBasicos[0]);
            //El nodo se obtiene ahora del PE al que pertenece el PT por lo que siempre debe tener valor
            //if (aDatosBasicos[1] == "") iCodCR = short.Parse(Session["NodoActivo"].ToString());
            //else 
            iCodCR = int.Parse(aDatosBasicos[1]);
            if (aDatosBasicos[2] == "") iT305IdProy = -1;
            else iT305IdProy = int.Parse(aDatosBasicos[2]);           

            #region Datos generales
            if (strDatosTarea != "")//No se ha modificado nada de la pestaña general
            {
                string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
                ///aDatosTarea[0] = Des. proyecto técnico
                ///aDatosTarea[1] = Estado
                ///aDatosTarea[2] = chkObligaEst
                ///aDatosTarea[3] = Orden
                ///aDatosTarea[4] = txtIdPST
                ///aDatosTarea[5] = txtDescripcion
                ///aDatosTarea[6] = bFechaModificada
                ///aDatosTarea[7] = txtValIni
                ///aDatosTarea[8] = txtValFin
                ///aDatosTarea[9] = chkHeredaCR
                ///aDatosTarea[10] = chkHeredaPE
                ///aDatosTarea[11] = AccesoIAP
                ///aDatosTarea[12] = txtPresupuesto
                ///aDatosTarea[13] = txtObservaciones
                sDesc = Utilidades.unescape(aDatosTarea[0]);
                sDescLong = Utilidades.unescape(aDatosTarea[5]);
                sObservaciones = Utilidades.unescape(aDatosTarea[15]);
                if (iCodPT <= 0)
                {
                    bInsert = true;
                    strDatosRecursos = "";//no tiene sentido ya que si estamos insertando un PT no puede tener tareas a las que asignar recursos
                    iOrden = EstrProy.GetOrdenPT(iT305IdProy);
                }
                else
                {
                    bInsert = false;
                    iOrden = short.Parse(aDatosTarea[3]);
                }
                iEstado = byte.Parse(aDatosTarea[1]);
                if (aDatosTarea[2] == "1") bObligaEst = true;
                if (aDatosTarea[9] == "1") bHeredaNodo = true;
                if (aDatosTarea[10] == "1") bHeredaPE = true;
                if (aDatosTarea[12] != "") sPresupuesto = aDatosTarea[12];
                if (aDatosTarea[13] != "") sAvance = aDatosTarea[13];
                if (aDatosTarea[14] == "1") bAvanceAuto = true;

                sIdPST = aDatosTarea[4];
                if (bInsert)
                {
                    if (sIdPST == "")
                        iCodPT = ProyTec.Insert(tr, sDesc, iT305IdProy, iEstado, bObligaEst, iOrden, null, sDescLong, bHeredaNodo, bHeredaPE, aDatosTarea[11], decimal.Parse(sPresupuesto), decimal.Parse(sAvance), bAvanceAuto, sObservaciones);
                    else
                        iCodPT = ProyTec.Insert(tr, sDesc, iT305IdProy, iEstado, bObligaEst, iOrden, null, sDescLong, bHeredaNodo, bHeredaPE, aDatosTarea[11], decimal.Parse(sPresupuesto), decimal.Parse(sAvance), bAvanceAuto, sObservaciones);
                }
                else
                {
                    if (sIdPST == "")
                        ProyTec.Update(tr, iCodPT, sDesc, iT305IdProy, iEstado, bObligaEst, iOrden, null, sDescLong, bHeredaNodo, bHeredaPE, aDatosTarea[11], decimal.Parse(sPresupuesto), decimal.Parse(sAvance), bAvanceAuto, sObservaciones);
                    else
                        ProyTec.Update(tr, iCodPT, sDesc, iT305IdProy, iEstado, bObligaEst, iOrden, int.Parse(aDatosTarea[4]), sDescLong, bHeredaNodo, bHeredaPE, aDatosTarea[11], decimal.Parse(sPresupuesto), decimal.Parse(sAvance), bAvanceAuto, sObservaciones);
                }
                if (!bInsert)
                {
                    if (aDatosTarea[6] == "1")
                    {
                        DateTime? dIniV = null;
                        DateTime? dFinV = null;
                        if (aDatosTarea[7] != "") dIniV = DateTime.Parse(aDatosTarea[7]);
                        if (aDatosTarea[8] != "") dFinV = DateTime.Parse(aDatosTarea[8]);
                        ArrayList aTarea = new ArrayList();
                        //Hay que actualizar la fechas de vigencia de las tareas que dependen del PT.
                        SqlDataReader dr = TAREAPSP.SelectByt331_idpt(tr, iCodPT);
                        while (dr.Read())
                        {
                            aTarea.Add(dr["t332_idtarea"].ToString());
                        }
                        dr.Close();
                        dr.Dispose();
                        for (int i = 0; i < aTarea.Count; i++)
                        {
                            int nResul = TAREAPSP.ModificarFechasVigencia(tr, int.Parse(aTarea[i].ToString()), dIniV, dFinV);
                        }
                    }
                }
            }
            #endregion
            #region Recursos
            if (strDatosRecursos != "")
            {
                bool bAdmiteRecursoPSTCalculado = false;
                bool bAdmiteRecursoPST = false;
                bool bFechaAltaCalculada = false;
                int iUltCierreEco = 0;
                string[] aRecursos = Regex.Split(strDatosRecursos, "///");

                foreach (string oRec in aRecursos)
                {
                    string[] aValores = Regex.Split(oRec, "##");
                    ///aValores[0] = opcionBD;
                    ///aValores[1] = idRecurso;
                    ///aValores[2] = ffp;
                    ///aValores[3] = idTarifa;
                    ///aValores[4] = indicaciones;
                    ///aValores[5] = fip;
                    ///aValores[6] = bNotifExceso
                    if (aValores[0] != "")
                    {
                        sOpcionBD = aValores[0];
                        iCodRecurso = int.Parse(aValores[1]);
                        if (aValores[2] != "")
                            dtFfp = DateTime.Parse(aValores[2]);
                        sAux = aValores[3];
                        if (sAux == "") iTarifa = -1;
                        else iTarifa = int.Parse(sAux);
                        sIndicaciones = aValores[4];
                        if (aValores[5] != "")
                            dtFip = DateTime.Parse(aValores[5]);
                        if (aValores[6] == "1") bNotifExceso = true;
                        switch (sOpcionBD)
                        {
                            case "I"://insertar recurso en todas las tareas
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
                                        NODO o = NODO.Select(tr, iCodCR);
                                        iUltCierreEco = o.t303_ultcierreeco;
                                        bFechaAltaCalculada = true;
                                    }
                                }
                                ProyTec.AsignarTareas2(tr, iCodPT, iCodRecurso, dtFip, dtFfp, iTarifa, sIndicaciones, bNotifExceso,
                                                            bAdmiteRecursoPST, iT305IdProy, iCodCR, iUltCierreEco);
                                break;
                            case "A"://activar el recurso en todas las tareas e insertarlo donde no estuviera
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
                                        NODO o = NODO.Select(tr, iCodCR);
                                        iUltCierreEco = o.t303_ultcierreeco;
                                        bFechaAltaCalculada = true;
                                    }
                                }
                                ProyTec.AsignarTareas2(tr, iCodPT, iCodRecurso, dtFip, dtFfp, iTarifa, sIndicaciones, bNotifExceso,
                                                            bAdmiteRecursoPST, iT305IdProy, iCodCR, iUltCierreEco);
                                ProyTec.EstadoRecursos(tr, iCodPT, iCodRecurso, "A");
                                break;
                            case "D"://desactivar el recurso de todas las tareas
                                ProyTec.EstadoRecursos(tr, iCodPT, iCodRecurso, "D");
                                break;
                        }
                        //Obtener a cuantas tareas está asignado el profesional después de las actualizaciones,
                        //para devolverlo al navegador y actualizar el dato en pantalla.
                        int nCount = ProyTec.NumeroTareasRecurso(tr, iCodPT, iCodRecurso);
                        if (sRecTareas != "") sRecTareas += "##"; //Separador de recursos
                        sRecTareas += iCodRecurso.ToString() + "//" + nCount.ToString();
                    }
                }
            }
            #endregion
            #region RTPT´s
            //Ahora recojo los responsables técnicos de proyecto técnico
            if (strDatosRTPT != "")
            {
                string[] aRTPTs = Regex.Split(strDatosRTPT, "///");
                for (int i = 0; i < aRTPTs.Length - 1; i++)
                {
                    sCad = aRTPTs[i];
                    iPos = sCad.IndexOf("##");
                    sOpcionBD = sCad.Substring(0, iPos);
                    sCad = sCad.Substring(iPos + 2);
                    iCodRecurso = int.Parse(sCad);
                    switch (sOpcionBD)
                    {
                        case "I"://insertar recurso como RTPT
                            RTPT.Insert(tr, iCodPT, iCodRecurso);
                            break;
                        case "D"://borrar el recurso como RTPT
                            RTPT.Delete(tr, iCodPT, iCodRecurso);
                            break;
                    }
                }
            }
            #endregion
            #region datos Atributos Estadísticos
            if (strDatosAE != "")
            {
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
                            AEPTPSP.Insert(tr, iCodPT, int.Parse(aValores[2]), int.Parse(aValores[3]));
                            break;
                        case "U":
                            AEPTPSP.Update(tr, iCodPT, int.Parse(aValores[2]), int.Parse(aValores[3]));
                            break;
                        case "D":
                            AEPTPSP.Delete(tr, iCodPT, int.Parse(aValores[3]));
                            break;
                    }
                }
            }
            //Se controla por trigger
            //if (!bInsert) AETAREAPSP.DeleteDuplicados(tr, -1, iCodPT);
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
                                PTCampoImporte.Insert(tr, iCodPT, int.Parse(aValores[1]), dAux);
                                break;
                            case "F":
                                DateTime? dtAux = null;
                                if (aValores[2] != "") dtAux = DateTime.Parse(aValores[2]);
                                PTCampoFecha.Insert(tr, iCodPT, int.Parse(aValores[1]), dtAux, aValores[3]);
                                break;
                            case "H":
                                DateTime? dFechaHora = null;
                                if (aValores[2] != "")
                                dFechaHora = DateTime.Parse(aValores[2] + " " + aValores[4] + ":" + aValores[5] + ":" + aValores[6]);
                                PTCampoFecha.Insert(tr, iCodPT, int.Parse(aValores[1]), dFechaHora, aValores[3]);
                                break;

                            case "T":                               
                                PTCampoTexto.Insert(tr, iCodPT, int.Parse(aValores[1]), aValores[2]);                               
                                break;
                        }
                                               
                        break;                   

                    case "U":
                        switch (aValores[3])
                        {
                            case "I":
                                decimal? dAux = null;
                                if (aValores[2] != "") dAux = decimal.Parse(aValores[2]);
                                PTCampoImporte.Update(tr, iCodPT, int.Parse(aValores[1]), dAux);
                                break;
                            case "F":
                                DateTime? dtAux = null;
                                if (aValores[2] != "") dtAux = DateTime.Parse(aValores[2]);
                                PTCampoFecha.Update(tr, iCodPT, int.Parse(aValores[1]), dtAux, aValores[3]);
                                break;
                            case "H":
                                DateTime? dFechaHora = null;
                                if (aValores[2] != "")
                                    dFechaHora = DateTime.Parse(aValores[2] + " " + aValores[4] + ":" + aValores[5] + ":" + aValores[6]);
                                PTCampoFecha.Update(tr, iCodPT, int.Parse(aValores[1]), dFechaHora, aValores[3]);
                                break;
                            case "T":                               
                                PTCampoTexto.Update(tr, iCodPT, int.Parse(aValores[1]), aValores[2]);
                                break;
                        }
                        break;

                    case "D":
                        switch (aValores[3])
                        {
                            case "I":
                                PTCampoImporte.Delete(tr, iCodPT, int.Parse(aValores[1]));
                                break;
                            case "F":
                            case "H":
                                PTCampoFecha.Delete(tr, iCodPT, int.Parse(aValores[1]));
                                break;
                            case "T":
                                PTCampoTexto.Delete(tr, iCodPT, int.Parse(aValores[1]));
                                break;
                        }
                        break;
                }
            }
            #endregion

            #region Pool de grupos funcionales y de profesionales
            //Ahora recojo los grupos funcionales del pool del proyecto técnico
            if (strDatosPoolGF != "")
            {
                string[] aPoolGF = Regex.Split(strDatosPoolGF, "///");
                for (int i = 0; i < aPoolGF.Length - 1; i++)
                {
                    if (aPoolGF[i].Trim() != "")
                    {
                        string[] aElemPoolGF = Regex.Split(aPoolGF[i], "##");
                        sOpcionBD = aElemPoolGF[0];
                        if (aElemPoolGF[1].Trim() != "")
                        {
                            iCodRecurso = int.Parse(aElemPoolGF[1].Trim());
                            switch (sOpcionBD)
                            {
                                case "I":
                                    POOL_GF_PT.Insert(tr, iCodPT, iCodRecurso);
                                    break;
                                case "D":
                                    POOL_GF_PT.Delete(tr, iCodPT, iCodRecurso);
                                    break;
                            }
                        }
                    }
                }
            }
            //Ahora recojo los profesionales del pool del proyecto técnico
            if (strDatosPoolProf != "")
            {
                string[] aPoolProf = Regex.Split(strDatosPoolProf, "///");
                for (int i = 0; i < aPoolProf.Length - 1; i++)
                {
                    if (aPoolProf[i].Trim() != "")
                    {
                        string[] aElemPoolProf = Regex.Split(aPoolProf[i], "##");
                        sOpcionBD = aElemPoolProf[0];
                        if (aElemPoolProf[1].Trim() != "")
                        {
                            iCodRecurso = int.Parse(aElemPoolProf[1].Trim());
                            switch (sOpcionBD)
                            {
                                case "I":
                                    POOL_PT.Insert(tr, iCodPT, iCodRecurso);
                                    break;
                                case "D":
                                    POOL_PT.Delete(tr, iCodPT, iCodRecurso);
                                    break;
                            }
                        }
                    }
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + DateTime.Now.ToString() + "@#@" + Session["UsuarioActual"].ToString() + "@#@" + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString() + "@#@" + sRecTareas + "@#@" + iCodPT.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del proyecto técnico", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string ObtenerRTPTs(string sCodPT, string sCodUne)
    {
        //Relacion de responsables técnicos asignados al proyecto técnico
        string sResul = "";
        int iCodPT;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (sCodPT != "")//Si estoy creando un PT no tiene sentido obtener sus RTPT´s
            {
                iCodPT = int.Parse(sCodPT.Replace(".", ""));
                SqlDataReader dr = ProyTec.CatalogoRTPTs(iCodPT);
                sb.Append("<table id='tblAsignados2' class='texto MM' style='width: 350px;'>");
                sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:310px;' /></colgroup>");
                sb.Append("<tbody id='tbodyDestino'>");
                while (dr.Read())
                {
                    //Por indicación de Victor quito la posibilidad de eliminar con doble-click
                    //sb.Append("id='" + dr["idrecurso"].ToString() + "' onclick='marcarUnaFila(this);' ondblclick='eliminarRecurso2(this)'>");
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:20px;' ");

                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");

                    sb.Append(" onclick='mm(event);' onmousedown='DD(event)'>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W310'>" + dr["empleado"].ToString() + "</nobr></td></tr>");
                }
                sb.Append("</tbody></table>");
                dr.Close(); dr.Dispose();
                strTablaRTPTs = sb.ToString();
                sResul = strTablaRTPTs;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de responsables técnicos asociados al proyecto técnico.", ex);
        }

        return sResul;
    }
    private void PonerRTPT()
    {
        //Ponemos al usuario actual como responsable técnicos asignado al proyecto técnico
        string sResul = "", sNombre;
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAsignados2' class='texto MM' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:310px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            sb.Append("<tr id='" + int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()) + "' bd='' style='height:20px;' onclick='mm(event);' onmousedown='DD(event)'>");
            sNombre = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
            sb.Append("<td><img src='../../../images/imgFN.gif'></td><td></td>");
            sb.Append("<td >" + sNombre + "</td></tr>");
            sb.Append("</tbody></table>");
            strTablaRTPTs = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de responsables técnicos asociados al proyecto técnico.", ex);
        }
    }
    private string PonerRTPT2(string sT305IdProy, string sCodUne)
    {
        //Ponemos el POOL de RTPT´s o (si es vacío) al usuario actual como responsable técnicos asignados al proyecto técnico
        string sResul = "", sNombre;
        int num_proyecto;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (sT305IdProy == "") num_proyecto = -1;
            else num_proyecto = int.Parse(sT305IdProy);
            //Asigno como responsable técnico por defecto al pool de RTPTs, si está vacío al usuario que está creando el registro
            sb.Append("<table id='tblAsignados2' class='texto MM' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:310px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            if (POOLRPT.HayPool(num_proyecto))
            {
                SqlDataReader dr = POOLRPT.SelectByProyecto(num_proyecto);
                while (dr.Read())
                {
                    //sb.Append("<tr id='" + int.Parse(rdr["t314_idusuario"].ToString()) + "' bd='' style='height:16px;' onclick='mm(event);' onmousedown='DD(event)'>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    //sb.Append("<td>" + rdr["denominacion"].ToString() + "</td></tr>");
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:20px;' ");

                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    //if (dr["t001_fecbaja"].ToString() == "")
                    //    sb.Append("baja='N' ");
                    //else
                    //{
                    //    if (System.Convert.ToDateTime(dr["t001_fecbaja"].ToString()) < System.DateTime.Today)
                    //        sb.Append("baja='S' ");
                    //    else
                    //        sb.Append("baja='N' ");
                    //}
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");

                    sb.Append(" onclick='mm(event);' onmousedown='DD(event)'>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W310'>" + dr["empleado"].ToString() + "</nobr></td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            else
            {
                sb.Append("<tr id='" + int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()) + "' bd='' style='height:20px;' onclick='mm(event);' onmousedown='DD(event)'>");
                sNombre = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
                sb.Append("<td><img src='../../../images/imgFN.gif'></td><td></td><td>" + sNombre + "</td></tr>");
            }
            sb.Append("</tbody></table>");
            strTablaRTPTs = sb.ToString();

            sResul = "OK@#@" + strTablaRTPTs;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de responsables técnicos asociados al proyecto técnico.", ex);
        }
        return sResul;
    }

    private void ObtenerTarifas()
    {
        cboTarifa.DataValueField = "t333_idperfilproy";
        cboTarifa.DataTextField = "t333_denominacion";
        cboTarifa.DataSource = TARIFAPROY.SelectByt301_idproyecto(nPE);
        cboTarifa.DataBind();
    }

    #region Atributos estadisticos
    private string ObtenerAtributosEstadisticosCR(string sNodo)
    {
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append("<div style='background-image:url(../../../Images/imgFT16.gif); width:180px'>");
        sbuilder.Append("<table id='tblAECR' class='texto MAM' style='width: 180px;'>");
        sbuilder.Append("<colgroup><col width='15px' /><col width='165px' /></colgroup>");
        sbuilder.Append("<tbody>");
        if (sNodo != "")
        {
            SqlDataReader dr = AE.Catalogo(null, "", true, null, null, short.Parse(sNodo), null, "T", 4, 0);
            while (dr.Read())
            {
                string sObl = "0";
                if ((bool)dr["t341_obligatorio"]) sObl = "1";
                if (this.hdnAcceso.Text == "R")
                {
                    sbuilder.Append("<tr id='" + dr["t341_idae"].ToString() + "' cliente='" + dr["cod_cliente"].ToString() + "' obl='" + sObl + "' style='height:16px'>");
                }
                else
                {
                    sbuilder.Append("<tr id='" + dr["t341_idae"].ToString() + "' cliente='" + dr["cod_cliente"].ToString() + "' obl='" + sObl +
                                    "' onclick='ms(this);' ondblclick='asociarAE(this, true)' onmousedown='DD(event);' style='height:16px'>");
                }
                if ((bool)dr["t341_obligatorio"])
                    sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                else
                    sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                sbuilder.Append("<td><nobr class='NBR W160'>" + dr["t341_nombre"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
        }
        sbuilder.Append("</tbody>");
        sbuilder.Append("</table></div>");
        strTablaAECR = sbuilder.ToString();
        return sbuilder.ToString();
    }
    private string ObtenerAtributosEstadisticosPT(string sCodPT)
    {
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append("<div style='background-image:url(../../../Images/imgFT16.gif); width:330px'>");
        sbuilder.Append("<table id='tblAET' class='texto MM' style='width: 330px;' mantenimiento='1'>");
        sbuilder.Append("<colgroup><col style='width:10px;' /><col style='width:15px;' /><col style='width:205px;' /><col style='width:100px;' /></colgroup>");
        sbuilder.Append("<tbody>");
        if (sCodPT != "")
        {
            int iCodPT = int.Parse(sCodPT.Replace(".", ""));
            SqlDataReader dr = AE.CatalogoByPT(iCodPT);
            while (dr.Read())
            {
                string sObl = "0";
                if ((bool)dr["t341_obligatorio"]) sObl = "1";

                if (this.hdnAcceso.Text == "R")
                {
                    sbuilder.Append("<tr id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl + "' bd='' style='height:16px'>");
                }
                else
                {
                    sbuilder.Append("<tr id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl +
                                    "' bd='' onclick='ms(this);mostrarValoresAE(this);' onKeyUp=\"mfa(this,'U')\" onmousedown='DD(event);' style='height:16px'>");
                }
                sbuilder.Append("<td><img src='../../../images/imgFN.gif'></td>");
                if (this.hdnAcceso.Text == "R")
                {
                    if ((bool)dr["t341_obligatorio"])
                        sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    sbuilder.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
                    sbuilder.Append("<td><NOBR class='NBR W90' title='" + dr["t340_valor"].ToString() + "'>" + dr["t340_valor"].ToString() + "</NOBR></td></tr>");
                }
                else
                {
                    if ((bool)dr["t341_obligatorio"])
                        //sbuilder.Append("<td ondblclick='desasociarAE(this.parentNode)'><img src='../../../images/imgOk.gif' title='Atributo estadístico obligatorio'></td>");
                        sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        //sbuilder.Append("<td ondblclick='desasociarAE(this.parentNode)'><img src='../../../images/imgSeparador.gif'></td>");
                        sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    //sbuilder.Append("<td ondblclick='desasociarAE(this.parentNode)'>" + dr["t341_nombre"].ToString() + "</td>");
                    //sbuilder.Append("<td ondblclick='desasignarValorAE(this.parentNode)'>" + dr["t340_valor"].ToString() + "</td></tr>");
                    sbuilder.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
                    sbuilder.Append("<td><NOBR class='NBR W90' title='" + dr["t340_valor"].ToString() + "'>" + dr["t340_valor"].ToString() + "</NOBR></td></tr>");
                }
            }
            dr.Close();
            dr.Dispose();
        }
        sbuilder.Append("</tbody>");
        sbuilder.Append("</table></div>");
        strTablaAET = sbuilder.ToString();
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

    #endregion
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
                DOCUPT.Delete(tr, int.Parse(oDoc));
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
    private string ObtenerListaDocumentos(string sIdPT, int idUser)
    {
        StringBuilder sbuilder = new StringBuilder();
        try
        {
            sbuilder.Append("<div style='background-image:url(../../../Images/imgFT20.gif); width:850px'>");
            sbuilder.Append("<table id='tblDocumentos2' class='texto MANO' style='width: 850px;'>");
            sbuilder.Append("<colgroup><col style='width:290px;' /><col style='width:235px' /><col style='width:225px' /><col style='width:100px' /></colgroup>");
            sbuilder.Append("<tbody>");
            if (sIdPT != "")
            {
                SqlDataReader dr = DOCUPT.Lista(null, int.Parse(sIdPT.Replace(".", "")), idUser);
                while (dr.Read())
                {
                    sbuilder.Append("<tr id='" + dr["idDocu"].ToString() + "' style='height:20px;' sTipo='" + dr["TIPO"].ToString() + "' sAutor='" + dr["Num_Autor"].ToString() + "' onmouseover='TTip(event);' onclick='mm(event);'>");
                    //No se permite modificar.
                    string sTTip = "";
                    if (dr["des_pt"].ToString() != "") sTTip += "Proy. Téc.: " + dr["des_pt"].ToString();
                    if (dr["des_fase"].ToString() != "") sTTip += (char)10 + "Fase:          " + dr["des_fase"].ToString();
                    if (dr["des_actividad"].ToString() != "") sTTip += (char)10 + "Actividad:   " + dr["des_actividad"].ToString();
                    if (dr["des_tarea"].ToString() != "") sTTip += (char)10 + "Tarea:        " + dr["des_tarea"].ToString();

                    sbuilder.Append("<td style='padding-left:5px;'><nobr class='NBR' style='width:280px' title=\"" + sTTip + "\" >" + dr["Descripcion"].ToString() + "</nobr></td>");

                    if (dr["Nombrearchivo"].ToString() == "")
                        sbuilder.Append("<td></td>");
                    else
                    {
                        string sNomArchivo = dr["Nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                        //Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                        //if ((!(bool)dr["Privado"]) || ((bool)dr["Privado"] && dr["Num_Autor"].ToString() == Session["UsuarioActual"].ToString()) || Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A")
                            sbuilder.Append("<td><img src=\"../../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">&nbsp;<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
                        //else
                        //    sbuilder.Append("<td><img src=\"../../../images/imgSeparador.gif\" width='16px' height='16px' style='vertical-align:bottom;'>&nbsp;<nobr class='NBR' style='width:205px;'>" + dr["Nombrearchivo"].ToString() + "</nobr></td>");
                    }

                    if (dr["Weblink"].ToString() == "")
                        sbuilder.Append("<td></td>");
                    else
                    {
                        string sHTTP = "";
                        if (dr["Weblink"].ToString().ToLower().IndexOf("http") == -1) sHTTP = "http://";
                        sbuilder.Append("<td><a href='" + sHTTP + dr["Weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr["Weblink"].ToString() + "</nobr></a></td>");
                    }

                    sbuilder.Append("<td><nobr class='NBR' style='width:90px;'>" + dr["Autor"].ToString() + "</nobr></td>");
                }
                dr.Close();
                dr.Dispose();
            }
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table></div>");
        }
        catch (Exception ex)
        {
            sbuilder.Append("Error@#@" + Errores.mostrarError("Error al obtener documentos dependientes del proyecto técnico", ex));
        }
        return sbuilder.ToString();
    }

    private string Permiso(string sT305IdProy, string sIdPT)
    {
        string sResul = "N", sEstProy;
        try
        {
            //1º miramos si hay acceso sobre el proyecto técnico
            string sUserAct = Session["UsuarioActual"].ToString();
            int iUserAct = int.Parse(sUserAct);
            sIdPT = sIdPT.Replace(".", "");
            sResul = ProyTec.getAcceso(null, int.Parse(sIdPT), iUserAct);
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
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener permisos sobre el proyecto técnico " + sIdPT, ex);
        }
        return sResul;
    }

    protected string Borrar(string sIdPT)
    {
        string sResul = "OK@#@";
        int nIdPT;

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
            sIdPT = sIdPT.Replace(".", "");
            nIdPT = int.Parse(sIdPT);

            if (EstrProy.ExistenConsumosPT(tr, nIdPT))
            {
                sResul += "No se puede borrar porque tiene consumos";
            }
            else
            {
                ProyTec.Eliminar(tr, nIdPT);
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar el proyecto técnico", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    #region Pool de Profesionales y Grupos Funcionales
    protected string ObtenerTecnicosPool(string strOpcion, string strValor1, string strValor2, string strValor3, string sCualidad, string sCR,
                                         string t305_idProyectoSubnodo)
    {
        //Relacion de técnicos candidatos a ser asignados al proyecto técnico
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();
        int? nUne = null;
        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);
            if (sCualidad == "P")
            {
                nUne = int.Parse(sCR);
            }
            //SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCR, t305_idProyectoSubnodo, sCualidad, "");
            SqlDataReader dr = Recurso.GetUsuariosPSN(strOpcion, sV1, sV2, sV3, sCR, t305_idProyectoSubnodo, sCualidad, "");
            sb.Append("<table id='tblRelacion3' class='texto MAM' style='width: 370px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[Profesional:&nbsp;");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else if (dr["t303_idnodo"].ToString() == sCR) sb.Append("tipo='P' ");
                //else sb.Append("tipo='N' ");

                if (this.hdnAcceso.Text == "R")
                    sb.Append(">");
                else
                    sb.Append(" onclick='mm(event);' ondblclick='insertarRecurso3(this);' onmousedown='DD(event)'>");

                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W330'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales para el pool.", ex);
        }

        return sResul;
    }
    private string ObtenerPoolProf(string sCodPT, string sCR, bool bMostrarBajas)
    {
        //Relacion de profesionales asignados al pool del proyecto técnico
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAsignados3' class='texto MM' style='width: 370px;'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:340px;' /></colgroup>");
            sb.Append("<tbody>");
            if (sCodPT != "")
            {
                int iCodPT = int.Parse(sCodPT.Replace(".", ""));
                SqlDataReader dr = POOL_PT.Catalogo(iCodPT, bMostrarBajas);
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px' bd='' id='" + dr["t314_idusuario"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    //if (dr["t001_fecbaja"].ToString() == "")
                    //    sb.Append("baja='N' ");
                    //else
                    //{
                    //    if (System.Convert.ToDateTime(dr["t001_fecbaja"].ToString()) < System.DateTime.Today)
                    //        sb.Append("baja='S' ");
                    //    else
                    //        sb.Append("baja='N' ");
                    //}
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCR) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");

                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append(" onclick='mm(event);' onmousedown='DD(event)'>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W330'>" + dr["empleado"].ToString() + "</nobr></td></tr>");

                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaPoolProf = sb.ToString();
            sResul = strTablaPoolProf;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales del pool asociados al proyecto técnico.", ex);
        }
        return sResul;
    }
    private string ObtenerPoolGF(string sCodPT)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblPoolGF' class='texto' style='width: 370px;'>");
        sb.Append("<colgroup><col style='width:15px;' /><col style='width:355px;' /></colgroup>");
        sb.Append("<tbody>");
        if (sCodPT != "")
        {
            int iCodPT = int.Parse(sCodPT.Replace(".", ""));
            SqlDataReader dr = POOL_GF_PT.Catalogo(iCodPT);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["idGF"].ToString() + "' bd='N' style='height:16px;' onclick='mm(event)'>");
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
        return sb.ToString();
    }
    #endregion
    private string recuperarPSN(string nPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nPSN), (int)Session["UsuarioActual"], "PST");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //4
                sb.Append(dr["estado"].ToString() + "@#@");  //5
                if ((bool)dr["t305_admiterecursospst"])
                    sb.Append("S");  //6
                else
                    sb.Append("N");  //6
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

    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pst", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, true, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = (dr["rtpt"].ToString() == "1") ? true : false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al buscar el proyecto", ex);
        }
        return sResul;
    }
    private string buscarPT(string t331_idpt)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            ProyTec o = ProyTec.Obtener(int.Parse(t331_idpt));
            if (o.t331_idpt.ToString() != "")
            {
                sb.Append(o.t305_idproyectosubnodo.ToString() + "@#@");
                sb.Append(o.num_proyecto.ToString() + "@#@");
                sb.Append(o.nom_proyecto + "@#@");
                sb.Append(o.t331_despt);
            }

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "AVISO@#@El proyecto técnico no existe o no está bajo tu ámbito de visión";
        }
    }

    private string ObtenerCamposTareas(int t331_idpt)
    {
        StringBuilder sbuilder = new StringBuilder();
        SqlDataReader dr = SUPER.DAL.CampoPT.CatalogoByPT(t331_idpt);
        while (dr.Read())
        {
            sbuilder.Append(dr["t290_idcampo"].ToString() + ",");
        }
        dr.Close();
        dr.Dispose();
        return sbuilder.ToString();
    }

    private string ObtenerAtributosEstadisticosTareas(int t331_idpt)
    {
        StringBuilder sbuilder = new StringBuilder();
        SqlDataReader dr = AETAREAPSP.CatalogoByPT(t331_idpt);
        while (dr.Read())
        {
            sbuilder.Append(dr["t341_idae"].ToString() + ",");
        }
        dr.Close();
        dr.Dispose();
        return sbuilder.ToString();
    }

    private string mostrarTareasConAE(string sIdPT, string sIdAE)
    {
        string sResul = "", sTarea="", sValor="";
        int iCont = 0;
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            dr = ProyTec.CatalogoTareasConAE(int.Parse(sIdPT), int.Parse(sIdAE));
            while (dr.Read())
            {
                sTarea = dr["t332_destarea"].ToString();
                if (sTarea.Length > 50) sTarea = sTarea.Substring(0, 50);
                sValor = dr["t340_valor"].ToString();
                if (sValor.Length > 30) sValor = sValor.Substring(0, 30);

                if (iCont < 20)
                    sb.Append(int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + sTarea + " (Valor: "+ sValor + ")" + "\n");
                iCont++;
            }
            if (iCont > 20)
                sb.Append("\n...");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de tareas con AE.", ex);
        }
        return sResul;
    }

    private string ObtenerCamposValor(string sIdPT)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";
        string sComboHoras;
        string sComboMinutos;
        string sComboSegundos;

        int iSel;

        try
        {
            sb.Append("<table id='tblCampos' class='texto MM' mantenimiento='1' style='width: 600px;'>");
            sb.Append(@"<colgroup><col style='width:20px;' /><col style='width:150px;' /><col style='width:60px'/>
                        <col style='width:80px;' /><col style='width:40px;' /><col style='width:40px;' />
                        <col style='width:40px;' /><col style='width:170px;' /></colgroup>");
            SqlDataReader dr = SUPER.DAL.CampoPT.obtenerCamposValor(null, int.Parse(sIdPT.Replace(".", "")));
            while (dr.Read())
            {
                sb.Append("<tr id='");
                sb.Append(dr["IDENTIFICADOR"].ToString());
                sb.Append("' tipodato='" + dr["t291_idtipodato"].ToString());
                //sb.Append("' bd='' onclick='ms(this)' style='height:20px;'>");
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

                        sb.Append("<td align='left' colspan='5'><input type='text' class='txtNumL' onblur=\"this.className='txtNumM'\" onfocus=\"this.select();fn(this,7,2)\" style='width:70px;' value=\"" + strImporte + "\" onkeyup='fm(event);aGAvanza(1);'>");
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
                            sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' style='width:70px; cursor:pointer' value='" + strFecha + "' Calendar='oCal' onclick='mc(event);' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" />");
                        //sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' onFocus=\"this.className='txtFecM';\" onblur=\"this.className='txtFecL'\" style='width:60px; cursor: pointer' value='" + strFecha + "' Calendar='oCal' onclick='mc(event);' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" />");
                        else
                            sb.Append("<input id='fC_" + dr["IDENTIFICADOR"].ToString() + "' type='text' class='txtFecL' style='width:70px; cursor:pointer' value='" + strFecha + "' Calendar='oCal' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\" onfocus='focoFecha(event)' onmousedown='mc1(event)' />");
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

                    //sComboHoras = "<select disabled class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";
                    sComboHoras = "<select class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";

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

                    //sComboMinutos = "<select disabled class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";
                    sComboMinutos = "<select class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";

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

                    //sComboSegundos = "<select disabled class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";
                    sComboSegundos = "<select class='combo' style='width:39px;' onchange=\"aGAvanza(1);mfa(this.parentNode.parentNode, 'U');\">";

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
                    sb.Append("</td><td>");
                }
                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            //strTablaCampoValor = sb.ToString();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los campos-valor asociados al proyecto técnico", ex);
        }
        return sResul;
    }

     private string mostrarTareasConCampo(string sIdPT, string sIdCampo)
    {
        string sResul = "", sTarea = "", sValor = "";
        int iCont = 0;
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            dr = ProyTec.CatalogoTareasConCampo(int.Parse(sIdPT), int.Parse(sIdCampo));
            while (dr.Read())
            {
                sTarea = dr["t332_destarea"].ToString();
                if (sTarea.Length > 50) sTarea = sTarea.Substring(0, 50);
                sValor = dr["valor"].ToString();
                if (sValor.Length > 30) sValor = sValor.Substring(0, 30);

                if (iCont < 20)
                    sb.Append(int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + sTarea + " (Valor: " + sValor + ")" + "\n");
                iCont++;
            }
            if (iCont > 20)
                sb.Append("\n...");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de tareas con campo libre.", ex);
        }
        return sResul;
    }
    
    private string getCamposPorAmbito(int codAmbito)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        ArrayList aLista = new ArrayList();
        sb.Append("<table id='tblDatos' class='texto MAM' style='width:180px;'>");
        sb.Append("<colgroup><col style='width:140px;' /></colgroup><tbody>");
        try
        {
            SqlDataReader dr = SUPER.Capa_Datos.CAMPOS.Catalogo(int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()),
                                                                     codAmbito,
                                                                     "9",
                                                                     int.Parse(hdnT305IdProy.Value),
                                                                     aLista);
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

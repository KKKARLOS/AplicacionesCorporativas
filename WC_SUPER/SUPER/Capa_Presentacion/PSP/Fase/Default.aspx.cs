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
    public string sErrores, sLectura, sNodo = "", sOTCHeredada = "false";
    public int nIdFase, nIdPT, nPE;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaRecursos, strTablaTareas, strTablaPoolGF, strTablaPoolProf, strIdCliente;

    protected void Page_Load(object sender, EventArgs e)
    {
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

            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //    this.imgForaneo2.Visible = false;
            //    this.lblForaneo2.Visible = false;
            //}
            sErrores = "";
            strTablaRecursos = "<table id='tblAsignados'></table>";

            strTablaTareas = "";
            sLectura = "false";
            sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            nIdFase = int.Parse(Utilidades.decodpar(Request.QueryString["f"].ToString()));//nIdFase
            try
            {
                Utilidades.SetEventosFecha(this.txtValIni);
                Utilidades.SetEventosFecha(this.txtValFin);
                Utilidades.SetEventosFecha(this.txtFIPRes);
                Utilidades.SetEventosFecha(this.txtFFPRes);

                rdbAmbito.Items[1].Text = sNodo + "  ";
                this.chkHeredaCR.Text = " " + Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                ObtenerDatosFase();
                ObtenerTarifas();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la fase", ex);
            }

            if (Request.QueryString["pm"] != null)//Permiso
                this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["pm"].ToString());//Permiso
            if (this.hdnAcceso.Text == "R")
            {
                ModoLectura.Poner(this.Controls);
                sLectura = "true";
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad = "", sCad2 = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
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
                if (aArgs[4] == "S") bMostrarBajasPool = true;
                sResultado += "OK@#@" + ObtenerPoolProf(aArgs[1], aArgs[2], aArgs[3], bMostrarBajasPool);
                break;
            case ("getActiv")://carga las tareas de una actividad en la pestaña de tareas
                sResultado += "OK@#@" + ObtenerTareas(aArgs[1], "A");
                break;
            case ("documentos"):
                //sResultado += "OK@#@" + ObtenerDocumentos(aArgs[1]);
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
            case ("getDatosPestana"):
                //sResultado += "OK@#@" + aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                    case 3://CONTROL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://PROFESIONALES
                        sCad = ObtenerRecursosAsociados(aArgs[2], aArgs[4], false);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 2://TAREAS
                        sCad = ObtenerTareas(aArgs[2], "F");
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 4://DOCUMENTACION
                        //sCad = ObtenerDocumentos(aArgs[2]);
                        sCad = Utilidades.ObtenerDocumentos("F", int.Parse(aArgs[2]), aArgs[4], aArgs[5]);
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
                        sCad = ObtenerPoolProf(aArgs[2], aArgs[3], aArgs[4], false);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else
                        {
                            sCad2 = ObtenerPoolGF(aArgs[2], aArgs[3]);
                            if (sCad2.IndexOf("Error@#@") >= 0) sResultado += sCad2;
                            else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad + "///" + sCad2;
                        }
                        //11/10/2001 Además devolvemos el nº de empleados del Nodo
                        int iNumEmp = NODO.GetNumEmpleados(null, int.Parse(aArgs[4]));
                        sResultado += "@#@" + iNumEmp.ToString("#,###") + ".";
                        break;
                }
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
    private void ObtenerDatosFase()
    {
        FASEPSP o = FASEPSP.Obtener(nIdFase);

        hdnT305IdProy.Value = o.t305_idproyectosubnodo.ToString();
        this.txtCualidad.Text = o.t305_cualidad.ToString();
        hndIdPE.Text = o.num_proyecto.ToString("#,###");
        hdnOrden.Text = o.t334_orden.ToString();
        nIdPT = o.t331_idpt;
        txtIdPT.Text = o.t331_idpt.ToString("#,###");
        txtDesPT.Text = o.t331_despt;
        txtDesPT.ToolTip= o.t331_despt;
        txtIdFase.Text = o.t334_idfase.ToString();
        txtDesFase.Text = o.t334_desfase;
        txtDesFase.ToolTip = o.t334_desfase;
        txtDescripcion.Text = o.t334_desfaselong;
        txtObservaciones.Text = o.t334_observaciones;

        this.hdnCRActual.Value = o.t303_idnodo.ToString();
        this.hdnDesCRActual.Value = o.t303_denominacion;
        strIdCliente = o.cod_cliente.ToString();

        if (o.t346_idpst == 0) txtIdPST.Text = "";
        else txtIdPST.Text = o.t346_idpst.ToString();
        txtCodPST.Text = o.t346_codpst;
        txtDesPST.Text = o.t346_despst;
        if (o.bOTCHeredada) sOTCHeredada = "true";

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
        if (o.t334_heredanodo) chkHeredaCR.Checked = true;
        else chkHeredaCR.ToolTip = "Asigna de forma automática a todos los profesionales presentes y futuros del " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + ". La opción está deshabilitada por bloqueo desde el proyecto económico.";
        //Si el PT ya hereda recursos del CR el campo debe quedar desactivado en la fase
        if (o.t331_heredanodo)
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
        if (!o.t305_avisorecursopst)
        {
            imgCorreo.ImageUrl = "../../../Images/imgCorreoBloqueado.gif";
            imgCorreo.ToolTip = "La notificación de asignación de profesionales a la tarea, está bloqueada por parametrización a nivel de proyecto económico.";
        }
        if (o.t334_heredaproyeco) chkHeredaPE.Checked = true;
        //Si el PT ya hereda recursos del PE el campo debe quedar desactivado en la fase
        if (o.t331_heredaproyeco)
        {
            chkHeredaPE.Checked = true;
            chkHeredaPE.Enabled = false;
        }

        this.hdnNivelPresupuesto.Value = o.t305_nivelpresupuesto.ToString();
        txtPresupuesto.Text = o.nPresupuesto.ToString("N");             

        if(o.t305_nivelpresupuesto == "F")
        {
            txtAvanReal.Text = o.t334_avance.ToString("N");
            if (o.t334_avanceauto) chkAvanceAuto.Checked = true;
            else chkAvanceAuto.Checked = false;
        }

        txtPE.Text = o.nom_proyecto;
        nPE = o.num_proyecto;
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
        //Modo de acceso
        this.hdnModoAcceso.Value = ProyTec.getAcceso(null, nIdPT, int.Parse(Session["UsuarioActual"].ToString()));
        hdnEsReplicable.Value = (o.t301_esreplicable) ? "1" : "0";
    }
    private string ObtenerRecursosAsociados(string sCodFase, string sCodUne, bool bMostrarBajas)
    {
        //Relacion de tecnicos asignados a la fase
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAsignados' class='texto MANO' style='width:380px;' mantenimiento='1'>");
            //sb.Append("<colgroup><col width='315px' /><col width='5px' /><col width='15px' /><col style='width:15px;text-align:right;padding-right:2px;' /></colgroup>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:20px;' /><col style='width:300px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            if (sCodFase != "")
            {
                int iCodFase = int.Parse(sCodFase.Replace(".", ""));
                SqlDataReader dr = FASEPSP.CatalogoRecursos(iCodFase, bMostrarBajas);

                int i = 0;
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px' bd='' ac=''id='" + dr["t314_idusuario"].ToString() + "' ");
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

                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                    sb.Append(">");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W300'>" + dr["empleado"].ToString() + "</nobr></td>");
                    sb.Append("<td style='text-align:right;padding-right:2px;cursor: url(../../../images/imgManoAzul2.cur),pointer;' ondblclick='mostrarTareasRecurso(" + dr["t314_idusuario"].ToString() + ");'>" + dr["num_tareas"].ToString() + "</td>");
                    sb.Append("</tr>");

                    i++;
                }
                dr.Close(); dr.Dispose();
            }
            sb.Append("</tbody></table>");
            strTablaRecursos = sb.ToString();
            sResul = strTablaRecursos;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales asociados a la fase.", ex);
        }

        return sResul;
    }
    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3, string sCodUne,
                                     string t305_idProyectoSubnodo, string sCualidad)
    {
        //Relacion de técnicos candidatos a ser asignados a la fase 
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();

        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);

            //SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad,"");
            SqlDataReader dr = Recurso.GetUsuariosPSN(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "");
            int i = 0;
            sb.Append("<table id='tblRelacion' class='texto MAM' style='width:350px'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup><tbody id='tbodyOrigen'>");

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

                if (this.hdnAcceso.Text == "R")
                {
                    sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' idTarifa='" + dr["IDTARIFA"].ToString() +
                                      "'><td></td><td><nobr class='NBR W330'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
                }
                else
                {
                    sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' idTarifa='" + dr["IDTARIFA"].ToString() +
                                      "' onmousedown='DD(event)' onclick='mm(event);' ondblclick='insertarRecurso(this);'><td></td><td><nobr class='NBR W330'>" +
                                      dr["Profesional"].ToString() + "</nobr></td></tr>");
                }
                i++;
            }
            sb.Append("</tbody></table>");
            dr.Close(); dr.Dispose();
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
        string sResul = "", sCad, sFecha;
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            if (sCodElem != "")//Si estoy creando un elemento no tiene sentido obtener sus tareas
            {
                int iCodElem = int.Parse(sCodElem.Replace(".", ""));

                #region leer tareas con estructura
                string sDesTipo = "", sWidth = "";
                int iMargen;
                if (sNivelEst == "F")
                {
                    sb.Append("<table id='tblTareas' style='width: 850px;text-align:center;'>");
                    sb.Append("<colgroup>");
                    sb.Append("    <col style='width:350px;' />");
                    sb.Append("    <col style='width:55px;'/>");
                    sb.Append("    <col style='width:65px;' />");
                    sb.Append("    <col style='width:65px;' />");
                    sb.Append("    <col style='width:55px;'/>");
                    sb.Append("    <col style='width:65px;' />");
                    sb.Append("    <col style='width:65px;' />");
                    sb.Append("    <col style='width:65px;' />");
                    sb.Append("    <col style='width:65px;' />");
                    sb.Append("</colgroup>");
                    sb.Append("<tbody>");
                }
                switch (sNivelEst)
                {
                    case "F":
                        dr = FASEPSP.CatalogoTareasFase(iCodElem);
                        sWidth = "300";
                        break;
                    case "A":
                        dr = ACTIVIDADPSP.CatalogoTareas(iCodElem);
                        sWidth = "290";
                        break;
                    default:
                        dr = null;
                        break;
                }
                while (dr.Read())
                {
                    sDesTipo = dr["Tipo"].ToString();

                    sb.Append("<tr id='" + int.Parse(dr["codElem"].ToString()) + "' otc='" + dr["idotc"].ToString() +
                              "' nivel='1' desplegado='0' tipo='" + sDesTipo + "' style='height:20px;'><td style='padding-right:5px;text-align:left;'>");
                    iMargen = int.Parse(dr["margen"].ToString()) - 20;
                    //sMargen = " style='margin-left:" + iMargen.ToString() + "px' ";
                    switch (sDesTipo)
                    {
                        case "A":
                            sb.Append("<img src='../../../Images/plus.gif' onclick='mostrar(this);' style='margin-left:" + iMargen.ToString() + ";cursor:pointer;'>");
                            sb.Append("<img src='../../../Images/imgActividadOff.gif' border='0' title='Actividad' class='ICO'>");
                            sb.Append("<nobr onmouseover='TTip(event)' class='NBR W" + sWidth + "'>" + dr["Nombre"].ToString() + "</nobr></td>");
                            break;
                        case "T":
                            sb.Append("<img src='../../../Images/imgTrans9x9.gif' onclick='mostrar(this);' style='margin-left:" + iMargen.ToString() + ";'>");
                            sb.Append("<img src='../../../Images/imgTareaOff.gif' border='0' title='Tarea' class='ICO'>");
                            sb.Append("<nobr onmouseover='TTip(event)' class='NBR W" + sWidth + "'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["Nombre"].ToString() + "</nobr></td>");
                            break;
                    }

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
                    sb.Append("<td>" + sFecha + "</td>");
                    if (dr["duracionPr"] != DBNull.Value)
                        sb.Append("<td style=text-align:right;padding-right:5px;'>" + double.Parse(dr["duracionPr"].ToString()).ToString("N") + "</td>");
                    else
                        sb.Append("<td style=text-align:right;padding-right:5px;'></td>");
                    sFecha = dr["finPr"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["finPr"].ToString()).ToShortDateString();
                    sb.Append("<td>" + sFecha + "</td>");
                    sFecha = dr["iniVig"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["iniVig"].ToString()).ToShortDateString();
                    sb.Append("<td>" + sFecha + "</td>");
                    sFecha = dr["finVig"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["finVig"].ToString()).ToShortDateString();
                    sb.Append("<td>" + sFecha + "</td>");
                    sb.Append("<td style='border-right:0px;padding-right:5px;text-align:right;'>" + double.Parse(dr["Consumo"].ToString()).ToString("N") + "</td></tr>");
                }
                if (sNivelEst == "F")
                {
                    sb.Append("</tbody>");
                    sb.Append("</table></div>");
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
    protected string Grabar(string strDatosBasicos, string strDatosTarea, string strDatosRecursos, string strDatosPoolGF, string strDatosPoolProf)
    {
        string sResul = "", sOpcionBD, sIndicaciones, sAux, sRecTareas = "", sPresupuesto ="0.00", sAvance = "0";
        int iCodRecurso, iTarifa, iCodFase, iT305IdProy, iCodCR;
        int? idPST = null;
        DateTime dtFechaAlta = System.DateTime.Today;
        DateTime? dtFip = null, dtFfp = null;
        bool bHeredaNodo = false, bHeredaPE = false, bNotifExceso = false, bAvanceAuto = false;
        #region Conexión
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] aDatosBasicos = Regex.Split(strDatosBasicos, "##");
            if (aDatosBasicos[0] == "") iCodFase = -1;
            else iCodFase = int.Parse(aDatosBasicos[0]);
            //El nodo se obtiene ahora del proyecto al que pertenece la fase por lo que siempre debe tener valor
            //if (aDatosBasicos[1] == "") iCodCR = short.Parse(Session["NodoActivo"].ToString());
            //else 
            iCodCR = int.Parse(aDatosBasicos[1]);
            if (aDatosBasicos[2] == "") iT305IdProy = -1;
            else iT305IdProy = int.Parse(aDatosBasicos[2]);
            #region Datos generales
            if (strDatosTarea != "")
            {
                string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
                ///aDatosTarea[0] = txtDesFase
                ///aDatosTarea[1] = hdnOrden
                ///aDatosTarea[2] = txtDescripcion
                ///aDatosTarea[3] = bFechaModificada
                ///aDatosTarea[4] = txtValIni
                ///aDatosTarea[5] = txtValFin
                ///aDatosTarea[6] = chkHeredaCR
                ///aDatosTarea[7] = chkHeredaPE
                ///aDatosTarea[8] = txtIdPST

                if (aDatosTarea[6] == "1") bHeredaNodo = true;
                if (aDatosTarea[7] == "1") bHeredaPE = true;
                if (aDatosTarea[8] != "") idPST = int.Parse(aDatosTarea[8]);
                if (aDatosTarea[9] != "") sPresupuesto = aDatosTarea[9];
                if (aDatosTarea[10] != "") sAvance = aDatosTarea[10];
                if (aDatosTarea[11] == "1") bAvanceAuto = true;

                FASEPSP.Update(tr, iCodFase, Utilidades.unescape(aDatosTarea[0]), short.Parse(aDatosTarea[1]),
                               Utilidades.unescape(aDatosTarea[2]), bHeredaNodo, bHeredaPE, idPST, decimal.Parse(sPresupuesto), decimal.Parse(sAvance), bAvanceAuto, Utilidades.unescape(aDatosTarea[12]));

                if (aDatosTarea[3] == "1")
                {
                    DateTime? dIniV = null;
                    DateTime? dFinV = null;
                    if (aDatosTarea[4] != "") dIniV = DateTime.Parse(aDatosTarea[4]);
                    if (aDatosTarea[5] != "") dFinV = DateTime.Parse(aDatosTarea[5]);
                    ArrayList aTarea = new ArrayList();
                    //Hay que actualizar la fechas de vigencia de las tareas que dependen de la Fase.
                    SqlDataReader dr = FASEPSP.CatalogoTareas(tr, iCodFase);
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
                    ///aValores[1] = t314_idusuario;
                    ///aValores[2] = ffp;
                    ///aValores[3] = idTarifa;
                    ///aValores[4] = indicaciones;
                    ///aValores[5] = fip;
                    ///aValores[6] = bNotifExceso
                    if (aValores[0] != "")
                    {
                        sOpcionBD = aValores[0];
                        iCodRecurso = int.Parse(aValores[1]);
                        sAux = aValores[2];
                        if (sAux != "")
                            dtFfp = DateTime.Parse(sAux);
                        sAux = aValores[3];
                        if (sAux == "") iTarifa = -1;
                        else iTarifa = int.Parse(sAux);
                        sIndicaciones = aValores[4];
                        sAux = aValores[5];
                        if (sAux != "")
                            dtFip = DateTime.Parse(sAux);
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
                                FASEPSP.AsignarTareas2(tr, iCodFase, iCodRecurso, dtFip, dtFfp, iTarifa, sIndicaciones, bNotifExceso,
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
                                FASEPSP.AsignarTareas2(tr, iCodFase, iCodRecurso, dtFip, dtFfp, iTarifa, sIndicaciones, bNotifExceso,
                                                            bAdmiteRecursoPST, iT305IdProy, iCodCR, iUltCierreEco);
                                FASEPSP.EstadoRecursos(tr, iCodFase, iCodRecurso, "A");
                                break;
                            case "D"://desactivar el recurso de todas las tareas
                                FASEPSP.EstadoRecursos(tr, iCodFase, iCodRecurso, "D");
                                break;
                        }
                        //Obtener a cuantas tareas está asignado el profesional después de las actualizaciones,
                        //para devolverlo al navegador y actualizar el dato en pantalla.
                        int nCount = FASEPSP.NumeroTareasRecurso(tr, iCodFase, iCodRecurso);
                        if (sRecTareas != "") sRecTareas += "##"; //Separador de recursos
                        sRecTareas += iCodRecurso.ToString() + "//" + nCount.ToString();
                    }
                }
            }
            #endregion
            #region Pool de grupos funcionales y de profesionales
            if (strDatosPoolGF != "")
            {
                //Ahora recojo los grupos funcionales del pool del proyecto técnico
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
                                    POOL_GF_FASE.Insert(tr, iCodFase, iCodRecurso);
                                    break;
                                case "D":
                                    POOL_GF_FASE.Delete(tr, iCodFase, iCodRecurso);
                                    break;
                            }
                        }
                    }
                }
            }
            if (strDatosPoolProf != "")
            {
                //Ahora recojo los profesionales del pool de la fase
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
                                    POOL_FASE.Insert(tr, iCodFase, iCodRecurso);
                                    break;
                                case "D":
                                    POOL_FASE.Delete(tr, iCodFase, iCodRecurso);
                                    break;
                            }
                        }
                    }
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + DateTime.Now.ToString() + "@#@" + Session["UsuarioActual"].ToString() + "@#@" + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString() + "@#@" + sRecTareas;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la fase", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
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
                DOCUF.Delete(tr, int.Parse(oDoc));
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
    private string ObtenerListaDocumentos(string sIdFase, int idUser)
    {
        StringBuilder sbuilder = new StringBuilder();
        try
        {
            sbuilder.Append("<table id='tblDocumentos2' class='texto MANO' style='width: 850px;'>");
            sbuilder.Append("<colgroup><col style='width:290px;' /><col style='width:235px' /><col style='width:225px' /><col style='width:100px' /></colgroup>");
            sbuilder.Append("<tbody>");
            if (sIdFase != "")
            {
                SqlDataReader dr = DOCUF.Lista(null, int.Parse(sIdFase.Replace(".", "")), idUser);
                int i = 0;
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
                        string sNomArchivo = dr["Nombrearchivo"].ToString();// + Utilidades.TamanoArchivo((int)dr["bytes"]);
                        //Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                        //if ((!(bool)dr["Privado"]) || ((bool)dr["Privado"] && dr["Num_Autor"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString()) || Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A")
                        sbuilder.Append("<td><img src=\"../../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">&nbsp;<nobr class='NBR' style='width:205px;' >" + sNomArchivo + "</nobr></td>");
                        //else
                        //    sbuilder.Append("<td><img src=\"../../../images/imgSeparador.gif\" width='16px' height='16px' style='vertical-align:bottom;'>&nbsp;<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
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

                    i++;
                }
                dr.Close();
                dr.Dispose();
            }
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table>");
        }
        catch (Exception ex)
        {
            sbuilder.Append("Error@#@" + Errores.mostrarError("Error al obtener documentos dependientes de la fase", ex));
        }
        return sbuilder.ToString();
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

    private string ObtenerPoolProf(string sCodPT, string sCodFase, string sCR, bool bMostrarBajas)
    {
        //Relacion de profesionales asignados al pool de la fase que no vengan heredados del PT
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAsignados3' class='texto' style='width: 370px;'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:340px;' /></colgroup>");
            sb.Append("<tbody>");
            if (sCodPT != "" && sCodFase != "")
            {
                int iCodPT = int.Parse(sCodPT.Replace(".", ""));
                int iCodFase = int.Parse(sCodFase.Replace(".", ""));
                SqlDataReader dr = POOL_FASE.Catalogo(iCodPT, iCodFase, bMostrarBajas);
                while (dr.Read())
                {
                    sb.Append("<tr bd='' id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCR) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");

                    if (int.Parse(dr["heredado"].ToString()) == 0)
                        sb.Append(" h='N' onclick='mm(event);' onmousedown='DD(event)' class='texto'>");
                    else
                        sb.Append(" h='S' style='color=gray'>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
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
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales del pool asociados a la fase.", ex);
        }
        return sResul;
    }

    private string ObtenerPoolGF(string sCodPT, string sCodFase)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;

        sb.Append("<table id='tblPoolGF' class='texto' style='width: 370px;'>");
        sb.Append("<colgroup><col style='width:15px;' /><col style='width:355px;' /></colgroup>");
        sb.Append("<tbody>");
        if (sCodPT != "" && sCodFase != "")
        {
            int iCodPT = int.Parse(sCodPT.Replace(".", ""));
            int iCodFase = int.Parse(sCodFase.Replace(".", ""));
            SqlDataReader dr = POOL_GF_FASE.Catalogo(iCodPT, iCodFase);
            while (dr.Read())
            {
                sb.Append("<tr bd='N' id='" + dr["idGF"].ToString());
                if (int.Parse(dr["heredado"].ToString()) == 0)
                    sb.Append("'h='N' onclick=\"mm(event)\" style='height:16px;'>");
                else
                    sb.Append("' h='S' style='height:16px;color=gray'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td><td>");
                sb.Append(dr["desGF"].ToString());
                sb.Append("</td></tr>");
                i++;
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

}

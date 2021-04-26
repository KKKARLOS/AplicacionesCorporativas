using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Capa_Presentacion_PSP_Tarea_Bitacora_Consulta_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, strTablaHtmlAsunto;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 27;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.TituloPagina = "Consulta de Bitácora de tarea";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

            try
            {
                if (!Page.IsPostBack)
                {
                    Utilidades.SetEventosFecha(this.txtFechaInicio);
                    Utilidades.SetEventosFecha(this.txtFechaFin);
                    Utilidades.SetEventosFecha(this.txtLimD);
                    Utilidades.SetEventosFecha(this.txtLimH);
                    Utilidades.SetEventosFecha(this.txtFinD);
                    Utilidades.SetEventosFecha(this.txtFinH);

                    strTablaHtmlAsunto = "<table id='tblDatos1'></table>";
                    //this.txtUne.Text = Session["NodoActivo"].ToString();
                    //this.txtDesCR.Text = Session["DesNodoActivo"].ToString();
                    if (Request.QueryString["sCodProy"] != null)
                    {
                        this.txtEstado.Text = Request.QueryString["sEstado"].ToString();
                        this.txtUne.Text = Request.QueryString["sCR"].ToString();
                        //this.txtDesCR.Text = Request.QueryString["sDesCR"].ToString();
                        this.txtCodProy.Text = Request.QueryString["sCodProy"].ToString();
                        this.txtNomProy.Text = Request.QueryString["sNomProy"].ToString();
                    }
                    else
                    {
                        if (Session["ID_PROYECTOSUBNODO"].ToString() != "")
                        {
                            int nPE = int.Parse(Session["ID_PROYECTOSUBNODO"].ToString());
                            this.hdnT305IdProy.Value = nPE.ToString();
                            SqlDataReader dr = PROYECTO.fgGetDatosProy2(nPE);
                            if (dr.Read())
                            {
                                this.txtUne.Text = dr["t303_idnodo"].ToString();
                                //this.txtDesCR.Text = dr["t303_denominacion"].ToString();
                                this.txtEstado.Text = dr["t301_estado"].ToString();
                                this.txtNomProy.Text = dr["t301_denominacion"].ToString();
                                this.txtCodProy.Text = int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###");
                            }
                            dr.Close();
                            dr.Dispose();
                        }
                    }
                    //Relleno el combo de tipo de asunto 
                    this.cboTipo.DataValueField = "t384_idtipo";
                    this.cboTipo.DataTextField = "t384_destipo";
                    this.cboTipo.DataSource = TIPOASUNTO.Catalogo("", null, null, 3, 0);
                    this.cboTipo.DataBind();
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getAsuntos"):
                sResultado += obtenerAsuntos(aArgs[1], aArgs[2], aArgs[3], byte.Parse(aArgs[5]), int.Parse(aArgs[6])
                                             , byte.Parse(aArgs[7]), byte.Parse(aArgs[8])
                                             , aArgs[9], aArgs[10]
                                             , aArgs[11], aArgs[12]
                                             , aArgs[13], aArgs[14]
                                             , aArgs[15], aArgs[16]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("getTarea"):
                sResultado += buscarTarea(aArgs[1]);
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
    private string obtenerAsuntos(string sT, string sOrden, string sAscDesc, byte iEstado, int iTipo,
                                  byte iSeveridad, byte iPrioridad,
                                  string notifD, string notifH,
                                  string limD, string limH,
                                  string finD, string finH,
                                  string sAcciones, string desAsunto)
    {
     try
        {    
            StringBuilder sb = new StringBuilder();
            int i = 0, nT;
            string sIdAsuntoAnt, sIdAsuntoAct, sFila;

            nT = int.Parse(quitaPuntos(sT));
 
            sb.Append("<table id='tblDatos1' style='width: 970px;'>");
            sb.Append("<colgroup><col style='width:70px' /><col style='width:100px' /><col style='width:270px' /><col style='width:50px' />");
            sb.Append("<col style='width:50px' /><col style='width:65px' /><col style='width:65px' /><col style='width:45px' />");
            sb.Append("<col style='width:65px' /><col style='width:190px' /></colgroup>");

            SqlDataReader dr = ASUNTO_T.Catalogo2(nT, desAsunto, iEstado, finD, finH, limD, limH, notifD, notifH, iPrioridad,
                                                iSeveridad, iTipo, byte.Parse(sOrden), byte.Parse(sAscDesc), sAcciones);
            sIdAsuntoAnt = "-1";
            while (dr.Read())
            {
                if (sAcciones == "N")
                {
                    sFila = ponerFilaAsunto(dr, i, false);
                    sb.Append(sFila);
                }
                else
                {
                    sIdAsuntoAct = dr["t600_idasunto"].ToString();
                    if (sIdAsuntoAnt != sIdAsuntoAct)
                    {
                        sFila = ponerFilaAsunto(dr, i, true);
                        sb.Append(sFila);
                        i++;
                        sFila = ponerFilaAccion(dr, i);
                    }
                    else
                    {
                        sFila = ponerFilaAccion(dr, i);
                    }
                    sb.Append(sFila);
                    sIdAsuntoAnt = sIdAsuntoAct;
                }
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            strTablaHtmlAsunto = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
        }
        return "OK@#@" + strTablaHtmlAsunto;
}
    private string quitaPuntos(string sCadena)
    {
        //Finalidad:Elimina los puntos de una cadena
        string sRes;

        sRes = sCadena;
        try
        {
            if (sCadena == "") return "";
            sRes = sRes.Replace(".", "");
        }
        catch (Exception ex)
        {
            Errores.mostrarError("Error al quitar puntos de la cadena" + sCadena, ex);
        }
        return sRes;
    }
    private string ponerFilaAsunto(SqlDataReader dr, int i, bool bConAcciones)
    {
        StringBuilder sB = new StringBuilder();
        string sDesAsunto, sFecha;
        try
        {
            if (bConAcciones)
            {//Si la consulta lleva asuntos y acciones, las lineas de asuntos las pintamos de color
                sB.Append("<tr class=FAM1 ");
            }
            else
            {
                if (i % 2 == 0) sB.Append("<tr class=FA ");
                else sB.Append("<tr class=FB ");
            }
            sDesAsunto = HttpUtility.HtmlEncode(dr["t600_desasunto"].ToString());
            sB.Append("id='" + dr["t600_idasunto"].ToString() + "' ");
            sB.Append("obs='" + Utilidades.escape(dr["t600_obs"].ToString()) + "' idAc='-1' onmouseover='TTip(event)'>");

            sFecha = dr["t600_fnotificacion"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t600_fnotificacion"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sB.Append("<td><span class='NBR' style='width:95px;'>" + dr["t384_destipo"].ToString() + "</span></td>");
            sB.Append("<td><span class='NBR' style='width:265px;'>" + sDesAsunto + "</span></td>");
            sB.Append("<td>" + dr["t600_severidad"].ToString() + "</td>");
            sB.Append("<td>" + dr["t600_prioridad"].ToString() + "</td>");
            sFecha = dr["t600_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t600_flimite"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t600_ffin"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t600_ffin"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sB.Append("<td style='text-align:right;'>&nbsp;</td>");//avance solo en aciones
            sB.Append("<td>" + dr["t600_estado"].ToString() + "</td>");
            sB.Append("<td><span class='NBR' style='width:188px;'>" + dr["t600_desasuntolong"].ToString() + "</span></td>");
            sB.Append("</tr>");
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al poner fila de asunto", ex);
        }
        return sB.ToString();
    }
    private string ponerFilaAccion(SqlDataReader dr, int i)
    {
        StringBuilder sB = new StringBuilder();
        string sDesAccion, sFecha;
        try
        {
            if (i % 2 == 0) sB.Append("<tr class=FA ");
            else sB.Append("<tr class=FB ");
            sDesAccion = HttpUtility.HtmlEncode(dr["t601_desaccion"].ToString());
            sB.Append("id='" + dr["t600_idasunto"].ToString() + "' ");
            sB.Append("obs='" + Utilidades.escape(dr["t601_obs"].ToString()) + "' ");
            sB.Append("idAc='" + dr["t601_idaccion"].ToString() + "' onmouseover='TTip(event)'>");
            sB.Append("<td>&nbsp;</td><td>&nbsp;</td>");//FNotif y tipo solo en asuntos
            sB.Append("<td><span class='NBR' style='width:265px;'>" + sDesAccion + "</span></td>");
            sB.Append("<td>&nbsp;</td><td>&nbsp;</td>");//Severidad y prioridad  solo en asuntos
            sFecha = dr["t601_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t601_flimite"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t601_ffin"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t601_ffin"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");

            sB.Append("<td style='text-align:right; padding-right:5px;'>" + dr["t601_avance"].ToString() + "</td>");
            sB.Append("<td>&nbsp;</td>");//Estado solo en asuntos
            sB.Append("<td><span class='NBR' style='width:188px;'>" + dr["t601_desaccionlong"].ToString() + "</nobr></td>");
            sB.Append("</tr>");
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al poner fila de acción", ex);
        }
        return sB.ToString();
    }
    private string recuperarPSN(string nIdProySubNodo)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nIdProySubNodo), (int)Session["UsuarioActual"], "PST");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //4
                sb.Append(dr["estado"].ToString());  //5
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
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pst", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], true, true, false);
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
    private string buscarTarea(string sIdTarea)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        int idTarea = int.Parse(sIdTarea);
        try
        {
            TAREAPSP oTar = TAREAPSP.Obtener(null, idTarea);
            if (oTar.t332_destarea != "")
            {
                sb.Append(oTar.t305_idproyectosubnodo + "##");
                sb.Append(oTar.t303_idnodo + "##");
                sb.Append(oTar.t301_estado + "##");
                sb.Append(oTar.num_proyecto.ToString("#,###") + "##");
                sb.Append(oTar.nom_proyecto + "##");
                sb.Append(oTar.t331_idpt + "##");
                sb.Append(oTar.t331_despt + "##");
                sb.Append(oTar.t334_idfase + "##");
                sb.Append(oTar.t334_desfase + "##");
                sb.Append(oTar.t335_idactividad + "##");
                sb.Append(oTar.t335_desactividad + "##");
                sb.Append(idTarea.ToString("#,###") + "##");
                sb.Append(oTar.t332_destarea + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al buscar la tarea", ex);
        }
        return sResul;
    }
}
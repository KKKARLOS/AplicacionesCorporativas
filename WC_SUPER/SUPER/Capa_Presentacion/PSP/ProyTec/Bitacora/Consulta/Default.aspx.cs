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


public partial class Capa_Presentacion_PSP_ProyTec_Bitacora_Consulta_Default : System.Web.UI.Page, ICallbackEventHandler
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
            Master.TituloPagina = "Consulta de Bitácora de proyecto técnico";
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
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string obtenerAsuntos(string sPT, string sOrden, string sAscDesc, byte iEstado, int iTipo,
                                  byte iSeveridad, byte iPrioridad,
                                  string notifD, string notifH,
                                  string limD, string limH,
                                  string finD, string finH,
                                  string sAcciones, string desAsunto)
    {
     try
        {    
            StringBuilder strBuilder = new StringBuilder();
            int i = 0, nPT;
            string sIdAsuntoAnt, sIdAsuntoAct, sFila;

            nPT = int.Parse(quitaPuntos(sPT));

            strBuilder.Append("<table id='tblDatos1' style='width: 970px;'>");
            strBuilder.Append("<colgroup><col style='width:70px' /><col style='width:100px' /><col style='width:270px' /><col style='width:50px' />");
            strBuilder.Append("<col style='width:50px' /><col style='width:65px' /><col style='width:65px' /><col style='width:45px' />");
            strBuilder.Append("<col style='width:65px' /><col style='width:190px' /></colgroup>");

            SqlDataReader dr = ASUNTO_PT.Catalogo2(nPT, desAsunto, iEstado, finD, finH, limD, limH, notifD, notifH, iPrioridad,
                                                iSeveridad, iTipo, byte.Parse(sOrden), byte.Parse(sAscDesc), sAcciones);
            sIdAsuntoAnt = "-1";
            while (dr.Read())
            {
                if (sAcciones == "N")
                {
                    sFila = ponerFilaAsunto(dr, i, false);
                    strBuilder.Append(sFila);
                }
                else
                {
                    sIdAsuntoAct = dr["t409_idasunto"].ToString();
                    if (sIdAsuntoAnt != sIdAsuntoAct)
                    {
                        sFila = ponerFilaAsunto(dr, i, true);
                        strBuilder.Append(sFila);
                        i++;
                        sFila = ponerFilaAccion(dr, i);
                    }
                    else
                    {
                        sFila = ponerFilaAccion(dr, i);
                    }
                    strBuilder.Append(sFila);
                    sIdAsuntoAnt = sIdAsuntoAct;
                }
                i++;
            }
            dr.Close();
            dr.Dispose();

            strBuilder.Append("</table>");
            strTablaHtmlAsunto = strBuilder.ToString();
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
            sDesAsunto = HttpUtility.HtmlEncode(dr["t409_desasunto"].ToString());

            sB.Append("id='" + dr["t409_idasunto"].ToString() + "' ");
            sB.Append("obs='" + Utilidades.escape(dr["t409_obs"].ToString()) + "' idAc='-1' onmouseover='TTip(event)'>");
            sFecha = dr["t409_fnotificacion"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t409_fnotificacion"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sB.Append("<td><span class='NBR' style='width:95px;'>" + dr["t384_destipo"].ToString() + "</span></td>");
            sB.Append("<td><span class='NBR' style='width:265px;'>" + sDesAsunto + "</span></td>");
            sB.Append("<td>" + dr["t409_severidad"].ToString() + "</td>");
            sB.Append("<td>" + dr["t409_prioridad"].ToString() + "</td>");
            sFecha = dr["t409_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t409_flimite"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t409_ffin"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t409_ffin"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sB.Append("<td style='text-align:right;'>&nbsp;</td>");//avance solo en aciones
            sB.Append("<td>" + dr["t409_estado"].ToString() + "</td>");
            sB.Append("<td><span class='NBR' style='width:188px;'>" + dr["t409_desasuntolong"].ToString() + "</span></td>");
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
            sDesAccion = HttpUtility.HtmlEncode(dr["t410_desaccion"].ToString());
            sB.Append("id='" + dr["t409_idasunto"].ToString() + "' ");
            sB.Append("obs='" + Utilidades.escape(dr["t410_obs"].ToString()) + "' ");
            sB.Append("idAc='" + dr["t410_idaccion"].ToString() + "' onmouseover='TTip(event)'>");
            sB.Append("<td>&nbsp;</td><td>&nbsp;</td>");//FNotif y tipo solo en asuntos
            sB.Append("<td><span class='NBR' style='width:265px;'>" + sDesAccion + "</nobr></td>");
            sB.Append("<td>&nbsp;</td><td>&nbsp;</td>");//Severidad y prioridad  solo en asuntos
            sFecha = dr["t410_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t410_flimite"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t410_ffin"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t410_ffin"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");

            sB.Append("<td style='text-align:right; padding-right:5px;'>" + dr["t410_avance"].ToString() + "</td>");
            sB.Append("<td>&nbsp;</td>");//Estado solo en asuntos
            sB.Append("<td><span class='NBR' style='width:188px;'>" + dr["t410_desaccionlong"].ToString() + "</span></td>");
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
                    Session["RTPT_PROYECTOSUBNODO"] = (dr["rtpt"].ToString() == "1") ? true : false; Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();

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
}
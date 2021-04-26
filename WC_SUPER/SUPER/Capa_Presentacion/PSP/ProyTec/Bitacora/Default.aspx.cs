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

using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtmlAsunto, strTablaHtmlAccion, sErrores, sOrigen = "", sAccesoBitacoraPT = "E", strTablaHtmlTs;//, sAccBitacora = "X";
    public SqlConnection oConn;
    public SqlTransaction tr;
    private int nPSN = -1;

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
            if (!Page.IsPostBack)
            {
                try
                {
                    LimpiarDatos();
                    //origen
                    if (Request.QueryString["o"] != null)
                        sOrigen = Utilidades.decodpar(Request.QueryString["o"].ToString());
                    //sAccBitacoraPT
                    if (Request.QueryString["b"] != null)
                        sAccesoBitacoraPT = Utilidades.decodpar(Request.QueryString["b"].ToString());
                    //if (Request.QueryString["sAccBitacora"] != null)
                    //    sAccBitacora = Request.QueryString["sAccBitacora"].ToString();
                    if (Request.QueryString["psn"] != null)
                        nPSN = int.Parse(Utilidades.decodpar(Request.QueryString["psn"].ToString()));
                    else
                    {
                        if (Session["ID_PROYECTOSUBNODO"].ToString() != "")
                            nPSN = int.Parse(Session["ID_PROYECTOSUBNODO"].ToString());
                    }
                    //Código de proyecto técnico
                    if (Request.QueryString["pt"] != null)
                    {
                        this.txtIdPT.Text = Utilidades.decodpar(Request.QueryString["pt"].ToString());
                        this.txtDesPT.Text = Utilidades.decodpar(Request.QueryString["npt"].ToString());
                        ProyTec oPT = ProyTec.Obtener(int.Parse(this.txtIdPT.Text));
                        nPSN = oPT.t305_idproyectosubnodo;
                    }
                    if (nPSN > -1)
                        {
                        this.hdnT305IdProy.Value = nPSN.ToString();
                        string sModoAcceso = PROYECTOSUBNODO.getAcceso(null, nPSN, (int)Session["UsuarioActual"]);
                        SqlDataReader dr = PROYECTO.fgGetDatosProy(nPSN);
                        if (dr.Read())
                        {
                            //sAccesoBitacoraPT = dr["t305_accesobitacora_pst"].ToString();
                            if (sAccesoBitacoraPT != "X")
                            {
                                this.txtEstado.Text = dr["t301_estado"].ToString();
                                this.txtNomProy.Text = dr["t301_denominacion"].ToString();
                                this.txtCodProy.Text = int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###");
                                if (sModoAcceso == "R")
                                    sAccesoBitacoraPT = "L";
                                else
                                {
                                    if (this.txtEstado.Text == "C" || this.txtEstado.Text == "H")
                                        sAccesoBitacoraPT = "L";
                                }
                            }
                        }
                        dr.Close();
                        dr.Dispose();
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
                        if (sAccesoBitacoraPT != "X" && this.txtIdPT.Text !="")
                        {
                            //ObtenerDatosAsunto(int.Parse(quitaPuntos(this.txtIdPT.Text)));
                            obtenerAsuntos(quitaPuntos(this.txtIdPT.Text),"8","0",0,0);
                            ObtenerDatosTs(int.Parse(this.txtIdPT.Text));
                        }
                    }
                    //Relleno el combo de tipo de asunto 
                    this.cboTipo.DataValueField = "t384_idtipo";
                    this.cboTipo.DataTextField = "t384_destipo";
                    this.cboTipo.DataSource = TIPOASUNTO.Catalogo("", null, null, 3, 0);
                    this.cboTipo.DataBind();
                }
                catch (Exception ex)
                {
                    sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
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
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getAsuntos"):
                sResultado += obtenerAsuntos(aArgs[1], aArgs[2], aArgs[3], byte.Parse(aArgs[5]), int.Parse(aArgs[6]));
                break;
            case ("getAsuntos2"):
                sResultado += obtenerAsuntos(aArgs[1], aArgs[2], aArgs[3], byte.Parse(aArgs[5]), int.Parse(aArgs[6]));
                sResultado += "@#@" + ObtenerDatosTs(int.Parse(aArgs[1]));
                break;
            case ("getAcciones"):
                sResultado += obtenerAcciones(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("borrarAsunto"):
                sResultado += borrarAsunto(aArgs[1]);
                break;
            case ("borrarAccion"):
                sResultado += borrarAccion(aArgs[1]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
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
    private string obtenerAsuntos(string sPT, string sOrden, string sAscDesc, byte iEstado, int iTipo)
    {
        StringBuilder sb = new StringBuilder();
        int nPT;
        string sIdAsunto, sDesAsunto, sFecha, sIdResponsable;

        nPT = int.Parse(quitaPuntos(sPT));

        sb.Append("<table id='tblDatos1' class='texto MA' style='width:940px; text-align:left;'>");
        sb.Append("<colgroup><col style='width:300px;' /><col style='width:190px;' /><col style='width:87px;' /><col style='width:90px;' /><col style='width:88px;' /><col style='width:90px;' /><col style='width:95px;' /></colgroup>");
        sb.Append("<tbody>");
        SqlDataReader dr = ASUNTO_PT.Catalogo(nPT, "", iEstado, null, null, null, null, null, null, null, "", null, "", null, null,
                                           null, "", iTipo, byte.Parse(sOrden), byte.Parse(sAscDesc));
        while (dr.Read())
        {
            sIdAsunto = dr["t409_idasunto"].ToString();
            sDesAsunto = HttpUtility.HtmlEncode(dr["t409_desasunto"].ToString());
            sIdResponsable = dr["t409_responsable"].ToString();
            sb.Append("<tr id='" + sIdAsunto + "' idR='" + sIdResponsable + "'");
            sb.Append(" ondblclick='mDetAsunto(this.id)'");
            sb.Append(" onclick='ms(this); obtenerAcciones(" + sIdAsunto + ")'");
            sb.Append(" style='height:16px;' onmouseover='TTip(event)'>");
            sb.Append("<td><nobr class='NBR W300' style='padding-left:3px;'>" + sDesAsunto + "</nobr></td>");
            sb.Append("<td><nobr class='NBR W180'>" + dr["t384_destipo"].ToString() + "</nobr></td>");
            sb.Append("<td>" + dr["t409_severidad"].ToString() + "</td>");
            sb.Append("<td>" + dr["t409_prioridad"].ToString() + "</td>");
            sFecha = dr["t409_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t409_flimite"].ToString()).ToShortDateString();
            sb.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t409_fnotificacion"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t409_fnotificacion"].ToString()).ToShortDateString();
            sb.Append("<td>" + sFecha + "</td>");
            sb.Append("<td>" + dr["t409_estado"].ToString() + "</td>");

            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtmlAsunto = sb.ToString();
        return "OK@#@" + strTablaHtmlAsunto;
    }
    private void LimpiarDatos()
    {
        StringBuilder sB = new StringBuilder();
        sB.Append("<table id='tblDatos1' class='texto' style='width:650px;'>");
        sB.Append("<colgroup><col style='width:300px;' /><col style='width:75px' /><col style='width:50px' /><col style='width:25px' /><col style='width:75px' /></colgroup>");
        sB.Append("</table>");
        strTablaHtmlAsunto = sB.ToString();

        StringBuilder sB2 = new StringBuilder();
        sB2.Append("<table id='tblDatos2' class='texto' style='width: 600px;'>");
        sB2.Append("<colgroup><col style='width:380px;' /><col style='width:65px;' /><col style='width:70px;' /><col style='width:85px;' /></colgroup>");
        sB2.Append("</table>");
        strTablaHtmlAccion = sB2.ToString();
    }
    private string obtenerAcciones(string sIdAsunto, string sOrden, string sAscDesc)
    {
        StringBuilder sB = new StringBuilder();
        int nIdAsunto;
        string sIdAccion, sFecha, sIdResponsable;

        if (sIdAsunto == "") return "error@#@Se ha intentado recoger las acciones de un asunto sin código";
        nIdAsunto = int.Parse(sIdAsunto);
        sB.Append("<table id='tblDatos2' class='texto MA' style='width:600px; text-align:left;'>");
        sB.Append("<colgroup><col style='width:380px;' /><col style='width:65px;' /><col style='width:70px;' /><col style='width:85px;' /></colgroup>");
        //sB.Append("<tbody>");
        //sDesAsunto = HttpUtility.HtmlEncode(sDesAsunto);
        SqlDataReader dr = ACCION_PT.Catalogo(null, nIdAsunto, null, "", null, null, null, byte.Parse(sOrden), byte.Parse(sAscDesc));
        while (dr.Read())
        {
            sIdAccion = dr["t410_idaccion"].ToString();
            sIdResponsable = dr["t409_responsable"].ToString();
            sB.Append("<tr id='" + sIdAccion + "' style='height:16px;' onclick='ms(this);' ondblclick='mDetAccion(this.id," + sIdAsunto + "," + sIdResponsable + ")' onmouseover='TTip(event)'>");
            sB.Append("<td style='padding-left:3px;'><nobr class='NBR' style='width:370px;'>" + HttpUtility.HtmlEncode(dr["t410_desaccion"].ToString()) + "</nobr></td>");
            sFecha = dr["t410_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t410_flimite"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");

            sB.Append("<td style='text-align:right; padding-right:10px;'>" + dr["t410_avance"].ToString() + "</td>");
            sFecha = dr["t410_ffin"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t410_ffin"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td></tr>");
        }
        dr.Close();
        dr.Dispose();
        //sB.Append("</tbody>");
        sB.Append("</table>");
        strTablaHtmlAccion = sB.ToString();
        return "OK@#@" + strTablaHtmlAccion;
    }
    private string borrarAsunto(string sIdAsunto)
    {
        string sResul;
        try
        {
            if (sIdAsunto != "")
                ASUNTO_PT.Delete(tr, int.Parse(sIdAsunto));
            sResul = "OK@#@" + sIdAsunto;
        }
        catch (Exception e)
        {
            sResul = "error@#@" + e.Message;
        }
        return sResul;
    }
    private string borrarAccion(string sIdAccion)
    {
        string sResul;
        try
        {
            if (sIdAccion != "")
                ACCION_PT.Delete(tr, int.Parse(sIdAccion));
            sResul = "OK@#@" + sIdAccion;
        }
        catch (Exception e)
        {
            sResul = "error@#@" + e.Message;
        }
        return sResul;
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
    private string recuperarPSN(string nPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy2(int.Parse(nPSN));
            if (dr.Read())
            {

                sb.Append(dr["t301_estado"].ToString() + "@#@");  //2
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //3
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "@#@");  //4
                sb.Append(dr["t303_idnodo"].ToString());//5
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
    private string ObtenerDatosTs(int nPT)
    {
        string sResul = "";
        SqlDataReader dr;
        try
        {
            StringBuilder sb = new StringBuilder();
            int iUser = (int)Session["UsuarioActual"];
            sb.Append("<table id='tblTs' class='texto MA' style='width:305px;'>");
            sb.Append("<tbody>");
            dr = TAREAPSP.CatalogoBitacora(nPT);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["cod_tarea"].ToString() + "' aIAP='" + dr["t332_acceso_iap"].ToString() + "'");
                sb.Append(" style='height:16px;' onclick='ms(this)' ondblclick='bitacoraT(this.id)'>");
                sb.Append("<td><nobr class='NBR' style='width:300px; padding-left:3px;'>" + int.Parse(dr["cod_tarea"].ToString()).ToString("#,###") + "-" +
                            HttpUtility.HtmlEncode(dr["nom_tarea"].ToString()) + "</nobr>");
                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtmlTs = sb.ToString();
            sResul = strTablaHtmlTs;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las tareas", ex);
        }
        return sResul;
    }

}
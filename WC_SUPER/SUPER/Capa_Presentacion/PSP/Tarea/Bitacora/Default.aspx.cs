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
    public string strTablaHtmlAsunto, strTablaHtmlAccion, sErrores, sAccesoBitacoraT = "E";
    public SqlConnection oConn;
    public SqlTransaction tr;

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

                    if (Request.QueryString["a"] != null)//sAccBitacoraT
                        sAccesoBitacoraT = Utilidades.decodpar(Request.QueryString["a"].ToString());
                    if (Request.QueryString["t"] != null)//sCodT
                    {
                        this.hdnOrigen.Value = "T";
                        this.txtIdTarea.Text = Utilidades.decodpar(Request.QueryString["t"].ToString());
                        if (sAccesoBitacoraT != "X")
                        {
                            string sIdTarea=quitaPuntos(this.txtIdTarea.Text);
                            getTarea(sIdTarea);
                            if (sAccesoBitacoraT != "X")
                                ObtenerDatosAsunto(int.Parse(sIdTarea));
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
                sResultado += obtenerAsuntos(aArgs[1], aArgs[2], aArgs[3], byte.Parse(aArgs[4]), int.Parse(aArgs[5]));
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
    private string obtenerAsuntos(string sTarea, string sOrden, string sAscDesc, byte iEstado, int iTipo)
    {
        StringBuilder strBuilder = new StringBuilder();
        int nT;
        string sIdAsunto, sDesAsunto, sFecha, sIdResponsable;

        nT = int.Parse(quitaPuntos(sTarea));

        strBuilder.Append("<table id='tblDatos1' class='texto MA' style='WIDTH: 940px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0'>");
        strBuilder.Append("<colgroup><col style='width:297px;' /><col style='width:190px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /></colgroup>");
        strBuilder.Append("<tbody>");
        SqlDataReader dr = ASUNTO_T.Catalogo(nT, "", iEstado, null, null, null, null, null, null, null, "", null, "", null, null,
                                           null, "", iTipo, byte.Parse(sOrden), byte.Parse(sAscDesc));
        while (dr.Read())
        {
            sIdAsunto = dr["t600_idasunto"].ToString();
            sDesAsunto = HttpUtility.HtmlEncode(dr["t600_desasunto"].ToString());
            sIdResponsable = dr["t600_responsable"].ToString();
            strBuilder.Append("<tr id='" + sIdAsunto + "' idR='" + sIdResponsable + "' ondblclick='mDetAsunto(this.id)' onclick='ms(this);obtenerAcciones(" + sIdAsunto + ")' style='height:16px; padding-left:3px;' onmouseover='TTip(event)'>");
            strBuilder.Append("<td><nobr class='NBR W300'>" + sDesAsunto + "</nobr></td>");
            strBuilder.Append("<td><nobr class='NBR W180'>" + dr["t384_destipo"].ToString() + "</nobr></td>");
            strBuilder.Append("<td>" + dr["t600_severidad"].ToString() + "</td>");
            strBuilder.Append("<td>" + dr["t600_prioridad"].ToString() + "</td>");
            sFecha = dr["t600_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t600_flimite"].ToString()).ToShortDateString();
            strBuilder.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t600_fnotificacion"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t600_fnotificacion"].ToString()).ToShortDateString();
            strBuilder.Append("<td>" + sFecha + "</td>");
            strBuilder.Append("<td>" + dr["t600_estado"].ToString() + "</td>");

            strBuilder.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        strBuilder.Append("</tbody>");
        strBuilder.Append("</table>");
        strTablaHtmlAsunto = strBuilder.ToString();
        return "OK@#@" + strTablaHtmlAsunto;
    }
    private void ObtenerDatosAsunto(int nTarea)
    {
        StringBuilder strBuilder = new StringBuilder();
        string sIdAsunto, sDesAsunto, sFecha;

        strBuilder.Append("<table id='tblDatos1' class='texto MA' style='WIDTH: 940px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0'>");
        strBuilder.Append("<colgroup><col style='width:297px;' /><col style='width:190px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /><col style='width:90px;' /></colgroup>");
        strBuilder.Append("<tbody>");
        SqlDataReader dr = ASUNTO_T.Catalogo(nTarea, "", null, null, null, null, null, null, null, null, "", null, "", null, null,
                                           null, "", null, 8, 0);
        while (dr.Read())
        {
            sIdAsunto = dr["t600_idasunto"].ToString();
            sDesAsunto = HttpUtility.HtmlEncode(dr["t600_desasunto"].ToString());
            strBuilder.Append("<tr id='" + sIdAsunto + "' ondblclick='mDetAsunto(this.id)' onclick='ms(this);obtenerAcciones(" + sIdAsunto + ")' style='height:16px;' onmouseover='TTip(event)'>");
            strBuilder.Append("<td STYLE='padding-left:3px;'><nobr class='NBR W300'>" + sDesAsunto + "</nobr></td>");
            strBuilder.Append("<td><nobr class='NBR W180'>" + dr["t384_destipo"].ToString() + "</nobr></td>");
            strBuilder.Append("<td>" + dr["t600_severidad"].ToString() + "</td>");
            strBuilder.Append("<td>" + dr["t600_prioridad"].ToString() + "</td>");
            sFecha = dr["t600_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t600_flimite"].ToString()).ToShortDateString();
            strBuilder.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t600_fnotificacion"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t600_fnotificacion"].ToString()).ToShortDateString();
            strBuilder.Append("<td>" + sFecha + "</td>");
            strBuilder.Append("<td>" + dr["t600_estado"].ToString() + "</td>");

            strBuilder.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        strBuilder.Append("</tbody>");
        strBuilder.Append("</table>");
        strTablaHtmlAsunto = strBuilder.ToString();
    }
    private void LimpiarDatos()
    {
        StringBuilder sB = new StringBuilder();
        sB.Append("<table id='tblDatos1' class='texto' style='width:650px;'>");
        sB.Append("<colgroup><col style='width:300px;' /><col style='width:75px' /><col style='width:50px' /><col style='width:25px' /><col style='width:75px' /></colgroup>");
        sB.Append("</table>");
        strTablaHtmlAsunto = sB.ToString();

        StringBuilder sB2 = new StringBuilder();
        sB2.Append("<table id='tblDatos2' class='texto' style='width:650px;'>");
        sB2.Append("<colgroup><col style='width:300px;' /><col style='width:75px' /><col style='width:50px' /><col style='width:25px' /><col style='width:75px' /></colgroup>");
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
        sB.Append("<table id='tblDatos2' class='texto MA' style='width:650px;'>");
        sB.Append("<colgroup><col style='width:420px;' /><col style='width:70px' /><col style='width:70px;' /><col style='width:90px;' /></colgroup>");
        sB.Append("<tbody>");
        SqlDataReader dr = ACCION_T.Catalogo(null, nIdAsunto, null, "", null, null, null, byte.Parse(sOrden), byte.Parse(sAscDesc));
        while (dr.Read())
        {
            sIdAccion = dr["t601_idaccion"].ToString();
            sIdResponsable = dr["t600_responsable"].ToString();
            sB.Append("<tr id='" + sIdAccion + "' style='height:16px;' onclick='ms(this);' ondblclick='mDetAccion(this.id," + sIdAsunto + "," + sIdResponsable + ")' onmouseover='TTip(event)'>");
            sB.Append("<td><nobr class='NBR' style='width:410px; padding-left:3px;'>" + HttpUtility.HtmlEncode(dr["t601_desaccion"].ToString()) + "</nobr></td>");
            sFecha = dr["t601_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t601_flimite"].ToString()).ToShortDateString();
            sB.Append("<td style='padding-left:5px;'>" + sFecha + "</td>");

            sB.Append("<td style='text-align:right; padding-right:10px;'>" + dr["t601_avance"].ToString() + "</td>");
            sFecha = dr["t601_ffin"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t601_ffin"].ToString()).ToShortDateString();
            sB.Append("<td style='padding-left:5px;'>" + sFecha + "</td></tr>");
        }
        dr.Close();
        dr.Dispose();
        sB.Append("</tbody>");
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
                ASUNTO_T.Delete(tr, int.Parse(sIdAsunto));
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
                ACCION_T.Delete(tr, int.Parse(sIdAccion));
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
    private void getTarea(string sIdTarea)
    {
        int idTarea = int.Parse(sIdTarea);
        try
        {
            TAREAPSP oTar = TAREAPSP.Obtener(null, idTarea);
            if (oTar.t332_destarea != "")
            {
                this.hdnT305IdProy.Value = oTar.t305_idproyectosubnodo.ToString();
                this.txtEstado.Text = oTar.t301_estado;
                if (this.txtEstado.Text == "C" || this.txtEstado.Text == "H")
                    sAccesoBitacoraT = "L";
                this.txtNomProy.Text = oTar.nom_proyecto;
                this.txtCodProy.Text = oTar.num_proyecto.ToString("#,###");
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
                this.hdnIdPT.Text = oTar.t331_idpt.ToString();
                this.txtDesPT.Text = oTar.t331_despt;
                this.hdnIdFase.Text = oTar.t334_idfase.ToString();
                this.txtFase.Text = oTar.t334_desfase;
                this.hdnIdActividad.Text = oTar.t335_idactividad.ToString();
                this.txtActividad.Text = oTar.t335_desactividad;
                this.txtIdTarea.Text = idTarea.ToString("#,###");
                this.txtDesTarea.Text = oTar.t332_destarea.Replace("<", " ");
            }
        }
        catch (Exception ex)
        {
            Errores.mostrarError("Error al buscar la tarea", ex);
        }
    }
}
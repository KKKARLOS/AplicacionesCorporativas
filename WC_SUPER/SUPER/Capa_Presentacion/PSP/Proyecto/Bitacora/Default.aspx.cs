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
    public string strTablaHtmlAsunto, strTablaHtmlAccion, sErrores, strTablaHtmlPTs;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int nPSN = -1;
    public string sOrigen = "", sAccesoBitacoraPE = "X";

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
                    strTablaHtmlAsunto = "<table id='tblDatos1'></table>";
                    strTablaHtmlAccion = "<table id='tblDatos2'></table>";
                    strTablaHtmlPTs = "<table id='tblPTs'></table>";

                    if (Request.QueryString["sOrigen"] != null)
                        sOrigen = Utilidades.decodpar(Request.QueryString["sOrigen"].ToString());
                    if (Request.QueryString["sAccesoBitacoraPE"] != null)
                        sAccesoBitacoraPE = Utilidades.decodpar(Request.QueryString["sAccesoBitacoraPE"].ToString());
                    
                    if (Request.QueryString["sT305IdProy"] != null)
                    {
                        this.hdnOrigen.Value = "T";
                        nPSN = int.Parse(Utilidades.decodpar(Request.QueryString["sT305IdProy"].ToString()));
                        this.hdnT305IdProy.Value = nPSN.ToString();
                        this.txtEstado.Text = Utilidades.decodpar(Request.QueryString["sEstado"].ToString());
                        this.txtCodProy.Text = Utilidades.decodpar(Request.QueryString["sCodProy"].ToString());
                        this.txtNomProy.Text = Utilidades.decodpar(Request.QueryString["sNomProy"].ToString());
                    }
                    else
                    {
                        if (Session["ID_PROYECTOSUBNODO"].ToString() != "")
                        {
                            nPSN = int.Parse(Session["ID_PROYECTOSUBNODO"].ToString());
                            this.hdnT305IdProy.Value = nPSN.ToString();
                            this.hdnAcceso.Value = PROYECTOSUBNODO.getAcceso(null, nPSN, (int)Session["UsuarioActual"]);
                            SqlDataReader dr = PROYECTO.fgGetDatosProy(nPSN);
                            if (dr.Read())
                            {
                                sAccesoBitacoraPE = dr["t305_accesobitacora_pst"].ToString();
                                if (sAccesoBitacoraPE != "X")
                                    {
                                        this.txtEstado.Text = dr["t301_estado"].ToString();
                                        this.txtNomProy.Text = dr["t301_denominacion"].ToString();
                                        this.txtCodProy.Text = int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###");
                                        if (this.hdnAcceso.Value == "R")
                                            sAccesoBitacoraPE = "L";
                                        else
                                        {
                                            if (this.txtEstado.Text == "C" || this.txtEstado.Text == "H")
                                                sAccesoBitacoraPE = "L";
                                        }
                                    }
                            }
                            dr.Close();
                            dr.Dispose();
                        }
                    }
                    if (nPSN > -1 && sAccesoBitacoraPE != "X")
                    {
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
                        ObtenerDatosAsunto(nPSN);
                        ObtenerPTs(nPSN.ToString());
                        LimpiarDatosAccion();
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
                //sResultado += obtenerAcciones(aArgs[1], Utilidades.unescape(aArgs[2]), aArgs[3], aArgs[4]);
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
                sb.Append(dr["t305_accesobitacora_pst"].ToString());  //5
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

    private string obtenerAsuntos(string sPE, string sOrden, string sAscDesc, byte iEstado, int iTipo)
    {
        StringBuilder sb = new StringBuilder();
        int nPSN;
        string sIdAsunto, sDesAsunto, sFecha, sIdResponsable;//
        sb.Append("<table id='tblDatos1' class='texto MA' style='width:940px; text-align:left;'>");
        sb.Append("<colgroup><col style='width:300px;' /><col style='width:190px;' /><col style='width:87px;' /><col style='width:90px;' /><col style='width:88px;' /><col style='width:90px;' /><col style='width:95px;' /></colgroup>");
        sb.Append("<tbody>");
        if (sPE != "")
        {
            nPSN = int.Parse(sPE);
            //Session["NUM_PROYECTO"] = nPE.ToString();
            //if (Session["NUM_PROYECTO"].ToString() == "")
            //{
            //    string sCad = PROYECTO.flGetNumProy(null, nPE);
            //    string[] aArgs = Regex.Split(sCad, "@");
            //    if (Session["NodoActivo"].ToString() == aArgs[1])
            //    {
            //        Session["NUM_PROYECTO"] = aArgs[0];
            //    }
            //}
            SqlDataReader dr = ASUNTO.Catalogo(nPSN, "", iEstado, null, null, null, null, null, null, null, "", null, "", null, null,
                                               null, "", iTipo, byte.Parse(sOrden), byte.Parse(sAscDesc));
            while (dr.Read())
            {
                sIdAsunto = dr["t382_idasunto"].ToString();
                sDesAsunto = HttpUtility.HtmlEncode(dr["t382_desasunto"].ToString());
                sIdResponsable = dr["t382_responsable"].ToString();
                sb.Append("<tr id='" + sIdAsunto + "' idR='" + sIdResponsable + "' ");
                sb.Append("ondblclick='mDetAsunto(this.id)' ");
                sb.Append("onclick='ms(this);obtenerAcciones(" + sIdAsunto + ")' ");
                sb.Append(" style='height:16px;' onmouseover='TTip(event)'>");
                sb.Append("<td><nobr class='NBR W300' style='padding-left:3px;'>" + sDesAsunto + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W180'>" + dr["t384_destipo"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t382_severidad"].ToString() + "</td>");
                sb.Append("<td>" + dr["t382_prioridad"].ToString() + "</td>");
                sFecha = dr["t382_flimite"].ToString();
                if (sFecha != "")
                    sFecha = DateTime.Parse(dr["t382_flimite"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sFecha = dr["t382_fnotificacion"].ToString();
                if (sFecha != "")
                    sFecha = DateTime.Parse(dr["t382_fnotificacion"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sb.Append("<td>" + dr["t382_estado"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
        }
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtmlAsunto = sb.ToString();
        //Ademas del html de los asuntos devuelvo el html de los PTs del PE
        return "OK@#@" + strTablaHtmlAsunto + ObtenerPTs(sPE);
    }
    private void ObtenerDatosAsunto(int nPSN)
    {
        StringBuilder sb = new StringBuilder();
        string sIdAsunto, sDesAsunto, sFecha;

        sb.Append("<table id='tblDatos1' class='texto MA' style='width:940px; text-align:left;'>");
        sb.Append("<colgroup><col style='width:300px;' /><col style='width:190px;' /><col style='width:87px;' /><col style='width:90px;' /><col style='width:88px;' /><col style='width:90px;' /><col style='width:95px;' /></colgroup>");
        sb.Append("<tbody>");
        SqlDataReader dr = ASUNTO.Catalogo(nPSN, "", null,null,null,null,null,null,null,null, "", null, "", null,null,null, "", null, 8, 0);
        while (dr.Read())
        {
            sIdAsunto = dr["t382_idasunto"].ToString();
            sDesAsunto = HttpUtility.HtmlEncode(dr["t382_desasunto"].ToString());
            sb.Append("<tr id='" + sIdAsunto + "' ondblclick=\"mDetAsunto(this.id)\" onclick=\"ms(this);obtenerAcciones(" + sIdAsunto + ")\" style='height:16px;' onmouseover='TTip(event)'>");
            sb.Append("<td><nobr class='NBR W300' style='padding-left:3px;'>" + sDesAsunto + "</nobr></td>");
            sb.Append("<td><nobr class='NBR W180'>" + dr["t384_destipo"].ToString() + "</nobr></td>");
            sb.Append("<td>" + dr["t382_severidad"].ToString() + "</td>");
            sb.Append("<td>" + dr["t382_prioridad"].ToString() + "</td>");
            sFecha = dr["t382_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t382_flimite"].ToString()).ToShortDateString();
            sb.Append("<td>" + sFecha + "</td>");
            sFecha = dr["t382_fnotificacion"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t382_fnotificacion"].ToString()).ToShortDateString();
            sb.Append("<td>" + sFecha + "</td>");
            sb.Append("<td>" + dr["t382_estado"].ToString() + "</td>");

            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtmlAsunto = sb.ToString();
    }
    private void LimpiarDatosAccion()
    {
        StringBuilder sB = new StringBuilder();

        sB.Append("<table id='tblDatos2' class='texto' style='width: 650px;'>");
        sB.Append("<colgroup><col style='width:300px;' /><col style='width:75px' /><col style='width:50px' align=right /><col style='width:25px' /><col style='width:75px' /></colgroup>");

        sB.Append("</table>");
        strTablaHtmlAccion = sB.ToString();
    }
    private string obtenerAcciones(string sIdAsunto, string sOrden, string sAscDesc)
    {
        StringBuilder sB = new StringBuilder();
        int i = 0, nIdAsunto;
        string sIdAccion, sI, sFecha, sIdResponsable;

        if (sIdAsunto == "") return "error@#@Se ha intentado recoger las acciones de un asunto sin código";
        nIdAsunto = int.Parse(sIdAsunto);
        sB.Append("<table id='tblDatos2' class='texto MA' style='width:600px; text-align:left;'>");
        sB.Append("<colgroup><col style='width:380px;' /><col style='width:65px;' /><col style='width:70px;' /><col style='width:85px;' /></colgroup>");
        sB.Append("<tbody>");
        SqlDataReader dr = ACCION.Catalogo(null, nIdAsunto, null, "", null, null, null, byte.Parse(sOrden), byte.Parse(sAscDesc));
        while (dr.Read())
        {
            sIdAccion = dr["t383_idaccion"].ToString();
            sI = i.ToString();
            sIdResponsable = dr["t382_responsable"].ToString();
            sB.Append("<tr id='" + sIdAccion + "' style='height:16px;' onclick='ms(this);' ");
            sB.Append("ondblclick=\"mDetAccion(this.id," + sIdAsunto + "," + sIdResponsable + ")\" onmouseover='TTip(event)'>");
            sB.Append("<td style='padding-left:3px;'><nobr class='NBR W370'>" + HttpUtility.HtmlEncode(dr["t383_desaccion"].ToString()) + "</nobr></td>");
            
            sFecha = dr["t383_flimite"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t383_flimite"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sB.Append("<td style='text-align:right; padding-right:10px;'>" + dr["t383_avance"].ToString() + "</td>");
            sFecha = dr["t383_ffin"].ToString();
            if (sFecha != "")
                sFecha = DateTime.Parse(dr["t383_ffin"].ToString()).ToShortDateString();
            sB.Append("<td>" + sFecha + "</td>");
            sB.Append("</tr>");
            i++;
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
                ASUNTO.Delete(tr, int.Parse(sIdAsunto));
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
                ACCION.Delete(tr, int.Parse(sIdAccion));
            sResul = "OK@#@" + sIdAccion;
        }
        catch (Exception e)
        {
            sResul = "error@#@" + e.Message;
        }
        return sResul;
    }
    private string ObtenerPTs(string sPSN)
    {
        string sResul = "", sDesPT;
        int nPSN;
        SqlDataReader dr;
        try
        {
            StringBuilder sb = new StringBuilder();
            int iUser = (int)Session["UsuarioActual"];
            sb.Append("<table id='tblPTs' class='texto MA' style='width:305px; text-align:left;'>");
            sb.Append("<tbody>");
            if (sPSN != "")
            {
                nPSN=int.Parse(sPSN);                
                dr = ProyTec.CatalogoBitacora(nPSN, iUser, "", "C");
                while (dr.Read())
                {
                    sDesPT = HttpUtility.HtmlEncode(dr["nom_pt"].ToString());
                    sb.Append("<tr id='" + dr["cod_pt"].ToString() + "' aIAP='" + dr["t331_acceso_iap"].ToString() + "'");
                    sb.Append(" style='height:16px;' onclick='ms(this)' ondblclick='bitacoraPT(this.rowIndex)'>");
                    sb.Append("<td><nobr class='NBR W300' style='padding-left:3px;'>" + HttpUtility.HtmlEncode(dr["nom_pt"].ToString()) + "</nobr>");
                    sb.Append("</td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtmlPTs = sb.ToString();
            sResul = "@#@" + strTablaHtmlPTs;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos", ex);
        }
        return sResul;
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

}
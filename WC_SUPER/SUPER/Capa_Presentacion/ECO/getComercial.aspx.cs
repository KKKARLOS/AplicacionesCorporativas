using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_ECO_getComercial : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["IDRED"] == null)
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
                return;
            }
        }
        catch (System.Threading.ThreadAbortException) { return; }

        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("profesionales"):
                sResultado += obtenerUsuarios(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string obtenerUsuarios(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool bMostrarBajas = false;
        if (sMostrarBajas == "1") bMostrarBajas = true;
        sb.Append("<table id='tblDatos' class='texto MA' style='width:490px;'><colgroup><col style='width:20px;' /><col style='width:470px;' /></colgroup>");
        try
        {
            SqlDataReader dr = USUARIO.ObtenerComerciales(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2),
                                                             Utilidades.unescape(sNombre), bMostrarBajas);

            while (dr.Read())
            {
                sb.Append("<tr style='noWrap:true; height:18px' tipo='" + dr["tipo"].ToString() + "' sexo='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' idficepi='" + dr["t001_idficepi"].ToString() + "'");
                sb.Append(" baja='" + dr["baja"].ToString() + "'");
                sb.Append(" onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[Profesional:&nbsp;");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td></td><td><nobr class='NBR W460'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + ex.ToString();
        }
        return sResul;
    }
}
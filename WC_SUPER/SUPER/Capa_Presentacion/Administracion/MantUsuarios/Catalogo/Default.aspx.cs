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
    public string sErrores="";
    public SqlConnection oConn;
    public SqlTransaction tr;
    //public StringBuilder sb = new StringBuilder();
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            sErrores = "";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.TituloPagina = "Mantenimiento de usuarios";

            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}

            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("profesionales"):
                sResultado += obtenerProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string obtenerProfesionales(string sAp1, string sAp2, string sNombre, string sUsuario, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        int? nUsuario = null;

        try
        {
            if (sUsuario != "0") nUsuario = int.Parse(sUsuario);

            DataSet ds = USUARIO.ObtenerProfFICEPISUPER(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), 
                                                        nUsuario, (sMostrarBajas=="1")? true:false);

            sb.Append("<table id='tblDatosFicepi' class='texto MM' style='WIDTH: 430px; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:410px;' /></colgroup>");
            sb.Append("<tbody>");
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("<tr id='" + oFila["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + oFila["tipo"].ToString() + "' ");
                sb.Append("sexo='" + oFila["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + oFila["baja"].ToString() + "' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W400' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["profesional"].ToString() + "<br><label style='width:70px;'>Empresa:</label>" + oFila["EMPRESA"].ToString() + "] hideselects=[off]\">" + oFila["Profesional"].ToString() + "<nobr></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W400' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["profesional"].ToString() + "] hideselects=[off]\">" + oFila["Profesional"].ToString() + "<nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("@#@");

            sb.Append("<table id='tblDatosSuperAlta' class='texto MA' style='WIDTH: 430px; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:305px;' /><col style='width:40px;' /><col style='width:65px;' /></colgroup>");
            sb.Append("<tbody>");
            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                sb.Append("<tr id='" + oFila["t314_idusuario"].ToString() + "' ");
                sb.Append("tipo='" + oFila["tipo"].ToString() + "' ");
                sb.Append("sexo='" + oFila["t001_sexo"].ToString() + "' ");
                sb.Append("idficepi=" + oFila["t001_idficepi"].ToString() + " ");
                //sb.Append("ondblclick='msse(this);MostrarUsuario(this);' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                if (oFila["tipo"].ToString() == "P")
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W300' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["profesional"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "] hideselects=[off]\">" + oFila["Profesional"].ToString() + "<nobr></td>");
                else
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W300' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["profesional"].ToString() + "<br><label style='width:70px;'>Empresa:</label>" + oFila["EMPRESA"].ToString() + "] hideselects=[off]\">" + oFila["Profesional"].ToString() + "<nobr></td>");
                sb.Append("<td style='text-align: right;'>" + int.Parse(oFila["t314_idusuario"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td style='padding-left:5px;'>" + ((DateTime)oFila["t314_falta"]).ToShortDateString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("@#@");

            sb.Append("<table id='tblDatosSuperBaja' class='texto MA' style='width: 430px; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:305px;' /><col style='width:40px;' /><col style='width:65px;' /></colgroup>");
            sb.Append("<tbody>");
            foreach (DataRow oFila in ds.Tables[2].Rows)
            {
                sb.Append("<tr id='" + oFila["t314_idusuario"].ToString() + "' ");
                sb.Append("tipo='" + oFila["tipo"].ToString() + "' ");
                sb.Append("sexo='" + oFila["t001_sexo"].ToString() + "' ");
                sb.Append("idficepi=" + oFila["t001_idficepi"].ToString() + " ");
                //sb.Append("ondblclick='msse(this);MostrarUsuario(this);' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                if (oFila["tipo"].ToString() == "P")
                    sb.Append("<td  style='padding-left:3px;'><nobr class='NBR W300' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["profesional"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "] hideselects=[off]\">" + oFila["Profesional"].ToString() + "<nobr></td>");
                else
                    sb.Append("<td  style='padding-left:3px;'><nobr class='NBR W300' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["profesional"].ToString() + "<br><label style='width:70px;'>Empresa:</label>" + oFila["EMPRESA"].ToString() + "] hideselects=[off]\">" + oFila["Profesional"].ToString() + "<nobr></td>");
                sb.Append("<td style='text-align: right;'>" + int.Parse(oFila["t314_idusuario"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td style='padding-left:5px;'>" + ((DateTime)oFila["t314_fbaja"]).ToShortDateString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            ds.Dispose();

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + ex.ToString();
        }
        return sResul;
    }

}


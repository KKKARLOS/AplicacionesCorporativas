using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
//para manejar listas
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Reconexion : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {

            Master.TituloPagina = "Reconexión";
            //Master.nBotonera = 1;
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("CargarProf"):
                sResultado += ObtenerDatos(Utilidades.unescape(aArgs[1].ToString()), Utilidades.unescape(aArgs[2].ToString()), Utilidades.unescape(aArgs[3].ToString()));
                break;
            //case ("SelecProf"):
            //    sResultado += SeleccionarProfesional(aArgs[1].ToString());
            //    break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string ObtenerDatos(string strApellido1, string strApellido2, string strNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = Recurso.ObtenerProfesionalesReconexion(strApellido1, strApellido2, strNombre);

            sb.Append("<table id='tblDatos' class='MA' style='width:400px;'>");
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:380px;'/></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T001_CODRED"].ToString() + "' idUsuario='" + dr["num_empleado"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("idf='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("ondblclick='SeleccionProfesional(this);' onClick='ms(this);' style='height:20px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t001_idficepi"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:left;'><div class='NBR W375'>" + dr["TECNICO"].ToString() + "</div></td>" + (char)13);
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los profesionales", ex);
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            if (!Page.IsCallback)
            {
                if (Request.QueryString["caso"] != null) hdnCaso.Value = Request.QueryString["caso"].ToString();
                if (Request.QueryString["criterio"] != null) hdnCriterio.Value = Request.QueryString["criterio"].ToString();
                if (Request.QueryString["valor"] != null) hdnValor.Value = Request.QueryString["valor"].ToString();

                string strTabla0 = "";

                if (hdnCaso.Value == "1")
                    strTabla0 = getDatosResumen(false);
                else if (hdnCaso.Value == "2")
                    strTabla0 = getDatosCriterio(int.Parse(hdnCriterio.Value), false);
                else if (hdnCaso.Value == "3")
                    strTabla0 = getDatosValor(int.Parse(hdnCriterio.Value), int.Parse(hdnValor.Value), true);

                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strTablaHTML = aTabla0[1];
                else sErrores = aTabla0[1];


                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("getDatos"):
                if (hdnCaso.Value == "1")
                    sResultado += getDatosResumen((aArgs[1] == "1") ? true : false);
                else if (hdnCaso.Value == "2")
                    sResultado += getDatosCriterio(int.Parse(hdnCriterio.Value),(aArgs[1] == "1") ? true : false);
                else if (hdnCaso.Value == "3")
                    sResultado += getDatosValor(int.Parse(hdnCriterio.Value), int.Parse(hdnValor.Value), (aArgs[1] == "1") ? true : false);
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
    protected string getDatosResumen(bool bValorAsignado)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        int indice = 0;
        try
        {
            dr = NODO.CEEC_RESUMEN(bValorAsignado);

            sb.Append("<table id='tblDatos' class='texto MANO' style='width:800px;'>");
            sb.Append("<colgroup><col style='width:280px;' /><col style='width:280px;' /><col style='width:100px;' /><col style='width:140px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                indice++;
                sb.Append("<tr id='" + indice + "' style='height:16px' onclick='ms(this)'>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W275'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W275'>" + dr["t345_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'>"+dr["Obligatorio"].ToString()+"</td>");
                sb.Append("<td><nobr class='NBR W135'>" + dr["valor"].ToString() + "</nobr></td>");
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
            sErrores = Errores.mostrarError("Error al obtener los datos resumen de los criterios económicos.", ex);
            return "Error@#@" + sErrores;
        }
    }

    protected string getDatosCriterio(int iCriterio, bool bValorAsignado)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        int indice = 0;
        try
        {
            dr = CEC.Asociados_A_Nodos(iCriterio, bValorAsignado);

            sb.Append("<table id='tblDatos' class='texto MANO' style='width:800px;'>");
            sb.Append("<colgroup><col style='width:280px;' /><col style='width:280px;' /><col style='width:100px;' /><col style='width:140px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                indice++;
                sb.Append("<tr id='" + indice + "' style='height:16px' onclick='ms(this)'>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W275'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W275'>" + dr["t345_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'>" + dr["Obligatorio"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W135'>" + dr["valor"].ToString() + "</nobr></td>");
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
            sErrores = Errores.mostrarError("Error al obtener los datos de CRs del criterio económico seleccionado.", ex);
            return "Error@#@" + sErrores;
        }
    }

    protected string getDatosValor(int iCriterio, int iValor, bool bValorAsignado)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        int indice = 0;
        try
        {
            dr = VCEC.Asociados_A_Nodos(iCriterio, iValor, bValorAsignado);

            sb.Append("<table id='tblDatos' class='texto MANO' style='width:800px;'>");
            sb.Append("<colgroup><col style='width:280px;' /><col style='width:280px;' /><col style='width:100px;' /><col style='width:140px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                indice++;
                sb.Append("<tr id='" + indice + "' style='height:16px' onclick='ms(this)'>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W275'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W275'>" + dr["t345_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'>" + dr["Obligatorio"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W135'>" + dr["valor"].ToString() + "</nobr></td>");
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
            sErrores = Errores.mostrarError("Error al obtener los datos de CRs del valor del criterio económico seleccionado.", ex);
            return "Error@#@" + sErrores;
        }
    }

}

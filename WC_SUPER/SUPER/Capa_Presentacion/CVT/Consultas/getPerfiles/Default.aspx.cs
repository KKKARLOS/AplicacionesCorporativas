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
using SUPER.Capa_Negocio;
using SUPER.BLL;
using System.Text;
using System.Text.RegularExpressions;
//Para el List
using System.Collections.Generic;
//Para usar WebMethods
using System.Web.Services;
using System.Web.Script.Services;


public partial class Capa_Presentacion_CVT_Consultas_getPerfiles_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    public string strTablaHTML = "", strTablaHTMLFam="";
    public string sErrores = "", sTitulo = "", sCriterios = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["caso"] != null)
            this.hdnCaso.Value = Request.QueryString["caso"];

        hdnIdTipo.Value = Request.QueryString["nTipo"].ToString();
        if (!Page.IsCallback)
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
                sTitulo = "Selección de criterio: ";
                switch (this.hdnIdTipo.Value)
                {
                    case "PER":
                        sTitulo += "Perfil en experiencia profesional";
                        //Por defecto sólo familias privadas
                        strTablaHTMLFam = obtenerFamiliasPerfiles(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), "S");
                        break;
                    case "PERENT":
                        sTitulo += "Perfil/Entorno en experiencia profesional";
                        //Por defecto sólo familias privadas
                        strTablaHTMLFam = obtenerFamiliasPerfiles(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), "S");
                        break;
                    case "ENT":
                        sTitulo += "Entorno en experiencia profesional";
                        //Por defecto sólo familias privadas
                        strTablaHTMLFam = obtenerFamiliasEntornos(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), "S");
                        break;
                    case "ENTPER":
                        sTitulo += "Entorno/Perfil en experiencia profesional";
                        //Por defecto sólo familias privadas
                        strTablaHTMLFam = obtenerFamiliasEntornos(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), "S");
                        break;
                }
                this.Title = sTitulo;

                strTablaHTML = "<table id='tblDatos' style='width:350px;' mantenimiento='0' class='texto MAM' cellspacing='0' cellpadding='0' border='0'><tbody id='tbodyPerf'></tbody></table>";                
                //strTablaHTML=obtenerPerfiles();

                sCriterios = "var js_cri = new Array();\n";

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
            }
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
            case "TipoConcepto":
                int tipo = -1;
                switch (aArgs[1])
                {
                    case "ENT"://Entornos tecnologicos
                    case "ENTPER":
                        tipo = 5;
                        break;
                    case "PER"://perfil en experiencia profesional
                    case "PERENT":
                        tipo = 3;
                        break;
                }
                sResultado += Curriculum.ObtenerTipoConcepto16(tipo, aArgs[2].ToString(), Utilidades.unescape(aArgs[3].ToString()));
                break;
            case ("cargarCriterio"):
                sResultado += "OK@#@" + aArgs[1] + "@#@" + aArgs[1] + "@#@" + CargarCriterio(aArgs[1]);
                break;
            case ("familias"):
                switch(aArgs[2])
                {
                    case "PER":
                    case "PERENT":
                        sResultado += "OK@#@" + obtenerFamiliasPerfiles(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), aArgs[1]);
                        break;
                    case "ENT":
                    case "ENTPER":
                        sResultado += "OK@#@" + obtenerFamiliasEntornos(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), aArgs[1]);
                        break;
                }
                
                break;
            //case ("perfilesfamilia"):
            //    sResultado += "OK@#@" + obtenerPerfilesFamilia(int.Parse(aArgs[1]));
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

    private string obtenerPerfiles()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatos' style='width:350px;' mantenimiento='0' class='texto MAM' cellspacing='0' cellpadding='0' border='0'>");
        sb.Append("<tbody id='tbodyPerf'>");

        SqlDataReader dr = SUPER.BLL.PerfilExper.getPerfiles(null, 0);
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["T035_IDCODPERFIL"].ToString() + "' style='height:16px;' tipo='P'");
            sb.Append(" onclick='mm(event)' ondblclick='insertarItem(this);' onmousedown='DD(event)'>");// 
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W340'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr></td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();

        sb.Append("</tbody>");
        sb.Append("</table>");

        return sb.ToString();
    }
    /// <summary>
    /// Obtiene las familias privadas + las públicas
    /// </summary>
    /// <param name="t001_idficepi"></param>
    /// <returns></returns>
    private string obtenerFamiliasPerfiles(int t001_idficepi, string sSoloPrivadas)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatosFam' style='width:350px;' mantenimiento='1' class='texto MAM' cellspacing='0' cellpadding='0' border='0'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:310px;' /><col style='width:20px;' /></colgroup>");
        sb.Append("<tbody id='tbodyPerf'>");

        List<SUPER.BLL.FamiliaPerfil> Lista = SUPER.BLL.FamiliaPerfil.CatalogoProfesional(t001_idficepi, sSoloPrivadas);
        foreach (SUPER.BLL.FamiliaPerfil oElem in Lista)
        {
            //sb.Append("<tr id='" + oElem.t859_idfamperfil + "' style='height:16px;' class='MAM'");
            sb.Append("<tr id='" + oElem.t859_idfamperfil + "' style='height:16px;' tipo='F' ");
            sb.Append(" onclick='mm(event)' ondblclick='insertarItem(this);' onmousedown='DD(event)'>");// 
            if (oElem.t859_publica)
                sb.Append("<td><img src='../../../../Images/imgGF.gif' title='Familia pública' alt='Familia pública' /></td>");
            else
                sb.Append("<td></td>");
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W300'>" + oElem.t859_denominacion + "</nobr></td>");
            sb.Append("<td class='MANO'><img src='../../../../Images/imgLupa.gif' onclick='verPerfiles(this.parentElement.parentElement.id)' title='Visualizar perfiles de la familia' alt='Visualizar perfiles de la familia' /></td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");

        return sb.ToString();
    }
    /// <summary>
    /// Obtiene las familias privadas + las públicas
    /// </summary>
    /// <param name="t001_idficepi"></param>
    /// <returns></returns>
    private string obtenerFamiliasEntornos(int t001_idficepi, string sSoloPrivadas)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatosFam' style='width:350px;' mantenimiento='1' class='texto MAM' cellspacing='0' cellpadding='0' border='0'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:310px;' /><col style='width:20px;' /></colgroup>");
        sb.Append("<tbody id='tbodyPerf'>");

        List<SUPER.BLL.FamiliaEntorno> Lista = SUPER.BLL.FamiliaEntorno.CatalogoProfesional(t001_idficepi, sSoloPrivadas);
        foreach (SUPER.BLL.FamiliaEntorno oElem in Lista)
        {
            //sb.Append("<tr id='" + oElem.t859_idfamperfil + "' style='height:16px;' class='MAM'");
            sb.Append("<tr id='" + oElem.t861_idfament + "' style='height:16px;' tipo='F' ");
            sb.Append(" onclick='mm(event)' ondblclick='insertarItem(this);' onmousedown='DD(event)'>");// 
            if (oElem.t861_publica)
                sb.Append("<td><img src='../../../../Images/imgGF.gif' title='Familia pública' alt='Familia pública' /></td>");
            else
                sb.Append("<td></td>");
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W300'>" + oElem.t861_denominacion + "</nobr></td>");
            //sb.Append("<td class='MANO'><img src='../../../../Images/imgLupa.gif' onclick='alert('hola'); verEntornos(this.parentElement.parentElement.id)' title='Visualizar entornos de la familia' alt='Visualizar entornos de la familia' /></td>");
            sb.Append("<td class='MANO'><img src='../../../../Images/imgLupa.gif' onclick='mostrarEntornos(this.parentElement.parentElement.id)' title='Visualizar entornos de la familia' alt='Visualizar entornos de la familia' /></td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");

        return sb.ToString();
    }

    //private string obtenerPerfilesFamilia(int idFamilia)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<table id='tblDatosFam' style='width:400px; text-align:left;' mantenimiento='0' class='texto' cellspacing='0' cellpadding='0' border='0'>");

    //    List<ElementoLista> Lista = SUPER.BLL.FamiliaPerfil.CatalogoPerfil(idFamilia);
    //    foreach (ElementoLista oElem in Lista)
    //    {
    //        sb.Append("<tr id='" + oElem.sValor + "' style='height:16px;' >"); 
    //        sb.Append("<td style='padding-left:3px;'><nobr class='NBR W390'>" + oElem.sDenominacion + "</nobr></td>");
    //        sb.Append("</tr>");
    //    }
    //    sb.Append("</table>");

    //    return sb.ToString();
    //}

    public string CargarCriterio(string sTipo)
    {
        StringBuilder sb = new StringBuilder();
        List<ElementoLista> oLista = null;
        switch (sTipo)
        {
            case "3"://perfiles de mercado
            case "16":
            case "161":
                oLista = Curriculum.obtenerPerfil(true, true);
                break;
        }
        if (oLista.Count > Constantes.nNumElementosMaxCriterios)
            sb.Append("S@#@");
        else
            sb.Append("N@#@");
        foreach (ElementoLista oElem in oLista)
        {
            sb.Append(oElem.sValor);
            sb.Append("##");
            sb.Append(oElem.sDenominacion);
            sb.Append("///");
        }
        return sb.ToString();
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string PerfilesFamilia(int idFamilia)
    {
        //return SUPER.BLL.Curriculum.CatalogoPendiente(t001_idficepi);
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatosFam' style='width:400px; text-align:left;' mantenimiento='0' class='texto' cellspacing='0' cellpadding='0' border='0'>");

        List<ElementoLista> Lista = SUPER.BLL.FamiliaPerfil.CatalogoPerfil(idFamilia);
        foreach (ElementoLista oElem in Lista)
        {
            sb.Append("<tr id='" + oElem.sValor + "' style='height:16px;' >");
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W390'>" + oElem.sDenominacion + "</nobr></td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        return sb.ToString();

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string EntornosFamilia(int idFamilia)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatosFam' style='width:400px; text-align:left;' mantenimiento='0' class='texto' cellspacing='0' cellpadding='0' border='0'>");

        List<ElementoLista> Lista = SUPER.BLL.FamiliaEntorno.CatalogoEntorno(idFamilia);
        foreach (ElementoLista oElem in Lista)
        {
            sb.Append("<tr id='" + oElem.sValor + "' style='height:16px;' >");
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W390'>" + oElem.sDenominacion + "</nobr></td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        return sb.ToString();

    }

}

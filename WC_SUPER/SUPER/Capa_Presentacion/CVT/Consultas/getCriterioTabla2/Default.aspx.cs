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
using SUPER.BLL;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_ECO_Consultas_getCriterioTabla2_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    public string sErrores = "", sTitulo = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sDenPadre = "", sTexto1="";
        if (Request.QueryString["caso"] != null)
            hdnCaso.Value = Request.QueryString["caso"];
        if (Request.QueryString["den"] != null)
            sDenPadre = Request.QueryString["den"];

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

                strTablaHTML = "<TABLE id='tblDatos' style='WIDTH: 350px;' class='texto MAM' cellSpacing='0' cellspacing='0' border='0'><colgroup><col style='width:350px;' /></colgroup></TABLE>";

                sTitulo = "Selección de criterio: ";
                switch (int.Parse(hdnIdTipo.Value))
                {
                    case 3: 
                    case 12:
                        sTitulo += "Perfil";
                        if (sDenPadre != "")
                            sTexto1 = "Selecciona el/los perfiles que quieres relacionar al entorno<br />" + sDenPadre; 
                        break;
                    case 5:
                    case 17:
                    case 171:
                    case 11:
                        sTitulo += "Entorno Tecnológico";
                        if (sDenPadre != "")
                            sTexto1 = "Selecciona el/los entornos tecnológicos que quieres relacionar al perfil<br />" + sDenPadre;
                        break;
                    case 161:
                    case 16: sTitulo += "Perfil en experiencia profesional"; break;
                }

                this.Title = sTitulo;
                if (sTexto1 != "")
                    this.divTexto1.InnerHtml = sTexto1;

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
                int tipo = int.Parse(aArgs[1]);
                switch (tipo)
                {
                    case 51://Entornos tecnologicos
                    case 17:
                    case 171:
                        tipo = 5;
                        break;
                    case 16://perfil en experiencia profesional
                    case 161:
                        tipo = 3;
                        break;
                }
                sResultado += Curriculum.ObtenerTipoConcepto(tipo, aArgs[2].ToString(), Utilidades.unescape(aArgs[3].ToString()));
                break;
            case ("cargarCriterio"):
                sResultado += "OK@#@" + aArgs[1] + "@#@" + CargarCriterio(aArgs[1]);
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
    public string CargarCriterio(string sTipo)
    {
        StringBuilder sb = new StringBuilder();
        List<SUPER.BLL.ElementoLista> oLista = null;
        switch (sTipo)
        {
            case "3"://perfiles de mercado
            case "16":
            case "161":
                oLista = Curriculum.obtenerPerfil(true, true);
                break;
            case "7"://Idiomas
            case "71":
                oLista = Curriculum.obtenerIdioma(true, true);
                break;
            case "15"://Entidades certificadoras
            case "151":
                oLista = Curriculum.obtenerEntidadesCertificadoras();
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
}

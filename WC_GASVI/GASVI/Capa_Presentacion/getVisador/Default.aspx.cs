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
using GASVI.BLL;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public string strTablaHTML = "", sProximaSituacion="";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["GVT_IDRED"] == null)
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
                    int nReferencia = int.Parse(Utilidades.decodpar(Request.QueryString["ref"].ToString()));
                    string sEstado = Utilidades.decodpar(Request.QueryString["es"].ToString());
                    if (sEstado == "T" || sEstado == "N")
                        sProximaSituacion = "aprobada";
                    else
                        sProximaSituacion = "aceptada";

                    string[] aDatos = Regex.Split(CABECERAGV.ObtenerVisadores(nReferencia, sEstado), "@#@");

                    this.lblPersonas.InnerText = aDatos[0];
                    this.strTablaHTML = aDatos[1];
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al cargar los datos", ex);
                }

                //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
                //   y la funci�n que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2� Se "registra" la funci�n que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos.", ex);
        }

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getBeneficiario"):
                try
                {
                    sResultado += "OK@#@" + USUARIO.ObtenerBeneficiarios((int)Session["GVT_IDFICEPI"], User.IsInRole("T"), User.IsInRole("S"), aArgs[3], aArgs[1], aArgs[2], "0", User.IsInRole("A"));
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los beneficiarios.", ex);
                }
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }

    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }    

}

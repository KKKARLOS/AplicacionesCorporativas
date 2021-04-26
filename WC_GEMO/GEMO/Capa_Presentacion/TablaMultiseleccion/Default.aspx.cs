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
using GEMO.BLL;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    public string sErrores = "", sTitulo = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["nT"] != null) hdnIdTipo.Value = Request.QueryString["nT"].ToString();

        if (!Page.IsCallback)
        {
            try
            {
                strTablaHTML = "<table id='tblDatos' style='width: 350px;' class='texto MAM'><colgroup><col style='width:350px;' /></colgroup></table>";
                if (int.Parse(hdnIdTipo.Value) == 2 || int.Parse(hdnIdTipo.Value) == 3)
                {
                    ambAp.Style.Add("display", "");
                    ambBajas.Style.Add("display", "");
                    ambIconosResp.Style.Add("display", "");
                    ambDenominacion.Style.Add("display", "none");
                    rdbTipo.Style.Add("display", "none");
                }
                else
                {
                    ambAp.Style.Add("display", "none");
                    ambBajas.Style.Add("display", "none");
                    ambIconosResp.Style.Add("display", "none");
                    ambDenominacion.Style.Add("display", "");
                    rdbTipo.Style.Add("display", "");
                }
                sTitulo = "Selección de criterio: ";
                switch (int.Parse(hdnIdTipo.Value))
                {
                    case 1: sTitulo += "Empresa"; break;
                    case 2: sTitulo += "Responsable"; break;
                    case 3: sTitulo += "Beneficiario"; break;
                    case 4: sTitulo += "Departamento"; break;
                    case 5: sTitulo += "Estado"; break;
                    case 6: sTitulo += "Medio"; break;
                }

                this.Title = sTitulo;

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
        sResultado = aArgs[0] + @"@#@";

        //2º Aquí realizaríamos el acceso a BD, etc,...
        try
        {
            switch (aArgs[0])
            {
                case "TipoConcepto":
                    sResultado += "OK@#@" + ObtenerTipoConcepto(aArgs[1], Utilidades.unescape(aArgs[2]));
                    break;
                case ("responsables"):
                    sResultado += "OK@#@" + GEMO.BLL.PROFESIONALES.Responsables(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), (aArgs[4] == "1") ? true : false);
                    break;
                case ("beneficiarios"):
                    sResultado += "OK@#@" + GEMO.BLL.PROFESIONALES.Beneficiarios(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), (aArgs[4] == "1") ? true : false);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("TipoConcepto"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener el concepto: " + aArgs[1], ex);
                    break;
                case ("responsables"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los responsables. ", ex);
                    break;
                case ("beneficiarios"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los beneficiarios. ", ex);
                    break;
            }
        }


        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }

    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string ObtenerTipoConcepto(string sTipoBusqueda, string sCadena)
    {
        string sResul = "";
        try
        {
            switch (int.Parse(hdnIdTipo.Value))
            {
                case 1: 	// EMPRESA
                    sResul += GEMO.BLL.EMPRESA.Obtener(sTipoBusqueda, sCadena, null);
                    break;
                case 2:		//
                    break;
                case 3:		// 
                    break;
                case 4: 	// DEPARTAMENTO
                    sResul += GEMO.BLL.DEPARTAMENTO.Obtener(sTipoBusqueda, sCadena, null);
                    break;
                case 5: 	// ESTADO
                    sResul += GEMO.BLL.ESTADO.Obtener(sTipoBusqueda, sCadena, null);
                    break;
                case 6: 	// MEDIO
                    sResul += GEMO.BLL.MEDIO.Obtener(sTipoBusqueda, sCadena, null);
                    break;

            }
        }
        catch (System.Exception objError)
        {
            sResul =  Errores.mostrarError("Error al leer : " + sTitulo, objError);
            throw (new Exception(sResul));
        }
        return sResul;
    }
}

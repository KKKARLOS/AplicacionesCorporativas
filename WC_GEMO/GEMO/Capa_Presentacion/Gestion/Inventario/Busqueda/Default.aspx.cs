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
using GEMO.BLL;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos' class='mano' style='width:934px;'></table>";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.sbotonesOpcionOn = "2,7";

                string sLectura = "true";
                //string[] aFiguras = Regex.Split(Session["FIGURAS"].ToString(), ",");
                //for (int i = 0; i < aFiguras.Length; i++)
                //{
                //    if (aFiguras[i] == "C")
                //    {
                //        sLectura = "false";
                //        break;
                //    }
                //}

                if (User.IsInRole("C")) sLectura = "false";
                if (sLectura == "true") Master.sbotonesOpcionOff = "2,7";
                else Master.sbotonesOpcionOff = "";

                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Catálogo de líneas";
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");

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
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@";
        try
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "OK@#@" + GEMO.BLL.LINEA.Busqueda ((aArgs[1]=="") ? null:(int?)int.Parse(aArgs[1]), (aArgs[2]=="") ? null:(int?)int.Parse(aArgs[2]), Utilidades.unescape(aArgs[3]), Utilidades.unescape(aArgs[4]),
                                                                    (aArgs[5] == "") ? null : (short?)short.Parse(aArgs[5]), aArgs[6], Utilidades.unescape(aArgs[7]), Utilidades.unescape(aArgs[8]), Utilidades.unescape(aArgs[9]), Utilidades.unescape(aArgs[10]), aArgs[11]
                                                                    );

                    break;
                case "eliminar":
                    GEMO.BLL.LINEA.Eliminar(aArgs[1]);
                    sResultado += "OK@#@";
                    break;	

                case ("generarExcel"):
                    sResultado += "OK@#@" + GEMO.BLL.LINEA.generarExcel((aArgs[1] == "") ? null : (int?)int.Parse(aArgs[1]), (aArgs[2] == "") ? null : (int?)int.Parse(aArgs[2]), Utilidades.unescape(aArgs[3]), Utilidades.unescape(aArgs[4]),
                                                                    (aArgs[5] == "") ? null : (short?)short.Parse(aArgs[5]), aArgs[6], Utilidades.unescape(aArgs[7]), Utilidades.unescape(aArgs[8]), Utilidades.unescape(aArgs[9]), Utilidades.unescape(aArgs[10]), aArgs[11]
                                                                    );
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener las líneas", ex);
                    break;
                case ("eliminar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al eliminar las líneas", ex);
                    break;            
                case ("generarExcel"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al generar el fichero excel", ex);
                    break;
            }
        }
        _callbackResultado = sResultado;
    }
 
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

}

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
using EO.Web;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos' class='mano' style='width:1015px;'></table>";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsCallback)
            {
                Master.nBotonera = 14;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Cat�logo de proyectos con CEEC";
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

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
                     sResultado += "OK@#@" + SUPER.Capa_Negocio.CEC.Busqueda (Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]),
                                                                    Utilidades.unescape(aArgs[3])
                                                                    );
                    break;

            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener las l�neas", ex);
                    break;
            }
        }
        _callbackResultado = sResultado;
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

}

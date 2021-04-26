using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
using SUPER.BLL;

public partial class Capa_Presentacion_CVT_Mantenimientos_Titulaciones_Default : System.Web.UI.Page,ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = String.Empty, strHTMLComboTipo = "", strHTMLComboModalidad = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Master.nBotonera = 9;
        Master.TituloPagina = "Mantenimiento de Titulaciones";
        Master.bFuncionesLocales = true;
        Master.Modulo = "CVT";
        if (!Page.IsCallback)
        {
            try
            {
                this.strTablaHTML = Titulacion.Catalogo("",byte.Parse("0"), "C", false);
                strHTMLComboModalidad = Utilidades.escape(Titulacion.modalidadHTML());
                strHTMLComboTipo = Utilidades.escape(Titulacion.tipoHTML());
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    
    }


    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";

        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += Titulacion.Grabar(aArgs[1]);
                    break;
                case ("buscar"):
                    sResultado += "OK@#@" + Titulacion.Catalogo(aArgs[1], (aArgs[2] == "") ? null : (byte?)byte.Parse(aArgs[2]), aArgs[3], false);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    if (ex.Message.IndexOf("ErrorControlado") > -1)
                    {
                        sResultado += "Error@#@" + ex.Message;
                    }
                    else
                        sResultado += "Error@#@" + Errores.mostrarError("Error al guardar", ex);
                    break;
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al buscar", ex);
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
}

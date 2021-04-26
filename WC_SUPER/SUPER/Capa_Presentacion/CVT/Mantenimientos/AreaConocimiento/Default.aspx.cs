using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
using SUPER.BLL;


public partial class Capa_Presentacion_CVT_ExpProfesional_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Master.nBotonera = 9;
                    Master.bFuncionesLocales = true;
                    Master.Modulo = "CVT";
                    hdnIdTipo.Value = Request.QueryString["OPCION"].ToString();
                    switch (int.Parse(hdnIdTipo.Value))
                    {
                        case 1:
                            Master.TituloPagina = "Mantenimiento de Área de Conocimiento Sectorial";
                            break;
                        case 2:
                            Master.TituloPagina = "Mantenimiento de Área de Conocimiento Tecnológico";
                            break;
                        default:
                            Master.TituloPagina = "Mantenimiento de Áreas de Conocimiento";
                            break;
                    }                    
                    string[] aTabla = Regex.Split(AreaConocimientoCTV.CatalogoAreaConocimientoSec(int.Parse(hdnIdTipo.Value), true), "@#@");
                    if (aTabla[0] == "OK") 
                    {
                        this.strTablaHTML = aTabla[1];                        
                    }
                    else Master.sErrores += Errores.mostrarError(aTabla[1]);

                }
                catch (Exception ex)
                {
                    Master.sErrores = SUPER.Capa_Negocio.Errores.mostrarError("Error al cargar la página", ex);
                }
            }

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
        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "OK@#@" + AreaConocimientoCTV.Grabar(aArgs[1]);
                    break;
                case ("mostrar"):
                    bool? bActiva = true;
                    if (aArgs[2] == "0") bActiva = null;
                    sResultado += AreaConocimientoCTV.CatalogoAreaConocimientoSec(int.Parse(aArgs[1]), bActiva);
                    break;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.IndexOf("ErrorControlado") > -1)
            {
                sResultado += "Error@#@" + ex.Message;
            }
            else
                sResultado += "Error@#@" + Errores.mostrarError("Error al guardar", ex);
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

    
    


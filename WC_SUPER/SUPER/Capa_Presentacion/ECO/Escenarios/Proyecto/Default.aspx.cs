using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EO.Web;
using SUPER.BLL;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_ECO_Escenarios_Proyecto_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nEscenario = -1;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 57;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;

                //Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/Escenarios/Proyecto/Functions/event_util.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/Escenarios/Proyecto/Functions/contextmenu.js");
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                if (Request.QueryString["ie"] != null)
                    nEscenario = int.Parse(Utilidades.decodpar(Request.QueryString["ie"].ToString()));

                nEscenario = 3;
                Session["ID_PROYECTOSUBNODO"] = 9337;
                hdnIdEscenario.Text = nEscenario.ToString();


                //divDatos.InnerHtml = ESCENARIOSCAB.ObtenerCatalogo(3, "EUR");
                //string[] aDatos = Regex.Split(ESCENARIOSCAB.ObtenerPlanningEscenario(3, "EUR"), "@#@");
                //if (aDatos[0] == "OK")        {
                //    string[] aTablas = Regex.Split(aDatos[1], "{{septabla}}");
                //    divTituloMovil.InnerHtml = aTablas[0];
                //    divBodyFijo.InnerHtml = aTablas[1];
                //    divBodyMovil.InnerHtml = aTablas[2];
                //}else{
                //    Master.sErrores = Errores.mostrarError(aDatos[1]);
                //}
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
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getCabeceraEscenario"):
                sResultado += getCabeceraEscenario(int.Parse(aArgs[1]));
                break;
            case ("getDatosEscenario"):
                sResultado += getDatosEscenario(int.Parse(aArgs[1]));
                break;
            case ("grabar"):
                sResultado += GrabarEscenario(int.Parse(aArgs[1]), aArgs[2], aArgs[3], aArgs[4], aArgs[5]);//, aArgs[6], aArgs[7]
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


    private string getCabeceraEscenario(int nIDEscenario)
    {
        try
        {
            ESCENARIOSCAB oESCAB = ESCENARIOSCAB.Obtener(null, nIDEscenario);

            return "OK@#@" + Utilidades.escape(oESCAB.t789_denominacion) + "@#@"  //2
                    + Utilidades.escape(oESCAB.Creador) + "@#@"         //3
                    + ((oESCAB.t301_idproyecto.HasValue) ? ((int)oESCAB.t301_idproyecto).ToString("#,###") : "") + "@#@" //4
                    + Utilidades.escape(oESCAB.t301_denominacion) + "@#@"   //5
                    + Utilidades.escape(oESCAB.ResponsableProyecto) + "@#@" //6
                    + oESCAB.t789_modelocoste + "@#@"
                    + oESCAB.t789_modelotarif;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos básicos del escenario", ex);
        }

    }
    private string getDatosEscenario(int nIDEscenario)
    {
        return ESCENARIOSCAB.ObtenerPlanningEscenario(nIDEscenario, "EUR");
    }

    private string GrabarEscenario(int nIDEscenario, string sDatosMeses, string sDatosFijos, string sDatosMovil, string sIDsMesesBorrados)//, string strAnomes, string strPartidas, string strMotivos
    {
        return ESCENARIOSCAB.Grabar(nIDEscenario, sDatosMeses, sDatosFijos, sDatosMovil, sIDsMesesBorrados);//, strAnomes, strPartidas, strMotivos
    }

}

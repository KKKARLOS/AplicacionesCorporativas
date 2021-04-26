using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_ECO_DialogoAlertas_CatalogoPendientes_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strHTMLTablaUsuario = "", strHTMLTablaGestor = "";
    public string sEsInterlocutor = "true", sEsGestor = "true";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                //Master.nBotonera = 58;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Revisión de diálogos pendientes";
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                string[] aResul = Regex.Split(ObtenerDialogosPendientes(), "@#@");
                if (aResul[0] == "OK")
                {
                    //string[] aTablas = Regex.Split(DIALOGOALERTAS.ObtenerDialogosPendientes((int)Session["UsuarioActual"]), "{septabla}");
                    string[] aTablas = Regex.Split(aResul[1], "{septabla}");
                    strHTMLTablaUsuario = aTablas[0];
                    strHTMLTablaGestor = aTablas[1];
                    //Por defecto se muestran los tablas.
                    if (aTablas[2] == "1" && aTablas[3] == "0")
                    {
                        sEsGestor = "false";
                        divGestor.Style.Add("display", "none");
                        divUsuario.Style.Add("height", "580px");
                        divCatalogoUsuario.Style.Add("height", "520px");
                    }
                    else if (aTablas[2] == "0" && aTablas[3] == "1")
                    {
                        sEsInterlocutor = "false";
                        divUsuario.Style.Add("display", "none");
                        divGestor.Style.Add("height", "580px");
                        divCatalogoGestor.Style.Add("height", "520px");
                    }
                }
                else
                    Master.sErrores = aResul[1];

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
            case ("getDialogosPendientes"):
                sResultado += ObtenerDialogosPendientes();
                break;
            case ("goCarrusel"):
                sResultado += goCarrusel(aArgs[1]);
                break;
            case ("getAccionesPendientes"):
                sResultado += ObtenerAccionesPendientes();
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


    private string ObtenerDialogosPendientes()
    {
        try
        {
            return "OK@#@"+ DIALOGOALERTAS.ObtenerDialogosPendientes((int)Session["UsuarioActual"]);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los diálogos pendientes.", ex);
        }
    }
    private string goCarrusel(string sNumProyecto)
    {
        string sResul = "", sAccesoCarrusel = "0"; ;
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, "C", false);
            if (dr.Read())
            {
                Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                Session["RTPT_PROYECTOSUBNODO"] = (dr["rtpt"].ToString() == "1") ? true : false;
                Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                sAccesoCarrusel = "1";
            }
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@" + sAccesoCarrusel;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        return sResul;
    }
    private string ObtenerAccionesPendientes()
    {
        try
        {
            return "OK@#@" + Utilidades.ObtenerAccionesPendientesV2();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las acciones pendientes.", ex);
        }
    }
}

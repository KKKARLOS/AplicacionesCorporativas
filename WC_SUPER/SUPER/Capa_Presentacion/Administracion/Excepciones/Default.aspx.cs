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
using System.Text.RegularExpressions;
using System.Text;


public partial class Excepciones : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaAdmin;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de excepciones a las alertas";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}
                //string strTabla0 = obtenerProfesionalesExcepciones();
                string strTabla0 = SUPER.Capa_Negocio.Ficepi.obtenerProfesionalesExcepciones();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strTablaAdmin = aTabla0[1];
                else Master.sErrores = aTabla0[1];
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los profesionales", ex);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case ("profesionales"):
                    sResultado += SUPER.Capa_Negocio.Ficepi.obtenerProfesionales(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), true);
                    break;
                case ("profesionalesExcepciones"):
                    sResultado += SUPER.Capa_Negocio.Ficepi.obtenerProfesionalesExcepciones();
                    break;
                case ("nodos"):
                    sResultado += SUPER.Capa_Negocio.NODO.obtenerNodos();
                    break;
                case ("nodosExcepciones"):
                    sResultado += SUPER.Capa_Negocio.NODO.obtenerNodosExcepciones();
                    break;
                case ("clientes"):
                    sResultado += SUPER.Capa_Negocio.CLIENTE.ObtenerClientes(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]));
                    break;
                case ("clientesExcepciones"):
                    sResultado += SUPER.Capa_Negocio.CLIENTE.ObtenerClientesAvisosExcepciones();
                    break;

                case ("grabar"):
                    if (aArgs[1] == "P")
                    {
                        sResultado += SUPER.Capa_Negocio.Ficepi.Grabar(aArgs[2]);                        
                    }
                    else if (aArgs[1] == "N")
                    {
                        sResultado += SUPER.Capa_Negocio.NODO.Grabar(aArgs[2]);
                    }
                    else if (aArgs[1] == "C")
                    {
                        sResultado += SUPER.Capa_Negocio.CLIENTE.Grabar(aArgs[2]);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("profesionales"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los profesionales", ex);
                    break;
                case ("profesionalesExcepciones"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los profesionales sin avisos", ex);
                    break;
                case ("nodos"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los nodos", ex);
                    break;
                case ("nodosExcepciones"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los nodos sin avisos", ex);
                    break;
                case ("clientes"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los clientes", ex);
                    break;
                case ("clientesExcepciones"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los clientes sin avisos", ex);
                    break;
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar las excepciones", ex);
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

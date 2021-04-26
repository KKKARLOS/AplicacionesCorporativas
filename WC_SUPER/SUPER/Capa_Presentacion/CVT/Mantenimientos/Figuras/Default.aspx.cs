using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EO.Web;
using SUPER.Capa_Negocio;
using SUPER.BLL;
using System.Text.RegularExpressions;
using System.Text;

public partial class Capa_Presentacion_CVT_Mantenimientos_Figuras_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLFiguras = "", sConsultores = "", sEstructura = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de figuras";
            Master.bFuncionesLocales = true;
            Master.bEstilosLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Capa_Presentacion/CVT/Mantenimientos/Figuras/Functions/ddfiguras.js");
            Master.Modulo = "CVT";
            try
            {
                strTablaHTMLFiguras = Curriculum.cargarAdministradoresCurvit();
                sConsultores = Curriculum.cargarConsultores();
                sEstructura = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                //string strTabla0 = obtenerAdministradores();
                //string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                //if (aTabla0[0] != "Error") strTablaAdmin = aTabla0[1];
                //else Master.sErrores = aTabla0[1];
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        
        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "getProfesionales":
                sResultado += getProfesionales(Utilidades.unescape(aArgs[1]), 
                                               Utilidades.unescape(aArgs[2]), 
                                               Utilidades.unescape(aArgs[3]),
                                               aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("grabar"):
                sResultado += grabar(aArgs[1], aArgs[2]);
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

    public static string getProfesionales(string sAp1, string sAp2, string sNombre, string sCoste, string sExterno, string sBaja)
    {
        try
        {
            return "OK@#@" + Ficepi.ObtenerProfesionalesFigurasCVT(sAp1, sAp2, sNombre, 
                                                                    (sCoste == "1") ? true : false,
                                                                    (sExterno == "1") ? true : false,
                                                                    (sBaja == "1") ? true : false);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los profesionales.", ex);
        }
    }

    public static string grabar(string slProfesionales, string perfiles)
    {
        SqlConnection oConn=null;
        SqlTransaction tr;
        string sResul = "";
        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            Curriculum.updateFigurasFicepi(tr, slProfesionales, perfiles);
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las figuras FICEPI", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);           
        }
        return sResul;
    }

}

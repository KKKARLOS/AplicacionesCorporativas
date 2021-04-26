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

using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Administracion_Cualificacion_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 34;// 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Relación de cualificadores de ventas";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    strTablaHTML = SUPER.BLL.Cualificador.getHtmlMantenimiento();
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    protected string Grabar(string strFunciones)
    {
        string sResul = "", sDesc = "";

        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }

        try
        {
            string[] aFun = Regex.Split(strFunciones, "///");
            bool bCR = false, bPIG = false;
            int? iClaseEco = null;

            foreach (string oFun in aFun)
            {
                string[] aValores = Regex.Split(oFun, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID Cualificador
                //2. Descripcion
                //3. Defecto CR
                //4. Defecto PIG
                //5. Clase económica
                sDesc = Utilidades.unescape(aValores[2]);

                if (aValores[3] == "1") bCR = true; else bCR = false;
                if (aValores[4] == "1") bPIG = true; else bPIG = false;
                if (aValores[5] != "" && aValores[5] != "null") iClaseEco = int.Parse(aValores[5]); else iClaseEco = null;

                switch (aValores[0])
                {
                    case "I":
                        SUPER.BLL.Cualificador.Insert(tr, Utilidades.unescape(aValores[2]), bCR, bPIG, iClaseEco);
                        break;
                    case "U":
                        SUPER.BLL.Cualificador.Update(tr, short.Parse(aValores[1]), Utilidades.unescape(aValores[2]), bCR, bPIG, iClaseEco);
                        break;
                    case "D":
                        SUPER.BLL.Cualificador.Delete(tr, short.Parse(aValores[1]));
                        break;
                }
            }
            #region Comprobar que haya uno y solo un cualificador marcado defecto para CR y para PIG

            if (SUPER.BLL.Cualificador.getNumDefectoParaNodos(tr) != 1)
                throw new Exception("Durante tu intervención, otro usuario ha modificado el nodo por defecto de los cualificadores.\n\nDebe haber uno y solo un cualificador con el campo defecto para " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + " marcado.");
            
            if (SUPER.BLL.Cualificador.getNumDefectoParaPIG(tr) != 1)
                throw new Exception("Durante tu intervención, otro usuario ha modificado el PIG por defecto de los cualificadores.\n\nDebe haber uno y solo un cualificador con el campo defecto para PIG marcado.");
            
            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al actualizar los cualificadores.", ex) + "@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
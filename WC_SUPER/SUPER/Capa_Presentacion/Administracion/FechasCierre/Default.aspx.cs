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


public partial class Administradores : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaOrgVtasSAP;
    public string strTablaOrgVtasSuper;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de fechas de cierre";
            Master.bFuncionesLocales = true;
            Master.bEstilosLocales = true;
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            txtAnno.Text = DateTime.Now.Year.ToString();

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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("obtenermeses"):
                sResultado += obtenerMesesCierre(int.Parse(aArgs[1]));
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
    private string obtenerMesesCierre(int iAnno)
    {
        try
        {
            return "OK@#@" + SUPER.BLL.MESESCIERRE.Obtener(iAnno);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses de cierre.", ex);
        }
    }
    private string Grabar(string strDatos)
    {
        string sResul = "" ;

        #region Abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
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
            #region Datos Límites de Facturación
            if (strDatos != "") 
            {
                string[] aFilas = Regex.Split(strDatos, "{sepreg}");
                foreach (string oFila in aFilas)
                {
                    if (oFila == "") continue;
                    string[] aDatos = Regex.Split(oFila, "{sep}");
                    ///aDatos[0] = añomes
                    ///aDatos[1] = Límite Facturación   t637_fecha
                    ///aDatos[2] = Límite Respuesta     t828_limitealertas
                    ///aDatos[3] = Previsión cierre ECO t855_prevcierreeco

                    LIMITEFACTURACION.Update(tr, int.Parse(aDatos[0]), (aDatos[1] == "") ? null : (DateTime?)DateTime.Parse(aDatos[1]));
                    SUPER.Capa_Datos.LIMITEALERTAS.Update(tr, int.Parse(aDatos[0]), (aDatos[2] == "") ? null : (DateTime?)DateTime.Parse(aDatos[2]));
                    SUPER.DAL.MESESCIERRE.UpdatePrevCierreECO(tr, int.Parse(aDatos[0]), (aDatos[3] == "") ? null : (DateTime?)DateTime.Parse(aDatos[3]));                        
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los meses de cierre", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}

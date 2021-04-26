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

            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("obtenermeses"):
                sResultado += obtenerMesesCierre(int.Parse(aArgs[1]));
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
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

        #region Abrir conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region Datos L�mites de Facturaci�n
            if (strDatos != "") 
            {
                string[] aFilas = Regex.Split(strDatos, "{sepreg}");
                foreach (string oFila in aFilas)
                {
                    if (oFila == "") continue;
                    string[] aDatos = Regex.Split(oFila, "{sep}");
                    ///aDatos[0] = a�omes
                    ///aDatos[1] = L�mite Facturaci�n   t637_fecha
                    ///aDatos[2] = L�mite Respuesta     t828_limitealertas
                    ///aDatos[3] = Previsi�n cierre ECO t855_prevcierreeco

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

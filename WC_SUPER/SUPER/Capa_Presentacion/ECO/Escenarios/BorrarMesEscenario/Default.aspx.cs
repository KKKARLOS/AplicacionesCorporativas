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
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sErrores = "", sMeses = "";
    public int nEscenario = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (Request.QueryString["sm"] != null)
                    sMeses = Utilidades.decodpar(Request.QueryString["sm"].ToString());

            }
            catch (Exception ex)
            {
                this.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        string sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        //switch (aArgs[0])
        //{
        //    //case ("procesar"):
        //    //    sResultado += Procesar(aArgs[1]);
        //    //    break;
        //    case ("getDatos"):
        //        sResultado += getDatos(int.Parse(aArgs[1]));
        //        break;
        //}

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    public string getDatos(int nIDEscenario)
    {
        return SUPER.Capa_Negocio.ESCENARIOMES.ObtenerMesesBorrables(nIDEscenario);
    }

    //protected string Procesar(string strDatos)
    //{
    //    string sResul = "";

    //    #region apertura de conexión y transacción
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccionSerializable(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        SEGMESPROYECTOSUBNODO.BorrarMesesAbiertos(tr, strDatos);

    //        Conexion.CommitTransaccion(tr);
    //        sResul = "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al borrar los meses abiertos indicados.", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }

    //    return sResul;
    //}

}

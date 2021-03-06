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
using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sLectura = "false";
    public SqlConnection oConn;
    public SqlTransaction tr;

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
                CargarDatos();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la cabecera de la orden", ex);
            }
            //1? Se indican (por este orden) la funci?n a la que se va a devolver el resultado
            //   y la funci?n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2? Se "registra" la funci?n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1? Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; 
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2? Aqu? realizar?amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
        }
        //3? Damos contenido a la variable que se env?a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env?a el resultado al cliente.
        return _callbackResultado;
    }

    private string Grabar(string sPassw)
    {
        string sResul = "";
        bool bErrorControlado = false;

        #region abrir conexi?n y transacci?n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi?n", ex);
            return sResul;
        }
        #endregion
        try
        {
            SUPER.Capa_Negocio.USUARIO.GrabarPasswServicio(int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), Utilidades.unescape(sPassw));

            Conexion.CommitTransaccion(tr);
            if (sPassw=="")
                sResul = "OK@#@S";//Para indicar que se ha borrado la contrase?a
            else
                sResul = "OK@#@N";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la contrase?a", ex);
            else sResul = "Error@#@Operaci?n rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private void CargarDatos()
    {
        this.hdnPassw.Value = SUPER.Capa_Negocio.USUARIO.GetPasswServicio(int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
        this.txtPass2.Value = this.hdnPassw.Value;
    }
}

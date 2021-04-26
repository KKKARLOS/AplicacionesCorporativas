using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;
using GASVI.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    public string strTablaHtml;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sErrores = "";
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["GVT_IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                if (Request.QueryString.Get("iC") != null)
                {
                    hdnIdConsulta.Text = Utilidades.decodpar(Request.QueryString.Get("iC").ToString());
                    if (Request.QueryString.Get("dC") != null)
                    {
                        txtDenominacion.Text = Utilidades.decodpar(Request.QueryString.Get("dC").ToString());
                    }
                    if (Request.QueryString.Get("eC") != null)
                    {
                        if (Utilidades.decodpar(Request.QueryString.Get("eC").ToString()) == "1") rdbEstado.Text = "1";
                        else rdbEstado.Text = "0";
                    }
                    //if (Request.QueryString.Get("dsB") != null)
                    //{
                    //    txtDescripcion.Text = Request.QueryString.Get("dsB").ToString();
                    //}
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del bono de transporte", ex);
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
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                try
                {
                    Administracion.Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los cambios realizados en la consulta.", ex);
                }
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
}

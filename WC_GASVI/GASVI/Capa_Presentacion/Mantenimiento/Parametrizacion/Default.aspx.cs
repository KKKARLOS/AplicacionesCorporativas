using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using GASVI.BLL;
using EO.Web;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 19;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Mantenimiento de parametrizaci�n GASVI";

            try
            {
                DIALIMITECONTABILIZACIONGV oDL = DIALIMITECONTABILIZACIONGV.Obtener();
                hdnAnioAnt.Text = oDL.t670_dialimitecontanoanterior.ToString();
                hdnMesAnt.Text = oDL.t670_dialimitecontmesanterior.ToString();
                hdnSemana.Text = oDL.t670_diapago.ToString();
                txtVigencia.Text = oDL.t670_vigenciaaparcadas.ToString();
                txtEliminacion.Text = oDL.t670_avisoaparcadas.ToString();
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos de la parametrizaci�n GASVI.", ex);
            }

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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                try
                {
                    DIALIMITECONTABILIZACIONGV.UpdateParametrizacion(aArgs[1]);
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los cambios.", ex);
                }
                break;
        } 

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
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

    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

    protected void Regresar()
    {
        string sUrl = HistorialNavegacion.Leer();
        try
        {
            Response.Redirect(sUrl, true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }
}

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
using SUPER.Capa_Negocio;

using System.Text.RegularExpressions;

public partial class Capa_Presentacion_ECO_AvanceDetalle_Comentario : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public int nTarea;
    public int nRecurso;


    private void Page_Load(object sender, System.EventArgs e)
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

                nTarea = int.Parse(Request.QueryString["nTarea"].ToString());
                nRecurso = int.Parse(Request.QueryString["nRecurso"].ToString());

                TareaRecurso oTR = TareaRecurso.Obtener(nTarea, nRecurso);
                txtIndicaciones.Text = oTR.sIndicaciones;
                txtObservaciones.Text = oTR.sComentario;
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
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

    private string Grabar(string sTarea, string sRecurso, string strIndicaciones)
    {
        try
        {
            TareaRecurso.ModificarIndicaciones(null, int.Parse(sTarea), int.Parse(sRecurso), Utilidades.unescape(strIndicaciones));
            return "OK";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al grabar las indicaciones", ex);
        }
    }

}

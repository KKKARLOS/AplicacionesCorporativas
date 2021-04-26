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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string  sErrores;
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

                if (Request.QueryString["sPSN"] != null)
                {
                    hdnPSN.Value = Request.QueryString["sPSN"].ToString();
                    //if (HayTareasParaAvisar(int.Parse(hdnPSN.Value)))
                    //    this.hdnAvisar.Value = "S";
                }
                if (Request.QueryString["RTPT"] != null)
                {
                    this.hdnRTPT.Value = Request.QueryString["RTPT"].ToString();
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al cargar la pantalla de exportaci�n", ex);
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
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("procesar"):
                sResultado += Procesar(int.Parse(aArgs[1]), byte.Parse(aArgs[2]), byte.Parse(aArgs[3]));
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

    private string Procesar(int iPSN, byte bLineaBase, byte bSoloRTPT)
    {
        string sResul = "OK@#@", sAux="";
        try
        {
            if (bLineaBase == 1)
            {
                //if (HayTareasParaAvisar(iPSN))
                //    sResul = "AVISO@#@Existen tareas cuya fecha de fin planificada\nes superior a la fecha de fin prevista.";
                if (bSoloRTPT == 1)
                    sAux = OpenProj.GetTareasPlanMayorPrevisto(null, iPSN, int.Parse(Session["UsuarioActual"].ToString()));
                else
                    sAux=OpenProj.GetTareasPlanMayorPrevisto(null, iPSN);
                if (sAux != "")
                    sResul = "AVISO@#@" + sAux;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@Operaci�n rechazada.\n\n" + ex.Message;
        }
        return sResul;
    }

    //Comprueba si hay tareas donde FFPL > FFPR
    public bool HayTareasParaAvisar(int nCodPE)
    {
        bool bAvisar;
        try
        {
            bAvisar = OpenProj.bTareasParaAvisar(null, nCodPE);
        }
        catch (Exception)
        {
            bAvisar = false;
        }
        return bAvisar;
    }
}

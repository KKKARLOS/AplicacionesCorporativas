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

using System.Text.RegularExpressions;
using EO.Web; 
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;



public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial;
    protected string sLectura;
    public string strTablaHTMLIntegrantes, sErrores="";

    protected void Page_Load(object sender, EventArgs e)
    {
        strInicial = "";
        sLectura = "false";
        if (!Page.IsCallback)
        {           
            if (!Page.IsPostBack)
            {
                try
                {
                    strTablaHTMLIntegrantes = "<table id='tblOpciones2'><tbody id='tbodyDatos'></tbody></table>";
                    string sGrupoAux = Request.QueryString["nIdGrupo"];
                    if (sGrupoAux != null)
                    {
                        this.hdnIdGp.Text = sGrupoAux;
                    }
                    if (sGrupoAux != "")
                    {
                        GrupoProf miGP = new GrupoProf();
                        miGP.Obtener(sGrupoAux);
                        this.txtDesGP.Text = miGP.sDesGP;
                        this.txtDesGP.Focus();
                        //Cargo la lista de integrantes que ya pertenezcan a este Grupo de profesionales
                        strTablaHTMLIntegrantes = SUPER.Capa_Negocio.GrupoProf.DetalleIntegrantes(sGrupoAux);
                    }
                }
                catch (Exception ex)
                {
                    sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += SUPER.Capa_Negocio.GrupoProf.ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                    break;
                case ("grabar"):
                    sResultado += SUPER.Capa_Negocio.GrupoProf.Grabar(aArgs[1], aArgs[2]);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
                case ("grabar"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
            }
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
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
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos'></table>";
    public string sErrores = "";
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
                if (!Page.IsPostBack)
                {
                    this.hdnIdFicepiCert.Value = Utilidades.decodpar(Request.QueryString["f"].ToString());

                    //cboEntCert.DataSource = SUPER.BLL.Examen.obtenerEntCert(8, 1); //tipo 8-->entidad certificadora y activo=1
                    cboEntCert.DataSource = SUPER.BLL.Examen.obtenerEntCert2(8, 1); //tipo 8-->entidad certificadora y activo=1
                    cboEntCert.DataValueField = "sValor";
                    cboEntCert.DataTextField = "sDenominacion";
                    cboEntCert.DataBind();
                    //Que solo muestre los validados
                    //cboEntorno.DataSource = SUPER.BLL.EntornoTecno.obtenerCboEntorno(int.Parse("0"));
                    cboEntorno.DataSource = SUPER.BLL.EntornoTecno.obtenerCboEntorno2(0);
                    cboEntorno.DataValueField = "sValor";
                    cboEntorno.DataTextField = "sDenominacion";
                    cboEntorno.DataBind();
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; 
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerCertificados(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("setCert"):
                sResultado += "OK";
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string ObtenerCertificados(string sDenominacion, string sEntidad, string sEntorno)
    {
        int? idEntorno = null;
        int? idEntidad = null;

        try
        {
            if (sEntorno != "" && sEntorno != "-1") idEntorno = int.Parse(sEntorno);
            if (sEntidad != "" && sEntidad != "-1") idEntidad = int.Parse(sEntidad);


            return "OK@#@" + SUPER.BLL.Certificado.GetCertificados(null, idEntidad, idEntorno, sDenominacion);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los certificados", ex);
            return "Error@#@Error al obtener los certificados";
        }
    }
}

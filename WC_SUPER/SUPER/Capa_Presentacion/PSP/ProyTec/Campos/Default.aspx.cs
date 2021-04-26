using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using EO.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "<table id='tblDatos'></table>";
    public string sErrores = "", sProfEntrada = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                obtenerTipos();
                hdn_ficepi_actual.Text = Session["IDFICEPI_PC_ACTUAL"].ToString();
                if (Request.QueryString["t305_idproyectosubnodo"] != null)
                    hdnT305_idproyectosubnodo.Value = Request.QueryString["t305_idproyectosubnodo"];

                if (Request.QueryString["nTarea"] != null)
                {
                    hdnCodTarea.Value = Request.QueryString["nTarea"];
                    hdnCodTarea.Value = hdnCodTarea.Value.Replace(".", "");
                }
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado y la función que va a acceder al servidor
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
        ArrayList aLista = new ArrayList();
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += SUPER.Capa_Negocio.CAMPOS.Grabar(aArgs[1]);
                    break;
                case ("getDatos"):
                    sResultado += SUPER.Capa_Negocio.CAMPOS.Catalogo(int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()),
                                                                             int.Parse(aArgs[1]),
                                                                             aArgs[2],
                                                                             int.Parse(hdnT305_idproyectosubnodo.Value),
                                                                             aLista);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
                case ("getDatos"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
            }
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private void obtenerTipos()
    {
        cboTipoDato.DataValueField = "ID";
        cboTipoDato.DataTextField = "DENOMINACION";
        cboTipoDato.DataSource = SUPER.Capa_Datos.TIPODATOCAMPO.Catalogo();
        cboTipoDato.DataBind();
        cboTipoDato.Items.Insert(cboTipoDato.Items.Count, new ListItem("Todos", "9"));
        cboTipoDato.SelectedValue = "9";
    }
}

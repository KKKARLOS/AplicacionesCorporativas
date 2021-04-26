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
    public string sErrores = "", sProfesional = "",sOrigen="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                sProfesional = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
                obtenerTipos();
            
                // Leer tarifa de datos

                hdn_ficepi_actual.Text = Session["IDFICEPI_PC_ACTUAL"].ToString();

                if (Request.QueryString["t305_idproyectosubnodo"] != null)
                    hdn_idproyectosubnodo.Text= Request.QueryString["t305_idproyectosubnodo"];

                if (Request.QueryString["origen"] != null)
                    sOrigen = Request.QueryString["origen"];

                if (Request.QueryString["bNueva"] != "true")
                {
                    hdnID.Text = Request.QueryString["ID"].ToString();
                    SUPER.Capa_Datos.CAMPOS oCAMPOS = SUPER.Capa_Datos.CAMPOS.Select(null, int.Parse(hdnID.Text));

                    cboAmbito.SelectedValue = oCAMPOS.codAmbito.ToString();

                    txtDenominacion.Text = oCAMPOS.t290_denominacion;
                    cboTipoDato.SelectedValue = oCAMPOS.t291_idtipodato.ToString();
                    txtCreador.Text = oCAMPOS.profesional_creador;
                    hdn_ficepi_creador.Text = oCAMPOS.t001_idficepi_creador.ToString();

                    switch (oCAMPOS.codAmbito.ToString())
                    {
                        case ("0"):
                            lblAmbitoSel.Style.Add("visibility", "hidden");
                            break;
                        case ("1"):
                            lblAmbitoSel.InnerText = oCAMPOS.profesional_owner;
                            break;
                        case ("2"):
                            lblAmbitoSel.InnerText = (oCAMPOS.t301_idproyecto.HasValue) ? ((int)oCAMPOS.t301_idproyecto).ToString("#,###,###") + "-" + oCAMPOS.denominacion_proyecto : "";
                            break;
                        case ("3"):
                            lblAmbitoSel.InnerText = (oCAMPOS.t302_idcliente.HasValue) ? ((int)oCAMPOS.t302_idcliente).ToString("#,###,###") + "-" + oCAMPOS.denominacion_cliente : "";
                            break;
                        case ("4"):
                            lblAmbitoSel.InnerText = (oCAMPOS.t303_idnodo.HasValue) ? ((int)oCAMPOS.t303_idnodo).ToString("#,###,###") + "-" + oCAMPOS.denominacion_nodo : "";
                            break;
                    }
                    ModoLectura.Poner(this.Controls);
                }
                else
                {
                    txtCreador.Text = Session["APELLIDO1_ENTRADA"].ToString() + " " + Session["APELLIDO2_ENTRADA"].ToString() + ", " + Session["NOMBRE_ENTRADA"].ToString();
                    cboAmbito.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la tarifa de datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    private void obtenerTipos()
    {
        cboTipoDato.DataValueField = "ID";
        cboTipoDato.DataTextField = "DENOMINACION";
        cboTipoDato.DataSource = SUPER.Capa_Datos.TIPODATOCAMPO.Catalogo();
        cboTipoDato.DataBind();
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@";
        try
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += SUPER.Capa_Negocio.CAMPOS.Grabar(aArgs[1]);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los campos", ex);
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
}

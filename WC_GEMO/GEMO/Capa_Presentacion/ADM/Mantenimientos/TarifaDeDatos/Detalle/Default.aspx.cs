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
using Microsoft.JScript;
using System.Text.RegularExpressions;
using System.Text;

using GEMO.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                obtenerProveedores();
            
                // Leer tarifa de datos

                if (Request.QueryString["bNueva"] != "true")
                {
                    hdnID.Text = Request.QueryString["ID"].ToString();
                    GEMO.DAL.TARIFADATOS oTARIFADATOS = GEMO.DAL.TARIFADATOS.Select(null, short.Parse(hdnID.Text));

                    txtDenominacion.Text = oTARIFADATOS.t711_denominacion;
                    cboProveedor.SelectedValue = oTARIFADATOS.t063_idproveedor.ToString();
                    txtCodTarProv.Text = oTARIFADATOS.t711_codtarifaprov;

                    txtPrecio.Text = decimal.Parse(oTARIFADATOS.t711_precio.ToString()).ToString("##0.0000");
                    //txtDesdeAcep.Text = decimal.Parse(oTARIFADATOS.t711_desde_acep.ToString()).ToString("##0.0000");
                    //txtHastaAcep.Text = decimal.Parse(oTARIFADATOS.t711_hasta_acep.ToString()).ToString("##0.0000");
                }
                else cboProveedor.SelectedValue = "1";
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
    private void obtenerProveedores()
    {
        cboProveedor.DataValueField = "ID";
        cboProveedor.DataTextField = "DENOMINACION";
        cboProveedor.DataSource = GEMO.DAL.PROVEEDORES.Catalogo();
        cboProveedor.DataBind();
        //cboProveedor.Items.Insert(0, new ListItem("", "0"));
        //cboProveedor.SelectedValue = "0";	
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
                    sResultado += "OK@#@" + 
                            GEMO.BLL.TARIFADATOS.Grabar
                                (
                                byte.Parse(aArgs[1]),			                            // 0=UPDATE 1=INSERT
                                short.Parse(aArgs[2]),                                      // t711_idtarifa
                                Utilidades.unescape(aArgs[3]),                            // t711_denominacion
                                byte.Parse(aArgs[4]),                                       // t063_idproveedor 
                                Utilidades.unescape(aArgs[5]),                            // t711_CodTarProv
                                double.Parse((aArgs[6]=="")? "0":aArgs[6])                // t711_precio Precio
                                //double.Parse((aArgs[7] == "") ? "0" : aArgs[7]),                // t711_desde_acep (Intervalo aceptación desde)
                                //double.Parse((aArgs[8] == "") ? "0" : aArgs[8])                 // t711_hasta_acep (Intervalo aceptación hasta)
                                );
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar las líneas", ex);
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

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
    //public SqlConnection oConn;
    //public SqlTransaction tr;
    public string sLectura = "true";
    public string sOrigen = "2";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                obtenerProveedores();
                obtenerMedios();
                obtenerTarifaDatos();
                obtenerTiposTarjeta();
                //obtenerEstados();

                if (Request.QueryString["sLectura"] != null) sLectura = Request.QueryString["sLectura"].ToString();
                if (Request.QueryString["sOrigen"] != null) sOrigen = Request.QueryString["sOrigen"].ToString();

                //string[] aFiguras = Regex.Split(Session["FIGURAS"].ToString(), ",");
                //for (int i = 0; i < aFiguras.Length; i++)
                //{
                //    if (aFiguras[i] == "C")
                //    {
                //        sLectura = "false";
                //        break;
                //    }
                //}
                if (User.IsInRole("C")) sLectura = "false";

                // Leer Línea

                if (Request.QueryString["bNueva"] != "true")
                {

                    hdnId.Text = Request.QueryString["ID"].ToString();
                    GEMO.DAL.LINEA oLINEA = GEMO.DAL.LINEA.Select(null, int.Parse(hdnId.Text));
                    //if (oLINEA.t708_prefintern.ToString() == "") txtPrefijo.Text = "";
                    //else txtPrefijo.Text = short.Parse(oLINEA.t708_prefintern.ToString()).ToString("000");
                    txtPrefijo.Text = oLINEA.t708_prefintern.ToString();
                    txtNumlinea.Text = long.Parse(oLINEA.t708_numlinea.ToString()).ToString("###########");
                    txtNumext.Text = int.Parse(oLINEA.t708_numext.ToString()).ToString("#########");

                    if (oLINEA.t313_idempresa == null)
                    {
                        hdnIdEmpresa.Text = "0";
                        //txtIDEmpresa.Text = "";
                        txtEmpresa.Text = "";
                    }
                    else
                    {
                        hdnIdEmpresa.Text = oLINEA.t313_idempresa.ToString();
                        //txtIDEmpresa.Text = int.Parse(oLINEA.t313_idempresa.ToString()).ToString("##,###,###");
                        txtEmpresa.Text = oLINEA.empresa;
                    }

                    if (oLINEA.t712_idtarjeta != null)   cboTipoTarjeta.SelectedValue = oLINEA.t712_idtarjeta.ToString();
                    if (oLINEA.t063_idproveedor != null) cboProveedor.SelectedValue = oLINEA.t063_idproveedor.ToString();

                    if (oLINEA.t134_idmedio != null) cboMedio.SelectedValue = oLINEA.t134_idmedio.ToString();

                    cboTarifa.SelectedValue = oLINEA.t711_idtarifa.ToString();
                    

                    if (sLectura == "false" || sOrigen=="2")
                    {
                        switch (oLINEA.t710_idestado.ToString())
                        {
                            case ("X"):
                                {
                                    cboEstado.Items.Add(new ListItem("Preactiva", "X"));
                                    cboEstado.Items.Add(new ListItem("Activa", "A"));
                                    cboEstado.Items.Add(new ListItem("Inactiva", "I"));
                                    break;
                                }
                            case ("A"):
                            case ("B"):
                                {
                                    cboEstado.Items.Add(new ListItem("Bloqueada", "B"));
                                    cboEstado.Items.Add(new ListItem("Activa", "A"));
                                    cboEstado.Items.Add(new ListItem("Inactiva", "I"));
                                    break;
                                }
                            case ("Y"):
                                {
                                    cboEstado.Items.Add(new ListItem("Preinactiva", "Y"));
                                    cboEstado.Items.Add(new ListItem("Activa", "A"));
                                    cboEstado.Items.Add(new ListItem("Inactiva", "I"));
                                    break;
                                }
                            case ("I"):
                                {
                                    cboEstado.Items.Add(new ListItem("Inactiva", "I"));
                                    cboEstado.Items.Add(new ListItem("Activa", "A"));
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (oLINEA.t710_idestado.ToString())
                        {
                            case ("X"):
                            case ("A"):
                            case ("Y"):
                                {
                                    cboEstado.Items.Add(new ListItem("Activa", "A"));
                                    break;
                                }
                            case ("B"):
                                {
                                    cboEstado.Items.Add(new ListItem("Bloqueada", "B"));
                                    break;
                                }
                            case ("I"):
                                {
                                    cboEstado.Items.Add(new ListItem("Inactiva", "I"));
                                    break;
                                }
                        }
                    }
                    cboEstado.SelectedValue = oLINEA.t710_idestado.ToString();
                    hdnEstado.Text = cboEstado.SelectedValue;

                    txtModelo.Text = oLINEA.t708_modelo;
                    txtIMEI.Text = oLINEA.t708_IMEI;
                    txtICC.Text = oLINEA.t708_ICC;
                    txtObserva.Text = oLINEA.t708_observa;

                    if (oLINEA.t001_responsable == null)
                    {
                        hdnIdResponsable.Text = "0";
                        //txtIDResponsable.Text = "";
                        txtResponsable.Text = "";
                    }
                    else
                    {
                        hdnIdResponsable.Text = oLINEA.t001_responsable.ToString();
                        //txtIDResponsable.Text = int.Parse(oLINEA.t001_responsable.ToString()).ToString("##,###,###");
                        txtResponsable.Text = oLINEA.responsable;
                    }

                    if (oLINEA.t708_tipouso != null) rdlTipoUso.SelectedValue = oLINEA.t708_tipouso;

                    if (oLINEA.t001_beneficiario == null)
                    {
                        hdnIdBeneficiario.Text = "0";
                        //txtIDBeneficiario.Text = "";
                        txtBeneficiario.Text = "";
                    }
                    else
                    {
                        hdnIdBeneficiario.Text = oLINEA.t001_beneficiario.ToString();
                        //txtIDBeneficiario.Text = int.Parse(oLINEA.t001_beneficiario.ToString()).ToString("##,###,###");
                        txtBeneficiario.Text = oLINEA.beneficiario;
                    }

                    txtDepartamento.Text = oLINEA.t708_departamento;

                    if ((bool)oLINEA.t708_QEQ) chkQEQ.Checked = true;
                    else chkQEQ.Checked = false;

                }
                else
                {
                    //cboEstado.Items.Add(new ListItem("", "0"));
                    cboEstado.Items.Add(new ListItem("Activa", "A"));
                    //cboProveedor.SelectedValue = "1";
                    //cboEstado.Items.Add(new ListItem("Preactiva", "X"));
                    //cboEstado.SelectedValue = "A";
                    //cboEstado.Enabled = false;
                }

                if (sLectura == "true") ModoLectura.Poner(this.Controls);

            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la línea", ex);
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
    private void obtenerMedios()
    {
        cboMedio.DataValueField = "ID";
        cboMedio.DataTextField = "DENOMINACION";
        cboMedio.DataSource = GEMO.DAL.MEDIO.Catalogo2();
        cboMedio.DataBind();
        //cboMedio.Items.Insert(0, new ListItem("", "0"));
        //cboMedio.SelectedValue = "0";	
    }
    private void obtenerTarifaDatos()
    {
        cboTarifa.DataValueField = "ID";
        cboTarifa.DataTextField = "DENOMINACION";
        cboTarifa.DataSource = GEMO.DAL.TARIFADATOS.Catalogo();
        cboTarifa.DataBind();
        //cboTarifa.Items.Insert(0, new ListItem("", "0"));
        //cboTarifa.SelectedValue = "0";	
    }
    private void obtenerEstados()
    {
        cboEstado.DataValueField = "ID";
        cboEstado.DataTextField = "DENOMINACION";
        cboEstado.DataSource = GEMO.DAL.ESTADO.Catalogo2();
        cboEstado.DataBind();
        //cboEstado.Items.Insert(0, new ListItem("", "0"));
        //cboEstado.SelectedValue = "0";	
    }    
    private void obtenerTiposTarjeta()
    {
        cboTipoTarjeta.DataValueField = "ID";
        cboTipoTarjeta.DataTextField = "DENOMINACION";
        cboTipoTarjeta.DataSource = GEMO.DAL.TIPOTARJETA.Catalogo();
        cboTipoTarjeta.DataBind();
        //cboTipoTarjeta.Items.Insert(0, new ListItem("", "0"));
        //cboTipoTarjeta.SelectedValue = "0";	
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
                            GEMO.BLL.LINEA.Grabar
                                (
                                byte.Parse(aArgs[1]),			                            // 0=UPDATE 1=INSERT
                                int.Parse(aArgs[2]),                                        // t708_idlinea
                                (aArgs[3] != "0") ? (short?)short.Parse(aArgs[3]) : null,    // t708_prefintern
                                long.Parse(aArgs[4]),                                       // t708_numlinea
                                int.Parse((aArgs[5] == "") ? "0" : aArgs[5]),               // t708_numext
                                (aArgs[6] != "0")?(int?)int.Parse(aArgs[6]):null,           // t313_idempresa 
                                (aArgs[7] != "")?(byte?)byte.Parse(aArgs[7]):null,          // t712_idtarjeta
                                (aArgs[8] != "")?(byte?)byte.Parse(aArgs[8]):null,          // t063_idproveedor 
                                (aArgs[9] != "")?(short?)short.Parse(aArgs[9]):null,        // t134_idmedio 
                                Utilidades.unescape(aArgs[10]),                              // t708_modelo
                                Utilidades.unescape(aArgs[11]),                             // t708_IMEI 
                                Utilidades.unescape(aArgs[12]),                             // t708_ICC 
                                Utilidades.unescape(aArgs[13]),                             // t708_observa
                                aArgs[14],                                                  // t710_idestado 
                                (aArgs[15] != "")?(short?)short.Parse(aArgs[15]):null,      // t711_idtarifa 
                                (aArgs[16] != "0")?(int?)int.Parse(aArgs[16]):null,         // t001_responsable 
                                aArgs[17],                                                  // t708_tipouso
                                (aArgs[18] != "0")?(int?)int.Parse(aArgs[18]):null,         // t001_beneficiario
                                Utilidades.unescape(aArgs[19]),                             // t708_departamento 
                                (aArgs[20] != "0") ? true : false                           // t708_QEQ 
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

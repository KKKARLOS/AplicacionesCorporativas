using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;
using SUPER.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nID = 0, nPSN=0, nIDNaturaleza=0;
    public string sEstado = "", sTipo = "", sMsgRecuperada = "", sOrigen = "", sFechaIAP="", sErrores="";
    public string sDiaLimiteContAnoAnterior = "1", sDiaLimiteContMesAnterior = "1", sNodoUsuario = "";
    public bool bLectura = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            Utilidades.SetEventosFecha(this.txtFecAnticipo);
            Utilidades.SetEventosFecha(this.txtFecDevolucion);
            //Master.bContienePestanas = true;
            //Master.bFuncionesLocales = true;
            //Master.TituloPagina = "Detalle de nota estándar";

            //Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            //Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            //Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            //Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);

            //if (Request.QueryString["ni"] != null)
            //    nID = int.Parse(Utilidades.decodpar(Request.QueryString["ni"].ToString()));
            //if (Request.QueryString["se"] != null)
            //    sEstado = Utilidades.decodpar(Request.QueryString["se"].ToString());
            //if (Request.QueryString["st"] != null)
            //    sTipo = Utilidades.decodpar(Request.QueryString["st"].ToString());
            //if (Request.QueryString["so"] != null)
            //    sOrigen = Utilidades.decodpar(Request.QueryString["so"].ToString());
            if (Request.QueryString["nPSN"] != null)
                nPSN = int.Parse(Utilidades.decodpar(Request.QueryString["nPSN"].ToString()));
            if (Request.QueryString["sF"] != null)
                sFechaIAP = Utilidades.decodpar(Request.QueryString["sF"].ToString());
            if (Request.QueryString["nN"] != null)
                nIDNaturaleza = int.Parse(Utilidades.decodpar(Request.QueryString["nN"].ToString()));

            lblBeneficiario.InnerText = (Session["SEXOUSUARIO"].ToString() == "V") ? "Beneficiario" : "Beneficiaria";
            cboEmpresa.Style.Add("display", "none");

            //if (sEstado == "" || sEstado == "R" || sEstado == "P")
                divDisposiciones.Style.Add("display", "block");
            //else
            //    divDisposiciones.Style.Add("display", "none");

            if (!Page.IsPostBack)
            {
                try
                {
                    obtenerMotivos();
                    cboMotivo.Enabled = false;
                    if (nIDNaturaleza == 20)
                        cboMotivo.SelectedValue = "6";
                    obtenerMonedas();

                    if (bLectura)
                    {
                        ModoLectura.Poner(this.Controls);
                        imgCalculadora.Visible = false;
                        Calculadora.Visible = false;
                    }
                    ObtenerDatosIAP(nPSN);
                    ObtenerDatosCabecera(nID);
                    ObtenerDatosGastos(nID);

                }
                catch (Exception ex)
                {
                    sErrores = Errores.mostrarError("Error al cargar los datos.", ex);
                }
            }
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("tramitar"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.TramitarNotaEstandar(aArgs[1], aArgs[2]);
                }
                catch (Exception ex)
                {
                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "Error@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al tramitar la nota estándar.", ex);
                }
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://Gastos
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Anticipos
                        //sCad = ObtenerRecursosAsociados(aArgs[2], aArgs[4], false);
                        if (sCad.IndexOf("Error@#@") >= 0)
                            sResultado += sCad;
                        else
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 2://Otros datos
                        try
                        {
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + CABECERAGV.ObtenerHistorial((aArgs[2]=="")?0:int.Parse(aArgs[2]));
                        }
                        catch (Exception ex)
                        {
                            sResultado += "Error@#@" + aArgs[1] + "@#@" + Errores.mostrarError("Error al obtener el historial.", ex);
                        }

                        break;
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

    protected void obtenerMotivos()
    {
        cboMotivo.DataSource = MOTIVO.ObtenerMotivos();
        cboMotivo.DataValueField = "sValor";
        cboMotivo.DataTextField = "sDenominacion";
        cboMotivo.DataBind();
        cboMotivo.SelectedValue = "1";
    }
    protected void obtenerMonedas()
    {
        List<ElementoLista> oLista = MONEDAGV.ObtenerMonedas(true);
        ListItem oLI = null;
        foreach (ElementoLista oMoneda in oLista)
        {
            oLI = new ListItem(oMoneda.sDenominacion, oMoneda.sValor);
            cboMoneda.Items.Add(oLI);
        }
        cboMoneda.SelectedValue = "1";
    }

    protected void ObtenerDatosIAP(int nPSN)
    {
        //Nueva nota
        if (nPSN > 0)
        {
            hdnIdProyectoSubNodo.Text = nPSN.ToString();
            txtProyecto.Text = PROYECTOGV.GetNombre(nPSN);
        }
    }
    protected void ObtenerDatosCabecera(int nReferencia)
    {
        //Nueva nota
        USUARIOGV oUsuario = USUARIOGV.Obtener((int)Session["UsuarioActual"]);
        if (oUsuario.oOficinaLiquidadora == null)
        {
            throw new Exception("No se ha podido determinar la oficina liquidadora.");
        }

        //txtInteresado.Text = oUsuario.t314_idusuario.ToString("#,###") + " - " + oUsuario.Nombre;
        txtInteresado.Text = oUsuario.Nombre;
        hdnInteresado.Text = oUsuario.t314_idusuario.ToString();
        sNodoUsuario = oUsuario.t303_denominacion;
        txtEmpresa.Text = oUsuario.t313_denominacion;
        txtOficinaLiq.Text = oUsuario.oOficinaLiquidadora.t010_desoficina;

        if (oUsuario.t422_idmoneda != "" && oUsuario.t422_idmoneda != null) //Moneda por defecto a nivel de usuario
            cboMoneda.SelectedValue = oUsuario.t422_idmoneda.ToString();
        if (oUsuario.oDietaKm != null)
        {
            cldKMCO.InnerText = oUsuario.oDietaKm.t069_ick.ToString("N");
            cldDCCO.InnerText = oUsuario.oDietaKm.t069_icdc.ToString("N");
            cldMDCO.InnerText = oUsuario.oDietaKm.t069_icmd.ToString("N");
            cldDECO.InnerText = oUsuario.oDietaKm.t069_icde.ToString("N");
            cldDACO.InnerText = oUsuario.oDietaKm.t069_icda.ToString("N");
        }
        if (oUsuario.oTerritorio != null)
        {
            cldKMEX.InnerText = oUsuario.oTerritorio.T007_ITERK.ToString("N");
            cldDCEX.InnerText = oUsuario.oTerritorio.T007_ITERDC.ToString("N");
            cldMDEX.InnerText = oUsuario.oTerritorio.T007_ITERMD.ToString("N");
            cldDEEX.InnerText = oUsuario.oTerritorio.T007_ITERDE.ToString("N");
            cldDAEX.InnerText = oUsuario.oTerritorio.T007_ITERDA.ToString("N");
        }

        hdnOficinaBase.Text = (oUsuario.t010_idoficina_base.HasValue) ? oUsuario.t010_idoficina_base.ToString() : "";
        hdnOficinaLiquidadora.Text = (oUsuario.oOficinaLiquidadora != null) ? oUsuario.oOficinaLiquidadora.t010_idoficina.ToString() : "";
        hdnAutorresponsable.Text = (oUsuario.bAutorresponsable) ? "1" : "0";

        //1ºComprobar si el profesional tiene más de una empresa.
        setEmpresaTerritorio((int)Session["UsuarioActual"]);
    }
    protected void ObtenerDatosGastos(int nReferencia)
    {
        divFondoCatalogoGastos.InnerHtml = POSICIONGV.CatalogoGastos(nReferencia, bLectura);
    }

    protected void setEmpresaTerritorio(int nUsuario)
    {
        //1ºComprobar si el profesional tiene más de una empresa.
        ArrayList aEmpresas = USUARIOGV.ObtenerEmpresasTerritorios(nUsuario);
        txtEmpresa.Text = "";
        hdnIDEmpresa.Text = "";
        if (aEmpresas.Count > 1)
        {
            txtEmpresa.Style.Add("display", "none");
            cboEmpresa.Style.Add("display", "block");

            ListItem oLI = null;
            for (int i = 0; i < aEmpresas.Count; i++)
            {
                oLI = new ListItem(((string[])aEmpresas[i])[1], ((string[])aEmpresas[i])[0]);
                oLI.Attributes.Add("idterritorio", ((string[])aEmpresas[i])[2]);
                oLI.Attributes.Add("nomterritorio", ((string[])aEmpresas[i])[3]);
                oLI.Attributes.Add("ITERDC", ((string[])aEmpresas[i])[4]);
                oLI.Attributes.Add("ITERMD", ((string[])aEmpresas[i])[5]);
                oLI.Attributes.Add("ITERDA", ((string[])aEmpresas[i])[6]);
                oLI.Attributes.Add("ITERDE", ((string[])aEmpresas[i])[7]);
                oLI.Attributes.Add("ITERK", ((string[])aEmpresas[i])[8]);

                cboEmpresa.Items.Add(oLI);

                if (cboEmpresa.Items.Count == 1 ||
                    (((string[])aEmpresas[i])[0] == "1" && nID == 0)
                    )
                {
                    cboEmpresa.SelectedValue = ((string[])aEmpresas[i])[0];
                    hdnIDEmpresa.Text = ((string[])aEmpresas[i])[0];
                    hdnIDTerritorio.Text = ((string[])aEmpresas[i])[2];
                    lblTerritorio.Text = ((string[])aEmpresas[i])[3];
                }
            }
        }
        else if (aEmpresas.Count == 1)
        {
            txtEmpresa.Style.Add("display", "block");
            cboEmpresa.Style.Add("display", "none");
            hdnIDEmpresa.Text = ((string[])aEmpresas[0])[0];
            txtEmpresa.Text = ((string[])aEmpresas[0])[1];
            hdnIDTerritorio.Text = ((string[])aEmpresas[0])[2];
            lblTerritorio.Text = ((string[])aEmpresas[0])[3];
            cldKMEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[8]).ToString("N");
            cldDCEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[4]).ToString("N");
            cldMDEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[5]).ToString("N");
            cldDEEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[7]).ToString("N");
            cldDAEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[6]).ToString("N");
        }
    }

}


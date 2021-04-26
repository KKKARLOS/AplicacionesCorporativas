using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using EO.Web;
using System.Text;
using GASVI.BLL;

public partial class Capa_Presentacion_BonoTransporte_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nID = 0;
    public string sEstado = "", sMsgRecuperada = "", sOrigen = "";
    public string sDiaLimiteContAnoAnterior = "1", sDiaLimiteContMesAnterior = "1", sNodoUsuario = "";
    public bool bLectura = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;

            Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FicherosCSS.Add("Capa_Presentacion/BonoTransporte/css/BonoTransporte.css");

            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            if (Request.QueryString["se"] != null)
                sEstado = Utilidades.decodpar(Request.QueryString["se"].ToString());
            if (Request.QueryString["so"] != null)
                sOrigen = Utilidades.decodpar(Request.QueryString["so"].ToString());
            if (Request.QueryString.Get("ni") != null)
                nID = int.Parse(Utilidades.decodpar(Request.QueryString.Get("ni").ToString()));

            lblBeneficiario.InnerText = (Session["GVT_SEXO"].ToString() == "V") ? "Beneficiario" : "Beneficiaria";
            cboEmpresa.Style.Add("display", "none");

            switch (sOrigen)
            {
                case "":
                    switch (sEstado)
                    {
                        case "A":// Aprobada
                        case "C":// Contabilizada
                        case "L":// Aceptada
                        case "N":// Notificada
                        case "S":// Pagada
                        case "X": // Anulada
                            Master.nBotonera = 8;
                            bLectura = true;
                            Master.TituloPagina = "Detalle de bono de transporte";
                            imgAM.Style.Add("display", "none");
                            imgSM.Style.Add("display", "none");
                            break;
                        case "B":// No aprobada
                        case "O":// No aceptada
                        //case "P":// Aparcada
                        case "R":// Recuperada
                            Master.nBotonera = 6;
                            Master.TituloPagina = "Detalle de bono de transporte";
                            break;
                        case "T":// Tramitada
                            Master.nBotonera = 5;
                            bLectura = true;
                            Master.TituloPagina = "Detalle de bono de transporte";
                            imgAM.Style.Add("display", "none");
                            imgSM.Style.Add("display", "none");
                            break;
                        default:
                            Master.nBotonera = 11;
                            Master.TituloPagina = "Nuevo bono de transporte";
                            break;
                    }
                    break;
                case "APROBAR":
                    bLectura = true;
                    Master.nBotonera = 13;
                    imgAM.Style.Add("display", "none");
                    imgSM.Style.Add("display", "none");
                    break;
                case "ACEPTAR":
                    bLectura = true;
                    Master.nBotonera = 15;
                    imgAM.Style.Add("display", "none");
                    imgSM.Style.Add("display", "none");
                    //paddingLeft = "3px";
                    DIALIMITECONTABILIZACIONGV oDL = DIALIMITECONTABILIZACIONGV.Obtener();
                    sDiaLimiteContAnoAnterior = oDL.t670_dialimitecontanoanterior.ToString();
                    sDiaLimiteContMesAnterior = oDL.t670_dialimitecontmesanterior.ToString();
                    break;
                case "CONSULTA":
                    bLectura = true;
                    Master.nBotonera = 8;
                    break;
            }

            if (sEstado == "T" || sEstado == "N" || sEstado == "A")
            {
                imgManoVisador.Style.Add("visibility", "visible");
                if (sEstado == "T")
                    imgManoVisador.ToolTip = "Muestra quien o quienes han de aprobar la solicitud.";
                else
                    imgManoVisador.ToolTip = "Muestra quien o quienes han de aceptar la solicitud.";
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    //obtenerMonedas();

                    if (nID == 0 || Request.QueryString.Get("ni") == null)
                    {
                        string sDatosIniciales = NuevoBonoTransporte.obtenerDatosIniciales(Session["GVT_USUARIOSUPER"].ToString(), Fechas.FechaAAnnomes(DateTime.Today).ToString());
                        string[] aDatosIniciales = null;
                        if (sDatosIniciales != "")
                        {
                            aDatosIniciales = Regex.Split(sDatosIniciales, "#sFin#");
                            if (aDatosIniciales.Length == 1)
                            {
                                string[] aElem = Regex.Split(aDatosIniciales[0], "#sCad#");
                                hdnIdBono.Text = aElem[0];
                                txtBono.Text = aElem[1];
                                txtImporte.Text = decimal.Parse(aElem[2]).ToString("N");
                                hdnImporte.Text = aElem[2];
                                lblMoneda.InnerText = aElem[5];
                                lblMoneda.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' /> Moneda] body=[" + aElem[6] + "] hideselects=[off]");
                                hdnMoneda.Text = aElem[5];
                                hdnIdProyectoSubNodo.Text = aElem[3];
                                hdnInteresado.Text = aElem[4];
                                txtProyecto.Text = aElem[7] + " - " + aElem[8];
                            }
                        }
                    }
                    else
                    { //if (Request.QueryString.Get("ni") != null)
                        txtReferencia.Text = nID.ToString();
                    }
                    if (bLectura)
                    {
                        ModoLectura.Poner(this.Controls);
                    }
                    ObtenerDatosCabeceraBono(nID);               
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al cargar los datos ", ex);
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
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("tramitar"):
                try
                {
                    sResultado += "OK@#@" + NuevoBonoTransporte.TramitarBono(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6],
                                                                            aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12]);
                }
                catch (Exception ex)
                {
                    //sResultado += "Error@#@" + Errores.mostrarError("Error al tramitar el bono de transporte.", ex);
                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "Error@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al tramitar el bono de transporte.", ex);
                }
                break;
            case ("bono"):
                try
                {
                    sResultado += "OK@#@" + NuevoBonoTransporte.obtenerDatosIniciales(aArgs[3], aArgs[1]) + "@#@" + NuevoBonoTransporte.obtenerCabeceraGVBono(aArgs[2], aArgs[3], aArgs[1].Substring(aArgs[1].Length - 2, 2));
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los datos de los bonos.", ex);
                }
                break;
            case ("getOtrosDatos"):
                try
                {
                    sResultado += "OK@#@" + NuevoBonoTransporte.ObtenerHistorial(aArgs[2]) + "///" + NuevoBonoTransporte.obtenerCabeceraGVBono(aArgs[2], aArgs[3], aArgs[1]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener el historial.", ex);
                }
                break;
            case ("aprobar"):
                try
                {
                    CABECERAGV.Aprobar(aArgs[1]);
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al aprobar la solicitud.", ex);
                }
                break;
            case ("noaprobar"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.NoAprobar(int.Parse(aArgs[1]), aArgs[2]).ToString();
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al no aprobar la solicitud estándar.", ex);
                }
                break;
            case ("aceptar"):
                try
                {
                    CABECERAGV.Aceptar(aArgs[1]);
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    //sResultado += "Error@#@" + Errores.mostrarError("Error al aceptar la solicitud.", ex);
                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "Error@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al aceptar la solicitud.", ex);
                }
                break;
            case ("noaceptar"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.NoAceptar(int.Parse(aArgs[1]), aArgs[2]).ToString();
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al no aceptar la solicitud estándar.", ex);
                }
                break;
            case ("anular"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.Anular(int.Parse(aArgs[1]), aArgs[2]).ToString();
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al anular la solicitud estándar.", ex);
                }
                break;
            case ("getHistoria"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.ObtenerHistorial(int.Parse(aArgs[1]));
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener el historial.", ex);
                }
                break;
            case ("getDatosBeneficiario"):
                try
                {
                    sResultado += "OK@#@" + ObtenerDatosBeneficiario(int.Parse(aArgs[1]));
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los datos del beneficiario.", ex);
                }
                break;
            case ("getCCMotivo"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.ObtenerCentroCosteMotivo(aArgs[1], aArgs[2], aArgs[3]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los datos del beneficiario.", ex);
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
            case "recuperar":
                Recuperar();
                break;
        }
    }
    protected void Regresar()
    {
        try
        {
            string strURL = HistorialNavegacion.Leer();
            //Response.Redirect(strURL, true);
            int nPos = strURL.IndexOf("?");
            if (nPos == -1)
                Response.Redirect(strURL, true);
            else
                Response.Redirect(strURL.Substring(0, nPos), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }

    protected void ObtenerDatosCabeceraBono(int nReferencia)
    {
        if (nReferencia > 0)
        {
            CABECERAGV oCab = CABECERAGV.ObtenerDatosCabeceraBono(nReferencia);
            hdnReferencia.Text = oCab.t420_idreferencia.ToString();
            hdnInteresado.Text = oCab.t314_idusuario_interesado.ToString();
            sNodoUsuario = oCab.t303_denominacion_beneficiario;
            hdnOficinaLiquidadora.Text = oCab.t010_idoficina.ToString();
            lblBeneficiario.InnerText = (oCab.t001_sexo_interesado == "V") ? "Beneficiario" : "Beneficiaria";
            imgEstado.ImageUrl = "~/Images/imgEstado2" + oCab.t431_idestado + ".gif";
            hdnEstado.Text = oCab.t431_idestado;
            hdnEstadoAnterior.Text = oCab.t431_idestado;
            txtInteresado.Text = oCab.Interesado;
            txtReferencia.Text = oCab.t420_idreferencia.ToString("#,###");
            //cboMoneda.SelectedValue = oCab.t422_idmoneda;
            //txtEmpresa.Text = oCab.t313_denominacion;
            txtImporte.Text = oCab.t420_importe.ToString("N");
            lblMoneda.InnerText = oCab.t422_idmoneda;
            hdnMoneda.Text = oCab.t422_idmoneda;
            lblMoneda.Attributes.Add("desMoneda", oCab.t422_denominacion);
            hdnIdProyectoSubNodo.Text = (oCab.t305_idproyectosubnodo.HasValue) ? oCab.t305_idproyectosubnodo.ToString() : "";
            txtObservacionesBono.Text = oCab.t420_comentarionota;
            hdnAnotacionesPersonales.Text = Utilidades.escape(oCab.t420_anotaciones);
            if (oCab.t001_idficepi_interesado != (int)Session["GVT_IDFICEPI_ENTRADA"])
                divAnotaciones.Style.Add("visibility", "hidden");

            if (oCab.t431_idestado == "B"   //No aprobada
                || oCab.t431_idestado == "O"//No aceptada
                || oCab.t431_idestado == "R"//Recuperada
                )
            {
                setEmpresaTerritorio(oCab.t314_idusuario_interesado);
            }
            else
            {
                txtEmpresa.Text = oCab.t313_denominacion;
                hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
                hdnIDTerritorio.Text = oCab.t007_idterrfis.ToString();
            }

            //hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
            //hdnIdTerritorio.Text = oCab.t007_idterrfis.ToString();

            hdnImporte.Text = oCab.t420_importe.ToString("N");
            txtBono.Text = oCab.t655_denominacion.ToString();
            hdnIdBono.Text = oCab.t655_idBono.ToString();
            hdnFecha.Text = oCab.t420_anomesbono.ToString();
            txtProyecto.Text = (oCab.t301_idproyecto.HasValue) ? ((int)oCab.t301_idproyecto).ToString("#,###") + " - " + oCab.t301_denominacion.ToString() : " - ";

            txtFecContabilizacion.Text = (oCab.t420_fcontabilizacion.HasValue) ? ((DateTime)oCab.t420_fcontabilizacion).ToShortDateString() : "";
            txtTipoCambio.Text = (oCab.t431_idestado == "A") ? "" : oCab.t420_tipocambio.ToString("#,##0.0000");

            if (oCab.t431_idestado == "L"
                || oCab.t431_idestado == "C"
                || oCab.t431_idestado == "S")
            {
                string sToolTip = "";
                if (User.IsInRole("A"))
                    sToolTip += "<label style='width:90px;'>Centro de coste:</label>" + oCab.t175_idcc_solicitud + " - " + oCab.t175_denominacion_solicitud + "<br>";
                sToolTip += "<label style='width:140px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + ":</label>" + oCab.t303_denominacion_solicitud;
                txtProyecto.ToolTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sToolTip + "] hideselects=[off]";
            }

        }
        else //Nueva nota
        {
            #region
            USUARIO oUsuario = USUARIO.Obtener((int)Session["GVT_USUARIOSUPER"]);

            txtInteresado.Text = oUsuario.Nombre;
            hdnInteresado.Text = oUsuario.t314_idusuario.ToString();
            sNodoUsuario = oUsuario.t303_denominacion;
            //txtEmpresa.Text = oUsuario.t313_denominacion;
            hdnOficinaLiquidadora.Text = (oUsuario.oOficinaLiquidadora != null) ? oUsuario.oOficinaLiquidadora.t010_idoficina.ToString() : "";
            setEmpresaTerritorio((int)Session["GVT_USUARIOSUPER"]);

            //hdnIDEmpresa.Text = oUsuario.t313_idempresa.ToString();
            //hdnIdTerritorio.Text = oUsuario.oTerritorio.T007_IDTERRFIS.ToString();
            #endregion
        }
    }
    protected void Recuperar()
    {
        int nBonosRecuperadas = CABECERAGV.RecuperarBono(int.Parse(hdnReferencia.Text));
        if (nBonosRecuperadas == 0)
        {
            sMsgRecuperada = "Durante su intervención, el estado de la solicitud de bono ha variado y no se permite su recuperación.";
        }
        else
        {
            Master.nBotonera = 6;
            Master.CargarBotonera();
            cboEmpresa.Enabled = true;
            txtObservacionesBono.ReadOnly = false;
            imgManoVisador.Style.Add("visibility", "hidden");
            bLectura = false;
            ObtenerDatosCabeceraBono(int.Parse(hdnReferencia.Text));
        }
    }
    protected void setEmpresaTerritorio(int nUsuario)
    {
        //1ºComprobar si el profesional tiene más de una empresa.
        ArrayList aEmpresas = Profesional.ObtenerEmpresasTerritorios(nUsuario);
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

                cboEmpresa.Items.Add(oLI);

                if (cboEmpresa.Items.Count == 1 ||
                    (((string[])aEmpresas[i])[0] == "1" && nID == 0)
                    )
                {
                    cboEmpresa.SelectedValue = ((string[])aEmpresas[i])[0];
                    hdnIDEmpresa.Text = ((string[])aEmpresas[i])[0];
                    hdnIDTerritorio.Text = ((string[])aEmpresas[i])[2];
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
        }
    }
    protected string ObtenerDatosBeneficiario(int nUsuario)
    {
        StringBuilder sb = new StringBuilder();

        USUARIO oUsuario = USUARIO.Obtener(nUsuario);
        sb.Append(oUsuario.Nombre + "{sep}");
        sb.Append(oUsuario.t314_idusuario.ToString() + "{sep}");
        sb.Append(oUsuario.t313_denominacion + "{sep}");
        //sb.Append(oUsuario.oOficinaLiquidadora.t010_desoficina + "{sep}");
        sb.Append(oUsuario.t422_idmoneda.ToString() + "{sepdatos}");
        //sb.Append(oUsuario.oDietaKm.t069_ick.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oDietaKm.t069_icdc.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oDietaKm.t069_icmd.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oDietaKm.t069_icde.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oDietaKm.t069_icda.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oTerritorio.T007_ITERK.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oTerritorio.T007_ITERDC.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oTerritorio.T007_ITERMD.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oTerritorio.T007_ITERDE.ToString("N") + "{sep}");
        //sb.Append(oUsuario.oTerritorio.T007_ITERDA.ToString("N") + "{sep}");
        //sb.Append((oUsuario.t010_idoficina_base.HasValue) ? oUsuario.t010_idoficina_base.ToString() + "{sep}" : "" + "{sep}");
        //sb.Append(oUsuario.oOficinaLiquidadora.t010_idoficina.ToString() + "{sep}");
        //sb.Append((oUsuario.bAutorresponsable) ? "1" + "{sepdatos}" : "0" + "{sepdatos}");

        //List<ElementoLista> aMotivos = MOTIVO.ObtenerMotivos(nUsuario, "");
        //for (int i = 0; i < aMotivos.Count; i++)
        //{
        //    sb.Append(aMotivos[i].sValor + "//" + aMotivos[i].sDenominacion + "{sep}");
        //}
        //sb.Append("{sepdatos}");

        //if (Profesional.bPerteneceVariasEmpresas(nUsuario))
        //{
        ArrayList aEmpresas = Profesional.ObtenerEmpresasTerritorios(nUsuario);
        for (int i = 0; i < aEmpresas.Count; i++)
        {
            sb.Append(((string[])aEmpresas[i])[0] + "//" //t313_idempresa
                    + ((string[])aEmpresas[i])[1] + "//" //t313_denominacion
                    + ((string[])aEmpresas[i])[2] + "//" //t007_idterrfis
                    + ((string[])aEmpresas[i])[3] + "//" //t007_nomterrfis
                    + ((string[])aEmpresas[i])[4] + "//" //T007_ITERDC
                    + ((string[])aEmpresas[i])[5] + "//" //T007_ITERMD
                    + ((string[])aEmpresas[i])[6] + "//" //T007_ITERDA
                    + ((string[])aEmpresas[i])[7] + "//" //T007_ITERDE
                    + ((string[])aEmpresas[i])[8] + "{sep}"); //T007_ITERK
        }
        //}

        return sb.ToString();
    }

}


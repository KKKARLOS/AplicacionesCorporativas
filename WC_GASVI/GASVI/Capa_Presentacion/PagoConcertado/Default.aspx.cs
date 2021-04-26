using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using EO.Web;
using System.Text;
using System.Collections;
using GASVI.BLL;

public partial class Capa_Presentacion_PagoConcertado_Default : System.Web.UI.Page, ICallbackEventHandler
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
            Master.FicherosCSS.Add("Capa_Presentacion/PagoConcertado/css/PagoConcertado.css");

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
                            Master.TituloPagina = "Detalle del pago concertado";
                            break;
                        case "B":// No aprobada
                        case "O":// No aceptada
                        //case "P":// Aparcada
                        case "R":// Recuperada
                            Master.nBotonera = 6;
                            Master.TituloPagina = "Detalle del pago concertado";
                            break;
                        case "T":// Tramitada
                            Master.nBotonera = 5;
                            bLectura = true;
                            Master.TituloPagina = "Detalle del pago concertado";
                            break;
                        default:
                            Master.nBotonera = 11;
                            Master.TituloPagina = "Nuevo pago concertado";
                            break;
                    }
                    if (sEstado != "" && sEstado != "R") {
                        if (sEstado != "T")
                        {
                            tdlblAcuerdo.Style.Add("visibility", "visible");
                            tdtxtAcuerdo.Style.Add("visibility", "visible");
                        }
                        lblProy.Attributes.Add("onclick", "");
                        lblProy.Attributes.Add("class", "texto");
                        lblAcuerdo.Attributes.Add("onclick", "");
                        lblAcuerdo.Attributes.Add("class", "texto");
                    }
                    else if (sEstado == "R") {
                        lblProy.Attributes.Add("onclick", "getPE()");
                        lblProy.Attributes.Add("class", "enlace");
                        tdlblAcuerdo.Style.Add("visibility", "hidden");
                        tdtxtAcuerdo.Style.Add("visibility", "hidden");
                    }
                    break;
                case "APROBAR":
                    bLectura = true;
                    Master.nBotonera = 13;
                    tdlblAcuerdo.Style.Add("visibility", "visible");
                    tdtxtAcuerdo.Style.Add("visibility", "visible");
                    lblProy.Attributes.Add("onclick", "");
                    lblProy.Attributes.Add("class", "texto");
                    break;
                case "ACEPTAR":
                    bLectura = true;
                    Master.nBotonera = 15;
                    DIALIMITECONTABILIZACIONGV oDL = DIALIMITECONTABILIZACIONGV.Obtener();
                    sDiaLimiteContAnoAnterior = oDL.t670_dialimitecontanoanterior.ToString();
                    sDiaLimiteContMesAnterior = oDL.t670_dialimitecontmesanterior.ToString();
                    tdlblAcuerdo.Style.Add("visibility", "visible");
                    tdtxtAcuerdo.Style.Add("visibility", "visible");
                    lblProy.Attributes.Add("onclick", "");
                    lblProy.Attributes.Add("class", "texto");
                    lblAcuerdo.Attributes.Add("onclick", "");
                    lblAcuerdo.Attributes.Add("class", "texto");
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
                    obtenerMotivos((int)Session["GVT_USUARIOSUPER"], sEstado);
                    obtenerMonedas();

                    hdnCCIberper.Text = Session["GVT_CCIBERPER"].ToString();
                    if ((int)Session["GVT_CCIBERPER"] > 1)
                    {
                        lblNodo.Attributes.Add("class", "enlace");
                        lblNodo.Attributes.Add("onclick", "getCCIberper()");
                    }
                    
                    if (bLectura)
                    {
                        ModoLectura.Poner(this.Controls);
                    }
                    ObtenerDatosCabeceraPago(nID);
                    //if (nID > 0) tabOtros.DefaultStyle.Add("Display", "block");

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
                    sResultado += "OK@#@" + CABECERAGV.TramitarPagoConcertado(aArgs[1]);
                }
                catch (Exception ex)
                {
                    //sResultado += "Error@#@" + Errores.mostrarError("Error al tramitar el bono de transporte.", ex);
                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "Error@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al tramitar el pago concertado.", ex);
                }
                break;
            case ("getOtrosDatos"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.ObtenerHistorial((aArgs[1] == "") ? 0 : int.Parse(aArgs[1])) + "///" + ACUERDOGV.obtenerOtrosPagos(int.Parse(aArgs[2]), (aArgs[1] == "") ? 0 : int.Parse(aArgs[1]));
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener el historial.", ex);
                }
                break;
            
            case ("aprobar"):
                try
                {
                    CABECERAGV.UpdateAcuerdo(aArgs[1], aArgs[2]);
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
            case ("getMotivosNodo"):
                try
                {
                    sResultado += "OK@#@" + ObtenerMotivosNodoBeneficiario(int.Parse(aArgs[1]), aArgs[2], int.Parse(aArgs[3]));
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

    protected void ObtenerDatosCabeceraPago(int nReferencia)
    {
        if (nReferencia > 0)
        {
            CABECERAGV oCab = CABECERAGV.ObtenerDatosCabeceraPago(nReferencia);
            hdnReferencia.Text = oCab.t420_idreferencia.ToString();
            hdnInteresado.Text = oCab.t314_idusuario_interesado.ToString();
            hdnNodoBeneficiario.Text = oCab.t303_idnodo_beneficiario.ToString();
            sNodoUsuario = oCab.t303_denominacion_beneficiario;
            hdnOficinaLiquidadora.Text = oCab.t010_idoficina.ToString();
            lblBeneficiario.InnerText = (oCab.t001_sexo_interesado == "V") ? "Beneficiario" : "Beneficiaria";
            imgEstado.ImageUrl = "~/Images/imgEstado2" + oCab.t431_idestado + ".gif";
            hdnEstado.Text = oCab.t431_idestado;
            hdnEstadoAnterior.Text = oCab.t431_idestado;
            txtInteresado.Text = oCab.Interesado;
            txtReferencia.Text = oCab.t420_idreferencia.ToString("#,###");
            txtEmpresa.Text = oCab.t313_denominacion;
            hdnIdProyectoSubNodo.Text = (oCab.t305_idproyectosubnodo.HasValue) ? oCab.t305_idproyectosubnodo.ToString() : "";
            txtProyecto.Text = (oCab.t305_idproyectosubnodo.HasValue) ? ((int)oCab.t301_idproyecto).ToString("#,###") +" - "+ oCab.t301_denominacion.ToString() : "";
            txtObservaciones.Text = oCab.t420_comentarionota;
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
                hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
                hdnIDTerritorio.Text = oCab.t007_idterrfis.ToString();
            }

            //hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
            //hdnIdTerritorio.Text = oCab.t007_idterrfis.ToString();

            hdnImporte.Text = oCab.t420_importe.ToString("N");
            txtImporte.Text = hdnImporte.Text;
            txtAcuerdo.Text = (oCab.t666_idacuerdogv.HasValue) ? oCab.t666_denominacion.ToString() : "";
            hdnIdAcuerdoGV.Text = (oCab.t666_idacuerdogv.HasValue) ? oCab.t666_idacuerdogv.ToString() : "";
            txtFecContabilizacion.Text = (oCab.t420_fcontabilizacion.HasValue) ? ((DateTime)oCab.t420_fcontabilizacion).ToShortDateString() : "";
            txtTipoCambio.Text = (oCab.t431_idestado == "A") ? "" : oCab.t420_tipocambio.ToString("#,##0.0000");
            hdnCentroCoste.Text = oCab.t175_idcc;
            hdnNodoCentroCoste.Text = oCab.t303_idnodo_solicitud.ToString();
            txtDesNodo.Text = oCab.t303_denominacion_solicitud;

            cboMotivo.SelectedValue = oCab.t423_idmotivo.ToString();
            cboMoneda.SelectedValue = oCab.t422_idmoneda;
            hdnIdMonedaAC.Text = oCab.t422_idmoneda_acuerdo;

            if (oCab.t431_idestado == "L"
                || oCab.t431_idestado == "C"
                || oCab.t431_idestado == "S"
                || oCab.t175_idcc != "")
            {

                string sToolTip = "";
                if (User.IsInRole("A"))
                    sToolTip += "<label style='width:90px;'>Centro de coste:</label>" + oCab.t175_idcc_solicitud + " - " + oCab.t175_denominacion_solicitud + "<br>";
                if (oCab.t423_idmotivo == 1)
                {
                    sToolTip += "<label style='width:140px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + ":</label>" + oCab.t303_denominacion_solicitud;
                    txtProyecto.ToolTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sToolTip + "] hideselects=[off]";
                }
                else
                    txtDesNodo.ToolTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sToolTip + "] hideselects=[off]";
            }
        }
        else //Nueva nota
        {
            #region
            USUARIO oUsuario = USUARIO.Obtener((int)Session["GVT_USUARIOSUPER"]);

            txtInteresado.Text = oUsuario.Nombre;
            hdnInteresado.Text = oUsuario.t314_idusuario.ToString();
            hdnNodoBeneficiario.Text = oUsuario.t303_idnodo.ToString();
            sNodoUsuario = oUsuario.t303_denominacion;
            txtEmpresa.Text = oUsuario.t313_denominacion;
            hdnOficinaLiquidadora.Text = (oUsuario.oOficinaLiquidadora != null) ? oUsuario.oOficinaLiquidadora.t010_idoficina.ToString() : "";
            if (Session["GVT_MOTIVODEFECTO"] != null)
                cboMotivo.SelectedValue = Session["GVT_MOTIVODEFECTO"].ToString();

            setEmpresaTerritorio((int)Session["GVT_USUARIOSUPER"]);

            //hdnIDEmpresa.Text = oUsuario.t313_idempresa.ToString();
            //hdnIdTerritorio.Text = oUsuario.oTerritorio.T007_IDTERRFIS.ToString();
            if (oUsuario.t422_idmoneda != "" && oUsuario.t422_idmoneda != null) //Moneda por defecto a nivel de usuario
                cboMoneda.SelectedValue = oUsuario.t422_idmoneda.ToString();

            #endregion
        }
    }
    protected void Recuperar()
    {
        int nPagosRecuperadas = CABECERAGV.RecuperarPago(int.Parse(hdnReferencia.Text));
        if (nPagosRecuperadas == 0)
        {
            sMsgRecuperada = "Durante su intervención, el estado de la solicitud del pago ha variado y no se permite su recuperación.";
        }
        else
        {
            Master.nBotonera = 6;
            Master.CargarBotonera();
            bLectura = false;
            cboMotivo.Enabled = true;
            cboEmpresa.Enabled = true;
            cboMoneda.Enabled = true;
            txtImporte.ReadOnly = false;
            txtObservaciones.ReadOnly = false;
            imgManoVisador.Style.Add("visibility", "hidden");
            cboMotivo.Items.Clear();
            obtenerMotivos((int)Session["GVT_USUARIOSUPER"], "");
            //obtenerMonedas();
            ObtenerDatosCabeceraPago(int.Parse(hdnReferencia.Text));
        }
    }

    protected void obtenerMotivos(int nBeneficiario, string sEstado)
    {
        List<ElementoLista> oLista = MOTIVO.ObtenerMotivos(nBeneficiario, sEstado);
        ListItem oLI = null;
        foreach (ElementoLista oMotivo in oLista)
        {
            oLI = new ListItem(oMotivo.sDenominacion, oMotivo.sValor);
            oLI.Attributes.Add("idcencos", oMotivo.sDatoAux1);
            oLI.Attributes.Add("des_cencos", oMotivo.sDatoAux2);
            oLI.Attributes.Add("idnodo", oMotivo.sDatoAux3);
            oLI.Attributes.Add("des_nodo", oMotivo.sDatoAux4);
            cboMotivo.Items.Add(oLI);
        }
        cboMotivo.SelectedValue = "1";
    }

    protected void obtenerMonedas()
    {
        List<ElementoLista> oLista = MONEDA.ObtenerMonedas(true);
        ListItem oLI = null;
        foreach (ElementoLista oMoneda in oLista)
        {
            oLI = new ListItem(oMoneda.sDenominacion, oMoneda.sValor);
            //oLI.Attributes.Add("defecto", oMoneda.sDatoAux1);
            //if (oMoneda.sDatoAux1 == "1") oLI.Selected = true;
            //if (oMoneda.sValor == Session["GVT_MONEDADEFECTO"].ToString()) oLI.Selected = true;
            cboMoneda.Items.Add(oLI);
        }
    }
    protected void setEmpresaTerritorio(int nUsuario)
    {
        //1ºComprobar si el profesional tiene más de una empresa.
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
        sb.Append(oUsuario.t422_idmoneda.ToString() + "{sep}");
        sb.Append(oUsuario.nCCIberper.ToString() + "{sepdatos}");
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

        List<ElementoLista> aMotivos = MOTIVO.ObtenerMotivos(nUsuario, "");
        for (int i = 0; i < aMotivos.Count; i++)
        {
            sb.Append(aMotivos[i].sValor + "//" + aMotivos[i].sDenominacion + "//" + aMotivos[i].sDatoAux1 + "//" + aMotivos[i].sDatoAux2 + "//" + aMotivos[i].sDatoAux3 + "//" + aMotivos[i].sDatoAux4 + "{sep}");
        }
        sb.Append("{sepdatos}");

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
    protected string ObtenerMotivosNodoBeneficiario(int nBeneficiario, string sEstado, int t303_idnodo)
    {
        StringBuilder sb = new StringBuilder();

        List<ElementoLista> aMotivos = MOTIVO.ObtenerMotivosNodoBeneficiario(nBeneficiario, sEstado, t303_idnodo);
        for (int i = 0; i < aMotivos.Count; i++)
        {
            sb.Append(aMotivos[i].sValor + "//" + aMotivos[i].sDenominacion + "//" + aMotivos[i].sDatoAux1 + "//" + aMotivos[i].sDatoAux2 + "//" + aMotivos[i].sDatoAux3 + "//" + aMotivos[i].sDatoAux4 + "{sep}");
        }
        return sb.ToString();
    }

}


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
using EO.Web;
using System.Text;
using GASVI.BLL;

public partial class Capa_Presentacion_NotaEstandar_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nID = 0, idEmpresaNotaAparcada=0;
    public string sEstado = "", sTipo = "", sMsgRecuperada = "", sOrigen = "";
    public string sDiaLimiteContAnoAnterior = "1", sDiaLimiteContMesAnterior = "1", sNodoUsuario = "";
    public bool bLectura = false;

    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!Page.IsCallback)
        {
            //Master.nBotonera = 1;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Detalle de nota estándar";

            Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            if (Request.QueryString["ni"] != null)
                nID = int.Parse(Utilidades.decodpar(Request.QueryString["ni"].ToString()));
            if (Request.QueryString["se"] != null)
                sEstado = Utilidades.decodpar(Request.QueryString["se"].ToString());
            if (Request.QueryString["st"] != null)
                sTipo = Utilidades.decodpar(Request.QueryString["st"].ToString());
            if (Request.QueryString["so"] != null)
                sOrigen = Utilidades.decodpar(Request.QueryString["so"].ToString());

            lblBeneficiario.InnerText = (Session["GVT_SEXO"].ToString() == "V") ? "Beneficiario" : "Beneficiaria";
            cboEmpresa.Style.Add("display","none");
            //nID = 20;
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
                            break;
                        case "B":// No aprobada
                        case "O":// No aceptada
                            Master.nBotonera = 6;
                            break;
                        case "R":// Recuperada
                            Master.nBotonera = 17;
                            break;
                        case "P":// Aparcada
                            Master.nBotonera = 16;
                            break;
                        case "T":// Tramitada
                            Master.nBotonera = 5;
                            bLectura = true;
                            break;
                        default:
                            Master.nBotonera = 1;
                            break;
                    }
                    break;
                case "APROBAR":
                    bLectura = true;
                    Master.nBotonera = 13;
                    break;
                case "ACEPTAR":
                    bLectura = true;
                    Master.nBotonera = 15;
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
                if (sEstado == "T" || sEstado == "N")
                    imgManoVisador.ToolTip = "Muestra quién o quiénes han de aprobar la solicitud.";
                else
                    imgManoVisador.ToolTip = "Muestra quién o quiénes han de aceptar la solicitud.";
            }

            inicializarToolTips();

            if (sEstado == "" || sEstado == "R" || sEstado == "P")
                divDisposiciones.Style.Add("display", "block");
            else
                divDisposiciones.Style.Add("display", "none");

            if (!Page.IsPostBack)
            {
                bool bAvisoEmpresaNoVigente = false;
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
                    if (sEstado == "P")
                    {
                        bAvisoEmpresaNoVigente=ObtenerDatosCabeceraAparcadaEstandar(nID);
                        ObtenerDatosGastosAparcadaEstandar(nID);
                    }
                    else
                    {
                        if (bLectura)
                        {
                            ModoLectura.Poner(this.Controls);
                            imgCalculadora.Visible = false;
                            Calculadora.Visible = false;
                        }
                        ObtenerDatosCabecera(nID);
                        ObtenerDatosGastos(nID);
                    }
                    if (bAvisoEmpresaNoVigente)
                    {
                        string sDenEmpresa = GASVI.BLL.Profesional.GetNombreEmpresa(idEmpresaNotaAparcada);
                        this.hdnMsg.Value = "La nota se aparcó con la empresa " + sDenEmpresa + " y el interesado ya no pertenece a la misma";
                    }
                }
                catch (Exception ex)
                {
                    //if (ex.Message.Length>15 && ex.Message.Substring(0,15)=="La nota se apar")
                    //    Master.sErrores = Errores.mostrarError("Error al cargar la nota aparcada ", ex, false);
                    //else
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
        string sResultado = "", sCad = "";
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
                    sResultado += "OK@#@" + CABECERAGV.TramitarNotaEstandar(aArgs[1], aArgs[2]);
                }
                catch (Exception ex)
                {
                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "Error@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al tramitar la nota estándar.", ex);
                }
                break;
            case ("aparcar"):
                try
                {
                    sResultado += "OK@#@" + CABECERAAPARCADA_NEGV.AparcarNotaEstandar(aArgs[1], aArgs[2]);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Solicitud aparcada no existente")
                        sResultado += "OK@#@" + ex.Message;
                    else
                        sResultado += "Error@#@" + Errores.mostrarError("Error al aparcar la nota estándar.", ex);
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
                            if (aArgs[3] != "P") sResultado += "OK@#@" + aArgs[1] + "@#@" + CABECERAGV.ObtenerHistorial((aArgs[2] == "") ? 0 : int.Parse(aArgs[2]));
                            else sResultado += "OK@#@" + aArgs[1] + "@#@";
                        }
                        catch (Exception ex)
                        {
                            sResultado += "Error@#@" + aArgs[1] + "@#@" + Errores.mostrarError("Error al obtener el historial.", ex);
                        }

                        break;
                }
                break;
            case ("eliminar"):
                try
                {
                    CABECERAAPARCADA_NEGV.Eliminar(int.Parse(aArgs[1]));
                    sResultado += "OK@#@";
                 }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al eliminar la solicitud estándar.", ex);
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
            case ("getDatosEmpresas"):
                try
                {
                    sResultado += "OK@#@" + ObtenerDatosEmpresas(int.Parse(aArgs[1]));
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

    protected void ObtenerDatosCabecera(int nReferencia)
    {
        if (nReferencia > 0)
        {
            #region solicitud existente
            CABECERAGV oCab = CABECERAGV.ObtenerDatosCabecera(nReferencia);
            hdnReferencia.Text = oCab.t420_idreferencia.ToString();
            lblBeneficiario.InnerText = (oCab.t001_sexo_interesado == "V") ? "Beneficiario" : "Beneficiaria";
            hdnInteresado.Text = oCab.t314_idusuario_interesado.ToString();
            hdnNodoBeneficiario.Text = oCab.t303_idnodo_beneficiario.ToString();
            sNodoUsuario = oCab.t303_denominacion_beneficiario;
            hdnOficinaLiquidadora.Text = oCab.t010_idoficina.ToString();
            imgEstado.ImageUrl = "~/Images/imgEstado"+ oCab.t431_idestado +".gif";
            hdnEstado.Text = oCab.t431_idestado;
            hdnEstadoAnterior.Text = oCab.t431_idestado;
            txtInteresado.Text = oCab.Interesado;
            txtReferencia.Text = oCab.t420_idreferencia.ToString("#,###");
            txtConcepto.Text = oCab.t420_concepto;
            txtEmpresa.Text = oCab.t313_denominacion;
            cboMotivo.SelectedValue = oCab.t423_idmotivo.ToString();
            txtOficinaLiq.Text = oCab.t010_desoficina;
            hdnIdProyectoSubNodo.Text = (oCab.t305_idproyectosubnodo.HasValue) ? oCab.t305_idproyectosubnodo.ToString() : "";
            txtProyecto.Text = (oCab.t305_idproyectosubnodo.HasValue) ? ((int)oCab.t301_idproyecto).ToString("#,###") + " - " + oCab.t301_denominacion : "";
            cboMoneda.SelectedValue = oCab.t422_idmoneda.ToString();
            if (oCab.t420_justificantes.HasValue)
            {
                if ((bool)oCab.t420_justificantes) rdbJustificantes.Items[0].Selected = true;
                else rdbJustificantes.Items[1].Selected = true;
            }
            txtObservacionesNota.Text = oCab.t420_comentarionota;
            hdnAnotacionesPersonales.Text = (oCab.t420_anotaciones == "") ? "&nbsp;" : Utilidades.escape(oCab.t420_anotaciones);
            //hdnAnotacionesPersonales.Text = Utilidades.escape(oCab.t420_anotaciones);
            if (oCab.t001_idficepi_interesado != (int)Session["GVT_IDFICEPI_ENTRADA"])
                divAnotaciones.Style.Add("visibility", "hidden");

            hdnOficinaBase.Text = (oCab.t010_idoficina_base.HasValue) ? oCab.t010_idoficina_base.ToString() : "";

            if (oCab.t431_idestado == "B"   //No aprobada
                || oCab.t431_idestado == "O"//No aceptada
                || oCab.t431_idestado == "R"//Recuperada
                || oCab.t431_idestado == "P"//Aparcada
                )
            {
                int idEmpresaDefecto = GASVI.DAL.Configuracion.GetEmpresaDefecto(null, oCab.t001_codred);
                setEmpresaTerritorio(oCab.t314_idusuario_interesado, oCab.t001_idficepi_interesado, oCab.Interesado,
                                     idEmpresaDefecto, oCab.t313_idempresa);

            }
            else
            {
                hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
                hdnIDTerritorio.Text = oCab.t007_idterrfis.ToString();
                lblTerritorio.Text = oCab.t007_nomterrfis;
            }

            txtFecAnticipo.Text = (oCab.t420_fanticipo.HasValue) ? ((DateTime)oCab.t420_fanticipo).ToShortDateString() : "";
            txtImpAnticipo.Text = (oCab.t420_importeanticipo>0)? oCab.t420_importeanticipo.ToString("N"):"";
            txtOficinaAnticipo.Text = oCab.t420_lugaranticipo;
            txtFecDevolucion.Text = (oCab.t420_fdevolucion.HasValue) ? ((DateTime)oCab.t420_fdevolucion).ToShortDateString() : "";
            txtImpDevolucion.Text = (oCab.t420_importedevolucion>0)? oCab.t420_importedevolucion.ToString("N"):"";
            txtOficinaDevolucion.Text = oCab.t420_lugardevolucion;
            txtAclaracionesAnticipos.Text = oCab.t420_aclaracionesanticipo;

            txtPagadoTransporte.Text = (oCab.t420_pagadotransporte > 0) ? oCab.t420_pagadotransporte.ToString("N") : "";
            txtPagadoHotel.Text = (oCab.t420_pagadohotel > 0) ? oCab.t420_pagadohotel.ToString("N") : "";
            txtPagadoOtros.Text = (oCab.t420_pagadootros > 0) ? oCab.t420_pagadootros.ToString("N") : "";
            txtAclaracionesPagado.Text = oCab.t420_aclaracionepagado;

            cldKMCO.InnerText = oCab.t420_impkmco.ToString("N");
            cldDCCO.InnerText = oCab.t420_impdico.ToString("N");
            cldMDCO.InnerText = oCab.t420_impmdco.ToString("N");
            cldDECO.InnerText = oCab.t420_impdeco.ToString("N");
            cldDACO.InnerText = oCab.t420_impalco.ToString("N");
            cldKMEX.InnerText = oCab.t420_impkmex.ToString("N");
            cldDCEX.InnerText = oCab.t420_impdiex.ToString("N");
            cldMDEX.InnerText = oCab.t420_impmdex.ToString("N");
            cldDEEX.InnerText = oCab.t420_impdeex.ToString("N");
            cldDAEX.InnerText = oCab.t420_impalex.ToString("N");
            hdnAutorresponsable.Text = (oCab.bAutorresponsable) ? "1" : "0";

            txtFecContabilizacion.Text = (oCab.t420_fcontabilizacion.HasValue) ? ((DateTime)oCab.t420_fcontabilizacion).ToShortDateString() : "";
            txtTipoCambio.Text = (oCab.t431_idestado == "A")? "":oCab.t420_tipocambio.ToString("#,##0.0000");
            hdnIDLote.Text = (oCab.t420_idreferencia_lote.HasValue) ? oCab.t420_idreferencia_lote.ToString() : "";
            hdnCentroCoste.Text = oCab.t175_idcc;
            hdnNodoCentroCoste.Text = oCab.t303_idnodo_solicitud.ToString();
            txtDesNodo.Text = oCab.t303_denominacion_solicitud;
            hdnCCIberper.Text = oCab.nCCIberper.ToString();

            if (oCab.t431_idestado == "A"
                && oCab.t420_idreferencia_lote.HasValue
                && Request.QueryString["so"] != null
                && Utilidades.decodpar(Request.QueryString["so"].ToString()) == "ACEPTAR"
                )
            {
                imgLote.Style.Add("visibility", "visible");
            }

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
            #endregion
        }
        else //
        {
            #region nueva solicitud
            USUARIO oUsuario = USUARIO.Obtener((int)Session["GVT_USUARIOSUPER"]);

            //txtInteresado.Text = oUsuario.t314_idusuario.ToString("#,###") + " - " + oUsuario.Nombre;
            txtInteresado.Text = oUsuario.Nombre;
            hdnInteresado.Text = oUsuario.t314_idusuario.ToString();
            hdnNodoBeneficiario.Text = oUsuario.t303_idnodo.ToString();
            sNodoUsuario = oUsuario.t303_denominacion;
            hdnCCIberper.Text = oUsuario.nCCIberper.ToString();

            txtEmpresa.Text = oUsuario.t313_denominacion;
            txtOficinaLiq.Text = (oUsuario.oOficinaLiquidadora != null) ? oUsuario.oOficinaLiquidadora.t010_desoficina : "";
            if (Session["GVT_MOTIVODEFECTO"] != null)
                cboMotivo.SelectedValue = Session["GVT_MOTIVODEFECTO"].ToString();

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
            setEmpresaTerritorio(oUsuario.t314_idusuario, oUsuario.t001_idficepi, oUsuario.Nombre, 
                                                          oUsuario.t313_idempresa_defecto, 0);

            #endregion
        }
    }
    protected void ObtenerDatosGastos(int nReferencia)
    {
        divFondoCatalogoGastos.InnerHtml = POSICIONGV.CatalogoGastos(nReferencia, bLectura);
    }

    protected bool ObtenerDatosCabeceraAparcadaEstandar(int nReferencia)
    {
        bool bAvisoEmpresaNoVigente = false;
        CABECERAAPARCADA_NEGV oCab = CABECERAAPARCADA_NEGV.ObtenerDatosCabecera(nReferencia);
        hdnReferencia.Text = oCab.t660_idreferencia.ToString();
        hdnInteresado.Text = oCab.t314_idusuario_interesado.ToString();
        sNodoUsuario = oCab.t303_denominacion_beneficiario;
        hdnOficinaLiquidadora.Text = oCab.oOficinaLiquidadora.t010_idoficina.ToString();
        imgEstado.ImageUrl = "~/Images/imgEstadoP.gif";
        hdnEstado.Text = "P";
        hdnEstadoAnterior.Text = "P";
        txtInteresado.Text = oCab.Interesado;
        //txtReferencia.Text = oCab.t420_idreferencia.ToString("#,###");
        txtConcepto.Text = oCab.t660_concepto;
        txtEmpresa.Text = oCab.t313_denominacion;
        cboMotivo.SelectedValue = oCab.t423_idmotivo.ToString();
        txtOficinaLiq.Text = oCab.oOficinaLiquidadora.t010_desoficina;
        hdnIdProyectoSubNodo.Text = (oCab.t305_idproyectosubnodo.HasValue) ? oCab.t305_idproyectosubnodo.ToString() : "";
        txtProyecto.Text = (oCab.t305_idproyectosubnodo.HasValue) ? ((int)oCab.t301_idproyecto).ToString("#,###") + " - " + oCab.t301_denominacion : "";
        cboMoneda.SelectedValue = oCab.t422_idmoneda.ToString();
        if (oCab.t660_justificantes.HasValue)
        {
            if ((bool)oCab.t660_justificantes) rdbJustificantes.Items[0].Selected = true;
            else rdbJustificantes.Items[1].Selected = true;
        }
        txtObservacionesNota.Text = oCab.t660_comentarionota;
        hdnAnotacionesPersonales.Text = Utilidades.escape(oCab.t660_anotaciones);
        if (oCab.t001_idficepi_interesado != (int)Session["GVT_IDFICEPI_ENTRADA"])
            divAnotaciones.Style.Add("visibility", "hidden");


        txtFecAnticipo.Text = (oCab.t660_fanticipo.HasValue) ? ((DateTime)oCab.t660_fanticipo).ToShortDateString() : "";
        txtImpAnticipo.Text = oCab.t660_importeanticipo.ToString("N");
        txtOficinaAnticipo.Text = oCab.t660_lugaranticipo;
        txtFecDevolucion.Text = (oCab.t660_fdevolucion.HasValue) ? ((DateTime)oCab.t660_fdevolucion).ToShortDateString() : "";
        txtImpDevolucion.Text = oCab.t660_importedevolucion.ToString("N");
        txtOficinaDevolucion.Text = oCab.t660_lugardevolucion;
        txtAclaracionesAnticipos.Text = oCab.t660_aclaracionesanticipo;

        txtPagadoTransporte.Text = oCab.t660_pagadotransporte.ToString("N");
        txtPagadoHotel.Text = oCab.t660_pagadohotel.ToString("N");
        txtPagadoOtros.Text = oCab.t660_pagadootros.ToString("N");
        txtAclaracionesPagado.Text = oCab.t660_aclaracionepagado;
        if (oCab.oDietaKm != null)
        {
            cldKMCO.InnerText = oCab.oDietaKm.t069_ick.ToString("N");
            cldDCCO.InnerText = oCab.oDietaKm.t069_icdc.ToString("N");
            cldMDCO.InnerText = oCab.oDietaKm.t069_icmd.ToString("N");
            cldDECO.InnerText = oCab.oDietaKm.t069_icde.ToString("N");
            cldDACO.InnerText = oCab.oDietaKm.t069_icda.ToString("N");
        }
        //cldKMEX.InnerText = oCab.oTerritorio.T007_ITERK.ToString("N");
        //cldDCEX.InnerText = oCab.oTerritorio.T007_ITERDC.ToString("N");
        //cldMDEX.InnerText = oCab.oTerritorio.T007_ITERMD.ToString("N");
        //cldDEEX.InnerText = oCab.oTerritorio.T007_ITERDE.ToString("N");
        //cldDAEX.InnerText = oCab.oTerritorio.T007_ITERDA.ToString("N");

        //hdnOficinaBase.Text = "";
        //hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
        //hdnIDTerritorio.Text = oCab.oTerritorio.T007_IDTERRFIS.ToString();
        hdnOficinaBase.Text = (oCab.t010_idoficina_base.HasValue) ? oCab.t010_idoficina_base.ToString() : "";
        int idEmpresaDefecto = GASVI.DAL.Configuracion.GetEmpresaDefecto(null, oCab.t001_codred);
        bAvisoEmpresaNoVigente = setEmpresaTerritorio(oCab.t314_idusuario_interesado, oCab.t001_idficepi_interesado, oCab.Interesado,
                                                      idEmpresaDefecto, (oCab.t313_idempresa == null) ? 0 : (int)oCab.t313_idempresa);
        //hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
        //hdnIDTerritorio.Text = oCab.oTerritorio.T007_IDTERRFIS.ToString();
        //lblTerritorio.Text = oCab.oTerritorio.T007_NOMTERRFIS;

        hdnOficinaLiquidadora.Text = oCab.oOficinaLiquidadora.t010_idoficina.ToString();

        hdnCCIberper.Text = oCab.nCCIberper.ToString();

        return bAvisoEmpresaNoVigente;
    }
    protected void ObtenerDatosGastosAparcadaEstandar(int nReferencia)
    {
        divFondoCatalogoGastos.InnerHtml = POSICIONAPARCADA_NEGV.CatalogoGastos(nReferencia);
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
            case "cancelar":
                Regresar();
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
    protected void Recuperar()
    {
        int nNotasRecuperadas = CABECERAGV.RecuperarNotaEstandar(int.Parse(hdnReferencia.Text));
        if (nNotasRecuperadas == 0)
        {
            sMsgRecuperada = "Durante su intervención, el estado de la nota ha variado y no se permite su recuperación.";
        }
        else
        {
            Master.nBotonera = 17;
            Master.CargarBotonera();
            cboMotivo.Items.Clear();
            obtenerMotivos((int)Session["GVT_USUARIOSUPER"], "");
            obtenerMonedas();
            bLectura = false;
            imgCalculadora.Visible = true;
            Calculadora.Visible = true;
            divDisposiciones.Style.Add("display", "block");
            imgManoVisador.Style.Add("visibility", "hidden");
            ObtenerDatosCabecera(int.Parse(hdnReferencia.Text));
            ObtenerDatosGastos(int.Parse(hdnReferencia.Text));

            //controlar los del beneficiario, no los del usuario conectado
            if ((int)Session["GVT_CCIBERPER"] > 1)
            {
                lblNodo.Attributes.Add("class", "enlace");
                lblNodo.Attributes.Add("onclick", "getCCIberper()");
            }
        }
    }

    protected void inicializarToolTips()
    {
        Tooltips oToolTips = Tooltips.ObtenerToolTipsAll();
        nbCompleta.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Nº de dietas completas] body=[" + oToolTips.t671_dietacompleta + "] hideselects=[off]");
        nbMedia.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Nº de medias dietas] body=[" + oToolTips.t671_mediadieta + "] hideselects=[off]");
        nbEspecial.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Nº de dietas especiales] body=[" + oToolTips.t671_dietaespecial + "] hideselects=[off]");
        nbAlojamiento.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Nº de dietas de alojamiento] body=[" + oToolTips.t671_dietaalojamiento + "] hideselects=[off]");
        lblPeajes.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Peajes y aparcamientos] body=[" + oToolTips.t671_peajes + "] hideselects=[off]");
        lblComidas.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Comidas e invitaciones] body=[" + oToolTips.t671_comidas + "] hideselects=[off]");
        lblTransporte.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Transporte] body=[" + oToolTips.t671_transporte + "] hideselects=[off]");
        imgDisposiciones.ToolTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Disposiciones generales] body=[" + oToolTips.t671_disposiciones + "] hideselects=[off]";
        //imgKMSEstandares.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Distancias estándares] body=[" + oToolTips.t671_kmsestandar + "] hideselects=[off]");
        hdnToolTipKmEstan.Text = Utilidades.CadenaParaTooltipExtendido(oToolTips.t671_kmsestandar==null ? "":oToolTips.t671_kmsestandar);
    }
    //protected bool setEmpresaTerritorio(int nUsuario, int idFicepi, string sInteresado, int idEmpresaDefecto, int idEmpresa)
    //{
    //    bool bAvisoEmpresaNoVigente = false;
    //    //1º Comprobar si el profesional tiene más de una empresa.
    //   ArrayList aEmpresas = Profesional.ObtenerEmpresasTerritorios(nUsuario);

    //   //2º Comprobar que si se aparcó con alguna empresa, ésta sigue estando asociada la profesional. O solo tiene una empresa
    //   if (aEmpresas.Count > 1)
    //   {
    //       if (idEmpresa != 0)
    //       {
    //           bool bEmpresaVigente = false;
    //           for (int i = 0; i < aEmpresas.Count; i++)
    //           {
    //               if (idEmpresa.ToString() == ((string[])aEmpresas[i])[0])
    //               {
    //                   bEmpresaVigente = true;
    //                   break;
    //               }
    //           }
    //           if (!bEmpresaVigente)
    //           {
    //               //string sDenEmpresa = GASVI.BLL.Profesional.GetNombreEmpresa(idEmpresa);
    //               //throw (new Exception("La nota se aparcó con la empresa " + sDenEmpresa + " y " + sInteresado + " ya no pertenece a la misma"));
    //               idEmpresaNotaAparcada = idEmpresa;
    //               bAvisoEmpresaNoVigente = true;
    //           }
    //       }
    //   }
    //   txtEmpresa.Text = "";
    //   hdnIDEmpresa.Text = "";
    //   if (aEmpresas.Count > 1)
    //   {
    //        #region Mas de una empresa
    //        txtEmpresa.Style.Add("display", "none");
    //        cboEmpresa.Style.Add("display", "block");
    //        int idEmpresaAux = idEmpresa;
    //        if (idEmpresaAux == 0) idEmpresaAux = idEmpresaDefecto;

    //        ListItem oLI = null;
    //        for (int i = 0; i < aEmpresas.Count; i++)
    //        {
    //            //if (Session["GVT_IDEMPRESADEFECTO"].ToString() == ((string[])aEmpresas[i])[1])
    //            if (i == 0 && idEmpresaDefecto == 0 && idEmpresa == 0)
    //            {
    //                oLI = new ListItem("", "0");
    //                oLI.Attributes.Add("idterritorio", "");
    //                oLI.Attributes.Add("nomterritorio", "");
    //                oLI.Attributes.Add("ITERDC", "");
    //                oLI.Attributes.Add("ITERMD", "");
    //                oLI.Attributes.Add("ITERDA", "");
    //                oLI.Attributes.Add("ITERDE", "");
    //                oLI.Attributes.Add("ITERK", "");
    //                cboEmpresa.Items.Add(oLI);
    //            }
    //            else if (idEmpresaAux.ToString() == ((string[])aEmpresas[i])[0])
    //            {
    //                cboEmpresa.SelectedValue = ((string[])aEmpresas[i])[0];
    //                hdnIDEmpresa.Text = ((string[])aEmpresas[i])[0];
    //                hdnIDTerritorio.Text = ((string[])aEmpresas[i])[2];
    //                lblTerritorio.Text = ((string[])aEmpresas[i])[3];
    //                cldKMEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[8]).ToString("N");
    //                cldDCEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[4]).ToString("N");
    //                cldMDEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[5]).ToString("N");
    //                cldDEEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[7]).ToString("N");
    //                cldDAEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[6]).ToString("N");
    //            }
    //            oLI = new ListItem(((string[])aEmpresas[i])[1], ((string[])aEmpresas[i])[0]);
    //            oLI.Attributes.Add("idterritorio", ((string[])aEmpresas[i])[2]);
    //            oLI.Attributes.Add("nomterritorio", ((string[])aEmpresas[i])[3]);
    //            oLI.Attributes.Add("ITERDC", ((string[])aEmpresas[i])[4]);
    //            oLI.Attributes.Add("ITERMD", ((string[])aEmpresas[i])[5]);
    //            oLI.Attributes.Add("ITERDA", ((string[])aEmpresas[i])[6]);
    //            oLI.Attributes.Add("ITERDE", ((string[])aEmpresas[i])[7]);
    //            oLI.Attributes.Add("ITERK", ((string[])aEmpresas[i])[8]);

    //            cboEmpresa.Items.Add(oLI);

    //        }
    //       #endregion
    //   }
    //   else if (aEmpresas.Count == 1)
    //   {
    //       #region Solo una empresa
    //       txtEmpresa.Style.Add("display", "block");
    //        cboEmpresa.Style.Add("display", "none");
    //        hdnIDEmpresa.Text = ((string[])aEmpresas[0])[0];
    //        txtEmpresa.Text = ((string[])aEmpresas[0])[1];
    //        hdnIDTerritorio.Text = ((string[])aEmpresas[0])[2];
    //        lblTerritorio.Text = ((string[])aEmpresas[0])[3];
    //        cldKMEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[8]).ToString("N");
    //        cldDCEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[4]).ToString("N");
    //        cldMDEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[5]).ToString("N");
    //        cldDEEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[7]).ToString("N");
    //        cldDAEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[6]).ToString("N");
    //       #endregion
    //   }

    //   return bAvisoEmpresaNoVigente;
    //}
    protected bool setEmpresaTerritorio(int nUsuario, int idFicepi, string sInteresado, int idEmpresaDefecto, int idEmpresa)
    {
        bool bAvisoEmpresaNoVigente = false;
        //1º Comprobar si el profesional tiene más de una empresa.
        ArrayList aEmpresas = Profesional.ObtenerEmpresasTerritorios(nUsuario);

        //2º Comprobar que si se aparcó con alguna empresa, ésta sigue estando asociada la profesional. O solo tiene una empresa
        if (aEmpresas.Count > 1)
        {
            if (idEmpresa != 0)
            {
                bool bEmpresaVigente = false;
                for (int i = 0; i < aEmpresas.Count; i++)
                {
                    if (idEmpresa.ToString() == ((string[])aEmpresas[i])[0])
                    {
                        bEmpresaVigente = true;
                        break;
                    }
                }
                if (!bEmpresaVigente)
                {
                    //string sDenEmpresa = GASVI.BLL.Profesional.GetNombreEmpresa(idEmpresa);
                    //throw (new Exception("La nota se aparcó con la empresa " + sDenEmpresa + " y " + sInteresado + " ya no pertenece a la misma"));
                    idEmpresaNotaAparcada = idEmpresa;
                    bAvisoEmpresaNoVigente = true;
                }
            }
        }
        txtEmpresa.Text = "";
        hdnIDEmpresa.Text = "";
        if (aEmpresas.Count > 1)
        {
            #region Mas de una empresa
            txtEmpresa.Style.Add("display", "none");
            cboEmpresa.Style.Add("display", "block");
            int idEmpresaAux = idEmpresa;
            if (idEmpresaAux == 0) idEmpresaAux = idEmpresaDefecto;

            ListItem oLI = null;
            if (idEmpresaDefecto == 0 && idEmpresa == 0)
            {
                oLI = new ListItem("", "0");
                oLI.Attributes.Add("idterritorio", "");
                oLI.Attributes.Add("nomterritorio", "");
                oLI.Attributes.Add("ITERDC", "");
                oLI.Attributes.Add("ITERMD", "");
                oLI.Attributes.Add("ITERDA", "");
                oLI.Attributes.Add("ITERDE", "");
                oLI.Attributes.Add("ITERK", "");
                cboEmpresa.Items.Add(oLI);
            }
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

                if (idEmpresaAux.ToString() == ((string[])aEmpresas[i])[0])
                {
                    cboEmpresa.SelectedValue = ((string[])aEmpresas[i])[0];
                    hdnIDEmpresa.Text = ((string[])aEmpresas[i])[0];
                    hdnIDTerritorio.Text = ((string[])aEmpresas[i])[2];
                    lblTerritorio.Text = ((string[])aEmpresas[i])[3];
                    cldKMEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[8]).ToString("N");
                    cldDCEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[4]).ToString("N");
                    cldMDEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[5]).ToString("N");
                    cldDEEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[7]).ToString("N");
                    cldDAEX.InnerText = decimal.Parse(((string[])aEmpresas[i])[6]).ToString("N");
                }
            }
            #endregion
        }
        else if (aEmpresas.Count == 1)
        {
            #region Solo una empresa
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
            #endregion
        }

        return bAvisoEmpresaNoVigente;
    }

    protected string ObtenerDatosBeneficiario(int nUsuario)
    {
        StringBuilder sb = new StringBuilder();

        USUARIO oUsuario = USUARIO.Obtener(nUsuario);
        sb.Append(oUsuario.Nombre +"{sep}");
        sb.Append(oUsuario.t314_idusuario.ToString() + "{sep}");
        sb.Append(oUsuario.t313_denominacion + "{sep}");
        sb.Append(oUsuario.oOficinaLiquidadora.t010_desoficina + "{sep}");
        sb.Append(oUsuario.t422_idmoneda.ToString() + "{sep}");
        sb.Append(oUsuario.oDietaKm.t069_ick.ToString("N") + "{sep}");
        sb.Append(oUsuario.oDietaKm.t069_icdc.ToString("N") + "{sep}");
        sb.Append(oUsuario.oDietaKm.t069_icmd.ToString("N") + "{sep}");
        sb.Append(oUsuario.oDietaKm.t069_icde.ToString("N") + "{sep}");
        sb.Append(oUsuario.oDietaKm.t069_icda.ToString("N") + "{sep}");
        sb.Append(oUsuario.oTerritorio.T007_ITERK.ToString("N") + "{sep}");
        sb.Append(oUsuario.oTerritorio.T007_ITERDC.ToString("N") + "{sep}");
        sb.Append(oUsuario.oTerritorio.T007_ITERMD.ToString("N") + "{sep}");
        sb.Append(oUsuario.oTerritorio.T007_ITERDE.ToString("N") + "{sep}");
        sb.Append(oUsuario.oTerritorio.T007_ITERDA.ToString("N") + "{sep}");
        sb.Append((oUsuario.t010_idoficina_base.HasValue) ? oUsuario.t010_idoficina_base.ToString() + "{sep}" : "" + "{sep}");
        sb.Append(oUsuario.oOficinaLiquidadora.t010_idoficina.ToString() + "{sep}");
        sb.Append((oUsuario.bAutorresponsable) ? "1" + "{sep}" : "0" + "{sep}");
        sb.Append(oUsuario.nCCIberper.ToString() + "{sepdatos}");

        List<ElementoLista> aMotivos = MOTIVO.ObtenerMotivos(nUsuario, "");
        for (int i=0; i<aMotivos.Count; i++){
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
        sb.Append("{sepdatos}");
        sb.Append(oUsuario.t313_idempresa_defecto.ToString());
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
    protected string ObtenerDatosEmpresas(int nUsuario)
    {
        StringBuilder sb = new StringBuilder();

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
        return sb.ToString();
    }

}


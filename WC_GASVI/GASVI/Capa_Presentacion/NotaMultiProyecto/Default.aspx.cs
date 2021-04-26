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
    public int nID = 0, idEmpresaNotaAparcada = 0;
    public string sEstado = "";
    public string sToolTipProyectoPorDefecto = "", sNodoUsuario = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 1;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Detalle de nota multiproyecto";

            Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            cboEmpresa.Style.Add("display", "none");

            if (!Page.IsPostBack)
            {
                try
                {
                    obtenerMonedas();

                    if (Request.QueryString["ni"] != null)
                        nID = int.Parse(Utilidades.decodpar(Request.QueryString["ni"].ToString()));
                    if (Request.QueryString["se"] != null)
                        sEstado = Utilidades.decodpar(Request.QueryString["se"].ToString());

                    //nID = 20;
                    inicializarToolTips();

                    if (sEstado == "P") //o nota aparcada
                    {
                        #region Datos Cabecera Nota Aparcada
                        Master.nBotonera = 16;
                        ObtenerDatosCabeceraAparcadaMultiProyecto(nID);
                        ObtenerDatosGastosAparcadaMultiProyecto(nID);
                        #endregion
                    }
                    else //o nota nueva 
                    {
                        #region Datos Cabecera
                        ObtenerDatosCabecera(nID);
                        ObtenerDatosGastosAparcadaMultiProyecto(nID);
                        //ObtenerDatosGastos(nID);
                        #endregion
                    }

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
                    sResultado += "OK@#@" + CABECERAGV.TramitarNotaMultiProyecto(aArgs[1], aArgs[2], aArgs[3]);
                }
                catch (Exception ex)
                {
                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "Error@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al tramitar la solicitud multiproyecto.", ex);
                }
                break;
            case ("aparcar"):
                try
                {
                    sResultado += "OK@#@" + CABECERAAPARCADA_NMPGV.AparcarNotaMultiProyecto(aArgs[1], aArgs[2]);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Solicitud aparcada no existente")
                        sResultado += "OK@#@" + ex.Message;
                    else
                        sResultado += "Error@#@" + Errores.mostrarError("Error al aparcar la solicitud multiproyecto.", ex);
                }
                break;


            //case ("tarearecurso"):
            //    sResultado += ObtenerDatosTareaRecurso(aArgs[1], aArgs[2]);
            //    break;
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
                        //sCad = ObtenerRecursosAsociados(aArgs[2], aArgs[4], false);
                        if (sCad.IndexOf("Error@#@") >= 0)
                            sResultado += sCad;
                        else
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                }
                break;
            case ("eliminar"):
                try
                {
                    CABECERAAPARCADA_NMPGV.Eliminar(int.Parse(aArgs[1]));
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al eliminar la solicitud multiproyecto.", ex);
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
        hdnToolTipKmEstan.Text = Utilidades.CadenaParaTooltipExtendido(oToolTips.t671_kmsestandar == null ? "" : oToolTips.t671_kmsestandar);
    }

    protected void ObtenerDatosCabecera(int nReferencia)
    {
        if (nReferencia > 0)
        {
            #region solicitud existente
            CABECERAGV oCab = CABECERAGV.ObtenerDatosCabecera(nReferencia);
            hdnReferencia.Text = oCab.t420_idreferencia.ToString();
            hdnInteresado.Text = oCab.t314_idusuario_interesado.ToString();
            sNodoUsuario = oCab.t303_denominacion_beneficiario;
            hdnOficinaLiquidadora.Text = oCab.t010_idoficina.ToString();
            hdnEstado.Text = oCab.t431_idestado;
            hdnEstadoAnterior.Text = oCab.t431_idestado;
            txtInteresado.Text = oCab.Interesado;
            //txtReferencia.Text = oCab.t420_idreferencia.ToString("#,###");
            sNodoUsuario = oCab.t303_denominacion_beneficiario;
            txtConcepto.Text = oCab.t420_concepto;
            //txtEmpresa.Text = oCab.t313_denominacion;
            //cboMotivo.SelectedValue = oCab.t423_idmotivo.ToString();
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
            hdnAnotacionesPersonales.Text = Utilidades.escape(oCab.t420_anotaciones);
            if (oCab.t001_idficepi_interesado != (int)Session["GVT_IDFICEPI_ENTRADA"])
                divAnotaciones.Style.Add("visibility", "hidden");

            hdnOficinaBase.Text = "";

            if (oCab.t431_idestado == "P")//Aparcada
            {
                //setEmpresaTerritorio(oCab.t314_idusuario_interesado);
                int idEmpresaDefecto = GASVI.DAL.Configuracion.GetEmpresaDefecto(null, oCab.t001_codred);
                setEmpresaTerritorio(oCab.t314_idusuario_interesado, oCab.t001_idficepi_interesado, oCab.Interesado,
                                     idEmpresaDefecto, oCab.t313_idempresa);

            }
            else
            {
                txtEmpresa.Text = oCab.t313_denominacion;
                hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
                hdnIDTerritorio.Text = oCab.t007_idterrfis.ToString();
                lblTerritorio.Text = oCab.t007_nomterrfis;
            }

            //hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
            //hdnIDTerritorio.Text = oCab.t007_idterrfis.ToString();
            //lblTerritorio.Text = oCab.t007_nomterrfis;

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
            #endregion
        }
        else //Nueva nota
        {
            #region nueva solicitud
            USUARIO oUsuario = USUARIO.Obtener((int)Session["GVT_USUARIOSUPER"]);

            txtInteresado.Text = oUsuario.Nombre;
            hdnInteresado.Text = oUsuario.t314_idusuario.ToString();
            sNodoUsuario = oUsuario.t303_denominacion;
            txtEmpresa.Text = oUsuario.t313_denominacion;
            txtOficinaLiq.Text = (oUsuario.oOficinaLiquidadora != null) ? oUsuario.oOficinaLiquidadora.t010_desoficina : "";

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

            //1ºComprobar si el profesional tiene más de una empresa.
            //setEmpresaTerritorio((int)Session["GVT_USUARIOSUPER"]);
            setEmpresaTerritorio(oUsuario.t314_idusuario, oUsuario.t001_idficepi, oUsuario.Nombre,
                                                          oUsuario.t313_idempresa_defecto, 0);
            #endregion
        }
    }

    protected bool ObtenerDatosCabeceraAparcadaMultiProyecto(int nReferencia)
    {
        bool bAvisoEmpresaNoVigente = false;
        CABECERAAPARCADA_NMPGV oCab = CABECERAAPARCADA_NMPGV.ObtenerDatosCabecera(nReferencia);

        hdnReferencia.Text = oCab.t663_idreferencia.ToString();
        hdnInteresado.Text = oCab.t314_idusuario_interesado.ToString();
        sNodoUsuario = oCab.t303_denominacion_beneficiario;
        hdnOficinaLiquidadora.Text = oCab.oOficinaLiquidadora.t010_idoficina.ToString();
        imgEstado.ImageUrl = "~/Images/imgEstadoP.gif";
        hdnEstado.Text = "P";
        hdnEstadoAnterior.Text = "P";
        txtInteresado.Text = oCab.Interesado;
        //txtReferencia.Text = oCab.t420_idreferencia.ToString("#,###");
        txtConcepto.Text = oCab.t663_concepto;
        txtEmpresa.Text = oCab.t313_denominacion;
        //cboMotivo.SelectedValue = oCab.t423_idmotivo.ToString();
        txtOficinaLiq.Text = oCab.oOficinaLiquidadora.t010_desoficina;
        hdnIdProyectoSubNodo.Text = (oCab.t305_idproyectosubnodo.HasValue) ? oCab.t305_idproyectosubnodo.ToString() : "";
        txtProyecto.Text = (oCab.t305_idproyectosubnodo.HasValue) ? ((int)oCab.t301_idproyecto).ToString("#,###") + " - " + oCab.t301_denominacion : "";
        if (oCab.t305_idproyectosubnodo.HasValue)
        {
            sToolTipProyectoPorDefecto = PROYECTO.ObtenerTooltipProyectoUsuario((int)oCab.t305_idproyectosubnodo, oCab.t314_idusuario_interesado);
            txtProyecto.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Transporte] body=[" + sToolTipProyectoPorDefecto + "] hideselects=[off]");
        }

        cboMoneda.SelectedValue = oCab.t422_idmoneda.ToString();
        if (oCab.t663_justificantes.HasValue)
        {
            if ((bool)oCab.t663_justificantes) rdbJustificantes.Items[0].Selected = true;
            else rdbJustificantes.Items[1].Selected = true;
        }
        txtObservacionesNota.Text = oCab.t663_comentarionota;
        hdnAnotacionesPersonales.Text = oCab.t663_anotaciones;

        //hdnOficinaBase.Text = (oCab.t010_idoficina_base.HasValue)? oCab.t010_idoficina_base.ToString():"";
        //hdnIDEmpresa.Text = oCab.t313_idempresa.ToString();
        //hdnIDTerritorio.Text = oCab.oTerritorio.T007_IDTERRFIS.ToString();

        cldKMCO.InnerText = oCab.oDietaKm.t069_ick.ToString("N");
        cldDCCO.InnerText = oCab.oDietaKm.t069_icdc.ToString("N");
        cldMDCO.InnerText = oCab.oDietaKm.t069_icmd.ToString("N");
        cldDECO.InnerText = oCab.oDietaKm.t069_icde.ToString("N");
        cldDACO.InnerText = oCab.oDietaKm.t069_icda.ToString("N");
        //cldKMEX.InnerText = oCab.oTerritorio.T007_ITERK.ToString("N");
        //cldDCEX.InnerText = oCab.oTerritorio.T007_ITERDC.ToString("N");
        //cldMDEX.InnerText = oCab.oTerritorio.T007_ITERMD.ToString("N");
        //cldDEEX.InnerText = oCab.oTerritorio.T007_ITERDE.ToString("N");
        //cldDAEX.InnerText = oCab.oTerritorio.T007_ITERDA.ToString("N");

        hdnOficinaBase.Text = (oCab.t010_idoficina_base.HasValue) ? oCab.t010_idoficina_base.ToString() : "";
        hdnOficinaLiquidadora.Text = oCab.oOficinaLiquidadora.t010_idoficina.ToString();

        //setEmpresaTerritorio(oCab.t314_idusuario_interesado);

        int idEmpresaDefecto = GASVI.DAL.Configuracion.GetEmpresaDefecto(null, oCab.t001_codred);
        bAvisoEmpresaNoVigente = setEmpresaTerritorio(oCab.t314_idusuario_interesado, oCab.t001_idficepi_interesado, oCab.Interesado,
                                                      idEmpresaDefecto, (oCab.t313_idempresa == null) ? 0 : (int)oCab.t313_idempresa);
        return bAvisoEmpresaNoVigente;
    }
    protected void ObtenerDatosGastosAparcadaMultiProyecto(int nReferencia)
    {
        divFondoCatalogoGastos.InnerHtml = POSICIONAPARCADA_NMPGV.CatalogoGastos(nReferencia);
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
        }
    }
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }
    //protected void setEmpresaTerritorio(int nUsuario)
    //{
    //    //1ºComprobar si el profesional tiene más de una empresa.
    //    ArrayList aEmpresas = Profesional.ObtenerEmpresasTerritorios(nUsuario);
    //    txtEmpresa.Text = "";
    //    hdnIDEmpresa.Text = "";

    //    if (aEmpresas.Count > 1)
    //    {
    //        txtEmpresa.Style.Add("display", "none");
    //        cboEmpresa.Style.Add("display", "block");

    //        ListItem oLI = null;
    //        for (int i = 0; i < aEmpresas.Count; i++)
    //        {
    //            oLI = new ListItem(((string[])aEmpresas[i])[1], ((string[])aEmpresas[i])[0]);
    //            oLI.Attributes.Add("idterritorio", ((string[])aEmpresas[i])[2]);
    //            oLI.Attributes.Add("nomterritorio", ((string[])aEmpresas[i])[3]);
    //            oLI.Attributes.Add("ITERDC", ((string[])aEmpresas[i])[4]);
    //            oLI.Attributes.Add("ITERMD", ((string[])aEmpresas[i])[5]);
    //            oLI.Attributes.Add("ITERDA", ((string[])aEmpresas[i])[6]);
    //            oLI.Attributes.Add("ITERDE", ((string[])aEmpresas[i])[7]);
    //            oLI.Attributes.Add("ITERK", ((string[])aEmpresas[i])[8]);

    //            cboEmpresa.Items.Add(oLI);

    //            if (cboEmpresa.Items.Count == 1 ||
    //                (((string[])aEmpresas[i])[0] == "1" && nID == 0)
    //                )
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
    //        }
    //    }
    //    else if (aEmpresas.Count == 1)
    //    {
    //        txtEmpresa.Style.Add("display", "block");
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
    //    }
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
        sb.Append(oUsuario.Nombre + "{sep}");
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
        sb.Append(oUsuario.oOficinaLiquidadora.t010_idoficina.ToString() + "{sepdatos}");
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


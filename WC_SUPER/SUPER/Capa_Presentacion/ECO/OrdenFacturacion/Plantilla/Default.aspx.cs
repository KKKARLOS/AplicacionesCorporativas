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
    public string sErrores = "", sLectura = "false", sIDDocuAux = "";
    public int nIdPlantilla = 0;
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

                Utilidades.SetEventosFecha(this.txtFecPrevEmFac);

                nIdPlantilla = int.Parse(Request.QueryString["nIdPlantilla"].ToString());
                hdnIdPlantilla.Text = nIdPlantilla.ToString();

                CargarDatosGenerales();

                if (nIdPlantilla == 0)//nueva
                {
                    sIDDocuAux = "SUPER-" + Session["IDFICEPI_ENTRADA"].ToString() + "-" + DateTime.Now.Ticks.ToString();
                }
                else
                {
                    CargarDatosPlantilla();
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al cargar los datos de la plantilla", ex);
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
        string sResultado = "", sCad = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("documentos"):
                string sModoAcceso = "W", sEstadoProyecto = "A";
                //sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), "W", "A");
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = getDocumentos(aArgs[1], aArgs[3], aArgs[4]);
                //sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("getDatosPestana"):
                //sResultado += aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0: //Cabecera
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1: //Posiciones
                        //sResultado += getPosiciones(aArgs[2]);
                        sCad = getPosiciones(aArgs[2], aArgs[3]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;

                        break;
                    case 2://DOCUMENTACION
                        sCad = getDocumentos(aArgs[2], aArgs[3], aArgs[4]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                }
                break;
            case ("getViasDePago"):
                sResultado += getViasDePago(aArgs[1], aArgs[2]);
                break;
           case ("getDestFactByOV"):
                sResultado += getDestFactByOV(aArgs[1], aArgs[2]);
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
                break;
            case ("setSolicitud"):
                sResultado += setSolicitud(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("getCondPagoDefecto"):
                sResultado += getCondPagoDefecto(aArgs[1], aArgs[2]);
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

    private void CargarDatosGenerales()
    {
        // Leer Organizacion de Ventas
        cboOV.DataValueField = "CODIGO";
        cboOV.DataTextField = "DENOMINACION";
        cboOV.DataSource = ORGVENTASSAP.Catalogo();
        cboOV.DataBind();
        cboOV.Items.Insert(0, new ListItem("", ""));

        // Leer Cond.Pago
        cboCondPago.DataValueField = "ID";
        cboCondPago.DataTextField = "DENOMINACION";
        cboCondPago.DataSource = CONDPAGO.Catalogo();
        cboCondPago.DataBind();
        cboCondPago.Items.Insert(0, new ListItem("", ""));

        // Leer Monedas
        cboMoneda.DataValueField = "ID";
        cboMoneda.DataTextField = "DENOMINACION";
        cboMoneda.DataSource = MONEDA.CatalogoSAP();
        cboMoneda.DataBind();
        cboMoneda.SelectedValue = "EUR";
    }
    private void CargarDatosPlantilla()
    {
        //// Leer Plantilla
        PLANTILLAORDENFAC oPlantilla = PLANTILLAORDENFAC.Select(null, nIdPlantilla);

        txtDenominacion.Text = oPlantilla.t629_denominacion;

        if (oPlantilla.t629_estado == "B") rdbEstado.Items[0].Selected = true;
        else rdbEstado.Items[1].Selected = true;

        hdnT305IdProy.Text = oPlantilla.t305_idproyectosubnodo.ToString();
        txtNumPE.Text = (oPlantilla.t301_idproyecto.HasValue) ? ((int)oPlantilla.t301_idproyecto).ToString("#,###") : "";
        txtDesPE.Text = oPlantilla.t301_denominacion;

        if (oPlantilla.t305_idproyectosubnodo.HasValue && oPlantilla.t302_idcliente_respago.HasValue){
            cboViaPago.DataValueField = "ID";
            cboViaPago.DataTextField = "DENOMINACION";
            //string sCodExtCliEmpProy = SUPER.DAL.PROYECTOSUBNODO.GetCodigoExternoClienteProyecto(int.Parse(Request.QueryString["sT305IdProy"].ToString()));
            string sCodExtCliEmpProy = SUPER.DAL.PROYECTOSUBNODO.GetCodigoExternoClienteProyecto((int)oPlantilla.t305_idproyectosubnodo);
            //cboViaPago.DataSource = VIAPAGO.Cliente(null, (int)oPlantilla.t305_idproyectosubnodo, (int)oPlantilla.t302_idcliente_respago);
            cboViaPago.DataSource = VIAPAGO.Cliente(null, sCodExtCliEmpProy, (int)oPlantilla.t302_idcliente_respago);
            cboViaPago.DataBind();
            if (cboViaPago.Items.Count != 1)
            {
                cboViaPago.Items.Insert(0, new ListItem("", "0"));
                cboViaPago.SelectedValue = "0";
            }
        }

        hdnIdCliSolicitante.Text = oPlantilla.t302_idcliente_solici.ToString();
        ////txtNIFCliSolicitante.Text = oORDENFAC.NifSolicitante;
        ////txtDesCliSolicitante.Text = oORDENFAC.t302_denominacion_solici;

        hdnIdCliPago.Text = oPlantilla.t302_idcliente_respago.ToString();
        txtNIFCliPago.Text = oPlantilla.NifRespPago;
        txtDesCliRespPago.Text = oPlantilla.t302_denominacion_respago;

        hdnIdCliDestFac.Text = oPlantilla.t302_idcliente_destfact.ToString();
        txtNIFCliDestFac.Text = oPlantilla.NifDestFra;
        txtDesClienteDestFac.Text = oPlantilla.t302_denominacion_destfact;
        cldDireccion.InnerHtml = oPlantilla.direccion;

        //hdnIdRespComercial.Text = oORDENFAC.t314_idusuario_respcomercial.ToString();
        //txtRespCom.Text = oORDENFAC.RespComercial;

        //txtEstado.Text = oORDENFAC.des_estado;
        txtRefCli.Text = oPlantilla.t629_refcliente;

        cboCondPago.SelectedValue = oPlantilla.t629_condicionpago.ToString().Trim();
        cboViaPago.SelectedValue = oPlantilla.t629_viapago.ToString();
        cboMoneda.SelectedValue = oPlantilla.t629_moneda.ToString();
        cboOV.SelectedValue = oPlantilla.t621_idovsap;


        //string sToolTip = "";
        //sToolTip = "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[";

        //sToolTip += "<label style='width:100px;'>Creada</label>" + oORDENFAC.t610_fcreacion.ToString().Substring(0, oORDENFAC.t610_fcreacion.ToString().Length - 3) + "<br>";
        //if (oORDENFAC.t610_ftramitada.HasValue)
        //    sToolTip += "<label style='width:100px;'>Tramitada</label>" + ((DateTime)oORDENFAC.t610_ftramitada).ToString().Substring(0, oORDENFAC.t610_ftramitada.ToString().Length - 3) + "<br>";
        //if (oORDENFAC.t610_fenviada.HasValue)
        //    sToolTip += "<label style='width:100px;'>Enviada</label>" + ((DateTime)oORDENFAC.t610_fenviada).ToString().Substring(0, oORDENFAC.t610_fenviada.ToString().Length - 3) + "<br>";
        ////if (oORDENFAC.t610_ftraspasada.HasValue)
        ////    sToolTip += "<label style='width:100px;'>Tramitada</label>" + ((DateTime)oORDENFAC.t610_ftraspasada).ToString() + "<br>";
        //if (oORDENFAC.t610_frecogida.HasValue)
        //    sToolTip += "<label style='width:100px;'>Recogida</label>" + ((DateTime)oORDENFAC.t610_frecogida).ToString().Substring(0, oORDENFAC.t610_frecogida.ToString().Length - 3) + "<br>";

        //sToolTip += "] hideselects=[off]";
        //cldCronologia.Attributes.Add("title", sToolTip);

        ////txtFecCreacion.Text = oORDENFAC.t610_fcreacion.ToShortDateString();
        ////txtFecTram.Text = (oORDENFAC.t610_ftramitada.HasValue) ? ((DateTime)oORDENFAC.t610_ftramitada).ToShortDateString() : "";
        ////txtFecTras.Text = (oORDENFAC.t610_ftraspasada.HasValue) ? ((DateTime)oORDENFAC.t610_ftraspasada).ToShortDateString() : "";
        ////txtFecEnvio.Text = (oORDENFAC.t610_fenviada.HasValue) ? ((DateTime)oORDENFAC.t610_fenviada).ToShortDateString() : "";
        ////txtFecRecogida.Text = (oORDENFAC.t610_frecogida.HasValue) ? ((DateTime)oORDENFAC.t610_frecogida).ToShortDateString() : "";
        txtFecPrevEmFac.Text = (oPlantilla.t629_fprevemifact.HasValue) ? ((DateTime)oPlantilla.t629_fprevemifact).ToShortDateString() : "";
        //txtFecDiferida.Text = (oORDENFAC.t610_fdiferida.HasValue) ? ((DateTime)oORDENFAC.t610_fdiferida).ToShortDateString() : "";

        //txtDocVentSAP.Text = oORDENFAC.t610_dvsap;
        txtClaveAgru.Text = (oPlantilla.t622_idagrupacion.HasValue) ? ((int)oPlantilla.t622_idagrupacion).ToString("#,###") : "";
        txtClaveAgru.Attributes.Add("title", oPlantilla.t622_denominacion);
        txtComentarios.Text = oPlantilla.t629_comentario;
        txtObsPool.Text = oPlantilla.t629_observacionespool;
        txtObsPlantilla.Text = oPlantilla.t629_observacionesplan;
        txtDtoPorc.Text = (oPlantilla.t629_dto_porcen == 0) ? "" : oPlantilla.t629_dto_porcen.ToString("N");
        txtDtoImporte.Text = (oPlantilla.t629_dto_importe == 0) ? "" : oPlantilla.t629_dto_importe.ToString("N");
        chkIVA.Checked = oPlantilla.t629_ivaincluido;
        this.txtCabFact.Text = oPlantilla.t629_textocabecera;
        if (oPlantilla.t302_efactur)
            this.hdnEfactur.Value = "S";
        else
            this.hdnEfactur.Value = "N";

    }
    private string recuperarPSN(string sT305IdProy)
    {
        //bool bErrorControlado = false;
        StringBuilder sb = new StringBuilder();
        string st302_idcliente_proyecto = "", st621_idovsap = "", sEsSolicitanteSAP = "";
        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy3(null, int.Parse(sT305IdProy));
            if (dr.Read())
            {
                //if (dr["t314_idusuario_comercialhermes"] == DBNull.Value)
                //{
                //    bErrorControlado = true;
                //}
                sb.Append(dr["t301_estado"].ToString() + "@#@");  //2
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //3
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "@#@");  //4
                sb.Append(dr["t302_denominacion"].ToString() + "@#@");  //5
                sb.Append(dr["t302_idcliente_proyecto"].ToString() + "@#@");  //6
                st302_idcliente_proyecto = dr["t302_idcliente_proyecto"].ToString();
                sb.Append(dr["NifRespPago"].ToString() + "@#@");  //7
                sb.Append(dr["t621_idovsap"].ToString() + "@#@");  //8
                st621_idovsap = dr["t621_idovsap"].ToString();

                if (dr["t314_idusuario_comercialhermes"] != DBNull.Value)
                    sb.Append(dr["t314_idusuario_comercialhermes"].ToString() + "@#@");  //9
                else
                    sb.Append(dr["t314_idusuario_responsable"].ToString() + "@#@");  //9

                sb.Append(dr["denominacion_SAP"].ToString() + "@#@");  //10
                if ((bool)dr["t302_efactur"]) sb.Append("S@#@");  //11
                else sb.Append("N@#@");  //11
            }
            dr.Close();
            dr.Dispose();

            //if (bErrorControlado)
            //{
            //    throw (new Exception("El proyecto seleccionado no tiene contrato."));
            //}

            sEsSolicitanteSAP = (CLIENTE.EsSolicitanteSAP(null, int.Parse(st302_idcliente_proyecto))) ? "1" : "0";
            sb.Append(sEsSolicitanteSAP + "@#@");  //12 (es cliente solicitante/responsable de pago en SAP)

            sb.Append(CONDPAGO.CondicionPorDefecto(int.Parse(st302_idcliente_proyecto), st621_idovsap) + "@#@"); //13
            sb.Append(CLIENTE.ObtenerDireccion(int.Parse(st302_idcliente_proyecto)) + "@#@"); //14 dirección

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);

            //if (!bErrorControlado) return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto.", ex);
            //else return "Error@#@Selección denegada.\n\n" + ex.Message;
        }
    }
    private string Grabar(string strDatosCabecera, string strDatosPosiciones)
    {
        string sResul = "";
        bool bErrorControlado = false;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region Datos Cabecera
            string[] aDatosCabecera = Regex.Split(strDatosCabecera, "##");
            ///aDatosCabecera[0] = ID Plantilla 
            ///aDatosCabecera[1] = ID Cliente Respon.Pago 
            ///aDatosCabecera[2] = ID Cliente Destinatario Fra
            ///aDatosCabecera[3] = Referencia del cliente
            ///aDatosCabecera[4] = Condición de pago
            ///aDatosCabecera[5] = Vía de pago
            ///aDatosCabecera[6] = Moneda
            ///aDatosCabecera[7] = Fecha prevista de emisión de fra
            ///aDatosCabecera[8] = // Observaciones Pool
            ///aDatosCabecera[9] = Clave de agrupación
            ///aDatosCabecera[10] = Denominación de la plantilla
            ///aDatosCabecera[11] = Comentarios
            ///aDatosCabecera[12] = Estado
            ///aDatosCabecera[13] = Responsable Comercial
            ///aDatosCabecera[14] = ID proyectosubnodo 
            ///aDatosCabecera[15] = ID Organización de ventas
            ///aDatosCabecera[16] = Descuento %
            ///aDatosCabecera[17] = Descuento importe
            ///aDatosCabecera[18] = Fecha diferida
            ///aDatosCabecera[19] = ID Cliente Socilitante
            ///aDatosCabecera[20] = sIDDocuAux
            ///aDatosCabecera[21] = chkIVA

            int nIdPlantilla = int.Parse(aDatosCabecera[0]);
            int? iIdCliSolicitante = (aDatosCabecera[19] == "") ? null : (int?)int.Parse(aDatosCabecera[19]);
            int? iIdCliResPago = (aDatosCabecera[1] == "") ? null : (int?)int.Parse(aDatosCabecera[1]);
            int? iIdCliDesFra = (aDatosCabecera[2] == "") ? null : (int?)int.Parse(aDatosCabecera[2]);
            string sRefCli = Utilidades.unescape(aDatosCabecera[3]);
            string sCondPago = aDatosCabecera[4];
            string sViaPago = aDatosCabecera[5];
            string sMoneda = aDatosCabecera[6];
            DateTime? dFechaPrevEmFra = (aDatosCabecera[7] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[7]);
            string sObservPool = Utilidades.unescape(aDatosCabecera[8]);
            int? iIDAgrupacion = (aDatosCabecera[9] == "") ? null : (int?)int.Parse(aDatosCabecera[9]);
            string sDenominacion = Utilidades.unescape(aDatosCabecera[10]);
            string sComentarios = Utilidades.unescape(aDatosCabecera[11]);
            string sEstado = aDatosCabecera[12];
            //int iIdRespComercial = int.Parse(aDatosCabecera[13]);
            int? nPSN = (aDatosCabecera[14] == "") ? null : (int?)int.Parse(aDatosCabecera[14]);
            string sOVSAP = aDatosCabecera[15];
            float nDtoPorc = (aDatosCabecera[16] == "") ? 0 : float.Parse(aDatosCabecera[16]);
            decimal nDtoImporte = (aDatosCabecera[17] == "") ? 0 : decimal.Parse(aDatosCabecera[17]);
            string sObservPlantilla = Utilidades.unescape(aDatosCabecera[18]);
            bool bIVAIncluido = (aDatosCabecera[21] == "1") ? true : false;
            string sTextoCabecera = Utilidades.unescape(aDatosCabecera[22]);

            if (nIdPlantilla == 0)
            {
                nIdPlantilla = PLANTILLAORDENFAC.Insert(tr,
                            sDenominacion,
                            sEstado,
                            (int)Session["UsuarioActual"],
                            nPSN,
                            iIdCliSolicitante,
                            iIdCliResPago,
                            iIdCliDesFra,
                            sCondPago,
                            sViaPago,
                            sRefCli,
                            dFechaPrevEmFra,
                            sMoneda,
                            iIDAgrupacion,
                            sObservPool,
                            sComentarios,
                            sOVSAP,
                            nDtoPorc,
                            nDtoImporte,
                            bIVAIncluido,
                            sObservPlantilla,
                            aDatosCabecera[20],
                            sTextoCabecera);
            }
            else //update
            {
                if (sEstado == "T")
                {
                    ///Estas tres comprobaciones no se pueden realizar bajo la transacción
                    ///existente porque acceden a SAP y no podemos realizar transacciones distribuidas.
                    if (iIdCliSolicitante != null && CLIENTE.EstaBloqueadoSAP(null, (int)iIdCliSolicitante))
                    {
                        bErrorControlado = true;
                        throw (new Exception("El cliente solicitante está bloqueado en SAP."));
                    }
                    if (iIdCliResPago != null && CLIENTE.EstaBloqueadoSAP(null, (int)iIdCliResPago))
                    {
                        bErrorControlado = true;
                        throw (new Exception("El cliente responsable de pago está bloqueado en SAP."));
                    }
                    if (iIdCliDesFra != null && CLIENTE.EstaBloqueadoSAP(null, (int)iIdCliDesFra))
                    {
                        bErrorControlado = true;
                        throw (new Exception("El cliente destinatario de factura está bloqueado en SAP."));
                    }
                }

                PLANTILLAORDENFAC.Update(tr,
                            nIdPlantilla,
                            sDenominacion,
                            sEstado,
                            //(int)Session["UsuarioActual"],
                            nPSN,
                            iIdCliSolicitante,
                            iIdCliResPago,
                            iIdCliDesFra,
                            sCondPago,
                            sViaPago,
                            sRefCli,
                            dFechaPrevEmFra,
                            sMoneda,
                            iIDAgrupacion,
                            sObservPool,
                            sComentarios,
                            sOVSAP,
                            nDtoPorc,
                            nDtoImporte,
                            bIVAIncluido,
                            sObservPlantilla, sTextoCabecera);
            }

            #endregion

            #region Datos Posiciones
            string[] aPosiciones = Regex.Split(strDatosPosiciones, "///");
            foreach (string oPosicion in aPosiciones)
            {
                if (oPosicion == "") continue;
                string[] aDatosPosicion = Regex.Split(oPosicion, "##");
                ///aDatosPosicion[0] = opción BD     
                ///aDatosPosicion[1] = ID Posición de la plantilla          
                ///aDatosPosicion[2] = Concepto de la posición
                ///aDatosPosicion[3] = Descripción de la posición
                ///aDatosPosicion[4] = Unidades
                ///aDatosPosicion[5] = Precio
                ///aDatosPosicion[6] = Descuento %
                ///aDatosPosicion[7] = Descuento importe

                switch (aDatosPosicion[0])
                {
                    case "I":
                        int nIDPosicion = PLANTILLAPOSICIONFAC.Insert(tr,
                                        nIdPlantilla,
                                        Utilidades.unescape(aDatosPosicion[2]),
                                        Utilidades.unescape(aDatosPosicion[3]),
                                        float.Parse(aDatosPosicion[4]),
                                        decimal.Parse(aDatosPosicion[5]),
                                        (aDatosPosicion[6] == "") ? 0 : float.Parse(aDatosPosicion[6]),
                                        (aDatosPosicion[7] == "") ? 0 : decimal.Parse(aDatosPosicion[7])
                                        );
                        break;
                    case "U":
                        PLANTILLAPOSICIONFAC.Update(tr,
                                        nIdPlantilla,
                                        int.Parse(aDatosPosicion[1]),
                                        Utilidades.unescape(aDatosPosicion[2]),
                                        Utilidades.unescape(aDatosPosicion[3]),
                                        float.Parse(aDatosPosicion[4]),
                                        decimal.Parse(aDatosPosicion[5]),
                                        (aDatosPosicion[6] == "") ? 0 : float.Parse(aDatosPosicion[6]),
                                        (aDatosPosicion[7] == "") ? 0 : decimal.Parse(aDatosPosicion[7])
                                        );
                        break;
                    case "D":
                        PLANTILLAPOSICIONFAC.Delete(tr,
                                        nIdPlantilla,
                                        int.Parse(aDatosPosicion[1])
                                        );
                        break;
                }
            }
            #endregion


            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nIdPlantilla.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la orden", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string getPosiciones(string st629_idplantillaof, string sModoAcceso)
    {
        StringBuilder sb = new StringBuilder();
        string sClase = "";
        try
        {
            if (sModoAcceso == "W") sClase = "MANO";
            sb.Append("<table id='tblPosiciones' class='" + sClase + "' style='width: 880px;' cellpadding='2'>");
            sb.Append("    <colgroup>");
            sb.Append("        <col style='width:15px;' />");
            sb.Append("        <col style='width:20px;' />");
            sb.Append("        <col style='width:425px;' />");
            sb.Append("        <col style='width:70px;' />");
            sb.Append("        <col style='width:70px;' />");
            sb.Append("        <col style='width:70px;' />");
            sb.Append("        <col style='width:70px;' />");
            sb.Append("        <col style='width:100px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = PLANTILLAPOSICIONFAC.CatalogoByPlantilla(null, int.Parse(st629_idplantillaof));
            string strDatos = "";//, sToolTip = "";

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t630_posicion"].ToString() + "' ");
                sb.Append("bd='' ");
                //sb.Append("estado='" + dr["t611_estado"].ToString() + "' ");
                sb.Append("onclick='mm(event)' ");
                sb.Append("style='height:38px; vertical-align:top'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");

                //sToolTip = " title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[";
                //sToolTip += "<label style='width:150px;'>&nbsp;</label><br>";
                //sToolTip += "<label style='width:100px;'><b><u>Posición:</u></b></label><br>";
                //sToolTip += "<label style='width:100px;'>Estado:</label>" + dr["estado"].ToString() + "<br>";
                //sToolTip += "<label style='width:100px;'>Nº SUPER:</label>" + dr["t611_posicion"].ToString() + "<br>";

                //if (dr["t611_estado"].ToString() != "D")
                //{
                //    sToolTip += "<label style='width:100px;'>Nº pedido de venta:</label>" + dr["t611_posicion_pedsap"].ToString() + "<br>";
                //    sToolTip += "<label style='width:100px;'>Nº factura:</label>" + dr["t611_posicion_facsap"].ToString() + "<br><br>";
                //    sToolTip += "<label style='width:100px;'><b><u>Factura:</u></b></label><br>";
                //    sToolTip += "<label style='width:100px;'>Serie:</label>" + dr["t611_seriefactura"].ToString() + "<br>";
                //    sToolTip += "<label style='width:100px;'>Número:</label>" + dr["t611_numfactura"].ToString() + "<br><br>";
                //    sToolTip += "<label style='width:100px;'><b><u>Cronología:</u></b></label><br>";
                //    strDatos = dr["t611_fprocesada"].ToString();
                //    if (strDatos != "")
                //        strDatos = strDatos.Substring(0, strDatos.Length - 3);
                //    sToolTip += "<label style='width:100px;'>Procesada</label>" + strDatos + "<br>";
                //    strDatos = dr["t611_ffacturada"].ToString();
                //    if (strDatos != "")
                //        strDatos = strDatos.Substring(0, strDatos.Length - 3);
                //    sToolTip += "<label style='width:100px;'>Facturada</label>" + strDatos + "<br>";
                //    strDatos = dr["t611_fcontabilizada"].ToString();
                //    if (strDatos != "")
                //        strDatos = strDatos.Substring(0, strDatos.Length - 3);
                //    sToolTip += "<label style='width:100px;'>Contabilizada</label>" + strDatos + "<br>";
                //    strDatos = dr["t611_fanulada"].ToString();
                //    if (strDatos != "")
                //        strDatos = strDatos.Substring(0, strDatos.Length - 3);
                //    sToolTip += "<label style='width:100px;'>Anulada</label>" + strDatos;
                //}
                //sToolTip += "] hideselects=[off]\"";
                sb.Append("<td></td>");
                sb.Append("<td>");
                //sb.Append("<input type='text' class='txtM' style='width:420px;' value=\"" + dr["t611_denominacion"].ToString() + "\" maxlength='40' onkeyup='aG(1);fm(this);' />");
                sb.Append("<textarea class='txtMultiM' style='width:420px;' rows='2' onkeyup='aG(1);fm(event);'>" + dr["t630_descripcion"].ToString() + "</textarea>");
                sb.Append("</td>");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + float.Parse(dr["t630_unidades"].ToString()).ToString("N") + "\" onfocus='fn(this);' onkeyup='aG(1);fm(event);sip(this);' /></td>");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + decimal.Parse(dr["t630_preciounitario"].ToString()).ToString("N") + "\" onfocus='fn(this);' onkeyup='aG(1);fm(event);sip(this);' /></td>");
                strDatos = "";
                if ((float)dr["t630_dto_porcen"] > 0) strDatos = float.Parse(dr["t630_dto_porcen"].ToString()).ToString("N");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + strDatos + "\" onfocus='fn(this);' onkeyup='delDtoImporte(this);fm(event);sip(this);' /></td>");
                strDatos = "";
                if ((decimal)dr["t630_dto_importe"] > 0) strDatos = decimal.Parse(dr["t630_dto_importe"].ToString()).ToString("N");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + strDatos + "\" onfocus='fn(this);' onkeyup='delDtoPorcentaje(this);fm(event);sip(this);' /></td>");
                sb.Append("<td><input type='text' class='txtNumM' style='width:90px;' value=\"" + decimal.Parse(dr["importe"].ToString()).ToString("N") + "\" onfocus='this.selected=false;' readonly /></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las posiciones de una plantilla.", ex);
        }
    }
    private string getDocumentos(string st629_idplantillaof, string sModoAcceso, string sEstProy)
    {
        StringBuilder sb = new StringBuilder();
        bool bModificable;

        try
        {
            SqlDataReader dr;
            if (Utilidades.isNumeric(st629_idplantillaof))
                dr = PLANTILLADOCUOF.Catalogo(int.Parse(st629_idplantillaof));
            else
                dr = PLANTILLADOCUOF.CatalogoByUsuTicks(st629_idplantillaof);

            if (sModoAcceso == "R")
                sb.Append("<table id='tblDocumentos' style='width: 850px;'>");
            else
                sb.Append("<table id='tblDocumentos' class='texto MANO' style='width: 850px;'>");
            
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("    <col style='width:250px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                //Si el usuario es el autor del archivo, o es administrador, se permite modificar.
                if ((dr["t314_idusuario_autor"].ToString() == Session["UsuarioActual"].ToString() || SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()))
                {
                    if (sModoAcceso == "R")
                        bModificable = false;
                    else
                        bModificable = true;
                }
                else
                    bModificable = false;

                sb.Append("<tr style='height:20px;' id='" + dr["t631_iddocuplanof"].ToString() + "' onclick='mm(event);' sTipo='PL_OF' sAutor='" + dr["t314_idusuario_autor"].ToString() + "' onmouseover='TTip(event)'>");

                if (bModificable)
                    sb.Append("<td style='padding-left:3px;' class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR W290'>" + dr["t631_descripcion"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W280'>" + dr["t631_descripcion"].ToString() + "</nobr></td>");

                if (dr["t631_nombrearchivo"].ToString() == "")
                {
                    if (bModificable)
                        sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                    else
                        sb.Append("<td></td>");
                }
                else
                {
                    string sNomArchivo = dr["t631_nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                    //Si la persona que entra es el autor, o es administrador, se permite descargar.
                    //if (dr["t314_idusuario_autor"].ToString() == Session["UsuarioActual"].ToString() || SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    sb.Append("<td><img src=\"../../../../images/imgDescarga.gif\" width='16px' height='16px' class='MANO' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                   // else
                        //sb.Append("<td><img src=\"../../../../images/imgSeparador.gif\" width='16px' height='16px' style='vertical-align:bottom;'>");
                    if (bModificable)
                        sb.Append("&nbsp;<nobr class='NBR MA' style='width:260px;' ondblclick=\"modificarDoc(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                    else
                        sb.Append("&nbsp;<nobr class='NBR' style='width:260px;'>" + sNomArchivo + "</nobr></td>");
                }

                //sb.Append("<td><nobr class='NBR' style='width:90px;'>" + dr["autor"].ToString() + "</nobr></td>");
                if (bModificable)
                    sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR W240'>" + dr["autor"].ToString() + "</nobr></td></tr>");
                else
                    sb.Append("<td><nobr class='NBR W240'>" + dr["autor"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documentos de la plantilla de orden de facturación", ex);
        }
    }
    protected string EliminarDocumentos(string strIdsDocs)
    {
        string sResul = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region eliminar documentos

            string[] aDocs = Regex.Split(strIdsDocs, "##");

            foreach (string oDoc in aDocs)
            {
                PLANTILLADOCUOF.Delete(tr, int.Parse(oDoc));
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string getViasDePago(string sIDPSN, string sIdClienteRP)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            //string sCodExtCliEmpProy = SUPER.DAL.PROYECTOSUBNODO.GetCodigoExternoClienteProyecto(int.Parse(Request.QueryString["sT305IdProy"].ToString()));
            string sCodExtCliEmpProy = SUPER.DAL.PROYECTOSUBNODO.GetCodigoExternoClienteProyecto(int.Parse(sIDPSN));
            //SqlDataReader dr = VIAPAGO.Cliente(null, int.Parse(sIDPSN), int.Parse(sIdClienteRP));
            SqlDataReader dr = VIAPAGO.Cliente(null, sCodExtCliEmpProy, int.Parse(sIdClienteRP));
            while (dr.Read())
            {
                sb.Append(dr["ID"].ToString()+"##"+dr["DENOMINACION"].ToString()+"///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las vías de pago.", ex);
        }
    }

    private string getDestFactByOV(string sIdClienteRP, string sOV)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            CLIENTE oCliente = CLIENTE.ObtenerResponsablePago(null, int.Parse(sIdClienteRP), sOV);

            sb.Append(oCliente.t302_idcliente.ToString() +"@#@");
            sb.Append(oCliente.t302_denominacion +"@#@");
            sb.Append(oCliente.NIF +"@#@");
            sb.Append(oCliente.direccion + "@#@");
            sb.Append(oCliente.denominacion_SAP);

            return "OK@#@" + sb.ToString();
        }
        catch (NullReferenceException)
        {
            return "OK@#@@#@@#@@#@@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el destinatario de factura.", ex);
        }
    }

    private string getCondPagoDefecto(string sIdClienteRP, string sOV)
    {
        try
        {
            return "OK@#@" + CONDPAGO.CondicionPorDefecto(int.Parse(sIdClienteRP), sOV);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la condición de pago por defecto.", ex);
        }
    }

    private string setSolicitud(string sIdPlantilla, string sPlantilla, string sProyecto, string sTextoSolicitud)
    {
        ArrayList aListCorreo = new ArrayList();
        string sTO = "", sAsunto = "", sTexto = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            string sNombre = Session["DES_EMPLEADO_ENTRADA"].ToString();
            sTO = ConfigurationManager.AppSettings["CorreoPlazoOF"].ToString();
            sAsunto = "Solicitud de modificación de plazo de pago en plantilla de orden de facturación";

            sb.Append("<BR>" + sNombre + " solicita cambio en el plazo de pago de la plantilla de orden de facturacion " + (char)34 + Utilidades.unescape(sPlantilla) + (char)34 + " (nº: " + int.Parse(sIdPlantilla).ToString("#,###") + ")");
            sb.Append(" perteneciente al proyecto: " + (char)34 + Utilidades.unescape(sProyecto) + (char)34 + "<br><br>");

            sb.Append("<b>Texto de la solicitud</b>: " + Utilidades.unescape(sTextoSolicitud) + "<br><br>");

            sTexto = sb.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            aListCorreo.Add(aMail);

            if (aListCorreo.Count > 0)
                Correo.EnviarCorreos(aListCorreo);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al solicitar la modificación del plazo de pago.", ex);
        }
    }
}

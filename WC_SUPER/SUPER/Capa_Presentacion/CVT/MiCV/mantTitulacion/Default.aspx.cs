using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.BLL;
using SUPER.Capa_Negocio;
using System.Collections;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_CVT_MiCV_mantTitulacion_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sNombreDoc = "", sNombreExp = "", sTareasPendientes = "", sIDDocuAuxTit = "", sIDDocuAuxExpte = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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

                cargarCombos();

                if (!Page.IsPostBack)
                {
                    hdnIdficepi.Value = Utilidades.decodpar(Request.QueryString["iF"]);
                    this.hdnProfAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();

                    sIDDocuAuxTit = "SUPER-" + this.hdnProfAct.Value + "-TIT-" + DateTime.Now.Ticks.ToString();
                    sIDDocuAuxExpte = "SUPER-" + this.hdnProfAct.Value + "-EXPTE-" + DateTime.Now.Ticks.ToString();

                    //Compruebo si estoy en mi propio curriculum
                    if (this.hdnIdficepi.Value == this.hdnProfAct.Value)
                        this.hdnEsMiCV.Value = "S";

                    if (Request.QueryString["eA"] != null) 
                        hdnEsEncargado.Value = Utilidades.decodpar(Request.QueryString["eA"]);
                    if (Request.QueryString["iT"] != null) //idTitulacion
                        hdnIdTituloficepi.Value = Utilidades.decodpar(Request.QueryString["iT"]);
                    if (hdnIdTituloficepi.Value != "")//Titulo ya existente
                    {
                        cargarDatos(int.Parse(hdnIdTituloficepi.Value));
                    //    deshabilitarUsuario();
                    }

                    switch (hdnEstadoInicial.Value)
                    {
                        case "S": //Pte. cumplimentar (origen ECV)
                        case "T": //Pte. cumplimentar (origen Validador)
                            imgEstado.ImageUrl = "~/Images/imgEstadoCVTPenCumplimentar.png";
                            break;
                        case "O": //Pte. validar (origen ECV)
                        case "P": //Pte. validar (origen Validador)
                            //imgEstado.ImageUrl = "~/Images/imgEstadoCVTPenValidar.png";
                            imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                            break;
                        case "Y": //Pseudovalidado (origen ECV)
                        case "X": //Pseudovalidado (origen Validador)
                            imgEstado.ImageUrl = "~/Images/imgEstadoCVTPseudovalidado.png";
                            break;
                        case "B": //Borrador
                            imgEstado.ImageUrl = "~/Images/imgEstadoCVTBorrador.png";
                            //imgHistorial.Style.Add("visibility", "hidden");
                            break;
                        case "R": //No Interesante
                            //imgEstado.ImageUrl = "~/Images/imgEstadoCVTNoInteresante.png";
                            imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                            break;
                        case "V": //Validado
                            //imgEstado.ImageUrl = "~/Images/imgEstadoCVTValidado.png";
                            imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                            break;
                    }

                    if (hdnEsEncargado.Value == "1")// || hdnIdtitulacion.Value != "")
                        omitirObligParaEncargado();
                    
                    bool bEsValidador = false;
                    if (Request.QueryString["v"] != null)
                    {
                        if (Request.QueryString["v"] == "1")
                            bEsValidador = true;
                        else
                            bEsValidador = false;
                    }

                    bool bEstaDeBaja = SUPER.BLL.Profesional.EstaDeBaja(int.Parse(hdnIdficepi.Value));
                    ArrayList aBotones = Curriculum.getBotonesAMostrar(hdnEstadoInicial.Value,
                                                                    (this.hdnEsMiCV.Value=="S") ? true : false, bEsValidador, false);

                    for (int i = 0; i < aBotones.Count; i++)
                    {
                        switch ((int)aBotones[i])
                        {
                            //case (int)CVT.Accion.Aparcar:  btnAparcar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Enviar: 
                                //23/10/2013 Dice María que sino está marcado como finalizado no se puede enviar a validar
                                //10/09/2015 Dice María que como solo se muestra el botón Grabar, no hay que hacer nada
                                //if (this.chkFinalizado.Checked)
                                    btnEnviar.Style.Add("display", "inline-block");
                                this.hdnPermiteEnviarValidar.Value = "S";
                                break;
                            case (int)CVT.Accion.Cumplimentar: 
                                if (!bEstaDeBaja) 
                                    btnCumplimentar.Style.Add("display", "inline-block"); 
                                break;
                            case (int)CVT.Accion.Validar: btnValidar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Pseudovalidar: if (!bEstaDeBaja) btnPseudovalidar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Rechazar: btnRechazar.Style.Add("display", "inline-block"); break;
                        }
                    }
                }
                if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    sTareasPendientes = Curriculum.MiCVTareasPendientes(3, int.Parse(hdnIdficepi.Value), (hdnIdTituloficepi.Value=="")? 0:int.Parse(this.hdnIdTituloficepi.Value),null);
                //Compruebo si en el historial la última acción fué enviar a cumplimentar, en cuyo caso cargo el mensaje
                //que el validador le quiere hacer llegar al profesional
                this.hdnMsgCumplimentar.Value = SUPER.BLL.Historial.GetMsgPdteValidar("TITULOFICEPICRONO",
                                                                                      (hdnIdTituloficepi.Value == "") ? -1 : int.Parse(this.hdnIdTituloficepi.Value), 
                                                                                      null);
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al cargar la pagina", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getDatosTitulo"):
                sResultado += getDatosTitulo(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], int.Parse(aArgs[2].Trim()), aArgs[3],
                                     Utilidades.unescape(aArgs[4]), aArgs[5], aArgs[6],
                                     aArgs[7], aArgs[8], Utilidades.unescape(aArgs[9]), Utilidades.unescape(aArgs[10]),
                                     aArgs[11], aArgs[12],
                                     Utilidades.unescape(aArgs[13]), Utilidades.unescape(aArgs[14]), Utilidades.unescape(aArgs[15]),
                                     aArgs[16], Utilidades.unescape(aArgs[17]), aArgs[18], aArgs[19], aArgs[20], aArgs[21],
                                     short.Parse(aArgs[22]), aArgs[23], aArgs[24], aArgs[25], aArgs[26], aArgs[27]
                                     );
                break;
            case ("documentos"):
                sCad = getDocumento(aArgs[1], aArgs[2]);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad;
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
    public string Grabar(string t012_idtituloficepi, int idFicepi, string sIdTitulacion, string sTitulo, string sTipo, string sModalidad,
                            string sTic,string sFinalizado, string sEspecialidad, string sCentro, string sInicio, string sFin,
                            string sNombreDoc, string sNombreExp, string sObs, string sEstadoNuevo, string sMotivoR,
                            string sCambioDoc, string sCambioExpte, string sEstadoInicial, string sOperacion, short idTitulacionIni,
                            string sUsuTickTit, string sUsuTickExpte, string sIdContentServer, string sIdContentServerExpte, string sEsMiCV)
    {
        string sRes = "OK@#@"; 
        try
        {
            long? idContentServer = null;
            long? idContentServerExpte = null;
            #region Atenea
            if (sUsuTickTit.Trim() != "" && sNombreDoc.Trim() != "")//Recupero el idDocumento de la tabla Temporal
            {
                SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sUsuTickTit);
                idContentServer = oDoc.t2_iddocumento;
            }
            else
            {
                if (sIdContentServer != "")
                    idContentServer = long.Parse(sIdContentServer);
            }
            if (sUsuTickExpte.Trim() != "" && sNombreExp.Trim() != "")//Recupero el idDocumento de la tabla Temporal
            {
                SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sUsuTickExpte);
                idContentServerExpte = oDoc.t2_iddocumento;
            }
            else
            {
                if (sIdContentServerExpte != "")
                    idContentServerExpte = long.Parse(sIdContentServerExpte);
            }
            #endregion

            TituloFicepi.Grabar((t012_idtituloficepi == "") ? null : (int?)int.Parse(t012_idtituloficepi),
                                sTitulo, (sIdTitulacion == "") ? (short)-1 : short.Parse(sIdTitulacion),
                                idFicepi, ((sTipo == "") ? null : (byte?)byte.Parse(sTipo)),
                                ((sModalidad == "") ? null : (byte?)byte.Parse(sModalidad)),
                                (sTic == "S") ? true : false,
                                (sFinalizado == "S") ? true : false,
                                sEspecialidad, sCentro,
                                ((sInicio == "") ? null : (short?)short.Parse(sInicio)),
                                ((sFin == "") ? null : (short?)short.Parse(sFin)),
                                sNombreDoc, sNombreExp, sObs, char.Parse(sEstadoNuevo), sMotivoR, int.Parse(Session["IDFICEPI_ENTRADA"].ToString()),
                                bool.Parse(sCambioDoc), bool.Parse(sCambioExpte), char.Parse(sEstadoInicial), sOperacion, idTitulacionIni, 
                                idContentServer, idContentServerExpte, sEsMiCV);

            if (sUsuTickTit.Trim() != "")
            {   //Marco el documento como asignado para que el trigger no lo borre de Atenea
                if (idContentServer != null)
                    SUPER.DAL.DocuAux.Asignar(null, (long)idContentServer);
                //Borro el documento de la tabla temporal
                SUPER.DAL.DocuAux.BorrarDocumento(null, "A", sUsuTickTit);
            }
            if (sUsuTickExpte.Trim() != "")
            {   //Marco el documento como asignado para que el trigger no lo borre de Atenea
                if (idContentServerExpte != null)
                    SUPER.DAL.DocuAux.Asignar(null, (long)idContentServerExpte);
                //Borro el documento de la tabla temporal
                SUPER.DAL.DocuAux.BorrarDocumento(null, "A", sUsuTickExpte);
            }
        }
        catch (Exception ex)
        {
            sRes = "Error@#@" + ex.Message;
        }
        return sRes;
    }

    public void cargarCombos()
    {
        cboTipo.DataSource = TituloFicepi.obtenerTipos();
        cboTipo.DataValueField = "sValor";
        cboTipo.DataTextField = "sDenominacion";
        cboTipo.DataBind();

        cboModalidad.DataSource = TituloFicepi.obtenerModalidades();
        cboModalidad.DataValueField = "sValor";
        cboModalidad.DataTextField = "sDenominacion";
        cboModalidad.DataBind();

        cboInicio.DataSource = TituloFicepi.obtenerAños();
        cboInicio.DataValueField = "sValor";
        cboInicio.DataTextField = "sDenominacion";
        cboInicio.DataBind();

        cboFin.DataSource = cboInicio.DataSource;
        cboFin.DataValueField = "sValor";
        cboFin.DataTextField = "sDenominacion";
        cboFin.DataBind();
    }
    public void cargarDatos(int idTitulacionFicepi) {
        try {
            TituloFicepi tf = TituloFicepi.Select(idTitulacionFicepi);
            txtTitulo.Value = tf.T019_DESCRIPCION.ToString();
            hdnIdTituloficepi.Value = tf.T012_IDTITULOFICEPI.ToString();
            hdnIdtitulacion.Value = tf.T019_IDCODTITULO.ToString();
            hdnIdtitulacionIni.Value = tf.T019_IDCODTITULO.ToString();
            if (tf.t019_tipo != null)
            {
                cboTipo.SelectedValue = tf.t019_tipo.ToString();
            }
            if (tf.t019_modalidad != null)
            {
                cboModalidad.SelectedValue = tf.t019_modalidad.ToString();
            }
            if (tf.T839_IDESTADO != "B")
            {
                cboTipo.Enabled = false;
                cboModalidad.Enabled = false;
                chkTIC.Disabled = true;
            }
            //Revisar. Poner un solo radio button con dos opciones.
            chkTIC.Checked = tf.t019_tic;
            chkFinalizado.Checked = tf.T012_FINALIZADO;
            txtEspecialidad.Text = tf.T012_ESPECIALIDAD.ToString();
            txtCentro.Text = tf.T012_CENTRO.ToString();
            if (tf.T012_INICIO != null)
                cboInicio.SelectedValue = tf.T012_INICIO.ToString();
            if (tf.T012_FIN != null)
                cboFin.SelectedValue = tf.T012_FIN.ToString();
            txtObservaciones.Text = tf.T012_OBSERVA.ToString();
            hdnEstadoInicial.Value = tf.T839_IDESTADO.ToString();
            if (tf.T019_ESTADO)
                hdnTituloEstadoIni.Value = "V";//Validado
            else
                hdnTituloEstadoIni.Value = "N";//No Validado
            //txtMotivoRT.Text = tf.t597_motivort.ToString();
            if (tf.T839_IDESTADO == "S"
                    || tf.T839_IDESTADO == "T") //Pendiente de cumplimentar.
            {
                //imgEstado.Attributes.Add("title", tf.t597_motivort.ToString());
                //imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(tf.t597_motivort.ToString()) + "\",\"Motivo\",null,300)");
                //imgEstado.Attributes.Add("onmouseout", "hideTTE()");
                imgInfoEstado.Style.Add("visibility", "visible");
                imgInfoEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(tf.t597_motivort.ToString()) + "\",\"Motivo\",null,300)");
                imgInfoEstado.Attributes.Add("onmouseout", "hideTTE()");
            }
            if (tf.T839_IDESTADO != "V")
            {
                imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(Curvit.ToolTipEstados(tf.T839_IDESTADO)) + "\",\"Información\",null,300)");
                imgEstado.Attributes.Add("onmouseout", "hideTTE()");
            }
            //if (tf.T012_DOCTITULO != null) 
            if (tf.t2_iddocumento != null) 
            {
                txtNombreDocumento.Text = tf.T012_NDOCTITULO;
                imgUploadDoc.Style.Add("display", "inline-block");
                imgDownloadDoc.Style.Add("display", "inline-block");
                imgBorrarDoc.Style.Add("display", "inline-block");
                imgDownloadDoc.Attributes.Add("onclick", "descargar('CVTDOCTIT','" + tf.T012_IDTITULOFICEPI.ToString() + "')");
            }
            //if (tf.T012_DOCEXPDTE != null)
            if (tf.t2_iddocumentoExpte != null) 
            {
                txtNombreExpediente.Text = tf.T012_NDOCEXPDTE;
                imgUploadExp.Style.Add("display", "inline-block");
                imgDownloadExp.Style.Add("display", "inline-block");
                imgBorrarExp.Style.Add("display", "inline-block");
                imgDownloadExp.Attributes.Add("onclick", "descargar('CVTDOCEX','" + tf.T012_IDTITULOFICEPI.ToString() + "')");
            }
            this.hdnNombreDocInicial.Value = tf.T012_NDOCTITULO;
            this.hdnContentServer.Value = tf.t2_iddocumento.ToString();
            this.hdnNombreDocInicialExpte.Value = tf.T012_NDOCEXPDTE;
            this.hdnContentServerExpte.Value = tf.t2_iddocumentoExpte.ToString();
        }
        catch (Exception ex) 
        {
            hdnErrores.Value = Errores.mostrarError("Error al cargar datos", ex);
        }
    }

    public void omitirObligParaEncargado() {
        try
        {
            foreach (Control ctrl in Page.FindControl("formTituloFicepi").Controls) {//Ocultar asteriscos para el encargado de curriculums
                if (ctrl.GetType().Name == "Label") {
                    if (((Label)ctrl).Text == "*") {
                        ((Label)ctrl).Style["display"] = "none";
                    }
                }
            }
        }
        catch (Exception ex) {
            hdnErrores.Value = Errores.mostrarError("Error al omitir obligatoriedad", ex);
        }
    }
    public string getDatosTitulo(string idTitulo)
    {
        try
        {
            Titulacion oT = Titulacion.Select(int.Parse(idTitulo));
            
            return "OK@#@" + oT.t019_tipo.ToString() +"@#@"+  oT.t019_modalidad.ToString() +"@#@"+ (((bool)oT.t019_tic)?"1":"0");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar la página.", ex);
        }
    }
    private string getDocumento(string sUsuTicks, string sTipo)
    {
        string sRes = "";
        try
        {
            if (Utilidades.isNumeric(sUsuTicks))
            {
                SUPER.BLL.Titulacion oTit = SUPER.BLL.Titulacion.SelectDocs(null, int.Parse(sUsuTicks));
                if (sTipo=="TAD")
                    sRes = oTit.NDOC + "@#@S@#@" + oTit.t2_iddocumento.ToString();
                else
                    sRes = oTit.NDOCEXPTE + "@#@S@#@" + oTit.t2_iddocumentoExpte.ToString();
            }
            else
            {
                SUPER.BLL.DocuAux oDoc = SUPER.BLL.DocuAux.GetDocumento(null, sUsuTicks);
                sRes = oDoc.t686_nombre + "@#@N@#@" + sUsuTicks;
            }
            sRes += "@#@" + sTipo;

            return sRes;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documento", ex);
        }
    }

}

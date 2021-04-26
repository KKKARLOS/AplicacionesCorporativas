using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlClient;
//Para el stringBuilder
using System.Text;
using SUPER.BLL;

public partial class Capa_Presentacion_CVT_MiCV_mantCertificado_Default : System.Web.UI.Page, ICallbackEventHandler
{
    public string sErrores = "";
    private string _callbackResultado = null;
    protected HttpPostedFile ArchivoExam, ArchivoCert;
    protected byte[] ArchivoEnBinarioCert = null;
    public string sNombreCert = "", sIDDocuAux = "";
    public string strTablaHTML = "";

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

                if (!Page.IsPostBack)
                {
                    sIDDocuAux = "SUPER-" + Session["IDFICEPI_CVT_ACTUAL"].ToString() + "-" + DateTime.Now.Ticks.ToString();
                    #region Lleno combos
                    //cboEntCert.DataSource = Examen.obtenerEntCert(8, 1); //tipo 8-->entidad certificadora y activo=1
                    //cboEntCert.DataValueField = "sValor";
                    //cboEntCert.DataTextField = "sDenominacion";
                    //cboEntCert.DataBind();
                    //Que solo muestre los validados
                    //cboEntorno.DataSource = EntornoTecno.obtenerCboEntorno(int.Parse("0"));
                    //cboEntorno.DataValueField = "sValor";
                    //cboEntorno.DataTextField = "sDenominacion";
                    //cboEntorno.DataBind();
                    #endregion
                    hdnIdFicAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();
                    //if ((Request.QueryString["iF"] != null) && (Request.QueryString["iF"] != ""))
                        hdnIdFicepi.Value = Utilidades.decodpar(Request.QueryString["iF"]);
                    //else
                    //    hdnIdFicepi.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();

                    if (Request.QueryString["eA"] != null && (Request.QueryString["eA"] != ""))
                        hdnEsEncargado.Value = Utilidades.decodpar(Request.QueryString["eA"]);
                    if (Request.QueryString["n"] != null && (Request.QueryString["n"] != ""))
                        hdnNomProf.Value = Utilidades.decodpar(Request.QueryString["n"]);

                    //Compruebo si estoy en mi propio curriculum
                    if (hdnIdFicepi.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                        this.hdnEsMiCV.Value = "S";

                    if (Request.QueryString["iC"] != null && Request.QueryString["iC"] != "")
                    {
                        hdnIdCertificadoInicial.Value = Utilidades.decodpar(Request.QueryString["iC"]);
                        CargarDatos(int.Parse(hdnIdCertificadoInicial.Value), int.Parse(hdnIdFicepi.Value));
                    }
                    else //Nuevo
                    {
                        btnNuevo.Style.Add("display", "inline-block");
                        //btnAceptar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                        btnEnviar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                        this.lblBuscar.Visible = true;
                    }

                    #region Mostrar icono estado
                    switch (hdnEstado.Value)
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
                            if (this.hdnDocRechazado.Value == "S")
                                imgEstado.ImageUrl = "~/Images/imgEstadoCVTDocNoValido.png";
                            else
                                imgEstado.ImageUrl = "~/Images/imgEstadoCVTPseudovalidado.png";
                            break;
                        case "B": //Borrador
                            imgEstado.ImageUrl = "~/Images/imgEstadoCVTBorrador.png";
                            break;
                        case "R": //No Interesante
                            //imgEstado.ImageUrl = "~/Images/imgEstadoCVTNoInteresante.png";
                            imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                            break;
                        case "I"://Certificado incompleto
                            imgEstado.ImageUrl = "~/Images/imgEstadoCVTIncompleto.png";
                            break;
                        case "V": //Validado
                            //imgEstado.ImageUrl = "~/Images/imgEstadoCVTValidado.png";
                            imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                            break;
                    }
                    #endregion

                    if (hdnEsEncargado.Value == "1")
                        omitirObligParaEncargado();

                    strTablaHTML = CargarExamenes(int.Parse(hdnIdFicepi.Value), int.Parse(hdnIdCertificadoInicial.Value));
                }
                /*
                else
                {
                    strTablaHTML = Examen.CatalogoFicepi(-1, -1);
                    if (hdnOP.Value == "1" || hdnOP.Value == "3")
                    {
                        hdnIdCertificadoInicial.Value = grabar(this.hdnAccBorr.Value).ToString();//para devolver en el init en caso de ser nuevo o de haber cambiado
                    }
                    else
                    {//Venimos de seleccionar un certificado en la búsqueda avanzada
                        if (hdnOP.Value == "4" && hdnIdCertificadoInicial.Value != "")
                        {
                            imgEstado.ImageUrl = "~/Images/imgEstadoCVTBorrador.png";
                            PonerCertificado(int.Parse(hdnIdCertificadoInicial.Value));
                        }
                    }
                }
                 * */
                //Compruebo si el certificado está pendiente de cumplimentar, en cuyo caso cargo el mensaje
                //que el validador le quiere hacer llegar al profesional
                this.hdnMsgCumplimentar.Value = SUPER.BLL.Historial.GetMsgPdteValidar("CERTIFICADO", int.Parse(hdnIdFicepi.Value),
                                                                                      (this.hdnIdCertificadoInicial.Value == "") ? -1 : int.Parse(this.hdnIdCertificadoInicial.Value));
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
            hdnErrores.Value = Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; 
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += "OK@#@" + Grabar(int.Parse(aArgs[1]), int.Parse(aArgs[2]), int.Parse(aArgs[3]), int.Parse(aArgs[4]),
                                               Utilidades.unescape(aArgs[5]), aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11]);
                break;
            case ("documentos"):
                sCad = getDocumento(aArgs[1], aArgs[5]);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad;
                break;
            case ("eliminar"):
                sResultado += "OK@#@" + BorrarCertificado(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("getdatos"):
                sResultado += "OK@#@" + GetDatosCertificado(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("examenes"):
                sResultado += "OK@#@" + CargarExamenes(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("recargarExamenes"):
                sResultado += "OK@#@" + ReCargarExamenes(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("viaCompletada1"):
            case ("viaCompletada2"):
                sResultado += "OK@#@" + ViaCompletada(int.Parse(aArgs[1]), aArgs[2], aArgs[3], int.Parse(aArgs[4]));
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

    public string GetDatosCertificado(int idFicepi, int idCert)
    {
        StringBuilder sb = new StringBuilder();
        Certificado o = Certificado.Select(idCert, idFicepi);
        sb.Append(o.T582_NOMBRE + "///");
        sb.Append(o.T582_ABREV + "///");
        sb.Append(o.T036_IDCODENTORNO.ToString() + "///");//Código Entorno tecnologico
        sb.Append(o.T576_IDCRITERIO.ToString() + "///");//Código entidad certificadora
        sb.Append(o.EntidadCertificadora + "///");
        sb.Append(o.EntornoTecnologico + "///");
        sb.Append(o.FOBTENCION + "///");//fecha de obtencion del certificado
        sb.Append(o.FCADUCIDAD + "///");
        sb.Append(o.EstadoCertificado + "///");//Estado del certificado
        sb.Append(o.t2_iddocumento.ToString() + "///");//identificador del documento en Atenea
        if (o.DocRechazado)//10
            sb.Append("S///");
        else
            sb.Append("N///");
        sb.Append(o.T593_NDOC + "///");//Nombre del documento acreditativo del certificado
        
        if (o.BDOC)//12
            sb.Append("S///");
        else
            sb.Append("N///");
        
        if (o.Completado)//13
            sb.Append("S///");
        else
            sb.Append("N///");
        
        if (o.T582_VALIDO)//14
            sb.Append("S///");
        else
            sb.Append("N///");
        sb.Append(o.MOTIVORT + "///");//Motivo de rechazo
        sb.Append(Utilidades.escape(Curvit.ToolTipEstados(o.EstadoCertificado)));//Denominación del estado
        return sb.ToString();
    }
    public void CargarDatos(int idCert, int idFicepi)
    {
        Certificado o = Certificado.Select(idCert, idFicepi);

        txtDenom.Value = o.T582_NOMBRE;
        txtAbrev.Text = o.T582_ABREV;
        //cboEntCert.SelectedValue = o.T576_IDCRITERIO.ToString();
        //cboEntorno.SelectedValue = o.T036_IDCODENTORNO.ToString();
        this.hdnIdEntorno.Value = o.T036_IDCODENTORNO.ToString();
        this.hdnIdEntCert.Value = o.T576_IDCRITERIO.ToString();
        this.txtEntCert.Text = o.EntidadCertificadora;
        this.txtEntorno.Text = o.EntornoTecnologico;

        txtFechaO.Text = o.FOBTENCION;
        txtFechaC.Text = o.FCADUCIDAD;
        //hdnEstado.Value = o.T839_IDESTADO;
        hdnEstado.Value = o.EstadoCertificado;
        hdnIdCertificadoInicial.Value = o.T582_IDCERTIFICADO.ToString();
        hdnCVTCert.Value = o.T582_IDCERTIFICADO.ToString();
        hdnIdFicepiCert.Value = o.IdFicepiCert.ToString();

        this.hdnContentServer.Value = o.t2_iddocumento.ToString();

        if (o.DocRechazado)
            this.hdnDocRechazado.Value = "S";
        else
            this.hdnDocRechazado.Value = "N";

        if (o.EstadoCertificado == "S" || o.EstadoCertificado == "T" || //Pendiente de cumplimentar.
            o.EstadoCertificado == "X" || o.EstadoCertificado == "Y")//Pendiente de Anexar
        {
            string sMotivoRechazo = o.MOTIVORT;
            if (sMotivoRechazo == "") 
                sMotivoRechazo = "El certificado carece de documento acreditativo";
            else 
                sMotivoRechazo = Utilidades.escape(o.MOTIVORT.ToString());
            imgInfoEstado.Style.Add("visibility", "visible");
            imgInfoEstado.Attributes.Add("onmouseover", "showTTE(\"" + sMotivoRechazo + "\",\"Motivo\",null,300)");
            imgInfoEstado.Attributes.Add("onmouseout", "hideTTE()");
            this.hdnMotivo.Value = sMotivoRechazo;
            this.txtMotivoRT.Text = o.MOTIVORT.ToString();
        }
        if (o.EstadoCertificado != "V")
        {
            imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(Curvit.ToolTipEstados(o.EstadoCertificado)) + "\",\"Información\",null,300)");
            imgEstado.Attributes.Add("onmouseout", "hideTTE()");
        }

        //12/06/2018 En caso de que exista documento pero el certificado no esté validado, se podrá realizar cambios en documento
        //En caso de que no exista documento, se podrá adjuntar documento.
        imgUploadDoc.Style.Add(HtmlTextWriterStyle.Display, "inline-block");        
        if (o.BDOC)
        {
            imgDownloadDoc.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            txtNombreDocumento.Text = o.T593_NDOC;
            this.hdnNombreDocInicial.Value = o.T593_NDOC;          
            
            //if (hdnCaso.Value != "2" || hdnEstado.Value != "V")//Siempre se podrá eliminar
            if (!o.Completado || o.EstadoCertificado != "V")//Siempre se podrá eliminar
            {
                imgBorrarDoc.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            }
            else
            {
                imgUploadDoc.Style.Add(HtmlTextWriterStyle.Display, "none");
                imgBorrarDoc.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            imgDownloadDoc.Attributes.Add("onclick", "verDOC()");
        }
        

        if (o.T582_VALIDO)
            this.hdnValido.Value = "S";
        else
            this.hdnValido.Value = "N";

        //Deshabilitar o no los campos correspondientes al certificado
        //Certificado completados o certificados validos o aquellos que alguno de sus exámenes no esté en pend. validar/borrador
        //if ((hdnCaso.Value == "1") || (hdnCaso.Value == "2") || o.T582_VALIDO || (hdnCaso.Value != "5" && hdnCaso.Value != "6")) 
        //if (o.Completado && (o.EstadoCertificado == "V" || o.EstadoCertificado == "X" || o.EstadoCertificado == "Y"))

        //20/02/2014 Solo estará habilitado si es un certificado nuevo o está en borrador o Pdte Validar o Pdte Cumplimentar
        //12/06/2018 Cualquier modificación sobre certificados se hará a través de una nueva solicitud
        /*if (o.EstadoCertificado != "B" && o.EstadoCertificado != "O" && o.EstadoCertificado != "P" && 
            o.EstadoCertificado != "S" && o.EstadoCertificado != "T")
        {*/
            deshabilitarCertificado();
            omitirObligParaEncargado();
        //}

        //Tratamiento especial para la botonera
        //12/06/2018 Cualquier modificación sobre certificados se hará a través de una nueva solicitud
        btnAceptar.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnEnviar.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnNuevo.Style.Add("display", "none");
        //if (hdnCaso.Value == "1" || hdnCaso.Value == "2")
        if (o.Completado)
        {
            this.hdnCompletado.Value = "S";
            if (o.EstadoCertificado == "V")
            {
                btnCancelar.Style.Add(HtmlTextWriterStyle.Display, "none");
                btnSalir.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            }
            else
            {
                //if (o.EstadoCertificado == "B")
                //    btnAparcar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                //else
                     btnEnviar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                //if (hdnEsEncargado.Value == "1")
                //{
                //    if (this.hdnEsMiCV.Value != "S")
                //    {
                //        bool bEstaDeBaja = SUPER.BLL.Profesional.EstaDeBaja(int.Parse(hdnIdFicepi.Value));
                //        if (!bEstaDeBaja)
                //            btnCumplimentar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                //    }
                //}
                //else if (o.EstadoCertificado == "B")
                //{
                //    btnAparcar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                //}
            }
        }
        else
        {//Certificados no completos            
            btnAceptar.Style.Add(HtmlTextWriterStyle.Display, "none");
            btnEnviar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            //12/06/2018 Cualquier modificación sobre certificados se hará a través de una nueva solicitud
            //btnNuevo.Style.Add("display", "inline-block");
        }
    }
    public void PonerCertificado(int idCertificado)
    {
        Certificado o = SUPER.BLL.Certificado.GetDatos(null, idCertificado);
        this.hdnCVTCert.Value = idCertificado.ToString();
        txtDenom.Value = o.T582_NOMBRE;
        txtAbrev.Text = o.T582_ABREV;
        this.hdnIdEntorno.Value = o.T036_IDCODENTORNO.ToString();
        this.hdnIdEntCert.Value = o.T576_IDCRITERIO.ToString();
        this.txtEntCert.Text = o.EntidadCertificadora;
        this.txtEntorno.Text = o.EntornoTecnologico;

        if (o.T582_VALIDO)
            this.hdnValido.Value = "S";
        else
            this.hdnValido.Value = "N";

    }
    private string CargarExamenes(int idFicepi, int idCertificado)
    {
        return Examen.CatalogoFicepi(idCertificado, idFicepi);
    }
    private string ReCargarExamenes(int idFicepi, int idCertificado)
    {
        string sRes = "";
        SUPER.BLL.Certificado oCert = SUPER.BLL.Certificado.Select(idCertificado, idFicepi);
        sRes+= Examen.CatalogoFicepi(idCertificado, idFicepi);
        if (oCert.Completado)
            sRes += "@#@S";
        else
            sRes += "@#@N";
        return sRes;
    }
    private string BorrarCertificado(int idCertificado, int idFicepi)
    {
        Certificado.BorrarProfesional(null, idCertificado, idFicepi);
        return "OK";
    }

    public void deshabilitarCertificado()
    {
        try
        {
            omitirObligParaEncargado();
            txtDenom.Disabled = true;
            //txtNombreDocumento.Enabled = false;
            //txtAbrev.Disabled = true;
            //txtFechaO.Enabled = false;
            //txtFechaC.Enabled = false;
            //cboEntCert.Enabled = false;
            //cboEntorno.Enabled = false;
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al deshabilitar controles", ex);
        }
    }

    public int Grabar(int t001_idficepi, int idCertOld, int idCertNew, int idFicepiCert, string sDenDoc, string sUsuTick, string sEstado,
                      string sMotivo, string sCertValido, string slExamenes, string sIdContentServer)
    {
        bool bTodosBorrados = true, bCertValido = false;

        /* En certificados un Encargado no puede validar pues se hará desde RRHH
         * Por tanto para un encargado, las opciones serán o enviar a validar o enviar a cumplimentar
         */
        #region Inicio Transacción

        SqlConnection oConn;
        SqlTransaction tr;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            throw (new Exception("Error al abrir la conexion", ex));
        }

        #endregion

        try
        {
            if (sCertValido == "S")
                bCertValido=true;
            if (sEstado != "")
            {
                if (idCertOld != -1 && idFicepiCert == -1) //Quiere ir a por un certificado sugerido
                {
                    sEstado = "P";
                    //idCertNew = idCertOld;
                }
            }
            #region Tramitar certificado
            long? idDocAtenea = null;
            if (sUsuTick.Trim() != "")//Recupero el idDocumento de la tabla Temporal
            {
                SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(tr, sUsuTick);
                idDocAtenea = oDoc.t2_iddocumento;
            }
            if (idFicepiCert == -1 || idCertOld != idCertNew)
            {
                if (sEstado == "X" || sEstado == "Y")
                {//Si estaba pdte de anexar y lo vuelvo a enviar a validar, borro el motivo por el que el documento no era válido
                    sMotivo = "";
                }
                if (sUsuTick.Trim() == "")
                {
                    if (sIdContentServer != "")
                        idDocAtenea = long.Parse(sIdContentServer);
                }
                SUPER.BLL.Certificado.InsertarProfesional(tr, idCertNew, t001_idficepi, sDenDoc, idDocAtenea);
                //Si ha habido cambio de certificado comprobamos lo siguiente
                //1.- Que en FicepiCert no quede registro para ese certificado y ese profesional
                //2.- Que si el certificado es no válido (y por tanto provenía de propuesta del profesional) y no tiene ningún 
                //      FicepiCert colgando, hay que borrarlo de la tabla maestra de certificados
                if (idCertOld != idCertNew)
                {
                    if (idCertOld != -1)
                    {
                        SUPER.BLL.Certificado.BorrarProfesional(tr, idCertOld, t001_idficepi);
                        if (!bCertValido)
                        {
                            if (!SUPER.BLL.Certificado.TieneProfesionales(tr, idCertOld))
                                SUPER.BLL.Certificado.DeleteNoValido(tr, idCertOld);
                        }
                    }
                }
            }
            else
            {//Si solo ha cambiado el documento
                if (idDocAtenea != null)
                {
                    SUPER.BLL.Certificado.PonerDocumento(tr, t001_idficepi, idCertNew, (long)idDocAtenea, sDenDoc);
                }
            }
            SUPER.BLL.Certificado.PonerMotivo(tr, t001_idficepi, idCertNew, sMotivo);
            if (sUsuTick.Trim() != "")
            {   //Marco el documento como asignado para que el trigger no lo borre de Atenea
                if (idDocAtenea != null)
                    SUPER.DAL.DocuAux.Asignar(tr, (long)idDocAtenea);
                //Borro el documento de la tabla temporal
                SUPER.DAL.DocuAux.BorrarDocumento(tr, "C", sUsuTick);
            }
            #endregion
            #region Borro examenes marcados para borrado y creo array con los restantes
            string[] aFilas = Regex.Split(slExamenes, "@exa@");
            for (int i = 0; i < aFilas.Length; i++)
            {
                string[] aColumnas = Regex.Split(aFilas[i], "@col@");
                if (aColumnas[1] == "D")
                {//Borrado de un examen
                    SUPER.BLL.Examen.BorrarAsistente("B", t001_idficepi, int.Parse(aColumnas[0]));
                }
                else
                {
                    bTodosBorrados = false;
                }
            }
            //Si el certificado se ha quedado sin exámenes, lo borramos
            if (bTodosBorrados)
                SUPER.BLL.Certificado.DeleteNoValido(tr, idCertNew);
            #endregion

            Conexion.CommitTransaccion(tr);
            return idCertNew;
        }

        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            throw ex;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
    }

    public void omitirObligParaEncargado()
    {
        try
        {
            foreach (Control ctrl in Page.FindControl("formExamenCert").Controls)
            {//Ocultar asteriscos para el encargado de curriculums
                if (ctrl.GetType().Name == "Label")
                {
                    if (((Label)ctrl).Text == "*")
                    {
                        ((Label)ctrl).Style["display"] = "none";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al omitir obligatoriedad", ex);
        }
    }

    private bool HayExamenesUsadosEnOtrosCertificados(int idFicepi, int idCert)
    {
        bool bHay = false;

        //if (Convert.ToBoolean(hdnCambioExam.Value))
        //{
        //    string[] aFilas = Regex.Split(hdnExamenes.Value, "@examen@");
        //    for (int i = 0; i < aFilas.Length; i++)
        //    {
        //        string[] aColumnas = Regex.Split(aFilas[i], "@columna@");
        //        if (aColumnas[10] == "D")
        //        {
        //            if (SUPER.BLL.Examen.ExisteEnOtroCertificado(null, idFicepi, idCert, int.Parse(aColumnas[0])))
        //            {
        //                bHay = true;
        //                break;
        //            }
        //        }
        //    }
        //}

        return bHay;
    }

    private string CalcularEstadoCertificado(string sEstadoActual, string sEstadoExamen)
    {
        string sRes = sEstadoActual;
        switch (sEstadoActual)
        {
            case "B":
                switch (sEstadoExamen)
                {
                    case "O":
                    case "P":
                    case "S":
                    case "T":
                        sRes = sEstadoExamen;
                        break;
                }
                break;
        }
        return sRes;
    }
    private string ViaCompletada(int idCertificado, string sListaExamenes, string sEsMiCV, int t001_idficepi)
    {
        string sRes = "SI";
        if (sListaExamenes == "")
            sRes = "NO";
        else
        {
            bool bViaCompletada = SUPER.BLL.Certificado.ViaCompletada(null, idCertificado, sListaExamenes);
            if (!bViaCompletada) sRes = "NO";
            if (sEsMiCV == "S")
                SUPER.DAL.Curriculum.ActualizadoCV(null, t001_idficepi);
        }
        return sRes;
    }

    private string getDocumento(string sUsuTicks, string sIdFicepi)
    {
        string sRes = "";
        try
        {
            if (Utilidades.isNumeric(sUsuTicks))
            {
                SUPER.BLL.Certificado oCert = SUPER.BLL.Certificado.SelectDoc(null, int.Parse(sUsuTicks), int.Parse(sIdFicepi));
                sRes = oCert.T593_NDOC + "@#@S@#@" + oCert.t2_iddocumento.ToString();
            }
            else
            {
                SUPER.BLL.DocuAux oDoc = SUPER.BLL.DocuAux.GetDocumento(null, sUsuTicks);
                sRes = oDoc.t686_nombre + "@#@N@#@" + sUsuTicks;
            }

            return sRes;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documento del examen", ex);
        }
    }

}

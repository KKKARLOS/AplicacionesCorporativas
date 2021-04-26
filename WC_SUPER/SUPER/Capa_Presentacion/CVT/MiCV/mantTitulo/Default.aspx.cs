using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using SUPER.BLL;
using System.Collections;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_CVT_MiCV_mantTitulo_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sTareasPendientes = "", sIDDocuAux = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        try
        {
            //ficepi=" + ficepi + "&idioma=" + idioma + "&usuticks" + sIDDocuAux + "&idtitulo" + idtitulo;
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
                
                Utilidades.SetEventosFecha(this.txtFecha);

                if (!Page.IsPostBack)
                {
                    this.hdnIdFicepi.Value = Utilidades.decodpar(Request.QueryString["iF"]);
                    this.hdnUserAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();

                    sIDDocuAux = "SUPER-" + this.hdnUserAct.Value + "-" + DateTime.Now.Ticks.ToString();

                    //Compruebo si estoy en mi propio curriculum
                    if (this.hdnIdFicepi.Value == this.hdnUserAct.Value) this.hdnEsMiCV.Value = "S";

                    if (Request.QueryString["eA"] != null) hdnEsEncargado.Value = Utilidades.decodpar(Request.QueryString["eA"]);
                    if (Request.QueryString["iT"] != "") hdnIdTitulo.Value = Utilidades.decodpar(Request.QueryString["iT"]);
                    if (Request.QueryString["pantalla"] != "") hdnPantalla.Value = Utilidades.decodpar(Request.QueryString["pantalla"]);
                    if (Request.QueryString["iI"]!="")
                    {
                        hdnIdCodIdioma.Value = Utilidades.decodpar(Request.QueryString["iI"]);
                    }
                    if (Request.QueryString["dI"] != "" && Request.QueryString["dI"] != null)
                    {
                        txtIdioma.InnerText = Utilidades.decodpar(Request.QueryString["dI"]);
                    }

                    if (hdnIdTitulo.Value != "" && hdnIdTitulo.Value != "-1")
                    {//Titulo ya existente
                        CargarDatos(TituloIdiomaFic.Detalle(int.Parse(hdnIdTitulo.Value)));
                        //deshabilitarUsuario();
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
                    if (hdnEsEncargado.Value == "1")
                        omitirObligParaAdmin();

                    bool bEsValidador = false;
                    if (Request.QueryString["v"] != null)
                    {
                        if (Request.QueryString["v"] == "1")
                            bEsValidador = true;
                        else
                            bEsValidador = false;
                    }

                    bool bEstaDeBaja = SUPER.BLL.Profesional.EstaDeBaja(int.Parse(hdnIdFicepi.Value));
                    ArrayList aBotones = 
                        Curriculum.getBotonesAMostrar(hdnEstadoInicial.Value,
                                                     (this.hdnEsMiCV.Value=="S") ? true : false, 
                                                     bEsValidador, false);

                    for (int i = 0; i < aBotones.Count; i++)
                    {
                        switch ((int)aBotones[i])
                        {
                            //case (int)CVT.Accion.Aparcar: btnAparcar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Enviar: btnEnviar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Cumplimentar: if (!bEstaDeBaja) btnCumplimentar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Validar: btnValidar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Pseudovalidar: if (!bEstaDeBaja) btnPseudovalidar.Style.Add("display", "inline-block"); break;
                            case (int)CVT.Accion.Rechazar: btnRechazar.Style.Add("display", "inline-block"); break;
                        }
                    }   
                }
                if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    sTareasPendientes = Curriculum.MiCVTareasPendientes(7, int.Parse(hdnIdFicepi.Value), (hdnIdTitulo.Value == "-1") ? 0 : int.Parse(this.hdnIdTitulo.Value), null);
                //Compruebo si en el historial la última acción fué enviar a cumplimentar, en cuyo caso cargo el mensaje
                //que el validador le quiere hacer llegar al profesional
                this.hdnMsgCumplimentar.Value = SUPER.BLL.Historial.GetMsgPdteValidar("TITIDIOMAFICCRONO", int.Parse(hdnIdFicepi.Value),
                                                                                      (this.hdnIdTitulo.Value == "") ? -1 : int.Parse(this.hdnIdTitulo.Value));
            }
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al cargar la pagina", ex);
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
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), int.Parse(aArgs[2].Trim()), int.Parse(aArgs[3]),
                                     Utilidades.unescape(aArgs[4]), aArgs[5], Utilidades.unescape(aArgs[6]), aArgs[7], 
                                     Utilidades.unescape(aArgs[8]), aArgs[9], aArgs[10], aArgs[11], 
                                     Utilidades.unescape(aArgs[12]), aArgs[13], aArgs[14], aArgs[15]);
                break;
            case ("documentos"):
                sCad = getDocumento(aArgs[1]);
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
    public void CargarDatos(TituloIdiomaFic o)
    {
        if (o != null)
        {
            hdnEstadoInicial.Value= o.t839_idestado.ToString();
            hdnndoc.Value = o.T021_NDOC;

            this.hdnNombreDocInicial.Value = o.T021_NDOC;
            this.hdnContentServer.Value = o.t2_iddocumento.ToString();

            if (o.t839_idestado == char.Parse("S")
                    || o.t839_idestado == char.Parse("T")) //Pendiente de cumplimentar.
            {
                //imgEstado.Attributes.Add("title", tf.t597_motivort.ToString());
                //imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(o.T835_MOTIVORT.ToString()) + "\",\"Motivo\",null,300)");
                //imgEstado.Attributes.Add("onmouseout", "hideTTE()");
                imgInfoEstado.Style.Add("visibility", "visible");
                imgInfoEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(o.T835_MOTIVORT.ToString()) + "\",\"Motivo\",null,300)");
                imgInfoEstado.Attributes.Add("onmouseout", "hideTTE()");
            }
            if (o.t839_idestado!= char.Parse("V"))
            {
                imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(Curvit.ToolTipEstados(o.t839_idestado.ToString())) + "\",\"Información\",null,300)");
                imgEstado.Attributes.Add("onmouseout", "hideTTE()");
            }
            //if (o.T021_DOC != null)
            if (o.t2_iddocumento != null)
            {
                txtNombreDocumento.Text = o.T021_NDOC;
                imgUploadDoc.Style.Add("display", "inline-block");
                imgDownloadDoc.Style.Add("display", "inline-block");
                imgBorrarDoc.Style.Add("display", "inline-block");
                imgDownloadDoc.Attributes.Add("onclick", "verDOC()");
            }

            txtTitulo.Value = o.T021_TITULO;
            txtFecha.Text = (o.T021_FECHA != null) ? ((DateTime)o.T021_FECHA).ToShortDateString() : "";
            txtObservaciones.Text = o.T021_OBSERVA;
            query.Value = o.T021_CENTRO;

            //txtMotivoRT.Text = o.T835_MOTIVORT;
            if (o.T020_DESCRIPCION != null)
            {
                txtIdioma.InnerText = o.T020_DESCRIPCION;    
            }
        }
    }

    public string Grabar(int idTituloIdioma, int idFicepi, int idIdioma, string sDenTitulo, string sFecha, string sObs, string sCentro,
                         string sDenDoc, string sCambioDoc, string sUsuTick, string sEstado, string sMotivo, string sEstadoIni, 
                         string sIdContentServer, string sEsMiCV)
    {
        string sRes = "OK@#@"; 
        try
        {
            long? idDocAtenea = null;
            if (sUsuTick.Trim() != "")//Recupero el idDocumento de la tabla Temporal
            {
                SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sUsuTick);
                idDocAtenea = oDoc.t2_iddocumento;
            }
            else
            {
                if (sIdContentServer != "")
                    idDocAtenea = long.Parse(sIdContentServer);
            }
            TituloIdiomaFic.Grabar((idTituloIdioma == -1) ? null : (int?)idTituloIdioma,
                                    sDenTitulo,
                                    (sFecha == "") ? null : (DateTime?)DateTime.Parse(sFecha), //Fecha Obtencion
                                    sObs, sCentro,idFicepi, idIdioma, sDenDoc,
                                    bool.Parse(sCambioDoc),
                                    char.Parse(sEstado), sMotivo,
                                    (int)Session["IDFICEPI_ENTRADA"],//IDFICEPIU
                                    char.Parse(sEstadoIni), idDocAtenea, sEsMiCV);
            if (sUsuTick.Trim() != "")
            {   //Marco el documento como asignado para que el trigger no lo borre de Atenea
                if (idDocAtenea != null)
                    SUPER.DAL.DocuAux.Asignar(null, (long)idDocAtenea);
                //Borro el documento de la tabla temporal
                SUPER.DAL.DocuAux.BorrarDocumento(null, "T", sUsuTick);
            }
        }
        catch (Exception ex)
        {
            sRes = "Error@#@" + ex.Message;
        }
        return sRes;
    }
    private string getDocumento(string sUsuTicks)
    {
        string sRes = "";
        try
        {
            if (Utilidades.isNumeric(sUsuTicks))
            {
                SUPER.BLL.TituloIdiomaFic oTit = SUPER.BLL.TituloIdiomaFic.SelectDoc(null, int.Parse(sUsuTicks));
                sRes = oTit.T021_NDOC + "@#@S@#@" + oTit.t2_iddocumento.ToString();
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
            return "Error@#@" + Errores.mostrarError("Error al obtener documento del título del idioma", ex);
        }
    }

    public void deshabilitarUsuario()
    {
        try
        {
            if (hdnEsEncargado.Value == "0")
            {
                
                txtMotivoRT.ReadOnly = true;
                txtTitulo.Disabled = true;
                txtFecha.Enabled = false;
                query.Disabled = true;
                txtObservaciones.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al deshabilitar controles", ex);
        }
    }
    public void omitirObligParaAdmin()
    {
        try
        {
            foreach (Control ctrl in Page.FindControl("formTituloIdioma").Controls)
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
}

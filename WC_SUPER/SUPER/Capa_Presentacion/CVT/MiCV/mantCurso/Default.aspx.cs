using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.IO;
using SUPER.BLL;
using System.Collections;

public partial class Capa_Presentacion_CVT_MiCV_mantCurso_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sNombre, sTareasPendientes = "", sIDDocuAux = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
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
                Utilidades.SetEventosFecha(this.txtFIni);
                Utilidades.SetEventosFecha(this.txtFFin);

                if (!Page.IsPostBack)
                {
                    sIDDocuAux = "SUPER-" + Session["IDFICEPI_CVT_ACTUAL"].ToString() + "-" + DateTime.Now.Ticks.ToString();

                    hdnIdFicepi.Value = Utilidades.decodpar(Request.QueryString["iF"]);
                    this.hdnUserAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();

                    //Compruebo si estoy en mi propio curriculum
                    if (this.hdnIdFicepi.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                        this.hdnEsMiCV.Value = "S";

                    if (Request.QueryString["eA"] != null) //esAdmin
                        hdnEsEncargado.Value = Utilidades.decodpar(Request.QueryString["eA"]);
                    if (Request.QueryString["iC"] != null) //idCurso
                    {
                        if (Request.QueryString["iC"].ToString() != "")
                            hdnIdCurso.Value = Utilidades.decodpar(Request.QueryString["iC"]);
                    }
                    
                    //MIRAR DESDE AQUI
                    if (hdnIdCurso.Value != "-1")//CURSO ya existente
                    {
                        CargarDatos(Curso.Detalle(int.Parse(hdnIdCurso.Value), int.Parse(hdnIdFicepi.Value)));
                    }
                    else cargarCombosProvinciasPais(66);

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
                    ArrayList aBotones = Curriculum.getBotonesAMostrar((hdnOrigen.Value != "3") ? "Lectura" : hdnEstadoInicial.Value,
                                                                       (hdnEsMiCV.Value=="S") ? true : false, 
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
                            case (int)CVT.Accion.Lectura: btnSalir.Style.Add("display", "inline-block"); btnCancelar.Style.Add("display", "none"); break;
                        }
                    }            
                }

                if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    sTareasPendientes = Curriculum.MiCVTareasPendientes(4, int.Parse(hdnIdFicepi.Value), (hdnIdCurso.Value == "-1") ? 0 : int.Parse(this.hdnIdCurso.Value),null);
                //Compruebo si en el historial la última acción fué enviar a cumplimentar, en cuyo caso cargo el mensaje
                //que el validador le quiere hacer llegar al profesional
                this.hdnMsgCumplimentar.Value = SUPER.BLL.Historial.GetMsgPdteValidar("ASICRONO", int.Parse(hdnIdFicepi.Value),
                                                                                      (this.hdnIdCurso.Value == "") ? -1 : int.Parse(this.hdnIdCurso.Value));
            }
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al cargar la pagina", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad="";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("grabarVisibleCV"):
                sResultado += "OK@#@";
                bool bVisible = false;
                if (aArgs[3] == "S") bVisible = true;
                SUPER.BLL.Curso.SetVisibilidadCV_Recibido(null, int.Parse(aArgs[1]), int.Parse(aArgs[2]), bVisible);
                break;
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), int.Parse(aArgs[2]), Utilidades.unescape(aArgs[3]), Utilidades.unescape(aArgs[4]),
                                     aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11],
                                     Utilidades.unescape(aArgs[12]), Utilidades.unescape(aArgs[13]), aArgs[14], aArgs[15]
                                     , Utilidades.unescape(aArgs[16]), aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21], aArgs[22]);
                break;
            case ("documentos"):
                sCad = getDocumento(aArgs[1], aArgs[5]);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad;
                break;
            case ("provinciasPais"):
                sResultado += cargarProvinciasPais(aArgs[1]);
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

    public void cargarCombos()
    {
        cboPais.DataSource = SUPER.DAL.PAIS.Catalogo();
        cboPais.DataValueField = "identificador";
        cboPais.DataTextField = "denominacion";
        cboPais.DataBind();
        cboPais.SelectedValue = "66"; // Por defecto España

        //Que solo muestre los validados
        //cboEntorno.DataSource = EntornoTecno.obtenerCboEntorno(int.Parse("1"));
        cboEntorno.DataSource = EntornoTecno.obtenerCboEntorno(int.Parse("0"));
        cboEntorno.DataValueField = "sValor";
        cboEntorno.DataTextField = "sDenominacion";
        cboEntorno.DataBind();

    }

    public void CargarDatos(Curso o)
    {
        if (o != null)
        {
            hdnEstadoInicial.Value = o.T839_IDESTADO.ToString();
            hdnndoc.Value = o.T575_NDOC;

            this.hdnNombreDocInicial.Value = o.T575_NDOC;
            this.hdnContentServer.Value = o.t2_iddocumento.ToString();

            if (o.T839_IDESTADO == char.Parse("S") || o.T839_IDESTADO == char.Parse("T")) //Pendiente de cumplimentar.
            {
                //imgEstado.Attributes.Add("title", tf.t597_motivort.ToString());
                //imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(o.T575_MOTIVO.ToString()) + "\",\"Motivo\",null,300)");
                //imgEstado.Attributes.Add("onmouseout", "hideTTE()");
                imgInfoEstado.Style.Add("visibility", "visible");
                imgInfoEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(o.T575_MOTIVO.ToString()) + "\",\"Motivo\",null,300)");
                imgInfoEstado.Attributes.Add("onmouseout", "hideTTE()");
            }
            if (o.T839_IDESTADO != char.Parse("V"))
            {
                imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(Curvit.ToolTipEstados(o.T839_IDESTADO.ToString())) + "\",\"Información\",null,300)");
                imgEstado.Attributes.Add("onmouseout", "hideTTE()");
            }
            //if (o.T575_DOC!= null)
            if (o.t2_iddocumento != null)
            {
                txtNombreDocumento.Text = o.T575_NDOC;
                imgUploadDoc.Style.Add("display", "inline-block");
                imgDownloadDoc.Style.Add("display", "inline-block");
                imgBorrarDoc.Style.Add("display", "inline-block");
                //imgDownloadDoc.Attributes.Add("onclick", "verDOC()");
            }
            txtTitulo.Text = o.T574_TITULO;
            if (o.T574_FINICIO != null)
                txtFIni.Text = ((DateTime)o.T574_FINICIO).ToShortDateString(); ;
            if (o.T574_FFIN != null)
                txtFFin.Text = ((DateTime)o.T574_FFIN).ToShortDateString();
            txtContenido.Text = o.T574_CONTENIDO;

            //if (o.T002_IDPROVIN != null)
            //    cboProvincia.SelectedValue=o.T002_IDPROVIN.ToString();
            //else if (o.T574_ORIGEN != 3)
            //{
            //    cboProvincia.Items.Insert(0, new ListItem("ONLINE", "0"));
            //    cboProvincia.SelectedValue = "0";	
            //}
            if (o.EsOnline)
                this.chkOnline.Checked = true;
            else
            {
                //if (o.T173_IDPROVINCIA != null)
                //    cboProvincia.SelectedValue = o.T173_IDPROVINCIA.ToString();

                cboPais.SelectedValue = (o.T172_IDPAIS.ToString() == "") ? "66" : o.T172_IDPAIS.ToString();
                if (o.T172_IDPAIS.ToString() != "")
                {
                    cargarCombosProvinciasPais((int)o.T172_IDPAIS);
                    cboProvincia.SelectedValue = o.T173_IDPROVINCIA.ToString();
                }
            }
            if (o.VisibleCV)
                this.chkVisibleCV.Checked = true;
            else
                this.chkVisibleCV.Checked = false;

            if (o.T036_IDCODENTORNO!=null)
                cboEntorno.SelectedValue = o.T036_IDCODENTORNO.ToString();

            txtHoras.Text = o.T574_HORAS.ToString();
            query.Value = o.DESPROVEEDOR;
            if (o.T574_TECNICOC ==0)
                rdbTecn.Checked = true;
            else
                rdbComp.Checked = true;
            hdnOrigen.Value = o.T574_ORIGEN.ToString();
            if (o.T574_ORIGEN != 3)
            {
                //imgHistorial.Style.Add("visibility", "hidden");
                //hdnOrigen.Value = "3";
                deshabilitarDetalle();
                //15/10/2013 Nos pide María que si el origen del curso no es CVT, no se vean las observaciones
                txtObservaciones.Text = "";
            } 
            else
                txtObservaciones.Text = o.T575_OBSERVA;
        }
    }

    public string Grabar(int idFicepi, int idCurso, string sDenCurso, string sDenDoc, string sUsuTick, string sFIni, string sFFin, 
                         string sProv, string sHoras, string sProveedor, string sEntorno, string sContenido, string sObs, string sTecnico, 
                         string sEstado, string sMotivo, string sOnLine, string sVisibleCV, string sEsMiCV,
                         string sCambioDoc, string sEstadoInicial, string sIdContentServer)
    {
        string sRes = "OK@#@";
        int idFicepiU = int.Parse(Session["IDFICEPI_ENTRADA"].ToString());
        try
        {
            int? iCursoOnline = null;
            long? idDocAtenea = null;
            if (sUsuTick.Trim() != "" && sDenDoc.Trim() != "")//Recupero el idDocumento de la tabla Temporal
            {
                SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sUsuTick);
                idDocAtenea = oDoc.t2_iddocumento;
            }
            else
            {
                if (sIdContentServer != "")
                    idDocAtenea = long.Parse(sIdContentServer);
            }
            if (sOnLine == "S")
                iCursoOnline = 77;

            if (idCurso == -1)
            {
                Curso.Grabar(sDenCurso.Trim(), 3, 1, //Tipo
                                        (sFIni != "") ? (DateTime?)DateTime.Parse(sFIni) : null, //Fecha Inicio
                                        (sFFin != "") ? (DateTime?)DateTime.Parse(sFFin) : null, //Fecha Fin
                                        (sProv == "") ? null : (int?)int.Parse(sProv), //Provincia
                                        (sHoras != "") ? float.Parse(sHoras) : (float)0, //Horas
                                        sProveedor.Trim(),//Proveedor (Si hdnIdProveedor "" then T574_Direccion=Proveedor
                                        (sEntorno == "") ? null : (int?)int.Parse(sEntorno), //Entorno
                                        sContenido, sObs, //Observaciones asistente
                                        (sTecnico=="N") ? 0 : 1,//Tecnico-Competencias
                                        idFicepi, sDenDoc, char.Parse(sEstado), sMotivo, idFicepiU, iCursoOnline,
                                        (sVisibleCV == "N") ? false : true,
                                        idDocAtenea, sEsMiCV );
            }
            else
            {
                Curso.Update(idCurso, sDenCurso.Trim(), 3, 1, //Tipo
                                        (sFIni != "") ? (DateTime?)DateTime.Parse(sFIni) : null, //Fecha Inicio
                                        (sFFin != "") ? (DateTime?)DateTime.Parse(sFFin) : null, //Fecha Fin
                                        (sProv == "") ? null : (int?)int.Parse(sProv), //Provincia
                                        (sHoras != "") ? float.Parse(sHoras) : (float)0, //Horas
                                        sProveedor.Trim(),//Proveedor (Si hdnIdProveedor "" then T574_Direccion=Proveedor
                                        (sEntorno == "") ? null : (int?)int.Parse(sEntorno), //Entorno
                                        sContenido,  sObs,  (sTecnico == "N") ? 0 : 1,//Tecnico-Competencias
                                        idFicepi, sDenDoc, bool.Parse(sCambioDoc), char.Parse(sEstado),
                                        sMotivo, idFicepiU, char.Parse(sEstadoInicial), iCursoOnline,
                                        (sVisibleCV == "N") ? false : true,
                                        idDocAtenea, sEsMiCV );
            }
            if (sUsuTick.Trim() != "")
            {   //Marco el documento como asignado para que el trigger no lo borre de Atenea
                if (idDocAtenea != null)
                    SUPER.DAL.DocuAux.Asignar(null, (long)idDocAtenea);
                //Borro el documento de la tabla temporal
                SUPER.DAL.DocuAux.BorrarDocumento(null, "R", sUsuTick);
            }
        }
        catch (Exception ex)
        {
            sRes = "Error@#@" + ex.Message;
        }
        return sRes;
    }

    public void deshabilitarDetalle()
    {
        try
        {
            omitirObligParaAdmin();
            txtTitulo.Enabled= false;
            txtContenido.ReadOnly = true;
            txtFIni.Enabled = false;
            txtFFin.Enabled = false;
            cboPais.Enabled = false;
            cboProvincia.Enabled = false;
            this.chkOnline.Disabled = true;
            cboEntorno.Enabled = false;
            txtHoras.Enabled = false;
            query.Disabled = true;
            txtObservaciones.ReadOnly = true;
            rdbTecn.Disabled = true;
            rdbComp.Disabled = true;
            imgUploadDoc.Style.Add("display", "none");
            imgBorrarDoc.Style.Add("display", "none");
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
            foreach (Control ctrl in Page.FindControl("formCurso").Controls)
            {//Ocultar asteriscos para el encargado de curriculums y para los cursos que vienen de EnForma(Origen 1 y2)
                if (ctrl.GetType().Name == "Label")
                {
                    if (((Label)ctrl).ID == "lblDeno") continue;
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

    private string getDocumento(string sUsuTicks, string sIdFicepi)
    {
        string sRes = "";
        try
        {
            if (Utilidades.isNumeric(sUsuTicks))
            {
                SUPER.BLL.Curso oCurso = SUPER.BLL.Curso.SelectDoc(null, int.Parse(sUsuTicks), int.Parse(sIdFicepi));
                sRes = oCurso.T575_NDOC + "@#@S@#@" + oCurso.t2_iddocumento.ToString();
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
            return "Error@#@" + Errores.mostrarError("Error al obtener documento del curso recibido", ex);
        }
    }
    private string cargarProvinciasPais(string sID)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUPER.DAL.PAIS.Provincias(int.Parse(sID)); //Mostrar todos todos las provincias relacionadas a un país determinado

            while (dr.Read())
            {
                sb.Append(dr["identificador"].ToString() + "##" + dr["denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener las provincias fiscales de un determinado país", ex);
        }
    }
    private void cargarCombosProvinciasPais(int iID)
    {
        cboProvincia.DataValueField = "identificador";
        cboProvincia.DataTextField = "denominacion";
        cboProvincia.DataSource = SUPER.DAL.PAIS.Provincias(iID);
        cboProvincia.DataBind();
        cboProvincia.Items.Insert(0, new ListItem("", ""));
    }
}
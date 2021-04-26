using System;
using System.Configuration;
using System.Web.UI;
using System.Web;
using SUPER.BLL;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using EO.Web;

public partial class Capa_Presentacion_CVT_MiCV_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sIDFicepiEntrada="", sDatosPendientes = "", sTareasPendientes = "", sOrigen = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                 /* Session["CVFINALIZADO"] indica si el encargado de CVs ha terminado de grabar un CV para que el profesional
                 * empiece a introducir sus datos.
                 * Request.QueryString["spr"] indica si se viene de la pantalla de mantenimiento de CVs y contiene el IdFicepi
                 * del profesional al que se le quiere mantener el CV
                 */

                if (Session["CVFINALIZADO"] == null || (!(bool)Session["CVFINALIZADO"] && Request.QueryString["spr"] == null))
                {
                    try { Response.Redirect("CVObras.aspx", true); }
                    catch (System.Threading.ThreadAbortException) { }
                }

                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.Modulo = "CVT";
                //Master.nBotonera = 59;

                if (Request.QueryString["origen"] == null) sOrigen = "1";
                else sOrigen = Request.QueryString["origen"].ToString();

                if (sOrigen == "1")
                {//He entrado a MiCV directamente desde el menú
                    Master.sbotonesOpcionOn = "89,71";//Exportar, Guia
                }
                else
                {
                    Master.sbotonesOpcionOn = "89,71,6";//Exportar, Guia, Regresar
                    Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                }
                Master.sbotonesOpcionOff = "";                

                cboMovilidad.DataSource = Curriculum.obtenerCboMovilidad();
                cboMovilidad.DataValueField = "sValor";
                cboMovilidad.DataTextField = "sDenominacion";
                cboMovilidad.DataBind();

                if (!Page.IsPostBack)
                {
                    string sAux = Curriculum.TextoLegal(4);
                    if (sAux != "")
                        this.txtTextoLegal.InnerHtml = sAux;

                    sIDFicepiEntrada = Session["IDFICEPI_ENTRADA"].ToString();
                    //if (Request.QueryString["iF"] != null)
                    //{//Código del profesional 
                    //    this.hdnProf.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iF"].ToString());
                    //}
                    if (Request.QueryString["spr"] != null){//Es decir, viene de la pantalla de mantenimiento de currículums
                     //   ((System.Web.UI.WebControls.Literal)Page.Master.FindControl("SiteMapPath1").Controls[Page.Master.FindControl("SiteMapPath1").Controls.Count - 1].Controls[0]).Text = "Mantenimiento de Currículum";//Cambio en la guía de profundización
                        hdnIdficepi.Value = Utilidades.decodpar(Request.QueryString["spr"].ToString());
                    }
                    else{//Es un usuario normal
                        hdnIdficepi.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();
                    }
                    if (User.IsInRole("ECV"))
                        hdnEsEncargado.Value = "1";
                    else
                        hdnEsEncargado.Value = "0";

                    CargarDatos(int.Parse(hdnIdficepi.Value));
                    sDatosPendientes = Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value));

                    //Compruebo si estoy en mi propio curriculum
                    if (this.hdnIdficepi.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                        this.hdnEsMiCV.Value = "S";

                    if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                            sTareasPendientes = Curriculum.MiCVTareasPendientes(0, int.Parse(hdnIdficepi.Value), null, null);
                }
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
            Master.sErrores = Errores.mostrarError("Error al cargar la página", ex);
        }

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
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("cargarFormacionAcad"):
                    sResultado += "OK@#@" + TituloFicepi.MiCvTitulacion(int.Parse(hdnIdficepi.Value)) + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarCursos"):
                    sResultado += "OK@#@" + Curso.MiCVFormacionRecibida(int.Parse(hdnIdficepi.Value)) + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarCursosImpartidos"):
                    sResultado += "OK@#@" + Curso.MiCVFormacionImpartida(int.Parse(hdnIdficepi.Value)) + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarCertExam"):
                case ("reCargarCertExam"):
                    sResultado += "OK@#@" + Certificado.MiCVFormacionCertExam(int.Parse(hdnIdficepi.Value), (hdnEsEncargado.Value == "1") ? true : false) + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarExam"):
                case ("reCargarExam"):
                    //sResultado += "OK@#@" + Certificado.MiCVFormacionExam(int.Parse(hdnIdficepi.Value), (hdnEsEncargado.Value == "1") ? true : false) + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    sResultado += "OK@#@" + Examen.MiCVFormacionExam(int.Parse(hdnIdficepi.Value)) + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarIdiomas"):
                    sResultado += "OK@#@" + Idioma.MiCvIdiomas(int.Parse(hdnIdficepi.Value)) + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarTitulos"):
                    sResultado += "OK@#@" + TituloIdiomaFic.MiCVCatalogo(int.Parse(hdnIdficepi.Value), int.Parse(aArgs[2])) + "@#@" + aArgs[1] + "@#@" + Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarExpIb"):
                    sResultado += "OK@#@" + SUPER.BLL.EXPPROF.MiCVExpProfEnIbermatica(int.Parse(hdnIdficepi.Value)) + "@#@" +
                                            Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("cargarExpNoIb"):
                    sResultado += "OK@#@" + SUPER.BLL.EXPPROF.MiCVExpProfFueraIbermatica(int.Parse(hdnIdficepi.Value)) + "@#@" +
                                    Curriculum.MiCVPendientes(int.Parse(hdnIdficepi.Value)).ToString();
                    break;
                case ("grabar"):
                    Curriculum.Grabar(null, aArgs[1], int.Parse(hdnIdficepi.Value), (aArgs[3] == "") ? null : (bool?)bool.Parse(aArgs[3].ToString()), (aArgs[2] == "") ? null : (int?)int.Parse(aArgs[2]), aArgs[4].ToString());
                    sResultado += "OK@#@";
                    break;
                case ("borrarForAcad"):
                    sResultado += TituloFicepi.Borrar(aArgs[1], int.Parse(hdnIdficepi.Value), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                    break;
                case ("borrarForRec"):
                    sResultado += Curso.BorrarAsistente(int.Parse(aArgs[1]), aArgs[2], int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                    break;
                case ("borrarForImp"):
                    sResultado += Curso.BorrarMonitor(aArgs[1], int.Parse(hdnIdficepi.Value), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                    break;
                case ("borrarCertif"):
                    //if (SUPER.BLL.Certificado.TieneExamenesValidados(int.Parse(aArgs[1]), aArgs[2]))
                    //    sResultado +="KO@#@S";
                    //else
                    sResultado += Certificado.BorrarAsistente(aArgs[1], int.Parse(aArgs[2]), aArgs[3], int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                    break;
                case ("borrarIdioma"):
                    sResultado += IdiomaFic.Borrar(int.Parse(aArgs[1]), aArgs[2], int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                    break;
                case ("borrarExamen"):
                    sResultado += SUPER.BLL.Examen.BorrarAsistentes(int.Parse(aArgs[1]), aArgs[2], int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                    break;
                case ("borrarExpIber"):
                case ("borrarExpNoIber"):
                    sResultado += SUPER.BLL.EXPPROF.Borrar(aArgs[2], int.Parse(aArgs[1]), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                    break;
                case ("FinCV"):
                    sResultado += Curriculum.FinalizacionCv(int.Parse(aArgs[1]), Utilidades.unescape(aArgs[2]), aArgs[3]);
                    break;
                case ("setCompletadoProf"):
                    sResultado += Curriculum.setCompletadoProf(int.Parse(aArgs[1]));
                    break;
                case ("setRevisadoActualizadoCV"):
                    sResultado += Curriculum.setRevisadoActualizadoCV(int.Parse(aArgs[1]));
                    break;
                case ("cargarSinopsis"):
                    sResultado += "OK@#@" + Curriculum.getSinopsis(int.Parse(hdnIdficepi.Value));    
                    break;
                case ("grabarS")://grabar sinopsis
                    Curriculum.GrabarSinopsis(int.Parse(hdnIdficepi.Value),aArgs[1]);
                    sResultado += "OK";
                    break;
            }
        }
        catch (Exception ex)
        {
            sResultado += "Error@#@" + Errores.mostrarError("Error al cargar datos", ex);
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    public void CargarDatos(int idFicepi)
    {
        Curriculum o = Curriculum.MiCV(idFicepi);        
        nombre.InnerText = o.NOMBREPROFESIONAL;
        idNIF.InnerText = o.T001_CIP;
        idSexo.InnerText = o.SEXO;
        idNacionalidad.InnerText = o.T001_NACIONALIDAD;
        idNacimiento.InnerText = o.T001_FECNACIM;
        empresa.InnerText = o.EMPRESA;
        SN2.InnerText = o.T392_DENOMINACION;
        CR.InnerText = o.T303_DENOMINACION;        
        fantigu.InnerText = o.T001_FECANTIGU;
        rol.InnerText = o.ROL;
        oficina.InnerText = o.OFICINA;
        cboMovilidad.SelectedValue = (o.movilidad != null) ? o.movilidad.ToString() : "";
        perfil.InnerText = o.T035_DESCRIPCION;
        //provincia.InnerText = o.T009_DESCENTRAB;
        provincia.InnerText = o.t173_denominacion;
        pais.InnerText = o.Pais;

        if (o.internacional != null)
        {
            rdlInternacional.SelectedValue = (o.internacional==true)? "1" : "0";
        }       
        txtObserva.Value = o.observaciones;        
        Session["FOTOUSUARIO"] = o.T001_FOTO;
        Session["PROFESIONAL_CVEXCLUSION"] = o.PROFESIONAL_CVEXCLUSION;
        Session["RESPONSABLE_CVEXCLUSION"] = o.RESPONSABLE_CVEXCLUSION;

        if (hdnEsEncargado.Value == "1" && o.CVFINALIZADO == 0)
        {
            //btnFinCv.Visible = true;
            btnFinCv.Style.Add("display", "inline-block");
        }
        else
        {
            btnFinCv.Style.Add("display", "none");
        }
           //btnFinCv.Visible = false;

        lblUltActu.InnerText = o.t001_cv_fechaactu.ToString();

        /*if (!o.t001_cvcompletado_prof.HasValue 
            && (int)Session["IDFICEPI_CVT_ACTUAL"] == idFicepi
            && (int)Session["IDFICEPI_CVT_ACTUAL"] == (int)Session["IDFICEPI_ENTRADA"]
            )
        {
            btnCVCompletadoProf.Style.Add("display", "block");
        }*/
    }
}

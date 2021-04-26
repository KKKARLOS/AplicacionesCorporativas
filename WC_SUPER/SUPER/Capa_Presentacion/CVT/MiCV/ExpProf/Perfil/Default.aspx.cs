using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
//using SUPER.Capa_Negocio;
using SUPER.BLL;
using SUPER.Capa_Negocio;
//using System.Xml;
//using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    private int? idFicepiValidador;
    private bool bEsMiCV = false;
    public string TipoPerfil = "Perfil", sTareasPendientes = "", sMsgCumplimentar="";
    public string strTablaHtml, strTablaHtmlConTec, strTablaHtmlConSec, strHTMLEntorno = "", sErrores;//, sNodo = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="o">Origen: F-> experiencia no asociada a proyecto relativa a fuera de Ibermática
    ///                         D-> experiencia no asociada a proyecto relativa a Ibermática
    /// </param>
    /// <param name="iE">Código de experiencia profesional (para entrar a registros ya existentes)</param>
    /// <param name="m">Modo de acceso R-> lectura, eoc-> escritura</param>
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

                bool bLectura = true;

                #region recojo parámetros
                if (Request.QueryString["iF"] != null)
                {//Código del profesional 
                    this.hdnProf.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iF"].ToString());
                }
                this.hdnUserAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();

                //Compruebo si estoy en mi propio curriculum
                if (this.hdnProf.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                    this.hdnEsMiCV.Value = "S";

                if (Request.QueryString["o"] != null)
                    this.hdnOrigen.Value = Request.QueryString["o"].ToString();

                if (Request.QueryString["iE"] != null)

                {//Código de experiencia profesional
                    this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iE"].ToString());
                    //CargarDatosExpProf(int.Parse(this.hdnEP.Value));
                }
                else
                {
                    if (Request.Form["iE"] != null)
                    {
                        this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.Form["iE"].ToString());
                        //CargarDatosExpProf(int.Parse(this.hdnEP.Value));
                    }
                }

                if (this.hdnEP.Value != "" && this.hdnEP.Value != "-1")
                {//Compruebo si es una Experiencia Profesional en IBERMATICA o fuera
                    CargarDatosExpProf(int.Parse(this.hdnEP.Value));
                }
                if (Request.QueryString["dE"] != null)
                {//Denominación de la Experiencia profesional 
                    this.txtDen.Text = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["dE"].ToString());
                }
                if (Request.QueryString["iEF"] != null)
                {//Código de la experiencia profesional del profesional (t812_idexpprofficepi)
                    this.hdnEPF.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iEF"].ToString());
                }
                if (Request.QueryString["iP"] != null)
                {//Código del perfil del profesional en la experiencia profesional (t813_idexpficepiperfil)
                    this.hdnEFP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iP"].ToString());
                }
                if (Request.QueryString["tipo"] != null)
                {//Plantilla del profesional en la experiencia profesional (t819_plantillacvt)
                    this.hdnPlantilla.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["tipo"].ToString());
                }
                if (Request.QueryString["eA"] != null)
                {//Es Administrador
                    this.hdnEsAdmin.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["eA"].ToString());
                }
                if (int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()) == int.Parse(this.hdnProf.Value))
                    bEsMiCV = true;
                GetPerfiles((this.hdnEsAdmin.Value != "N" && bEsMiCV == false) ? byte.Parse("1") : byte.Parse("0"));
                GetIdiomas();

                if (this.hdnProf.Value == "-1")
                    throw (new Exception("Es necesario indicar el profesional de la experiencia"));
                else
                    this.txtDenProf.Text = SUPER.Capa_Negocio.USUARIO.GetNombreProfesional(int.Parse(this.hdnProf.Value));
                            
                #endregion
                #region inicializo datos

                if (this.hdnEFP.Value != "" && this.hdnEFP.Value != "-1")
                {//Entramos en una Experiencia Profesional ya existente
                    GetDatos(int.Parse(this.hdnProf.Value), int.Parse(this.hdnEP.Value), int.Parse(this.hdnEPF.Value), int.Parse(this.hdnEFP.Value));
                }
                else
                {
                    #region Datos de la experiencia profesional
                    if (this.hdnEP.Value != "" && this.hdnProf.Value != "")
                    {
                        EXPPROF oExpProf = EXPPROF.DatosExpProf(null, int.Parse(this.hdnEP.Value), int.Parse(this.hdnProf.Value));
                        if (int.Parse(this.hdnEP.Value) != -1)
                        {
                            this.txtDen.Text = oExpProf.t808_denominacion;
                            if (oExpProf.t808_enibermatica)
                                this.hdnEnIb.Value = "S";
                            this.hdnSegmentoC.Value = oExpProf.idSegmento_ori.ToString();
                            this.hdnProfVal.Value = oExpProf.idValidador.ToString();
                            this.txtValidador.Text = oExpProf.denValidador;
                        }
                        else
                            SetValidador(int.Parse(this.hdnProf.Value));
                    }
                    else
                        SetValidador(int.Parse(this.hdnProf.Value));
                    #endregion
                }

                EXPPROFFICEPI oEPF = new EXPPROFFICEPI(null, int.Parse(this.hdnEPF.Value));
                if (oEPF.t812_idexpprofficepi != -1)
                {
                    imgInfoPerfil.Style.Add("visibility", "visible");
                    imgInfoPerfil.Attributes.Add("onmouseover", "showTTE(\"" +
                        "<label style='width:70px'>Fecha alta:</label>" + ((oEPF.t812_finicio.HasValue) ? ((DateTime)oEPF.t812_finicio).ToShortDateString() : "") +
                        "<br><label style='width:70px'>Fecha baja:</label>" + ((oEPF.t812_ffin.HasValue)? ((DateTime)oEPF.t812_ffin).ToShortDateString() : "") +
                        "\",\"Asociación a la experiencia profesional\",null,250)");
                    imgInfoPerfil.Attributes.Add("onmouseout", "hideTTE()");
                    if (oEPF.t812_ffin != null)
                    {
                        if (this.hdnLigadoProy.Value == "S")
                        {
                            if (oEPF.t812_ffin < DateTime.Now)
                                lblFFin.Style.Add("visibility", "visible");
                            else
                                lblFFin.Style.Add("visibility", "hidden");
                        }
                        else
                            lblFFin.Style.Add("visibility", "hidden");
                    }
                    else 
                        lblFFin.Style.Add("visibility", "hidden");
                }

                //if (Request.QueryString["LSuper"] != null && SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["LSuper"]) == "LS")
                GetDatosFechaExpProfFicepi(int.Parse(this.hdnEPF.Value));

                if (hdnEFP.Value == "-1") txtFI.Text = hdnFechaIni.Value;

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
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTPseudovalidado.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
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
                if (hdnEstadoInicial.Value == "R")
                {
                    SUPER.Capa_Negocio.ModoLectura.Poner(this.Controls);
                    btnNewET.Disabled = true;
                    //btnDelET.Disabled = true;
                }
                #endregion

                #region Establezco si es Mantenedor de CVs y/o Validador de CVs
                //Si el registro de la T812_EXPPROFFICEPI no tiene indicado validador, miro si el usuario actual es responsable Progress del profesional
                if (idFicepiValidador == null)
                {
                    //string sRes = SUPER.Capa_Negocio.Ficepi.GetResponsableProgress(int.Parse(this.hdnProf.Value));
                    //if (sRes != "")
                    //{
                    //    string[] aD = Regex.Split(sRes, "@#@");
                    //    if (aD[0] == Session["IDFICEPI"].ToString())
                    //        this.hdnValidador.Value = "S";
                    //}
                    if (this.hdnProfVal.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                        this.hdnValidador.Value = "S";
                }
                else
                {//Si el validador indicado en el registro de la T812_EXPPROFFICEPI es el usuario actual -> es validador
                    if (idFicepiValidador == int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()))
                        this.hdnValidador.Value = "S";
                }
                if (User.IsInRole("ECV") && this.hdnProf.Value != Session["IDFICEPI_CVT_ACTUAL"].ToString()) 
                    this.hdnMantenedor.Value = (User.IsInRole("ECV")) ? "S" : "N";

                //Si es un perfil nuevo y es mi curriculum, el estado inicial será Borrador
                //Sino es mi curriculum el estado será Pendiente de validar con origen según el usuario actual
                if (this.hdnEFP.Value == "-1")
                {
                    if (bEsMiCV)
                        this.hdnEstadoNuevo.Value = "B";
                    else
                    {
                        if (this.hdnMantenedor.Value == "S")
                            this.hdnEstadoNuevo.Value = "O";
                        else
                            this.hdnEstadoNuevo.Value = "P";
                    }
                }
                #endregion
                #region Establezco si el acceso es en lectura o en escritura
                //if (this.hdnEPF.Value == "-1" || this.hdnEPF.Value == "")//Si es un registro nuevo, debe ser en escritura
                //    this.hdnModo.Value = "W";
                //else
                //{
                //    if (this.hdnEstadoNuevo.Value != "R")//Si el perfil está rechazado -> modo lectura
                //    {
                //        if (bEsMiCV)//Es mi curriculum
                //            this.hdnModo.Value = "W";
                //        else
                //        {
                //            if (this.hdnMantenedor.Value == "S")//Es mantenedor de CVs
                //                this.hdnModo.Value = "W";
                //        }
                //    }
                //}
                #endregion
                #region Establezco los botones visibles

                bool bEsValidador = false;
                if (Request.QueryString["v"] != null)
                {
                    if (Request.QueryString["v"] == "1")
                        bEsValidador = true;
                    else
                        bEsValidador = false;
                }
                if (this.hdnValidador.Value == "S")
                    bEsValidador = true;

                bool bEstaDeBaja = SUPER.BLL.Profesional.EstaDeBaja(int.Parse(hdnProf.Value));
                //ArrayList aBotones = Curriculum.getBotonesAMostrar((this.hdnModo.Value != "W" || hdnPlant.Value != "") ? "Lectura" : hdnEstadoInicial.Value,
                //                                    (int.Parse(hdnProf.Value) == (int)Session["IDFICEPI_CVT_ACTUAL"]) ? true : false,
                //                                    bEsValidador, false);
                ArrayList aBotones = Curriculum.getBotonesAMostrar((this.hdnModo.Value != "W") ? "Lectura" : hdnEstadoInicial.Value,
                                                    (int.Parse(hdnProf.Value) == (int)Session["IDFICEPI_CVT_ACTUAL"]) ? true : false,
                                                    bEsValidador, false);

                for (int i = 0; i < aBotones.Count; i++)
                {
                    switch ((int)aBotones[i])
                    {
                        //case (int)CVT.Accion.Aparcar: btnAparcar.Style.Add("display", "inline-block"); break;
                        case (int)CVT.Accion.Enviar:
                            if (bEsValidador)
                                btnValidar.Style.Add("display", "inline-block"); 
                            else
                                btnEnviar.Style.Add("display", "inline-block");

                            break;
                        case (int)CVT.Accion.Cumplimentar: if (!bEstaDeBaja) btnCumplimentar.Style.Add("display", "inline-block"); break;
                        case (int)CVT.Accion.Validar: 
                            btnValidar.Style.Add("display", "inline-block"); 
                            break;
                        //case (int)CVT.Accion.Pseudovalidar: btnPseudovalidar.Style.Add("display", "inline-block"); break;
                        case (int)CVT.Accion.Rechazar: btnRechazar.Style.Add("display", "inline-block"); break;
                        case (int)CVT.Accion.Lectura: 
                            btnSalir.Style.Add("display", "inline-block"); 
                            btnCancelar.Style.Add("display", "none"); 
                            break;
                    }
                } 

                #endregion
                #region Resto de datos
                //if (this.hdnModo.Value == "R" || hdnPlant.Value != "")
                if (this.hdnModo.Value == "R")
                    {
                    SUPER.Capa_Negocio.ModoLectura.Poner(this.Controls);
                    
                    //if (hdnPlant.Value != "")
                    //{
                    //    btnNewET.Visible = false;
                    //    btnDelET.Visible = false;
                    //    //imgHistorial.Style.Add("visibility", "hidden");
                    //}
                }
                else
                {
                    bLectura = false;
                    SUPER.Capa_Negocio.Utilidades.SetEventosFecha(this.txtFI);
                    SUPER.Capa_Negocio.Utilidades.SetEventosFecha(this.txtFF);
                }
                if (hdnPlant.Value == "")
                    GetEntTec(int.Parse(this.hdnEFP.Value), bLectura);
                else
                    GetEntTecPlantilla(int.Parse(this.hdnPlant.Value));

                //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                //    sTareasPendientes = Curriculum.MiCVTareasPendientes(2, int.Parse(this.hdnProf.Value), int.Parse(this.hdnEFP.Value),null);            

                //Compruebo si en el historial la última acción fué enviar a cumplimentar, en cuyo caso cargo el mensaje
                //que el validador le quiere hacer llegar al profesional
                //sMsgCumplimentar = SUPER.BLL.Historial.GetMsgPdteValidar("T838_EXPFICEPIPERFILCRONO", int.Parse(this.hdnEFP.Value), null);
                this.hdnMsgCumplimentar.Value = SUPER.BLL.Historial.GetMsgPdteValidar("T838_EXPFICEPIPERFILCRONO", int.Parse(this.hdnEFP.Value), null);
                #endregion
            }
            catch (Exception ex)
            {
                sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener los datos", ex);
            }
            //}
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            //case ("profesionales"):
            //    sResultado += GetProfesionales(aArgs[1], int.Parse(aArgs[2]), aArgs[3], aArgs[4]);
            //    break;
        }
        _callbackResultado = sResultado;
    }
    
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private void GetDatos(int idFicepi, int idExpProf, int idExpProfFicepi, int idExpFicepiPerfil)
    {
        #region Datos de la experiencia profesional
        EXPPROF oExpProf = EXPPROF.DatosExpProf(null, idExpProf, idFicepi);
        if (idExpProf != -1)
        {
            this.txtDen.Text = oExpProf.t808_denominacion;
            if (oExpProf.t808_enibermatica)
                this.hdnEnIb.Value = "S";
            this.hdnSegmentoC.Value = oExpProf.idSegmento_ori.ToString();
            this.hdnProfVal.Value = oExpProf.idValidador.ToString();
            this.txtValidador.Text = oExpProf.denValidador;
        }
        else
            SetValidador(idFicepi);
        #endregion
        #region Datos del profesional en la experiencia profesional
        SUPER.BLL.EXPPROFFICEPI oEPF = new SUPER.BLL.EXPPROFFICEPI(null, idExpProfFicepi);
        if (idExpProfFicepi == -1)
        {
            //oEPF.t812_visiblecv = true;
            oEPF.t001_idficepi = int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString());
            oEPF.t808_idexpprof = idExpProf;
            
        }
        else
        {
            if (oEPF.t001_idficepi_validador != null)
                idFicepiValidador = oEPF.t001_idficepi_validador;
            //this.hdnProfVal.Value = oEPF.idValidador.ToString();
            //this.txtValidador.Text = oEPF.denValidador;
        }
        //this.hdnVisibleCV.Value = "1";
        //this.hdnProfVal.Value = oEPF.t001_idficepi_validador.ToString();
        this.hdnPlant.Value = oEPF.t819_idplantillacvt.ToString();
        #endregion
        #region Datos del perfil/Plantilla del profesional en la experiencia
        if (hdnPlantilla.Value == "P")
        {
            PLANTILLACVT oPl = new PLANTILLACVT();
            oPl = PLANTILLACVT.Detalle(int.Parse(hdnPlant.Value), int.Parse(hdnProf.Value));
            this.txtFI.Text = oPl.t812_finicio;
            this.txtFF.Text = oPl.t812_ffin;
            this.cboPerfil.SelectedValue = oPl.t035_idcodperfil.ToString();
            this.cboIdioma.SelectedValue = oPl.t020_idcodidioma.ToString();
            this.hdnEstadoInicial.Value = "V";
            this.txtFun.Text = oPl.t819_funcion;
            this.txtObs.Text = oPl.t819_observa;
            this.TipoPerfil = "Plantilla";
        }
        else if (idExpFicepiPerfil != -1)
        {
            this.TipoPerfil = "Perfil";
            SUPER.BLL.EXPFICEPIPERFIL oEFP = new SUPER.BLL.EXPFICEPIPERFIL(null, idExpFicepiPerfil);
            if (oEFP.t813_finicio != null)
                this.txtFI.Text = DateTime.Parse(oEFP.t813_finicio.ToString()).ToShortDateString();
            if (oEFP.t813_ffin != null)
                this.txtFF.Text = DateTime.Parse(oEFP.t813_ffin.ToString()).ToShortDateString();
            this.cboPerfil.SelectedValue = oEFP.t035_idcodperfil.ToString();
            this.cboIdioma.SelectedValue = oEFP.t020_idcodidioma.ToString();

            if (oEFP.t839_idestado == "S" || oEFP.t839_idestado == "T")//|| oEFP.t839_idestado == "R"
            {
                imgInfoEstado.Style.Add("visibility", "visible");
                imgInfoEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(oEFP.t838_motivort.ToString()) + "\",\"Motivo\",null,300)");
                imgInfoEstado.Attributes.Add("onmouseout", "hideTTE()");
            }
            if (oEFP.t839_idestado != "V")
            {
                imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(Curvit.ToolTipEstados(oEFP.t839_idestado)) + "\",\"Información\",null,300)");
                imgEstado.Attributes.Add("onmouseout", "hideTTE()");
            }

            this.hdnEstadoInicial.Value = oEFP.t839_idestado;

            this.txtFun.Text = oEFP.t813_funcion;
            this.txtObs.Text = oEFP.t813_observa;
        }   

        

        #endregion
    }
    private void SetValidador(int idFicepi)
    {
        string sRes = SUPER.Capa_Negocio.Ficepi.GetResponsableProgress(idFicepi);
        if (sRes != "")
        {
            string[] aD = Regex.Split(sRes, "@#@");
            this.hdnProfVal.Value = aD[0];
            this.txtValidador.Text = aD[1];
        }
    }

    private void GetEntTec(int idExpFicepiPerfil, bool bModoLectura)
    {
        strHTMLEntorno = SUPER.BLL.EntornoTecno.CatalogoByProf(idExpFicepiPerfil);
        //return "OK@#@" + strTablaHtmlConSec;
    }

    private void GetEntTecPlantilla(int t819_idplantillacvt)
    {
        strHTMLEntorno = SUPER.BLL.EntornoTecno.CatalogoPlantilla(t819_idplantillacvt);
        //01-16(Lacalle)
        //btnNewET.Visible = false;

        //btnDelET.Visible = false;
        //return "OK@#@" + strTablaHtmlConSec;
    }

    private void GetPerfiles(byte tipo)
    {
        ListItem Elemento;
        //04/11/2013 El perfil POR DETERMINAR lo añadimos siempre y luego a la hora de grabar controlamos si es el propio usuario en si CV
        // (y no le dejaremos grabar con ese valor) o es un encargado
        //if (User.IsInRole("ECV") && this.hdnProf.Value != Session["IDFICEPI_CVT_ACTUAL"].ToString()) 
        //{
            Elemento = new ListItem("POR DETERMINAR", "-1");
            this.cboPerfil.Items.Add(Elemento);
        //}
        SqlDataReader dr = SUPER.BLL.PerfilExper.getPerfiles(null,tipo);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["t035_descripcion"].ToString(), dr["t035_idcodperfil"].ToString());
            this.cboPerfil.Items.Add(Elemento);
        }

        this.cboPerfil.SelectedValue = "-1";
        dr.Close();
        dr.Dispose();
    }
    private void GetIdiomas()
    {
        ListItem Elemento;
        SqlDataReader dr = SUPER.BLL.Idioma.Catalogo(null);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["T020_DESCRIPCION"].ToString(), dr["T020_IDCODIDIOMA"].ToString());
            this.cboIdioma.Items.Add(Elemento);
        }
        this.cboIdioma.SelectedValue = "34";//CASTELLANO
        dr.Close();
        dr.Dispose();
    }

    /// <summary>
    /// Grabar datos en la T812, T813, T815
    /// </summary>
    /// <param name="strDatosGen"></param>
    /// <param name="strET"></param>
    /// <returns></returns>
    protected string Grabar(string strDatosGen, string strET)
    {
        string sResul = "";
        try
        {
            //Llamar a Grabar en la capa BLL y recoger un churro que contenga los códigos insertados (o updateados) en 812, 813 y 815
            //para actualizar los campos hidden de la pantalla
            sResul = SUPER.BLL.EXPPROFFICEPI.GrabarPerfil(strDatosGen, strET, this.hdnEsMiCV.Value);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar.", ex, false);// +"@#@" + sDesc;
        }
        return sResul;
    }

    private void CargarDatosExpProf(int idExpProf)
    {
        SUPER.BLL.EXPPROF oExpProf = SUPER.BLL.EXPPROF.DatosExpProf(null, idExpProf);
        this.hdnSegmentoC.Value = oExpProf.idSegmento_ori.ToString();
        if (oExpProf.t808_enibermatica)
            this.hdnEnIb.Value = "S";
        else
            this.hdnEnIb.Value = "N";

        if (oExpProf.bTieneProyecto)
            this.hdnLigadoProy.Value = "S";
        else
            this.hdnLigadoProy.Value = "N";
    }

    private void GetDatosFechaExpProfFicepi(int idExpProfFicepi)
    {
        EXPPROFFICEPI o = new EXPPROFFICEPI(null,idExpProfFicepi);
        this.hdnFechaIni.Value = (o.t812_finicio.ToString() == "") ? "" : DateTime.Parse(o.t812_finicio.ToString()).ToShortDateString();
        this.hdnFechaFin.Value = (o.t812_ffin.ToString() == "") ? "" : DateTime.Parse(o.t812_ffin.ToString()).ToShortDateString();
    }
    
}
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
using SUPER.BLL;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_CVT_MiCV_ExpProf_ValidarPerfil : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    private int? idFicepiValidador;
    public string strTablaHtml, strTablaHtmlConTec, strTablaHtmlConSec, strHTMLEntorno = "", sErrores, strArraySEG;//, sNodo = "";
    public string Tipo = "";
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
                bool bEsValidador = true;
                GetPerfiles();
                GetIdiomas();
                GetSectores();
                GetArraySegmentos();

                #region recojo parámetros

                if (Request.QueryString["iE"] != null)
                {//Código de experiencia profesional
                    this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iE"].ToString());
                }
                else
                {
                    if (Request.Form["iE"] != null)
                    {
                        this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.Form["iE"].ToString());
                    }
                }
                if (Request.QueryString["iF"] != null)
                {//Código del profesional 
                    this.hdnProf.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iF"].ToString());
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
                //Compruebo si estoy en mi propio curriculum
                if (this.hdnProf.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                    this.hdnEsMiCV.Value = "S";

                if (User.IsInRole("ECV"))
                {//Es Administrador
                    this.hdnEsAdmin.Value = "S";
                }
                if (Request.QueryString["tipoExp"] != null)
                {//Tipo de experiencia
                    string tipoExp = "";
                    tipoExp = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["tipoExp"].ToString());
                    if (tipoExp == "expout")
                    {
                        Tipo = "fuera de Ibermática";
                        this.filCliente.Style.Add("display", "none");
                        this.filClienteC.Style.Add("display", "none");
                    }
                    else
                    {
                        Tipo = "en Ibermática";
                        this.filEmpresa.Style.Add("display", "none");
                        this.filEmpresaEC.Style.Add("display", "none");
                        this.filClienteP.Style.Add("display", "none");
                        this.filClientePD.Style.Add("display", "none");
                        this.imglblCliente.Style.Add("display", "none");
                        this.txtDescripcion.Rows = 5;

                    }
                }

                this.txtDenProf.InnerHtml = SUPER.Capa_Negocio.USUARIO.GetNombreProfesional(int.Parse(this.hdnProf.Value));


                #endregion

                #region inicializo datos

                if (this.hdnEFP.Value != "" && this.hdnEFP.Value != "-1")
                {//Entramos en una Experiencia Profesional ya existente
                    GetDatos(int.Parse(this.hdnProf.Value), int.Parse(this.hdnEP.Value), int.Parse(this.hdnEPF.Value), int.Parse(this.hdnEFP.Value));
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
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTPseudovalidado.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                        break;
                    case "B": //Borrador
                        imgEstado.ImageUrl = "~/Images/imgEstadoCVTBorrador.png";
                        imgHistorial.Style.Add("visibility", "hidden");
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
                }
                #endregion

                #region Establezco si es Mantenedor de CVs y/o Validador de CVs
                //Si el registro de la T812_EXPPROFFICEPI no tiene indicado validador, miro si el usuario actual es responsable Progress del profesional
                if (idFicepiValidador == null)
                {
                    string sRes = SUPER.Capa_Negocio.Ficepi.GetResponsableProgress(int.Parse(this.hdnProf.Value));
                    if (sRes != "")
                    {
                        string[] aD = Regex.Split(sRes, "@#@");
                        if (aD[0] == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                            this.hdnValidador.Value = "S";
                    }
                }
                else
                {//Si el validador indicado en el registro de la T812_EXPPROFFICEPI es el usuario actual -> es validador
                    if (idFicepiValidador == int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()))
                        this.hdnValidador.Value = "S";
                }
                if (User.IsInRole("ECV"))
                    this.hdnMantenedor.Value = "S";
                //Si es un perfil nuevo y es mi curriculum, el estado inicial será Borrador
                //Sino es mi curriculum el estado será Pendiente de validar con origen según el usuario actual

                #endregion

                #region Establezco los botones visibles

                ArrayList aBotones = 
                    Curriculum.getBotonesAMostrar((this.hdnModo.Value != "W" || hdnPlant.Value != "") ? "Lectura" : hdnEstadoInicial.Value,
                                                  (int.Parse(hdnProf.Value) == (int)Session["IDFICEPI_CVT_ACTUAL"]) ? true : false, 
                                                  bEsValidador, false);
                bool bEstaDeBaja = SUPER.BLL.Profesional.EstaDeBaja(int.Parse(hdnProf.Value));


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
                        case (int)CVT.Accion.Cumplimentar: 
                            if (!bEstaDeBaja) 
                                btnCumplimentar.Style.Add("display", "inline-block"); 
                            break;
                        case (int)CVT.Accion.Validar: btnValidar.Style.Add("display", "inline-block"); break;
                        case (int)CVT.Accion.Rechazar: btnRechazar.Style.Add("display", "inline-block"); break;
                        case (int)CVT.Accion.Lectura: 
                            btnSalir.Style.Add("display", "inline-block"); 
                            btnCancelar.Style.Add("display", "none"); 
                            break;
                    }
                }

                #endregion

                #region Resto de datos

                if (this.hdnModo.Value == "R" || hdnPlant.Value != "")
                {
                    SUPER.Capa_Negocio.ModoLectura.Poner(this.Controls);

                    if (hdnPlant.Value != "")
                    {
                        btnNewET.Visible = false;
                        imgHistorial.Style.Add("visibility", "hidden");
                    }
                }
                else
                {
                    bLectura = false;
                    SUPER.Capa_Negocio.Utilidades.SetEventosFecha(this.txtFI);
                    SUPER.Capa_Negocio.Utilidades.SetEventosFecha(this.txtFF);
                }

                GetEntTec(int.Parse(this.hdnEFP.Value), bLectura);

                #endregion

                //Compruebo si en el historial la última acción fué enviar a cumplimentar, en cuyo caso cargo el mensaje
                //que el validador le quiere hacer llegar al profesional
                this.hdnMsgCumplimentar.Value = SUPER.BLL.Historial.GetMsgPdteValidar("T838_EXPFICEPIPERFILCRONO", 
                                                                                    int.Parse(this.hdnEFP.Value), null);

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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
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

        EXPPROF oExpProf = EXPPROF.DatosExpProfDetPerfil(null, idExpProf);
        //EXPPROF oExpProf = EXPPROF.DatosExpProf(null, idExpProf, idFicepi);
        if (idExpProf != -1)
        {
            this.txtDen.Value = oExpProf.t808_denominacion;
            if (oExpProf.t808_enibermatica)
                this.hdnEnIb.Value = "S";
            this.nbrACS.InnerText = oExpProf.ACS;
            this.nbrACT.InnerText = oExpProf.ACT;
            this.hdnACS.Value = oExpProf.idACS;
            this.hdnACT.Value = oExpProf.idACT;

            //Cliente (Dentro de Iber)
            this.cboSectorC.SelectedValue = oExpProf.idSectorCliente.ToString();
            //if (oExpProf.idSectorCliente != null)
            if (oExpProf.idSectorCliente != 0) //Un entero (no nullable) se inicializa a cero, por lo que siempre será distinto de nulo
                GetSegmentosCli((int)oExpProf.idSectorCliente);
            this.cboSegmentoC.SelectedValue = oExpProf.idSegmentoCliente.ToString();
            if (oExpProf.idCliente != null)
                this.hdnCli.Value = oExpProf.idCliente.ToString();
            else
                this.hdnCli.Value = "null";
            if (hdnCli.Value != "null")
            {
                cboSectorC.Enabled = false;
                cboSegmentoC.Enabled = false;
            }
            this.hdnTipo.Value = oExpProf.Dentro;
            this.txtCliente.Value = oExpProf.Cliente;


            //Empresa Contratante (Fuera de Iber)
            this.cboSectorEC.SelectedValue = oExpProf.idSectorEmpresaC.ToString();
            //if (oExpProf.idSectorEmpresaC != null)
            if (oExpProf.idSectorEmpresaC != 0) //Un entero (no nullable) se inicializa a cero, por lo que siempre será distinto de nulo
                GetSegmentosEC((int)oExpProf.idSectorEmpresaC);
            this.cboSegmentoEC.SelectedValue = oExpProf.idSegmentoEmpresaC.ToString();
            if (oExpProf.idEmpresaC != null)
                this.hdnEC.Value = (-1 * oExpProf.idEmpresaC).ToString();
            else
                this.hdnEC.Value = "null";
            if (hdnEC.Value != "null")
            {
                cboSectorEC.Enabled = false;
                cboSegmentoEC.Enabled = false;
            }
            this.txtEmpresaC.Value = oExpProf.EmpresaC;
            this.hdnSegmentoC.Value = oExpProf.idSegmentoEmpresaC.ToString();

            //Cliente (Fuera de Iber)
            this.cboSectorClienteP.SelectedValue = oExpProf.idSectorP.ToString();
            //if (oExpProf.idSectorP != null)
            if (oExpProf.idSectorP != 0) //Un entero (no nullable) se inicializa a cero, por lo que siempre será distinto de nulo
                GetSegmentosCliP((int)oExpProf.idSectorP);
            this.cboSegmentoClienteP.SelectedValue = oExpProf.idSegmentoP.ToString();
            if (oExpProf.idClienteP != null)
                this.hdnCliP.Value = (-1 * oExpProf.idClienteP).ToString();
            else
                this.hdnCliP.Value = "null";
            if (hdnCliP.Value != "null")
            {
                cboSectorClienteP.Enabled = false;
                cboSegmentoClienteP.Enabled = false;
            }
            this.txtClienteP.Value = oExpProf.ClienteP;

            this.txtDescripcion.Text = oExpProf.t808_descripcion;

            //si la experiencia es de Tipo L (Ligada a un proyecto super) los datos de la experiencia no se pueden modificar
            if (oExpProf.Tipo == "L")
            {
                this.txtDen.Disabled = true;
                this.lblACS.Attributes.Remove("onclick");
                this.lblACS.Attributes.Remove("class");
                this.lblACT.Attributes.Remove("onclick");
                this.lblACT.Attributes.Remove("class");
                this.txtCliente.Disabled = true;
                //this.txtDescripcion.Enabled = false;
                this.txtDescripcion.Attributes.Add("ReadOnly", "true");
                this.txtDescripcion.Style.Add("color", "gray");

                GetDatosFechaExpProfFicepi(idExpProfFicepi);

                this.imgInfoExpProy.Visible = true;
            }
            //if (oExpProf.idValidador != null)
            //{
            //    this.hdnProfVal.Value = oExpProf.idValidador.ToString();
            //    idFicepiValidador = oExpProf.idValidador;
            //}
        }
        #endregion

        #region Datos del profesional en la experiencia profesional
        SUPER.BLL.EXPPROFFICEPI oEPF = new SUPER.BLL.EXPPROFFICEPI(null, idExpProfFicepi);
        if (idExpProfFicepi == -1)
        {
            oEPF.t001_idficepi = int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString());
            oEPF.t808_idexpprof = idExpProf;
        }
        if (oEPF.t001_idficepi_validador != null && idFicepiValidador==null)
            idFicepiValidador = oEPF.t001_idficepi_validador;
        this.hdnProfVal.Value = oEPF.t001_idficepi_validador.ToString();

        #endregion

        #region Datos del perfil del profesional en la experiencia
        if (idExpFicepiPerfil != -1)
        {
            SUPER.BLL.EXPFICEPIPERFIL oEFP = new SUPER.BLL.EXPFICEPIPERFIL(null, idExpFicepiPerfil);
            if (oEFP.t813_finicio != null)
                this.txtFI.Text = DateTime.Parse(oEFP.t813_finicio.ToString()).ToShortDateString();
            if (oEFP.t813_ffin != null)
                this.txtFF.Text = DateTime.Parse(oEFP.t813_ffin.ToString()).ToShortDateString();
            this.cboPerfil.SelectedValue = oEFP.t035_idcodperfil.ToString();
            this.cboIdioma.SelectedValue = oEFP.t020_idcodidioma.ToString();

            if (oEFP.t839_idestado == "S" || oEFP.t839_idestado == "T")//|| oEFP.t839_idestado == "R"
            {
                imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(oEFP.t838_motivort.ToString()) + "\",\"Motivo\",null,300)");
                imgEstado.Attributes.Add("onmouseout", "hideTTE()");
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

    /// <summary>
    /// Grabar datos en la T812, T813, T815
    /// </summary>
    /// <param name="strDatosGen"></param>
    /// <param name="strET"></param>
    /// <returns></returns>
    protected string Grabar(string strDatosGen, string strET, string strDatosEP)
    {
        string sResul = "";
        try
        {
            //Llamar a Grabar en la capa BLL y recoger un churro que contenga los códigos insertados (o updateados) en 812, 813 y 815
            //para actualizar los campos hidden de la pantalla
            SUPER.BLL.EXPPROF.Update(strDatosEP);
            sResul = SUPER.BLL.EXPPROFFICEPI.GrabarPerfil(strDatosGen, strET, hdnEsMiCV.Value);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar.", ex, false);// +"@#@" + sDesc;
        }
        return sResul;
    }

    private void GetEntTec(int idExpFicepiPerfil, bool bModoLectura)
    {
        strHTMLEntorno = SUPER.BLL.EntornoTecno.CatalogoByProf(idExpFicepiPerfil);
    }

    private void GetPerfiles()
    {
        ListItem Elemento;
        SqlDataReader dr = SUPER.BLL.PerfilExper.getPerfiles(null, 0);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["t035_descripcion"].ToString(), dr["t035_idcodperfil"].ToString());
            this.cboPerfil.Items.Add(Elemento);
        }
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

    private void GetSectores()
    {
        ListItem Elemento, Elemento1, Elemento2;
        Elemento = new ListItem("", "-1");
        Elemento1 = new ListItem("", "-1");
        Elemento2 = new ListItem("", "-1");
        this.cboSectorC.Items.Add(Elemento);
        this.cboSectorEC.Items.Add(Elemento1);
        this.cboSectorClienteP.Items.Add(Elemento2);
        SqlDataReader dr = SUPER.Capa_Negocio.SECTOR.CatalogoDenominacion();
        while (dr.Read())
        {
            Elemento = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
            Elemento1 = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
            Elemento2 = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
            this.cboSectorC.Items.Add(Elemento);
            this.cboSectorEC.Items.Add(Elemento1);
            this.cboSectorClienteP.Items.Add(Elemento2);
        }
        dr.Close();
        dr.Dispose();
    }

    private void GetArraySegmentos()
    {
        StringBuilder sbuilder = new StringBuilder();
        int i = 0;
        sbuilder.Append(" aSEG_js = new Array();\n");
        SqlDataReader dr = SUPER.Capa_Negocio.SEGMENTO.Catalogo(null, null);
        while (dr.Read())
        {
            sbuilder.Append("\taSEG_js[" + i.ToString() + "] = new Array(\"" + dr["t483_idSector"].ToString() + "\",\"" +
                            dr["t484_idSegmento"].ToString() + "\",\"" + dr["t484_denominacion"].ToString() + "\");\n");
            i++;
        }
        strArraySEG = sbuilder.ToString();
        dr.Close();
        dr.Dispose();
    }

    //Cliente(Dentro Super)
    private void GetSegmentosCli(int idSector)
    {
        ListItem Elemento;
        SqlDataReader dr = SUPER.Capa_Negocio.SEGMENTO.Catalogo(null, idSector);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["T484_DENOMINACION"].ToString(), dr["T484_IDSEGMENTO"].ToString());
            this.cboSegmentoC.Items.Add(Elemento);
        }
        dr.Close();
        dr.Dispose();
    }

    //Empresa Contratante(Fuera Super)
    private void GetSegmentosEC(int idSector)
    {
        ListItem Elemento;
        SqlDataReader dr = SUPER.Capa_Negocio.SEGMENTO.Catalogo(null, idSector);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["T484_DENOMINACION"].ToString(), dr["T484_IDSEGMENTO"].ToString());
            this.cboSegmentoEC.Items.Add(Elemento);
        }
        dr.Close();
        dr.Dispose();
    }

    //Cliente(Fuera Super)
    private void GetSegmentosCliP(int idSector)
    {
        ListItem Elemento;
        SqlDataReader dr = SUPER.Capa_Negocio.SEGMENTO.Catalogo(null, idSector);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["T484_DENOMINACION"].ToString(), dr["T484_IDSEGMENTO"].ToString());
            this.cboSegmentoClienteP.Items.Add(Elemento);
        }
        dr.Close();
        dr.Dispose();
    }

    private void GetDatosFechaExpProfFicepi(int idExpProfFicepi)
    {
        EXPPROFFICEPI o = new EXPPROFFICEPI(null, idExpProfFicepi);
        this.hdnFechaIni.Value = (o.t812_finicio.HasValue) ? ((DateTime)o.t812_finicio).ToShortDateString() : "";
        this.hdnFechaFin.Value = (o.t812_ffin.HasValue) ? ((DateTime)o.t812_ffin).ToShortDateString() : "";

        //if (o.t812_idexpprofficepi != -1 && ((DateTime)o.t812_finicio).Year > 1)
        if (o.t812_idexpprofficepi != -1)
        {
            string dfIni = (o.t812_finicio.HasValue) ? ((DateTime)o.t812_finicio).ToShortDateString() : "";
            string dfFin = (o.t812_ffin.HasValue) ? ((DateTime)o.t812_ffin).ToShortDateString() : "";
            imgInfoPerfil.Style.Add("visibility", "visible");
            imgInfoPerfil.Attributes.Add("onmouseover", "showTTE(\"" +
                "<label style='width:70px'>Fecha alta:</label>" + dfIni +
                "<br><label style='width:70px'>Fecha baja:</label>" + dfFin +
                "\",\"Asociación a la experiencia profesional\",null,250)");
            imgInfoPerfil.Attributes.Add("onmouseout", "hideTTE()");
        }

    }
}
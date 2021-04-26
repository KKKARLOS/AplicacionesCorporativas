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
//using System.Xml;
//using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, strTablaHtmlConTec, strTablaHtmlConSec, strHTMLEntorno = "", sErrores, strArraySEG;//, sNodo = "";
    public string sTareasPendientes = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="o">Origen: F-> experiencia no asociada a proyecto relativa a fuera de Ibermática
    ///                         D-> experiencia no asociada a proyecto relativa a Ibermática
    /// </param>
    /// <param name="ep">Código de experiencia profesional (para entrar a registros ya existentes)</param>
    /// <param name="m">Modo de acceso R-> lectura, eoc-> escritura</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //if (!Page.IsPostBack)
            //{
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
                if (Request.QueryString["o"] != null)
                    this.hdnOrigen.Value = Request.QueryString["o"].ToString();
                if (Request.QueryString["ep"] != null)
                {//Código de experiencia profesional
                    this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["ep"].ToString());
                }
                else
                {
                    if (Request.Form["ep"] != null)
                    {
                        this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.Form["ep"].ToString());
                    }
                }
                if (Request.QueryString["iEF"] != null)
                {//Código de experiencia ficepi
                    this.hdnidExpFicepi.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iEF"].ToString());
                }

                if (Request.QueryString["idf"] != null)
                {//Código del profesional 
                    this.hdnProf.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["idf"].ToString());
                }
                if (this.hdnProf.Value == "-1")
                    throw (new Exception("Es necesario indicar el profesional de la experiencia"));

                //Compruebo si estoy en mi propio curriculum
                if (this.hdnProf.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                    this.hdnEsMiCV.Value = "S";

                //Encargado de CVs
                //if (Request.QueryString["a"] != null)
                //{
                //    if (Request.QueryString["a"].ToString() == "1")
                //        this.hdnEsEncargado.Value = "S";
                //}
                //if (User.IsInRole("ECV") && this.hdnProf.Value != Session["IDFICEPI_CVT_ACTUAL"].ToString()) 
                //    this.hdnEsEncargado.Value = (User.IsInRole("ECV")) ? "S" : "N";
                if (User.IsInRole("ECV"))
                {
                    this.hdnEsEncargado.Value = "S";
                    omitirObligParaEncargado();
                }
                
                //Estado
                if (Request.QueryString["e"] != null)
                {//Modo de acceso (lectura o escritura)
                    switch (Request.QueryString["e"].ToString())
                    {
                        case "R":
                            this.hdnModo.Value = "R";
                            break;
                        case "V":
                        case "O":
                        case "P":
                        case "S":
                        case "T":
                        case "B":
                        case "X":
                        case "Y":
                            this.hdnModo.Value = "W";
                            break;
                    }
                }
                if (this.hdnModo.Value == "R")
                    SUPER.Capa_Negocio.ModoLectura.Poner(this.Controls);
                else
                    bLectura = false;
                GetSectores();
                GetArraySegmentos();
                //GetPerfilesExper();
                GetConTec(int.Parse(this.hdnEP.Value), bLectura);
                GetConSec(int.Parse(this.hdnEP.Value), bLectura);

                GetPerfiles(int.Parse(this.hdnProf.Value), int.Parse(this.hdnEP.Value));

                if (this.hdnEP.Value != "" && this.hdnEP.Value != "-1")
                {//Entramos en una Experiencia Profesional ya existente
                    GetDatos(int.Parse(this.hdnProf.Value), int.Parse(this.hdnEP.Value));
                }
                else
                    SetValidador(int.Parse(this.hdnProf.Value));

                if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    sTareasPendientes = Curriculum.MiCVTareasPendientes(1, int.Parse(this.hdnProf.Value), int.Parse(this.hdnidExpFicepi.Value), int.Parse(this.hdnEP.Value));
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
    public void omitirObligParaEncargado()
    {
        try
        {
            spDescri.Style["display"] = "none";
            foreach (Control ctrl in Page.FindControl("formExpProfNoIbermatica").Controls)
            {//Ocultar asteriscos para el encargado de curriculums
                if (ctrl.GetType().Name == "Label")
                {
                    if (((Label)ctrl).ID == "lblDeno" || ((Label)ctrl).ID == "lblEmpCont" || ((Label)ctrl).ID == "lblDescri") continue;

                    if (((Label)ctrl).Text == "*")
                    {
                        ((Label)ctrl).Style["display"] = "none";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sErrores = SUPER.Capa_Negocio.Errores.mostrarError("Error al omitir obligatoriedad", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("delPerfil"):
                sResultado += SUPER.BLL.EXPFICEPIPERFIL.Borrar(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), aArgs[2], aArgs[3], aArgs[4], aArgs[5], int.Parse(aArgs[6]));
                break;
            case ("RevisadaPerfilExper"):
                sResultado += RevisadoPerfilExper(int.Parse(this.hdnidExpFicepi.Value));
                break;
            case ("getperfil"):
                string[] aDatos = Regex.Split(SUPER.BLL.EXPFICEPIPERFIL.MiCVCatalogo(true, int.Parse(aArgs[1]), int.Parse(aArgs[2])), "@#@");
                sResultado += "OK@#@" + aDatos[0].ToString();
                break;
            case ("borrarEP"):
                sResultado += SUPER.BLL.EXPPROF.Borrar(aArgs[2], int.Parse(aArgs[1]),int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private void GetDatos(int idFicepi, int idExpProf)
    {
        #region Datos de la experiencia profesional
        EXPPROF oExpProf = EXPPROF.DatosExpProfFuera(null, idExpProf, idFicepi);
        if (idExpProf != -1)
        {
            this.txtDen.Text = oExpProf.t808_denominacion;
            this.txtDes.Text = oExpProf.t808_descripcion;

            //Inicio Empresa contratante
            if (oExpProf.t811_idcuenta_ori != null)
                this.hdnCtaOri.Value = (-1 * oExpProf.t811_idcuenta_ori).ToString();
            else
                this.hdnCtaOri.Value = "null";
            this.txtEmpresaC.Value = oExpProf.ctaOrigen;

            this.cboSectorC.SelectedValue = oExpProf.idSector_ori.ToString();
            if (oExpProf.idSector_ori != null)
                GetSegmentosOri((int)oExpProf.idSector_ori);

            this.cboSegmentoC.SelectedValue = oExpProf.idSegmento_ori.ToString();

            if (hdnCtaOri.Value != "null" && oExpProf.idSector_ori != null && oExpProf.idSegmento_ori != null)
            {
                cboSectorC.Enabled = false;
                cboSegmentoC.Enabled = false;
            }
            //Fin Empresa contratante

            //Inicio Cliente
            if (oExpProf.t811_idcuenta_para != null)
                this.hdnCtaDes.Value = (-1 * oExpProf.t811_idcuenta_para).ToString();
            else
                this.hdnCtaDes.Value = "null";
            this.txtEmpresaD.Value = oExpProf.ctaDestino;

            this.cboSectorD.SelectedValue = oExpProf.idSector_des.ToString();
            if (oExpProf.idSector_des != null)
                GetSegmentosDes((int)oExpProf.idSector_des);
            this.cboSegmentoD.SelectedValue = oExpProf.idSegmento_des.ToString();

            if (hdnCtaDes.Value != "null" && oExpProf.idSector_des != null && oExpProf.idSegmento_des != null)
            {
                cboSectorD.Enabled = false;
                cboSegmentoD.Enabled = false;
            }
            if (oExpProf.t808_enibermatica)
                this.hdnEnIb.Value = "S";
            this.hdnProfVal.Value = oExpProf.idValidador.ToString();
            this.txtValidador.Text = oExpProf.denValidador;
        }
        else
            SetValidador(idFicepi);
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

    #region Areas Conocimiento
    
    private void GetConTec(int idExpProf, bool bModoLectura)
    {
        strTablaHtmlConTec = SUPER.BLL.EXPPROFACT.getAreas(null, idExpProf, bModoLectura);

        //return "OK@#@" + strTablaHtmlConTec;
    }
    private void GetConSec(int idExpProf, bool bModoLectura)
    {
        strTablaHtmlConSec = SUPER.BLL.EXPPROFACS.getAreas(null, idExpProf, bModoLectura);

        //return "OK@#@" + strTablaHtmlConSec;
    }

    #endregion

    #region Sectores-Segmentos

    private void GetSectores()
    {
        ListItem Elemento, Elemento1;
        Elemento = new ListItem("", "-1");
        Elemento1 = new ListItem("", "-1");
        this.cboSectorC.Items.Add(Elemento);
        this.cboSectorD.Items.Add(Elemento1);
        SqlDataReader dr = SUPER.Capa_Negocio.SECTOR.CatalogoDenominacion();
        while (dr.Read())
        {
            Elemento = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
            Elemento1 = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
            this.cboSectorC.Items.Add(Elemento);
            this.cboSectorD.Items.Add(Elemento1);
        }
        dr.Close();
        dr.Dispose();
    }
    private void GetSegmentosOri(int idSector)
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
    private void GetSegmentosDes(int idSector)
    {
        ListItem Elemento;
        SqlDataReader dr = SUPER.Capa_Negocio.SEGMENTO.Catalogo(null, idSector);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["T484_DENOMINACION"].ToString(), dr["T484_IDSEGMENTO"].ToString());
            this.cboSegmentoD.Items.Add(Elemento);
        }
        dr.Close();
        dr.Dispose();
    }
    /// <summary>
    /// Carga una array para manejar los segmentos dinámicamente en cliente
    /// </summary>
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
    #endregion

    private void GetPerfiles(int idProfesional, int idExpProf)
    {
        string[] aArgs = Regex.Split(SUPER.BLL.EXPFICEPIPERFIL.MiCVCatalogo(false, idProfesional, idExpProf), "@#@");
        strTablaHtml = aArgs[0].ToString();
    }

    /// <summary>
    /// Grabar datos en la T808, T816, T817, T812, T813, T815
    /// </summary>
    /// <param name="idExpProf"></param>
    /// <param name="idExpProfFicepi"></param>
    /// <param name="idExpFicepiPerfil"></param>
    /// <param name="idFicepi"></param>
    /// <param name="strDatosGen"></param>
    /// <param name="strDatosACT"></param>
    /// <param name="strDatosACS"></param>
    /// <param name="strEPF"></param>
    /// <param name="strEFP"></param>
    /// <param name="strET"></param>
    /// <returns></returns>
    protected string Grabar(int idExpProf, string strDatosGen, string strDatosACT, string strDatosACS, string strDatosPERF, int t001_idficepi)
    {
        string sResul = "";
        try
        {
            //Llamar a Grabar en la capa BLL y recoger un churro que contenga los códigos insertados (o updateados) en 808, 812 y 813
            //para actualizar los campos hidden de la pantalla


            sResul = SUPER.BLL.EXPPROF.GrabarFueraIb(idExpProf, strDatosGen, strDatosACT, strDatosACS, strDatosPERF, t001_idficepi);

        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar.", ex, false);// +"@#@" + sDesc;
        }
        return sResul;
    }
    protected string RevisadoPerfilExper(int idExpFicepi)
    {
        string sResul = "";
        try
        {
            sResul = SUPER.BLL.EXPPROFFICEPI.RevisadoPerfilExper(idExpFicepi);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar.", ex, false);// +"@#@" + sDesc;
        }
        return sResul;
    }
}
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

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, strTablaHtmlConTec, strTablaHtmlConSec, sErrores;//, sNodo = "";
    public string sIdFicepi = "", sTareasPendientes = "";

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

                    this.hdnUserAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();
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
                    {//Código de experiencia ficepi
                        this.hdnidExpFicepi.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iEF"].ToString());
                        if (this.hdnidExpFicepi.Value == "null") hdnidExpFicepi.Value = "0";
                    }
                    
                    if (this.hdnProf.Value == "-1")
                        throw (new Exception("Es necesario indicar el profesional de la experiencia"));
                    
                    //Compruebo si estoy en mi propio curriculum
                    //if (this.hdnProf.Value == this.hdnUserAct.Value)
                    if (this.hdnProf.Value == Session["IDFICEPI_CVT_ACTUAL"].ToString())
                        this.hdnEsMiCV.Value = "S";

                    //Encargado de CVs
                    //if (Request.QueryString["eA"] != null)
                    //{
                    //    if (Request.QueryString["eA"].ToString()=="1")
                    //        this.hdnEsEncargado.Value = "S";
                    //}
                    if (User.IsInRole("ECV"))
                        this.hdnEsEncargado.Value = "S";
                    //Estado
                    if (this.hdnEP.Value == "-1")
                        this.hdnModo.Value = "W";
                    else
                    {
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
                    }
                    if (Request.QueryString["pr"] != null && Utilidades.decodpar(Request.QueryString["pr"]) != "-1" && this.hdnEP.Value != "-1")
                    {
                        this.hdnModo.Value = "R";
                    }
                    if (this.hdnModo.Value == "R"){
                        SUPER.Capa_Negocio.ModoLectura.Poner(this.Controls);
                        this.imgGomaCliente.Style.Add("visibility", "hidden");
                        this.txtDen.Enabled=false;
                        //this.txtDes.Enabled = false;
                        this.txtDes.Attributes.Add("ReadOnly", "true");
                        this.txtDes.Style.Add("color", "gray");
                        this.txtCliente.Enabled = false;
                        this.txtValidador.Enabled = false;
                    }
                    else
                        bLectura = false;

                    //Recojo el nombre del profesional (por si hay que hacer una petición de borrado)

                    if (Request.QueryString["n"] != null)
                    {
                        this.hdnNomProf.Value = Utilidades.decodpar(Request.QueryString["n"]);
                    }
                    GetConTec(int.Parse(this.hdnEP.Value), bLectura);
                    GetConSec(int.Parse(this.hdnEP.Value), bLectura);
                    GetPerfiles(int.Parse(this.hdnProf.Value), int.Parse(this.hdnEP.Value));
                    if (this.hdnEP.Value != "" && this.hdnEP.Value != "-1")
                    {//Entramos en una Experiencia Profesional ya existente
                        GetDatos(int.Parse(this.hdnEP.Value), int.Parse(this.hdnProf.Value));
                    }
                    else
                        SetValidador(int.Parse(this.hdnProf.Value));

                    //Si se trata de una experiencia profesional de proyecto, se muestra el icono para acceder a 
                    //la relación de proyectos asociados a la misma.

                    if (this.hdnEP.Value != "-1" && EXPPROFPROYECTO.CountProyectosByExperiencia(int.Parse(this.hdnEP.Value)) > 0)
                    {
                        this.imgInfoExpProy.Visible = true;
                    }

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
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("delPerfil"):
                sResultado += SUPER.BLL.EXPFICEPIPERFIL.Borrar(aArgs[1], aArgs[2]);
                break;
            case ("borrarEP"):
                sResultado += SUPER.BLL.EXPPROF.Borrar(aArgs[2], int.Parse(aArgs[1]), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                break;
            case ("RevisadaPerfilExper"):
                sResultado += RevisadoPerfilExper(int.Parse(this.hdnidExpFicepi.Value));
                break;
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), aArgs[2], aArgs[3], aArgs[4], aArgs[5], int.Parse(aArgs[6]));
                break;
            case ("getperfil"):
                string[] aDatos = Regex.Split(SUPER.BLL.EXPFICEPIPERFIL.MiCVCatalogo(true, int.Parse(aArgs[1]), int.Parse(aArgs[2])), "@#@");
                sResultado +="OK@#@" + aDatos[0].ToString();
                if (aArgs[1] == "P")
                {
                    btnNewPerf.Style.Add("visibility", "hidden");
                }
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
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
    private void GetDatos(int idExpProf, int idProfesional)
    {
        #region Datos de la experiencia profesional
        EXPPROF oExpProf = EXPPROF.DatosExpProf(null, idExpProf, idProfesional);
        if (idExpProf != -1)
        {
            this.txtDen.Text = oExpProf.t808_denominacion;
            this.txtDes.Text = oExpProf.t808_descripcion;
            this.txtCliente.Text = oExpProf.t302_denominacion;
            if (oExpProf.t808_enibermatica)
                this.hdnEnIb.Value = "S";
            if (oExpProf.bTieneProyecto)
                this.hdnTieneProy.Value = "S";

            this.hdnProfVal.Value = oExpProf.idValidador.ToString();
            this.txtValidador.Text = oExpProf.denValidador;
        }
        else
        {
            SetValidador(int.Parse(this.hdnProf.Value));
        }
        #endregion
    }
    private void GetConTec(int idExpProf, bool bModoLectura)
    {
        strTablaHtmlConTec = SUPER.BLL.EXPPROFACT.getAreas(null, idExpProf, bModoLectura);
    }
    private void GetConSec(int idExpProf, bool bModoLectura)
    {
        strTablaHtmlConSec = SUPER.BLL.EXPPROFACS.getAreas(null, idExpProf, bModoLectura);
    }
    private void GetPerfiles(int idProfesional, int idExpProf)
    {
        string[] aArgs = Regex.Split(SUPER.BLL.EXPFICEPIPERFIL.MiCVCatalogo(true, idProfesional, idExpProf), "@#@");
        strTablaHtml = aArgs[0].ToString();
        if (aArgs[1] == "P")
        {
            //btnNewPerf.Style.Add("visibility", "hidden");
            this.hdnEsPlant.Value = "S";
        }
    }

    /// <summary>
    /// Grabar datos en la T808, T816, T817, T812, T813, T815
    /// </summary>
    /// <param name="idExpProf"></param>
    /// <param name="strDatosGen"></param>
    /// <param name="strDatosACT"></param>
    /// <param name="strDatosACS"></param>
    /// <param name="strDatosPERF"></param>
    /// <returns></returns>
    protected string Grabar(int idExpProf, string strDatosGen, string strDatosACT, string strDatosACS, string strDatosPERF, int idProfesional)
    {
        string sResul = "";
        try
        {
            //Llamar a Grabar en la capa BLL y recoger un churro que contenga los códigos insertados (o updateados)
            //para actualizar los campos hidden de la pantalla
            sResul = SUPER.BLL.EXPPROF.GrabarEnIb(idExpProf, strDatosGen, strDatosACT, strDatosACS, strDatosPERF, idProfesional);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar.", ex, false);// +"@#@" + sDesc;
        }
        return sResul;
    }
    protected string RevisadoPerfilExper( int idExpFicepi)
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
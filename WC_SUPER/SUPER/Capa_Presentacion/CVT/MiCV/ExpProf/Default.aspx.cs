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
using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, strTablaHtmlConTec, strTablaHtmlConSec, sErrores;//, sNodo = "";
    //private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string nIdProy = "-1";
    public string sHayExperiencias = "false";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="o">Origen: P-> experiencia de proyecto 
    ///                         F-> experiencia no asociada a proyecto relativa a fuera de Ibermática
    ///                         D-> experiencia no asociada a proyecto relativa a Ibermática
    /// </param>
    /// <param name="ep">Código de experiencia profesional (para entrar a registros ya existentes)</param>
    /// <param name="pr">Código de proyecto</param>
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

                if (Request.QueryString["o"] != null)
                {//Módulo desde el que se accede a la Experiencia profesional. P-> desde proyecto SUPER
                    this.hdnOri.Value = Request.QueryString["o"].ToString();
                    if (this.hdnOri.Value != "P")
                        this.hdnEnIb.Value = this.hdnOri.Value;
                }
                if (Request.QueryString["ep"] != null && Request.QueryString["ep"].ToString() != "")
                {//Código de experiencia profesional
                    this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["ep"].ToString());
                }
                else
                {
                    if (Request.Form["ep"] != null && Request.Form["ep"].ToString() != "")
                    {
                        this.hdnEP.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.Form["ep"].ToString());
                    }
                }

                if (Request.QueryString["pr"] != null)
                {//Código de proyecto
                    this.hdnPR.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["pr"].ToString());
                }
                    
                if (Request.QueryString["m"] != null)
                {//Modo de acceso (lectura o escritura)
                    this.hdnModo.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["m"]);
                }
                if (this.hdnModo.Value == "R")
                {
                    SUPER.Capa_Negocio.ModoLectura.Poner(this.Controls);
                    lblProyRef.Style.Add("display", "none");
                    txtProyRef.Style.Add("display", "none");
                    txtDes.Style.Add("height", "262px");
                    Button1.Disabled = true;
                    btnNuevo.Disabled = true;
                    btnCancelar.Disabled = true;
                    Button2.Disabled = true;
                    //lblPlantillasAsociadas.Style.Add("display", "none");
                }
                else
                    bLectura = false;

                if (this.hdnEP.Value == "-1")
                {
                    if (Request.Form["nIdProy"] != null)
                    {
                        nIdProy = Request.Form["nIdProy"].ToString();
                        GetDatosProyRef(int.Parse(nIdProy), int.Parse(this.hdnPR.Value), bLectura);
                    }
                    else
                    {
                        GetDatosProy(int.Parse(this.hdnPR.Value), bLectura);
                    }
                }
                else
                {
                    GetDatosExperiencia(int.Parse(this.hdnEP.Value), bLectura);
                }
                //Si se trata de una experiencia profesional de proyecto, se muestra el icono para acceder a 
                //la relación de proyectos asociados a la misma.
                if (this.hdnOri.Value == "P" && this.hdnEP.Value != "-1")
                {
                    this.imgInfoExpProy.Visible = true;
                }

                //Si se accede desde un proyecto y no existe la experiencia profesional,
                //Se comprueba si el cliente del proyecto tiene otras experiencias profesionales
                //para dar un aviso a profesional y valore si tiene que crear una nueva experiencia
                //o asociar el proyecto a una existente.
                if (this.hdnOri.Value == "P"
                    && this.hdnPR.Value != "-1"
                    && this.hdnEP.Value == "-1")
                {
                    if (SUPER.DAL.EXPPROF.bHayExpProfByCliente(null, int.Parse(hdnCliProy.Value)))
                    {
                        sHayExperiencias = "true";
                    }
                }
            }
            catch (Exception ex)
            {
                sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener los datos", ex);
            }

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
                sResultado += Grabar(aArgs[1], int.Parse(aArgs[2]), int.Parse(aArgs[3]), aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                lblProyRef.Style.Add("display", "none");
                txtProyRef.Style.Add("display", "none");
                txtDes.Style.Add("height", "262px");
                break;
        }
        _callbackResultado = sResultado;
    }
    
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }
    private void GetDatosExperiencia(int idExpProf, bool bModoLectura)
    {
        EXPPROF oExpProf = EXPPROF.DatosExpProf(null, idExpProf);

        this.txtDen.Text = oExpProf.t808_denominacion;
        this.txtDes.Text = oExpProf.t808_descripcion;

        lblProyRef.Style.Add("display", "none");
        txtProyRef.Style.Add("display", "none");
        txtDes.Style.Add("height", "262px");

        GetConTec(idExpProf, bModoLectura);
        GetConSec(idExpProf, bModoLectura);

        GetProfesionales(idExpProf, bModoLectura);
    }

    private void GetDatosProyRef(int idPRRef,int idPR, bool bModoLectura)
    {
        int idExpProf = GetDatosGenericosProy(idPRRef, bModoLectura);
        this.hdnEP.Value = idExpProf.ToString();
        GetConTec(idExpProf, bModoLectura);
        GetConSec(idExpProf, bModoLectura);

        GetProfesionales(idPR, idExpProf, bModoLectura);
    }

    private void GetDatosProy(int idPR, bool bModoLectura)
    {
        int idExpProf = GetDatosGenericosProy(idPR, bModoLectura);
        this.hdnEP.Value = idExpProf.ToString();
        GetConTec(idExpProf, bModoLectura);
        GetConSec(idExpProf, bModoLectura);
        
        GetProfesionales(idPR, idExpProf, bModoLectura);
    }
    
    private int GetDatosGenericosProy(int t301_idproyecto, bool bModoLectura)
    {
        EXPPROF oExpProf = EXPPROF.Datos(null, t301_idproyecto);
       
        this.txtDen.Text = oExpProf.denProyecto;
        this.txtDes.Text = oExpProf.t808_descripcion;
        
        if (oExpProf.t808_idexpprof != -1) {
            if (nIdProy != "-1")
                txtProyRef.Text = oExpProf.denProyecto;
            else
            {
                lblProyRef.Style.Add("display", "none");
                txtProyRef.Style.Add("display", "none");
                txtDes.Style.Add("height", "262px");
            }
        }
        else{
            hdnCliProy.Value = oExpProf.t302_idcliente_proyecto.ToString();
            
        }
        return oExpProf.t808_idexpprof;
    }

    private string GetConTec(int idExpProf, bool bModoLectura)
    {
        strTablaHtmlConTec = SUPER.BLL.EXPPROFACT.getAreas(null, idExpProf, bModoLectura);

        return "OK@#@" + strTablaHtmlConTec;
    }
    
    private string GetConSec(int idExpProf, bool bModoLectura)
    {
        strTablaHtmlConSec = SUPER.BLL.EXPPROFACS.getAreas(null, idExpProf, bModoLectura);

        return "OK@#@" + strTablaHtmlConSec;
    }

    private string GetProfesionales(int idExpProf, bool bModoLectura)
    {
        strTablaHtml = SUPER.BLL.EXPPROFFICEPI.getProfesionales(null, idExpProf, bModoLectura);

        return "OK@#@" + strTablaHtml;
    }
    private string GetProfesionales(int t301_idproyecto, int idExpProf, bool bModoLectura)
    {
        strTablaHtml = SUPER.BLL.EXPPROFFICEPI.getProfesionales(null, t301_idproyecto, idExpProf, bModoLectura);

        return "OK@#@" + strTablaHtml;
    }

    protected string Grabar(string idProyNew, int idEP, int idPSN, string strDatosGen, string strDatosACT, string strDatosACS, string strProf)
    {
        string sResul = "";
        string sEPNewMasInsertados = "-1";
        try
        {
            if (idProyNew=="")
                sEPNewMasInsertados = EXPPROF.Grabar(idPSN, idEP, idPSN, strDatosGen, strDatosACT, strDatosACS, strProf);
            else
                sEPNewMasInsertados = EXPPROF.Grabar(int.Parse(idProyNew), idEP, idPSN, strDatosGen, strDatosACT, strDatosACS, strProf);

            sResul = "OK@#@" + sEPNewMasInsertados;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar.", ex, false);// +"@#@" + sDesc;
        }
        return sResul;
    }
}
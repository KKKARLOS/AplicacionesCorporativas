using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using IB.SUPER.Shared;

public partial class Capa_Presentacion_Consultas_Facturabilidad_Default : System.Web.UI.Page
{
    public string idficepi;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        obtenerOpcionReconexion();

        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";

        string script1 = "IB.vars.imgFacturable = '" + Session["strServer"].ToString() + "images/imgIcoMonedas.gif';";
        script1 += "IB.vars.imgNoFacturable = '" + Session["strServer"].ToString() + "images/imgIcoMonedasOff.gif';";
        script1 += "IB.vars.idficepi = '" + Session["IDFICEPI_IAP"] + "';";
        script1 += "IB.vars.nReconectar = '" + Session["reconectar_iap"].ToString() + "';";
        //script1 += "IB.vars.imageUrl = 'images/imgUSU" + Session["TIPORECURSO"].ToString() + Session["SEXOUSUARIO"].ToString() + ".gif';";
        script1 += "IB.vars.UMC_IAP = " + Session["UMC_IAP"].ToString();        
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

        //Carga de guia de ayuda
        aspnetUtils.visualizarGuia(Menu);
      
    }
    private void obtenerOpcionReconexion()
    {
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()
            //|| User.IsInRole("S")
            )//SECRETARIA    --  PENDIENTE DE DETERMINAR QUÉ HARÁN LAS SECRETARIAS
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "A";
        }
        else if (User.IsInRole("RG") && User.IsInRole("SN"))//Session["reconectar_iap"].ToString() == "" && 
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "GS";
        }
        else if (User.IsInRole("RG"))//Session["reconectar_iap"].ToString() == "" && 
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "RG";
        }
        else if (User.IsInRole("SN"))
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "SN";
        }
        else
        {
            if (Session["IDRED"].ToString() != "")
            {
                //Contemplar que la persona pueda tener dos usuario con los que imputar
                //Ej: externo que pasa a interno
                if (SUPER.Capa_Negocio.Recurso.ObtenerCountUsuarios(Session["IDRED"].ToString()) > 1)
                {
                    Session["reconectar_iap"] = "1";
                    Session["perfil_iap"] = "P";  //Personal
                }
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.ConsultaFacturabilidad> getFacturabilidadProyectos(string sDesde, string sHasta)
    {
        BLL.ConsultaFacturabilidad bFacturabilidad = new BLL.ConsultaFacturabilidad();
        try
        {
            DateTime dDesde = DateTime.Parse(sDesde), dHasta = DateTime.Parse(sHasta);
            int codProf = (int)HttpContext.Current.Session["IDFICEPI_IAP"];
            List<Models.ConsultaFacturabilidad> lFacturabilidad = null;
            lFacturabilidad = bFacturabilidad.Catalogo(codProf, dDesde, dHasta);
            bFacturabilidad.Dispose();
            return lFacturabilidad;
        }
        catch (Exception ex)
        {
            if (bFacturabilidad != null) bFacturabilidad.Dispose();
            throw ex;
        }
        finally
        {
            bFacturabilidad.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.Recursos establecerUsuarioIAP(string sUsuario)
    {
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.Recursos oRecursos = new BLL.Recursos(cDblib);
        BLL.FestivosCals oFestivosCals = new BLL.FestivosCals(cDblib);
        Models.Recursos oRecursoModel = new Models.Recursos();                

        try
        {
            Models.Recursos cProfesionalIap = new Models.Recursos();
            oRecursoModel = oRecursos.establecerUsuarioIAP("", int.Parse(sUsuario));
            oRecursoModel.aFestivos = oFestivosCals.CatalogoFestivosString(oRecursoModel.IdCalendario, Fechas.AnnomesAFecha((int)oRecursoModel.t303_ultcierreIAP).AddMonths(1).AddDays(-1));

            HttpContext.Current.Session["NUM_EMPLEADO_IAP"] = oRecursoModel.t314_idusuario;
            HttpContext.Current.Session["DES_EMPLEADO_IAP"] = oRecursoModel.NOMBRE + " " + oRecursoModel.APELLIDO1 + " " + oRecursoModel.APELLIDO2;
            HttpContext.Current.Session["IDFICEPI_IAP"] = oRecursoModel.t001_IDFICEPI;
            HttpContext.Current.Session["IDRED_IAP"] = oRecursoModel.t001_codred;
            HttpContext.Current.Session["JORNADA_REDUCIDA"] = oRecursoModel.t314_jornadareducida;
            HttpContext.Current.Session["CONTROLHUECOS"] = oRecursoModel.t314_controlhuecos;
            HttpContext.Current.Session["IDCALENDARIO_IAP"] = oRecursoModel.IdCalendario;
            HttpContext.Current.Session["DESCALENDARIO_IAP"] = oRecursoModel.desCalendario;
            HttpContext.Current.Session["COD_CENTRO"] = oRecursoModel.T009_IDCENTRAB;
            HttpContext.Current.Session["DES_CENTRO"] = oRecursoModel.T009_DESCENTRAB;
            HttpContext.Current.Session["FEC_ULT_IMPUTACION"] = (!oRecursoModel.fUltImputacion.Equals(null)) ? ((DateTime)oRecursoModel.fUltImputacion).ToShortDateString() : null;
            HttpContext.Current.Session["FEC_ALTA"] = oRecursoModel.t314_falta.ToShortDateString();
            HttpContext.Current.Session["FEC_BAJA"] = (!Convert.IsDBNull(oRecursoModel.t314_fbaja)) ? ((DateTime)oRecursoModel.t314_fbaja).ToShortDateString() : null;
            HttpContext.Current.Session["UMC_IAP"] = (!Convert.IsDBNull(oRecursoModel.t303_ultcierreIAP)) ? (int?)oRecursoModel.t303_ultcierreIAP : DateTime.Now.AddMonths(-1).Year * 100 + DateTime.Now.AddMonths(-1).Month;
            HttpContext.Current.Session["NHORASRED"] = oRecursoModel.t314_horasjor_red;
            HttpContext.Current.Session["FECDESRED"] = (!Convert.IsDBNull(oRecursoModel.t314_fdesde_red)) ? ((DateTime)oRecursoModel.t314_fdesde_red).ToShortDateString() : null;
            HttpContext.Current.Session["FECHASRED"] = (!Convert.IsDBNull(oRecursoModel.t314_fhasta_red)) ? ((DateTime)oRecursoModel.t314_fhasta_red).ToShortDateString() : null;
            HttpContext.Current.Session["aSemLab"] = oRecursoModel.t066_semlabL + "," + oRecursoModel.t066_semlabM + "," + oRecursoModel.t066_semlabX + "," + oRecursoModel.t066_semlabJ + "," + oRecursoModel.t066_semlabV + "," + oRecursoModel.t066_semlabS + "," + oRecursoModel.t066_semlabD;
            HttpContext.Current.Session["SEXOUSUARIO"] = oRecursoModel.t001_sexo;
            HttpContext.Current.Session["TIPORECURSO"] = oRecursoModel.tipo;

            return oRecursoModel;
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("Error al establecer el usuario." + ex.Message));
        }
        finally
        {
            oFestivosCals.Dispose();
            oRecursos.Dispose();                        
            DBConn.Dispose();
        }
    }
    
}
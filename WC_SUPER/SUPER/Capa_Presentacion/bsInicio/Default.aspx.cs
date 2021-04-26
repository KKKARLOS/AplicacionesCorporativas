using Newtonsoft.Json;
using SUPER.Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using IB.SUPER.Shared;
using System.Configuration;

public partial class Capa_Presentacion_Inicio2_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    public string imgFotoBase64;    
    public string sUS = "", sMB = "", sRP = "", sMensajeMMOFF = "", sIG = "", sCI = "", sPP = "";
    protected bool bMostrarMMOFF = false;
    protected bool BloquearPGEByAcciones = false;
    protected bool BloquearPSTByAcciones = false;
    protected bool BloquearIAPByAcciones = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //SUPER.DAL.Log.Insertar("bsInicio->1");
            //Mensaje de bienvenida ( foto de usuario )                                    
            if (Session["FOTOUSUARIO"] != null) {
                imgFotoBase64 = System.Convert.ToBase64String((byte[])Session["FOTOUSUARIO"]);                
            }
            //SUPER.DAL.Log.Insertar("bsInicio->2");
            //25/05/2015 Petición de Javi Asenjo. Para verificar si la petición de entrada a SUPER viene del CURVIT viejo
            //en cuyo caso se le redirije a la pantala de CVT->MiCV
            if (Session["OLDCURVIT"] != null)
            {
                if (Session["OLDCURVIT"].ToString() == "S")
                {
                    string scriptCVT = "IB.vars.vieneDeCurvitViejo = 'S';";
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptCVT", scriptCVT, true);                    
                }
            }
            //SUPER.DAL.Log.Insertar("bsInicio->3");
            if (Request.QueryString["sUS"] != null) sUS = Request.QueryString["sUS"].ToString();

            //Mensaje de Bienvenida (Foto usuario)
            if (Request.QueryString["sMB"] != null)
            {
                if (Request.QueryString["sMB"].ToString() == "M")                    
                    mostrarMensajeBienvenida();                
            }
            //SUPER.DAL.Log.Insertar("bsInicio->4");
            //Si hay novedades no leídas y no mostradas, cargamos el fichero novedades.js
            //if (true)
            if (Session["NOVEDADESLEIDAS"] == null) Session["NOVEDADESLEIDAS"] = "0";
            if (Session["HAYNOVEDADES"].ToString() == "1" && 
                Session["NOVEDADESLEIDAS"].ToString() == "0" && 
                !(bool)Session["NOVEDADESMOSTRADAS"])                        
            {
                Session["NOVEDADESMOSTRADAS"] = true;
                string scriptNovedades = "<script src='js/Novedades.js'></script>";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "scriptNovedades", scriptNovedades, false);
            }
            //SUPER.DAL.Log.Insertar("bsInicio->5");
            //Si hay avisos, cargamos el fichero avisos.js
            //if(true)
            if (Session["HAYAVISOS"] == null) Session["HAYAVISOS"] = "0";
            if (Session["HAYAVISOS"].ToString() == "1")
            {
                Session["HAYAVISOS"] = "0"; //19/12/2014: Victor dixit: para que los avisos salgan una sola vez cuando se entra en la aplicación.
                string scriptAvisos = "<script src='js/AvisosView.js'></script>";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "scriptAvisos", scriptAvisos, false);
            }
            //SUPER.DAL.Log.Insertar("bsInicio->6");
        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("bsInicio->Error: " + ex.Message);
            LogError.LogearError("Error al obtener los datos de la Home", ex);            
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la Home.';", true);
        }

    }

    /// <summary>
    /// Obtiene todos los avisos de un usuario determinado
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.SUPER.Models.bsUsuariosAvisos> obtenerAvisos()
    {
        IB.SUPER.Negocio.bsUsuariosavisos bsUsuariosAvisosBLL = new IB.SUPER.Negocio.bsUsuariosavisos();

        try
        {
            //SUPER.DAL.Log.Insertar("bsInicio->obtenerAvisos");
            return bsUsuariosAvisosBLL.Select((int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"]);
        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("bsInicio->obtenerAvisos->Error: " + ex.Message);
            LogError.LogearError("Error al obtener los avisos del usuario", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los avisos del usuario."));
        }
        finally
        {
            if (bsUsuariosAvisosBLL != null) bsUsuariosAvisosBLL.Dispose();            
        }
    }

    /// <summary>
    /// Elimina un aviso de la Home 
    /// </summary>
    /// <param name="t448_idaviso"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void eliminarAviso(int t448_idaviso)
    {

        IB.SUPER.Negocio.bsUsuariosavisos bsUsuariosAvisosBLL = null;
        List<IB.SUPER.Models.bsUsuariosAvisos> lstbsUsuariosAvisosModels = null;

        try
        {
            //SUPER.DAL.Log.Insertar("bsInicio->eliminarAviso");
            bsUsuariosAvisosBLL = new IB.SUPER.Negocio.bsUsuariosavisos();
            int resultado = bsUsuariosAvisosBLL.Delete(t448_idaviso, (int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"]);
            lstbsUsuariosAvisosModels = bsUsuariosAvisosBLL.Select((int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"]);

            int iNumAvisos = bsUsuariosAvisosBLL.CountByUsuario((int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"]);

            if (iNumAvisos == 0)
                HttpContext.Current.Session["HAYAVISOS"] = "0";

            bsUsuariosAvisosBLL.Dispose();

        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("bsInicio->eliminarAviso->Error: " + ex.Message);
            LogError.LogearError("Error al eliminar el aviso", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han eliminar el aviso."));            
        }

        finally {
            if (bsUsuariosAvisosBLL != null) bsUsuariosAvisosBLL.Dispose();
        }
    }

    /// <summary>
    /// Cuando viene del menú viejo de SUPER
    /// </summary>
    public void mostrarMensajeBienvenida()
    {
        try
        {
            //SUPER.DAL.Log.Insertar("bsInicio->mostrarMensajeBienvenida");
            Session["BIENVENIDAMOSTRADA"] = false;
            Session["MostrarMensajeBienvenida"] = true;
            if (Session["FOTOUSUARIO"] == null)
            {
                Recurso objRec = new Recurso();
                bool bIdentificado = objRec.ObtenerRecurso(Session["IDRED"].ToString(), ((int)Session["UsuarioActual"] == 0) ? null : (int?)int.Parse(Session["UsuarioActual"].ToString()));
                if (bIdentificado) Session["FOTOUSUARIO"] = objRec.t001_foto;
            }
            if (Session["FOTOUSUARIO"] != null)
                imgFotoBase64 = System.Convert.ToBase64String((byte[])Session["FOTOUSUARIO"]);
            else
                imgFotoBase64 = "";
        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("bsInicio->mostrarMensajeBienvenida->Error: " + ex.Message);
            LogError.LogearError("Error al mostrar el mensaje de bienvenida", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido mostrar el mensaje de bienvenida."));            
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ObtenerAccionesPendientes()
    {

        IB.SUPER.Negocio.bsUsuario bsUsuariosBLL = null;
        List<IB.SUPER.Models.bsUsuario> lstbsUsuarioModels = null;
        try
        {
            //SUPER.DAL.Log.Insertar("bsInicio->ObtenerAccionesPendientes");
            int? idFicepi = null;
            int? idUser = null;

            HttpContext.Current.Session["BloquearPGEByAcciones"] = false;
            HttpContext.Current.Session["BloquearPSTByAcciones"] = false;
            HttpContext.Current.Session["BloquearIAPByAcciones"] = false;
            

            if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString() ||
                HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString())
            {//Hay reconexión como otro
                if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString())
                    idFicepi = int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString());

                if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString())
                    idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            }
            else
            {
                idFicepi = int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString());
                idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            }

            bsUsuariosBLL = new IB.SUPER.Negocio.bsUsuario();

            lstbsUsuarioModels = bsUsuariosBLL.Catalogo(idUser, idFicepi, int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()));            
            bsUsuariosBLL.Dispose();

            string retval = JsonConvert.SerializeObject(lstbsUsuarioModels);

            return retval;

        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("bsInicio->mostrarMensajeBienvenida->Error: " + ex.Message);
            LogError.LogearError("Error al obtener las acciones pendientes", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener las acciones pendientes."));
        }

        finally {
            if (bsUsuariosBLL != null) bsUsuariosBLL.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void BienvenidaMostrada()
    {
        try
        {
            //SUPER.DAL.Log.Insertar("bsInicio->BienvenidaMostrada");
            HttpContext.Current.Session["BIENVENIDAMOSTRADA"] = true;
            HttpContext.Current.Session["MostrarMensajeBienvenida"] = false;
                        
        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("bsInicio->BienvenidaMostrada->Error: " + ex.Message);
            LogError.LogearError("Error al ejecutar la Bienvenida mostrada", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido mostrar el mensaje de bienvenida."));            
        }
    }

    /// <summary>
    /// obtiene los usuarios super (A día de hoy, Pablo Carretero es el único con dos usuarios 17/10/2016)
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.SUPER.SIC.Models.ProfesionalSimple> UsuariosSuper()
    {

        IB.SUPER.SIC.BLL.AyudaProfesionales cAP = null;
        try
        {
            //SUPER.DAL.Log.Insertar("bsInicio->UsuariosSuper");
            cAP = new IB.SUPER.SIC.BLL.AyudaProfesionales();

            List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.UsuariosSuper(HttpContext.Current.Session["IDRED"].ToString(), byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"].ToString()));

            cAP.Dispose();

            return lst;
        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("bsInicio->UsuariosSuper->Error: " + ex.Message);
            cAP.Dispose();

            LogError.LogearError("Ocurrió un error obteniendo la lista de profesionales", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de profesionales"));
        }


    }
}
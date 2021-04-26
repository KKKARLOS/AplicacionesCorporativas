using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using IB.SUPER.Shared;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using System.Web.UI.HtmlControls;

public partial class Capa_Presentacion_Consultas_ParteAct_Default : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";

        try
        {
            //Variables de sesión
            string script1 = "IB.vars.superEditor = '" + Utilidades.EsAdminProduccion() + "';";
            
            //script1 += "IB.vars.codUsu = '" + Session["NUM_EMPLEADO_IAP"].ToString() + "';";

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

            //Para evitar que se pueda modificar en cliente la vision de CRs
            HtmlGenericControl divCR = new HtmlGenericControl("div");

            if (Utilidades.EsAdminProduccion())
            {
                divCR = (HtmlGenericControl)this.FindControl("CR");
            }
            else
            {
                divCR = (HtmlGenericControl)this.FindControl("CRAdmin");
            }
            
            divCR.Visible = false;

        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
    }

    /// <summary>
    /// Obtiene los partes de actividad en base a los filtros de la pantalla
    /// </summary>
    /// <returns>List<Models.ParteActividad></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ParteActividad> obtenerPartesActividad(string idClientes, string idproyectosubnodos, Nullable<bool> facturable, DateTime dDesde, DateTime dHasta)
    {
        BLL.ParteActividad ParteActividadBLL = new BLL.ParteActividad();

        try
        {

            List<Models.ParteActividad> lParteActividad = null;

            lParteActividad = ParteActividadBLL.Catalogo(HttpContext.Current.Session["NUM_EMPLEADO_IAP"].ToString(), idClientes, idproyectosubnodos, facturable, dDesde, dHasta);            
            return lParteActividad;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los partes de actividad", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los partes de actividad"));

        }
        finally
        {

            ParteActividadBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los proyectos en base a los filtros de su modal
    /// </summary>
    /// <returns>List<Models.DesgloseCalendario></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ProyectoEconomico> obtenerProyectos(Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, Nullable<int> idResponsable, Nullable<int> numPE, string sDesPE, string sTipoBusqueda, string sCualidad, Nullable<int> nContrato, Nullable<int> nHorizontal, 
                                                    Nullable<int> nCNP, Nullable<int> nCSN1P, Nullable<int> nCSN2P, Nullable<int> nCSN3P, Nullable<int> nCSN4P, Nullable<int> nModeloContratacion)
    {
        BLL.ProyectoEconomico ProyectoEconomicoBLL = new BLL.ProyectoEconomico();

        try
        {

            List<Models.ProyectoEconomico> lProyectoEconomico = null;

            lProyectoEconomico = ProyectoEconomicoBLL.Catalogo(Utilidades.EsAdminProduccion(), idNodo, sEstado, sCategoria, idCliente, idResponsable, numPE, sDesPE, sTipoBusqueda, sCualidad, nContrato, nHorizontal, nCNP, nCSN1P, nCSN2P, nCSN3P, nCSN4P, false, false, (int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], false, null, nModeloContratacion);
            return lProyectoEconomico;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los proyectos", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los proyectos"));

        }
        finally
        {

            ProyectoEconomicoBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los modelos de contratación
    /// </summary>
    /// <returns>List<Models.ModalidadContrato></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ModalidadContrato> obtenerModContra(Nullable<byte> t316_idmodalidad, string t316_denominacion, bool bTodos, byte nOrden, byte nAscDesc)
    {
        BLL.ModalidadContrato ModalidadContratoBLL = new BLL.ModalidadContrato();

        try
        {

            List<Models.ModalidadContrato> lModalidadContrato = null;

            lModalidadContrato = ModalidadContratoBLL.Catalogo(t316_idmodalidad, t316_denominacion, bTodos, nOrden, nAscDesc);
            return lModalidadContrato;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los modelos de contratación", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los modelos de contratación"));

        }
        finally
        {

            ModalidadContratoBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los nodos de un usuario
    /// </summary>
    /// <returns>List<Models.NodoProyUsu></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.NodoProyUsu> obtenerNodosCR()
    {
        BLL.NodoProyUsu NodoProyUsuBLL = new BLL.NodoProyUsu();

        try
        {

            List<Models.NodoProyUsu> lNodoProyUsu = null;

            lNodoProyUsu = NodoProyUsuBLL.Catalogo((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], true, false);
            return lNodoProyUsu;
            

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los nodos de C.R.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los nodos de C.R."));

        }
        finally
        {

            NodoProyUsuBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los nodos de un usuario administrador
    /// </summary>
    /// <returns>List<Models.NodoProyUsu></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.Nodo> obtenerNodosCRAdmin()
    {
        BLL.Nodo NodoBLL = new BLL.Nodo();

        try
        {

            List<Models.Nodo> lNodo = null;

            lNodo = NodoBLL.Catalogo();
            return lNodo;


        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los nodos de C.R.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los nodos de C.R."));

        }
        finally
        {

            NodoBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los contratos de un usuario
    /// </summary>
    /// <returns>List<Models.Contrato></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static Models.Contrato obtenerDenominacionContrato(int t306_idcontrato)
    {
        BLL.Contrato ContratoBLL = new BLL.Contrato();

        try
        {

            Models.Contrato oContrato = null;

            oContrato = ContratoBLL.ObtenerExtensionPadre(t306_idcontrato);
            return oContrato;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener información del contrato.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener información del contrato."));

        }
        finally
        {

            ContratoBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los contratos de un usuario
    /// </summary>
    /// <returns>List<Models.Contrato></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.Contrato> obtenerContratos(bool bMostrarTodos, Nullable<int> t306_idcontrato, string t377_denominacion, string sTipoBusq, Nullable<int> t302_idcliente)
    {
        BLL.Contrato ContratoBLL = new BLL.Contrato();

        try
        {

            List<Models.Contrato> lContrato = null;

            lContrato = ContratoBLL.Catalogo((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], bMostrarTodos, t306_idcontrato, t377_denominacion, sTipoBusq, t302_idcliente, Utilidades.EsAdminProduccion());
            return lContrato;


        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los contratos.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los contratos."));

        }
        finally
        {

            ContratoBLL.Dispose();

        }
    }    
}
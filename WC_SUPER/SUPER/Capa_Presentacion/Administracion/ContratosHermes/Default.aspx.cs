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

public partial class Capa_Presentacion_Administracion_ContratosHermes_Default : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        IB.SUPER.APP.BLL.Parametro oPar = new IB.SUPER.APP.BLL.Parametro();
        try
        {
            //Variables de sesión
            string script1 = "IB.vars.superEditor = '" + Utilidades.EsAdminProduccion() + "';";
            string sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            script1 += "IB.vars.denNodoCorta='"+ sNodo + "';";

            IB.SUPER.APP.Models.Parametro mResponsableContrato = oPar.GetDatos(5);
            script1 += "IB.vars.codResponsable='" + mResponsableContrato.valor.ToString() + "';";
            script1 += "IB.vars.denResponsable='" + mResponsableContrato.denominacion + "';";
            

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);
            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
        finally
        {
            oPar.Dispose();
        }
    }

    /// <summary>
    /// Obtiene los partes de actividad en base a los filtros de la pantalla
    /// </summary>
    /// <returns>List<Models.ParteActividad></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.SUPER.APP.Models.OportunidadNegocio> obtenerOportunidades(int idNodo, DateTime dDesde, DateTime dHasta)
    {
        IB.SUPER.APP.BLL.OportunidadNegocio OportunidadBLL = new IB.SUPER.APP.BLL.OportunidadNegocio();
        try
        {
            List<IB.SUPER.APP.Models.OportunidadNegocio> lOportunidad = null;

            lOportunidad = OportunidadBLL.CatalogoSinContrato(idNodo, dDesde, dHasta);
            return lOportunidad;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener las oportunidades de negocio de HERMES", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener las oportunidades de negocio de HERMES"));
        }
        finally
        {
            OportunidadBLL.Dispose();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.SUPER.APP.Models.ProyectoEconomico> generar(List<IB.SUPER.APP.Models.OportunidadNegocio> Oportunidades)
    {
        // Genera contratos para cada una de las oportunidades de negocio seleccionadas
        IB.SUPER.APP.BLL.OportunidadNegocio OportunidadBLL = new IB.SUPER.APP.BLL.OportunidadNegocio();
        try
        {
            //AsuntoBLL.grabar(DatosGenerales, Integrantes);
            List<IB.SUPER.APP.Models.ProyectoEconomico> lProyectoEconomico = null;
            lProyectoEconomico = OportunidadBLL.generarContratos(Oportunidades);

            return lProyectoEconomico;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al generar contratos.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al generar contratos. " + ex.Message));
        }
        finally
        {
            OportunidadBLL.Dispose();
        }
    }

}
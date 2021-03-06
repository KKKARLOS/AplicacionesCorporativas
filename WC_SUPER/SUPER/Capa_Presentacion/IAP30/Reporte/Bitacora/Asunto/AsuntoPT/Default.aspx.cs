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
using System.Text.RegularExpressions;
using System.Text;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Reporte_Bitacora_Asunto_AsuntoPT_Default : System.Web.UI.Page
{
    //public string sNodo = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        //Recogida de parámetros y volcado en IB.vars

        string sEstadoProy = "";

        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.NodoPT oNodoPT_BLL = new BLL.NodoPT(cDblib);
        BLL.ProyectoEconomico oEstadoPE_BLL = new BLL.ProyectoEconomico(cDblib);
        BLL.ProyectoTecnico oPT_BLL = new BLL.ProyectoTecnico(cDblib);

        try
        {
            string sIdUser = Session["NUM_EMPLEADO_IAP"].ToString();
            //sNodo = SUPER.Capa_Negocio.Estructura.getDefCorta(SUPER.Capa_Negocio.Estructura.sTipoElem.NODO);

            string script1 = "IB.vars.codUsu = '" + sIdUser + "';";
            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());

            if (ht["idAsunto"].ToString() != "")
                script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
            else
                script1 += "IB.vars.idAsunto = '';";

            if (ht["p"].ToString() != "")
                script1 += "IB.vars.permiso = '" + ht["p"].ToString() + "';";
            else
                script1 += "IB.vars.permiso = '';";

            if (ht["ori"] != null && ht["ori"].ToString() != "")
                script1 += "IB.vars.origen = '" + ht["ori"].ToString() + "';";
            else
                script1 += "IB.vars.origen = '';";


            if (ht["nPT"] != null && ht["nPT"].ToString() != "")
            {
                script1 += "IB.vars.nPT = '" + ht["nPT"].ToString() + "';";

                // Obtener el nodo del proyecto técnico
                Models.NodoPT oNodoPT = new Models.NodoPT();

                oNodoPT = oNodoPT_BLL.Select(int.Parse(ht["nPT"].ToString()));
                script1 += "IB.vars.idNodo = '" + oNodoPT.t303_idnodo + "';";

                Models.ProyectoTecnico oPT = new Models.ProyectoTecnico();
                oPT = oPT_BLL.Select(int.Parse(ht["nPT"].ToString()));
                script1 += "IB.vars.nPE = '" + oPT.num_proyecto + "';";
                script1 += "IB.vars.estadoProyecto = '" + oPT.t301_estado + "';";
                script1 += "IB.vars.desPE = '" + oPT.nom_proyecto + "';";
                script1 += "IB.vars.nPSN = '" + oPT.t305_idproyectosubnodo.ToString() + "';";
                sEstadoProy = oPT.t301_estado;
            }
            else
            {
                script1 += "IB.vars.nPT = '';";
                script1 += "IB.vars.idNodo = '';";
                if (ht["nPE"] != null && ht["nPE"].ToString() != "")
                {
                    script1 += "IB.vars.nPE = '" + ht["nPE"].ToString() + "';";
                    // Obtener el estado del proyecto económico
                    Models.ProyectoEconomico oEstadoPE = new Models.ProyectoEconomico();
                    oEstadoPE = oEstadoPE_BLL.Select(int.Parse(ht["nPE"].ToString()));
                    script1 += "IB.vars.estadoProyecto = '" + oEstadoPE.t301_estado + "';";
                    sEstadoProy = oEstadoPE.t301_estado;
                }
                else
                {
                    script1 += "IB.vars.nPE = '';";
                    script1 += "IB.vars.estadoProyecto = '';";
                }
                if (ht["desPE"] != null && ht["desPE"].ToString() != "")
                    script1 += "IB.vars.desPE = '" + ht["desPE"].ToString() + "';";
                else
                    script1 += "IB.vars.desPE = '';";
                if (ht["nPSN"].ToString() != "")
                    script1 += "IB.vars.nPSN = '" + ht["nPSN"].ToString() + "';";
                else
                    script1 += "IB.vars.nPSN = '';";
            }

            if (ht["desPT"].ToString() != "")
                script1 += "IB.vars.desPT = '" + ht["desPT"].ToString() + "';";
            else
                script1 += "IB.vars.desPT = '';";


            script1 += "IB.vars.fechaDia = '" + DateTime.Now.ToShortDateString() + "';";
            script1 += "IB.vars.idEmpleadoEntrada = '" + Session["NUM_EMPLEADO_ENTRADA"].ToString() + "';";
            script1 += "IB.vars.nombreEmpleadoEntrada = '" + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString() + "';";

            //Modo en el se accederá al contenedor de documentos --> Edición o Consulta
            string sModoContainer = ht["p"].ToString();
            if (sModoContainer != "E") sModoContainer = "C";
            else
            {
                if (sEstadoProy == "C" || sEstadoProy == "H") sModoContainer = "C";
            }
            script1 += "IB.vars.superEditor = '" + Utilidades.EsAdminProduccion() + "';";
            script1 += "IB.vars.sModoContainer = '" + sModoContainer + "';";
            script1 += "IB.vars.idResponsable = '" + Session["NUM_EMPLEADO_ENTRADA"].ToString() + "';";
            script1 += "IB.vars.coEstadoAnterior = '0';";

            script1 += "IB.vars.bCambios = 0;";
            //parametros para poder volver a la pantalla de imputación
            script1 += "IB.vars.qs = '" + Request.QueryString.ToString() + "';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

            //ObtenerDatosAsunto();
        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars.error = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
        finally
        {
            oNodoPT_BLL.Dispose();
            oEstadoPE_BLL.Dispose();
            oPT_BLL.Dispose();
            DBConn.Dispose();
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.TipoAsunto> getAsuntos()
    {
        IB.SUPER.IAP30.BLL.TipoAsunto cCatalogoTipoAsunto = new IB.SUPER.IAP30.BLL.TipoAsunto();

        try
        {
            List<Models.TipoAsunto> lTipoAsuntos = cCatalogoTipoAsunto.Catalogo();
            return lTipoAsuntos;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se han podido obtener los tipos de asunto.", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los tipos de asunto. " + ex.Message));
        }
        finally
        {
            cCatalogoTipoAsunto.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.AsuntoPT obtenerDetalleAsunto(string idAsunto)
    {

        BLL.AsuntoPT asuntoPTBitacoraBLL = new BLL.AsuntoPT();
        Models.AsuntoPT oAsuntoPTBitacora;
        try
        {
            oAsuntoPTBitacora = asuntoPTBitacoraBLL.Select(Int32.Parse(idAsunto));
            return oAsuntoPTBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle del asunto (" + idAsunto + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle del asunto (" + idAsunto + ")." + ex.Message));
        }
        finally
        {
            asuntoPTBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AsuntoEstadoPT> obtenerDetalleCronologia(string idAsunto)
    {

        BLL.AsuntoEstadoPT asuntoEstadoPTBitacoraBLL = new BLL.AsuntoEstadoPT();
        List<Models.AsuntoEstadoPT> oAsuntoEstadoPTBitacora;
        try
        {
            oAsuntoEstadoPTBitacora = asuntoEstadoPTBitacoraBLL.Catalogo(Int32.Parse(idAsunto));
            return oAsuntoEstadoPTBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle de la cronología (" + idAsunto + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la cronología (" + idAsunto + ")." + ex.Message));
        }
        finally
        {
            asuntoEstadoPTBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AsuntoRecursosPT> obtenerProfesionalesAsunto(string idAsunto)
    {

        BLL.AsuntoRecursosPT asuntoRecursosPTBitacoraBLL = new BLL.AsuntoRecursosPT();
        List<Models.AsuntoRecursosPT> oListaAsuntoRecursosPTBitacora;
        Models.AsuntoRecursosPT oAsuntoRecurso = new Models.AsuntoRecursosPT();
        try
        {
            oAsuntoRecurso.T409_idasunto = Int32.Parse(idAsunto);
            oListaAsuntoRecursosPTBitacora = asuntoRecursosPTBitacoraBLL.Catalogo(oAsuntoRecurso);
            return oListaAsuntoRecursosPTBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se han podido obtener los profesionales del asunto (" + idAsunto + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los profesionales del asunto (" + idAsunto + ")." + ex.Message));
        }
        finally
        {
            asuntoRecursosPTBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.Tecnicos> obtenerProfesionales(Models.Tecnicos oTecnicos)
    {
        BLL.Tecnicos RecursosPEBitacoraBLL = new BLL.Tecnicos();
        List<Models.Tecnicos> oListaRecursosPEBitacora;
        try
        {
            oTecnicos.Cualidad = "";
            oListaRecursosPEBitacora = RecursosPEBitacoraBLL.Catalogo(oTecnicos);
            return oListaRecursosPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se han podido obtener los profesionales.", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los profesionales." + ex.Message));
        }
        finally
        {
            RecursosPEBitacoraBLL.Dispose();
        }
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int grabar(Models.AsuntoPT DatosGenerales, List<Models.AsuntoRecursosPT> Integrantes)
    {
        // Grabar datos
        BLL.AsuntoPT AsuntoBLL = new BLL.AsuntoPT();
        int idReferencia;
        bool bAlta;
        try
        {
            if (DatosGenerales.T409_idasunto == -1) bAlta = true;
            else bAlta = false;
            idReferencia = AsuntoBLL.grabar(DatosGenerales, Integrantes);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar los datos del asunto", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al grabar los datos del asunto. " + ex.Message));
        }
        finally
        {
            AsuntoBLL.Dispose();
        }
        // Envío de Correos
        BLL.AsuntoPT CorreoAsuntoBLL = new BLL.AsuntoPT();
        try
        {
            DatosGenerales.T409_idasunto = idReferencia;
            CorreoAsuntoBLL.EnviarCorreo(DatosGenerales, Integrantes, bAlta);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al enviar correo de asunto de Bitácora de PE. Asunto=" + idReferencia, ex);
            throw new Exception(System.Uri.EscapeDataString("Error al enviar correo de asunto de Bitácora de PE. Asunto=" + idReferencia + " " + ex.Message));
        }
        finally
        {
            CorreoAsuntoBLL.Dispose();
        }
        return idReferencia;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.ProyectoSubNodoSD obtenerPSN_OPD(string nPSN)
    {

        BLL.ProyectoSubNodoSD proyectoSubnodoSD_BLL = new BLL.ProyectoSubNodoSD();
        Models.ProyectoSubNodoSD oProyectoSubnodoSD;
        try
        {

            oProyectoSubnodoSD = proyectoSubnodoSD_BLL.Select(Int32.Parse(nPSN));

            return oProyectoSubnodoSD;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener información del proyecto subnodo. PSN=" + nPSN, ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener información del proyecto subnodo (" + nPSN + ")."));
        }
        finally
        {
            proyectoSubnodoSD_BLL.Dispose();
        }
    }
}
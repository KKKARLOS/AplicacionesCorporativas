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

public partial class Capa_Presentacion_Reporte_Bitacora_Accion_AccionPE_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        //Recogida de parámetros y volcado en IB.vars
        string sEstadoProy = "";
        BLL.NodoPSN oNodoPSN_BLL = new BLL.NodoPSN();
        try
        {
            string sIdUser = Session["NUM_EMPLEADO_IAP"].ToString();
            //sNodo = SUPER.Capa_Negocio.Estructura.getDefCorta(SUPER.Capa_Negocio.Estructura.sTipoElem.NODO);

            string script1 = "IB.vars.codUsu = '" + sIdUser + "';";
            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());

            if (ht["idAccion"].ToString() != "")
                script1 += "IB.vars.idAccion = '" + ht["idAccion"].ToString() + "';";
            else
                script1 += "IB.vars.idAccion = '';";

            if (ht["idAsunto"] != null && ht["idAsunto"].ToString() != "")
                script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
            else
            {
                if (ht["idAccion"].ToString() != "")
                {
                    BLL.Accion accionPEBitacoraBLL = new BLL.Accion();
                    Models.Accion oAccionPEBitacora;
                    oAccionPEBitacora = accionPEBitacoraBLL.Select(Int32.Parse(ht["idAccion"].ToString()));
                    script1 += "IB.vars.idAsunto = '"+ oAccionPEBitacora.t382_idasunto.ToString() + "';";
                    accionPEBitacoraBLL.Dispose();
                }
                else
                    script1 += "IB.vars.idAsunto = '';";
            }
            if (ht["ori"] != null && ht["ori"].ToString() != "")
                script1 += "IB.vars.origen = '" + ht["ori"].ToString() + "';";
            else
                script1 += "IB.vars.origen = '';";

            if (ht["p"].ToString() != "")
                script1 += "IB.vars.permiso = '" + ht["p"].ToString() + "';";
            else
                script1 += "IB.vars.permiso = '';";

            if (ht["nPE"] != null && ht["nPE"].ToString() != "")
                script1 += "IB.vars.nPE = '" + ht["nPE"].ToString() + "';";
            else
                script1 += "IB.vars.nPE = '';";

            if (ht["desPE"] != null && ht["desPE"].ToString() != "")
                script1 += "IB.vars.desPE = '" + ht["desPE"].ToString() + "';";
            else
                script1 += "IB.vars.desPE = '';";

            if (ht["r"] != null && ht["r"].ToString() != "")
                script1 += "IB.vars.IdResponsable = '" + ht["r"].ToString() + "';";
            else
                script1 += "IB.vars.IdResponsable = '';";

            if (ht["nPSN"].ToString() != "")
            {

                script1 += "IB.vars.nPSN = '" + ht["nPSN"].ToString() + "';";
                // Obtener el nodo y el estado del proyecto subnodo
                Models.NodoPSN oNodoPSN = new Models.NodoPSN();
                oNodoPSN = oNodoPSN_BLL.Select(int.Parse(ht["nPSN"].ToString()));
                script1 += "IB.vars.idNodo = '" + oNodoPSN.t303_idnodo + "';";
                script1 += "IB.vars.estadoProyecto = '" + oNodoPSN.t301_estado + "';";
                sEstadoProy = oNodoPSN.t301_estado;
            }
            else
            {
                script1 += "IB.vars.nPSN = '';";
                script1 += "IB.vars.idNodo = '';";
                script1 += "IB.vars.estadoProyecto = '';";
            }

            script1 += "IB.vars.fechaDia = '" + DateTime.Now.ToShortDateString() + "';";
            script1 += "IB.vars.idEmpleadoEntrada = '" + Session["NUM_EMPLEADO_ENTRADA"].ToString() + "';";
            script1 += "IB.vars.nombreEmpleadoEntrada = '" + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString() + "';";

            //Modo en el se accederá al contenedor de documentos --> Edición o Consulta
            string sModoContainer = ht["p"].ToString();
            if (sModoContainer != "E") sModoContainer = "C";
            else{
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
            oNodoPSN_BLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.Asunto obtenerDetalleAsunto(string idAsunto)
    {

        BLL.Asunto asuntoPEBitacoraBLL = new BLL.Asunto();
        Models.Asunto oAsuntoPEBitacora;
        try
        {
            oAsuntoPEBitacora = asuntoPEBitacoraBLL.Select(Int32.Parse(idAsunto));
            return oAsuntoPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle del asunto (" + idAsunto + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle del asunto (" + idAsunto + ")." + ex.Message));
        }
        finally
        {
            asuntoPEBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.Accion obtenerDetalleAccion(string idAccion)
    {

        BLL.Accion accionPEBitacoraBLL = new BLL.Accion();
        Models.Accion oAccionPEBitacora;
        try
        {
            oAccionPEBitacora = accionPEBitacoraBLL.Select(Int32.Parse(idAccion));
            return oAccionPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle de la acción (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la acción (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            accionPEBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AccionTareas> obtenerRelacionTareas(string idAccion)
    {

        BLL.AccionTareas AccionTareasPEBitacoraBLL = new BLL.AccionTareas();
        List<Models.AccionTareas> oAccionTareasPEBitacora;
        try
        {
            oAccionTareasPEBitacora = AccionTareasPEBitacoraBLL.Catalogo(Int32.Parse(idAccion));
            return oAccionTareasPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle de las tareas (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de las tareas (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            AccionTareasPEBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AccionRecursos> obtenerProfesionalesAccion(string idAccion)
    {

        BLL.AccionRecursos accionRecursosPEBitacoraBLL = new BLL.AccionRecursos();
        List<Models.AccionRecursos> oListAccionRecursosPEBitacora;
        Models.AccionRecursos oAccionRecurso = new Models.AccionRecursos();
        try
        {
            oAccionRecurso.T383_idaccion= Int32.Parse(idAccion);
            oListAccionRecursosPEBitacora = accionRecursosPEBitacoraBLL.Catalogo(oAccionRecurso);
            return oListAccionRecursosPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se han podido obtener los profesionales de la acción (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los profesionales de la accción (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            accionRecursosPEBitacoraBLL.Dispose();
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
    public static int grabar(Models.Accion DatosGenerales, List<Models.AccionRecursos> Integrantes, List<Models.AccionTareas> Tareas)
    {
        // Grabar datos
        BLL.Accion AccionBLL = new BLL.Accion();
        int idReferencia;
        bool bAlta;
        try
        {
            if (DatosGenerales.t383_idaccion == -1) bAlta = true;
            else bAlta = false;
            idReferencia = AccionBLL.grabar(DatosGenerales, Integrantes, Tareas );
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar los datos de la acción", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al grabar los datos de la acción. " + ex.Message));
        }
        finally
        {
            AccionBLL.Dispose();
        }
        // Envío de Correos
        BLL.Accion CorreoAccionBLL = new BLL.Accion();
        try
        {
            DatosGenerales.t383_idaccion = idReferencia;
            CorreoAccionBLL.EnviarCorreo(DatosGenerales, Integrantes, bAlta);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al enviar correo de la acción de Bitácora de PE. Acción=" + idReferencia, ex);
            throw new Exception(System.Uri.EscapeDataString("Error al enviar correo de acción de Bitácora de PE. Acción=" + idReferencia + " " + ex.Message));
        }
        finally
        {
            CorreoAccionBLL.Dispose();
        }
        return idReferencia;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.Tarea> obtenerTareas(string nPSN)
    {
        BLL.Tarea TareasPEBitacoraBLL = new BLL.Tarea();
        List<Models.Tarea> oListaTareasPEBitacora;
        try
        {
            oListaTareasPEBitacora = TareasPEBitacoraBLL.Catalogo(Int32.Parse(nPSN));
            return oListaTareasPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se han podido obtener las tareas a seleccionar.", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener las tareas a seleccionar." + ex.Message));
        }
        finally
        {
            TareasPEBitacoraBLL.Dispose();
        }
    }
}
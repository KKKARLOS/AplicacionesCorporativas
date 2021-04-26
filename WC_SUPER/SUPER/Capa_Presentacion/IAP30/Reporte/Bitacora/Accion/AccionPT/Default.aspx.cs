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

public partial class Capa_Presentacion_Reporte_Bitacora_Accion_AccionPT_Default : System.Web.UI.Page
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

        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.NodoPT oNodoPT_BLL = new BLL.NodoPT(cDblib);
        BLL.ProyectoEconomico oEstadoPE_BLL = new BLL.ProyectoEconomico(cDblib);
        BLL.AccionPT oAccion_BLL = new BLL.AccionPT(cDblib);

        try
        {
            string sIdUser = Session["NUM_EMPLEADO_IAP"].ToString();
            //sNodo = SUPER.Capa_Negocio.Estructura.getDefCorta(SUPER.Capa_Negocio.Estructura.sTipoElem.NODO);

            string script1 = "IB.vars.codUsu = '" + sIdUser + "';";
            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());

            if (ht["ori"] != null && ht["ori"].ToString() != "")
                script1 += "IB.vars.origen = '" + ht["ori"].ToString() + "';";
            else
                script1 += "IB.vars.origen = '';";

            if (ht["idAccion"].ToString() != "")
            {
                script1 += "IB.vars.idAccion = '" + ht["idAccion"].ToString() + "';";
                if (ht["idAsunto"] != null && ht["idAsunto"].ToString() != "")
                    script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
                else
                {
                    //script1 += "IB.vars.idAsunto = '';";
                    Models.AccionPT oAccion = new Models.AccionPT();
                    oAccion = oAccion_BLL.Select(int.Parse(ht["idAccion"].ToString()));
                    script1 += "IB.vars.idAsunto = '" + oAccion.T409_idasunto.ToString() + "';";
                }
            }
            else
            {
                script1 += "IB.vars.idAccion = '';";
                if (ht["idAsunto"] != null && ht["idAsunto"].ToString() != "")
                    script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
                else
                    script1 += "IB.vars.idAsunto = '';";
            }

            if (ht["p"].ToString() != "")
                script1 += "IB.vars.permiso = '" + ht["p"].ToString() + "';";
            else
                script1 += "IB.vars.permiso = '';";

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

            if (ht["nPT"] != null && ht["nPT"].ToString() != "")
            {
                script1 += "IB.vars.nPT = '" + ht["nPT"].ToString() + "';";
                
                // Obtener el nodo del proyecto técnico
                Models.NodoPT oNodoPT = new Models.NodoPT();

                oNodoPT = oNodoPT_BLL.Select(int.Parse(ht["nPT"].ToString()));
                script1 += "IB.vars.idNodo = '" + oNodoPT.t303_idnodo + "';";
            }
            else
            {
                script1 += "IB.vars.nPT = '';";
                script1 += "IB.vars.idNodo = '';";
            }


            if (ht["desPT"] != null && ht["desPT"].ToString() != "")
                script1 += "IB.vars.desPT = '" + ht["desPT"].ToString() + "';";
            else
                script1 += "IB.vars.desPT = '';";

            if (ht["r"] != null && ht["r"].ToString() != "")
                script1 += "IB.vars.IdResponsable = '" + ht["r"].ToString() + "';";
            else
                script1 += "IB.vars.IdResponsable = '';";


            if (ht["nPSN"] != null && ht["nPSN"].ToString() != "")
                script1 += "IB.vars.nPSN = '" + ht["nPSN"].ToString() + "';";
            else
                script1 += "IB.vars.nPSN = '';";

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
            oAccion_BLL.Dispose();
            DBConn.Dispose();
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
    public static Models.AccionPT obtenerDetalleAccion(string idAccion)
    {

        BLL.AccionPT accionPTBitacoraBLL = new BLL.AccionPT();
        Models.AccionPT oAccionPTBitacora;
        try
        {
            oAccionPTBitacora = accionPTBitacoraBLL.Select(Int32.Parse(idAccion));
            return oAccionPTBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle de la acción (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la acción (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            accionPTBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AccionTareasPT> obtenerRelacionTareas(string idAccion)
    {

        BLL.AccionTareasPT AccionTareasPTBitacoraBLL = new BLL.AccionTareasPT();
        List<Models.AccionTareasPT> oAccionTareasPTBitacora;
        try
        {
            oAccionTareasPTBitacora = AccionTareasPTBitacoraBLL.Catalogo(Int32.Parse(idAccion));
            return oAccionTareasPTBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle de las tareas (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de las tareas (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            AccionTareasPTBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AccionRecursosPT> obtenerProfesionalesAccion(string idAccion)
    {

        BLL.AccionRecursosPT accionRecursosPTBitacoraBLL = new BLL.AccionRecursosPT();
        List<Models.AccionRecursosPT> oListAccionRecursosPEBitacora;
        Models.AccionRecursosPT oAccionRecurso = new Models.AccionRecursosPT();
        try
        {
            oAccionRecurso.t410_idaccion = Int32.Parse(idAccion);
            oListAccionRecursosPEBitacora = accionRecursosPTBitacoraBLL.Catalogo(oAccionRecurso);
            return oListAccionRecursosPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se han podido obtener los profesionales de la acción (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los profesionales de la accción (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            accionRecursosPTBitacoraBLL.Dispose();
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
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los profesionales. " + ex.Message));
        }
        finally
        {
            RecursosPEBitacoraBLL.Dispose();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int grabar(Models.AccionPT DatosGenerales, List<Models.AccionRecursosPT> Integrantes, List<Models.AccionTareasPT> Tareas)
    {
        // Grabar datos
        BLL.AccionPT AccionBLL = new BLL.AccionPT();
        int idReferencia=0;
        bool bAlta;
        try
        {
            if (DatosGenerales.T410_idaccion == -1) bAlta = true;
            else bAlta = false;
            idReferencia = AccionBLL.grabar(DatosGenerales, Integrantes, Tareas);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar los datos de la acción.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al grabar los datos de la acción. " + ex.Message));
        }
        finally
        {
            AccionBLL.Dispose();
        }
        // Envío de Correos
        BLL.AccionPT CorreoAccionBLL = new BLL.AccionPT();
        try
        {
            DatosGenerales.T410_idaccion = idReferencia;
            CorreoAccionBLL.EnviarCorreo(DatosGenerales, Integrantes, bAlta);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al enviar correo de asunto de Bitácora de PT. Acción=" + idReferencia, ex);
            throw new Exception(System.Uri.EscapeDataString("Error al enviar correo de acción de Bitácora de PT. Acción=" + idReferencia + " " + ex.Message));
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
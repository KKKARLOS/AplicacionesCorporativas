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

public partial class Capa_Presentacion_Reporte_Bitacora_Accion_AccionTarea_Default : System.Web.UI.Page
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

        BLL.TareaBitacora oTarea_BLL = new BLL.TareaBitacora(cDblib);
        BLL.ProyectoEconomico oEstadoPE_BLL = new BLL.ProyectoEconomico(cDblib);
        BLL.AccionT oAccion_BLL = new BLL.AccionT(cDblib);
        try
        {
            string sIdUser = Session["NUM_EMPLEADO_IAP"].ToString();
            //sNodo = SUPER.Capa_Negocio.Estructura.getDefCorta(SUPER.Capa_Negocio.Estructura.sTipoElem.NODO);

            string script1 = "IB.vars.codUsu = '" + sIdUser + "';";
            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
            if (ht["idAccion"].ToString() != "")
            {
                script1 += "IB.vars.idAccion = '" + ht["idAccion"].ToString() + "';";
                if (ht["idAsunto"] != null && ht["idAsunto"].ToString() != "")
                    script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
                else
                {
                    //script1 += "IB.vars.idAsunto = '';";
                    Models.AccionT oAccion = new Models.AccionT();
                    oAccion = oAccion_BLL.Select(int.Parse(ht["idAccion"].ToString()));
                    script1 += "IB.vars.idAsunto = '" + oAccion.T600_idasunto.ToString() + "';";
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

            if (ht["ori"] != null && ht["ori"].ToString() != "")
                script1 += "IB.vars.origen = '" + ht["ori"].ToString() + "';";
            else
                script1 += "IB.vars.origen = '';";


            if (ht["idTarea"] != null && ht["idTarea"].ToString() != "")
            {
                script1 += "IB.vars.idTarea = '" + ht["idTarea"].ToString() + "';";

                // Obtener el nodo de la tarea
                //Models.NodoTarea oNodoTarea = new Models.NodoTarea();
                //oNodoTarea = oNodoTarea_BLL.Select(int.Parse(ht["idTarea"].ToString()));
                //script1 += "IB.vars.idNodo = '" + oNodoTarea.t303_idnodo + "';";

                //Obtengo datos de la tarea
                Models.TareaBitacora oTarea = new Models.TareaBitacora();
                oTarea = oTarea_BLL.Select(int.Parse(ht["idTarea"].ToString()));
                script1 += "IB.vars.idNodo = '" + oTarea.cod_une.ToString() + "';";
                script1 += "IB.vars.nPE = '" + oTarea.cod_pe.ToString() + "';";
                script1 += "IB.vars.estadoProyecto = '" + oTarea.t301_estado + "';";
                script1 += "IB.vars.desPE = '" + oTarea.nom_pe + "';";
                script1 += "IB.vars.nPSN = '" + oTarea.t305_idproyectosubnodo.ToString() + "';";
                script1 += "IB.vars.nPT = '" + oTarea.cod_pt.ToString() + "';";
                script1 += "IB.vars.desPT = '" + oTarea.nom_pt + "';";
                script1 += "IB.vars.desTarea = '" + oTarea.nom_tarea + "';";
                script1 += "IB.vars.fase = '" + oTarea.nom_fase + "';";
                script1 += "IB.vars.actividad = '" + oTarea.nom_actividad + "';";
                sEstadoProy = oTarea.t301_estado;
            }
            else
            {
                script1 += "IB.vars.idTarea = '';";
                script1 += "IB.vars.idNodo = '';";
                if (ht["nPE"].ToString() != "")
                {
                    script1 += "IB.vars.nPE = '" + ht["nPE"].ToString() + "';";
                    // Obtener el estado del proyecto económico
                    Models.ProyectoEconomico oEstadoPE = new Models.ProyectoEconomico();
                    oEstadoPE = oEstadoPE_BLL.Select(int.Parse(ht["nPE"].ToString()));
                    script1 += "IB.vars.estadoProyecto = '" + oEstadoPE.t301_estado + "';";
                    sEstadoProy = oEstadoPE.t301_estado;
                }
                else
                    script1 += "IB.vars.nPE = '';";

                if (ht["desPE"].ToString() != "")
                    script1 += "IB.vars.desPE = '" + ht["desPE"].ToString() + "';";
                else
                    script1 += "IB.vars.desPE = '';";

                if (ht["nPSN"].ToString() != "")
                    script1 += "IB.vars.nPSN = '" + ht["nPSN"].ToString() + "';";
                else
                    script1 += "IB.vars.nPSN = '';";

                if (ht["nPT"].ToString() != "")
                    script1 += "IB.vars.nPT = '" + ht["nPT"].ToString() + "';";
                else
                    script1 += "IB.vars.nPT = '';";

                if (ht["desPT"].ToString() != "")
                    script1 += "IB.vars.desPT = '" + ht["desPT"].ToString() + "';";
                else
                    script1 += "IB.vars.desPT = '';";
                if (ht["desTarea"].ToString() != "")
                    script1 += "IB.vars.desTarea = '" + ht["desTarea"].ToString() + "';";
                else
                    script1 += "IB.vars.desTarea = '';";

                if (ht["fase"].ToString() != "")
                    script1 += "IB.vars.fase = '" + ht["fase"].ToString() + "';";
                else
                    script1 += "IB.vars.fase = '';";

                if (ht["actividad"].ToString() != "")
                    script1 += "IB.vars.actividad = '" + ht["actividad"].ToString() + "';";
                else
                    script1 += "IB.vars.actividad = '';";
            }

            script1 += "IB.vars.IdResponsable = '';";
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
            oTarea_BLL.Dispose();
            oEstadoPE_BLL.Dispose();
            oAccion_BLL.Dispose();
            DBConn.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.AsuntoT obtenerDetalleAsunto(string idAsunto)
    {

        BLL.AsuntoT asuntoTareaBitacoraBLL = new BLL.AsuntoT();
        Models.AsuntoT oAsuntoTareaBitacora;
        try
        {
            oAsuntoTareaBitacora = asuntoTareaBitacoraBLL.Select(Int32.Parse(idAsunto));
            return oAsuntoTareaBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle de la accion (" + idAsunto + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la accion (" + idAsunto + ")." + ex.Message));
        }
        finally
        {
            asuntoTareaBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.AccionT obtenerDetalleAccion(string idAccion)
    {

        BLL.AccionT accionTareaBitacoraBLL = new BLL.AccionT();
        Models.AccionT oAccionTareaBitacora;
        try
        {
            oAccionTareaBitacora = accionTareaBitacoraBLL.Select(Int32.Parse(idAccion));
            return oAccionTareaBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se ha podido obtener el detalle de la acción (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la acción (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            accionTareaBitacoraBLL.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AccionRecursosT> obtenerProfesionalesAccion(string idAccion)
    {
        BLL.AccionRecursosT accionRecursosTareasBitacoraBLL = new BLL.AccionRecursosT();
        List<Models.AccionRecursosT> oListAccionRecursosPEBitacora;
        Models.AccionRecursosT oAccionRecurso = new Models.AccionRecursosT();
        try
        {
            oAccionRecurso.t601_idaccion = Int32.Parse(idAccion);
            oListAccionRecursosPEBitacora = accionRecursosTareasBitacoraBLL.Catalogo(oAccionRecurso);
            return oListAccionRecursosPEBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("No se han podido obtener los profesionales de la acción (" + idAccion + ").", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los profesionales de la accción (" + idAccion + ")." + ex.Message));
        }
        finally
        {
            accionRecursosTareasBitacoraBLL.Dispose();
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
    public static int grabar(Models.AccionT DatosGenerales, List<Models.AccionRecursosT> Integrantes)
    {
        // Grabar datos
        BLL.AccionT AccionBLL = new BLL.AccionT();
        int idReferencia = 0;
        bool bAlta;
        try
        {
            if (DatosGenerales.T601_idaccion == -1) bAlta = true;
            else bAlta = false;
            idReferencia = AccionBLL.grabar(DatosGenerales, Integrantes);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar los datos de la acción. ", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al grabar los datos de la acción. " + ex.Message));
        }
        finally
        {
            AccionBLL.Dispose();
        }
        // Envío de Correos
        BLL.AccionT CorreoAccionBLL = new BLL.AccionT();
        try
        {
            DatosGenerales.T601_idaccion = idReferencia;
            CorreoAccionBLL.EnviarCorreo(DatosGenerales, Integrantes, bAlta);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al enviar correo de acción de Bitácora de Tarea. Acción=" + idReferencia, ex);
            throw new Exception(System.Uri.EscapeDataString("Error al enviar correo de acción de Bitácora de Tarea. Acción=" + idReferencia + " " + ex.Message));
        }
        finally
        {
            CorreoAccionBLL.Dispose();
        }
        return idReferencia;
    }
}
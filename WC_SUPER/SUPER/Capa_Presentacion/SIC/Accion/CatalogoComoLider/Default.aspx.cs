using IB.SUPER.Shared;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_SIC_Accion_CatalogoComoLider_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int ta204_idaccionpreventa = 0;
            IB.SUPER.Shared.HistorialNavegacion.Resetear();
            IB.SUPER.Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);

            Hashtable ht = IB.SUPER.Shared.Utils.ParseQuerystring(Request.QueryString.ToString());

            if (ht["idAccion"] != null)
                ta204_idaccionpreventa = int.Parse(ht["idAccion"].ToString());

            string script1 = "IB.vars.ta204_idaccionpreventa = '" + ta204_idaccionpreventa.ToString() + "';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

            if (ht["filters"] != null)
            {
                string script0 = "IB.vars.filters = " + JsonConvert.SerializeObject(IB.SUPER.Shared.Utils.ConvertQSFiltersToJson(ht["filters"].ToString())) + ";";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script0", script0, true);
            }


        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al cargar la pantalla de mis acciones como líder", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la pantalla.';", true);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CatalogoMisAcciones()
    {

        IB.SUPER.SIC.BLL.AccionPreventa cAP = new IB.SUPER.SIC.BLL.AccionPreventa();

        try
        {
            return JsonConvert.SerializeObject(cAP.CatalogoMisAccionescomoLider());
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al cargar la pantalla de mis acciones como líder", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los datos."));
        }
        finally
        {
            cAP.Dispose();
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string obtenerTareasbyAccion(int ta204_idaccionpreventa)
    {

        IB.SUPER.SIC.BLL.TareaPreventa cTP = new IB.SUPER.SIC.BLL.TareaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cTP.CatalogoPorAccion(ta204_idaccionpreventa));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar las tareas de la acción", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error cargando los datos."));
        }
        finally
        {
            cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string misTareasComoParticipante()
    {

        IB.SUPER.SIC.BLL.TareaPreventa cTP = new IB.SUPER.SIC.BLL.TareaPreventa();
        
        try
        {
            return JsonConvert.SerializeObject(cTP.misTareasComoParticipante());
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al cargar las tareas del participante", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error cargando los datos."));
        }
        finally
        {
            cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void quitarNegritasTareasPendientesAccion(int idaccion)
    {

        IB.SUPER.SIC.BLL.TareaPendientePreventa cTP = new IB.SUPER.SIC.BLL.TareaPendientePreventa();
        List<int> tablaconceptos = new List<int>();
        tablaconceptos.Add(1);

        try
        {
            cTP.quitarNegritaTareaPendiente((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], tablaconceptos, idaccion, null);
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al quitar las negritas de las tareas pendientes.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al quitar las negritas de las tareas pendientes."));
        }
        finally
        {
            cTP.Dispose();
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void quitarNegritasTareasPendientesParticipante(int idtarea)
    {

        IB.SUPER.SIC.BLL.TareaPendientePreventa cTP = new IB.SUPER.SIC.BLL.TareaPendientePreventa();
        List<int> tablaconceptos = new List<int>();
        tablaconceptos.Add(4);
        try
        {
            cTP.quitarNegritaTareaPendiente((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], tablaconceptos, null, idtarea);
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al quitar las negritas de las tareas pendientes.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al quitar las negritas de las tareas pendientes."));
        }
        finally
        {
            cTP.Dispose();
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Shared = IB.SUPER.Shared;
using System.Collections;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Capa_Presentacion_SIC_Tarea_MisParticipacionesHistorico_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);
            string origenmenu = String.Empty;
            string script1 = "";
            Hashtable ht = Shared.Utils.ParseQuerystring(Request.QueryString.ToString());

            if (ht["filters"] != null)
            {
                string script0 = "IB.vars.filters = " + JsonConvert.SerializeObject(Shared.Utils.ConvertQSFiltersToJson(ht["filters"].ToString())) + ";";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script0", script0, true);
            }

            if (ht["origenmenu"] != null) 
                script1 = "IB.vars.origenMenu = '" + ht["origenmenu"].ToString() + "'";            
            else script1 = "IB.vars.origenMenu = '" + origenmenu + "'"; 
            
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
            
            


        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar la pantalla de mis participaciones (histórico)", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la pantalla.';", true);
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Catalogo(Models.TareaCatHistoricoFilter filter)
    {
        if (filter == null) return JsonConvert.SerializeObject("");

        BLL.ParticipanteTareaPreventa cAP = new BLL.ParticipanteTareaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cAP.CatalogoHistoricoParticipante(filter));

        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar el catálogo de acciones", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los datos."));
        }
        finally
        {
            cAP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void quitarNegritasTareasPendientes(int idtarea)
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
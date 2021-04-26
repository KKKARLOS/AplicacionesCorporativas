using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text;
using Newtonsoft.Json;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Shared = IB.SUPER.Shared;
using System.Collections;
using System.Web.Script.Serialization;

public partial class Capa_Presentacion_SIC_Accion_AmbitoCRM_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            Shared.HistorialNavegacion.Resetear();
            Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);

            Hashtable ht = Shared.Utils.ParseQuerystring(Request.QueryString.ToString());

            if (ht["filters"] != null)
            {
                string script0 = "IB.vars.filters = " + JsonConvert.SerializeObject(Shared.Utils.ConvertQSFiltersToJson(ht["filters"].ToString())) + ";";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script0", script0, true);
            }


        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar la pantalla de catalogo de acciones para figuras de area y subarea pendientes de asignar lider", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la pantalla.';", true);
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Catalogo(Models.AccionCatAmbitoCRMFilter filter)
    {
        if (filter == null) return JsonConvert.SerializeObject("");

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();

        try
        {
            return JsonConvert.SerializeObject(cAP.CatalogoAmbitoCRM(filter));

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
}
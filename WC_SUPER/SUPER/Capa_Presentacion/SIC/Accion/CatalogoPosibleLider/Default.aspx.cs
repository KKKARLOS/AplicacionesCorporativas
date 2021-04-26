using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Shared = IB.SUPER.Shared;
using System.Collections;

public partial class Capa_Presentacion_SIC_Accion_CatalogoPosibleLider_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {            
            Hashtable ht = Shared.Utils.ParseQuerystring(Request.QueryString.ToString());
            int ta204_idaccionpreventa = 0;

            if (ht["idAccion"] != null && ht["AL"] != null)
                ta204_idaccionpreventa = int.Parse(ht["idAccion"].ToString());
            else
            {
                Shared.HistorialNavegacion.Resetear();
                Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);
            }

            string script1 = "IB.vars.ta204_idaccionpreventa = '" + ta204_idaccionpreventa.ToString() + "';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar la pantalla de acciones de autoasignación de lider", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la pantalla.';", true);
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Catalogo()
    {

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();

        try
        {
            return JsonConvert.SerializeObject(cAP.CatalogoPosibleLider());
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar el catálogo de acciones de autoasignación de lider", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los datos."));
        }
        finally
        {
            cAP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void quitarNegritasTareasPendientes(int idaccion)
    {

        BLL.TareaPendientePreventa cTP = new BLL.TareaPendientePreventa();
        List<int> tablaconceptos = new List<int>();
        tablaconceptos.Add(3);
        try
        {
            cTP.quitarNegritaTareaPendiente((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], tablaconceptos, idaccion, null);
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al quitar las negritas de las tareas pendientes.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al quitar las negritas de las tareas pendientes."));
        }
        finally
        {
            cTP.Dispose();
        }

    }

}
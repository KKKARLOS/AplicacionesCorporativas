using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Shared = IB.SUPER.Shared;
using System.Collections;
using IB.SUPER.Shared;

public partial class Capa_Presentacion_SIC_Participante_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
        int ta207_idtareapreventa = 0;

        if (ht["idTarea"] != null && ht["TP"] != null)
            ta207_idtareapreventa = int.Parse(ht["idTarea"].ToString());
        else {
            //Historial de navegacion
            Shared.HistorialNavegacion.Resetear();
            Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);
        }

        string script1 = "IB.vars.ta207_idtareapreventa = '" + ta207_idtareapreventa.ToString() + "';";
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Catalogo()
    {

        BLL.TareaPreventa cTP = new BLL.TareaPreventa();

        try
        {            
            return JsonConvert.SerializeObject(cTP.misTareasComoParticipante());            
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar las tareas del participante", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error cargando los datos."));
        }
        finally
        {
            cTP.Dispose();
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
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

public partial class Capa_Presentacion_SIC_Solicitud_CatalogoSUPER_Default : System.Web.UI.Page
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

            
            string script1 = "IB.vars.origenMenu = '" + ht["origenmenu"].ToString() + "'";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
            
            
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar la pantalla de catalogo de solicitudes desde SUPER", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la pantalla.';", true);
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Catalogo(string origenMenu, Models.SolicCatSuperRF filter)
    {
        if (filter == null) return JsonConvert.SerializeObject("");

        BLL.SolicitudPreventa cSP = new BLL.SolicitudPreventa();

        try
        {
            return JsonConvert.SerializeObject(cSP.CatalogoSolicitudesSUPER(origenMenu, filter));

        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar el catálogo de solicitudes", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los datos."));
        }
        finally
        {
            cSP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int AltaSolicitud(string denominacion, int area)
    {

        BLL.SolicitudPreventa cSP = new BLL.SolicitudPreventa();
        Models.SolicitudPreventa o = new Models.SolicitudPreventa();

        try
        {
            o.ta206_denominacion = denominacion;
            o.ta206_itemorigen = "S";
            o.ta200_idareapreventa = area;

            return cSP.Insert(o);
            
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al grabar la solicitud preventa SUPER", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al grabar la solicitud."));
        }
        finally
        {
            cSP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void EdicionSolicitud(int ta206_idsolicitudpreventa, string denominacion, string ta206_estado, string motivoAnulacion)
    {

        BLL.SolicitudPreventa cSP = new BLL.SolicitudPreventa();

        try
        {
            cSP.UpdateDenominacion(ta206_idsolicitudpreventa, denominacion, ta206_estado, motivoAnulacion);
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al actualizar la denominación de la solicitud preventa SUPER", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al actualizar la denominación de la solicitud."));
        }
        finally
        {
            cSP.Dispose();
        }

    }
    /// <summary>
    /// Elimina la solicitud
    /// </summary>
    /// <param name="ta206_idsolicitudpreventa"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void EliminarSolicitud(int ta206_idsolicitudpreventa)
    {

        BLL.SolicitudPreventa cSP = new BLL.SolicitudPreventa();

        try
        {
            cSP.EliminarSolicitud(ta206_idsolicitudpreventa);
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al eliminar la solicitud preventa SUPER", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al eliminar la solicitud preventa SUPER."));
        }
        finally
        {
            cSP.Dispose();
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CatalogoAccionesBySolicitud(int ta206_idsolicitudpreventa)
    {
        if (ta206_idsolicitudpreventa == -1) return JsonConvert.SerializeObject("");

        BLL.AccionPreventa cAcc = new BLL.AccionPreventa();

        try
        {
            return JsonConvert.SerializeObject(cAcc.CatalogoAccionesBySolicitud(ta206_idsolicitudpreventa));

        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar el catálogo de solicitudes", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los datos."));
        }
        finally
        {
            cAcc.Dispose();
        }

    }

}
using IB.SUPER.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_SIC_Mantenimientos_area_subarea_Masivo_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getareassubareasppl()
    {
        IB.SUPER.SIC.BLL.AreaPreventa cTP = new IB.SUPER.SIC.BLL.AreaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cTP.getareassubareasppl(int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString())));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener los áreas-subáreas", ex);
            throw new Exception("Error al obtener los áreas-subáreas");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getpplporsubareaparaficepi()
    {
        IB.SUPER.SIC.BLL.AreaPreventa cTP = new IB.SUPER.SIC.BLL.AreaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cTP.getpplporsubareaparaficepi(int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString())));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener los áreas-subáreas", ex);
            throw new Exception("Error al obtener los áreas-subáreas");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void grabar(List<IB.SUPER.SIC.Models.SubareaPreventa> lstSubareas)
    {
        IB.SUPER.SIC.BLL.SubareaPreventa cAP = new IB.SUPER.SIC.BLL.SubareaPreventa();

        try
        {
            cAP.grabar(lstSubareas);

        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar el pool de posibles líderes de un subárea ", ex);
            throw ex;
        }
        finally
        {
            if (cAP != null) cAP.Dispose();
        }

    }

}
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
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;


public partial class Capa_Presentacion_SIC_Mantenimientos_area_subarea_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Hashtable ht = IB.SUPER.Shared.Utils.ParseQuerystring(Request.QueryString.ToString());

        string script1 = "IB.vars.origenMenu = '" + ht["origenmenu"].ToString() + "'";
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);        
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getAreasByFicepi(string origenMenu)
    {
       IB.SUPER.SIC.BLL.AreaPreventa cTP = new IB.SUPER.SIC.BLL.AreaPreventa();
        bool actuocomoadministrador = false;

        if (origenMenu == "ADM") actuocomoadministrador = true;
        
        try
        {
            return JsonConvert.SerializeObject(cTP.getAreasByFicepi(int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()), actuocomoadministrador));            
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener los áreas", ex);
            throw new Exception("Error al obtener los áreas");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getSubAreasByFicepi(int ta200_idareapreventa, string origenMenu)
    {
        IB.SUPER.SIC.BLL.SubareaPreventa cTP = new IB.SUPER.SIC.BLL.SubareaPreventa();

        bool actuocomoadministrador = false;

        if (origenMenu == "ADM") actuocomoadministrador = true;

        try
        {
            return JsonConvert.SerializeObject(cTP.getSubAreasByFicepi(ta200_idareapreventa, int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()), actuocomoadministrador));            
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener los áreas", ex);
            throw new Exception("Error al obtener los áreas");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getAreaSel(int ta200_idareapreventa)
    {
        IB.SUPER.SIC.BLL.AreaPreventa cTP = new IB.SUPER.SIC.BLL.AreaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cTP.Select(ta200_idareapreventa));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener el detalle del área", ex);
            throw new Exception("Error al obtener el detalle del área");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getSubAreaSel(int ta201_idsubareapreventa)
    {
        IB.SUPER.SIC.BLL.SubareaPreventa cTP = new IB.SUPER.SIC.BLL.SubareaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cTP.getSubAreaSel(ta201_idsubareapreventa));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener el detalle del subárea", ex);
            throw new Exception("Error al obtener el detalle del subárea");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getFiguras_Area(int ta200_idareapreventa)
    {
        IB.SUPER.SIC.BLL.AreaPreventa cTP = new IB.SUPER.SIC.BLL.AreaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cTP.getFiguras_Area(ta200_idareapreventa));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener las figuras del área", ex);
            throw new Exception("Error al obtener las figuras del área");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getFiguras_SubArea(int ta201_idsubareapreventa)
    {
        IB.SUPER.SIC.BLL.SubareaPreventa cTP = new IB.SUPER.SIC.BLL.SubareaPreventa();

        try
        {
            return JsonConvert.SerializeObject(cTP.getFiguras_SubArea(ta201_idsubareapreventa));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener las figuras del Subarea", ex);
            throw new Exception("Error al obtener las figuras del Subarea");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int grabarArea(Models.AreaPreventa oArea, List<Models.FiguraAreaPreventa> lstFigurasArea)
    {
        BLL.AreaPreventa cAP = new BLL.AreaPreventa();

        try
        {
            return cAP.grabarArea(oArea, lstFigurasArea);
            
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar el área", ex);
            throw ex;
        }
        finally
        {
            if (cAP != null) cAP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int grabarSubArea(Models.SubareaPreventa oSubArea, List<Models.FiguraSubareaPreventa> lstFigurasSubArea)
    {
        BLL.SubareaPreventa cSAP = new BLL.SubareaPreventa();

        try
        {
            return cSAP.grabarSubArea(oSubArea, lstFigurasSubArea);            

        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar el Subarea", ex);
            throw ex;
        }
        finally
        {
            if (cSAP != null) cSAP.Dispose();
        }

    }



}
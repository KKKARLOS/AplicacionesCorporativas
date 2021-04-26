using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.APP.BLL;
using Models = IB.SUPER.APP.Models;
using Shared = IB.SUPER.Shared;
using Newtonsoft.Json;

public partial class Capa_Presentacion_Administracion_AlertasPrioridad_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        /*
        string script1 = "";
        Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
        try
        {
            script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
        }
        catch { script1 += "IB.vars.idAsunto = '';"; }

        script1 += "IB.vars.qs = '" + Utils.decodpar(Request.QueryString.ToString()) + "';";
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        */
        string script0 = "IB.vars.lstAlertas = " + JsonConvert.SerializeObject(getListaAlertas()) + ";";
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script0", script0, true);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.PRIOALERTAS> getCatalogo()
    {
        BLL.PRIOALERTAS bPrioridad = new BLL.PRIOALERTAS();
        try
        {
            List<Models.PRIOALERTAS> lMotivos = null;

            lMotivos = bPrioridad.Catalogo();

            bPrioridad.Dispose();
            return lMotivos;
        }
        catch (Exception ex)
        {
            if (bPrioridad != null) bPrioridad.Dispose();
            throw ex;
        }
        finally
        {
            bPrioridad.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int Alta(Models.PRIOALERTAS mPrioridad)
    {

        BLL.PRIOALERTAS cSP = new BLL.PRIOALERTAS();
        Models.PRIOALERTAS o = new Models.PRIOALERTAS();

        try
        {
            o.t820_idalerta_1 = mPrioridad.t820_idalerta_1;
            o.t820_idalerta_2 = mPrioridad.t820_idalerta_2;
            o.t820_idalerta_g = mPrioridad.t820_idalerta_g;

            return cSP.Insert(o);

        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al grabar la prioridad de alerta", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al grabar."));
        }
        finally
        {
            cSP.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void Edicion(Models.PRIOALERTAS mPrioridad)
    {
        BLL.PRIOALERTAS cSP = new BLL.PRIOALERTAS();
        Models.PRIOALERTAS o = new Models.PRIOALERTAS();
        try
        {
            o.t820_idalerta_1 = mPrioridad.t820_idalerta_1;
            o.t820_idalerta_2 = mPrioridad.t820_idalerta_2;
            o.t820_idalerta_g = mPrioridad.t820_idalerta_g;

            cSP.Update(o);
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al actualizar la prioridad de alerta", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al actualizar la prioridad de alerta."));
        }
        finally
        {
            cSP.Dispose();
        }
    }

    public static List<Models.ALERTA> getListaAlertas()
    {
        BLL.ALERTA bPrioridad = new BLL.ALERTA();
        try
        {
            List<Models.ALERTA> lMotivos = null;

            lMotivos = bPrioridad.Lista();

            bPrioridad.Dispose();
            return lMotivos;
        }
        catch (Exception ex)
        {
            if (bPrioridad != null) bPrioridad.Dispose();
            throw ex;
        }
        finally
        {
            bPrioridad.Dispose();
        }
    }

}
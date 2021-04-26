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
public partial class Capa_Presentacion_Administracion_MantMotivoOCyFA_Default : System.Web.UI.Page
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
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.MOTIVOOCFA> getCatalogo()
    {
        BLL.MOTIVOOCFA bMotivo = new BLL.MOTIVOOCFA();
        try
        {
            List<Models.MOTIVOOCFA> lMotivos = null;

            Models.MOTIVOOCFA oMOTIVOOCFAFilter = new Models.MOTIVOOCFA();
            lMotivos = bMotivo.Catalogo(oMOTIVOOCFAFilter);

            bMotivo.Dispose();
            return lMotivos;
        }
        catch (Exception ex)
        {
            if (bMotivo != null) bMotivo.Dispose();
            throw ex;
        }
        finally
        {
            bMotivo.Dispose();
        }
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrar(List<Models.MOTIVOOCFA> lineas)
    {
        BLL.MOTIVOOCFA motivo = new BLL.MOTIVOOCFA();
        try
        {
            motivo.BorrarMotivos(lineas);
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al eliminar motivo", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al eliminar motivo " + ex.Message));

        }
        finally
        {
            motivo.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int Alta(string denominacion, string tipo)
    {

        BLL.MOTIVOOCFA cSP = new BLL.MOTIVOOCFA();
        Models.MOTIVOOCFA o = new Models.MOTIVOOCFA();

        try
        {
            o.t840_descripcion = denominacion;
            o.t820_tipo = tipo;

            return cSP.Insert(o);

        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al grabar el motivo OC y FA", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al grabar."));
        }
        finally
        {
            cSP.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void Edicion(int t840_idmotivo, string denominacion, string tipo)
    {

        BLL.MOTIVOOCFA cSP = new BLL.MOTIVOOCFA();
        Models.MOTIVOOCFA o = new Models.MOTIVOOCFA();
        try
        {
            o.t840_descripcion = denominacion;
            o.t820_tipo = tipo;
            o.t840_idmotivo = t840_idmotivo;

            cSP.Update(o);
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al actualizar la denominación del motivo OC y FA", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al actualizar la denominación del motivo OC y FA."));
        }
        finally
        {
            cSP.Dispose();
        }
    }

}
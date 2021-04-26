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

public partial class Capa_Presentacion_SIC_Accion_CatalogoCRM_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            //params itemorigen=S&iditemorigen=19
            //qs --> aXRlbW9yaWdlbj1TJmlkaXRlbW9yaWdlbj0xOQ==

            //params itemorigen=O&iditemorigen=1
            //qs --> aXRlbW9yaWdlbj1PJmlkaXRlbW9yaWdlbj0x

            //Historial de navegacion
            Shared.HistorialNavegacion.Resetear();
            Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);

            Hashtable ht = Shared.Utils.ParseQuerystring(Request.QueryString.ToString());

            string itemorigen = ht["itemorigen"].ToString().Trim().ToUpper();
            string iditemorigen = ht["iditemorigen"].ToString().Trim().ToUpper();

            //validaciones
            if (itemorigen != "O" && itemorigen != "P" && itemorigen != "E")
                throw new Exception("Error de parámetros. Origen no permitido.");

            string script1 = "IB.vars.ta206_iditemorigen = " + iditemorigen + ";";
            script1 += "IB.vars.ta206_itemorigen = '" + itemorigen + "';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

            //Información de cabecera. Solo el ID, el resto por ajax en el init del js.
            string lbl = "";
            switch (itemorigen)
            {
                case "O":
                    lbl = "Oportunidad"; break;
                case "E":
                    lbl = "Extensión";
                    divOportExt.Attributes.Add("class", "form-group show");                    
                    break;
                case "P":
                    lbl = "Objetivo";                    
                    break;
                case "S": lbl = "Solicitud";
                    div_txtCuenta_cab.Visible = false;
                    break;
            }
            this.lblItemorigen_cab.InnerText = lbl;
            this.txtIditemorigen_cab.Value = iditemorigen;
            
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar el catálogo de acción preventa del CRM", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la pantalla.';", true);
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string obtenerAccionesCRM(string ta206_itemorigen, int ta206_iditemorigen)
    {

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();
        Models.AccionCatRequestFilter rf = new Models.AccionCatRequestFilter();

        rf.ta206_itemorigen = ta206_itemorigen;
        rf.ta206_iditemorigen = ta206_iditemorigen;

        try
        {
            return JsonConvert.SerializeObject(cAP.Catalogo(rf));
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar la acción preventa", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los datos de la acción preventa."));
        }
        finally
        {
            cAP.Dispose();
        }

    }
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string imputaciones(string ta206_itemorigen, int ta206_iditemorigen)
    {

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();
        
        try
        {
            return JsonConvert.SerializeObject(cAP.CatalogoImputaciones(ta206_itemorigen, ta206_iditemorigen));
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar la modal de imputaciones realizadas", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener los datos de la acción preventa."));
        }
        finally
        {
            cAP.Dispose();
        }

    }

    
}
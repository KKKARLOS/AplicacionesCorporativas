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

public partial class Capa_Presentacion_SIC_Exportaciones_Acciones_Default : System.Web.UI.Page
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

            string script1 = "IB.vars.origenMenu = '" + ht["origenmenu"].ToString() + "'";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al cargar la pantalla de exportación de acciones", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la pantalla.';", true);
        }

    }
}
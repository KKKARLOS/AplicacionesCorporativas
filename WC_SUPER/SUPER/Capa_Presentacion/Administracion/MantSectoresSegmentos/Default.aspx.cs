using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using SUPER.Capa_Negocio;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text.RegularExpressions;


public partial class MantSectoresSegmentos : System.Web.UI.Page
{
    public string sTreeView = "";
    public string bOnline = "true";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (bOnline != "true")
            {
                Master.sbotonesOpcionOn = "4";
                Master.sbotonesOpcionOff = "4";
            }

            Master.TituloPagina = "Mantenimiento de Sectores y Segmentos";

            //Master.FuncionesJavaScript.Add("Javascript/jquery-ui.min.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.dnd.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.edit.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.menu.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.gridnav.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.table.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.ui-contextmenu.js");
            Master.FicherosCSS.Add("App_Themes/Corporativo/FancyTree/jquery-ui.css");
            Master.FicherosCSS.Add("App_Themes/Corporativo/FancyTree/ui.fancytree.css");

            Master.bFuncionesLocales = true;

            sTreeView = Arbol();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
     
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GrabarAjax(string sDelete, string sInsert, string sUpdate)
    {
        try
        {
            return "OK@#@" + SECTOR.Grabar(sDelete, sInsert, sUpdate);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al grabar los Sectores/Segmentos.", ex);
        }
    }

    private static string Arbol()
    {
        string sArbol = SECTOR.Arbol();
        return sArbol;
    }
}

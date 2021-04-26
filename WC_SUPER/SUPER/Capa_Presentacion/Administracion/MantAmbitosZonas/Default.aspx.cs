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


public partial class MantAmbitosZonas : System.Web.UI.Page 
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

                Master.TituloPagina = "Mantenimiento de Ámbitos y Zonas";

                //Master.FuncionesJavaScript.Add("Javascript/jquery-ui.min.js");
                //Master.FuncionesJavaScript.Add("Javascript/jquery-1.12.3.min.js");
                //Master.FuncionesJavaScript.Add("Javascript/jquery-ui-1-11-4.js");  
                         
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
            return "OK@#@" + AMBITO.Grabar(sDelete, sInsert, sUpdate);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al grabar los Ámbitos/Zonas.", ex);
        }
        //catch (System.Data.SqlClient.SqlException sqlex)
        //{
        //    if (SUPER.BLL.Log.logger.IsDebugEnabled) SUPER.BLL.Log.logger.Debug("Grabación erronea-1.");

        //    if (sqlex.Number == 547)
        //    {
        //        if (SUPER.BLL.Log.logger.IsDebugEnabled) SUPER.BLL.Log.logger.Debug("Número de error: " + sqlex.Number.ToString());
        //        return "547"; //error de integridad referencial. No se puede eliminar la estruc.eco. o organizativa si hay datos económicos para la visión.
        //    }
        //    throw sqlex; //si no es 547 throw ex
        //}
        //catch (Exception ex)
        //{
        //    if (SUPER.BLL.Log.logger.IsDebugEnabled) SUPER.BLL.Log.logger.Debug("Grabación erronea-2.");
        //    throw ex;
        //}
    }
    private static string Arbol()
    {
        string sArbol = AMBITO.Arbol();
        return sArbol;
    }
}

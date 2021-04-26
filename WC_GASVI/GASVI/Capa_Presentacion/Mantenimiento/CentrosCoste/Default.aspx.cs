using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using GASVI.BLL;
using System.Text.RegularExpressions;
using EO.Web;

public partial class Capa_Presentacion_Mantenimiento_CentrosCoste_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.bFuncionesLocales = true;
        Master.nBotonera = 21;
        Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
        
        Master.FicherosCSS.Add("Capa_Presentacion/Mantenimiento/CentrosCoste/css/jquery-ui.css");
        //Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1/jquery-1.7.1.min.js");
        //Master.FuncionesJavaScript.Add("Javascript/Treeview/jquery-ui.min.js");
        Master.FicherosCSS.Add("Capa_Presentacion/Mantenimiento/CentrosCoste/css/ui.fancytree.css");
        Master.FuncionesJavaScript.Add("Javascript/Treeview/jquery.fancytree.js");
        Master.FuncionesJavaScript.Add("Javascript/Treeview/jquery.fancytree.dnd.js");
        Master.FuncionesJavaScript.Add("Javascript/Treeview/jquery.fancytree.table.js");
        Master.FuncionesJavaScript.Add("Javascript/Treeview/jquery.ui-contextmenu.js");
        Master.FicherosCSS.Add("Capa_Presentacion/Mantenimiento/CentrosCoste/css/estilos.css");
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CargaInicialEstructura(int inactivos)
    {
        try
        {
            return Estructura.getEstructuraCenCos(inactivos);
            
        }
        catch (Exception ex)
        {
            throw new Exception(Utilidades.escape(Errores.mostrarError("Error en la carga inicial de la estructura", ex)));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CargaInicialCentrosCoste()
    {
        try
        {
            return CentrosCoste.CatalogoCenCosEstructura();
        }
        catch (Exception ex)
        {
            throw new Exception(Utilidades.escape(Errores.mostrarError("Error en la carga inicial del Centro de coste", ex)));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GrabarCCCheck(string id, short ischecked)
    {
        try
        {
            CentrosCoste.GrabarEstadoGasvi(Regex.Split(id, "@#sep#@")[1], ischecked);
        }
        catch (Exception ex)
        {
            throw new Exception(Utilidades.escape(Errores.mostrarError("Error en al grabar el CCCheck", ex)));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GrabarVinEstCC(string nodeKey, string idCC)
    {
        try
        {
            CentrosCoste.GrabarEstrucCenCos(nodeKey, idCC);
        }
        catch (Exception ex)
        {
            throw new Exception(Utilidades.escape(Errores.mostrarError("Error al grabar VinEstCC", ex)));
        }
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }  
}

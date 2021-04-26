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

public partial class TreeViewFancy : System.Web.UI.Page
{
    public string sTreeView = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.TituloPagina = "Prueba de FancyTreeView";
                Master.FuncionesJavaScript.Add("Javascript/jquery.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery-ui.custom.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.dnd.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.edit.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.menu.js");

                
                //Master.FicherosCSS.Add("App_Themes/src/skin-win7/ui.fancytree.css");
                Master.FicherosCSS.Add("Capa_Presentacion/Pruebas/TreeView-Fancy/css/ui.fancytree.css");

                Master.FicherosCSS.Add("Capa_Presentacion/Pruebas/TreeView-Fancy/jquery.contextMenu.css");
                Master.bFuncionesLocales = true;


                sTreeView = GenerarArbol();

            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    private string GenerarArbol()
    {
        SqlDataReader dr = SUPER.DAL.SECTOR.Arbol();
        StringBuilder sb = new StringBuilder();

        string sIdSector = "";
        string sIdSegmento = "";
        int indice1 = 0;

        while (dr.Read())
        {
            if (sIdSector != dr["identificador1"].ToString())
            {
                if (indice1 == 1)
                {
                    sb.Append("</ul>");
                    indice1 = 0;
                }
                sb.Append("<li id='" + dr["identificador1"].ToString() + "' >" + dr["denominacion1"].ToString());
                sIdSector = dr["identificador1"].ToString();
                if (indice1 == 0)
                {
                    sb.Append("<ul>");
                    indice1 = 1;
                }
            }
            if (sIdSegmento != dr["identificador2"].ToString())
            {
                sb.Append("<li id='" + dr["identificador2"].ToString() + "' >" + dr["denominacion2"].ToString());
                sIdSegmento = dr["identificador2"].ToString();
            }
        }
        dr.Close();
        dr.Dispose();
        return sb.ToString();
    }
}

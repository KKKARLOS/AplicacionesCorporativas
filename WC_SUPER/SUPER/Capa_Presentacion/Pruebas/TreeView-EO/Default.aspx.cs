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

public partial class Capa_Presentacion_Pruebas_ArbolTabla_Default : System.Web.UI.Page
{
    private EO.Web.TreeNode newNode;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!CallbackPanel.IsCallbackByMe)
            {
                //Master.sbotonesOpcionOn = "2,7,4";
                //Master.sbotonesOpcionOff = "4";

                Master.TituloPagina = "Prueba de TreeView";
                Master.bFuncionesLocales = true;

                Arbol.Nodes.Clear();
                GenerarArbol();

                /*
                EO.Web.TreeNode newNode = new EO.Web.TreeNode("Finanzas");

                Arbol.Nodes.Add(newNode);

                newNode = new EO.Web.TreeNode("Bancos");
                Arbol.Nodes[0].ChildNodes.Add(newNode);

                newNode = new EO.Web.TreeNode("Cajas de Ahorros");
                Arbol.Nodes[0].ChildNodes.Add(newNode);

                newNode = new EO.Web.TreeNode("Seguros");
                Arbol.Nodes.Add(newNode);

                newNode = new EO.Web.TreeNode("Admón Pública");
                Arbol.Nodes.Add(newNode);

                newNode = new EO.Web.TreeNode("Industria");
                Arbol.Nodes.Add(newNode);

                newNode = new EO.Web.TreeNode("Sanidad");
                Arbol.Nodes.Add(newNode);

                newNode = new EO.Web.TreeNode("Servicios");
                Arbol.Nodes.Add(newNode);
                */

                //GenerarArbol();
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
    }
    protected void btnNuevo_Click(object sender, System.EventArgs e)
    {
        newNode = new EO.Web.TreeNode("Nuevo");
        newNode.ItemID = "9999";
        Arbol.SelectedNode.ChildNodes.Add(newNode);
        Arbol.SearchItems("9999");
    }

    protected void btnEliminar_Click(object sender, System.EventArgs e)
    {
        Arbol.Nodes.Remove(Arbol.SelectedNode);
    }
   
    private void GenerarArbol()
    {
        SqlDataReader dr = SUPER.DAL.SECTOR.Arbol();

        string sIdSector = "";
        string sIdSegmento = "";
        int indice = 0;

        while (dr.Read())
        {
            if (sIdSector != dr["identificador1"].ToString())
            {
                newNode = new EO.Web.TreeNode(dr["denominacion1"].ToString());
                newNode.ItemID = dr["identificador1"].ToString();
                Arbol.Nodes.Add(newNode);
                sIdSector = dr["identificador1"].ToString();
                indice++;
            }
            if (sIdSegmento != dr["identificador2"].ToString())
            {
                newNode = new EO.Web.TreeNode(dr["denominacion2"].ToString());
                newNode.ItemID = dr["identificador2"].ToString();
                Arbol.Nodes[indice - 1].ChildNodes.Add(newNode);
                sIdSegmento = dr["identificador2"].ToString();
            }
        }
        dr.Close();
        dr.Dispose();
    }

    protected void CallbackPanel_Execute(object sender, EO.Web.CallbackEventArgs e)
    {
        //Encontrar el nodo que ha hecho click
        EO.Web.TreeNode node = (EO.Web.TreeNode)Arbol.FindItem(e.Parameter);

    } 
}

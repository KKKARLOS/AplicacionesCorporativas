using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_UserControls_Menu : System.Web.UI.UserControl
{
    protected string strUrl;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsCallback)
        {
            this.strUrl = Session["strServer"].ToString();
            this.Menu1.DynamicPopOutImageUrl = strUrl + "App_Themes/Corporativo/Images/imgFleMenuDr.gif";
            this.Menu1.ScrollDownImageUrl = strUrl + "App_Themes/Corporativo/Images/imgFleMenuDown.gif";
            this.Menu1.ScrollUpImageUrl = strUrl + "App_Themes/Corporativo/Images/imgFleMenuUp.gif";
            this.Menu1.StaticPopOutImageUrl = strUrl + "App_Themes/Corporativo/Images/imgSeparador.gif";
        }
    }

    protected void Menu1_MenuItemDataBound(object sender, MenuEventArgs e)
    {
        if (!Page.IsCallback)
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            bool bEsMiembro = false;

            foreach (string RolMenu in mapNode.Roles)
            {

                //if (
                //    (mapNode.Title == "PGE" && Session["BloquearPGEByAcciones"] != null && (bool)Session["BloquearPGEByAcciones"] == true)
                //    || (mapNode.Title == "PST" && Session["BloquearPSTByAcciones"] != null && (bool)Session["BloquearPSTByAcciones"] == true)
                //    || (mapNode.Title == "IAP" && Session["BloquearIAPByAcciones"] != null && (bool)Session["BloquearIAPByAcciones"] == true)
                //    )//Si el acceso está bloqueado por alguna acción pendiente, no se muestran aunque el usuario sea administrador.
                //{
                //    bEsMiembro = false;
                //    break;
                //}


                if (
                    (mapNode.Title == "PGE" && !Utilidades.EsModuloAccesible("PGE"))
                    || (mapNode.Title == "PST" && !Utilidades.EsModuloAccesible("PST"))
                    || (mapNode.Title == "IAP" && !Utilidades.EsModuloAccesible("IAP"))
                    )//Si el acceso a los módulos está cortado, no se muestran...
                {
                    //if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "A")
                    if (!SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {// a menos que el usuario sea administrador.
                        bEsMiembro = false;
                        break;
                    }
                }

                if (mapNode.Title == "ADP")
                {
                    if (Utilidades.EsModuloAccesible("ADP") && HttpContext.Current.User.IsInRole("PER"))
                    {
                        bEsMiembro = true;
                        break;
                    }
                }

                //if (mapNode.ParentNode.Title == "Correos infomativos")
                //{
                //    if (mapNode.Title == "Activar" && (bool)HttpContext.Current.Session["RECIBIRMAILS"]) mapNode.Title = "Activar (x)";
                //    if (mapNode.Title == "Desactivar" && !(bool)HttpContext.Current.Session["RECIBIRMAILS"]) mapNode.Title = "Desactivar (√)";
                //}
                if (RolMenu == "*")
                {
                    //if (mapNode.Title == "Reconexión")
                    //    bEsMiembro = false;
                    //else
                        bEsMiembro = true;
                    //break;
                }
                string RolMenuAux = RolMenu.Trim();
                if (RolMenuAux != "")
                {
                    foreach (string MiRol in ((RolePrincipal)Page.User).GetRoles())
                    {
                        if (RolMenu == MiRol)
                        {
                            bEsMiembro = true;
                            break;
                        }
                    }
                }
                if (bEsMiembro) break;
            }

            if (!bEsMiembro)
            {
                //System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);
                if (e.Item.Depth == 0)
                {
                    menu.Items.Remove(e.Item);
                    //itemToRemove.Enabled = false;
                }
                else
                {
                    System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                    if (parent != null)
                    {
                        parent.ChildItems.Remove(e.Item);
                        //parent.Enabled = false;
                    }
                }
            }
        }
    }


}

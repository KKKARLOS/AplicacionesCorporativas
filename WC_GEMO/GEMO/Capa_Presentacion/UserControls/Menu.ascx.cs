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
                //bEsMiembro = true;
                //if (HttpContext.Current.User.IsInRole("A"))
                //{
                //    bEsMiembro = true;
                //    break;
                //}
                if (RolMenu == "*")
                {
                    //if (mapNode.Title == "Reconexión" && Session["Admin"].ToString() != "A")
                    //    bEsMiembro = false;
                    //else
                        bEsMiembro = true;
                    break;
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
                System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);

                if (e.Item.Depth == 0)
                {
                    menu.Items.Remove(e.Item);
                }
                else
                {
                    System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                    if (parent != null)
                    {
                        parent.ChildItems.Remove(e.Item);
                    }
                }
            }
        }
    }


}

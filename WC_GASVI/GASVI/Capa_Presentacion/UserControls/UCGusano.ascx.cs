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

public partial class Capa_Presentacion_UserControls_UCGusano : System.Web.UI.UserControl
{
    public string sRoles = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (string Rol in Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name))
        {
            if (sRoles == "") sRoles = Rol;
            else sRoles += ","+ Rol;
        }
    }
}

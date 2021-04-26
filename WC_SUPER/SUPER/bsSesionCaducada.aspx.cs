using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bsSesionCaducada : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Cuando se llega a esta página pueden existir variables de sesión asi que se vacían para una entrada limpia a Default.aspx
        Session.Clear();
    }
}
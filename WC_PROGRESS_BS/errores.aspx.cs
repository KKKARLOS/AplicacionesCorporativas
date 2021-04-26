using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class errores : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["IDRED"] == null)
        {
            Response.Redirect("~/default.aspx");
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        else {
            Error.Style.Add("display", "block");       
        }
    }
}
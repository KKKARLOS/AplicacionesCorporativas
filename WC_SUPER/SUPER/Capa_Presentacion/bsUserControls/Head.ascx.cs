using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_bsUserControls_Head : System.Web.UI.UserControl
{
    private string _PreCss = "";
    private string _PostCss = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_PreCss.Trim().Length > 0) {
            this.ltrPreCss.Text = "<link href='" + _PreCss + "' rel='stylesheet' />";
        }
        if (_PostCss.Trim().Length > 0)
        {
            this.ltrPostCss.Text = "<link href='" + _PostCss + "' rel='stylesheet' />";
        }

        Session["VERSIONAPP"] = "bs";        
        
    }

    public string PreCss{
        get { return _PreCss;  }
        set { _PreCss = value; }
    }
    public string PostCss
    {
        get { return _PostCss; }
        set { _PostCss = value; }
    }
}
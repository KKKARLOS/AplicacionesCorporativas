using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Utilities;

public partial class BaseWebUserControl : System.Web.UI.UserControl
{
    public int CurrentID
    {
        get
        {
            General objGeneral = new General();
            return objGeneral.GetCurrentID();
        }
    }

    
}

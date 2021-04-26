using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_SIC_Test_uploadfile_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "IB.vars.miarray = [1,2,3,4,5]; IB.vars.miarray2 = {campo1: 'valor del campo 1', campo2: 'valor del campo 2'}", true);
    }
}
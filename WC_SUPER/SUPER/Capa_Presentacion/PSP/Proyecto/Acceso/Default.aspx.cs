using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page
{
    public string sOpcion = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Estructura técnica";

            sOpcion = Request.QueryString["sOP"].ToString();
            //this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/BotoneraAccesoRapido.ascx"));
        }
    }
}

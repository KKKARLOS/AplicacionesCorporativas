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
using GASVI.BLL;

public partial class Calendario_getFecha : System.Web.UI.Page
{
    public string strErrores;
    public string sFecha = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["sFecha"] != null)
                    sFecha = Request.QueryString["sFecha"].ToString();
            }
            catch (Exception ex)
            {
                this.strErrores = Errores.mostrarError("Error al obtener el calendario", ex);
            }
        }
    }
}

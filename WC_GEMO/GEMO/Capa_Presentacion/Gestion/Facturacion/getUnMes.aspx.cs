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
using GEMO.BLL;
using Microsoft.JScript;

public partial class getUnMes : System.Web.UI.Page
{
    public string sErrores= "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cboMes.Value = DateTime.Now.Month.ToString();
            txtAnno.Text = DateTime.Now.Year.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
}

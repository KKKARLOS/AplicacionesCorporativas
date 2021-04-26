using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.BLL;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            strTablaHTML = SUPER.BLL.CRONOACTUMA.Catalogo();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
}

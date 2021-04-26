using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using SUPER.Capa_Negocio;


/// <summary>
/// Descripción breve de obtenerRecurso.
/// </summary>
public partial class NivelPresupuesto : System.Web.UI.Page
{
    public string strErrores;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback && Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        try
        {
            if (!Page.IsCallback)
            {

                if (Request.QueryString["sNP"] != null)
                {
                    string sNP = Request.QueryString["sNP"].ToString();
                    switch (sNP)
                    {
                        case "P":
                            this.rdbNivel.Items[0].Selected = true;
                            break;
                        case "F":
                            this.rdbNivel.Items[1].Selected = true;
                            break;
                        case "A":
                            this.rdbNivel.Items[2].Selected = true;
                            break;
                        case "T":
                            this.rdbNivel.Items[3].Selected = true;
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            strErrores = Errores.mostrarError("Error al cargar los datos de la página", ex);
        }
    }
}


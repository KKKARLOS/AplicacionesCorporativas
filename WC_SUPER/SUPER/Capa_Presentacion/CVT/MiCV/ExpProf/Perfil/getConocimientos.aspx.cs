using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SUPER.BLL;
using SUPER.Capa_Negocio;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_CVT_MiCV_ExpProf_Perfil_getConocimientos : System.Web.UI.Page
{
    public string TipoCono = "", sErrores;
    public string strTablaHtmlTodos, strTablaHtml;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
            
                if (Request.QueryString["Tipo"] != null)
                {//Tipo de conocimiento
                    this.hdnTipo.Value = Request.QueryString["Tipo"];
                    if (hdnTipo.Value == "ACS")
                        TipoCono = "Sectorial";
                    else
                        TipoCono = "Tecnologico";
                }
                CargarDatos(hdnTipo.Value, Request.QueryString["cono"]);
               
            }
            catch (Exception ex)
            {
                sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener los datos", ex);
            }
            
        }
    }

    private void CargarDatos(string tipo, string conocimientos)
    {
        string[] aDatos = Regex.Split(SUPER.BLL.AreaConocimientoCTV.CatalogoAreas(tipo, conocimientos),"@#@");
        strTablaHtmlTodos = aDatos[0];
        strTablaHtml = aDatos[1];
    }
}

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
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_ECO_Consultas_getCriterio_Default : System.Web.UI.Page
{
    public string sErrores = "", sTipo="";

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
                sTipo = Request.QueryString["nTipo"].ToString();
                string sTitulo = "Selección de criterio: ";
                switch (int.Parse(sTipo))
                {
                    case 1: sTitulo  += "Unidad de Negocio"; break;
                    case 2: sTitulo  += "Centro de Responsabilidad"; break;
                    case 3: case 12: sTitulo += "Perfil"; break;
                    case 4: sTitulo  += "Título Académico"; break;
                    case 5: case 11: sTitulo  += "Entorno Tecnológico"; break;
                    case 6: sTitulo  += "Certificado"; break;
                    case 7: sTitulo  += "Idioma"; break;
                    case 8: sTitulo  += "Titulación Idiomas"; break;
                    case 9: sTitulo  += "Área Conocimiento Sectorial"; break;
                    case 10: sTitulo += "Área Conocimiento Tecnológico"; break;
                    case 13: sTitulo += "Sector"; break;
                    case 14: sTitulo += "Segmento"; break;
                    case 15: sTitulo += "Cliente"; break;

                    //case 27: sTitulo += "Profesional"; break;
                }
                this.Title = sTitulo;
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
            }
        }
    }
}

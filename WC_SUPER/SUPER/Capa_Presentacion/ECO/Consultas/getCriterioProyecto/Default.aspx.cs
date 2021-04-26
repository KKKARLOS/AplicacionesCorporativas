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
                    case 1: sTitulo += "Ámbito"; break;
                    case 2: sTitulo += "Responsable de proyecto"; break;
                    case 3: sTitulo += "Naturaleza"; break;
                    case 4: sTitulo += "Modelo de contratación"; break;
                    case 5: sTitulo += "Horizontal"; break;
                    case 6: sTitulo += "Sector"; break;
                    case 7: sTitulo += "Segmento"; break;
                    case 8: sTitulo += "Cliente"; break;
                    case 9: sTitulo += "Contrato"; break;
                    case 10: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO); break;
                    case 11: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1); break;
                    case 12: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2); break;
                    case 13: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3); break;
                    case 14: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4); break;
                    case 16: sTitulo += "Proyecto"; break;
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

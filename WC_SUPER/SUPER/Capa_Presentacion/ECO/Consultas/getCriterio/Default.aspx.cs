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
                    case 1: sTitulo  += "Ámbito"; break;
                    case 2: sTitulo  += "Responsable de proyecto"; break;
                    case 3: sTitulo  += "Naturaleza"; break;
                    case 4: sTitulo  += "Modelo de contratación"; break;
                    case 5: sTitulo  += "Horizontal"; break;
                    case 6: sTitulo  += "Sector"; break;
                    case 7: sTitulo  += "Segmento"; break;
                    case 8: sTitulo  += "Cliente"; break;
                    case 9: sTitulo  += "Contrato"; break;
                    case 10: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO); break;
                    case 11: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1); break;
                    case 12: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2); break;
                    case 13: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3); break;
                    case 14: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4); break;
                    case 17: sTitulo += "Proveedores"; break;
                    case 18: sTitulo += "Centro de responsabilidad "; break;
                    case 19: sTitulo += "Concepto económico "; break;
                    case 20: sTitulo += "Calificador OC / FA"; break;
                    case 22: sTitulo += "Empresa de facturación"; break;
                    case 23: sTitulo += "Rol "; break;
                    case 24: sTitulo += "Evaluador "; break;
                    case 25: sTitulo += "Centro de trabajo "; break;
                    case 26: sTitulo += "Oficina "; break;
                    case 27: sTitulo += "Profesional "; break;
                    case 32: sTitulo += "Comercial"; break;
                    case 34: sTitulo += "País"; break;
                    case 35: sTitulo += "Provincia"; break;
                    case 37: sTitulo += "Organización comercial"; break;
                    case 38: sTitulo += "Soporte administrativo"; break;
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

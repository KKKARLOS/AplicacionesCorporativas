using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using SUPER.Capa_Negocio;

namespace SUPER.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerGF
	/// </summary>
    public partial class obtenerGF : System.Web.UI.Page
	{
        public static string strTablaHTML = "<table id='tblDatos'></table>";
        public string strErrores, sNodo = "";        
        
        private void Page_Load(object sender, System.EventArgs e)
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


                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) 
                {
                    txtDesNodo.Visible = true;
                    this.lblNodo.Visible = true;
                    gomaNodo.Visible = true;
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    
                }     

                string sCR = Request.QueryString["nCR"].ToString();
                listaGF(sCR);
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener los grupos funcionales", ex);
            }
		}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string listaGF(string sCR)
        {

            StringBuilder strBuilder = new StringBuilder();
            SqlDataReader dr;

            string titulo = "";
            Boolean esAdmin = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();

            strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
            strBuilder.Append("<colgroup><col style='width: 396px;' /></colgroup>");
            strBuilder.Append("<tbody>");
            if(sCR == "" )  dr = GrupoFun.CatalogoGrupos(null);
            else  dr = GrupoFun.CatalogoGrupos(int.Parse(sCR));

            while (dr.Read())
            {
                if (esAdmin) titulo = dr["DenominacionCR"].ToString() + " - " + dr["Nombre"].ToString();
                else titulo = dr["Nombre"].ToString();

                strBuilder.Append("<tr id='" + dr["IdGrupro"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)' title='"+ titulo + "'>");
                strBuilder.Append("<td><span style='width:380px;' class='NBR'>" + dr["Nombre"].ToString() + "</span></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            strBuilder.Append("</tbody>");
            strBuilder.Append("</table>");
            strTablaHTML = strBuilder.ToString();

            return "OK@#@" + strTablaHTML;
        }


    }

}

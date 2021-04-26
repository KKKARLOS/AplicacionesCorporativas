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
using SUPER.Capa_Negocio;

namespace SUPER.Capa_Presentacion
{
    /// <summary>
    /// Descripción breve de obtenerTarea
    /// </summary>
    public partial class obtenerTarea : System.Web.UI.Page
    {
        public string strErrores;
        public string strTablaHtml;

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

                int nIdPE;
                nIdPE = int.Parse(quitaPuntos(Request.QueryString["nPE"].ToString()));
                listaTareas(nIdPE);
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener las tareas del proyecto económico", ex);
            }
        }

        private void listaTareas(int iNumPE)
        {
            StringBuilder strBuilder = new StringBuilder();
            double fAvance, fETPL, fETPR, fConsumo;
            string sCad, sFecha;

            strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
            strBuilder.Append("<colgroup><col style='width:50px'/><col style='width:346px'/></colgroup>");
            strBuilder.Append("<tbody>");
            SqlDataReader dr = HITOPSP.CatalogoTareasPE(iNumPE);

            while (dr.Read())
            {
                strBuilder.Append("<tr style='height:16px;' ");

                StringBuilder sbTitle = new StringBuilder();
                sbTitle.Append("<b>Proy. Eco.</b>: ");
                sbTitle.Append(dr["nom_proyecto"].ToString().Replace((char)34, (char)39));
                sbTitle.Append("<br><b>Proy. Téc.</b>: ");
                sbTitle.Append(dr["t331_despt"].ToString().Replace((char)34, (char)39));
                if (dr["t334_desfase"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Fase</b>:          ");
                    sbTitle.Append(dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                }
                if (dr["t335_desactividad"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Actividad</b>:  ");
                    sbTitle.Append(dr["t335_desactividad"].ToString().Replace((char)34, (char)39));
                }
                sbTitle.Append("<br><b>Tarea</b>:  ");
                sbTitle.Append(dr["desTarea"].ToString().Replace((char)34, (char)39));

                strBuilder.Append("id='");
                strBuilder.Append(dr["codTarea"].ToString());
                strBuilder.Append("' onclick='mm(event)' ondblclick='aceptar()' onmouseover='TTip(event);' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[");
                strBuilder.Append(sbTitle);
                strBuilder.Append("]\"");
                sCad = dr["ETPL"].ToString();
                if (sCad == "") fETPL = 0;
                else fETPL = double.Parse(sCad);
                strBuilder.Append(" ETPL='" + fETPL.ToString("N") + "'");

                sCad = dr["FIPL"].ToString();
                if (sCad != "")
                {
                    sFecha = DateTime.Parse(sCad).ToShortDateString();
                }
                else sFecha = "";
                strBuilder.Append(" FIPL='" + sFecha + "'");

                sCad = dr["FFPL"].ToString();
                if (sCad != "")
                {
                    sFecha = DateTime.Parse(sCad).ToShortDateString();
                }
                else sFecha = "";
                strBuilder.Append(" FFPL='" + sFecha + "'");

                sCad = dr["ETPR"].ToString();
                if (sCad == "") fETPR = 0;
                else fETPR = double.Parse(sCad);
                strBuilder.Append(" ETPR='" + fETPR.ToString("N") + "'");

                sCad = dr["FFPR"].ToString();
                if (sCad != "")
                {
                    sFecha = DateTime.Parse(sCad).ToShortDateString();
                }
                else sFecha = "";
                strBuilder.Append(" FFPR='" + sFecha + "'");

                sCad = dr["Consumo"].ToString();
                if (sCad == "")
                {
                    fConsumo = 0;
                }
                else
                {
                    fConsumo = double.Parse(sCad);
                }
                strBuilder.Append(" CONSUMO='" + fConsumo.ToString("N") + "'");

                if (fConsumo == 0) fAvance = 0;
                else fAvance = (fETPR * 100) / fConsumo;
                strBuilder.Append(" AVANCE='" + fAvance.ToString("N") + "'");
                strBuilder.Append(">");

                strBuilder.Append("<td style='text-align:right'>");
                strBuilder.Append(int.Parse(dr["codTarea"].ToString()).ToString("#,###"));
                strBuilder.Append("</td><td style='padding-left:5px;'><nobr style='width:346px;' class='NBR'>");
                strBuilder.Append(dr["desTarea"].ToString());
                strBuilder.Append("</nobr></td>");
            }
            dr.Close();
            dr.Dispose();
            strBuilder.Append("</tbody>");
            strBuilder.Append("</table>");
            strTablaHtml = strBuilder.ToString();
        }

        private string quitaPuntos(string sCadena)
        {//Finalidad:Elimina los puntos de una cadena
            string sRes = sCadena;
            if (sCadena == "") return "";
            sRes = sRes.Replace(".", "");
            return sRes;
        }
    }
}

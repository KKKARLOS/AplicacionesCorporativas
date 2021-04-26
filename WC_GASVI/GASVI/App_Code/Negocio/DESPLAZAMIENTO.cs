using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using GASVI.DAL;

namespace GASVI.BLL
{
    public partial class DESPLAZAMIENTO
    {
        #region Metodos

        public static string ObtenerDesplazamientosRangoFechas(int t314_idusuario, DateTime fec_desde, DateTime fec_hasta, int t420_idreferencia)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style='width:750px;cursor:url(../images/imgManoAzul2.cur),pointer;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:60px; padding-right:10px; text-align:right;' />");
            sb.Append("    <col style='width:220px; ' />");
            sb.Append("    <col style='width:220px;' />");
            sb.Append("    <col style='width:100px;' />");
            sb.Append("    <col style='width:100px;' />");
            sb.Append("    <col style='width:50px; padding-right:2px; text-align:right;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.DESPLAZAMIENTO.ObtenerDesplazamientos(null, t314_idusuario, fec_desde, fec_hasta, t420_idreferencia);
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;' id='" + dr["t615_iddesplazamiento"].ToString() + "' ");
                sb.Append("ondblclick='aceptarClick(this)'>");

                sb.Append("<td>" + int.Parse(dr["t615_iddesplazamiento"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W210' ondblclick='aceptarClick(this)'>" + dr["t615_destino"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W210' ondblclick='aceptarClick(this)' ");
                if (dr["t615_observaciones"].ToString() != "")
                {
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../images/info.gif' style='vertical-align:middle' />  Información]");
                    sb.Append(" body=[<label style='width:70px;'>Observaciones:</label>" + dr["t615_observaciones"].ToString() + "] hideselects=[off] \" ");
                }
                sb.Append(">" + dr["t615_observaciones"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t615_fechoraida"].ToString().Substring(0, dr["t615_fechoraida"].ToString().Length -3) + "</td>");
                sb.Append("<td>" + dr["t615_fechoravuelta"].ToString().Substring(0, dr["t615_fechoravuelta"].ToString().Length - 3) + "</td>");
                sb.Append("<td>" + dr["numero_usos"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        #endregion
    }
}
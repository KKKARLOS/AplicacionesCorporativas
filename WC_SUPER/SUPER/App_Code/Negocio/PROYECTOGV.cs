using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using SUPER.Capa_Datos;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    public partial class PROYECTOGV
    {
        #region Metodos

        public static string ObtenerProyectoNuevaNota(int t314_idusuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class=' texto MA' style='width:750px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:60px;' />");
            sb.Append("    <col style='width:420px;' />");
            sb.Append("    <col style='width:250px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            //string sCod = "";

            SqlDataReader dr = SUPER.Capa_Datos.PROYECTOGV.ObtenerCatalogoCreacionNota(null, t314_idusuario);
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px' id=\"" + dr["t305_idproyectosubnodo"].ToString() + "\" ");

                sb.Append("responsable=\"" + Utilidades.escape(dr["Responsable_Proyecto"].ToString()) + "\" ");
                sb.Append("sexo_aprobador=\"" + dr["Sexo_Aprobador"].ToString() + "\" ");
                sb.Append("aprobador=\"" + Utilidades.escape(dr["Aprobador"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString().Trim()) + "\" ");
                
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("ondblclick='aceptarClick(this)'>");

                sb.Append("<td style='padding-left:2px;'>");
                switch (dr["t301_estado"].ToString()) {
                    case "A": sb.Append("<img class='ICO' src='../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' />"); break;
                    case "C": sb.Append("<img class='ICO' src='../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' />"); break;
                    case "H": sb.Append("<img class='ICO' src='../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' />"); break;
                    case "P": sb.Append("<img class='ICO' src='../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' />"); break;
                }
                sb.Append("</td>");
                sb.Append("<td style='padding-right:10px; text-align: right;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W400' ondblclick='aceptarClick(this)' style='noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información]");
                sb.Append(" body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable_Proyecto"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>" + ((dr["Sexo_Aprobador"].ToString() == "V") ? "Aprobador" : "Aprobadora") + ":</label>" + dr["Aprobador"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] ");
                sb.Append("hideselects=[off]\">" + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W240' ondblclick='aceptarClick(this)' style='noWrap:true;'>" + dr["t302_denominacion"].ToString().Trim() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string GetNombre(int t305_idproyectosubnodo)
        {
            string sRes = "";
            SqlDataReader dr = PROYECTO.fgGetDatosProy(t305_idproyectosubnodo);
            if (dr.Read())
                sRes = dr["t301_denominacion"].ToString();
            dr.Close();
            dr.Dispose();

            return sRes;
        }
        public static string GetCodigoyNombre(int t305_idproyectosubnodo)
        {
            string sRes = "";
            SqlDataReader dr = PROYECTO.fgGetDatosProy(t305_idproyectosubnodo);
            if (dr.Read())
                sRes = dr["t301_idproyecto"].ToString() + " - " + dr["t301_denominacion"].ToString();
            dr.Close();
            dr.Dispose();

            return sRes;
        }

        #endregion
    }
}

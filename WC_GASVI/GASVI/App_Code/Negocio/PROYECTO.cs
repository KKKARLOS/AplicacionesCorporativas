using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using GASVI.DAL;

namespace GASVI.BLL
{
    public partial class PROYECTO
    {
        #region Metodos

        public static string ObtenerProyectoNuevaNota(int t314_idusuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:750px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:60px; padding-right:10px; text-align: right;' />");
            sb.Append("    <col style='width:420px;' />");
            sb.Append("    <col style='width:250px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            //string sCod = "";

            SqlDataReader dr = DAL.PROYECTO.ObtenerCatalogoCreacionNota(null, t314_idusuario);
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
                sb.Append("onclick='ms(this)' ");
                sb.Append("ondblclick='aceptarClick(this)'>");

                sb.Append("<td>");
                switch (dr["t301_estado"].ToString()) {
                    case "A": sb.Append("<img class='ICO' src='../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' />"); break;
                    case "C": sb.Append("<img class='ICO' src='../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' />"); break;
                    case "H": sb.Append("<img class='ICO' src='../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' />"); break;
                    case "P": sb.Append("<img class='ICO' src='../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' />"); break;
                }
                sb.Append("</td>");
                sb.Append("<td>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W400' ondblclick='aceptarClick(this)' style='noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información]");
                sb.Append(" body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable_Proyecto"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>" + ((dr["Sexo_Aprobador"].ToString() == "V") ? "Aprobador" : "Aprobadora") + ":</label>" + dr["Aprobador"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] ");
                sb.Append("hideselects=[off]\">" + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W240'>" + dr["t302_denominacion"].ToString().Trim() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerProyectoConsulta(int t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:750px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px; ' />");
            sb.Append("    <col style='width:80px; ' />");
            sb.Append("    <col style='width:400px;' />");
            sb.Append("    <col style='width:250px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            //string sCod = "";

            SqlDataReader dr = DAL.PROYECTO.ObtenerCatalogoConsulta(null, t001_idficepi);
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px' id=\"" + dr["t305_idproyectosubnodo"].ToString() + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString().Trim()) + "\" ");

                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("onclick='ms(this)' ");
                sb.Append("ondblclick='aceptarClick(this)'>");

                sb.Append("<td style='text-align:center;'>");
                switch (dr["t301_estado"].ToString())
                {
                    case "A": sb.Append("<img class='ICO' src='../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' />"); break;
                    case "C": sb.Append("<img class='ICO' src='../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' />"); break;
                    case "H": sb.Append("<img class='ICO' src='../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' />"); break;
                    case "P": sb.Append("<img class='ICO' src='../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' />"); break;
                }
                sb.Append("</td>");
                sb.Append("<td style='padding-right:3px; text-align: right;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W400' ondblclick='aceptarClick(this)' style='noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información]");
                sb.Append(" body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] ");
                sb.Append("hideselects=[off]\">" + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W250'>" + dr["t302_denominacion"].ToString().Trim() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerProyectosConSolicitudes(string sNodo, 
            string sEstado, 
            string sCategoria, 
            string sIdCliente, 
            string sIdResponsable,
            string sNumPE, 
            string sDesPE, 
            string sTipoBusqueda)
        {
            StringBuilder sb = new StringBuilder();
            int? idNodo = null;
            int? idCliente = null;
            int? idResponsable = null;
            int? numPE = null;

            if (sNodo != "") idNodo = int.Parse(sNodo);
            if (sIdCliente != "") idCliente = int.Parse(sIdCliente);
            if (sIdResponsable != "") idResponsable = int.Parse(sIdResponsable);
            if (sNumPE != "" && sNumPE != "0") numPE = int.Parse(sNumPE);

            sb.Append("<table id='tblDatos' class='MA' style='width:940px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:20px' />");
            sb.Append("     <col style='width:20px' />");
            sb.Append("     <col style='width:80px; ' />");
            sb.Append("     <col style='width:340px' />");
            sb.Append("     <col style='width:220px' />");
            sb.Append("     <col style='width:260px' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.PROYECTO.CatalogoProyectosConSolicitudes(idNodo, sEstado, sCategoria, idCliente, idResponsable, numPE,
                        Utilidades.unescape(sDesPE), sTipoBusqueda);

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px' id='" + dr["t305_idproyectosubnodo"].ToString() + "' ");

                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:3px;'>");
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                sb.Append("<td><nobr class='NBR W340' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                sb.Append("header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información]");
                sb.Append("body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] ");
                sb.Append("hideselects=[off]\">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W210'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W250'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerTooltipProyectoUsuario(int t305_idproyectosubnodo, int t314_idusuario)
        {
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = DAL.PROYECTO.ObtenerTooltipProyectoUsuario(null, t305_idproyectosubnodo, t314_idusuario);
            if (dr.Read())
            {
                sb.Append("<label style='width:70px;'>Proyecto:</label>" + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable_Proyecto"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>" + ((dr["Sexo_Aprobador"].ToString() == "V") ? "Aprobador" : "Aprobadora") + ":</label>" + dr["Aprobador"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return sb.ToString();
        }
        #endregion
    }
}

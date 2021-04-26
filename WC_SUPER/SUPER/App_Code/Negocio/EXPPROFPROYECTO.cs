using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    public partial class EXPPROFPROYECTO
    {
	    public EXPPROFPROYECTO()
	    {
		    //
		    // TODO: Agregar aquí la lógica del constructor
		    //
	    }

        #region Metodos

        public static string ObtenerProyectosByExperiencia(int t808_idexpprof)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' style='width:420px;' cellspacing='0' cellpadding='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:60px;' />");
            sb.Append(" <col style='width:340px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = SUPER.DAL.EXPPROFPROYECTO.CatalogoByExperiencia(null, t808_idexpprof);
            while (dr.Read())
            {
                sb.Append("<tr ");
                string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString();
                sb.Append("sTooltip=\"" + Utilidades.escape(sTooltip) + "\" "); 
                sb.Append("onmouseover=\"showTTE(this.getAttribute('sTooltip'))\" onMouseout=\"hideTTE()\" >");
                sb.Append("<td><img src=\"" + HttpContext.Current.Session["strServer"].ToString() + "images/");
                //imgFN.gif
                switch (dr["t301_estado"].ToString())
                {
                    case "P": sb.Append("imgIconoProyPresup.gif"); break;
                    case "A": sb.Append("imgIconoProyAbierto.gif"); break;
                    case "C": sb.Append("imgIconoProyCerrado.gif"); break;
                    case "H": sb.Append("imgIconoProyHistorico.gif"); break;
                }
                sb.Append("\"></td>");
                sb.Append("<td style='text-align:right;'><nobr class='NBR W50'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W350'>" + dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return sb.ToString();
        }

        public static int CountProyectosByExperiencia(int t808_idexpprof)
        {
            return SUPER.DAL.EXPPROFPROYECTO.CountProyectosByExperiencia(null, t808_idexpprof);
        }


        #endregion
    }
}
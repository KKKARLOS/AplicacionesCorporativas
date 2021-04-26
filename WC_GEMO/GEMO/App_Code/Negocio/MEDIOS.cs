using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;
using GEMO.DAL;

namespace GEMO.BLL
{
	public partial class MEDIOS
	{
		#region Metodos

        public static string obtenerMedios(string sIdFicepi)
        {
            string sResul = "", sToolTip = "";
            StringBuilder sb = new StringBuilder();

            DataSet ds = GEMO.DAL.MEDIOS.ObtenerMedios(int.Parse(sIdFicepi));

            sb.Append("<table id='tblMedios1' style='WIDTH: 430px;'>");
            sb.Append("<colgroup><col style='width:430px;padding-left:3px;' /></colgroup>");

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                switch (oFila["medios"].ToString())
                {
                    case "10": sToolTip = oFila["t133_denominacion"].ToString(); break;
                    case "01": sToolTip = "Rol"; break;
                    case "11": sToolTip = "Rol / " + oFila["t133_denominacion"].ToString(); break;
                }

                sb.Append("<tr id='" + oFila["t134_idmedio"].ToString() + "' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td><nobr class='NBR W420' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Disponible por:] body=[ " + sToolTip + "] hideselects=[off]\">" + oFila["t134_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append("@#@");

            sb.Append("<table id='tblMedios2' style='WIDTH: 430px; '>");
            sb.Append("<colgroup><col style='width:165px;padding-left:3px;' /><col style='width:25px;' /><col style='width:80px;' /><col style='width:160px;' /></colgroup>");

            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                sb.Append("<tr id='" + oFila["t134_idmedio"].ToString() + "' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td>" + oFila["t134_denominacion"].ToString() + "</td>");
                //if (oFila["idPrefijo"].ToString()=="")
                //    sb.Append("<td>" + oFila["idPrefijo"].ToString() + "</td>");
                //else
                //    sb.Append("<td>" + short.Parse(oFila["idPrefijo"].ToString()).ToString("000") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:5px'>" + oFila["idPrefijo"].ToString() + "</td>");
                sb.Append("<td>" + oFila["idItem"].ToString() + "</td>");
                sb.Append("<td>" + oFila["desItem"].ToString() + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            ds.Dispose();

            sResul = sb.ToString();

            return sResul;
        }

        #endregion
	}
}

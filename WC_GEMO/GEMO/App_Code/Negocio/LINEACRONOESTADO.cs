using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;
using GEMO.DAL;

namespace GEMO.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : LINEACRONOESTADO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T709_LINEACRONOESTADO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public class LINEACRONOESTADO
	{
		#region Metodos
        public static string Catalogo(int iIdLinea)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style='WIDTH: 600px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:90px' />");    //Estado   
            sb.Append("    <col style='width:140px;' />");  //Fecha
            sb.Append("    <col style='width:370px;' />");  //Usuario
            sb.Append("</colgroup>");

            SqlDataReader dr = GEMO.DAL.LINEACRONOESTADO.Catalogo(iIdLinea);

            int i = 0;
            while (dr.Read())
            {
                i++;
                sb.Append("<tr style='height:20px;' onmouseover='TTip(event)'>");
                sb.Append("<td>" + dr["estado"].ToString().Trim() + "</td>");
                sb.Append("<td>" + dr["fecha"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W360'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

		#endregion
	}
}

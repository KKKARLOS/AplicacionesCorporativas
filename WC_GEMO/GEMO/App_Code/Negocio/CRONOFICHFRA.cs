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
    /// Class	 : CRONOFICHFRA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T709_LINEACRONOESTADO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
    public class CRONOFICHFRA
	{
		#region Metodos
        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style='width:600px;text-align:left'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:110px' />");   //Fichero   
            sb.Append("    <col style='width:120px;' />");  //Fecha
            sb.Append("    <col style='width:370px;' />");  //Facturador
            sb.Append("</colgroup>");

            SqlDataReader dr = GEMO.DAL.CRONOFICHFRA.Catalogo();

            int i = 0;
            while (dr.Read())
            {
                i++;
                sb.Append("<tr style='height:20px;' onmouseover='TTip(event)'>");
                sb.Append("<td>" + dr["fichero"].ToString().Trim() + "</td>");
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

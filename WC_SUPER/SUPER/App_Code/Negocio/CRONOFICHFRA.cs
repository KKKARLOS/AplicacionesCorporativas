using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.DAL;

namespace SUPER.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
    /// Class	 : CRONOACTUMA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T961_ACTU_GLOB_TPL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
    public class CRONOACTUMA
	{
		#region Metodos
        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style='width: 220px;text-align:center'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:110px' />");   //F.Límite  
            sb.Append("    <col style='width:110px;' />");  //F.Realización
            sb.Append("</colgroup>");

            SqlDataReader dr = SUPER.DAL.CRONOACTUMA.Catalogo();

            int i = 0;
            while (dr.Read())
            {
                i++;
                sb.Append("<tr style='height:20px;' onmouseover='TTip()'>");
                sb.Append("<td>" + DateTime.Parse(dr["t961_flimite"].ToString()).ToShortDateString() + "</td>");
                if ((dr["t961_frealizacion"].ToString()) == "")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + DateTime.Parse(dr["t961_frealizacion"].ToString()).ToShortDateString() + "</td>");
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

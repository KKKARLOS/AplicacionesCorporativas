using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

//using SUPER.Capa_Datos;
//Para usar el StringBuilder
using System.Text;
namespace SUPER.BLL
{

    /// <summary>
    /// Areas de conocimiento sectorial
    /// </summary>
    public class ACONSECT
    {
        public ACONSECT()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static string GetAreas(Nullable<int> t809_idaconsect, string t809_denominacion, byte nOrden, byte nAscDesc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px; border-collapse: collapse; ' cellspacing='0' border='0'>");
            //sb.Append("<tbody>");
            SqlDataReader dr = SUPER.DAL.ACONSECT.Catalogo(t809_idaconsect, t809_denominacion, nOrden, nAscDesc);

            while (dr.Read())
            {
                sb.Append("<tr style='noWrap:true; height:16px;' ");
                sb.Append(" id='" + dr["t809_idaconsect"].ToString() + "'");
                sb.Append(" onclick='mm(event)' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td>" + dr["t809_denominacion"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            //sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
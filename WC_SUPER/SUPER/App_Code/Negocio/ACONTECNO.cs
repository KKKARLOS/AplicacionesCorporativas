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
    /// Areas de conocimiento tecnológico
    /// </summary>
    public class ACONTECNO
    {
        public ACONTECNO()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static string GetAreas(Nullable<int> t810_idacontecno, string t810_denominacion, byte nOrden, byte nAscDesc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px; border-collapse: collapse; ' cellspacing='0' border='0'>");
            //sb.Append("<tbody>");
            SqlDataReader dr = SUPER.DAL.ACONTECNO.Catalogo(t810_idacontecno, t810_denominacion, nOrden, nAscDesc);

            while (dr.Read())
            {
                sb.Append("<tr style='noWrap:true; height:16px;' ");
                sb.Append(" id='" + dr["t810_idacontecno"].ToString() + "'");
                sb.Append(" onclick='mm(event)' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td>" + dr["t810_denominacion"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            //sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
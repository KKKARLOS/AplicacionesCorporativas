using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using GEMO.DAL;

/// <summary>
/// Summary description for DEPARTAMENTO
/// </summary>
namespace GEMO.BLL
{
    public class ESTADO
    {
        public static string Obtener(string t710_denominacion, string sTipoBusqueda, Nullable<int> t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MAM' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='padding-left:5px;'></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = GEMO.DAL.ESTADO.Catalogo(t710_denominacion, sTipoBusqueda, t001_idficepi);

            int i = 0;
            bool bExcede = false;
            while (dr.Read())
            {
                i++;
                sb.Append("<tr id='" + i.ToString() + "' "); 
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;' onmouseover='TTip(event)'>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);

                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();
            if (!bExcede)
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
            }
            else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }
            return sb.ToString();
        }
        public static string Obtener2(string sTS)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='padding-left:5px;'></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = GEMO.DAL.ESTADO.Catalogo2();

            int i = 0;
            bool bExcede = false;
            while (dr.Read())
            {
                i++;
                sb.Append("<tr id='" + dr["ID"].ToString() + "' onclick=");

                if (sTS == "S") sb.Append("'ms(this)'");
                else sb.Append("'mm(event)'");

                sb.Append(" ondblclick='aceptarClick(this.rowIndex)' style='height:20px;' onmouseover='TTip(event)'>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);

                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();
            if (!bExcede)
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
            }
            else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }
            return sb.ToString();
        }

    }
}
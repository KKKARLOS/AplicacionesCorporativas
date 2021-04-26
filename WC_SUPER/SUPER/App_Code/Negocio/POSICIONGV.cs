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
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : POSICIONGV
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T421_POSICIONGV
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	16/03/2011 11:54:32	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class POSICIONGV
    {
        public static string CatalogoGastos(int nIDNota, bool bLectura)
        {
            StringBuilder sb = new StringBuilder();
			sb.Append("<table id='tblGastos' ");
            if (!bLectura)
                sb.Append("class='MANO' ");
            sb.Append("style='width:970px;text-align:right;' mantenimiento='1'>");
	        sb.Append("<colgroup>");
            sb.Append("    <col style='width:130px;' />");//Fechas
            sb.Append("    <col style='width:165px;' />");//Destino
		    sb.Append("    <col style='width:20px;' />");//Comentario
		    sb.Append("    <col style='width:30px' />");//C
		    sb.Append("    <col style='width:30px' />");//M
		    sb.Append("    <col style='width:30px' />");//E
		    sb.Append("    <col style='width:30px' />");//A
		    sb.Append("    <col style='width:65px' />");//Importe
		    sb.Append("    <col style='width:40px' />");//Kms.
		    sb.Append("    <col style='width:65px' />");//Importe
		    sb.Append("    <col style='width:30px' />");//ECO
		    sb.Append("    <col style='width:65px' />");//Peajes
		    sb.Append("    <col style='width:65px' />");//Comidas
		    sb.Append("    <col style='width:65px' />");//Transp.
		    sb.Append("    <col style='width:65px' />");//Hoteles
		    sb.Append("    <col style='width:75px' />");//Total
            sb.Append("</colgroup>" + (char)13);

            SqlDataReader dr = SUPER.Capa_Datos.POSICIONGV.CatalogoGastos(null, nIDNota);
            int iFila = 0;

            while (dr.Read())
            {
                sb.Append("<tr id='" + iFila + "' bd='' ");
                sb.Append("comentario=\"" + Uri.EscapeDataString(dr["t421_comentariopos"].ToString()) + "\" ");
                sb.Append("eco='" + dr["t615_iddesplazamiento"].ToString() + "' ");
                sb.Append("destino=\"" + Utilidades.escape(dr["t615_destino"].ToString()) + "\" ");
                sb.Append("ida='" + ((dr["t615_fechoraida"].ToString() == "") ? "" : dr["t615_fechoraida"].ToString().Substring(0, dr["t615_fechoraida"].ToString().Length - 3)) + "' ");
                sb.Append("vuelta='" + ((dr["t615_fechoravuelta"].ToString() == "") ? "" : dr["t615_fechoravuelta"].ToString().Substring(0, dr["t615_fechoravuelta"].ToString().Length - 3)) + "' ");
                sb.Append("style=\"height:20px;\" ");
                if (!bLectura)
                    sb.Append("onclick=\"ii(this, event);ms_class(this,'FG')\"");
                sb.Append(">");
                sb.Append("    <td style='text-align:left; padding-left:5px;'>" + ((DateTime)dr["t421_fechadesde"]).ToShortDateString() + "&nbsp;&nbsp;" + ((DateTime)dr["t421_fechahasta"]).ToShortDateString() + "</td>");//Fechas
                sb.Append("    <td style='text-align:left;'>" + dr["t421_destino"].ToString() + "</td>");//Destino
                if (dr["t421_comentariopos"].ToString() == "")
                    sb.Append("    <td></td>");//Comentario
                else
                    sb.Append("    <td style='text-align:left;'><img src='../../Images/imgComGastoOn.gif'></td>");//Comentario
                sb.Append("    <td>" + (((byte)dr["t421_ncdieta"] == 0) ? "" : dr["t421_ncdieta"].ToString()) + "</td>");//C
                sb.Append("    <td>" + (((byte)dr["t421_nmdieta"] == 0) ? "" : dr["t421_nmdieta"].ToString()) + "</td>");//M
                sb.Append("    <td>" + (((byte)dr["t421_nedieta"] == 0) ? "" : dr["t421_nedieta"].ToString()) + "</td>");//A
                sb.Append("    <td>" + (((byte)dr["t421_nadieta"] == 0) ? "" : dr["t421_nadieta"].ToString()) + "</td>");//E
                sb.Append("    <td></td>");//Importe
                sb.Append("    <td>" + (((short)dr["t421_nkms"] == 0) ? "" : short.Parse(dr["t421_nkms"].ToString()).ToString("#,###")) + "</td>");//Kms.
                sb.Append("    <td></td>");//Importe
                //sb.Append("    <td>" + dr["t615_iddesplazamiento"].ToString() + "</td>");//ECO
                sb.Append("    <td></td>");//ECO
                sb.Append("    <td>" + ((double.Parse(dr["t421_peajepark"].ToString()) == 0) ? "" : double.Parse(dr["t421_peajepark"].ToString()).ToString("N")) + "</td>");//Peajes
                sb.Append("    <td>" + ((double.Parse(dr["t421_comida"].ToString()) == 0) ? "" : double.Parse(dr["t421_comida"].ToString()).ToString("N")) + "</td>");//Comidas
                sb.Append("    <td>" + ((double.Parse(dr["t421_transporte"].ToString()) == 0) ? "" : double.Parse(dr["t421_transporte"].ToString()).ToString("N")) + "</td>");//Transp.
                sb.Append("    <td>" + ((double.Parse(dr["t421_hotel"].ToString()) == 0) ? "" : double.Parse(dr["t421_hotel"].ToString()).ToString("N")) + "</td>");//Hoteles
                sb.Append("    <td></td>");//Total
                sb.Append("</tr>" + (char)13);

                iFila++;
            }
            dr.Close();
            dr.Dispose();

            do
            {
                sb.Append("<tr id='" + iFila + "' bd='' eco='' ");
                sb.Append("comentario=\"\" ");
                sb.Append("eco='' ");
                sb.Append("destino=\"\" ");
                sb.Append("ida='' ");
                sb.Append("vuelta='' ");
                sb.Append("style=\"height:20px;\" ");
                if (!bLectura)
                    sb.Append("onclick=\"ii(this, event);ms_class(this,'FG')\"");
                sb.Append(">");
                sb.Append("    <td></td>");//Fechas
                sb.Append("    <td></td>");//Destino
                sb.Append("    <td></td>");//Comentario
                sb.Append("    <td></td>");//C
                sb.Append("    <td></td>");//M
                sb.Append("    <td></td>");//E
                sb.Append("    <td></td>");//A
                sb.Append("    <td></td>");//Importe
                sb.Append("    <td></td>");//Kms.
                sb.Append("    <td></td>");//Importe
                sb.Append("    <td></td>");//ECO
                sb.Append("    <td></td>");//Peajes
                sb.Append("    <td></td>");//Comidas
                sb.Append("    <td></td>");//Transp.
                sb.Append("    <td></td>");//Hoteles
                sb.Append("    <td></td>");//Total
                sb.Append("</tr>" + (char)13);

                iFila++;
            } while (iFila < 15);

            sb.Append("</table>");

            return sb.ToString();
        }

    }
}

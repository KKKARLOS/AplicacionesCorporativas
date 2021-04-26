using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using GASVI.DAL;

namespace GASVI.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GASVI
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
            sb.Append("style='width:970px; text-align:right;' cellSpacing='0' cellPadding='0' border='0' mantenimiento='1'>");
	        sb.Append("<colgroup>");
            sb.Append("    <col style='width:130px;' />");//Fechas
            sb.Append("    <col style='width:165px;' />");//Destino
		    sb.Append("    <col style='width:20px; ' />");//Comentario
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

            SqlDataReader dr = DAL.POSICIONGV.CatalogoGastos(null, nIDNota);
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
                    sb.Append("onclick=\"ii(this,event);ms(this,'FG')\"");
                sb.Append(">");
                sb.Append("    <td>" + ((DateTime)dr["t421_fechadesde"]).ToShortDateString() + "&nbsp;&nbsp;" + ((DateTime)dr["t421_fechahasta"]).ToShortDateString() + "&nbsp;&nbsp;" + "</td>");//Fechas
                sb.Append("    <td style='text-align:left;'>" + dr["t421_destino"].ToString() + "</td>");//Destino
                if (dr["t421_comentariopos"].ToString() == "")
                    sb.Append("    <td style='text-align:left;' class='MA'></td>");//Comentario
                else
                {
                    sb.Append("<td style='text-align:left;' class='MA'><img src='../../Images/imgComGastoOn.gif' ");//Comentario
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/imgComGastoOn.gif' style='vertical-align:middle;' />&nbsp;Comentario] ");
                    sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(dr["t421_comentariopos"].ToString()) + "] ");
                    sb.Append("hideselects=[off]\" ");
                    sb.Append(" /></td>");//Comentario
                } 
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
                    sb.Append("onclick=\"ii(this, event);ms(this,'FG')\"");
                sb.Append(">");
                sb.Append("    <td></td>");//Fechas
                sb.Append("    <td></td>");//Destino
                sb.Append("    <td class='MA'></td>");//Comentario
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

        //public static string CatalogoGastosNotaMultiProyecto(int nIDNota)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<table id='tblGastos' class='MANO' style='width:970px;text-align:right;' mantenimiento='1'>");
        //    sb.Append("<colgroup>");
        //    sb.Append("    <col style='width:130px; text-align:left;' />");//Fechas
        //    sb.Append("    <col style='width:165px; text-align:left;' />");//Destino
        //    sb.Append("    <col style='width:70px; text-align:left;' />");//Proyecto
        //    sb.Append("    <col style='width:20px; text-align:left;' />");//Comentario
        //    sb.Append("    <col style='width:25px' />");//C
        //    sb.Append("    <col style='width:25px' />");//M
        //    sb.Append("    <col style='width:25px' />");//E
        //    sb.Append("    <col style='width:25px' />");//A
        //    sb.Append("    <col style='width:65px' />");//Importe
        //    sb.Append("    <col style='width:40px' />");//Kms.
        //    sb.Append("    <col style='width:65px' />");//Importe
        //    sb.Append("    <col style='width:30px' />");//ECO
        //    sb.Append("    <col style='width:55px' />");//Peajes
        //    sb.Append("    <col style='width:55px' />");//Comidas
        //    sb.Append("    <col style='width:55px' />");//Transp.
        //    sb.Append("    <col style='width:55px' />");//Hoteles
        //    sb.Append("    <col style='width:65px' />");//Total
        //    sb.Append("</colgroup>" + (char)13);

        //    SqlDataReader dr = DAL.POSICIONGV.CatalogoGastos(null, nIDNota);
        //    int iFila = 0;

        //    while (dr.Read())
        //    {
        //        sb.Append("<tr id='" + iFila + "' bd='' ");
        //        sb.Append("idPSN='' ");
        //        sb.Append("eco='" + dr["t615_iddesplazamiento"].ToString() + "' ");
        //        sb.Append("comentario=\"" + Uri.EscapeDataString(dr["t421_comentariopos"].ToString()) + "\" ");
        //        //System.String.Format(
        //        //System.Uri.EscapeDataString(dr["t421_comentariopos"].ToString())
        //        sb.Append("style=\"height:20px;\" onclick=\"ii(this);ms(this,'FG');setProyReq(this);\">");
        //        sb.Append("    <td>" + ((DateTime)dr["t421_fechadesde"]).ToShortDateString() + " " + ((DateTime)dr["t421_fechahasta"]).ToShortDateString() + "</td>");//Fechas
        //        sb.Append("    <td>" + dr["t421_destino"].ToString() + "</td>");//Destino
        //        sb.Append("    <td></td>");//Proyecto
        //        sb.Append("    <td>" + dr["t421_comentariopos"].ToString() + "</td>");//Comentario
        //        sb.Append("    <td>" + (((byte)dr["t421_ncdieta"] == 0) ? "" : dr["t421_ncdieta"].ToString()) + "</td>");//C
        //        sb.Append("    <td>" + (((byte)dr["t421_nmdieta"] == 0) ? "" : dr["t421_nmdieta"].ToString()) + "</td>");//M
        //        sb.Append("    <td>" + (((byte)dr["t421_nedieta"] == 0) ? "" : dr["t421_nedieta"].ToString()) + "</td>");//A
        //        sb.Append("    <td>" + (((byte)dr["t421_nadieta"] == 0) ? "" : dr["t421_nadieta"].ToString()) + "</td>");//E
        //        sb.Append("    <td></td>");//Importe
        //        sb.Append("    <td>" + (((short)dr["t421_nkms"] == 0) ? "" : short.Parse(dr["t421_nkms"].ToString()).ToString("#,###")) + "</td>");//Kms.
        //        sb.Append("    <td></td>");//Importe
        //        sb.Append("    <td></td>");//ECO
        //        //sb.Append("    <td>" + dr["t615_iddesplazamiento"].ToString() + "</td>");//ECO
        //        sb.Append("    <td>" + ((double.Parse(dr["t421_peajepark"].ToString()) == 0) ? "" : double.Parse(dr["t421_peajepark"].ToString()).ToString("N")) + "</td>");//Peajes
        //        sb.Append("    <td>" + ((double.Parse(dr["t421_comida"].ToString()) == 0) ? "" : double.Parse(dr["t421_comida"].ToString()).ToString("N")) + "</td>");//Comidas
        //        sb.Append("    <td>" + ((double.Parse(dr["t421_transporte"].ToString()) == 0) ? "" : double.Parse(dr["t421_transporte"].ToString()).ToString("N")) + "</td>");//Transp.
        //        sb.Append("    <td>" + ((double.Parse(dr["t421_hotel"].ToString()) == 0) ? "" : double.Parse(dr["t421_hotel"].ToString()).ToString("N")) + "</td>");//Hoteles
        //        sb.Append("    <td></td>");//Total
        //        sb.Append("</tr>" + (char)13);

        //        iFila++;
        //    }
        //    dr.Close();
        //    dr.Dispose();

        //    do
        //    {
        //        sb.Append("<tr id='" + iFila + "' bd='' eco='' ");
        //        sb.Append("idPSN='' ");
        //        sb.Append("comentario=\"\" ");
        //        sb.Append("style=\"height:20px;\" onclick=\"ii(this);ms(this,'FG');setProyReq(this);\">");
        //        sb.Append("    <td></td>");//Fechas
        //        sb.Append("    <td></td>");//Destino
        //        sb.Append("    <td></td>");//Proyecto
        //        sb.Append("    <td></td>");//Comentario
        //        sb.Append("    <td></td>");//C
        //        sb.Append("    <td></td>");//M
        //        sb.Append("    <td></td>");//E
        //        sb.Append("    <td></td>");//A
        //        sb.Append("    <td></td>");//Importe
        //        sb.Append("    <td></td>");//Kms.
        //        sb.Append("    <td></td>");//Importe
        //        sb.Append("    <td></td>");//ECO
        //        sb.Append("    <td></td>");//Peajes
        //        sb.Append("    <td></td>");//Comidas
        //        sb.Append("    <td></td>");//Transp.
        //        sb.Append("    <td></td>");//Hoteles
        //        sb.Append("    <td></td>");//Total
        //        sb.Append("</tr>" + (char)13);

        //        iFila++;
        //    } while (iFila < 15);

        //    sb.Append("</table>");

        //    return sb.ToString();
        //}
    }
}

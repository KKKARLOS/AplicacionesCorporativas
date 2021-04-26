using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using GASVI.DAL;

namespace GASVI.BLL
{
    public class POSICIONAPARCADA_NEGV
    {
        #region Propiedades y Atributos

        private int _t661_idposiciongv;
        public int t661_idposiciongv
        {
            get { return _t661_idposiciongv; }
            set { _t661_idposiciongv = value; }
        }

        private int _t660_idreferencia;
        public int t660_idreferencia
        {
            get { return _t660_idreferencia; }
            set { _t660_idreferencia = value; }
        }

        private DateTime? _t661_fechadesde;
        public DateTime? t661_fechadesde
        {
            get { return _t661_fechadesde; }
            set { _t661_fechadesde = value; }
        }

        private DateTime? _t661_fechahasta;
        public DateTime? t661_fechahasta
        {
            get { return _t661_fechahasta; }
            set { _t661_fechahasta = value; }
        }

        private string _t661_destino;
        public string t661_destino
        {
            get { return _t661_destino; }
            set { _t661_destino = value; }
        }

        private byte _t661_ncdieta;
        public byte t661_ncdieta
        {
            get { return _t661_ncdieta; }
            set { _t661_ncdieta = value; }
        }

        private byte _t661_nmdieta;
        public byte t661_nmdieta
        {
            get { return _t661_nmdieta; }
            set { _t661_nmdieta = value; }
        }

        private byte _t661_nadieta;
        public byte t661_nadieta
        {
            get { return _t661_nadieta; }
            set { _t661_nadieta = value; }
        }

        private byte _t661_nedieta;
        public byte t661_nedieta
        {
            get { return _t661_nedieta; }
            set { _t661_nedieta = value; }
        }

        private short _t661_nkms;
        public short t661_nkms
        {
            get { return _t661_nkms; }
            set { _t661_nkms = value; }
        }

        private decimal _t661_peajepark;
        public decimal t661_peajepark
        {
            get { return _t661_peajepark; }
            set { _t661_peajepark = value; }
        }

        private decimal _t661_comida;
        public decimal t661_comida
        {
            get { return _t661_comida; }
            set { _t661_comida = value; }
        }

        private decimal _t661_transporte;
        public decimal t661_transporte
        {
            get { return _t661_transporte; }
            set { _t661_transporte = value; }
        }

        private decimal _t661_hotel;
        public decimal t661_hotel
        {
            get { return _t661_hotel; }
            set { _t661_hotel = value; }
        }

        private int? _t615_iddesplazamiento;
        public int? t615_iddesplazamiento
        {
            get { return _t615_iddesplazamiento; }
            set { _t615_iddesplazamiento = value; }
        }

        private string _t661_comentariopos;
        public string t661_comentariopos
        {
            get { return _t661_comentariopos; }
            set { _t661_comentariopos = value; }
        }
        #endregion

        #region Constructor

        public POSICIONAPARCADA_NEGV()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static int Insert(SqlTransaction tr, int t660_idreferencia, Nullable<DateTime> t661_fechadesde, Nullable<DateTime> t661_fechahasta, string t661_destino, byte t661_ncdieta, byte t661_nmdieta, byte t661_nadieta, byte t661_nedieta, short t661_nkms, decimal t661_peajepark, decimal t661_comida, decimal t661_transporte, decimal t661_hotel, Nullable<int> t615_iddesplazamiento, string t661_comentariopos)
        {
            SqlParameter[] aParam = new SqlParameter[15];
            aParam[0] = new SqlParameter("@t660_idreferencia", SqlDbType.Int, 4);
            aParam[0].Value = t660_idreferencia;
            aParam[1] = new SqlParameter("@t661_fechadesde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t661_fechadesde;
            aParam[2] = new SqlParameter("@t661_fechahasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t661_fechahasta;
            aParam[3] = new SqlParameter("@t661_destino", SqlDbType.Text, 50);
            aParam[3].Value = t661_destino;
            aParam[4] = new SqlParameter("@t661_ncdieta", SqlDbType.TinyInt, 1);
            aParam[4].Value = t661_ncdieta;
            aParam[5] = new SqlParameter("@t661_nmdieta", SqlDbType.TinyInt, 1);
            aParam[5].Value = t661_nmdieta;
            aParam[6] = new SqlParameter("@t661_nadieta", SqlDbType.TinyInt, 1);
            aParam[6].Value = t661_nadieta;
            aParam[7] = new SqlParameter("@t661_nedieta", SqlDbType.TinyInt, 1);
            aParam[7].Value = t661_nedieta;
            aParam[8] = new SqlParameter("@t661_nkms", SqlDbType.SmallInt, 2);
            aParam[8].Value = t661_nkms;
            aParam[9] = new SqlParameter("@t661_peajepark", SqlDbType.Money, 4);
            aParam[9].Value = t661_peajepark;
            aParam[10] = new SqlParameter("@t661_comida", SqlDbType.Money, 4);
            aParam[10].Value = t661_comida;
            aParam[11] = new SqlParameter("@t661_transporte", SqlDbType.Money, 4);
            aParam[11].Value = t661_transporte;
            aParam[12] = new SqlParameter("@t661_hotel", SqlDbType.Money, 4);
            aParam[12].Value = t661_hotel;
            aParam[13] = new SqlParameter("@t615_iddesplazamiento", SqlDbType.Int, 4);
            aParam[13].Value = t615_iddesplazamiento;
            aParam[14] = new SqlParameter("@t661_comentariopos", SqlDbType.Text, 2147483647);
            aParam[14].Value = t661_comentariopos;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_POSICIONAPARCADA_NEGV_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_POSICIONAPARCADA_NEGV_I", aParam));
        }

        public static string CatalogoGastos(int nIDNota)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblGastos' class='MANO' cellSpacing='0' cellPadding='0' border='0' style='width:970px;text-align:right;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:130px; ' />");//Fechas
            sb.Append("    <col style='width:165px; ' />");//Destino
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

            SqlDataReader dr = DAL.POSICIONAPARCADA_NEGV.CatalogoGastos(null, nIDNota);
            int iFila = 0;

            while (dr.Read())
            {
                sb.Append("<tr id='" + iFila + "' bd='' ");
                sb.Append("comentario=\"" + Utilidades.escape(dr["t661_comentariopos"].ToString()) + "\" ");
                sb.Append("eco='" + dr["t615_iddesplazamiento"].ToString() + "' ");
                sb.Append("destino=\"" + Utilidades.escape(dr["t615_destino"].ToString()) + "\" ");
                sb.Append("ida='" + ((dr["t615_fechoraida"].ToString() == "") ? "" : dr["t615_fechoraida"].ToString().Substring(0, dr["t615_fechoraida"].ToString().Length - 3)) + "' ");
                sb.Append("vuelta='" + ((dr["t615_fechoravuelta"].ToString() == "") ? "" : dr["t615_fechoravuelta"].ToString().Substring(0, dr["t615_fechoravuelta"].ToString().Length - 3)) + "' ");
                sb.Append("style=\"height:20px;\" onclick=\"ii(this, event);ms(this,'FG')\">");
                if (dr["t661_fechadesde"].ToString() != "")
                    sb.Append("    <td style='text-align:left'>&nbsp;&nbsp;" + ((DateTime)dr["t661_fechadesde"]).ToShortDateString() + "&nbsp;&nbsp;" + ((DateTime)dr["t661_fechahasta"]).ToShortDateString() + "</td>");//Fechas
                else
                    sb.Append("    <td></td>");
                sb.Append("    <td style='text-align:left;'>" + dr["t661_destino"].ToString() + "</td>");//Destino
                if (dr["t661_comentariopos"].ToString() == "")
                    sb.Append("    <td style='text-align:left;' class='MA'></td>");//Comentario
                else
                {
                    sb.Append("<td style='text-align:left;' class='MA'><img src='../../Images/imgComGastoOn.gif' ");//Comentario
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/imgComGastoOn.gif' style='vertical-align:middle;' />&nbsp;Comentario] ");
                    sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(dr["t661_comentariopos"].ToString()) + "] ");
                    sb.Append("hideselects=[off]\" ");
                    sb.Append(" /></td>");//Comentario
                }
                sb.Append("    <td>" + (((byte)dr["t661_ncdieta"] == 0) ? "" : dr["t661_ncdieta"].ToString()) + "</td>");//C
                sb.Append("    <td>" + (((byte)dr["t661_nmdieta"] == 0) ? "" : dr["t661_nmdieta"].ToString()) + "</td>");//M
                sb.Append("    <td>" + (((byte)dr["t661_nedieta"] == 0) ? "" : dr["t661_nedieta"].ToString()) + "</td>");//E
                sb.Append("    <td>" + (((byte)dr["t661_nadieta"] == 0) ? "" : dr["t661_nadieta"].ToString()) + "</td>");//A
                sb.Append("    <td></td>");//Importe
                sb.Append("    <td>" + (((short)dr["t661_nkms"] == 0) ? "" : short.Parse(dr["t661_nkms"].ToString()).ToString("#,###")) + "</td>");//Kms.
                sb.Append("    <td></td>");//Importe
                sb.Append("    <td></td>");//ECO
                //sb.Append("    <td>" + dr["t615_iddesplazamiento"].ToString() + "</td>");//ECO
                sb.Append("    <td>" + ((double.Parse(dr["t661_peajepark"].ToString()) == 0) ? "" : double.Parse(dr["t661_peajepark"].ToString()).ToString("N")) + "</td>");//Peajes
                sb.Append("    <td>" + ((double.Parse(dr["t661_comida"].ToString()) == 0) ? "" : double.Parse(dr["t661_comida"].ToString()).ToString("N")) + "</td>");//Comidas
                sb.Append("    <td>" + ((double.Parse(dr["t661_transporte"].ToString()) == 0) ? "" : double.Parse(dr["t661_transporte"].ToString()).ToString("N")) + "</td>");//Transp.
                sb.Append("    <td>" + ((double.Parse(dr["t661_hotel"].ToString()) == 0) ? "" : double.Parse(dr["t661_hotel"].ToString()).ToString("N")) + "</td>");//Hoteles
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
                sb.Append("style=\"height:20px;\" onclick=\"ii(this,event);ms(this,'FG')\">");
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

        #endregion
    }

}
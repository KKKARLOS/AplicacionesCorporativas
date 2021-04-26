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
	/// Class	 : POSICIONAPARCADA_NMPGV
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T664_POSICIONAPARCADA_NMPGV
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	29/04/2011 9:52:30	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class POSICIONAPARCADA_NMPGV
	{
		#region Propiedades y Atributos

		private int _t664_idposiciongv;
		public int t664_idposiciongv
		{
			get {return _t664_idposiciongv;}
			set { _t664_idposiciongv = value ;}
		}

		private int _t663_idreferencia;
		public int t663_idreferencia
		{
			get {return _t663_idreferencia;}
			set { _t663_idreferencia = value ;}
		}

		private DateTime? _t664_fechadesde;
		public DateTime? t664_fechadesde
		{
			get {return _t664_fechadesde;}
			set { _t664_fechadesde = value ;}
		}

		private DateTime? _t664_fechahasta;
		public DateTime? t664_fechahasta
		{
			get {return _t664_fechahasta;}
			set { _t664_fechahasta = value ;}
		}

		private string _t664_destino;
		public string t664_destino
		{
			get {return _t664_destino;}
			set { _t664_destino = value ;}
		}

		private int? _t305_idproyectosubnodo;
		public int? t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private byte _t664_ncdieta;
		public byte t664_ncdieta
		{
			get {return _t664_ncdieta;}
			set { _t664_ncdieta = value ;}
		}

		private byte _t664_nmdieta;
		public byte t664_nmdieta
		{
			get {return _t664_nmdieta;}
			set { _t664_nmdieta = value ;}
		}

		private byte _t664_nadieta;
		public byte t664_nadieta
		{
			get {return _t664_nadieta;}
			set { _t664_nadieta = value ;}
		}

		private byte _t664_nedieta;
		public byte t664_nedieta
		{
			get {return _t664_nedieta;}
			set { _t664_nedieta = value ;}
		}

		private short _t664_nkms;
		public short t664_nkms
		{
			get {return _t664_nkms;}
			set { _t664_nkms = value ;}
		}

		private decimal _t664_peajepark;
		public decimal t664_peajepark
		{
			get {return _t664_peajepark;}
			set { _t664_peajepark = value ;}
		}

		private decimal _t664_comida;
		public decimal t664_comida
		{
			get {return _t664_comida;}
			set { _t664_comida = value ;}
		}

		private decimal _t664_transporte;
		public decimal t664_transporte
		{
			get {return _t664_transporte;}
			set { _t664_transporte = value ;}
		}

		private decimal _t664_hotel;
		public decimal t664_hotel
		{
			get {return _t664_hotel;}
			set { _t664_hotel = value ;}
		}

		private int? _t615_iddesplazamiento;
		public int? t615_iddesplazamiento
		{
			get {return _t615_iddesplazamiento;}
			set { _t615_iddesplazamiento = value ;}
		}

		private string _t664_comentariopos;
		public string t664_comentariopos
		{
			get {return _t664_comentariopos;}
			set { _t664_comentariopos = value ;}
		}
		#endregion

		#region Constructor

		public POSICIONAPARCADA_NMPGV() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T664_POSICIONAPARCADA_NMPGV.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	29/04/2011 9:52:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t663_idreferencia , Nullable<DateTime> t664_fechadesde , Nullable<DateTime> t664_fechahasta , string t664_destino , Nullable<int> t305_idproyectosubnodo , byte t664_ncdieta , byte t664_nmdieta , byte t664_nadieta , byte t664_nedieta , short t664_nkms , decimal t664_peajepark , decimal t664_comida , decimal t664_transporte , decimal t664_hotel , Nullable<int> t615_iddesplazamiento , string t664_comentariopos)
		{
			SqlParameter[] aParam = new SqlParameter[16];
			aParam[0] = new SqlParameter("@t663_idreferencia", SqlDbType.Int, 4);
			aParam[0].Value = t663_idreferencia;
			aParam[1] = new SqlParameter("@t664_fechadesde", SqlDbType.SmallDateTime, 4);
			aParam[1].Value = t664_fechadesde;
			aParam[2] = new SqlParameter("@t664_fechahasta", SqlDbType.SmallDateTime, 4);
			aParam[2].Value = t664_fechahasta;
			aParam[3] = new SqlParameter("@t664_destino", SqlDbType.Text, 50);
			aParam[3].Value = t664_destino;
			aParam[4] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[4].Value = t305_idproyectosubnodo;
			aParam[5] = new SqlParameter("@t664_ncdieta", SqlDbType.TinyInt, 1);
			aParam[5].Value = t664_ncdieta;
			aParam[6] = new SqlParameter("@t664_nmdieta", SqlDbType.TinyInt, 1);
			aParam[6].Value = t664_nmdieta;
			aParam[7] = new SqlParameter("@t664_nadieta", SqlDbType.TinyInt, 1);
			aParam[7].Value = t664_nadieta;
			aParam[8] = new SqlParameter("@t664_nedieta", SqlDbType.TinyInt, 1);
			aParam[8].Value = t664_nedieta;
			aParam[9] = new SqlParameter("@t664_nkms", SqlDbType.SmallInt, 2);
			aParam[9].Value = t664_nkms;
			aParam[10] = new SqlParameter("@t664_peajepark", SqlDbType.Money, 4);
			aParam[10].Value = t664_peajepark;
			aParam[11] = new SqlParameter("@t664_comida", SqlDbType.Money, 4);
			aParam[11].Value = t664_comida;
			aParam[12] = new SqlParameter("@t664_transporte", SqlDbType.Money, 4);
			aParam[12].Value = t664_transporte;
			aParam[13] = new SqlParameter("@t664_hotel", SqlDbType.Money, 4);
			aParam[13].Value = t664_hotel;
			aParam[14] = new SqlParameter("@t615_iddesplazamiento", SqlDbType.Int, 4);
			aParam[14].Value = t615_iddesplazamiento;
			aParam[15] = new SqlParameter("@t664_comentariopos", SqlDbType.Text, 2147483647);
			aParam[15].Value = t664_comentariopos;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_POSICIONAPARCADA_NMPGV_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_POSICIONAPARCADA_NMPGV_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Borra los registros de la tabla T664_POSICIONAPARCADA_NMPGV en función de una foreign key.
		/// </summary>
		/// <remarks>
		/// 	Creado por [sqladmin]	29/04/2011 9:52:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void DeleteByT663_idreferencia(SqlTransaction tr, int t663_idreferencia)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t663_idreferencia", SqlDbType.Int, 4);
			aParam[0].Value = t663_idreferencia;


			if (tr == null)
				SqlHelper.ExecuteNonQuery("GVT_POSICIONAPARCADA_NMPGV_DByT663_idreferencia", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_POSICIONAPARCADA_NMPGV_DByT663_idreferencia", aParam);
		}

        public static string CatalogoGastos(int nIDNota)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblGastos' class='MANO' cellpadding='0' style='width:970px;text-align:right;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:130px;' />");//Fechas
            sb.Append("    <col style='width:165px; ' />");//Destino
            sb.Append("    <col style='width:70px; ' />");//Proyecto
            sb.Append("    <col style='width:20px; ' />");//Comentario
            sb.Append("    <col style='width:25px' />");//C
            sb.Append("    <col style='width:25px' />");//M
            sb.Append("    <col style='width:25px' />");//E
            sb.Append("    <col style='width:25px' />");//A
            sb.Append("    <col style='width:65px' />");//Importe
            sb.Append("    <col style='width:40px' />");//Kms.
            sb.Append("    <col style='width:65px' />");//Importe
            sb.Append("    <col style='width:30px' />");//ECO
            sb.Append("    <col style='width:55px' />");//Peajes
            sb.Append("    <col style='width:55px' />");//Comidas
            sb.Append("    <col style='width:55px' />");//Transp.
            sb.Append("    <col style='width:55px' />");//Hoteles
            sb.Append("    <col style='width:65px' />");//Total
            sb.Append("</colgroup>" + (char)13);

            SqlDataReader dr = DAL.POSICIONAPARCADA_NMPGV.CatalogoGastos(null, nIDNota);
            int iFila = 0;

            while (dr.Read())
            {
                sb.Append("<tr id='" + iFila + "' bd='' ");
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("comentario=\"" + Utilidades.escape(dr["t664_comentariopos"].ToString()) + "\" ");
                sb.Append("eco='" + dr["t615_iddesplazamiento"].ToString() + "' ");
                sb.Append("destino=\"" + Utilidades.escape(dr["t615_destino"].ToString()) + "\" ");
                sb.Append("ida='" + ((dr["t615_fechoraida"].ToString() == "") ? "" : dr["t615_fechoraida"].ToString().Substring(0, dr["t615_fechoraida"].ToString().Length-3)) + "' ");
                sb.Append("vuelta='" + ((dr["t615_fechoravuelta"].ToString() == "") ? "" : dr["t615_fechoravuelta"].ToString().Substring(0, dr["t615_fechoravuelta"].ToString().Length - 3)) + "' ");

                sb.Append("style=\"height:20px;\" onclick=\"ii(this,event);ms(this,'FG');setProyReq(this);\">");
                if (dr["t664_fechadesde"] != DBNull.Value)
                    sb.Append("    <td style='text-align:left'>&nbsp;&nbsp;" + ((DateTime)dr["t664_fechadesde"]).ToShortDateString() + "&nbsp;&nbsp;" + ((DateTime)dr["t664_fechahasta"]).ToShortDateString() + "</td>");//Fechas
                else
                    sb.Append("    <td></td>");
                sb.Append("    <td style='text-align:left;'>" + dr["t664_destino"].ToString() + "</td>");//Destino
                if (dr["t305_idproyectosubnodo"].ToString() != "")
                   // sb.Append("    <td>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");//Proyecto
                    sb.Append("<td style='text-align:right;'><nobr class='NBR W65 MA' ondblclick='setProyectoGasto(this.parentNode)' onselectstart='return false;' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;margin-right:15px;' />Informaci&oacute;n] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable_Proyecto"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + ((dr["Sexo_Aprobador"].ToString() == "V") ? "Aprobador" : "Aprobadora") + ":</label>" + dr["Aprobador"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString() + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</nobr></td>");
                else
                    sb.Append("    <td class='MA' style=\"background-image:url(../../images/imgRequerido.gif);background-repeat:no-repeat;text-align:right;\"></td>");//Proyecto
                if (dr["t664_comentariopos"].ToString() == "")
                    sb.Append("    <td class='MA' style='text-align:left;'></td>");//Comentario
                else
                {
                    sb.Append("<td class='MA' style='text-align:left;'><img src='../../Images/imgComGastoOn.gif' ");//Comentario
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/imgComGastoOn.gif' style='vertical-align:middle;' />&nbsp;Comentario] ");
                    sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(dr["t664_comentariopos"].ToString()) + "] ");
                    sb.Append("hideselects=[off]\" ");
                    sb.Append(" /></td>");//Comentario
                } 
                sb.Append("    <td>" + (((byte)dr["t664_ncdieta"] == 0) ? "" : dr["t664_ncdieta"].ToString()) + "</td>");//C
                sb.Append("    <td>" + (((byte)dr["t664_nmdieta"] == 0) ? "" : dr["t664_nmdieta"].ToString()) + "</td>");//M
                sb.Append("    <td>" + (((byte)dr["t664_nedieta"] == 0) ? "" : dr["t664_nedieta"].ToString()) + "</td>");//A
                sb.Append("    <td>" + (((byte)dr["t664_nadieta"] == 0) ? "" : dr["t664_nadieta"].ToString()) + "</td>");//E
                sb.Append("    <td></td>");//Importe
                sb.Append("    <td>" + (((short)dr["t664_nkms"] == 0) ? "" : short.Parse(dr["t664_nkms"].ToString()).ToString("#,###")) + "</td>");//Kms.
                sb.Append("    <td></td>");//Importe
                sb.Append("    <td></td>");//ECO
                //sb.Append("    <td>" + dr["t615_iddesplazamiento"].ToString() + "</td>");//ECO
                sb.Append("    <td>" + ((double.Parse(dr["t664_peajepark"].ToString()) == 0) ? "" : double.Parse(dr["t664_peajepark"].ToString()).ToString("N")) + "</td>");//Peajes
                sb.Append("    <td>" + ((double.Parse(dr["t664_comida"].ToString()) == 0) ? "" : double.Parse(dr["t664_comida"].ToString()).ToString("N")) + "</td>");//Comidas
                sb.Append("    <td>" + ((double.Parse(dr["t664_transporte"].ToString()) == 0) ? "" : double.Parse(dr["t664_transporte"].ToString()).ToString("N")) + "</td>");//Transp.
                sb.Append("    <td>" + ((double.Parse(dr["t664_hotel"].ToString()) == 0) ? "" : double.Parse(dr["t664_hotel"].ToString()).ToString("N")) + "</td>");//Hoteles
                sb.Append("    <td></td>");//Total
                sb.Append("</tr>" + (char)13);

                iFila++;
            }
            dr.Close();
            dr.Dispose();

            do
            {
                sb.Append("<tr id='" + iFila + "' bd='' eco='' ");
                sb.Append("idPSN='' ");
                sb.Append("comentario=\"\" ");
                sb.Append("eco='' ");
                sb.Append("destino=\"\" ");
                sb.Append("ida='' ");
                sb.Append("vuelta='' ");
                sb.Append("style=\"height:20px;\" onclick=\"ii(this,event);ms(this,'FG');setProyReq(this);\">");
                sb.Append("    <td></td>");//Fechas
                sb.Append("    <td></td>");//Destino
                sb.Append("    <td class='MA'></td>");//Proyecto
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

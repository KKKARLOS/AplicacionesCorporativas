using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class PosicionGV
    {

        /// <summary>
        /// Summary description for PosicionGV
        /// </summary>
		#region Private Variables
		private Int32 _t420_idreferencia;
		private DateTime _t421_fechadesde;
		private DateTime _t421_fechahasta;
		private String _t421_destino;
		private String _t421_comentariopos;
		private Byte _t421_ncdieta;
		private Byte _t421_nmdieta;
		private Byte _t421_nedieta;
		private Byte _t421_nadieta;
		private Int16 _t421_nkms;
		private Int32 _t615_iddesplazamiento;
		private Decimal _t421_peajepark;
		private Decimal _t421_comida;
		private Decimal _t421_transporte;
		private Decimal _t421_hotel;

		#endregion

		#region Public Properties
		public Int32 t420_idreferencia
		{
			get{return _t420_idreferencia;}
			set{_t420_idreferencia = value;}
		}

		public DateTime t421_fechadesde
		{
			get{return _t421_fechadesde;}
			set{_t421_fechadesde = value;}
		}

		public DateTime t421_fechahasta
		{
			get{return _t421_fechahasta;}
			set{_t421_fechahasta = value;}
		}

		public String t421_destino
		{
			get{return _t421_destino;}
			set{_t421_destino = value;}
		}

		public String t421_comentariopos
		{
			get{return _t421_comentariopos;}
			set{_t421_comentariopos = value;}
		}

		public Byte t421_ncdieta
		{
			get{return _t421_ncdieta;}
			set{_t421_ncdieta = value;}
		}

		public Byte t421_nmdieta
		{
			get{return _t421_nmdieta;}
			set{_t421_nmdieta = value;}
		}

		public Byte t421_nedieta
		{
			get{return _t421_nedieta;}
			set{_t421_nedieta = value;}
		}

		public Byte t421_nadieta
		{
			get{return _t421_nadieta;}
			set{_t421_nadieta = value;}
		}

		public Int16 t421_nkms
		{
			get{return _t421_nkms;}
			set{_t421_nkms = value;}
		}

		public Int32 t615_iddesplazamiento
		{
			get{return _t615_iddesplazamiento;}
			set{_t615_iddesplazamiento = value;}
		}

		public Decimal t421_peajepark
		{
			get{return _t421_peajepark;}
			set{_t421_peajepark = value;}
		}

		public Decimal t421_comida
		{
			get{return _t421_comida;}
			set{_t421_comida = value;}
		}

		public Decimal t421_transporte
		{
			get{return _t421_transporte;}
			set{_t421_transporte = value;}
		}

		public Decimal t421_hotel
		{
			get{return _t421_hotel;}
			set{_t421_hotel = value;}
		}


        #endregion

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Contrato
    {

        /// <summary>
        /// Summary description for Contrato
        /// </summary>
		#region Private Variables
		private Int32 _t306_idcontrato;
		private Int32 _t302_idcliente_contrato;
		private String _t302_denominacion;
		private String _t377_denominacion;
		private Int32 _t377_idextension;
		private Decimal _importe_servicio;
		private Decimal _importe_producto;
		private Decimal _pendiente_servicio;
		private Decimal _pendiente_producto;
        private Boolean _con_extensiones;

		#endregion

		#region Public Properties
		public Int32 t306_idcontrato
		{
			get{return _t306_idcontrato;}
			set{_t306_idcontrato = value;}
		}

		public Int32 t302_idcliente_contrato
		{
			get{return _t302_idcliente_contrato;}
			set{_t302_idcliente_contrato = value;}
		}

		public String t302_denominacion
		{
			get{return _t302_denominacion;}
			set{_t302_denominacion = value;}
		}

		public String t377_denominacion
		{
			get{return _t377_denominacion;}
			set{_t377_denominacion = value;}
		}

		public Int32 t377_idextension
		{
			get{return _t377_idextension;}
			set{_t377_idextension = value;}
		}

		public Decimal importe_servicio
		{
			get{return _importe_servicio;}
			set{_importe_servicio = value;}
		}

		public Decimal importe_producto
		{
			get{return _importe_producto;}
			set{_importe_producto = value;}
		}

		public Decimal pendiente_servicio
		{
			get{return _pendiente_servicio;}
			set{_pendiente_servicio = value;}
		}

		public Decimal pendiente_producto
		{
			get{return _pendiente_producto;}
			set{_pendiente_producto = value;}
		}

        public Boolean con_extensiones
        {
            get { return _con_extensiones; }
            set { _con_extensiones = value; }
        }


        #endregion

	}
}

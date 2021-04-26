using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Motivo
    {

        /// <summary>
        /// Summary description for Motivo
        /// </summary>
		#region Private Variables
		private Byte _t423_idmotivo;
		private String _t423_denominacion;
		private Boolean _t423_estado;
		private Int32 _t423_cuenta;

		#endregion

		#region Public Properties
		public Byte t423_idmotivo
		{
			get{return _t423_idmotivo;}
			set{_t423_idmotivo = value;}
		}

		public String t423_denominacion
		{
			get{return _t423_denominacion;}
			set{_t423_denominacion = value;}
		}

		public Boolean t423_estado
		{
			get{return _t423_estado;}
			set{_t423_estado = value;}
		}

		public Int32 t423_cuenta
		{
			get{return _t423_cuenta;}
			set{_t423_cuenta = value;}
		}


        #endregion

	}
}

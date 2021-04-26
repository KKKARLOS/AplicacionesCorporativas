using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Moneda
    {

        /// <summary>
        /// Summary description for Moneda
        /// </summary>
		#region Private Variables
		private String _t422_idmoneda;
		private String _t422_denominacion;
		private Boolean _t422_estado;

		#endregion

		#region Public Properties
		public String t422_idmoneda
		{
			get{return _t422_idmoneda;}
			set{_t422_idmoneda = value;}
		}

		public String t422_denominacion
		{
			get{return _t422_denominacion;}
			set{_t422_denominacion = value;}
		}

		public Boolean t422_estado
		{
			get{return _t422_estado;}
			set{_t422_estado = value;}
		}


        #endregion

	}
}

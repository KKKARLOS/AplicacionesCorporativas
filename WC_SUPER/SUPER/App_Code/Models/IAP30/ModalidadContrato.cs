using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ModalidadContrato
    {

        /// <summary>
        /// Summary description for ModalidadContrato
        /// </summary>
		#region Private Variables
		private Byte _t316_idmodalidad;
		private String _t316_denominacion;

		#endregion

		#region Public Properties
		public Byte t316_idmodalidad
		{
			get{return _t316_idmodalidad;}
			set{_t316_idmodalidad = value;}
		}

		public String t316_denominacion
		{
			get{return _t316_denominacion;}
			set{_t316_denominacion = value;}
		}


        #endregion

	}
}

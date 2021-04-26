using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{
   
   public class ConceptoTareaPendiente
    {

        /// <summary>
        /// Summary description for ConceptoTareaPendiente
        /// </summary>
		#region Private Variables
		private Byte _ta209_idconceptotareapendiente;
		private String _ta209_denominacion;

		#endregion

		#region Public Properties
		public Byte ta209_idconceptotareapendiente
		{
			get{return _ta209_idconceptotareapendiente;}
			set{_ta209_idconceptotareapendiente = value;}
		}

		public String ta209_denominacion
		{
			get{return _ta209_denominacion;}
			set{_ta209_denominacion = value;}
		}


        #endregion

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Horizontal
    {

        /// <summary>
        /// Summary description for Horizontal
        /// </summary>
		#region Private Variables
		private Int32 _identificador;
		private String _denominacion;

		#endregion

		#region Public Properties
		public Int32 identificador
		{
            get { return _identificador; }
            set { _identificador = value; }
		}

		public String denominacion
		{
			get{return _denominacion;}
			set{_denominacion = value;}
		}


        #endregion

	}
}

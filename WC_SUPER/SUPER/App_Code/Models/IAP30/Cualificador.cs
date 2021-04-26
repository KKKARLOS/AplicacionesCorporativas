using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Cualificador
    {

        /// <summary>
        /// Summary description for Cualificador
        /// </summary>
		#region Private Variables
		private Int32 _identificador;
		private String _denominacion;
		private Byte _orden;

		#endregion

		#region Public Properties
		public Int32 identificador
		{
			get{return _identificador;}
			set{_identificador = value;}
		}

		public String denominacion
		{
			get{return _denominacion;}
			set{_denominacion = value;}
		}

		public Byte orden
		{
			get{return _orden;}
			set{_orden = value;}
		}


        #endregion

	}
}

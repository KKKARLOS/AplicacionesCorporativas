using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Historial
    {

        /// <summary>
        /// Summary description for Historial
        /// </summary>
		#region Private Variables
		private String _t431_denominacion;
		private DateTime _t659_fecha;
		private String _Profesional;
		private String _t659_motivo;

		#endregion

		#region Public Properties
		public String t431_denominacion
		{
			get{return _t431_denominacion;}
			set{_t431_denominacion = value;}
		}

		public DateTime t659_fecha
		{
			get{return _t659_fecha;}
			set{_t659_fecha = value;}
		}

		public String Profesional
		{
			get{return _Profesional;}
			set{_Profesional = value;}
		}

		public String t659_motivo
		{
			get{return _t659_motivo;}
			set{_t659_motivo = value;}
		}


        #endregion

	}
}

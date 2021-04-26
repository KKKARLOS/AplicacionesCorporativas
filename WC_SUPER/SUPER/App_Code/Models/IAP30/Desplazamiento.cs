using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DESPLAZAMIENTO
    {

        /// <summary>
        /// Summary description for DESPLAZAMIENTO
        /// </summary>
		#region Private Variables
		private Int32 _t615_iddesplazamiento;
		private String _t615_destino;
		private String _t615_observaciones;
		private DateTime _t615_fechoraida;
		private DateTime _t615_fechoravuelta;
		private Int32 _numero_usos;

		#endregion

		#region Public Properties
		public Int32 t615_iddesplazamiento
		{
			get{return _t615_iddesplazamiento;}
			set{_t615_iddesplazamiento = value;}
		}

		public String t615_destino
		{
			get{return _t615_destino;}
			set{_t615_destino = value;}
		}

		public String t615_observaciones
		{
			get{return _t615_observaciones;}
			set{_t615_observaciones = value;}
		}

		public DateTime t615_fechoraida
		{
			get{return _t615_fechoraida;}
			set{_t615_fechoraida = value;}
		}

		public DateTime t615_fechoravuelta
		{
			get{return _t615_fechoravuelta;}
			set{_t615_fechoravuelta = value;}
		}

		public Int32 numero_usos
		{
			get{return _numero_usos;}
			set{_numero_usos = value;}
		}


        #endregion

	}
}

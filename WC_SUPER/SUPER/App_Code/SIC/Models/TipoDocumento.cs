using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{
   
   public class TipoDocumento
    {

        /// <summary>
        /// Summary description for TipoDocumento
        /// </summary>
		#region Private Variables
		private Byte _ta211_idtipodocumento;
		private String _ta211_denominacion;

		#endregion

		#region Public Properties
		public Byte ta211_idtipodocumento
		{
			get{return _ta211_idtipodocumento;}
			set{_ta211_idtipodocumento = value;}
		}

		public String ta211_denominacion
		{
			get{return _ta211_denominacion;}
			set{_ta211_denominacion = value;}
		}


        #endregion

	}
}

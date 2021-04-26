using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.ADM.SIC.Models
{
   
   public class TipoDocumento
    {

        /// <summary>
        /// Summary description for TipoDocumento
        /// </summary>
		#region Private Variables
		private Int16 _ta211_idtipodocumento;
		private String _ta211_denominacion;
        private Boolean _ta211_estadoactiva;
        private Int32 _ta211_orden;

        #endregion

        #region Public Properties
        public Int16 ta211_idtipodocumento
		{
			get{return _ta211_idtipodocumento;}
			set{_ta211_idtipodocumento = value;}
		}

		public String ta211_denominacion
		{
			get{return _ta211_denominacion;}
			set{_ta211_denominacion = value;}
		}

        public Boolean ta211_estadoactiva
        {
            get { return _ta211_estadoactiva; }
            set { _ta211_estadoactiva = value; }
        }

        public Int32 ta211_orden
        {
            get { return _ta211_orden; }
            set { _ta211_orden = value; }
        }
        #endregion

    }
}

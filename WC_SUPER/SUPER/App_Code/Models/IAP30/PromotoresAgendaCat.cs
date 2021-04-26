using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class PromotoresAgendaCat
    {

        /// <summary>
        /// Summary description for PromotoresAgendaCat
        /// </summary>
		#region Private Variables
		private Int32 _t458_idPlanif;
		private Int32 _t001_idficepi_mod;
        private Int32 _t001_idficepi;
		private String _Motivo;
		private DateTime _t458_fechoraini;
		private DateTime _t458_fechorafin;
		private String _Profesional;
		private String _t001_codred_promotor;

		#endregion

		#region Public Properties
		public Int32 t458_idPlanif
		{
			get{return _t458_idPlanif;}
			set{_t458_idPlanif = value;}
		}

		public Int32 t001_idficepi_mod
		{
			get{return _t001_idficepi_mod;}
			set{_t001_idficepi_mod = value;}
		}

        public Int32 t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

		public String Motivo
		{
			get{return _Motivo;}
			set{_Motivo = value;}
		}

		public DateTime t458_fechoraini
		{
			get{return _t458_fechoraini;}
			set{_t458_fechoraini = value;}
		}

		public DateTime t458_fechorafin
		{
			get{return _t458_fechorafin;}
			set{_t458_fechorafin = value;}
		}

		public String Profesional
		{
			get{return _Profesional;}
			set{_Profesional = value;}
		}

		public String t001_codred_promotor
		{
			get{return _t001_codred_promotor;}
			set{_t001_codred_promotor = value;}
		}


        #endregion

	}
}

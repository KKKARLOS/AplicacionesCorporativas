using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DesgloseCalendario
    {

        /// <summary>
        /// Summary description for DesgloseCalendario
        /// </summary>
		#region Private Variables
		private Int32 _t066_idcal;
		private DateTime _t067_dia;
		private Single _t067_horas;
        private Double _t067_horasD;
		private Int32 _t067_festivo;

		#endregion

		#region Public Properties
		public Int32 t066_idcal
		{
			get{return _t066_idcal;}
			set{_t066_idcal = value;}
		}

		public DateTime t067_dia
		{
			get{return _t067_dia;}
			set{_t067_dia = value;}
		}

		public Single t067_horas
		{
			get{return _t067_horas;}
			set{_t067_horas = value;}
		}

        public Double t067_horasD
        {
            get { return _t067_horasD; }
            set { _t067_horasD = value; }
        }

		public Int32 t067_festivo
		{
			get{return _t067_festivo;}
			set{_t067_festivo = value;}
		}


        #endregion

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareaProyectoTecnico
    {

        /// <summary>
        /// Summary description for TareaProyectoTecnico
        /// </summary>
		#region Private Variables
		private Int32 _t331_IDPT;
		private DateTime _dVigIni;
		private DateTime _dVigFin;
		private DateTime _dPlanIni;
		private DateTime _dPlanFin;
		private Double _nPlanEstimado;
		private DateTime _dPrevFin;
		private Double _nPrevEstimado;
		private Decimal _nPresupuesto;

		#endregion

		#region Public Properties
		public Int32 t331_IDPT
		{
			get{return _t331_IDPT;}
			set{_t331_IDPT = value;}
		}

		public DateTime dVigIni
		{
			get{return _dVigIni;}
			set{_dVigIni = value;}
		}

		public DateTime dVigFin
		{
			get{return _dVigFin;}
			set{_dVigFin = value;}
		}

		public DateTime dPlanIni
		{
			get{return _dPlanIni;}
			set{_dPlanIni = value;}
		}

		public DateTime dPlanFin
		{
			get{return _dPlanFin;}
			set{_dPlanFin = value;}
		}

		public Double nPlanEstimado
		{
			get{return _nPlanEstimado;}
			set{_nPlanEstimado = value;}
		}

		public DateTime dPrevFin
		{
			get{return _dPrevFin;}
			set{_dPrevFin = value;}
		}

		public Double nPrevEstimado
		{
			get{return _nPrevEstimado;}
			set{_nPrevEstimado = value;}
		}

		public Decimal nPresupuesto
		{
			get{return _nPresupuesto;}
			set{_nPresupuesto = value;}
		}


        #endregion

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareaIAPS
    {

        /// <summary>
        /// Summary description for TareaIAPS
        /// </summary>
		#region Private Variables
        private Int32 _t332_idtarea;
		private DateTime _dPrimerConsumo;
		private DateTime _dUltimoConsumo;
		private DateTime _dFinEstimado;
		private Double _nTotalEstimado;
		private Double _nConsumidoHoras;
		private Double _nConsumidoJornadas;
		private Double _nPendienteEstimado;
		private Int32 _nAvanceTeorico;

		#endregion

		#region Public Properties
        public Int32 t332_idtarea
		{
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
		}

		public DateTime dPrimerConsumo
		{
			get{return _dPrimerConsumo;}
			set{_dPrimerConsumo = value;}
		}

		public DateTime dUltimoConsumo
		{
			get{return _dUltimoConsumo;}
			set{_dUltimoConsumo = value;}
		}

		public DateTime dFinEstimado
		{
			get{return _dFinEstimado;}
			set{_dFinEstimado = value;}
		}

		public Double nTotalEstimado
		{
			get{return _nTotalEstimado;}
			set{_nTotalEstimado = value;}
		}

		public Double nConsumidoHoras
		{
			get{return _nConsumidoHoras;}
			set{_nConsumidoHoras = value;}
		}

		public Double nConsumidoJornadas
		{
			get{return _nConsumidoJornadas;}
			set{_nConsumidoJornadas = value;}
		}

		public Double nPendienteEstimado
		{
			get{return _nPendienteEstimado;}
			set{_nPendienteEstimado = value;}
		}

		public Int32 nAvanceTeorico
		{
			get{return _nAvanceTeorico;}
			set{_nAvanceTeorico = value;}
		}


        #endregion

	}
}

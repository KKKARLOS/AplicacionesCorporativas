using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoIAPFact
    {

        /// <summary>
        /// Summary description for ConsumoIAPFact
        /// </summary>
		#region Private Variables
		private Int32 _t301_idproyecto;
		private String _t305_seudonimo;
		private Int32 _t332_idtarea;
		private String _t332_destarea;
		private Boolean _t332_facturable;
		private Int32 _t332_orden;
		private String _t331_despt;
		private String _t335_desactividad;
		private String _t334_desfase;
		private Double _t332_etpl;
		private Double _t336_etp;
		private Double _horas_planificadas_periodo;
		private Double _horas_tecnico_periodo;
		private Double _horas_otros_periodo;
		private Double _horas_total_periodo;
		private Double _horas_planificadas_finperiodo;
		private Double _horas_tecnico_finperiodo;
		private Double _horas_otros_finperiodo;
		private Double _horas_total_finperiodo;

		#endregion

		#region Public Properties
		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

		public String t305_seudonimo
		{
			get{return _t305_seudonimo;}
			set{_t305_seudonimo = value;}
		}

		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public String t332_destarea
		{
			get{return _t332_destarea;}
			set{_t332_destarea = value;}
		}

		public Boolean t332_facturable
		{
			get{return _t332_facturable;}
			set{_t332_facturable = value;}
		}

		public Int32 t332_orden
		{
			get{return _t332_orden;}
			set{_t332_orden = value;}
		}

		public String t331_despt
		{
			get{return _t331_despt;}
			set{_t331_despt = value;}
		}

		public String t335_desactividad
		{
			get{return _t335_desactividad;}
			set{_t335_desactividad = value;}
		}

		public String t334_desfase
		{
			get{return _t334_desfase;}
			set{_t334_desfase = value;}
		}

		public Double t332_etpl
		{
			get{return _t332_etpl;}
			set{_t332_etpl = value;}
		}

		public Double t336_etp
		{
			get{return _t336_etp;}
			set{_t336_etp = value;}
		}

		public Double horas_planificadas_periodo
		{
			get{return _horas_planificadas_periodo;}
			set{_horas_planificadas_periodo = value;}
		}

		public Double horas_tecnico_periodo
		{
			get{return _horas_tecnico_periodo;}
			set{_horas_tecnico_periodo = value;}
		}

		public Double horas_otros_periodo
		{
			get{return _horas_otros_periodo;}
			set{_horas_otros_periodo = value;}
		}

		public Double horas_total_periodo
		{
			get{return _horas_total_periodo;}
			set{_horas_total_periodo = value;}
		}

		public Double horas_planificadas_finperiodo
		{
			get{return _horas_planificadas_finperiodo;}
			set{_horas_planificadas_finperiodo = value;}
		}

		public Double horas_tecnico_finperiodo
		{
			get{return _horas_tecnico_finperiodo;}
			set{_horas_tecnico_finperiodo = value;}
		}

		public Double horas_otros_finperiodo
		{
			get{return _horas_otros_finperiodo;}
			set{_horas_otros_finperiodo = value;}
		}

		public Double horas_total_finperiodo
		{
			get{return _horas_total_finperiodo;}
			set{_horas_total_finperiodo = value;}
		}


        #endregion

	}
}

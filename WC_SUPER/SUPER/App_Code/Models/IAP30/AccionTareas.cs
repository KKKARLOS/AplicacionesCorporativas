using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{   
   public class AccionTareas
    {
        /// <summary>
        /// Summary description for AccionTareas
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private String _t332_destarea;
		private Int32 _t332_orden;
		private Double _t332_etpl;
        private DateTime? _t332_fipl;
        private DateTime? _t332_ffpl;
		private Double _t332_etpr;
        private DateTime? _t332_ffpr;
		private Double _Consumo;
		private Boolean _t332_avanceauto;
		private Double _t332_avance;
		private String _Estado;
		private Int32 _num_proyecto;
		private String _nom_proyecto;
		private Int32 _t331_idpt;
		private String _t331_despt;
		private Int32 _t334_idfase;
		private String _t334_desfase;
		private Int32 _t335_idactividad;
		private String _t335_desactividad;
        private Int32 _t383_idaccion;
        private Int32 _t382_idasunto;
        private String _accionBD;
		#endregion

		#region Public Properties

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

		public Int32 t332_orden
		{
			get{return _t332_orden;}
			set{_t332_orden = value;}
		}

		public Double t332_etpl
		{
			get{return _t332_etpl;}
			set{_t332_etpl = value;}
		}

        public DateTime? t332_fipl
		{
			get{return _t332_fipl;}
			set{_t332_fipl = value;}
		}

        public DateTime? t332_ffpl
		{
			get{return _t332_ffpl;}
			set{_t332_ffpl = value;}
		}

		public Double t332_etpr
		{
			get{return _t332_etpr;}
			set{_t332_etpr = value;}
		}

        public DateTime? t332_ffpr
		{
			get{return _t332_ffpr;}
			set{_t332_ffpr = value;}
		}

		public Double Consumo
		{
			get{return _Consumo;}
			set{_Consumo = value;}
		}

		public Boolean t332_avanceauto
		{
			get{return _t332_avanceauto;}
			set{_t332_avanceauto = value;}
		}

		public Double t332_avance
		{
			get{return _t332_avance;}
			set{_t332_avance = value;}
		}

		public String Estado
		{
			get{return _Estado;}
			set{_Estado = value;}
		}

		public Int32 num_proyecto
		{
			get{return _num_proyecto;}
			set{_num_proyecto = value;}
		}

		public String nom_proyecto
		{
			get{return _nom_proyecto;}
			set{_nom_proyecto = value;}
		}

		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
		}

		public String t331_despt
		{
			get{return _t331_despt;}
			set{_t331_despt = value;}
		}

		public Int32 t334_idfase
		{
			get{return _t334_idfase;}
			set{_t334_idfase = value;}
		}

		public String t334_desfase
		{
			get{return _t334_desfase;}
			set{_t334_desfase = value;}
		}

		public Int32 t335_idactividad
		{
			get{return _t335_idactividad;}
			set{_t335_idactividad = value;}
		}

		public String t335_desactividad
		{
			get{return _t335_desactividad;}
			set{_t335_desactividad = value;}
		}
        public String accionBD
        {
            get { return _accionBD; }
            set { _accionBD = value; }
        }
        public Int32 t382_idasunto
        {
            get { return _t382_idasunto; }
            set { _t382_idasunto = value; }
        }
        public Int32 t383_idaccion
        {
            get { return _t383_idaccion; }
            set { _t383_idaccion = value; }
        }
        #endregion
	}
}

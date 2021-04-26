using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareaIAPMasiva
    {

        /// <summary>
        /// Summary description for TareaIAPMasiva
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private Int32 _t331_idpt;
		private String _denominacion;
		private Int32 _t323_regfes;
		private Int32 _t323_regjornocompleta;
		private Int32 _t331_obligaest;
		private Int32 _t334_idfase;
		private Int32 _t335_idactividad;
		private Int32 _t332_idtarea;
		private Byte _t332_estado;
		private Boolean _t332_impiap;
		private String _t301_estado;
        private DateTime? _fechaInicioImpPermitida;
        private DateTime? _fechaFinImpPermitida;

        #endregion

        #region Public Properties
        public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
		}

		public String denominacion
		{
			get{return _denominacion;}
			set{_denominacion = value;}
		}

		public Int32 t323_regfes
		{
			get{return _t323_regfes;}
			set{_t323_regfes = value;}
		}

		public Int32 t323_regjornocompleta
		{
			get{return _t323_regjornocompleta;}
			set{_t323_regjornocompleta = value;}
		}

		public Int32 t331_obligaest
		{
			get{return _t331_obligaest;}
			set{_t331_obligaest = value;}
		}

		public Int32 t334_idfase
		{
			get{return _t334_idfase;}
			set{_t334_idfase = value;}
		}

		public Int32 t335_idactividad
		{
			get{return _t335_idactividad;}
			set{_t335_idactividad = value;}
		}

		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public Byte t332_estado
		{
			get{return _t332_estado;}
			set{_t332_estado = value;}
		}

		public Boolean t332_impiap
		{
			get{return _t332_impiap;}
			set{_t332_impiap = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}

        public DateTime? fechaInicioImpPermitida
        {
            get { return _fechaInicioImpPermitida; }
            set { _fechaInicioImpPermitida = value; }
        }
        public DateTime? fechaFinImpPermitida
        {
            get { return _fechaFinImpPermitida; }
            set { _fechaFinImpPermitida = value; }
        }

        #endregion

    }
}
